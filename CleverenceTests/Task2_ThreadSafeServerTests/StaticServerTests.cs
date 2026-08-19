using Xunit;
using Task2_ThreadSafeServer.Server.Services;
using Task2_ThreadSafeServer.Server.Interfaces;


namespace CleverenceTests.Task2_ThreadSafeServerTests
{
    public class StaticServerTests
    {
        [Fact]
        public void GetCount_ReturnsInitialValue_WhenNoWrites()
        {
           
            var server = new StaticServer();

            int result = server.GetCount();

            Assert.Equal(0, result);
        }
        [Fact]
        public void AddToCount_IncreasesValueCorrectly()
        {
            
            var server = new StaticServer();

            server.AddToCount(5);

            Assert.Equal(5, server.GetCount());
        }

        [Fact]
        public void AddToCount_ThreadSafe_MultipleWriters()
        {
           
            var server = new StaticServer();

            Parallel.For(0, 100, _ => server.AddToCount(1));

            Assert.Equal(100, server.GetCount());
        }

        [Fact]
        public void AddToCount_And_GetCount_MixedWorkload_NoExceptions()
        {
            // Arrange
            var server = new StaticServer();

            // Act
            Parallel.Invoke(
                () => server.AddToCount(10),
                () => server.AddToCount(20),
                () => server.GetCount(),
                () => server.AddToCount(30),
                () => server.GetCount()
            );

            // Assert
            Assert.Equal(60, server.GetCount());
        }

        [Fact]
        public void AddToCount_ZeroValue_DoesNotChangeCount()
        {
            var server = new StaticServer();
            server.AddToCount(10);
            server.AddToCount(0);
            Assert.Equal(10, server.GetCount());
        }

        [Fact]
        public void Reset_SetsCountToZero()
        {
            var server = new StaticServer();
            server.AddToCount(10);
            server.Reset();
            Assert.Equal(0, server.GetCount());
        }

        [Fact]
        public void AddToCount_Overflow_ThrowsException()
        {
            var server = new StaticServer();
            server.AddToCount(int.MaxValue);

            Assert.Throws<InvalidOperationException>(() => server.AddToCount(1));
        }
    }
}
