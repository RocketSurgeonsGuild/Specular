# Changelog

All notable changes to Indago will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

### Added

- Initial release of Indago — compile-time assembly/type-scanning for .NET.
- AOT-safe Roslyn incremental source generator that emits `IIndagoProvider` implementations.
- Cross-assembly scan result caching via `IndagoProvider.ctpjson`.
- Fluent selector API compatible with Scrutor conventions.
- Support for .NET 8 and .NET 10.
