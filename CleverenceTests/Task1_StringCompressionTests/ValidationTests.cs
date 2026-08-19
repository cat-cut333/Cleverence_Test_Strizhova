using Xunit;
using Task1_StringCompression.Validation.Services;

public class ValidationTests
{
    private readonly DefaultStringValidator _validator = new DefaultStringValidator();

    [Fact]
    public void IsValid_ValidString_ReturnsTrue()
    {
        // Arrange
        string input = "abc";

        // Act
        bool result = _validator.IsValid(input);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void IsValid_InvalidString_ReturnsFalse()
    {
        string input = "ab c";
        bool result = _validator.IsValid(input);
        Assert.False(result);
    }

    [Fact]
    public void IsValid_EmptyString_ReturnsFalse()
    {
        string input = "";
        bool result = _validator.IsValid(input);
        Assert.False(result);
    }

    [Fact]
    public void IsValid_EmptyStringWithAllowEmpty_ReturnsTrue()
    {
        string input = "";
        bool result = _validator.IsValid(input, allowEmpty: true);
        Assert.True(result);
    }

    [Fact]
    public void IsValid_NullString_ReturnsFalse()
    {
        string input = null;
        bool result = _validator.IsValid(input);
        Assert.False(result);
    }

    [Fact]
    public void ValidateOrThrow_ValidString_DoesNotThrow()
    {
        string input = "abc";
        var exception = Record.Exception(() => _validator.ValidateOrThrow(input));
        Assert.Null(exception);
    }

    
}
