using System.Text.Json;
using System.Text.Json.Serialization;
using Pure.RelationalSchema.Abstractions.Index;

namespace Pure.RelationalSchema.Abstractions.Serialization.System;

using Index = RelationalSchema.Index.Index;

public sealed class IndexConverter : JsonConverter<IIndex>
{
    public override IIndex Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options
    )
    {
        return JsonSerializer.Deserialize<Index>(ref reader, options)!;
    }

    public override void Write(
        Utf8JsonWriter writer,
        IIndex value,
        JsonSerializerOptions options
    )
    {
        JsonSerializer.Serialize(
            writer,
            new Index(value.IsUnique, value.Columns),
            options
        );
    }
}
