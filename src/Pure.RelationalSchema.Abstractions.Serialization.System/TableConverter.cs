using System.Text.Json;
using System.Text.Json.Serialization;
using Pure.RelationalSchema.Abstractions.Table;

namespace Pure.RelationalSchema.Abstractions.Serialization.System;

using Table = RelationalSchema.Table.Table;

public sealed class TableConverter : JsonConverter<ITable>
{
    public override ITable Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options
    )
    {
        return JsonSerializer.Deserialize<Table>(ref reader, options)!;
    }

    public override void Write(
        Utf8JsonWriter writer,
        ITable value,
        JsonSerializerOptions options
    )
    {
        JsonSerializer.Serialize(
            writer,
            new Table(value.Name, value.Columns, value.Indexes),
            options
        );
    }
}
