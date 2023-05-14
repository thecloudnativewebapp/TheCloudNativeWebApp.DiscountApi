namespace TheCloudNativeWebApp.Discounts.Core.Tests.UnitTests;

public class EuroTests
{
    [Fact]
    public void WhenDefaultValue_ShouldEqualZero()
    {
        var expected = Euro.Create(-0.00m);

        var actual = default(Euro);

        actual.Should().Be(expected);
    }
    
    [Fact]
    public void WhenNegativeValue_ShouldThrowArgumentException()
    {
        var actual = () => Euro.Create(-0.01m);

        actual.Should().Throw<ArgumentException>();
    }

    [Theory]
    [InlineData(1.001)]
    [InlineData(0.001)]
    [InlineData(0.121)]
    [InlineData(1.121)]
    public void WhenMoreThanTwoDigitsAfterPoint_ShouldThrowArgumentException(decimal value)
    {
        var actual = () => Euro.Create(value);

        actual.Should().Throw<ArgumentException>();
    }
    
    [Theory]
    [InlineData(0)]
    [InlineData(0.01)]
    [InlineData(1)]
    [InlineData(12873648723874.23)]
    public void WhenAmount_ShouldCreateEuro(decimal value)
    {
        var actual = Euro.Create(value);

        actual.ToDecimal().Should().Be(value);
    }
}