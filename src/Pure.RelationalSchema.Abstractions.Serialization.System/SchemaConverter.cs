using System.Text.Json;
using System.Text.Json.Serialization;
using Pure.RelationalSchema.Abstractions.Schema;

namespace Pure.RelationalSchema.Abstractions.Serialization.System;

using Schema = RelationalSchema.Schema.Schema;

public sealed class SchemaConverter : JsonConverter<ISchema>
{
    public override ISchema Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options
    )
    {
        return JsonSerializer.Deserialize<Schema>(ref reader, options)!;
    }

    public override void Write(
        Utf8JsonWriter writer,
        ISchema value,
        JsonSerializerOptions options
    )
    {
        JsonSerializer.Serialize(
            writer,
            new Schema(value.Name, value.Tables, value.ForeignKeys),
            options
        );
    }
}
