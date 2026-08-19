using System;
using System.Threading;
using Task2_ThreadSafeServer.Server.Interfaces;
namespace Task2_ThreadSafeServer.Server.Services
{
    /// <summary>
    /// A thread-safe server implementation using <see cref="ReaderWriterLockSlim"/>.
    /// Optimized for scenarios with multiple readers and occasional writers.
    /// </summary>
    /// <remarks>
    /// This implementation allows concurrent reads and ensures exclusive access for writes.
    /// It includes protection against integer overflow
    /// </remarks>
    public class StaticServer : IStaticServer
    {
        private int _count = 0;
        private readonly ReaderWriterLockSlim _lock = new ReaderWriterLockSlim();

        /// <summary>
        /// Gets the current count in a thread-safe manner.
        /// Multiple readers can enter simultaneously without blocking each other.
        /// </summary>
        /// <returns>The current count value.</returns>
        /// <example>
        /// <code>
        /// var server = new StaticServer();
        /// int currentValue = server.GetCount();
        /// Console.WriteLine($"Current count: {currentValue}");
        /// </code>
        /// </example>
        public int GetCount()
        {
            _lock.EnterReadLock();
            try
            {
                return _count;
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }
        /// <summary>
        /// Adds a value to the count in a thread-safe manner.
        /// </summary>
        /// <param name="value">The value to add to the count. Can be positive or negative.</param>
        /// <remarks>
        /// Writers are executed sequentially. All readers are blocked while a writer is active.
        /// If the value is 0, the method returns immediately without acquiring the write lock.
        /// </remarks>
        /// <exception cref="InvalidOperationException">
        /// Thrown when the operation would cause an integer overflow (<see cref="int.MaxValue"/>) 
        /// or underflow (<see cref="int.MinValue"/>).
        /// </exception>
       
        public void AddToCount(int value)
        {
            if (value == 0) return;

            _lock.EnterWriteLock();
            try
            {
                _count = checked(_count + value);
                
            }
            catch (OverflowException ex)
            {
                
                throw new InvalidOperationException("Count overflow occurred.", ex);
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        /// <summary>
        /// Resets the count to 0 in a thread-safe manner.
        /// </summary>
        /// <remarks>
        /// This method is intended primarily for testing and debugging.
        /// </remarks>
        /// <example>
        /// <code>
        /// var server = new StaticServer();
        /// server.AddToCount(100);
        /// server.Reset();
        /// Console.WriteLine($"Count after reset: {server.GetCount()}"); // Outputs: 0
        /// </code>
        /// </example>
        public void Reset()
        {
            _lock.EnterWriteLock();
            try
            {
                _count = 0;
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }
    }
}
