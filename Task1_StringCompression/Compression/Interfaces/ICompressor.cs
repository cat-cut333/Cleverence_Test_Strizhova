using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1_StringCompression.Compression.Interfaces
{
    /// <summary>
    /// Defines a contract for string compression and decompression algorithms.
    /// </summary>
    public interface ICompressor
    {
        /// <summary>
        /// Compresses a string using the implemented algorithm.
        /// </summary>
        /// <param name="input">The string to compress. Must contain only lowercase Latin letters.</param>
        /// <returns>The compressed string.</returns>
        /// <exception cref="ArgumentException">Thrown when the input is invalid.</exception>
        string Compress(string input);

        /// <summary>
        /// Decompresses a previously compressed string.
        /// </summary>
        /// <param name="input">The compressed string containing letters and numbers.</param>
        /// <returns>The original uncompressed string.</returns>
        /// <exception cref="ArgumentException">Thrown when the compressed string is malformed.</exception>
        string Decompress(string input);
    }
}
