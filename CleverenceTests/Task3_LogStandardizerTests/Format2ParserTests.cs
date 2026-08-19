using Xunit;
using Task3_LogStandardizer.Parsers;
using Task3_LogStandardizer.Core.Enums;

public class Format2ParserTests
{
    [Fact]
    public void Parse_ValidLine_ReturnsLogEntry()
    {
        // Arrange
        var parser = new Format2Parser();
        string line = "2025-03-10 15:14:51.5882| INFO|11|MobileComputer.GetDeviceId| Код устройства: '@MINDEO-M40-D-410244015546'";

        // Act
        var result = parser.Parse(line);

        // Assert
        Assert.Equal(2025, result.Date.Year);
        Assert.Equal(3, result.Date.Month);
        Assert.Equal(10, result.Date.Day);
        Assert.Equal(LogLevel.INFO, result.Level);
        Assert.Equal("MobileComputer.GetDeviceId", result.Method);
        Assert.Equal("Код устройства: '@MINDEO-M40-D-410244015546'", result.Message);
    }

    [Fact]
    public void Parse_LineWithoutMethod_DefaultsToDEFAULT()
    {
        var parser = new Format2Parser();
        string line = "2025-03-10 15:14:51.5882| INFO|11|| Код устройства";

        var result = parser.Parse(line);
        Assert.Equal("DEFAULT", result.Method);
    }

    [Fact]
    public void Parse_InvalidLine_ThrowsArgumentException()
    {
        var parser = new Format2Parser();
        string invalidLine = "Это не лог";

        Assert.Throws<ArgumentException>(() => parser.Parse(invalidLine));
    }
}