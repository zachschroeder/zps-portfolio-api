namespace Portfolio.Infrastructure;

using System;
using System.Text.Json;
using System.Text.Json.Serialization;

public class StrictStringConverter : JsonConverter<string>
{
    public override string? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var stringToDeserialize = reader.GetString();

        if (string.IsNullOrEmpty(stringToDeserialize))
            throw new JsonException("String must not be null or empty");

        return stringToDeserialize;        
    }

    public override void Write(Utf8JsonWriter writer, string value, JsonSerializerOptions options)
    {} // Not used for deserialization
}