namespace TheCloudNativeWebApp.Discounts.Core.Tests.UnitTests;

public class ZoneInfoTests
{
    [Fact]
    public void WhenDefault_ShouldBeEuropeAmsterdam()
    {
        var expected = ZoneInfo.Create("Europe/Amsterdam");
        
        var actual = default(ZoneInfo);

        actual.Should().Be(expected);
    }
    
    [Theory]
    [InlineData("Europe/Amsterdam")]
    [InlineData("Europe/London")]
    [InlineData("America/Los_Angeles")]
    public void WhenSupportedZoneInfo_ShouldCreateZoneInfo(string supportedZoneInfo)
    {
        var actual = ZoneInfo.Create(supportedZoneInfo);

        actual.ToString().Should().Be(supportedZoneInfo);
    }
    
    [Theory]
    [InlineData("Europe/Berlin")]
    [InlineData("InvalidValue")]
    [InlineData("")]
    public void WhenUnsupportedZoneInfo_ShouldThrowArgumentException(string supportedZoneInfo)
    {
        var actual = () => ZoneInfo.Create(supportedZoneInfo);

        actual.Should().Throw<ArgumentException>();
    }
}