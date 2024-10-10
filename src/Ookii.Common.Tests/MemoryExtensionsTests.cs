using System.Globalization;

namespace Ookii.Common.Tests;

[TestClass]
public class MemoryExtensionsTests
{
    [TestMethod]
    public void TestSplitOnceSpan()
    {
        var target = "test1,test2,test3".AsSpan().SplitOnce(',');
        Assert.IsTrue(target.HasValue);
        Assert.AreEqual("test1", target.Value.Item1.ToString());
        Assert.AreEqual("test2,test3", target.Value.Item2.ToString());

        target = "test1,test2,test3".AsSpan().SplitOnce(",".AsSpan(), StringComparison.Ordinal);
        Assert.IsTrue(target.HasValue);
        Assert.AreEqual("test1", target.Value.Item1.ToString());
        Assert.AreEqual("test2,test3", target.Value.Item2.ToString());

        target = "test1Abctest2Abctest3".AsSpan().SplitOnce("aBC".AsSpan(), StringComparison.OrdinalIgnoreCase);
        Assert.IsTrue(target.HasValue);
        Assert.AreEqual("test1", target.Value.Item1.ToString());
        Assert.AreEqual("test2Abctest3", target.Value.Item2.ToString());

        target = "test1Atest2Atest3".AsSpan().SplitOnce("a".AsSpan(), StringComparison.InvariantCulture);
        Assert.IsFalse(target.HasValue);

        target = "test1Atest2Atest3".AsSpan().SplitOnce("a".AsSpan(), CultureInfo.GetCultureInfo("fr-FR"), CompareOptions.IgnoreCase);
        Assert.IsTrue(target.HasValue);
        Assert.AreEqual("test1", target.Value.Item1.ToString());
        Assert.AreEqual("test2Atest3", target.Value.Item2.ToString());

        target = "test1Atest2Atest3".AsSpan().SplitOnce("a".AsSpan(), CultureInfo.InvariantCulture, CompareOptions.IgnoreCase);
        Assert.IsTrue(target.HasValue);
        Assert.AreEqual("test1", target.Value.Item1.ToString());
        Assert.AreEqual("test2Atest3", target.Value.Item2.ToString());
    }

    [TestMethod]
    public void TestSplitOnceMemory()
    {
        var target = "test1,test2,test3".AsMemory().SplitOnce(',');
        Assert.IsTrue(target.HasValue);
        Assert.AreEqual("test1", target.Value.Item1.ToString());
        Assert.AreEqual("test2,test3", target.Value.Item2.ToString());

        target = "test1,test2,test3".AsMemory().SplitOnce(",".AsSpan(), StringComparison.Ordinal);
        Assert.IsTrue(target.HasValue);
        Assert.AreEqual("test1", target.Value.Item1.ToString());
        Assert.AreEqual("test2,test3", target.Value.Item2.ToString());

        target = "test1Abctest2Abctest3".AsMemory().SplitOnce("aBC".AsSpan(), StringComparison.OrdinalIgnoreCase);
        Assert.IsTrue(target.HasValue);
        Assert.AreEqual("test1", target.Value.Item1.ToString());
        Assert.AreEqual("test2Abctest3", target.Value.Item2.ToString());

        target = "test1Atest2Atest3".AsMemory().SplitOnce("a".AsSpan(), StringComparison.InvariantCulture);
        Assert.IsFalse(target.HasValue);

        target = "test1Atest2Atest3".AsMemory().SplitOnce("a".AsSpan(), CultureInfo.GetCultureInfo("fr-FR"), CompareOptions.IgnoreCase);
        Assert.IsTrue(target.HasValue);
        Assert.AreEqual("test1", target.Value.Item1.ToString());
        Assert.AreEqual("test2Atest3", target.Value.Item2.ToString());

        target = "test1Atest2Atest3".AsMemory().SplitOnce("a".AsSpan(), CultureInfo.InvariantCulture, CompareOptions.IgnoreCase);
        Assert.IsTrue(target.HasValue);
        Assert.AreEqual("test1", target.Value.Item1.ToString());
        Assert.AreEqual("test2Atest3", target.Value.Item2.ToString());
    }

    [TestMethod]
    public void TestSplitOnceLastSpan()
    {
        var target = "test1,test2,test3".AsSpan().SplitOnceLast(',');
        Assert.IsTrue(target.HasValue);
        Assert.AreEqual("test1,test2", target.Value.Item1.ToString());
        Assert.AreEqual("test3", target.Value.Item2.ToString());

        target = "test1,test2,test3".AsSpan().SplitOnceLast(",".AsSpan(), StringComparison.Ordinal);
        Assert.IsTrue(target.HasValue);
        Assert.AreEqual("test1,test2", target.Value.Item1.ToString());
        Assert.AreEqual("test3", target.Value.Item2.ToString());

        target = "test1Abctest2Abctest3".AsSpan().SplitOnceLast("aBC".AsSpan(), StringComparison.OrdinalIgnoreCase);
        Assert.IsTrue(target.HasValue);
        Assert.AreEqual("test1Abctest2", target.Value.Item1.ToString());
        Assert.AreEqual("test3", target.Value.Item2.ToString());

        target = "test1Atest2Atest3".AsSpan().SplitOnceLast("a".AsSpan(), StringComparison.InvariantCulture);
        Assert.IsFalse(target.HasValue);

        target = "test1Atest2Atest3".AsSpan().SplitOnceLast("a".AsSpan(), CultureInfo.GetCultureInfo("fr-FR"), CompareOptions.IgnoreCase);
        Assert.IsTrue(target.HasValue);
        Assert.AreEqual("test1Atest2", target.Value.Item1.ToString());
        Assert.AreEqual("test3", target.Value.Item2.ToString());

        target = "test1Atest2Atest3".AsSpan().SplitOnceLast("a".AsSpan(), CultureInfo.InvariantCulture, CompareOptions.IgnoreCase);
        Assert.IsTrue(target.HasValue);
        Assert.AreEqual("test1Atest2", target.Value.Item1.ToString());
        Assert.AreEqual("test3", target.Value.Item2.ToString());
    }

    [TestMethod]
    public void TestSplitOnceLastMemory()
    {
        var target = "test1,test2,test3".AsMemory().SplitOnceLast(',');
        Assert.IsTrue(target.HasValue);
        Assert.AreEqual("test1,test2", target.Value.Item1.ToString());
        Assert.AreEqual("test3", target.Value.Item2.ToString());

        target = "test1,test2,test3".AsMemory().SplitOnceLast(",".AsSpan(), StringComparison.Ordinal);
        Assert.IsTrue(target.HasValue);
        Assert.AreEqual("test1,test2", target.Value.Item1.ToString());
        Assert.AreEqual("test3", target.Value.Item2.ToString());

        target = "test1Abctest2Abctest3".AsMemory().SplitOnceLast("aBC".AsSpan(), StringComparison.OrdinalIgnoreCase);
        Assert.IsTrue(target.HasValue);
        Assert.AreEqual("test1Abctest2", target.Value.Item1.ToString());
        Assert.AreEqual("test3", target.Value.Item2.ToString());

        target = "test1Atest2Atest3".AsMemory().SplitOnceLast("a".AsSpan(), StringComparison.InvariantCulture);
        Assert.IsFalse(target.HasValue);

        target = "test1Atest2Atest3".AsMemory().SplitOnceLast("a".AsSpan(), CultureInfo.GetCultureInfo("fr-FR"), CompareOptions.IgnoreCase);
        Assert.IsTrue(target.HasValue);
        Assert.AreEqual("test1Atest2", target.Value.Item1.ToString());
        Assert.AreEqual("test3", target.Value.Item2.ToString());

        target = "test1Atest2Atest3".AsMemory().SplitOnceLast("a".AsSpan(), CultureInfo.InvariantCulture, CompareOptions.IgnoreCase);
        Assert.IsTrue(target.HasValue);
        Assert.AreEqual("test1Atest2", target.Value.Item1.ToString());
        Assert.AreEqual("test3", target.Value.Item2.ToString());
    }

    [TestMethod]
    public void TestStripPrefixSpan()
    {
        var target = "foobarbaz".AsSpan().StripPrefix("foo".AsSpan());
        Assert.IsTrue(target.HasValue);
        Assert.AreEqual("barbaz", target.Value.ToString());

        target = "foobarbaz".AsSpan().StripPrefix("FOO".AsSpan(), StringComparison.OrdinalIgnoreCase);
        Assert.IsTrue(target.HasValue);
        Assert.AreEqual("barbaz", target.Value.ToString());

        target = "foobarbaz".AsSpan().StripPrefix("FOO".AsSpan(), StringComparison.InvariantCulture);
        Assert.IsFalse(target.HasValue);

        target = "foobarbaz".AsSpan().StripPrefix("FOO".AsSpan(), CultureInfo.GetCultureInfo("fr-FR"), CompareOptions.IgnoreCase);
        Assert.IsTrue(target.HasValue);
        Assert.AreEqual("barbaz", target.Value.ToString());

        target = "foobarbaz".AsSpan().StripPrefix("FOO".AsSpan(), CultureInfo.InvariantCulture, CompareOptions.IgnoreCase);
        Assert.IsTrue(target.HasValue);
        Assert.AreEqual("barbaz", target.Value.ToString());
    }

    [TestMethod]
    public void TestStripPrefixMemory()
    {
        var target = "foobarbaz".AsMemory().StripPrefix("foo".AsSpan());
        Assert.IsTrue(target.HasValue);
        Assert.AreEqual("barbaz", target.Value.ToString());

        target = "foobarbaz".AsMemory().StripPrefix("FOO".AsSpan(), StringComparison.OrdinalIgnoreCase);
        Assert.IsTrue(target.HasValue);
        Assert.AreEqual("barbaz", target.Value.ToString());

        target = "foobarbaz".AsMemory().StripPrefix("FOO".AsSpan(), StringComparison.InvariantCulture);
        Assert.IsFalse(target.HasValue);

        target = "foobarbaz".AsMemory().StripPrefix("FOO".AsSpan(), CultureInfo.GetCultureInfo("fr-FR"), CompareOptions.IgnoreCase);
        Assert.IsTrue(target.HasValue);
        Assert.AreEqual("barbaz", target.Value.ToString());

        target = "foobarbaz".AsMemory().StripPrefix("FOO".AsSpan(), CultureInfo.InvariantCulture, CompareOptions.IgnoreCase);
        Assert.IsTrue(target.HasValue);
        Assert.AreEqual("barbaz", target.Value.ToString());
    }

    [TestMethod]
    public void TestStripSuffixSpan()
    {
        var target = "foobarbaz".AsSpan().StripSuffix("baz".AsSpan());
        Assert.IsTrue(target.HasValue);
        Assert.AreEqual("foobar", target.Value.ToString());

        target = "foobarbaz".AsSpan().StripSuffix("BAZ".AsSpan(), StringComparison.OrdinalIgnoreCase);
        Assert.IsTrue(target.HasValue);
        Assert.AreEqual("foobar", target.Value.ToString());

        target = "foobarbaz".AsSpan().StripSuffix("BAZ".AsSpan(), StringComparison.InvariantCulture);
        Assert.IsFalse(target.HasValue);

        target = "foobarbaz".AsSpan().StripSuffix("BAZ".AsSpan(), CultureInfo.GetCultureInfo("fr-FR"), CompareOptions.IgnoreCase);
        Assert.IsTrue(target.HasValue);
        Assert.AreEqual("foobar", target.Value.ToString());
    }

    [TestMethod]
    public void TestStripSuffixMemory()
    {
        var target = "foobarbaz".AsMemory().StripSuffix("baz".AsSpan());
        Assert.IsTrue(target.HasValue);
        Assert.AreEqual("foobar", target.Value.ToString());

        target = "foobarbaz".AsMemory().StripSuffix("BAZ".AsSpan(), StringComparison.OrdinalIgnoreCase);
        Assert.IsTrue(target.HasValue);
        Assert.AreEqual("foobar", target.Value.ToString());

        target = "foobarbaz".AsMemory().StripSuffix("BAZ".AsSpan(), StringComparison.InvariantCulture);
        Assert.IsFalse(target.HasValue);

        target = "foobarbaz".AsMemory().StripSuffix("BAZ".AsSpan(), CultureInfo.GetCultureInfo("fr-FR"), CompareOptions.IgnoreCase);
        Assert.IsTrue(target.HasValue);
        Assert.AreEqual("foobar", target.Value.ToString());

        target = "foobarbaz".AsMemory().StripSuffix("BAZ".AsSpan(), CultureInfo.InvariantCulture, CompareOptions.IgnoreCase);
        Assert.IsTrue(target.HasValue);
        Assert.AreEqual("foobar", target.Value.ToString());
    }
}
