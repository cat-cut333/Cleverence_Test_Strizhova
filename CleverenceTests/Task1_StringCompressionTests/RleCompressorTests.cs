using Xunit;
using Task1_StringCompression.Compression.Services;
using Task1_StringCompression.Validation.Services;

public class RleCompressorTests
{
    [Fact]
    public void Compress_ValidString_ReturnsCompressed()
    {
        // Arrange
        var validator = new DefaultStringValidator();
        var compressor = new RleCompressor(validator);
        string input = "aaabbcccdde";

        // Act
        string result = compressor.Compress(input);

        // Assert
        Assert.Equal("a3b2c3d2e", result);
    }

    [Fact]
    public void Decompress_ValidCompressedString_ReturnsOriginal()
    {
        var validator = new DefaultStringValidator();
        var compressor = new RleCompressor(validator);
        string input = "a3b2c3d2e";

        string result = compressor.Decompress(input);

        Assert.Equal("aaabbcccdde", result);
    }

    [Fact]
    public void Compress_EmptyString_ReturnsEmpty()
    {
        var validator = new DefaultStringValidator();
        var compressor = new RleCompressor(validator);

        string result = compressor.Compress("");

        Assert.Equal("", result);
    }

    [Fact]
    public void Compress_InvalidString_ThrowsArgumentException()
    {
        var validator = new DefaultStringValidator();
        var compressor = new RleCompressor(validator);
        string invalid = "aa bb";

        Assert.Throws<ArgumentException>(() => compressor.Compress(invalid));
    }

    [Fact]
    public void CompressDecompress_RoundTrip_ReturnsOriginal()
    {
        var validator = new DefaultStringValidator();
        var compressor = new RleCompressor(validator);
        string original = "aaabbcccdde";

        string compressed = compressor.Compress(original);
        string decompressed = compressor.Decompress(compressed);

        Assert.Equal(original, decompressed);
    }

}