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
    public class LogProcessor
    {
        private readonly List<ILogParser> _parsers = new List<ILogParser>
        {
            new Format1Parser(),
            new Format2Parser()
        };

        private readonly ILogFormatter _formatter = new StandardLogFormatter();

        public async Task ProcessAsync(string inputFile, string outputFile)
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
                        tasks.Add(ProblemLogger.LogProblemAsync(line));
                    }
                }
                catch (Exception)
                {
                    tasks.Add(ProblemLogger.LogProblemAsync(line));
                }
            }

            await Task.WhenAll(tasks);
        }

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