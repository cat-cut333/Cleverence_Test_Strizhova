using Xunit;
using Task3_LogStandardizer.Parsers;
using Task3_LogStandardizer.Core.Enums;

public class Format1ParserTests
{
    [Fact]
    public void Parse_ValidLine_ReturnsLogEntry()
    {
        // Arrange
        var parser = new Format1Parser();
        string line = "10.03.2025 15:14:49.523 INFORMATION Версия программы: '3.4.0'";

        // Act
        var result = parser.Parse(line);

        // Assert
        Assert.Equal(2025, result.Date.Year);
        Assert.Equal(3, result.Date.Month);
        Assert.Equal(10, result.Date.Day);
        Assert.Equal(LogLevel.INFO, result.Level);
        Assert.Equal("Версия программы: '3.4.0'", result.Message);
        Assert.Equal("DEFAULT", result.Method);
    }

    [Fact]
    public void Parse_InvalidLine_ThrowsArgumentException()
    {
        var parser = new Format1Parser();
        string invalidLine = "Это не лог";

        Assert.Throws<ArgumentException>(() => parser.Parse(invalidLine));
    }
}