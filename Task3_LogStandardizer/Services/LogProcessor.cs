using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Task3_LogStandardizer.Core.Entities;
using Task3_LogStandardizer.Core.Interfaces;
using Task3_LogStandardizer.Parsers;
using Task3_LogStandardizer.Formatters;
using Task3_LogStandardizer.Infrastructure;

namespace Task3_LogStandardizer.Services
{
    /// <summary>
    /// Orchestrates the log processing pipeline: reads, parses, formats, and writes logs.
    /// </summary>
    /// <remarks>
    /// The processing flow:
    /// <list type="number">
    /// <item><description>Reads all lines from the input file.</description></item>
    /// <item><description>For each line, attempts to parse it using registered parsers.</description></item>
    /// <item><description>If parsing succeeds, formats the log entry and writes to the output file.</description></item>
    /// <item><description>If parsing fails, asynchronously writes the original line to the problem file.</description></item>
    /// </list>
    /// </remarks>
    public class LogProcessor
    {
        private readonly List<ILogParser> _parsers = new List<ILogParser>
        {
            new Format1Parser(),
            new Format2Parser()
        };

        private readonly ILogFormatter _formatter = new StandardLogFormatter();

        /// <summary>
        /// Processes the input log file and writes standardized logs to the output file.
        /// </summary>
        /// <param name="inputFile">Path to the input log file. Must exist.</param>
        /// <param name="outputFile">Path to the output file. Appends if the file already exists.</param>
        /// <param name="problemFile">Path to the file where invalid/unparseable lines will be written.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        /// <exception cref="FileNotFoundException">
        /// Thrown when the <paramref name="inputFile"/> does not exist.
        /// </exception>
        /// <exception cref="IOException">
        /// Thrown when an I/O error occurs while reading or writing files.
        /// </exception>
        /// <example>
        /// <code>
        /// var processor = new LogProcessor();
        /// await processor.ProcessAsync("input.txt", "output.txt", "problems.txt");
        /// </code>
        /// </example>
        public async Task ProcessAsync(string inputFile, string outputFile,string problemFile)
        {
            if (!File.Exists(inputFile))
                throw new FileNotFoundException($"Input file not found: {inputFile}");

            var lines = File.ReadAllLines(inputFile);
            var tasks = new List<Task>();

            using var writer = new StreamWriter(outputFile, append: true);

            foreach (var line in lines)
            {
                try
                {
                    var entry = TryParse(line);
                    if (entry != null)
                    {
                        var formatted = _formatter.Format(entry);
                        await writer.WriteLineAsync(formatted);
                    }
                    else
                    {
                        tasks.Add(ProblemLogger.LogProblemAsync(line, problemFile));
                    }
                }
                catch (Exception)
                {
                    tasks.Add(ProblemLogger.LogProblemAsync(line, problemFile));
                }
            }

            await Task.WhenAll(tasks);
        }

        /// <summary>
        /// Attempts to parse a log line using all registered parsers.
        /// </summary>
        /// <param name="line">The log line to parse.</param>
        /// <returns>
        /// A <see cref="LogEntry"/> if parsing succeeds; otherwise, <c>null</c>.
        /// </returns>
        /// <remarks>
        /// Each parser is tried in the order they were registered.
        /// The first parser that successfully parses the line returns the result.
        /// If all parsers fail, <c>null</c> is returned.
        /// </remarks>
        private LogEntry TryParse(string line)
        {
            foreach (var parser in _parsers)
            {
                try
                {
                    return parser.Parse(line);
                }
                catch
                {
                    continue;
                }
            }
            return null;
        }
    }
}