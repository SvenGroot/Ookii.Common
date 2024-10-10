namespace Ookii.Common.Tests;

[TestClass]
public class NullableExtensionsTest
{
    [TestMethod]
    public void TestMap()
    {
        int? value = null;
        Assert.IsNull(value.Map(x => x * 2));
        Assert.IsFalse(value.Map(x => x.ToString().AsSpan()).HasValue);
        Assert.IsNull(value.Map(x => x.ToString()!));

        value = 5;
        Assert.AreEqual(10, value.Map(x => x * 2));
        Assert.IsTrue(value.Map(x => x.ToString().AsSpan()).HasValue);
        Assert.AreEqual("5", value.Map(x => x.ToString()!));
    }

    [TestMethod]
    public void TestMapClass()
    {
        string? value = null;
        Assert.IsNull(value.Map(x => x + "test"));
        Assert.IsFalse(value.Map(x => x.AsSpan()).HasValue);
        Assert.IsNull(value.Map(x => x.Length));

        value = "test";
        Assert.AreEqual("testtest", value.Map(x => x + "test"));
        Assert.IsTrue(value.Map(x => x.AsSpan()).HasValue);
        Assert.AreEqual(4, value.Map(x => x.Length));
    }
}
