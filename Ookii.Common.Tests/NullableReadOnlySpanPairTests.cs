namespace Ookii.Common.Tests;

[TestClass]
public class NullableReadOnlySpanPairTests
{
    [TestMethod]
    public void TestEmpty()
    {
        var target = new NullableReadOnlySpanPair<char, byte>();
        Assert.IsFalse(target.HasValue);
        Assert.IsFalse(target.TryGetValue(out var value));
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
            _ = (ReadOnlySpanPair<char, byte>)target;
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
        var target = NullableReadOnlySpanPair.Create("test".AsSpan(), new ReadOnlySpan<byte>([1, 2, 3]));
        Assert.IsTrue(target.HasValue);
        Assert.IsTrue(target.TryGetValue(out var value));
        Assert.AreEqual("test", value.First.ToString());
        Assert.AreEqual("test", target.Value.First.ToString());
        Assert.AreEqual("test", ((ReadOnlySpanPair<char, byte>)target).First.ToString());
        CollectionAssert.AreEqual(new byte[] { 1, 2, 3 }, value.Second.ToArray());
        CollectionAssert.AreEqual(new byte[] { 1, 2, 3 }, target.Value.Second.ToArray());

        // Implicit conversion
        target = ReadOnlySpanPair.Create("test2".AsSpan(), new ReadOnlySpan<byte>([4, 5, 6]));
        Assert.IsTrue(target.HasValue);
        Assert.IsTrue(target.TryGetValue(out var first, out var second));
        Assert.AreEqual("test2", first.ToString());
        CollectionAssert.AreEqual(new byte[] { 4, 5, 6 }, second.ToArray());
    }

    [TestMethod]
    public void TestOr()
    {
        var target = NullableReadOnlySpanPair.Create("test".AsSpan(), new ReadOnlySpan<byte>([1, 2, 3]));
        var (first, second) = target.Or(new("test2".AsSpan(), [4, 5, 6]));
        Assert.AreEqual("test", first.ToString());
        CollectionAssert.AreEqual(new byte[] { 1, 2, 3 }, second.ToArray());

        target = default;
        (first, second) = target.Or(new("test2".AsSpan(), [4, 5, 6]));
        Assert.AreEqual("test2", first.ToString());
        CollectionAssert.AreEqual(new byte[] { 4, 5, 6 }, second.ToArray());
    }
}
