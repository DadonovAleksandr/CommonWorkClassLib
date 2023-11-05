using System;

namespace VersionChanger;

internal class SimpleColorLogger
{
    private static readonly object _lock = new object();
    private static readonly Dictionary<MessageTypeEnum, ConsoleColor> _colors = new Dictionary<MessageTypeEnum, ConsoleColor>
    {
        { MessageTypeEnum.Trace, ConsoleColor.Gray },
        { MessageTypeEnum.Debug, ConsoleColor.Gray },
        { MessageTypeEnum.Warn, ConsoleColor.Yellow },
        { MessageTypeEnum.Error, ConsoleColor.Red },
        { MessageTypeEnum.Fatal, ConsoleColor.Red }
    };

    public void Log(MessageTypeEnum type, string msg)
    {
        lock (_lock)
        {
            Console.ForegroundColor = _colors[type];
            Console.WriteLine(msg);
            Console.ResetColor();
        }
    }
}