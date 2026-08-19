using Xunit;
using Task3_LogStandardizer.Parsers;
using Task3_LogStandardizer.Core.Enums;

public class Format2ParserTests
{
    

    [Fact]
    public void Parse_LineWithEmptyMethod_DefaultsToDEFAULT()
    {
        var parser = new Format2Parser();
        string line = "2025-03-10 15:14:51.5882| INFO|11|| Сообщение";

        var result = parser.Parse(line);
        Assert.Equal("DEFAULT", result.Method);
    }

    [Fact]
    public void Parse_LineWithWarningLevel_ReturnsWARN()
    {
        var parser = new Format2Parser();
        string line = "2025-03-10 15:14:51.5882| WARN|11|Method| Сообщение";

        var result = parser.Parse(line);
        Assert.Equal(LogLevel.WARN, result.Level);
    }

    [Fact]
    public void Parse_LineWithErrorLevel_ReturnsERROR()
    {
        var parser = new Format2Parser();
        string line = "2025-03-10 15:14:51.5882| ERROR|11|Method| Сообщение";

        var result = parser.Parse(line);
        Assert.Equal(LogLevel.ERROR, result.Level);
    }

    [Fact]
    public void Parse_LineWithDebugLevel_ReturnsDEBUG()
    {
        var parser = new Format2Parser();
        string line = "2025-03-10 15:14:51.5882| DEBUG|11|Method| Сообщение";

        var result = parser.Parse(line);
        Assert.Equal(LogLevel.DEBUG, result.Level);
    }

    [Fact]
    public void Parse_EmptyLine_ThrowsArgumentException()
    {
        var parser = new Format2Parser();
        string invalidLine = "";

        Assert.Throws<ArgumentException>(() => parser.Parse(invalidLine));
    }

    [Fact]
    public void Parse_NullLine_ThrowsArgumentException()
    {
        var parser = new Format2Parser();
        string invalidLine = null;

        Assert.Throws<ArgumentException>(() => parser.Parse(invalidLine));
    }

    [Fact]
    public void Parse_InvalidDateFormat_ThrowsArgumentException()
    {
        var parser = new Format2Parser();
        string invalidLine = "10.03.2025 15:14:51.5882| INFO|11|Method| Сообщение";

        Assert.Throws<ArgumentException>(() => parser.Parse(invalidLine));
    }

    
    [Fact]
    public void Parse_InvalidLevel_ThrowsArgumentException()
    {
        var parser = new Format2Parser();
        string invalidLine = "2025-03-10 15:14:51.5882| UNKNOWN|11|Method| Сообщение";

        Assert.Throws<ArgumentException>(() => parser.Parse(invalidLine));
    }

    [Fact]
    public void Parse_LineWithMissingParts_ThrowsArgumentException()
    {
        var parser = new Format2Parser();
        string invalidLine = "2025-03-10 15:14:51.5882| INFO|11";

        Assert.Throws<ArgumentException>(() => parser.Parse(invalidLine));
    }
    [Fact]
    public void Parse_ValidLine_ReturnsLogEntry()
    {
        
        var parser = new Format2Parser();
        string line = "2025-03-10 15:14:51.5882| INFO|11|MobileComputer.GetDeviceId| Код устройства: '@MINDEO-M40-D-410244015546'";

       
        var result = parser.Parse(line);

        
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