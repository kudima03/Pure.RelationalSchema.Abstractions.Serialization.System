# Pure.RelationalSchema.Abstractions.Serialization.System

`System.Text.Json` converters for **Pure.RelationalSchema** abstract types — plug-and-play serialization for `ISchema`, `ITable`, `IColumn`, `IColumnType`, `IForeignKey`, and `IIndex`.

[![.NET build & test](https://github.com/kudima03/Pure.RelationalSchema.Abstractions.Serialization.System/actions/workflows/build-and-test.yml/badge.svg?branch=main)](https://github.com/kudima03/Pure.RelationalSchema.Abstractions.Serialization.System/actions/workflows/build-and-test.yml)
[![Build and Deploy](https://github.com/kudima03/Pure.RelationalSchema.Abstractions.Serialization.System/actions/workflows/publish-nuget.yml/badge.svg?branch=main)](https://github.com/kudima03/Pure.RelationalSchema.Abstractions.Serialization.System/actions/workflows/publish-nuget.yml)
[![NuGet](https://img.shields.io/nuget/v/Pure.RelationalSchema.Abstractions.Serialization.System)](https://www.nuget.org/packages/Pure.RelationalSchema.Abstractions.Serialization.System)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](LICENSE)

## Overview

`Pure.RelationalSchema.Abstractions.Serialization.System` provides a set of `System.Text.Json` `JsonConverter` implementations that bridge the gap between the interface-typed Pure.RelationalSchema model and JSON serialization. Because the domain model exposes only interfaces (`ISchema`, `ITable`, etc.), the standard `System.Text.Json` serializer cannot resolve a concrete type at deserialization time — these converters handle that mapping explicitly.

All converters read and write via the concrete types from `Pure.RelationalSchema` and are collected in a single `RelationalSchemaConverters` enumerable for convenient bulk registration.

## Converters

| Class | Handles | Description |
|-------|---------|-------------|
| `ColumnConverter` | `IColumn` | Reads/writes via `Column(name, type)` |
| `ColumnTypeConverter` | `IColumnType` | Serializes as the type's name string; deserializes by matching against all known column types |
| `ForeignKeyConverter` | `IForeignKey` | Reads/writes via `ForeignKey(referencingTable, referencingColumns, referencedTable, referencedColumns)` |
| `IndexConverter` | `IIndex` | Reads/writes via `Index(isUnique, columns)` |
| `SchemaConverter` | `ISchema` | Reads/writes via `Schema(name, tables, foreignKeys)` |
| `TableConverter` | `ITable` | Reads/writes via `Table(name, columns, indexes)` |
| `RelationalSchemaConverters` | — | `IEnumerable<JsonConverter>` yielding all six converters above |

## Dependencies

- [`Pure.RelationalSchema`](https://github.com/kudima03/Pure.RelationalSchema/tree/2.0.1) — concrete implementations of the relational schema domain model (`Column`, `Table`, `Schema`, `ForeignKey`, `Index`, and all `IColumnType` variants)

## Target Frameworks

- .NET 7
- .NET 8
- .NET 9
- .NET 10

## Installation

```shell
dotnet add package Pure.RelationalSchema.Abstractions.Serialization.System
```

## Usage

Register all converters at once via `RelationalSchemaConverters`:

```csharp
using System.Text.Json;
using Pure.RelationalSchema.Abstractions.Schema;
using Pure.RelationalSchema.Abstractions.Serialization.System;

var options = new JsonSerializerOptions();
foreach (JsonConverter converter in new RelationalSchemaConverters())
    options.Converters.Add(converter);

string json = JsonSerializer.Serialize(schema, options);
ISchema deserialized = JsonSerializer.Deserialize<ISchema>(json, options)!;
```
