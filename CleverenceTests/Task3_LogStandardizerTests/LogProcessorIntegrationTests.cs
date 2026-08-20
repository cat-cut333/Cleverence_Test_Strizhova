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
        string problems = Path.GetTempFileName();
        string inputLine = "10.03.2025 15:14:49.523 INFORMATION Версия программы: '3.4.0'";
        string expectedOutput = "10-03-2025\t15:14:49.523\tINFO\tDEFAULT\tВерсия программы: '3.4.0'";

        File.WriteAllText(inputFile, inputLine);

        var processor = new LogProcessor();

        // Act
        await processor.ProcessAsync(inputFile, outputFile, problems);

        // Assert
        string result = File.ReadAllText(outputFile).Trim();
        Assert.Equal(expectedOutput, result);

        // Cleanup
        File.Delete(inputFile);
        File.Delete(outputFile);
    }

    [Fact]
    public async Task Process_MultipleValidLines_ProducesMultipleOutputLines()
    {
        // Arrange
        string inputFile = Path.GetTempFileName();
        string outputFile = Path.GetTempFileName();
        string problems = Path.GetTempFileName();
        string[] inputLines = new[]
        {
            "10.03.2025 15:14:49.523 INFORMATION Версия программы: '3.4.0'",
            "10.03.2025 15:14:49.523 ERROR Ошибка подключения"
        };
        string[] expectedOutputs = new[]
        {
            "10-03-2025\t15:14:49.523\tINFO\tDEFAULT\tВерсия программы: '3.4.0'",
            "10-03-2025\t15:14:49.523\tERROR\tDEFAULT\tОшибка подключения"
        };

        File.WriteAllLines(inputFile, inputLines);

        var processor = new LogProcessor();

        // Act
        await processor.ProcessAsync(inputFile, outputFile, problems);

        // Assert
        string[] result = File.ReadAllLines(outputFile);
        Assert.Equal(expectedOutputs, result);

        // Cleanup
        File.Delete(inputFile);
        File.Delete(outputFile);
        File.Delete(problems);
    }

    [Fact]
    public async Task Process_InvalidLine_WritesToProblemsTxt()
    {
        // Arrange
        string inputFile = Path.GetTempFileName();
        string outputFile = Path.GetTempFileName();
        string problems = Path.GetTempFileName();
        string invalidLine = "Это невалидная строка";

        File.WriteAllText(inputFile, invalidLine);

        var processor = new LogProcessor();

        // Act
        await processor.ProcessAsync(inputFile, outputFile, problems);

        // Assert
        string problem = File.ReadAllText(problems).Trim();
        Assert.Equal(invalidLine, problem);

        // Cleanup
        File.Delete(inputFile);
        File.Delete(outputFile);
        File.Delete(problems);
    }

    [Fact]
    public async Task Process_MixedValidAndInvalidLines_HandlesBothCorrectly()
    {
        // Arrange
        string inputFile = Path.GetTempFileName();
        string outputFile = Path.GetTempFileName();
        string problems = Path.GetTempFileName();
        string[] inputLines = new[]
        {
            "10.03.2025 15:14:49.523 INFORMATION Валидная строка",
            "Невалидная строка",
            "10.03.2025 15:14:49.523 ERROR Ещё одна валидная"
        };
        string[] expectedOutputs = new[]
        {
            "10-03-2025\t15:14:49.523\tINFO\tDEFAULT\tВалидная строка",
            "10-03-2025\t15:14:49.523\tERROR\tDEFAULT\tЕщё одна валидная"
        };

        File.WriteAllLines(inputFile, inputLines);

        var processor = new LogProcessor();

        // Act
        await processor.ProcessAsync(inputFile, outputFile, problems);

        // Assert
        string[] result = File.ReadAllLines(outputFile);
        Assert.Equal(expectedOutputs, result);

        string problem = File.ReadAllText(problems).Trim();
        Assert.Equal("Невалидная строка", problem);

        // Cleanup
        File.Delete(inputFile);
        File.Delete(outputFile);
        File.Delete(problems);
    }

    [Fact]
    public async Task Process_NonExistentInputFile_ThrowsFileNotFoundException()
    {
        // Arrange
        var processor = new LogProcessor();
        string nonExistentFile = "nonexistent.txt";

        // Act & Assert
        await Assert.ThrowsAsync<FileNotFoundException>(() => processor.ProcessAsync(nonExistentFile, "output.txt","problem.txt"));
    }
}