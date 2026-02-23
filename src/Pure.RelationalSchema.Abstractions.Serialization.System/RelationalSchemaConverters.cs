using System.Collections;
using System.Text.Json.Serialization;

namespace Pure.RelationalSchema.Abstractions.Serialization.System;

public sealed class RelationalSchemaConverters : IEnumerable<JsonConverter>
{
    public IEnumerator<JsonConverter> GetEnumerator()
    {
        yield return new ColumnConverter();
        yield return new ColumnTypeConverter();
        yield return new ForeignKeyConverter();
        yield return new IndexConverter();
        yield return new SchemaConverter();
        yield return new TableConverter();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
