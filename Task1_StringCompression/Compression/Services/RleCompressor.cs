using System;
using System.Text;
using Task1_StringCompression.Compression.Interfaces;
using Task1_StringCompression.Validation.Interfaces;

namespace Task1_StringCompression.Compression.Services
{ /// <summary>
  /// Implements Run-Length Encoding (RLE) compression and decompression.
  /// </summary>
    public class RleCompressor : ICompressor
    {
      
        private readonly IValidator<string> _validator;
        /// <summary>
        /// Initializes a new instance of the <see cref="RleCompressor"/> class.
        /// </summary>
        /// <param name="validator">The validator used to validate input strings.</param>
        /// <exception cref="ArgumentNullException">Thrown when validator is null.</exception>
        public RleCompressor(IValidator<string> validator)
        {
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }
        /// <summary>
        /// Compresses a string using the RLE algorithm.
        /// </summary>
        /// <param name="input">The string to compress. Must contain only lowercase Latin letters.</param>
        /// <returns>The compressed string. Groups of identical characters are replaced with the character and count.</returns>
        /// <example>
        /// <code>
        /// var compressor = new RleCompressor(new DefaultStringValidator());
        /// string result = compressor.Compress("aaabbcccdde"); // "a3b2c3d2e"
        /// </code>
        /// </example>
        /// <exception cref="ArgumentException">Thrown when the input contains invalid characters.</exception>
        public string Compress(string input)
        {
            _validator.ValidateOrThrow(input, allowEmpty: true);

            if (string.IsNullOrEmpty(input))
                return string.Empty;

            var result = new StringBuilder();
            int i = 0;

            while (i < input.Length)
            {
                char currentChar = input[i];
                int count = 1;

                while (i + count < input.Length && input[i + count] == currentChar)
                {
                    count++;
                }

                result.Append(currentChar);
                if (count > 1)
                    result.Append(count);

                i += count;
            }

            return result.ToString();
        }

        /// <summary>
        /// Decompresses a string that was compressed with the RLE algorithm.
        /// </summary>
        /// <param name="input">The compressed string (e.g., "a3b2c3d2e").</param>
        /// <returns>The original uncompressed string.</returns>
        /// <exception cref="ArgumentException">Thrown when the compressed string is malformed.</exception>
        public string Decompress(string input)
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;

            var result = new StringBuilder();
            int i = 0;

            while (i < input.Length)
            {
                if (!char.IsLetter(input[i]))
                    throw new ArgumentException("Invalid compressed string");

                char currentChar = input[i];
                i++;

                int count = 1;
                if (i < input.Length && char.IsDigit(input[i]))
                {
                    count = 0;
                    while (i < input.Length && char.IsDigit(input[i]))
                    {
                        count = count * 10 + (input[i] - '0');
                        i++;
                    }
                }

                for (int j = 0; j < count; j++)
                {
                    result.Append(currentChar);
                }
            }

            return result.ToString();
        }
    }
}
