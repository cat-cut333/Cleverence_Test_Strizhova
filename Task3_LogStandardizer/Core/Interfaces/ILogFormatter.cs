using Task3_LogStandardizer.Core.Entities;

namespace Task3_LogStandardizer.Core.Interfaces
{
    /// <summary>
    /// Defines a contract for formatting a <see cref="LogEntry"/> into a string representation.
    /// </summary>
    public interface ILogFormatter
    {
        string Format(LogEntry entry);
    }
}