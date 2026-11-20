# RegistrationLifetimeAttribute Constructors

- Package: [Specular](/Specular/pr-preview/pr-448/api/specular.md)
- Type: [RegistrationLifetimeAttribute](/Specular/pr-preview/pr-448/api/specular/registrationlifetimeattribute.md)
- Kind: `Constructors`
- Members: `1`

Defines the lifetime that should be used service registrations created using the [ISpecularProvider](/Specular/pr-preview/pr-448/api/specular/ispecularprovider.md)

<a id="constructor"></a>
<a id="constructor-servicelifetime"></a>

## RegistrationLifetimeAttribute(ServiceLifetime)

- Name: `Constructor(ServiceLifetime)`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-448-merge.Sha.69052bf45a1b3de253f1409be1700c8f80800784/src/Specular/RegistrationLifetimeAttribute.cs#L14-L20)

Defines the lifetime that should be used service registrations created using the [ISpecularProvider](/Specular/pr-preview/pr-448/api/specular/ispecularprovider.md)

```csharp
public class RegistrationLifetimeAttribute
{
    public RegistrationLifetimeAttribute(
        ServiceLifetime lifetime)
    {
        // ...
    }
}
```

## Parameters

- `lifetime` (`ServiceLifetime`)

## Remarks

Constructor to specify the lifetime
