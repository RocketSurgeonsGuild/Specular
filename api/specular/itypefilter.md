# ITypeFilter

- Kind: `interface`
- Package: [Specular](/Specular/api/specular.md)
- Version: `0.3.3-beta.5`
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

- [AssignableTo](/Specular/api/specular/itypefilter/methods.md#assignableto) : [ITypeFilter](/Specular/api/specular/itypefilter.md) `abstract` -- Will match all types that are assignable to `T`.
- [AssignableTo(Type)](/Specular/api/specular/itypefilter/methods.md#assignableto-type) : [ITypeFilter](/Specular/api/specular/itypefilter.md) `abstract` -- Will match all types that are assignable to the specified `type`.
- [AssignableToAny(Type, Type[])](/Specular/api/specular/itypefilter/methods.md#assignabletoany-type-type) : [ITypeFilter](/Specular/api/specular/itypefilter.md) `abstract` -- Will match all types that are assignable to any of the specified `types`.
- [Contains(string, string[])](/Specular/api/specular/itypefilter/methods.md#contains-string-string) : [ITypeFilter](/Specular/api/specular/itypefilter.md) `abstract` -- Will match all types that contain the given values
- [EndsWith(string, string[])](/Specular/api/specular/itypefilter/methods.md#endswith-string-string) : [ITypeFilter](/Specular/api/specular/itypefilter.md) `abstract` -- Will match all types that end with
- [InExactNamespaceOf](/Specular/api/specular/itypefilter/methods.md#inexactnamespaceof) : [ITypeFilter](/Specular/api/specular/itypefilter.md) `abstract` -- Will match all types in the exact same namespace as the type `T`
- [InExactNamespaceOf(Type, Type[])](/Specular/api/specular/itypefilter/methods.md#inexactnamespaceof-type-type) : [ITypeFilter](/Specular/api/specular/itypefilter.md) `abstract` -- Will match all types in the exact same namespace as the type `types`
- [InExactNamespaces(string, string[])](/Specular/api/specular/itypefilter/methods.md#inexactnamespaces-string-string) : [ITypeFilter](/Specular/api/specular/itypefilter.md) `abstract` -- Will match all types in the exact same namespace as the type `namespaces`
- [InfoOf(TypeInfoFilter, TypeInfoFilter[])](/Specular/api/specular/itypefilter/methods.md#infoof-typeinfofilter-typeinfofilter) : [ITypeFilter](/Specular/api/specular/itypefilter.md) `abstract` -- Will match all types that are of the specified `typeInfoFilter`.
- [InNamespaceOf](/Specular/api/specular/itypefilter/methods.md#innamespaceof) : [ITypeFilter](/Specular/api/specular/itypefilter.md) `abstract` -- Will match all types in the same namespace as the type `T`.
- [InNamespaceOf(Type, Type[])](/Specular/api/specular/itypefilter/methods.md#innamespaceof-type-type) : [ITypeFilter](/Specular/api/specular/itypefilter.md) `abstract` -- Will match all types in any of the namespaces of the `types` specified.
- [InNamespaces(string, string[])](/Specular/api/specular/itypefilter/methods.md#innamespaces-string-string) : [ITypeFilter](/Specular/api/specular/itypefilter.md) `abstract` -- Will match all types in any of the `namespaces` specified.
- [KindOf(TypeKindFilter, TypeKindFilter[])](/Specular/api/specular/itypefilter/methods.md#kindof-typekindfilter-typekindfilter) : [ITypeFilter](/Specular/api/specular/itypefilter.md) `abstract` -- Will match all types that are of the specified `typeKindFilter`.
- [NotAssignableTo](/Specular/api/specular/itypefilter/methods.md#notassignableto) : [ITypeFilter](/Specular/api/specular/itypefilter.md) `abstract` -- Will not match all types that are assignable to `T`.
- [NotAssignableTo(Type)](/Specular/api/specular/itypefilter/methods.md#notassignableto-type) : [ITypeFilter](/Specular/api/specular/itypefilter.md) `abstract` -- Will not match all types that are assignable to the specified `type`.
- [NotAssignableToAny(Type, Type[])](/Specular/api/specular/itypefilter/methods.md#notassignabletoany-type-type) : [ITypeFilter](/Specular/api/specular/itypefilter.md) `abstract` -- Will not match all types that are assignable to any of the specified `types`.
- [NotContains(string, string[])](/Specular/api/specular/itypefilter/methods.md#notcontains-string-string) : [ITypeFilter](/Specular/api/specular/itypefilter.md) `abstract` -- Will match all types that contain the given values
- [NotEndsWith(string, string[])](/Specular/api/specular/itypefilter/methods.md#notendswith-string-string) : [ITypeFilter](/Specular/api/specular/itypefilter.md) `abstract` -- Will match all types that end with
- [NotInfoOf(TypeInfoFilter, TypeInfoFilter[])](/Specular/api/specular/itypefilter/methods.md#notinfoof-typeinfofilter-typeinfofilter) : [ITypeFilter](/Specular/api/specular/itypefilter.md) `abstract` -- Will match all types that are not of the specified `typeInfoFilter`.
- [NotInNamespaceOf](/Specular/api/specular/itypefilter/methods.md#notinnamespaceof) : [ITypeFilter](/Specular/api/specular/itypefilter.md) `abstract` -- Will match all types outside of the same namespace as the type `T`.
- [NotInNamespaceOf(Type, Type[])](/Specular/api/specular/itypefilter/methods.md#notinnamespaceof-type-type) : [ITypeFilter](/Specular/api/specular/itypefilter.md) `abstract` -- Will match all types outside of all of the namespaces of the `types` specified.
- [NotInNamespaces(string, string[])](/Specular/api/specular/itypefilter/methods.md#notinnamespaces-string-string) : [ITypeFilter](/Specular/api/specular/itypefilter.md) `abstract` -- Will match all types outside of all of the `namespaces` specified.
- [NotKindOf(TypeKindFilter, TypeKindFilter[])](/Specular/api/specular/itypefilter/methods.md#notkindof-typekindfilter-typekindfilter) : [ITypeFilter](/Specular/api/specular/itypefilter.md) `abstract` -- Will match all types that are not of the specified `typeKindFilter`.
- [NotStartsWith(string, string[])](/Specular/api/specular/itypefilter/methods.md#notstartswith-string-string) : [ITypeFilter](/Specular/api/specular/itypefilter.md) `abstract` -- Will match all types that start with
- [StartsWith(string, string[])](/Specular/api/specular/itypefilter/methods.md#startswith-string-string) : [ITypeFilter](/Specular/api/specular/itypefilter.md) `abstract` -- Will match all types that start with
- [WithAnyAttribute(Type, Type[])](/Specular/api/specular/itypefilter/methods.md#withanyattribute-type-type) : [ITypeFilter](/Specular/api/specular/itypefilter.md) `abstract` -- Will match all types that has an attribute of `attributeType` defined.
- [WithAnyAttribute(string?, string[])](/Specular/api/specular/itypefilter/methods.md#withanyattribute-string-string) : [ITypeFilter](/Specular/api/specular/itypefilter.md) `abstract` -- Will match all types that has an attribute of `attributeFullName` defined.
- [WithAttribute](/Specular/api/specular/itypefilter/methods.md#withattribute) : [ITypeFilter](/Specular/api/specular/itypefilter.md) `abstract` -- Will match all types that has an attribute of type `T` defined.
- [WithAttribute(Type)](/Specular/api/specular/itypefilter/methods.md#withattribute-type) : [ITypeFilter](/Specular/api/specular/itypefilter.md) `abstract` -- Will match all types that has an attribute of `attributeType` defined.
- [WithAttribute(string?)](/Specular/api/specular/itypefilter/methods.md#withattribute-string) : [ITypeFilter](/Specular/api/specular/itypefilter.md) `abstract` -- Will match all types that has an attribute of `attributeFullName` defined.
- [WithoutAttribute](/Specular/api/specular/itypefilter/methods.md#withoutattribute) : [ITypeFilter](/Specular/api/specular/itypefilter.md) `abstract` -- Will match all types that doesn't have an attribute of type `T` defined.
- [WithoutAttribute(Type)](/Specular/api/specular/itypefilter/methods.md#withoutattribute-type) : [ITypeFilter](/Specular/api/specular/itypefilter.md) `abstract` -- Will match all types that doesn't have an attribute of `attributeType` defined.
- [WithoutAttribute(string?)](/Specular/api/specular/itypefilter/methods.md#withoutattribute-string) : [ITypeFilter](/Specular/api/specular/itypefilter.md) `abstract` -- Will match all types that doesn't have an attribute of `attributeFullName` defined.
