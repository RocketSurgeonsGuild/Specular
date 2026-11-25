---
title: Scan cache file
description: The shape of the .ctpjson file Specular.Scanning writes for each generator, and the rules that decide when its entries are used.
tags:
    - architecture
    - source-generator
---

# Scan cache file

Generators built on [`Specular.Scanning`](../../guide/scanning-from-your-generator/) keep their results for referenced assemblies
in one file per generator: `$(IntermediateOutputPath)ctp/<ConsumerId>.ctpjson`. This page describes what the file holds and
when its entries are used. You don't need to read or write it yourself; the format is internal and may change between
versions.

## Lifecycle

```
build N    generator scans  ──►  writes obj/…/ctp/<ConsumerId>.ctpjson   (only if the content changed)
build N+1  MSBuild adds ctp/*.ctpjson to AdditionalFiles before CoreCompile
           generator reads its own file  ──►  reuses entries whose MVID still matches
                                         ──►  rescans the rest, writes the file again
clean      MSBuild deletes ctp/*.ctpjson
```

- The generator picks out its file by name: `<ConsumerId>.ctpjson`, ignoring case. Every generator gets its own file, so
  generators never read each other's entries.
- `context.Scan` writes the file from an implementation-only output step, so the IDE never writes it. A generator that saves a
  typed store with `ScanCache.Save` decides where it saves. Either way the file is written only when its content changes,
  which keeps its timestamp, and with it the project's up-to-date check, stable.
- The MSBuild wiring that feeds the file back (`Specular.Scanning.Cache.targets`) ships in every generator package packed with
  the library. See [Packaging and MSBuild](../../guide/scanning-from-your-generator/#packaging-and-msbuild).

## Shape

The file is compact JSON, written on one line. Laid out:

```json
{
    "schema": 1,
    "scans": {
        "<request key>": {
            "MyLib": {
                "mvid": "6f9619ff-8b86-d011-b42d-00c04fc964ff",
                "types": [
                    { "name": "MyLib.FirstPlugin", "kind": 2, "access": 6, "accessible": true },
                    { "name": "MyLib.Outer+NestedPlugin", "kind": 2, "access": 4 }
                ]
            }
        }
    },
    "store": {
        "<store key>": {
            "MyLib": { "mvid": "6f9619ff-8b86-d011-b42d-00c04fc964ff", "version": 1, "data": { "name": "First" } }
        }
    }
}
```

| Field                                    | Meaning                                                                                                                                                                                                                                              |
| ---------------------------------------- | ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| `schema`                                 | The file format version. A file with any other version is ignored and rebuilt.                                                                                                                                                                       |
| `scans`                                  | The types each request matched in each referenced assembly: request key, then assembly metadata name.                                                                                                                                                |
| `scans.<key>.<assembly>.mvid`            | The module version id of the assembly when the entry was written.                                                                                                                                                                                    |
| `scans.<key>.<assembly>.types[]`         | The matched types: `name` (full metadata name), `kind` (Roslyn `TypeKind`), `access` (Roslyn `Accessibility`), `accessible` (whether the consuming compilation can reference it), `arity`, and `assembly` when it differs from the entry's assembly. |
| `store`                                  | The values a generator stored with `ScanCache.Set`: the generator's key, then assembly metadata name.                                                                                                                                                |
| `store.<key>.<assembly>.mvid`, `version` | The assembly's MVID and the generator's data version when the value was written.                                                                                                                                                                     |
| `store.<key>.<assembly>.data`            | The value, serialized with the generator's `JsonTypeInfo<T>`.                                                                                                                                                                                        |

Enums are written as numbers. Default values (`false`, `0`, `null`) are left out. Keys are written in ordinal order, so the
same entries always produce the same bytes.

The request key is `ScanRequest.CacheKey`: a SHA-256 hash, in base64url, of a canonical text form of the request. That form
includes every assembly and type clause, and for each custom filter its class name and its `CacheKey`. The key is stable
across processes and builds, and changes whenever the request changes.

## When an entry is used

| Situation                                                                 | Result                                                                                    |
| ------------------------------------------------------------------------- | ----------------------------------------------------------------------------------------- |
| The referenced assembly's MVID matches the entry's                        | The entry is used; the assembly isn't walked.                                             |
| The MVID differs (the assembly was rebuilt with different content)        | The entry is ignored; the assembly is walked and the entry replaced.                      |
| The reference has no readable MVID (an IDE project reference)             | Never cached on disk; always walked. In-memory reuse still applies.                       |
| The compilation's own assembly                                            | Never cached; always walked.                                                              |
| A typed-store value with a different data version                         | A miss.                                                                                   |
| A typed-store value that no longer deserializes into the generator's type | A miss.                                                                                   |
| The file is missing, empty, corrupt or has another `schema`               | Treated as empty; everything is rescanned and the file rewritten.                         |
| An entry wasn't read or written during the run                            | Dropped when the file is saved, so entries for removed references and requests disappear. |
| The file can't be written (locked, no permission)                         | Ignored; the next build rescans. The cache never fails a build.                           |

## In memory

The disk file only helps across builds. Within one process, such as the IDE while you type, `context.Scan` also remembers each
referenced assembly's results for as long as the compiler keeps the same metadata reference object. An edit to the project's
own code then rescans only the project's own assembly. These results are kept per consuming assembly, because accessibility
depends on who is looking.

## Specular's own file

Specular's generator uses consumer id `SpecularProvider`, so its file keeps the name `SpecularProvider.ctpjson`. What it
stores is described in [Cross-Assembly Caching](../cross-assembly-caching/).
