using System;
using System.Text;
using Task3_LogStandardizer.Core.Entities;
using Task3_LogStandardizer.Core.Interfaces;

namespace Task3_LogStandardizer.Formatters
{
    public class StandardLogFormatter : ILogFormatter
    {
        public string Format(LogEntry entry)
        {
            if (entry == null)
                throw new ArgumentNullException(nameof(entry));

            var sb = new StringBuilder();

            // Дата 
            sb.Append(entry.Date.ToString("dd-MM-yyyy"));
            sb.Append('\t');

            sb.Append(entry.Time);
            
            sb.Append('\t');

            // Уровень логирования
            sb.Append(entry.Level.ToString());
            sb.Append('\t');

            // Метод (или DEFAULT)
            var methodStr = string.IsNullOrEmpty(entry.Method) ? "DEFAULT" : entry.Method;
            sb.Append(methodStr);
            sb.Append('\t');

            // Сообщение
            sb.Append(entry.Message);

            return sb.ToString();
        }

        
    }
}