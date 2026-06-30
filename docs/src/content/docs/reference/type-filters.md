---
title: Type Filters
description: The fluent selector API for filtering types by class, interface, namespace, attribute, and assignability.
---

# Type Filters

Indago provides a fluent selector API for describing which assemblies and types the generator should include in a scan. There are two parallel selector hierarchies — one for reflection-based results (`GetAssemblies` / `GetTypes`) and one for DI registration (`Scan`). Both share the same `ITypeFilter` interface for per-type predicates.

## Assembly Selectors

### `IReflectionAssemblySelector`

Used with `IIndagoProvider.GetAssemblies` and `IIndagoProvider.GetTypes`. Returns an `IReflectionTypeSelector` to continue the chain.

| Method                             | Description                                                          |
| ---------------------------------- | -------------------------------------------------------------------- |
| `FromAssembly()`                   | Scans the calling assembly.                                          |
| `FromAssemblies()`                 | Scans all metadata assemblies in the compilation.                    |
| `FromAssemblyOf<T>()`              | Scans the assembly that contains `T`.                                |
| `FromAssemblyOf(Type)`             | Non-generic overload of the above.                                   |
| `FromAssemblyDependenciesOf<T>()`  | Scans assemblies that the assembly containing `T` depends on.        |
| `FromAssemblyDependenciesOf(Type)` | Non-generic overload of the above.                                   |
| `NotFromAssemblyOf<T>()`           | Excludes the assembly containing `T` from the scan.                  |
| `NotFromAssemblyOf(Type)`          | Non-generic overload of the above.                                   |
| `IncludeSystemAssemblies()`        | Also includes .NET framework / BCL assemblies (excluded by default). |

### `IServiceDescriptorAssemblySelector`

Used with `IIndagoProvider.Scan`. Has the same set of assembly-selection methods as `IReflectionAssemblySelector`, but each method returns an `IServiceDescriptorTypeSelector` to continue into the DI-specific chain.

## Type Selectors

### `IReflectionTypeSelector`

Extends `IReflectionAssemblySelector`. Provides methods to retrieve `IEnumerable<Type>` directly.

| Method                                           | Description                                                    |
| ------------------------------------------------ | -------------------------------------------------------------- |
| `GetTypes()`                                     | Returns all types in the selected assemblies.                  |
| `GetTypes(bool publicOnly)`                      | Limits results to public types when `true`.                    |
| `GetTypes(Action<ITypeFilter>)`                  | Applies a filter predicate (public non-abstract classes only). |
| `GetTypes(bool publicOnly, Action<ITypeFilter>)` | Combines public-only and filter predicate.                     |

### `IServiceDescriptorTypeSelector`

Extends `IServiceDescriptorAssemblySelector`. Entry point for describing what to register in the DI container.

| Method                                             | Description                                               |
| -------------------------------------------------- | --------------------------------------------------------- |
| `AddClasses()`                                     | Selects all public, non-abstract classes.                 |
| `AddClasses(bool publicOnly)`                      | Controls whether non-public classes are included.         |
| `AddClasses(Action<ITypeFilter>)`                  | Selects public, non-abstract classes matching the filter. |
| `AddClasses(Action<ITypeFilter>, bool publicOnly)` | Filter plus public-only control.                          |

Returns `IServiceTypeSelector` to continue the chain with a service-type strategy.

## Service Type Strategies

### `IServiceTypeSelector`

Specifies how each matched type is registered against service types.

| Method                                         | Description                                                                                                               |
| ---------------------------------------------- | ------------------------------------------------------------------------------------------------------------------------- |
| `AsSelf()`                                     | Registers each class as its own concrete type.                                                                            |
| `As<T>()`                                      | Registers each class as `T`.                                                                                              |
| `As(Type)`                                     | Non-generic overload of `As`.                                                                                             |
| `AsImplementedInterfaces()`                    | Registers each class as every interface it implements.                                                                    |
| `AsImplementedInterfaces(Action<ITypeFilter>)` | Same, filtered to specific interfaces.                                                                                    |
| `AsSelfWithInterfaces()`                       | Registers as self **and** as every implemented interface; the interface registrations return the same singleton instance. |
| `AsSelfWithInterfaces(Action<ITypeFilter>)`    | Same, filtered to specific interfaces.                                                                                    |
| `AsMatchingInterface()`                        | Registers each class as the interface whose name matches `I{ClassName}`.                                                  |

Returns `IServiceLifetimeSelector` to complete the chain.

## Lifetime Selector

### `IServiceLifetimeSelector`

Extends `IServiceTypeSelector`. Specifies the DI service lifetime.

| Method                          | Description                                     |
| ------------------------------- | ----------------------------------------------- |
| `WithSingletonLifetime()`       | Registers with `ServiceLifetime.Singleton`.     |
| `WithScopedLifetime()`          | Registers with `ServiceLifetime.Scoped`.        |
| `WithTransientLifetime()`       | Registers with `ServiceLifetime.Transient`.     |
| `WithLifetime(ServiceLifetime)` | Registers with an explicitly supplied lifetime. |

## Per-Type Filter: `ITypeFilter`

`ITypeFilter` is passed to the `Action<ITypeFilter>` callbacks in `AddClasses`, `GetTypes`, `AsImplementedInterfaces`, and similar methods. All methods return `ITypeFilter` for chaining.

### Assignability

```csharp
ITypeFilter AssignableTo<T>();
ITypeFilter AssignableTo(Type type);
ITypeFilter AssignableToAny(Type type, params Type[] types);
ITypeFilter NotAssignableTo<T>();
ITypeFilter NotAssignableTo(Type type);
ITypeFilter NotAssignableToAny(Type type, params Type[] types);
```

Includes or excludes types based on whether they are assignable to `T` (i.e. implement an interface or inherit from a base class).

### Name Matching

```csharp
ITypeFilter EndsWith(string value, params string[] values);
ITypeFilter NotEndsWith(string value, params string[] values);
ITypeFilter StartsWith(string value, params string[] values);
ITypeFilter NotStartsWith(string value, params string[] values);
ITypeFilter Contains(string value, params string[] values);
ITypeFilter NotContains(string value, params string[] values);
```

Filters types by the text of their simple name.

### Namespace Scoping

```csharp
ITypeFilter InNamespaceOf<T>();
ITypeFilter InNamespaceOf(Type type, params Type[] types);
ITypeFilter InNamespaces(string first, params string[] namespaces);
ITypeFilter InExactNamespaceOf<T>();
ITypeFilter InExactNamespaceOf(Type type, params Type[] types);
ITypeFilter InExactNamespaces(string first, params string[] namespaces);
ITypeFilter NotInNamespaceOf<T>();
ITypeFilter NotInNamespaceOf(Type type, params Type[] types);
ITypeFilter NotInNamespaces(string first, params string[] namespaces);
```

`InNamespaceOf<T>()` matches types in the same namespace **or any child namespace**. `InExactNamespaceOf<T>()` matches only the exact namespace.

### Attribute Presence

```csharp
ITypeFilter WithAttribute<T>() where T : Attribute;
ITypeFilter WithAttribute(Type attributeType);
ITypeFilter WithAttribute(string? attributeFullName);
ITypeFilter WithAnyAttribute(Type attributeType, params Type[] attributeTypes);
ITypeFilter WithAnyAttribute(string? attributeFullName, params string[] attributeFullNames);
ITypeFilter WithoutAttribute<T>() where T : Attribute;
ITypeFilter WithoutAttribute(Type attributeType);
ITypeFilter WithoutAttribute(string? attributeFullName);
```

Filters types by the presence or absence of an attribute. The overload accepting a string takes the fully-qualified attribute type name.

### Type Kind

```csharp
ITypeFilter KindOf(TypeKindFilter typeKindFilter, params TypeKindFilter[] typeKindFilters);
ITypeFilter NotKindOf(TypeKindFilter typeKindFilter, params TypeKindFilter[] typeKindFilters);
```

`TypeKindFilter` values: `Array`, `Class`, `Delegate`, `Enum`, `Interface`, `Struct`.

### Type Info

```csharp
ITypeFilter InfoOf(TypeInfoFilter typeInfoFilter, params TypeInfoFilter[] typeInfoFilters);
ITypeFilter NotInfoOf(TypeInfoFilter typeInfoFilter, params TypeInfoFilter[] typeInfoFilters);
```

`TypeInfoFilter` values: `Unknown`, `Abstract`, `Visible`, `ValueType`, `Sealed`, `GenericType`, `Static`.

## `[ExcludeFromIndago]` interaction

Applying `[ExcludeFromIndago]` to an assembly removes the entire assembly from Indago scan results regardless of any selector. See [ExcludeFromIndagoAttribute](./exclude-from-indago) for details.

## Complete chained example

```csharp
// Register all public, non-abstract classes in the MyApp assembly that
// implement IMyService, as their implemented interfaces, with scoped lifetime.
provider.Scan(
    services,
    s => s
        .FromAssemblyOf<MyApp>()
        .AddClasses(c => c.AssignableTo<IMyService>())
        .AsImplementedInterfaces()
        .WithScopedLifetime()
);
```

```csharp
// Retrieve all public types in the MyApp assembly that end with "Repository"
// and are in the Data namespace.
IEnumerable<Type> repos = provider.GetTypes(
    s => s
        .FromAssemblyOf<MyApp>()
        .GetTypes(f => f
            .EndsWith("Repository")
            .InNamespaceOf<MyApp.Data.Placeholder>())
);
```

## See also

- [IIndagoProvider](./iindago-provider) — the entry point that accepts these selectors
- [ServiceRegistrationAttribute](./service-registration) — attribute-driven alternative
