using System.Reflection;

namespace ProjectVersionInfo;

public class ProjectVersion
{
    private Version _version;
    
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="asm">Сборка</param>
    public ProjectVersion(Assembly asm)
    {
        _version = asm?.GetName()?.Version ?? new Version(0, 0, 0, 0);
    }
    
    /// <summary>
    /// Конструктор для тестов
    /// </summary>
    /// <param name="version"></param>
    internal ProjectVersion(Version version)
    {
        _version = version;
    }
    
    /// <summary>
    /// Короткая версия проекта в формате Major.Minor
    /// </summary>
    public string Version => $"{_version.Major}.{_version.Minor}";
    
    /// <summary>
    /// Дата сборки проекта
    /// </summary>
    public string BuildDate => $"{GetBuildDateTime().ToShortDateString()}";

    /// <summary>
    /// Время сборки проекта
    /// </summary>
    public string BuildTime => $"{GetBuildDateTime().ToShortTimeString()}";

    /// <summary>
    /// Полная версия проекта в формате Major.Minor.Build.Revision
    /// </summary>
    public string FullVersion => $"{_version.Major}.{_version.Minor}.{_version.Build}.{_version.Revision}";

    public static DateTime ReferenceDateTime => new DateTime(2000, 1, 1);

    private DateTime GetBuildDateTime()
    {
        var buildDateTime = ReferenceDateTime
                .Add(new TimeSpan(
                    TimeSpan.TicksPerDay * _version.Build +                 // days since 1 January 2000
                    TimeSpan.TicksPerSecond * 2 * _version.Revision));      // seconds since midnight, (multiply by 2 to get original)
        return buildDateTime;
    }
}