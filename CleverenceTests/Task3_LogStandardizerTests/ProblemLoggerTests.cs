using Xunit;
using Task3_LogStandardizer.Infrastructure;
using System.IO;
using System.Threading.Tasks;

public class ProblemLoggerTests
{
    [Fact]
    public async Task LogProblemAsync_WritesLineToProblemsTxt()
    {
        // Arrange
        string line = "Test problem line";

        // Act
        await ProblemLogger.LogProblemAsync(line);

        // Assert
        string content = File.ReadAllText("problems.txt").Trim();
        Assert.Equal(line, content);

        // Cleanup
        File.Delete("problems.txt");
    }

    [Fact]
    public async Task LogProblemAsync_AppendsMultipleLines()
    {
        // Arrange
        string line1 = "First problem";
        string line2 = "Second problem";

        // Act
        await ProblemLogger.LogProblemAsync(line1);
        await ProblemLogger.LogProblemAsync(line2);

        // Assert
        string[] lines = File.ReadAllLines("problems.txt");
        Assert.Equal(2, lines.Length);
        Assert.Equal(line1, lines[0]);
        Assert.Equal(line2, lines[1]);

        // Cleanup
        File.Delete("problems.txt");
    }

    [Fact]
    public async Task LogProblemAsync_HandlesEmptyLine()
    {
        // Arrange
        string line = "";

        // Act
        await ProblemLogger.LogProblemAsync(line);

        // Assert
        string content = File.ReadAllText("problems.txt").Trim();
        Assert.Equal("", content);

        // Cleanup
        File.Delete("problems.txt");
    }
}