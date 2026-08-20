using System;
using System.IO;
using System.Threading.Tasks;

namespace Task3_LogStandardizer.Infrastructure
{
    public static class ProblemLogger
    {
        private static readonly object _lock = new object();

        public static async Task LogProblemAsync(string line,string nameFile)
        {
            await Task.Run(() =>
            {
                lock (_lock)
                {
                    File.AppendAllText(nameFile, line + Environment.NewLine);
                }
            });
        }
    }
}