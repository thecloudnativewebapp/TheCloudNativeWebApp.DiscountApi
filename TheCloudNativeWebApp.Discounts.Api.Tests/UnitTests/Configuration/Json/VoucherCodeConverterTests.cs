using System.Text.Json;
using TheCloudNativeWebApp.Discounts.Core;
using TheCloudNativeWebApp.Discounts.Api.Configuration.Json;

namespace TheCloudNativeWebApp.Discounts.Api.Tests.UnitTests.Configuration.Json;

public class VoucherCodeConverterTests
{
    private readonly VoucherCodeConverter _voucherCodeConverter = new();
    
    [Fact]
    public void WhenValidVoucherCode_ShouldConvert()
    {
        var json = Utf8JsonReaderFactory.CreateJsonReaderWithValue($"\"00000000\"");

        var actual = _voucherCodeConverter.Read(ref json, typeof(VoucherCode), JsonSerializerOptions.Default);

        actual.Should().BeOfType<VoucherCode>();
    }
    
    [Theory]
    [InlineData("")]
    [InlineData("voucher code should only contain digits")]
    public void WhenInvalidVoucherCode_ShouldThrowJsonException(string invalidVoucherCode)
    {
        var actual = () =>
        {
            var json = Utf8JsonReaderFactory.CreateJsonReaderWithValue($"\"${invalidVoucherCode}\"");
            
            return _voucherCodeConverter.Read(ref json, typeof(VoucherCode), JsonSerializerOptions.Default);
        };

        actual.Should().Throw<JsonException>();
    }
    
    [Fact]
    public void WhenNull_ShouldReturnDefault()
    {
        var json = Utf8JsonReaderFactory.CreateJsonReaderWithValue("null");

        var actual = _voucherCodeConverter.Read(ref json, typeof(VoucherCode), JsonSerializerOptions.Default);

        actual.Should().Be(default(VoucherCode));
    }
}