using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1_StringCompression.Validation.Interfaces
{
    interface IValidator<T>
    {
        bool IsValid(T input, bool allowEmpty = false);
        void ValidateOrThrow(T input, bool allowEmpty = false);
    }
}
