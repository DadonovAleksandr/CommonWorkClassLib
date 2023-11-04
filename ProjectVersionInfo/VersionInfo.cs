using System.Reflection;

namespace ProjectVersionInfo;

[Obsolete]
public class VersionInfo
{
    private Assembly _asm;
    
    public VersionInfo(Assembly asm)
    {
        _asm = asm;
    }
    
    public string GetVersion()
    {
        try
        {
            Version version = _asm.GetName().Version ?? new Version(0,0,0,0);
            return $"{version.Major}.{version.Minor}";
        }
        catch (Exception e)
        {
            return "0.0";
        }
    }
    
    public string GetBuildDate()
    {
        try
        {
            Version version = _asm.GetName().Version ?? new Version(0,0,0,0);
            var buildDateTime = new DateTime(2000, 1, 1).Add(new TimeSpan(
                TimeSpan.TicksPerDay * version.Build +          // days since 1 January 2000
                TimeSpan.TicksPerSecond * 2 * version.Revision));   // seconds since midnight, (multiply by 2 to get original)
            return $"{buildDateTime.ToShortDateString()}";
        }
        catch (Exception e)
        {
            return "01.01.2001";
        }
    }
}