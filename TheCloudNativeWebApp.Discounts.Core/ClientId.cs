namespace TheCloudNativeWebApp.Discounts.Core;

public readonly struct ClientId
{
    private readonly Guid _value = Guid.Empty;

    public static ClientId Create(Guid guid) => new(guid);
    
    private ClientId(Guid guid)
    {
        _value = guid;
    }
    
    public Guid ToGuid() => _value;
}