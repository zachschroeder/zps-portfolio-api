namespace Portfolio.Infrastructure.Serialization;

using System.Text.Json;
using Azure.Core.Serialization;

public class CamelCaseSerializer : JsonObjectSerializer
{
    private readonly JsonSerializerOptions _options = new() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

    public override void Serialize(Stream stream, object? value, Type inputType, CancellationToken cancellationToken)
    {
        var buffer = JsonSerializer.SerializeToUtf8Bytes(value, inputType, _options);
        stream.Write(buffer, 0, buffer.Length);
    }

    public override async ValueTask SerializeAsync(Stream stream, object? value, Type inputType, CancellationToken cancellationToken)
    {
        await JsonSerializer.SerializeAsync(stream, value, inputType, _options, cancellationToken).ConfigureAwait(false);
    }
}
