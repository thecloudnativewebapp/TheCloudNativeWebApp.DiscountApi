using System.Globalization;
using System.Text.Json;
using TheCloudNativeWebApp.Discounts.Core;
using TheCloudNativeWebApp.Discounts.Api.Configuration.Json;

namespace TheCloudNativeWebApp.Discounts.Api.Tests.UnitTests.Configuration.Json;

public class EuroConverterTests
{
    private readonly EuroConverter _euroConverter = new();

    public EuroConverterTests()
    {
        Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
    }
    
    [Theory]
    [InlineData("0")]
    [InlineData("1")]
    [InlineData("0.1")]
    [InlineData("0.01")]
    public void WhenValidAmount_ShouldConvert(string value)
    {
        var json = Utf8JsonReaderFactory.CreateJsonReaderWithValue($"{value}");

        var actual = _euroConverter.Read(ref json, typeof(Euro), JsonSerializerOptions.Default);

        actual.Should().BeOfType<Euro>();
    }
    
    [Theory]
    [InlineData("1.001")]
    [InlineData("-1")]
    [InlineData("-0.01")]
    public void WhenInvalidAmount_ShouldThrowJsonException(string value)
    {
        var actual = () =>
        {
            var json = Utf8JsonReaderFactory.CreateJsonReaderWithValue($"{value}");
            
            return _euroConverter.Read(ref json, typeof(Euro), JsonSerializerOptions.Default);
        };

        actual.Should().Throw<JsonException>();
    }
}