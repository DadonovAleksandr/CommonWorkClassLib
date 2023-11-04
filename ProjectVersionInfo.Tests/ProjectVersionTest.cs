using System.Reflection;

namespace ProjectVersionInfo.Tests
{
    [TestClass]
    public class ProjectVersionTest
    {
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