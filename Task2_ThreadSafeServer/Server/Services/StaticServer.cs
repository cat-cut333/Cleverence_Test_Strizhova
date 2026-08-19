using System;
using System.Threading;
using Task2_ThreadSafeServer.Server.Interfaces;
namespace Task2_ThreadSafeServer.Server.Services
{
    /// <summary>
    /// A thread-safe server implementation using ReaderWriterLockSlim.
    /// Optimized for scenarios with multiple readers and occasional writers.
    /// </summary>
   public class StaticServer : IStaticServer
    {
        private int _count = 0;
        private readonly ReaderWriterLockSlim _lock = new ReaderWriterLockSlim();

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
