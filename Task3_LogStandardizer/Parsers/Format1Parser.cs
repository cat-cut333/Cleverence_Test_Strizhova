using System;
using System.Globalization;
using Task3_LogStandardizer.Core.Abstract;
using Task3_LogStandardizer.Core.Entities;

namespace Task3_LogStandardizer.Parsers
{
    public class Format1Parser : LogParserBase
    {
        public override LogEntry Parse(string line)
        {
            if (string.IsNullOrWhiteSpace(line))
                throw new ArgumentException("Line cannot be null or empty.");

            var parts = line.Split(' ', 4, StringSplitOptions.None);
            if (parts.Length < 4)
                throw new ArgumentException($"Invalid Format1 line: {line}");

        
            if (!DateTime.TryParseExact(parts[0], "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var date))
                throw new ArgumentException($"Invalid date format: {parts[0]}");

            
            if (!TimeSpan.TryParse(parts[1], CultureInfo.InvariantCulture, out var time))
                throw new ArgumentException($"Invalid time format: {parts[1]}");

            var level = MapLevel(parts[2]);
            var message = parts[3];

            return new LogEntry
            {
                Date = date,
                Time = time,
                Level = level,
                Method = "DEFAULT",
                Message = message
            };
        }
    }
}