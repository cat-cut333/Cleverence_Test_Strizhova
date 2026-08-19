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
        /// Multiple readers can access this method concurrently without blocking each other.
        /// </summary>
        /// <returns>The current count value.</returns>
        int GetCount();

        /// <summary>
        /// Adds a value to the count in a thread-safe manner.
        /// Writers are executed sequentially (one at a time). 
        /// All readers are blocked while a writer is active.
        /// </summary>
        /// <param name="value">The value to add to the count.</param>
        /// <exception cref="InvalidOperationException">Thrown when the operation would cause an integer overflow or underflow.</exception>
        void AddToCount(int value);

        /// <summary>
        /// Resets the count to 0 in a thread-safe manner.
        /// </summary>
        /// <remarks>
        /// This method is primarily intended for testing and debugging purposes.
        /// </remarks>
        void Reset();
    }
}
