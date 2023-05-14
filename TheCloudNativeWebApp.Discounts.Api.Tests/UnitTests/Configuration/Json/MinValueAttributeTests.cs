using TheCloudNativeWebApp.Discounts.Api.Configuration.Json;
using TheCloudNativeWebApp.Discounts.Core;

namespace TheCloudNativeWebApp.Discounts.Api.Tests.UnitTests.Configuration.Json;

public class MinValueAttributeTests
{
    [Theory]
    [InlineData(1.0, 1.0)]
    [InlineData(1.0, 1.01)]
    public void WhenValidValue_ShouldReturnTrue(double minValue, double @double)
    {
        // Unfortunately xUnit doesn't work well with decimals, so an implicit cast is required here
        Euro valueToCompare = @double;

        var sut = new MinAmountAttribute(minValue);

        var actual = sut.IsValid(valueToCompare);

        actual.Should().BeTrue();
    }
    
    [Fact]
    public void WhenInvalidValue_ShouldReturnFalse()
    {
        Euro valueToCompare = 0.9;

        var sut = new MinAmountAttribute(1);

        var actual = sut.IsValid(valueToCompare);

        actual.Should().BeFalse();
    }
    
    [Fact]
    public void WhenNull_ShouldReturnTrue()
    {
        var sut = new MinAmountAttribute(1);

        var actual = sut.IsValid(null);

        actual.Should().BeTrue();
    }
}