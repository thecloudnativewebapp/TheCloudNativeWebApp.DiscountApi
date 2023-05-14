using System.Text.Json;
using System.Text.Json.Serialization;
using TheCloudNativeWebApp.Discounts.Core;

namespace TheCloudNativeWebApp.Discounts.Api.Configuration.Json;

public class EuroConverter : JsonConverter<Euro>
{
    public override Euro Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var value = reader.GetDecimal();
        try
        {
            return Euro.Create(value);
        }
        catch (ArgumentException e)
        {
            throw new JsonException($"{value} is not a valid {typeToConvert}", e);
        }
    }

    public override void Write(Utf8JsonWriter writer, Euro value, JsonSerializerOptions options)
    {
        writer.WriteNumberValue(value.ToDecimal());
    }
}