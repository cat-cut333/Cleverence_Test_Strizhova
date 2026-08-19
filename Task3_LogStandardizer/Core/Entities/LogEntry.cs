using System;
using Task3_LogStandardizer.Core.Enums;

namespace Task3_LogStandardizer.Core.Entities
{
    public class LogEntry
    {
        public DateTime Date { get; set; }
        public string Time { get; set; } = string.Empty;
        public LogLevel Level { get; set; }
        public string Method { get; set; } = "DEFAULT";
        public string Message { get; set; } = string.Empty;
    }
}