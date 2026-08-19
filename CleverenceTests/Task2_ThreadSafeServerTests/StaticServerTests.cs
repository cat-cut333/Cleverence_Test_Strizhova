using Xunit;
using Task2_ThreadSafeServer.Server.Services;
using Task2_ThreadSafeServer.Server.Interfaces;


namespace CleverenceTests.Task2_ThreadSafeServerTests
{
    class StaticServerTests
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

    }
}
