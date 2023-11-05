using System.Reflection;
using VersionChanger;

namespace ProjectVersionInfo.Tests
{
    [TestClass]
    public class ProjectVersionTest
    {
        [TestMethod]
        public void TestVersionMatching()
        {
            // arrange
            var random = new Random();
            var curVersion = new Version(
                random.Next(0, 10000), 
                random.Next(0, 10000), 
                random.Next(0, 99999), 
                random.Next(0, 99999));
            // act
            var newVersion = new VersionIncrementer().GetNewVersion(curVersion, "Release");
            var versionInfo = new ProjectVersion(newVersion);
            var fullVersion = versionInfo.FullVersion;
            // assert
            Assert.AreEqual(fullVersion, $"{newVersion}");
        }

        [TestMethod]
        public void TestGetVersion_1_2()
        {
            // arrange
            var versionInfo = new ProjectVersion(new Version(1, 2, 3, 4));
            // act
            var version = versionInfo.Version;
            // assert
            Assert.AreEqual("1.2", version);
        }

        [TestMethod]
        public void TestGetBuildDate()
        {
            // arrange
            var versionInfo = new ProjectVersion(new Version(1, 2, 3, 4));

            // act
            var buildDate = versionInfo.BuildDate;
            // assert
            Assert.AreEqual("04.01.2000", buildDate);
        }
    }
}