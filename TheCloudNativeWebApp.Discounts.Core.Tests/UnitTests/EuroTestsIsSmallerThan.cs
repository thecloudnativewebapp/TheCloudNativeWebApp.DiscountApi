namespace TheCloudNativeWebApp.Discounts.Core.Tests.UnitTests;

public class EuroIsSmallerThanTests
{
    [Fact]
    public void WhenSmallerThan_ShouldReturnTrue()
    {
        var value = Euro.Create(1);
        var valueToCompare = Euro.Create(1.01m);

        var actual = value.IsSmallerThan(valueToCompare);

        actual.Should().BeTrue();
    }
    
    [Fact]
    public void WhenEqual_ShouldReturnFalse()
    {
        var value = Euro.Create(1);
        var valueToCompare = Euro.Create(1);

        var actual = value.IsSmallerThan(valueToCompare);

        actual.Should().BeFalse();
    }
    
    [Fact]
    public void WhenGreaterThan_ShouldReturnFalse()
    {
        var value = Euro.Create(1);
        var valueToCompare = Euro.Create(0.99m);

        var actual = value.IsSmallerThan(valueToCompare);

        actual.Should().BeFalse();
    }
}