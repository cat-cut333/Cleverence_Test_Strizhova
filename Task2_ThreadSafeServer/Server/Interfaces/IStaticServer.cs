using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2_ThreadSafeServer.Server.Interfaces
{
    /// <summary>
    /// Defines a contract for a thread-safe static server.
    /// </summary>
    public interface IStaticServer
    {
        /// <summary>
        /// Gets the current count in a thread-safe manner.
        /// </summary>
        int GetCount();

        /// <summary>
        /// Adds a value to the count in a thread-safe manner.
        /// </summary>
        void AddToCount(int value);
    }
}
