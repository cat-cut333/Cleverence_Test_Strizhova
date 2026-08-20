using Task3_LogStandardizer.Core.Entities;

namespace Task3_LogStandardizer.Core.Interfaces
{
    /// <summary>
    /// Defines a contract for parsing log lines into structured <see cref="LogEntry"/> objects.
    /// </summary>
    /// <remarks>
    /// Implementations of this interface are responsible for parsing log lines from specific formats
    /// (e.g., space-separated, pipe-separated) into a unified <see cref="LogEntry"/> representation.
    /// </remarks>
    public interface ILogParser
    {
        /// <summary>
        /// Parses a raw log line and returns a structured <see cref="LogEntry"/> object.
        /// </summary>
        /// <param name="line">The raw log line to parse. Must not be <c>null</c> or empty.</param>
        /// <returns>A <see cref="LogEntry"/> object containing the parsed data.</returns>
        LogEntry Parse(string line);
    }
}