using System;
using System.Text;
using Task3_LogStandardizer.Core.Entities;
using Task3_LogStandardizer.Core.Interfaces;

namespace Task3_LogStandardizer.Formatters
{
    /// <summary>
    /// Formats a <see cref="LogEntry"/> into a tab-separated string in the standard output format.
    /// </summary>
    /// <remarks>
    /// The output format is: <c>DD-MM-YYYY\tHH:MM:SS.fff\tLEVEL\tMETHOD\tMESSAGE</c>
    /// <para>
    /// This formatter preserves the original time format as it was parsed from the source log.
    /// If the method is <c>null</c> or empty, it is replaced with <c>"DEFAULT"</c>.
    /// </para>
    /// </remarks>
    public class StandardLogFormatter : ILogFormatter
    {
        /// <summary>
        /// Formats the specified <see cref="LogEntry"/> into a tab-separated string.
        /// </summary>
        /// <param name="entry">The log entry to format. Must not be <c>null</c>.</param>
        /// <returns>
        /// A tab-separated string in the format:
        /// <c>DD-MM-YYYY\tHH:MM:SS.fff\tLEVEL\tMETHOD\tMESSAGE</c>
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="entry"/> is <c>null</c>.
        /// </exception>
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