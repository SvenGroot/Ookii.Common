using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;

namespace Ookii.Common.Tests;

[TestClass]
public class NullableReadOnlySpanTests
{
    [TestMethod]
    public void TestEmpty()
    {
        var target = new NullableReadOnlySpan<char>();
        Assert.IsFalse(target.HasValue);
        Assert.AreEqual(string.Empty, target.ToString());
        try
        {
            _ = target.Value;
            Assert.Fail();
        }
        catch (InvalidOperationException)
        {
        }

        try
        {
            _ = (ReadOnlySpan<char>)target;
            Assert.Fail();
        }
        catch (InvalidOperationException)
        {
        }

        // Default
        target = default;
        Assert.IsFalse(target.HasValue);
    }

    [TestMethod]
    public void TestConversion()
    {
        // Explicit construction
        var target = new NullableReadOnlySpan<char>("test".AsSpan());
        Assert.IsTrue(target.HasValue);
        Assert.AreEqual("test", target.Value.ToString());
        Assert.AreEqual("test", ((ReadOnlySpan<char>)target).ToString());
        Assert.AreEqual("test", target.ToString());

        // Implicit conversion
        target = "test2".AsSpan();
        Assert.IsTrue(target.HasValue);
        Assert.AreEqual("test2", target.Value.ToString());
        Assert.AreEqual("test2", ((ReadOnlySpan<char>)target).ToString());
        Assert.AreEqual("test2", target.ToString());
    }

    [TestMethod]
    public void TestTryGetValue()
    {
        var target = new NullableReadOnlySpan<char>();
        Assert.IsFalse(target.TryGetValue(out _));

        target = "test".AsSpan();
        Assert.IsTrue(target.TryGetValue(out var value));
        Assert.AreEqual("test", value.ToString());
    }

    [TestMethod]
    public void TestOr()
    {
        var target = new NullableReadOnlySpan<char>();
        Assert.AreEqual("test", target.GetValueOrDefault("test".AsSpan()).ToString());
        Assert.AreEqual("test", target.GetValueOrElse(() => "test".AsSpan()).ToString());

        target = "test".AsSpan();
        Assert.AreEqual("test", target.GetValueOrDefault("test2".AsSpan()).ToString());
        Assert.AreEqual("test", target.GetValueOrElse(() => "test2".AsSpan()).ToString());
    }

    [TestMethod]
    public void TestMap()
    {
        var target = new NullableReadOnlySpan<char>();
        Assert.IsFalse(target.Map(val => Encoding.UTF8.GetBytes(val.ToArray()).AsSpan()).HasValue);
        Assert.IsNull(target.Map(val => val.Length));
        Assert.IsNull(target.Map(val => val.ToString()));

        target = "123".AsSpan();
        Assert.IsTrue(target.Map(val => Encoding.UTF8.GetBytes(val.ToArray()).AsSpan()).HasValue);
        Assert.AreEqual(3, target.Map(val => val.Length));
        Assert.AreEqual("123", target.Map(val => val.ToString()));
    }

    [TestMethod]
    public void TestEquality()
    {
        var left = new NullableReadOnlySpan<char>("test".AsSpan());
        var right = new NullableReadOnlySpan<char>("test".AsSpan());
        Assert.IsTrue(left == right);
        Assert.IsFalse(left != right);

        right = new NullableReadOnlySpan<char>("test2".AsSpan());
        Assert.IsFalse(left == right);
        Assert.IsTrue(left != right);

        left = default;
        Assert.IsFalse(left == right);
        Assert.IsTrue(left != right);
        Assert.IsTrue(left == default);
        Assert.IsFalse(left != default);
    }
}