# ITypeFilter

- Kind: `interface`
- Package: [Specular](/Specular/v0.3.1/api/specular.md)
- Version: `0.3.1`
- Namespace: `Specular.Abstractions`
- Target framework: `net10.0`

The Compiled Implementation Type Filter

## Definition

```csharp
namespace Specular.Abstractions;

public interface ITypeFilter
{
    // ...
}
```

## Methods

- [AssignableTo](/Specular/v0.3.1/api/specular/itypefilter/methods.md#assignableto) : [ITypeFilter](/Specular/v0.3.1/api/specular/itypefilter.md) `abstract` -- Will match all types that are assignable to `T`.
- [AssignableTo(Type)](/Specular/v0.3.1/api/specular/itypefilter/methods.md#assignableto-type) : [ITypeFilter](/Specular/v0.3.1/api/specular/itypefilter.md) `abstract` -- Will match all types that are assignable to the specified `type`.
- [AssignableToAny(Type, Type[])](/Specular/v0.3.1/api/specular/itypefilter/methods.md#assignabletoany-type-type) : [ITypeFilter](/Specular/v0.3.1/api/specular/itypefilter.md) `abstract` -- Will match all types that are assignable to any of the specified `types`.
- [Contains(string, string[])](/Specular/v0.3.1/api/specular/itypefilter/methods.md#contains-string-string) : [ITypeFilter](/Specular/v0.3.1/api/specular/itypefilter.md) `abstract` -- Will match all types that contain the given values
- [EndsWith(string, string[])](/Specular/v0.3.1/api/specular/itypefilter/methods.md#endswith-string-string) : [ITypeFilter](/Specular/v0.3.1/api/specular/itypefilter.md) `abstract` -- Will match all types that end with
- [InExactNamespaceOf](/Specular/v0.3.1/api/specular/itypefilter/methods.md#inexactnamespaceof) : [ITypeFilter](/Specular/v0.3.1/api/specular/itypefilter.md) `abstract` -- Will match all types in the exact same namespace as the type `T`
- [InExactNamespaceOf(Type, Type[])](/Specular/v0.3.1/api/specular/itypefilter/methods.md#inexactnamespaceof-type-type) : [ITypeFilter](/Specular/v0.3.1/api/specular/itypefilter.md) `abstract` -- Will match all types in the exact same namespace as the type `types`
- [InExactNamespaces(string, string[])](/Specular/v0.3.1/api/specular/itypefilter/methods.md#inexactnamespaces-string-string) : [ITypeFilter](/Specular/v0.3.1/api/specular/itypefilter.md) `abstract` -- Will match all types in the exact same namespace as the type `namespaces`
- [InfoOf(TypeInfoFilter, TypeInfoFilter[])](/Specular/v0.3.1/api/specular/itypefilter/methods.md#infoof-typeinfofilter-typeinfofilter) : [ITypeFilter](/Specular/v0.3.1/api/specular/itypefilter.md) `abstract` -- Will match all types that are of the specified `typeInfoFilter`.
- [InNamespaceOf](/Specular/v0.3.1/api/specular/itypefilter/methods.md#innamespaceof) : [ITypeFilter](/Specular/v0.3.1/api/specular/itypefilter.md) `abstract` -- Will match all types in the same namespace as the type `T`.
- [InNamespaceOf(Type, Type[])](/Specular/v0.3.1/api/specular/itypefilter/methods.md#innamespaceof-type-type) : [ITypeFilter](/Specular/v0.3.1/api/specular/itypefilter.md) `abstract` -- Will match all types in any of the namespaces of the `types` specified.
- [InNamespaces(string, string[])](/Specular/v0.3.1/api/specular/itypefilter/methods.md#innamespaces-string-string) : [ITypeFilter](/Specular/v0.3.1/api/specular/itypefilter.md) `abstract` -- Will match all types in any of the `namespaces` specified.
- [KindOf(TypeKindFilter, TypeKindFilter[])](/Specular/v0.3.1/api/specular/itypefilter/methods.md#kindof-typekindfilter-typekindfilter) : [ITypeFilter](/Specular/v0.3.1/api/specular/itypefilter.md) `abstract` -- Will match all types that are of the specified `typeKindFilter`.
- [NotAssignableTo](/Specular/v0.3.1/api/specular/itypefilter/methods.md#notassignableto) : [ITypeFilter](/Specular/v0.3.1/api/specular/itypefilter.md) `abstract` -- Will not match all types that are assignable to `T`.
- [NotAssignableTo(Type)](/Specular/v0.3.1/api/specular/itypefilter/methods.md#notassignableto-type) : [ITypeFilter](/Specular/v0.3.1/api/specular/itypefilter.md) `abstract` -- Will not match all types that are assignable to the specified `type`.
- [NotAssignableToAny(Type, Type[])](/Specular/v0.3.1/api/specular/itypefilter/methods.md#notassignabletoany-type-type) : [ITypeFilter](/Specular/v0.3.1/api/specular/itypefilter.md) `abstract` -- Will not match all types that are assignable to any of the specified `types`.
- [NotContains(string, string[])](/Specular/v0.3.1/api/specular/itypefilter/methods.md#notcontains-string-string) : [ITypeFilter](/Specular/v0.3.1/api/specular/itypefilter.md) `abstract` -- Will match all types that contain the given values
- [NotEndsWith(string, string[])](/Specular/v0.3.1/api/specular/itypefilter/methods.md#notendswith-string-string) : [ITypeFilter](/Specular/v0.3.1/api/specular/itypefilter.md) `abstract` -- Will match all types that end with
- [NotInfoOf(TypeInfoFilter, TypeInfoFilter[])](/Specular/v0.3.1/api/specular/itypefilter/methods.md#notinfoof-typeinfofilter-typeinfofilter) : [ITypeFilter](/Specular/v0.3.1/api/specular/itypefilter.md) `abstract` -- Will match all types that are not of the specified `typeInfoFilter`.
- [NotInNamespaceOf](/Specular/v0.3.1/api/specular/itypefilter/methods.md#notinnamespaceof) : [ITypeFilter](/Specular/v0.3.1/api/specular/itypefilter.md) `abstract` -- Will match all types outside of the same namespace as the type `T`.
- [NotInNamespaceOf(Type, Type[])](/Specular/v0.3.1/api/specular/itypefilter/methods.md#notinnamespaceof-type-type) : [ITypeFilter](/Specular/v0.3.1/api/specular/itypefilter.md) `abstract` -- Will match all types outside of all of the namespaces of the `types` specified.
- [NotInNamespaces(string, string[])](/Specular/v0.3.1/api/specular/itypefilter/methods.md#notinnamespaces-string-string) : [ITypeFilter](/Specular/v0.3.1/api/specular/itypefilter.md) `abstract` -- Will match all types outside of all of the `namespaces` specified.
- [NotKindOf(TypeKindFilter, TypeKindFilter[])](/Specular/v0.3.1/api/specular/itypefilter/methods.md#notkindof-typekindfilter-typekindfilter) : [ITypeFilter](/Specular/v0.3.1/api/specular/itypefilter.md) `abstract` -- Will match all types that are not of the specified `typeKindFilter`.
- [NotStartsWith(string, string[])](/Specular/v0.3.1/api/specular/itypefilter/methods.md#notstartswith-string-string) : [ITypeFilter](/Specular/v0.3.1/api/specular/itypefilter.md) `abstract` -- Will match all types that start with
- [StartsWith(string, string[])](/Specular/v0.3.1/api/specular/itypefilter/methods.md#startswith-string-string) : [ITypeFilter](/Specular/v0.3.1/api/specular/itypefilter.md) `abstract` -- Will match all types that start with
- [WithAnyAttribute(Type, Type[])](/Specular/v0.3.1/api/specular/itypefilter/methods.md#withanyattribute-type-type) : [ITypeFilter](/Specular/v0.3.1/api/specular/itypefilter.md) `abstract` -- Will match all types that has an attribute of `attributeType` defined.
- [WithAnyAttribute(string?, string[])](/Specular/v0.3.1/api/specular/itypefilter/methods.md#withanyattribute-string-string) : [ITypeFilter](/Specular/v0.3.1/api/specular/itypefilter.md) `abstract` -- Will match all types that has an attribute of `attributeFullName` defined.
- [WithAttribute](/Specular/v0.3.1/api/specular/itypefilter/methods.md#withattribute) : [ITypeFilter](/Specular/v0.3.1/api/specular/itypefilter.md) `abstract` -- Will match all types that has an attribute of type `T` defined.
- [WithAttribute(Type)](/Specular/v0.3.1/api/specular/itypefilter/methods.md#withattribute-type) : [ITypeFilter](/Specular/v0.3.1/api/specular/itypefilter.md) `abstract` -- Will match all types that has an attribute of `attributeType` defined.
- [WithAttribute(string?)](/Specular/v0.3.1/api/specular/itypefilter/methods.md#withattribute-string) : [ITypeFilter](/Specular/v0.3.1/api/specular/itypefilter.md) `abstract` -- Will match all types that has an attribute of `attributeFullName` defined.
- [WithoutAttribute](/Specular/v0.3.1/api/specular/itypefilter/methods.md#withoutattribute) : [ITypeFilter](/Specular/v0.3.1/api/specular/itypefilter.md) `abstract` -- Will match all types that doesn't have an attribute of type `T` defined.
- [WithoutAttribute(Type)](/Specular/v0.3.1/api/specular/itypefilter/methods.md#withoutattribute-type) : [ITypeFilter](/Specular/v0.3.1/api/specular/itypefilter.md) `abstract` -- Will match all types that doesn't have an attribute of `attributeType` defined.
- [WithoutAttribute(string?)](/Specular/v0.3.1/api/specular/itypefilter/methods.md#withoutattribute-string) : [ITypeFilter](/Specular/v0.3.1/api/specular/itypefilter.md) `abstract` -- Will match all types that doesn't have an attribute of `attributeFullName` defined.
