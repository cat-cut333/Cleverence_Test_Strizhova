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

            // Время с сохранением исходного количества знаков
            var timeStr = entry.Time.ToString(@"hh\:mm\:ss\.fffffff");
            timeStr = NormalizeTimeFormat(timeStr);
            sb.Append(timeStr);
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

        private string NormalizeTimeFormat(string timeStr)
        {
            var parts = timeStr.Split('.');
            if (parts.Length != 2)
                return timeStr;

            var mainPart = parts[0];
            var fractionalPart = parts[1].TrimEnd('0');

            
            if (string.IsNullOrEmpty(fractionalPart))
                return mainPart + ".0";

            return mainPart + "." + fractionalPart;
        }
    }
}