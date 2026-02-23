using System.Text.Json;
using System.Text.Json.Serialization;
using Pure.RelationalSchema.Abstractions.ForeignKey;

namespace Pure.RelationalSchema.Abstractions.Serialization.System;

using ForeignKey = RelationalSchema.ForeignKey.ForeignKey;

public sealed class ForeignKeyConverter : JsonConverter<IForeignKey>
{
    public override IForeignKey Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options
    )
    {
        return JsonSerializer.Deserialize<ForeignKey>(ref reader, options)!;
    }

    public override void Write(
        Utf8JsonWriter writer,
        IForeignKey value,
        JsonSerializerOptions options
    )
    {
        JsonSerializer.Serialize(
            writer,
            new ForeignKey(
                value.ReferencingTable,
                value.ReferencingColumns,
                value.ReferencedTable,
                value.ReferencedColumns
            ),
            options
        );
    }
}
