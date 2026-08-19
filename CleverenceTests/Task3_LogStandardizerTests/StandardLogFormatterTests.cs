using Xunit;
using Task3_LogStandardizer.Formatters;
using Task3_LogStandardizer.Core.Entities;
using Task3_LogStandardizer.Core.Enums;
using System;

public class StandardLogFormatterTests
{
    [Fact]
    public void Format_ValidLogEntry_ReturnsTabSeparatedString()
    {
        // Arrange
        var formatter = new StandardLogFormatter();
        var entry = new LogEntry
        {
            Date = new DateTime(2025, 3, 10),
            Time = "15:14:49.523",
            Level = LogLevel.INFO,
            Method = "TestMethod",
            Message = "Test message"
        };

        // Act
        string result = formatter.Format(entry);

        // Assert
        Assert.Equal("10-03-2025\t15:14:49.523\tINFO\tTestMethod\tTest message", result);
    }

    [Fact]
    public void Format_EntryWithoutMethod_ReplacesWithDEFAULT()
    {
        // Arrange
        var formatter = new StandardLogFormatter();
        var entry = new LogEntry
        {
            Date = new DateTime(2025, 3, 10),
            Time = "15:14:49.523",
            Level = LogLevel.WARN,
            Method = null,
            Message = "Test message"
        };

        // Act
        string result = formatter.Format(entry);

        // Assert
        Assert.Contains("DEFAULT", result);
    }

    [Fact]
    public void Format_EntryWithEmptyMethod_ReplacesWithDEFAULT()
    {
        var formatter = new StandardLogFormatter();
        var entry = new LogEntry
        {
            Date = new DateTime(2025, 3, 10),
            Time = "15:14:49.523",
            Level = LogLevel.ERROR,
            Method = "",
            Message = "Test message"
        };

        string result = formatter.Format(entry);
        Assert.Contains("DEFAULT", result);
    }


    [Fact]
    public void Format_NullEntry_ThrowsArgumentNullException()
    {
        var formatter = new StandardLogFormatter();
        Assert.Throws<ArgumentNullException>(() => formatter.Format(null));
    }

    [Fact]
    public void Format_EntryWithTimeHaving4DigitMilliseconds_ReturnsCorrectFormat()
    {
        var formatter = new StandardLogFormatter();
        var entry = new LogEntry
        {
            Date = new DateTime(2025, 3, 10),
            Time = "15:14:51.5882",
            Level = LogLevel.INFO,
            Method = "TestMethod",
            Message = "Test message"
        };

        string result = formatter.Format(entry);
        Assert.Equal("10-03-2025\t15:14:51.5882\tINFO\tTestMethod\tTest message", result);
    }
}