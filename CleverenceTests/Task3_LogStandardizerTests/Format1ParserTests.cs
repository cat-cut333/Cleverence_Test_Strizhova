using Xunit;
using Task3_LogStandardizer.Parsers;
using Task3_LogStandardizer.Core.Enums;
using System;

namespace Task3_LogStandardizerTests
{
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
        public void Parse_LineWithWarningLevel_ReturnsWARN()
        {
            // Arrange
            var parser = new Format1Parser();
            string line = "10.03.2025 15:14:49.523 WARNING Предупреждение";

            // Act
            var result = parser.Parse(line);

            // Assert
            Assert.Equal(LogLevel.WARN, result.Level);
        }

        [Fact]
        public void Parse_LineWithErrorLevel_ReturnsERROR()
        {
            // Arrange
            var parser = new Format1Parser();
            string line = "10.03.2025 15:14:49.523 ERROR Ошибка";

            // Act
            var result = parser.Parse(line);

            // Assert
            Assert.Equal(LogLevel.ERROR, result.Level);
        }

        [Fact]
        public void Parse_LineWithDebugLevel_ReturnsDEBUG()
        {
            // Arrange
            var parser = new Format1Parser();
            string line = "10.03.2025 15:14:49.523 DEBUG Отладка";

            // Act
            var result = parser.Parse(line);

            // Assert
            Assert.Equal(LogLevel.DEBUG, result.Level);
        }

        [Fact]
        public void Parse_LineWithInformationLevel_ReturnsINFO()
        {
            // Arrange
            var parser = new Format1Parser();
            string line = "10.03.2025 15:14:49.523 INFORMATION Информация";

            // Act
            var result = parser.Parse(line);

            // Assert
            Assert.Equal(LogLevel.INFO, result.Level);
        }

        [Fact]
        public void Parse_EmptyLine_ThrowsArgumentException()
        {
            // Arrange
            var parser = new Format1Parser();
            string invalidLine = "";

            // Act & Assert
            Assert.Throws<ArgumentException>(() => parser.Parse(invalidLine));
        }

        [Fact]
        public void Parse_NullLine_ThrowsArgumentException()
        {
            // Arrange
            var parser = new Format1Parser();
            string invalidLine = null;

            // Act & Assert
            Assert.Throws<ArgumentException>(() => parser.Parse(invalidLine));
        }

        [Fact]
        public void Parse_InvalidDateFormat_ThrowsArgumentException()
        {
            // Arrange
            var parser = new Format1Parser();
            string invalidLine = "2025/03/10 15:14:49.523 INFORMATION Сообщение";

            // Act & Assert
            Assert.Throws<ArgumentException>(() => parser.Parse(invalidLine));
        }


        [Fact]
        public void Parse_InvalidLevel_ThrowsArgumentException()
        {
            // Arrange
            var parser = new Format1Parser();
            string invalidLine = "10.03.2025 15:14:49.523 UNKNOWN Сообщение";

            // Act & Assert
            Assert.Throws<ArgumentException>(() => parser.Parse(invalidLine));
        }

        [Fact]
        public void Parse_LineWithoutMessage_ThrowsArgumentException()
        {
            // Arrange
            var parser = new Format1Parser();
            string invalidLine = "10.03.2025 15:14:49.523 INFORMATION";

            // Act & Assert
            Assert.Throws<ArgumentException>(() => parser.Parse(invalidLine));
        }
    }
}