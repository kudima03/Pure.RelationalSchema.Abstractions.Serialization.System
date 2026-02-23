using System.Text.Json;
using System.Text.Json.Serialization;
using Pure.RelationalSchema.Abstractions.Column;

namespace Pure.RelationalSchema.Abstractions.Serialization.System;

using Column = RelationalSchema.Column.Column;

public sealed class ColumnConverter : JsonConverter<IColumn>
{
    public override IColumn Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options
    )
    {
        return JsonSerializer.Deserialize<Column>(ref reader, options)!;
    }

    public override void Write(
        Utf8JsonWriter writer,
        IColumn value,
        JsonSerializerOptions options
    )
    {
        JsonSerializer.Serialize(writer, new Column(value.Name, value.Type), options);
    }
}
