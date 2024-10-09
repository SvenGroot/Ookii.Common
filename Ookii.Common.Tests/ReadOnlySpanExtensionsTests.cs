using System.Globalization;

namespace Ookii.Common.Tests;

[TestClass]
public class ReadOnlySpanExtensionsTests
{
    [TestMethod]
    public void TestSplitOnce()
    {
        var target = "test1,test2,test3".AsSpan().SplitOnce(',');
        Assert.IsTrue(target.HasValue);
        Assert.AreEqual("test1", target.Value.First.ToString());
        Assert.AreEqual("test2,test3", target.Value.Second.ToString());

        target = "test1,test2,test3".AsSpan().SplitOnce(",".AsSpan(), StringComparison.Ordinal);
        Assert.IsTrue(target.HasValue);
        Assert.AreEqual("test1", target.Value.First.ToString());
        Assert.AreEqual("test2,test3", target.Value.Second.ToString());

        target = "test1Abctest2Abctest3".AsSpan().SplitOnce("aBC".AsSpan(), StringComparison.OrdinalIgnoreCase);
        Assert.IsTrue(target.HasValue);
        Assert.AreEqual("test1", target.Value.First.ToString());
        Assert.AreEqual("test2Abctest3", target.Value.Second.ToString());

        target = "test1Atest2Atest3".AsSpan().SplitOnce("a".AsSpan(), StringComparison.InvariantCulture);
        Assert.IsFalse(target.HasValue);

        target = "test1Atest2Atest3".AsSpan().SplitOnce("a".AsSpan(), CultureInfo.GetCultureInfo("fr-FR"), CompareOptions.IgnoreCase);
        Assert.IsTrue(target.HasValue);
        Assert.AreEqual("test1", target.Value.First.ToString());
        Assert.AreEqual("test2Atest3", target.Value.Second.ToString());
    }

    [TestMethod]
    public void TestSplitOnceLast()
    {
        var target = "test1,test2,test3".AsSpan().SplitOnceLast(',');
        Assert.IsTrue(target.HasValue);
        Assert.AreEqual("test1,test2", target.Value.First.ToString());
        Assert.AreEqual("test3", target.Value.Second.ToString());

        target = "test1,test2,test3".AsSpan().SplitOnceLast(",".AsSpan(), StringComparison.Ordinal);
        Assert.IsTrue(target.HasValue);
        Assert.AreEqual("test1,test2", target.Value.First.ToString());
        Assert.AreEqual("test3", target.Value.Second.ToString());

        target = "test1Abctest2Abctest3".AsSpan().SplitOnceLast("aBC".AsSpan(), StringComparison.OrdinalIgnoreCase);
        Assert.IsTrue(target.HasValue);
        Assert.AreEqual("test1Abctest2", target.Value.First.ToString());
        Assert.AreEqual("test3", target.Value.Second.ToString());

        target = "test1Atest2Atest3".AsSpan().SplitOnceLast("a".AsSpan(), StringComparison.InvariantCulture);
        Assert.IsFalse(target.HasValue);

        target = "test1Atest2Atest3".AsSpan().SplitOnceLast("a".AsSpan(), CultureInfo.GetCultureInfo("fr-FR"), CompareOptions.IgnoreCase);
        Assert.IsTrue(target.HasValue);
        Assert.AreEqual("test1Atest2", target.Value.First.ToString());
        Assert.AreEqual("test3", target.Value.Second.ToString());
    }

    [TestMethod]
    public void TestStripPrefix()
    {
        var target = "foobar".AsSpan().StripPrefix("foo".AsSpan());
        Assert.IsTrue(target.HasValue);
        Assert.AreEqual("bar", target.Value.ToString());

        target = "foobar".AsSpan().StripPrefix("FOO".AsSpan(), StringComparison.OrdinalIgnoreCase);
        Assert.IsTrue(target.HasValue);
        Assert.AreEqual("bar", target.Value.ToString());

        target = "foobar".AsSpan().StripPrefix("FOO".AsSpan(), StringComparison.InvariantCulture);
        Assert.IsFalse(target.HasValue);

        target = "foobar".AsSpan().StripPrefix("FOO".AsSpan(), CultureInfo.GetCultureInfo("fr-FR"), CompareOptions.IgnoreCase);
        Assert.IsTrue(target.HasValue);
        Assert.AreEqual("bar", target.Value.ToString());
    }

    [TestMethod]
    public void TestStripSuffix()
    {
        var target = "foobar".AsSpan().StripSuffix("bar".AsSpan());
        Assert.IsTrue(target.HasValue);
        Assert.AreEqual("bar", target.Value.ToString());

        target = "foobar".AsSpan().StripSuffix("BAR".AsSpan(), StringComparison.OrdinalIgnoreCase);
        Assert.IsTrue(target.HasValue);
        Assert.AreEqual("bar", target.Value.ToString());

        target = "foobar".AsSpan().StripSuffix("BAR".AsSpan(), StringComparison.InvariantCulture);
        Assert.IsFalse(target.HasValue);

        target = "foobar".AsSpan().StripSuffix("BAR".AsSpan(), CultureInfo.GetCultureInfo("fr-FR"), CompareOptions.IgnoreCase);
        Assert.IsTrue(target.HasValue);
        Assert.AreEqual("bar", target.Value.ToString());
    }
}
