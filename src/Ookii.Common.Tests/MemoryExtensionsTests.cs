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
    public void TestSplitOnceAnySpan()
    {
        var target = "test1,test2;test3".AsSpan().SplitOnceAny([',', ';']);
        Assert.IsTrue(target.HasValue);
        Assert.AreEqual("test1", target.Value.Item1.ToString());
        Assert.AreEqual("test2;test3", target.Value.Item2.ToString());

        target = "test1;test2,test3".AsSpan().SplitOnceAny([',', ';']);
        Assert.IsTrue(target.HasValue);
        Assert.AreEqual("test1", target.Value.Item1.ToString());
        Assert.AreEqual("test2,test3", target.Value.Item2.ToString());

        target = "test1test2test3".AsSpan().SplitOnceAny([',', ';']);
        Assert.IsFalse(target.HasValue);
    }

    [TestMethod]
    public void TestSplitOnceAnyMemory()
    {
        var target = "test1,test2;test3".AsMemory().SplitOnceAny([',', ';']);
        Assert.IsNotNull(target);
        Assert.AreEqual("test1", target.Value.Item1.ToString());
        Assert.AreEqual("test2;test3", target.Value.Item2.ToString());

        target = "test1;test2,test3".AsMemory().SplitOnceAny([',', ';']);
        Assert.IsNotNull(target);
        Assert.AreEqual("test1", target.Value.Item1.ToString());
        Assert.AreEqual("test2,test3", target.Value.Item2.ToString());

        target = "test1test2test3".AsMemory().SplitOnceAny([',', ';']);
        Assert.IsNull(target);
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
    public void TestSplitOnceLastAnySpan()
    {
        var target = "test1,test2;test3".AsSpan().SplitOnceLastAny([',', ';']);
        Assert.IsTrue(target.HasValue);
        Assert.AreEqual("test1,test2", target.Value.Item1.ToString());
        Assert.AreEqual("test3", target.Value.Item2.ToString());

        target = "test1;test2,test3".AsSpan().SplitOnceLastAny([',', ';']);
        Assert.IsTrue(target.HasValue);
        Assert.AreEqual("test1;test2", target.Value.Item1.ToString());
        Assert.AreEqual("test3", target.Value.Item2.ToString());

        target = "test1test2test3".AsSpan().SplitOnceLastAny([',', ';']);
        Assert.IsFalse(target.HasValue);
    }

    [TestMethod]
    public void TestSplitOnceLastAnyMemory()
    {
        var target = "test1,test2;test3".AsMemory().SplitOnceLastAny([',', ';']);
        Assert.IsNotNull(target);
        Assert.AreEqual("test1,test2", target.Value.Item1.ToString());
        Assert.AreEqual("test3", target.Value.Item2.ToString());

        target = "test1;test2,test3".AsMemory().SplitOnceLastAny([',', ';']);
        Assert.IsNotNull(target);
        Assert.AreEqual("test1;test2", target.Value.Item1.ToString());
        Assert.AreEqual("test3", target.Value.Item2.ToString());

        target = "test1test2test3".AsMemory().SplitOnceLastAny([',', ';']);
        Assert.IsNull(target);
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

    [TestMethod]
    public void TestSplitSpan()
    {
        SplitHelper("test1", ",", StringSplitOptions.None, ["test1"]);
        SplitHelper("test1,test2", ",", StringSplitOptions.None, ["test1", "test2"]);
        SplitHelper("test1,test2,test3", ",", StringSplitOptions.None, ["test1", "test2", "test3"]);
        SplitHelper(",test1,,test2,test3,", ",", StringSplitOptions.None, ["", "test1", "", "test2", "test3", ""]);
        SplitHelper(",test1,,test2,test3,", ",", StringSplitOptions.RemoveEmptyEntries, ["test1", "test2", "test3"]);
        SplitHelper("", ",", StringSplitOptions.None, [""]);
        SplitHelper("", ",", StringSplitOptions.RemoveEmptyEntries, []);

#if NET6_0_OR_GREATER
        SplitHelper(", test1 ,  ,test2,test3,", ",", StringSplitOptions.TrimEntries, ["", "test1", "", "test2", "test3", ""]);

        SplitHelper(", test1 ,  ,test2,test3,", ",", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries,
            ["test1", "test2", "test3"]);

        SplitHelper(" ", ",", StringSplitOptions.TrimEntries, [""]);
        SplitHelper(" ", ",", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries, []);
#endif
    }

    [TestMethod]
    public void TestSplitMemory()
    {
        SplitMemoryHelper("test1", ",", StringSplitOptions.None, ["test1"]);
        SplitMemoryHelper("test1,test2", ",", StringSplitOptions.None, ["test1", "test2"]);
        SplitMemoryHelper("test1,test2,test3", ",", StringSplitOptions.None, ["test1", "test2", "test3"]);
        SplitMemoryHelper(",test1,,test2,test3,", ",", StringSplitOptions.None, ["", "test1", "", "test2", "test3", ""]);
        SplitMemoryHelper(",test1,,test2,test3,", ",", StringSplitOptions.RemoveEmptyEntries, ["test1", "test2", "test3"]);
        SplitMemoryHelper("", ",", StringSplitOptions.None, [""]);
        SplitMemoryHelper("", ",", StringSplitOptions.RemoveEmptyEntries, []);

#if NET6_0_OR_GREATER
        SplitMemoryHelper(", test1 ,  ,test2,test3,", ",", StringSplitOptions.TrimEntries, ["", "test1", "", "test2", "test3", ""]);

        SplitMemoryHelper(", test1 ,  ,test2,test3,", ",", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries,
            ["test1", "test2", "test3"]);

        SplitMemoryHelper(" ", ",", StringSplitOptions.TrimEntries, [""]);
        SplitMemoryHelper(" ", ",", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries, []);
#endif
    }


    private static void SplitHelper(string value, string separator, StringSplitOptions options, string[] expected)
    {
        var actual = new List<string>();
        foreach (var item in value.AsSpan().Split(separator.AsSpan(), options))
        {
            actual.Add(item.ToString());
        }

        CollectionAssert.AreEqual(expected, actual);
    }

    private static void SplitMemoryHelper(string value, string separator, StringSplitOptions options, string[] expected)
    {
        var actual = new List<string>();
        foreach (var item in value.AsMemory().Split(separator.AsSpan(), options))
        {
            actual.Add(item.ToString());
        }

        CollectionAssert.AreEqual(expected, actual);
    }
}
