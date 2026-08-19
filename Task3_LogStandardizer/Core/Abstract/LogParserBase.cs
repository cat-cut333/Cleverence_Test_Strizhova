using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task3_LogStandardizer.Core.Entities;
using Task3_LogStandardizer.Core.Enums;

namespace Task3_LogStandardizer.Core.Abstract
{
    public abstract class LogParserBase
    {
        public abstract LogEntry Parse(string line);

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
