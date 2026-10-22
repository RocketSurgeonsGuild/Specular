; Release tracking: https://github.com/dotnet/roslyn/blob/main/src/RoslynAnalyzers/Microsoft.CodeAnalysis.Analyzers/ReleaseTrackingAnalyzers.Help.md

## Release 0.0.1

### New Rules

Rule ID | Category | Severity | Notes
--------|----------|----------|-------
RSGD0001 | Dependency Injection | Error | RSGD0001: a method the generator analyzes statically is not a single expression; blocks and variables are not allowed
RSGD0002 | Dependency Injection | Error | RSGD0002: a method that must be called with `typeof(...)` was given a variable instead
RSGD0003 | Dependency Injection | Warning | RSGD0003: the generator could not handle the indicated symbol
RSGD0004 | Dependency Injection | Warning | RSGD0004: a namespace filter is not a constant string
RSGD0005 | Dependency Injection | Warning | RSGD0005: a type has more than one service registration attribute, or a value that must be a constant string is not one
RSGD0006 | Dependency Injection | Warning | RSGD0006: the generator hit an unhandled exception; the message carries the exception and stack trace
RSGD0007 | Dependency Injection | Warning | RSGD0007: the service type named in a registration could not be found

## Release 0.0.3

### New Rules

Rule ID | Category | Severity | Notes
--------|----------|----------|-------
RSGD0008 | Dependency Injection | Info | RSGD0008: an inaccessible type was skipped because its assembly exposes no public type to anchor it under Native AOT
