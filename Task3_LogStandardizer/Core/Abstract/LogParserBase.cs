using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task3_LogStandardizer.Core.Entities;
using Task3_LogStandardizer.Core.Enums;
using Task3_LogStandardizer.Core.Interfaces;

namespace Task3_LogStandardizer.Core.Abstract
{
    public abstract class LogParserBase : ILogParser
    {
        /// <summary>
        /// Abstract base class for log parsers.
        /// Provides common functionality for parsing log entries from different formats.
        /// </summary>
        /// <remarks>
        /// This class implements the <see cref="ILogParser"/> interface and provides
        /// a shared method for mapping log level strings to <see cref="LogLevel"/> enum values.
        /// Derived classes must implement the <see cref="Parse"/> method.
        /// </remarks>
        public abstract LogEntry Parse(string line);

        /// <summary>
        /// Parses a log line and returns a structured <see cref="LogEntry"/> object.
        /// </summary>
        /// <param name="line">The raw log line to parse.</param>
        /// <returns>A <see cref="LogEntry"/> object containing the parsed data.</returns>
        /// <exception cref="ArgumentException">
        /// Thrown when the line format is invalid or cannot be parsed.
        /// </exception>
        protected LogLevel MapLevel(string input)
        {
            return input.ToUpperInvariant() switch
            {
                "INFORMATION" or "INFO" => LogLevel.INFO,
                "WARNING" or "WARN" => LogLevel.WARN,
                "ERROR" => LogLevel.ERROR,
                "DEBUG" => LogLevel.DEBUG,
                _ => throw new ArgumentException($"Unknown log level: {input}")
            };
        }
    }
}
