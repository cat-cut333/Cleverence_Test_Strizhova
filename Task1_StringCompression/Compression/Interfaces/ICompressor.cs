using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1_StringCompression.Compression.Interfaces
{
    public interface ICompressor
    {
        string Compress(string input);
        string Decompress(string input);
    }
}
