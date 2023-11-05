///Полуавтоматическо икрементирование версии проекта в Visual Studio
//xx.yy.zzzz.kkkk
//xx    - основной номер (Major version). Критические изменения проекта (введен новый функционал, в корне переработан существующий). Устанавливается вручную.
//yy    - дополниельный номер (Minor version). Изменение функциональных частей приложения. Автоинкрементируется в конфигурации RELEASE.
//zzzz  - номер сборки (Build number). Кол-во дней от точки отсчета (1 января 2000 года).
//kkkk  - номер ревизии (Revision). Кол-во секунд от начала суток.

using System.Text.RegularExpressions;
using VersionChanger;

var buildType = args[0];    // первым аргументом передаем текущую конфигурация интерпритатора: Debug/Release
var filePath = AssemblyInfoSearcher.GetPath();

var logger = new SimpleColorLogger();   

if (string.IsNullOrEmpty(buildType))
{
    logger.Log(MessageTypeEnum.Error, $"Тип компиляции не определен");
    Console.ReadLine();
    return;
}
if (string.IsNullOrEmpty(filePath))
{
    logger.Log(MessageTypeEnum.Error, $"Не найден файл версионности");
    Console.ReadLine();
    return;
}
if (!File.Exists(filePath))
{
    logger.Log(MessageTypeEnum.Error, $"Не существует файла: {filePath}");
    Console.ReadLine();
    return;
}

try
{
    logger.Log(MessageTypeEnum.Trace, $"Чтение файла: {filePath}");
    string text = File.ReadAllText(filePath);
    logger.Log(MessageTypeEnum.Trace, "Поиск текущей версии проекта");
    Match match = new Regex("AssemblyVersion\\(\"(.*?)\"\\)").Match(text);
    logger.Log(MessageTypeEnum.Trace, "Текущая версия проекта: " + match.Groups[1].Value);
    Version ver = new Version(match.Groups[1].Value);
    logger.Log(MessageTypeEnum.Trace, $"Текущая конфигурация интерпритатора: {buildType}");
    var newVer = new VersionIncrementer().GetNewVersion(ver, buildType);
    logger.Log(MessageTypeEnum.Trace, $"Новая версия проекта: {newVer}");
    text = Regex.Replace(text, @"AssemblyVersion\((.*?)\)", $"AssemblyVersion(\"{newVer}\")");
    text = Regex.Replace(text, @"AssemblyFileVersion\((.*?)\)",$"AssemblyFileVersion(\"{newVer}\")");
    logger.Log(MessageTypeEnum.Trace, $"Запись файла: {filePath}");
    File.WriteAllText(filePath, text);
}
catch (Exception e)
{
    logger.Log(MessageTypeEnum.Error, e.Message);
    Console.ReadLine();
}