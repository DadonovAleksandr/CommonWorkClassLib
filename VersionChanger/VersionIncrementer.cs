using ProjectVersionInfo;

namespace VersionChanger;

internal class VersionIncrementer
{
    internal Version GetNewVersion(Version version, string buildType)
    {
        var minor = buildType == "Release" ? version.Minor + 1 : version.Minor;
        var build = (DateTime.Today - ProjectVersion.ReferenceDateTime).Days;
        var revision = (int)new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second).TotalSeconds / 2;
        return new Version(version.Major, minor, build, revision);
    }
}
