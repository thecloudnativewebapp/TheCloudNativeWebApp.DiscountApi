namespace TheCloudNativeWebApp.Discounts.Core;

public readonly struct ZoneInfo
{
    private const string DefaultZone = "Europe/Amsterdam";
    
    private static readonly string[] SupportedZones = new[]
    {
        "Europe/Amsterdam",
        "Europe/London",
        "America/Los_Angeles"
    };

    private readonly string? _value;

    public static ZoneInfo Create(string zoneInfo) => new(zoneInfo);

    private ZoneInfo(string zoneInfo)
    {
        if (!SupportedZones.Contains(zoneInfo))
        {
            throw new ArgumentException();
        }

        _value = zoneInfo == DefaultZone 
            ? null 
            : zoneInfo;
    }

    public override string ToString() => _value ?? DefaultZone;
}