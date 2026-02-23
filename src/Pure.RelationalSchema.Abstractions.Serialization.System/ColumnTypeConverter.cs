using System.Text.Json;
using System.Text.Json.Serialization;
using Pure.RelationalSchema.Abstractions.ColumnType;
using Pure.RelationalSchema.ColumnType;

namespace Pure.RelationalSchema.Abstractions.Serialization.System;

public sealed class ColumnTypeConverter : JsonConverter<IColumnType>
{
    public override IColumnType Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options
    )
    {
        string columnType = JsonSerializer.Deserialize<string>(ref reader, options)!;
        IEnumerable<IColumnType> types =
        [
            new BoolColumnType(),
            new DateColumnType(),
            new DateTimeColumnType(),
            new DeterminedHashColumnType(),
            new IntColumnType(),
            new LongColumnType(),
            new StringColumnType(),
            new TimeColumnType(),
            new UIntColumnType(),
            new ULongColumnType(),
            new UShortColumnType(),
        ];

        return types.FirstOrDefault(x => x.Name.TextValue == columnType)
            ?? throw new JsonException($"Unable to deserialize {nameof(IColumnType)}");
    }

    public override void Write(
        Utf8JsonWriter writer,
        IColumnType value,
        JsonSerializerOptions options
    )
    {
        JsonSerializer.Serialize(writer, value.Name, options);
    }
}
