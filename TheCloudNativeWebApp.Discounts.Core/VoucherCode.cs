using System.Text.RegularExpressions;

namespace TheCloudNativeWebApp.Discounts.Core;

public readonly struct VoucherCode
{
    private const string DefaultValue = "00000000";
    
    private readonly string? _value = DefaultValue;

    public static VoucherCode Create(string value) => new (value);

    private VoucherCode(string value)
    {
        if (!Regex.IsMatch(value, "^[0-9]{8}$"))
        {
            throw new ArgumentException("Invalid vouchercode.");
        }
        
        _value = value == DefaultValue ? null : value;
    }

    public override string ToString() => _value ?? DefaultValue;
}