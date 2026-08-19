using System;
using Task1_StringCompression.Validation.Interfaces;

namespace Task1_StringCompression.Validation.Services
{
    public class DefaultStringValidator : IValidator<string>
    {
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

        public void ValidateOrThrow(string input, bool allowEmpty = false)
        {
            if (!IsValid(input, allowEmpty))
                throw new ArgumentException("Invalid input");
        }
    }
}
