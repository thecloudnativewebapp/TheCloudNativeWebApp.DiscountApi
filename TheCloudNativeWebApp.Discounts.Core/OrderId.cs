namespace TheCloudNativeWebApp.Discounts.Core;

public readonly struct OrderId
{
    private readonly Guid _value = Guid.Empty;

    public static OrderId Create(Guid guid) => new(guid);
    
    private OrderId(Guid guid)
    {
        _value = guid;
    }
    
    public Guid ToGuid() => _value;
}