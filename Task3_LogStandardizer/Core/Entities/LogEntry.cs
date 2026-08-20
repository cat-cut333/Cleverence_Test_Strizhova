using System;
using Task3_LogStandardizer.Core.Enums;

namespace Task3_LogStandardizer.Core.Entities
{
    /// <summary>
    /// Represents a structured log entry with standardized fields.
    /// </summary>
    /// <remarks>
    /// This class is used as a data transfer object (DTO) between parsers and formatters.
    /// It contains all fields required for the standard log output format.
    /// </remarks>
    public class LogEntry
    {
        /// <summary>
        /// Gets or sets the date of the log entry.
        /// </summary>
        /// <value>The date in <see cref="DateTime"/> format.</value>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets the time of the log entry in its original string format.
        /// </summary>
        /// <value>
        /// The time as it appeared in the source log, preserving the original format
        /// (e.g., "15:14:49.523" or "15:14:51.5882").
        /// </value>
        /// <remarks>
        /// Time is stored as a string rather than <see cref="TimeSpan"/> to preserve
        /// the exact original format and number of decimal places.
        /// </remarks>
        public string Time { get; set; } = string.Empty;

        // <summary>
        /// Gets or sets the log level.
        /// </summary>
        /// <value>One of the <see cref="LogLevel"/> enum values.</value>
        public LogLevel Level { get; set; }

        /// <summary>
        /// Gets or sets the method name that generated the log entry.
        /// </summary>
        /// <value>
        /// The method name, or <c>"DEFAULT"</c> if no method was specified in the source log.
        /// </value>
        public string Method { get; set; } = "DEFAULT";

        /// <summary>
        /// Gets or sets the log message content.
        /// </summary>
        /// <value>The message text from the log entry.</value>
        public string Message { get; set; } = string.Empty;
    }
}