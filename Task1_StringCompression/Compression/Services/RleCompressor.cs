using System;
using System.Text;
using Task1_StringCompression.Compression.Interfaces;
using Task1_StringCompression.Validation.Interfaces;

namespace Task1_StringCompression.Compression.Services
{
    public class RleCompressor : ICompressor
    {
        private readonly IValidator<string> _validator;

        public RleCompressor(IValidator<string> validator)
        {
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }

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
