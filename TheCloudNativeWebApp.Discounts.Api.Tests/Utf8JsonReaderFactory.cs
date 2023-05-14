using System.Text;
using System.Text.Json;

namespace TheCloudNativeWebApp.Discounts.Api.Tests;

public static class Utf8JsonReaderFactory
{
    public static Utf8JsonReader CreateJsonReaderWithValue(string json)
    {
        // Quality of code = number of WTF's per minute
        // Here's some high quality code: (Sarcasm...)
        
        var bytes = Encoding.UTF8.GetBytes(json).AsSpan();
        var reader = new Utf8JsonReader(bytes);
        reader.Read();

        return reader;
        
        // Don't ask...
    }
}