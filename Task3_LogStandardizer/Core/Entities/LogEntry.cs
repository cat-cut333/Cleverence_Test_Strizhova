using System;
using Task3_LogStandardizer.Core.Enums;

namespace Task3_LogStandardizer.Core.Entities
{
    public class LogEntry
    {
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public LogLevel Level { get; set; }
        public string Method { get; set; } = "DEFAULT";
        public string Message { get; set; } = string.Empty;
    }
}