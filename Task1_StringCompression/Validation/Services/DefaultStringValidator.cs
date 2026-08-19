using System;
using Task1_StringCompression.Validation.Interfaces;

namespace Task1_StringCompression.Validation.Services
{ /// <summary>
  /// Default implementation of <see cref="IValidator{T}"/> for strings.
  /// Validates that a string contains only lowercase Latin letters (a-z).
  /// </summary>
    public class DefaultStringValidator : IValidator<string>
    {
        /// <summary>
        /// Determines whether the input string contains only lowercase Latin letters.
        /// </summary>
        /// <param name="input">The string to validate.</param>
        /// <param name="allowEmpty">If true, empty or null strings are considered valid.</param>
        /// <returns>True if the string is valid; otherwise, false.</returns>
        public bool IsValid(string input, bool allowEmpty = false)
        {
            if (allowEmpty && string.IsNullOrEmpty(input))
                return true;

            if (string.IsNullOrEmpty(input))
                return false;

            foreach (char c in input)
            {
                if (c < 'a' || c > 'z')
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Validates the input string and throws an exception if it is invalid.
        /// </summary>
        /// <param name="input">The string to validate.</param>
        /// <param name="allowEmpty">If true, empty strings are considered valid.</param>
        /// <exception cref="ArgumentException">Thrown when the string contains invalid characters.</exception>
        public void ValidateOrThrow(string input, bool allowEmpty = false)
        {
            

            if (!IsValid(input, allowEmpty))
                throw new ArgumentException("Invalid input");
        }
    }
}
