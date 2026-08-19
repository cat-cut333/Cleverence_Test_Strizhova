using System;
using System.Globalization;
using Task3_LogStandardizer.Core.Abstract;
using Task3_LogStandardizer.Core.Entities;

namespace Task3_LogStandardizer.Parsers
{
    public class Format2Parser : LogParserBase
    {
        public override LogEntry Parse(string line)
        {
            if (string.IsNullOrWhiteSpace(line))
                throw new ArgumentException("Line cannot be null or empty.");

            var parts = line.Split('|');
            if (parts.Length < 5)
                throw new ArgumentException($"Invalid Format2 line: {line}");

            // Парсим дату+время (YYYY-MM-DD HH:MM:SS.ffff)
            var dateTimeParts = parts[0].Trim().Split(' ', 2);
            if (dateTimeParts.Length < 2)
                throw new ArgumentException($"Invalid date-time format: {parts[0]}");

            if (!DateTime.TryParseExact(dateTimeParts[0], "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var date))
                throw new ArgumentException($"Invalid date format: {dateTimeParts[0]}");

            if (!TimeSpan.TryParse(dateTimeParts[1], CultureInfo.InvariantCulture, out var time))
                throw new ArgumentException($"Invalid time format: {dateTimeParts[1]}");

            var level = MapLevel(parts[1].Trim());
            var method = parts[3].Trim();
            var message = parts[4].Trim();

            return new LogEntry
            {
                Date = date,
                Time = time,
                Level = level,
                Method = string.IsNullOrEmpty(method) ? "DEFAULT" : method,
                Message = message
            };
        }
    }
}