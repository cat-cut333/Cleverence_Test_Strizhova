using Task3_LogStandardizer.Core.Entities;

namespace Task3_LogStandardizer.Core.Interfaces
{
    public interface ILogFormatter
    {
        string Format(LogEntry entry);
    }
}