# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Commands

All `dotnet` commands must be run from the `./src` directory.

```bash
dotnet restore
dotnet build --no-restore -warnaserror
dotnet format --verify-no-changes             # check code style (CI enforces this)
dotnet format && csharpier format .           # auto-fix code style
dotnet test --no-build --verbosity normal --logger trx --collect:"XPlat Code Coverage"
dotnet pack --configuration Release -p:PackageVersion=<version> --output .
```

CI also runs mutation testing via `dotnet stryker --mutation-level Complete`. Run locally with `dotnet tool install -g dotnet-stryker` then `dotnet stryker`.

## Architecture

This is a **JSON converter library** — six sealed `JsonConverter<T>` classes plus one `IEnumerable<JsonConverter>` collection. No interfaces, no domain logic. Every file defines exactly one public type.

**Public types:**

- `ColumnConverter : JsonConverter<IColumn>` — deserializes to `Column(name, type)`
- `ColumnTypeConverter : JsonConverter<IColumnType>` — serializes as the type name string; deserializes by matching against all known `IColumnType` implementations
- `ForeignKeyConverter : JsonConverter<IForeignKey>` — deserializes to `ForeignKey(referencingTable, referencingColumns, referencedTable, referencedColumns)`
- `IndexConverter : JsonConverter<IIndex>` — deserializes to `Index(isUnique, columns)`
- `SchemaConverter : JsonConverter<ISchema>` — deserializes to `Schema(name, tables, foreignKeys)`
- `TableConverter : JsonConverter<ITable>` — deserializes to `Table(name, columns, indexes)`
- `RelationalSchemaConverters : IEnumerable<JsonConverter>` — yields all six converters; register with `JsonSerializerOptions.Converters`

**Dependency on `Pure.RelationalSchema`:** this package depends on the concrete implementations to drive serialization. The `Read` methods deserialize into the concrete types (e.g., `Column`, `Schema`), and the `Write` methods construct a new concrete instance from the interface value before serializing — this ensures the output is stable regardless of which implementation of `ISchema` (or any other interface) is passed in.

**Multi-targeting:** net7.0, net8.0, net9.0, net10.0. `IsAotCompatible` is `false` — the `ColumnTypeConverter` builds an `IEnumerable` of all known `IColumnType` instances at runtime to match by name.

**Package validation:** `EnablePackageValidation = true` with `PackageValidationBaselineVersion = 0.1.0-preview.1.0.0`. Breaking changes fail the build.

**Publishing:** triggered by pushing a semver tag (`*.*.*`). The tag becomes the `PackageVersion`. Packages are published to both GitHub Packages and NuGet.org.

## Code Style

Enforced via `.editorconfig`, `dotnet format --verify-no-changes`, and `csharpier check .` in CI:

- No `var` — always use explicit types
- No expression-bodied methods, constructors, or operators; expression-bodied properties, indexers, accessors, and lambdas are allowed
- File-scoped namespaces (`namespace Foo.Bar;`)
- `using` directives outside the namespace
- Allman braces — open brace always on its own line
- Max line length: 90 characters
- Private fields: `_camelCase`
- Explicit accessibility modifiers required on all members

## Commit Messages

Do not mention Claude or AI assistance in commit messages.
