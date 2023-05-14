namespace TheCloudNativeWebApp.Discounts.Core;

public struct Euro
{
    private readonly decimal _value = 0;

    public static Euro Create(decimal value) => new(value);

    public static implicit operator Euro(decimal value) => Create(value);
    
    public static implicit operator Euro(double value) => (decimal)value;

    private Euro(decimal value)
    {
        var excess = value % 0.01m;
        if (excess >= 0.001m || value < 0)
        {
            throw new ArgumentException();
        }

        _value = value;
    }

    public bool IsSmallerThan(Euro valueToCompare) => _value < valueToCompare._value;
    
    public decimal ToDecimal() => _value;
}