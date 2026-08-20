using System;
using System.Globalization;
using Task3_LogStandardizer.Core.Abstract;
using Task3_LogStandardizer.Core.Entities;

namespace Task3_LogStandardizer.Parsers
{/// <summary>
 /// Parses log entries in Format 2: pipe-separated values.
 /// </summary>
 /// <remarks>
 /// Expected format: <c>YYYY-MM-DD HH:MM:SS.ffff| LEVEL|ID|METHOD|MESSAGE</c>
 /// <para>
 /// Example: <c>2025-03-10 15:14:51.5882| INFO|11|MobileComputer.GetDeviceId| Код устройства: '@MINDEO-M40-D-410244015546'</c>
 /// </para>
 /// <para>
 /// The time is preserved exactly as it appears in the source log,
 /// maintaining the original number of decimal places in milliseconds.
 /// </para>
 /// </remarks>
    public class Format2Parser : LogParserBase
    {
        // <summary>
        /// Parses a log line in Format 2 and returns a structured <see cref="LogEntry"/>.
        /// </summary>
        /// <param name="line">The log line to parse. Must not be <c>null</c> or empty.</param>
        /// <returns>A <see cref="LogEntry"/> object containing the parsed data.</returns>
        /// <exception cref="ArgumentException">
        /// Thrown when:
        /// <list type="bullet">
        /// <item><description>The line is <c>null</c> or empty.</description></item>
        /// <item><description>The line has fewer than 5 pipe-separated parts.</description></item>
        /// <item><description>The date-time part has fewer than 2 parts (date and time).</description></item>
        /// <item><description>The date format is not <c>yyyy-MM-dd</c>.</description></item>
        /// <item><description>The time field is empty.</description></item>
        /// <item><description>The log level is unknown.</description></item>
        /// </list>
        /// </exception>
        public override LogEntry Parse(string line)
        {
            if (string.IsNullOrWhiteSpace(line))
                throw new ArgumentException("Line cannot be null or empty.");

            var parts = line.Split('|');
            if (parts.Length < 5)
                throw new ArgumentException($"Invalid Format2 line: {line}");

            // Парсим дату (YYYY-MM-DD)
            var dateTimeParts = parts[0].Trim().Split(' ', 2, StringSplitOptions.None);
            if (dateTimeParts.Length < 2)
                throw new ArgumentException($"Invalid date-time format: {parts[0]}");

            if (!DateTime.TryParseExact(dateTimeParts[0], "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var date))
                throw new ArgumentException($"Invalid date format: {dateTimeParts[0]}");

            // Время — сохраняем КАК ЕСТЬ
            var timeStr = dateTimeParts[1];
            if (string.IsNullOrWhiteSpace(timeStr))
                throw new ArgumentException($"Invalid time format: {timeStr}");

            var level = MapLevel(parts[1].Trim());
            var method = parts[3].Trim();
            var message = parts[4].Trim();

            return new LogEntry
            {
                Date = date,
                Time = timeStr,
                Level = level,
                Method = string.IsNullOrEmpty(method) ? "DEFAULT" : method,
                Message = message
            };
        }
    }
}