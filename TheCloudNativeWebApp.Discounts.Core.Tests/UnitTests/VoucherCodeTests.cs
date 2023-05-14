namespace TheCloudNativeWebApp.Discounts.Core.Tests.UnitTests;

public class VoucherCodeTests
{
    [Fact]
    public void WhenDefault_ValueShouldBe00000000()
    {
        var expected = VoucherCode.Create("00000000");
        
        var actual = default(VoucherCode);

        actual.Should().Be(expected);
    }
    
    [Theory]
    [InlineData("1000000")]
    [InlineData("100000000")]
    [InlineData("abcdefgh")]
    [InlineData("100.0000")]
    [InlineData("100,0000")]
    [InlineData("-0000000")]
    [InlineData("10000000,1")]
    public void WhenNotExactlyEightNumbers_ShouldThrowArgumentException(string invalidValue)
    {   
        var actual = () => VoucherCode.Create(invalidValue);

        actual.Should().Throw<ArgumentException>().WithMessage("Invalid vouchercode.");
    }

    [Theory]
    [InlineData("00000000")]
    [InlineData("12345678")]
    public void WhenEightNumbers_ShouldCreateVoucherCode(string value)
    {
        var actual = VoucherCode.Create(value);

        actual.ToString().Should().Be(value);
    }
}