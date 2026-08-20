using System;
using System.Globalization;
using Task3_LogStandardizer.Core.Abstract;
using Task3_LogStandardizer.Core.Entities;

namespace Task3_LogStandardizer.Parsers
{
    /// <summary>
    /// Parses log entries in Format 1: space-separated values.
    /// </summary>
    /// <remarks>
    /// Expected format: <c>DD.MM.YYYY HH:MM:SS.fff LEVEL MESSAGE</c>
    /// <para>
    /// Example: <c>10.03.2025 15:14:49.523 INFORMATION Версия программы: '3.4.0'</c>
    /// </para>
    /// <para>
    /// The time is preserved exactly as it appears in the source log,
    /// maintaining the original number of decimal places in milliseconds.
    /// </para>
    /// </remarks>
    public class Format1Parser : LogParserBase
    {

        /// <summary>
        /// Parses a log line in Format 1 and returns a structured <see cref="LogEntry"/>.
        /// </summary>
        /// <param name="line">The log line to parse. Must not be <c>null</c> or empty.</param>
        /// <returns>A <see cref="LogEntry"/> object containing the parsed data.</returns>
        /// <exception cref="ArgumentException">
        /// Thrown when:
        /// <list type="bullet">
        /// <item><description>The line is <c>null</c> or empty.</description></item>
        /// <item><description>The line has fewer than 4 parts.</description></item>
        /// <item><description>The date format is not <c>dd.MM.yyyy</c>.</description></item>
        /// <item><description>The time field is empty.</description></item>
        /// <item><description>The log level is unknown.</description></item>
        /// </list>
        /// </exception>
        
        public override LogEntry Parse(string line)
        {
            if (string.IsNullOrWhiteSpace(line))
                throw new ArgumentException("Line cannot be null or empty.");

            var parts = line.Split(' ', 4, StringSplitOptions.None);
            if (parts.Length < 4)
                throw new ArgumentException($"Invalid Format1 line: {line}");

        
            if (!DateTime.TryParseExact(parts[0], "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var date))
                throw new ArgumentException($"Invalid date format: {parts[0]}");


            // Время — сохраняем КАК ЕСТЬ, без изменений
            var timeStr = parts[1];

            // Минимальная проверка: хотя бы один символ, не пустая строка
            if (string.IsNullOrWhiteSpace(timeStr))
                throw new ArgumentException($"Invalid time format: {timeStr}");


            var level = MapLevel(parts[2]);
            var message = parts[3];

            return new LogEntry
            {
                Date = date,
                Time = timeStr,
                Level = level,
                Method = "DEFAULT",
                Message = message
            };
        }
    }
}