using System;

namespace VersionChanger;

internal class AssemblyInfoSearcher
{
    private const string AssemblyInfoFileName = @"AssemblyInfo.cs";
    private const int deep = 5;

    public static string GetPath()
    {
        var dir = new DirectoryInfo(Directory.GetCurrentDirectory());
        Console.WriteLine($"Начинаем поиск файла {AssemblyInfoFileName} из директории {dir}");

        for (int i = 0; i < deep; i++)
        {
            foreach (var file in dir.GetFiles())
            {
                Console.WriteLine($"Проверка файла {file.Name}");
                if (file.Name == AssemblyInfoFileName)
                {
                    Console.WriteLine($"Файл {AssemblyInfoFileName} найден!");
                    return Path.Combine(dir.FullName, AssemblyInfoFileName);
                }
            }

            if (dir.Parent is null)
            {
                Console.WriteLine($"Вышестоящей директории не существует");
                break;
            }
            Console.WriteLine($"В директории {dir.FullName} файл {AssemblyInfoFileName} не найден");
            dir = dir.Parent;
            Console.WriteLine($"Переходим в вышестоящую директорию {dir.FullName}");
        }
        Console.WriteLine($"Файл {AssemblyInfoFileName} не был найден в {deep} вышестоящих директориях");
        return String.Empty;
    }
}
