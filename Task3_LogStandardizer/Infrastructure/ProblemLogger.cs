using System;
using System.IO;
using System.Threading.Tasks;

namespace Task3_LogStandardizer.Infrastructure
{
    /// <summary>
    /// Provides asynchronous logging functionality for problematic log lines.
    /// </summary>
    /// <remarks>
    /// This utility class writes invalid or unparseable log lines to a specified file.
    /// All operations are thread-safe using a <see cref="lock"/> mechanism.
    /// </remarks>
    public static class ProblemLogger
    {
        private static readonly object _lock = new object();

        /// <summary>
        /// Asynchronously writes a problematic log line to the specified file.
        /// </summary>
        /// <param name="line">The problematic log line to write. Cannot be <c>null</c>.</param>
        /// <param name="nameFile">The name of the file to write to (e.g., "problems.txt").</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="line"/> or <paramref name="nameFile"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="IOException">
        /// Thrown when an I/O error occurs while writing to the file.
        /// </exception>
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