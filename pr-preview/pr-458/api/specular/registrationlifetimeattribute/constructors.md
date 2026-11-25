# RegistrationLifetimeAttribute Constructors

- Package: [Specular](/Specular/pr-preview/pr-458/api/specular.md)
- Type: [RegistrationLifetimeAttribute](/Specular/pr-preview/pr-458/api/specular/registrationlifetimeattribute.md)
- Kind: `Constructors`
- Members: `1`

Defines the lifetime that should be used service registrations created using the [ISpecularProvider](/Specular/pr-preview/pr-458/api/specular/ispecularprovider.md)

<a id="constructor"></a>
<a id="constructor-servicelifetime"></a>

## RegistrationLifetimeAttribute(ServiceLifetime)

- Name: `Constructor(ServiceLifetime)`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-458-merge.Sha.e349b4d07697a27d2f7d2f0edd92c5e34f9b8f68/src/Specular/RegistrationLifetimeAttribute.cs#L14-L20)

Defines the lifetime that should be used service registrations created using the [ISpecularProvider](/Specular/pr-preview/pr-458/api/specular/ispecularprovider.md)

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
