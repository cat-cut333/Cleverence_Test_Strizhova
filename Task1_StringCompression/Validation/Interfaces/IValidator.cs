using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1_StringCompression.Validation.Interfaces
{/// <summary>
 /// Defines a contract for validating data of type <typeparamref name="T"/>.
 /// </summary>
 /// <typeparam name="T">The type of data to validate.</typeparam>
    public interface IValidator<T>
    { /// <summary>
      /// Determines whether the input data is valid.
      /// </summary>
      /// <param name="input">The data to validate.</param>
      /// <param name="allowEmpty">If true, empty or null values are considered valid.</param>
      /// <returns>True if the data is valid; otherwise, false.</returns>
        bool IsValid(T input, bool allowEmpty = false);
        // <summary>
        /// Validates the input and throws an exception if it is invalid.
        /// </summary>
        /// <param name="input">The data to validate.</param>
        /// <param name="allowEmpty">If true, empty or null values are considered valid.</param>
        /// <exception cref="ArgumentException">Thrown when the data is invalid.</exception>
        void ValidateOrThrow(T input, bool allowEmpty = false);
    }
}
