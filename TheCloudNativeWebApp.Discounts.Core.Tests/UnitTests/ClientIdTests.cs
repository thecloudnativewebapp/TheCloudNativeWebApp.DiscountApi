namespace TheCloudNativeWebApp.Discounts.Core.Tests.UnitTests;

public class ClientIdTests
{
    private readonly Faker _faker = new ();
    
    [Fact]
    public void WhenDefault_ShouldBeEmpty()
    {
        var expected = ClientId.Create(Guid.Empty);
        
        var actual = default(ClientId);

        actual.Should().Be(expected);
    }
    
    [Fact]
    public void WhenCreatedWithValue_ShouldSaveValue()
    {
        var guid = _faker.Random.Guid();
        
        var actual = ClientId.Create(guid);

        actual.ToGuid().Should().Be(guid);
    }
}