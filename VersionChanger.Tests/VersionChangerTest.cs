using ProjectVersionInfo;

namespace VersionChanger.Tests;

[TestClass]
public class VersionChangerTest
{
    [TestMethod]
    public void TestMajorChange()
    {
        // arrange
        var version = GetRandomVersion();
        var major = version.Major;
        // act
        var newVersionRelease = new VersionIncrementer().GetNewVersion(version, "Release");
        var newVersionDebug = new VersionIncrementer().GetNewVersion(version, "Debug");
        // assert
        Assert.AreEqual(major, newVersionRelease.Major);
        Assert.AreEqual(major, newVersionDebug.Major);
    }

    [TestMethod]
    public void TestMinorChange()
    {
        // arrange
        var version = GetRandomVersion();
        var minor = version.Minor;
        // act
        var newVersionRelease = new VersionIncrementer().GetNewVersion(version, "Release");
        var newVersionDebug = new VersionIncrementer().GetNewVersion(version, "Debug");
        // assert
        Assert.AreEqual(minor + 1, newVersionRelease.Minor);
        Assert.AreEqual(minor, newVersionDebug.Minor);
    }

    [TestMethod]
    public void TestBuildChange()
    {
        // arrange
        var version = GetRandomVersion();
        // act
        var newVersionRelease = new VersionIncrementer().GetNewVersion(version, "Release");
        var newVersionDebug = new VersionIncrementer().GetNewVersion(version, "Debug");
        var build = (DateTime.Today - ProjectVersion.ReferenceDateTime).Days;
        // assert
        Assert.AreEqual(build, newVersionRelease.Build);
        Assert.AreEqual(build, newVersionDebug.Build);
    }

    [TestMethod]
    public void TestRevisionChange()
    {
        // arrange
        var version = GetRandomVersion();
        // act
        var newVersionRelease = new VersionIncrementer().GetNewVersion(version, "Release");
        var newVersionDebug = new VersionIncrementer().GetNewVersion(version, "Debug");
        var revision = (int)new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second).TotalSeconds / 2;
        // assert
        Assert.AreEqual(revision, newVersionRelease.Revision);
        Assert.AreEqual(revision, newVersionDebug.Revision);
    }

    private Version GetRandomVersion()
    {
        var random = new Random();

        var major = random.Next(0, 10_000);
        var minor = random.Next(0, 10_000);
        var build = random.Next(0, 10_000);
        var revision = random.Next(0, 10_000);

        return new Version(major, minor, build, revision);
    }
}