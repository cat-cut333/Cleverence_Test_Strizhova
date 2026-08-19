using Xunit;
using Task3_LogStandardizer.Services;
using System.IO;
using System.Threading.Tasks;

public class LogProcessorIntegrationTests
{
    [Fact]
    public async Task Process_ValidInputFile_ProducesCorrectOutput()
    {
        // Arrange
        string inputFile = Path.GetTempFileName();
        string outputFile = Path.GetTempFileName();
        string expectedOutput = "10-03-2025\t15:14:49.523\tINFO\tDEFAULT\tВерсия программы: '3.4.0'";

        File.WriteAllText(inputFile, "10.03.2025 15:14:49.523 INFORMATION Версия программы: '3.4.0'");

        var processor = new LogProcessor();

        // Act
        await processor.ProcessAsync(inputFile, outputFile);

        // Assert
        string result = File.ReadAllText(outputFile).Trim();
        Assert.Equal(expectedOutput, result);

        // Cleanup
        File.Delete(inputFile);
        File.Delete(outputFile);
    }

    [Fact]
    public async Task Process_InvalidLine_WritesToProblemsTxt()
    {
        // Arrange
        string inputFile = Path.GetTempFileName();
        string outputFile = Path.GetTempFileName();
        string invalidLine = "Это невалидная строка";

        File.WriteAllText(inputFile, invalidLine);

        var processor = new LogProcessor();

        // Act
        await processor.ProcessAsync(inputFile, outputFile);

        // Assert
        string problems = File.ReadAllText("problems.txt").Trim();
        Assert.Equal(invalidLine, problems);

        // Cleanup
        File.Delete(inputFile);
        File.Delete(outputFile);
        File.Delete("problems.txt");
    }
}