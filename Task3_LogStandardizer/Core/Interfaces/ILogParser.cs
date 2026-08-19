using Task3_LogStandardizer.Core.Entities;

namespace Task3_LogStandardizer.Core.Interfaces
{
    public interface ILogParser
    {
        LogEntry Parse(string line);
    }
}