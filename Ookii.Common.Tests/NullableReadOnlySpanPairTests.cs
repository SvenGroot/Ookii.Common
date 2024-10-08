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

    [TestMethod]
    public void TestToString()
    {
        var target = NullableReadOnlySpanPair.Create("test".AsSpan(), new ReadOnlySpan<byte>([1, 2, 3]));
        Assert.AreEqual("(test, System.ReadOnlySpan<Byte>[3])", target.ToString());

        target = default;
        Assert.AreEqual("", target.ToString());
    }

    [TestMethod]
    public void TestMap()
    {
        var target = NullableReadOnlySpanPair.Create("test".AsSpan(), new ReadOnlySpan<byte>([1, 2, 3]));
        Assert.AreEqual("testSystem.ReadOnlySpan<Byte>[3]", target.Map((value) => value.First.ToString() + value.Second.ToString()));
        var result = target.Map((value) => (value.First.ToString(), value.Second.ToArray()));
        Assert.IsNotNull(result);
        Assert.AreEqual("test", result.Value.Item1);
        CollectionAssert.AreEqual(new byte[] { 1, 2, 3 }, result.Value.Item2);
        var result2 = target.Map(value => ReadOnlySpanPair.Create(value.Second, value.First));
        Assert.IsTrue(result2.HasValue);
        CollectionAssert.AreEqual(new byte[] { 1, 2, 3 }, result2.Value.First.ToArray());
        Assert.AreEqual("test", result2.Value.Second.ToString());

        target = default;
        Assert.IsNull(target.Map((value) => value.First.ToString() + value.Second.ToString()));
        Assert.IsNull(target.Map((value) => (value.First.ToString(), value.Second.ToArray())));
        Assert.IsFalse(target.Map(value => ReadOnlySpanPair.Create(value.Second, value.First)).HasValue);
    }

    [TestMethod]
    public void TestUnzip()
    {
        var target = NullableReadOnlySpanPair.Create("test".AsSpan(), new ReadOnlySpan<byte>([1, 2, 3]));
        target.Unzip(out var first, out var second);
        Assert.AreEqual("test", first.ToString());
        CollectionAssert.AreEqual(new byte[] { 1, 2, 3 }, second.Value.ToArray());

        target = default;
        target.Unzip(out first, out second);
        Assert.IsFalse(first.HasValue);
        Assert.IsFalse(second.HasValue);
    }
}
