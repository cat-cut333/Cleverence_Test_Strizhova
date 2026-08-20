using Xunit;
using Task3_LogStandardizer.Infrastructure;
using System.IO;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

public class ProblemLoggerTests
{
    [Fact]
    public async Task LogProblemAsync_WritesLineToProblemsTxt()
    {
        // Arrange
        string line = "Test problem line";
        string problems = Path.GetTempFileName();
        // Act
        await ProblemLogger.LogProblemAsync(line, problems);

        // Assert
        string content = File.ReadAllText(problems).Trim();
        Assert.Equal(line, content);

        // Cleanup
        File.Delete(problems);
    }

    [Fact]
    public async Task LogProblemAsync_AppendsMultipleLines()
    {
        // Arrange
        string line1 = "First problem";
        string line2 = "Second problem";
        string problems = Path.GetTempFileName();

        // Act
        await ProblemLogger.LogProblemAsync(line1, problems);
        await ProblemLogger.LogProblemAsync(line2, problems);

        // Assert
        string[] lines = File.ReadAllLines(problems);
        Assert.Equal(2, lines.Length);
        Assert.Equal(line1, lines[0]);
        Assert.Equal(line2, lines[1]);

        // Cleanup
        File.Delete(problems);
    }

    [Fact]
    public async Task LogProblemAsync_HandlesEmptyLine()
    {
        // Arrange
        string line = "";
        string problems = Path.GetTempFileName();

        // Act
        await ProblemLogger.LogProblemAsync(line, problems);

        // Assert
        string content = File.ReadAllText(problems).Trim();
        Assert.Equal("", content);

        // Cleanup
        File.Delete(problems);
    }
}