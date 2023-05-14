using System.Text.Json;
using System.Text.Json.Serialization;

namespace TheCloudNativeWebApp.Discounts.Api.Configuration.Json;

public abstract class JsonStringConverter<T> : JsonConverter<T>
{
    public override T? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var value = reader.GetString();
        try
        {
            return value == null ? default : Create(value);
        }
        catch (ArgumentException e)
        {
            throw new JsonException($"{value} is not a valid {typeToConvert}", e);
        }
    }

    protected abstract T? Create(string value);
    
    public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}