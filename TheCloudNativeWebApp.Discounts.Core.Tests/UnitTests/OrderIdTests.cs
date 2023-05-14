namespace TheCloudNativeWebApp.Discounts.Core.Tests.UnitTests;

public class OrderIdTests
{
    private readonly Faker _faker = new ();
    
    [Fact]
    public void WhenDefault_ShouldBeEmpty()
    {
        var expected = OrderId.Create(Guid.Empty);
        
        var actual = default(OrderId);

        actual.Should().Be(expected);
    }
    
    [Fact]
    public void WhenCreatedWithValue_ShouldSaveValue()
    {
        var guid = _faker.Random.Guid();
        
        var actual = OrderId.Create(guid);

        actual.ToGuid().Should().Be(guid);
    }
}