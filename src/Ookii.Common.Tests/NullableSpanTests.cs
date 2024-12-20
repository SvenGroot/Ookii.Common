using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;

namespace Ookii.Common.Tests;

[TestClass]
public class NullableSpanTests
{
    [TestMethod]
    public void TestEmpty()
    {
        var target = new NullableSpan<char>();
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
            _ = (Span<char>)target;
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
        var target = new NullableSpan<char>([.. "test"]);
        Assert.IsTrue(target.HasValue);
        Assert.AreEqual("test", target.Value.ToString());
        Assert.AreEqual("test", ((Span<char>)target).ToString());
        Assert.AreEqual("test", target.ToString());

        // Implicit conversion
        target = "test2".ToArray().AsSpan();
        Assert.IsTrue(target.HasValue);
        Assert.AreEqual("test2", target.Value.ToString());
        Assert.AreEqual("test2", ((Span<char>)target).ToString());
        Assert.AreEqual("test2", target.ToString());
    }

    [TestMethod]
    public void TestTryGetValue()
    {
        var target = new NullableSpan<char>();
        Assert.IsFalse(target.TryGetValue(out _));

        target = "test".ToArray().AsSpan();
        Assert.IsTrue(target.TryGetValue(out var value));
        Assert.AreEqual("test", value.ToString());
    }

    [TestMethod]
    public void TestOr()
    {
        var target = new NullableSpan<char>();
        Assert.AreEqual("test", target.GetValueOrDefault([.. "test"]).ToString());
        Assert.AreEqual("test", target.GetValueOrElse(() => "test".ToArray().AsSpan()).ToString());

        target = "test".ToArray().AsSpan();
        Assert.AreEqual("test", target.GetValueOrDefault([.. "test2"]).ToString());
        Assert.AreEqual("test", target.GetValueOrElse(() => "test2".ToArray().AsSpan()).ToString());
    }

    [TestMethod]
    public void TestMap()
    {
        var target = new NullableSpan<char>();
        Assert.IsFalse(target.Map(val => Encoding.UTF8.GetBytes(val.ToArray()).AsSpan()).HasValue);
        Assert.IsNull(target.Map(val => val.Length));
        Assert.IsNull(target.Map(val => val.ToString()));

        target = "123".ToArray().AsSpan();
        Assert.IsTrue(target.Map(val => Encoding.UTF8.GetBytes(val.ToArray()).AsSpan()).HasValue);
        Assert.AreEqual(3, target.Map(val => val.Length));
        Assert.AreEqual("123", target.Map(val => val.ToString()));
    }

    [TestMethod]
    public void TestEquality()
    {
        var left = new NullableSpan<char>([.. "test"]);
        var right = left;
        Assert.IsTrue(left == right);
        Assert.IsFalse(left != right);

        right = new NullableSpan<char>([.. "test2"]);
        Assert.IsFalse(left == right);
        Assert.IsTrue(left != right);

        left = default;
        Assert.IsFalse(left == right);
        Assert.IsTrue(left != right);
        Assert.IsTrue(left == default);
        Assert.IsFalse(left != default);
    }
}
