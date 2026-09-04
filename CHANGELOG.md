# Changelog

All notable changes to Pure.RelationalSchema.Abstractions.Serialization.System are documented here.

Format follows [Keep a Changelog](https://keepachangelog.com/en/1.1.0/).

---

## [0.1.0-preview.1.0.3] — 2026-08-04

### Changed

- Updated the `Pure.RelationalSchema` dependency to 2.0.3.

## [0.1.0-preview.1.0.2] — 2026-06-17

### Fixed

- **`ColumnTypeConverter`** now recognizes `FloatColumnType` and `UuidColumnType`
  when reading — values of these column types previously failed to deserialize.

## [0.1.0-preview.1.0.1] — 2026-05-28

### Changed

- Updated the `Pure.RelationalSchema` dependency to 2.0.2.

## [0.1.0-preview.1.0.0] — 2026-05-08

### Added

- **`ColumnTypeConverter`** now recognizes `DoubleColumnType`.

### Changed

- Updated the `Pure.RelationalSchema` dependency to 2.0.1.

## [0.1.0-preview.0.2.0] — 2026-02-23

### Added

- **`RelationalSchemaConverters`** — `IEnumerable<JsonConverter>` yielding all six
  converters (`ColumnConverter`, `ColumnTypeConverter`, `ForeignKeyConverter`,
  `IndexConverter`, `SchemaConverter`, `TableConverter`) for convenient bulk
  registration with `JsonSerializerOptions.Converters`.

## [0.1.0-preview.0.1.0] — 2026-02-23

Initial release.

### Added

- **`ColumnConverter`** — `JsonConverter<IColumn>`; reads/writes via
  `Column(name, type)`.
- **`ColumnTypeConverter`** — `JsonConverter<IColumnType>`; serializes as the
  type's name string, deserializes by matching against known `IColumnType`
  implementations.
- **`ForeignKeyConverter`** — `JsonConverter<IForeignKey>`; reads/writes via
  `ForeignKey(referencingTable, referencingColumns, referencedTable, referencedColumns)`.
- **`IndexConverter`** — `JsonConverter<IIndex>`; reads/writes via
  `Index(isUnique, columns)`.
- **`SchemaConverter`** — `JsonConverter<ISchema>`; reads/writes via
  `Schema(name, tables, foreignKeys)`.
- **`TableConverter`** — `JsonConverter<ITable>`; reads/writes via
  `Table(name, columns, indexes)`.
