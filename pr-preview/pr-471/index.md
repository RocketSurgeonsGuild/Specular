# Specular — Static Assembly Scanning for .NET

**AOT & Trim Safe**
Generated code requires zero runtime reflection — fully compatible with .NET Native AOT and IL trimming.

**Build-Time Scanning**
A Roslyn incremental source generator resolves selector expressions at compile time and emits a strongly-typed `ISpecularProvider`.

**Drop-In for Scrutor**
Familiar fluent selector API — `FromAssemblyOf<T>()`, `AddClasses()`, `AsMatchingInterface()` — but evaluated at build time.