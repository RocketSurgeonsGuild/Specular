# Specular

- Package: `Specular`
- Version: `0.3.3-pr.11`
- Target framework: `net10.0`
- NuGet: [Specular](https://www.nuget.org/packages/Specular)

## Namespaces

### Specular

- [ISpecularProvider](/Specular/pr-preview/pr-353/api/specular/ispecularprovider.md) -- `interface` -- A provider that gets a list of assemblies for a given context
- [KeyedServiceRegistrationAttribute](/Specular/pr-preview/pr-353/api/specular/keyedserviceregistrationattribute.md) -- `class` `sealed` -- Attribute used to define the keyed service registration of a given type. Behaves the same as [ServiceRegistrationAttribute](/Specular/pr-preview/pr-353/api/specular/serviceregistrationattribute.md) except the resulting registration is a [keyed service](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-10.0#keyed-services).
- [KeyedServiceRegistrationAttribute<TService>](/Specular/pr-preview/pr-353/api/specular/keyedserviceregistrationattribute-1.md) -- `class` `sealed` -- Attribute used to define the keyed service registration of a given type
- [KeyedServiceRegistrationAttribute<TService1, TService2, TService3, TService4, TService5, TService6, TService7, TService8>](/Specular/pr-preview/pr-353/api/specular/keyedserviceregistrationattribute-8.md) -- `class` `sealed` -- Attribute used to define the keyed service registration of a given type
- [KeyedServiceRegistrationAttribute<TService1, TService2, TService3, TService4, TService5, TService6, TService7>](/Specular/pr-preview/pr-353/api/specular/keyedserviceregistrationattribute-7.md) -- `class` `sealed` -- Attribute used to define the keyed service registration of a given type
- [KeyedServiceRegistrationAttribute<TService1, TService2, TService3, TService4, TService5, TService6>](/Specular/pr-preview/pr-353/api/specular/keyedserviceregistrationattribute-6.md) -- `class` `sealed` -- Attribute used to define the keyed service registration of a given type
- [KeyedServiceRegistrationAttribute<TService1, TService2, TService3, TService4, TService5>](/Specular/pr-preview/pr-353/api/specular/keyedserviceregistrationattribute-5.md) -- `class` `sealed` -- Attribute used to define the keyed service registration of a given type
- [KeyedServiceRegistrationAttribute<TService1, TService2, TService3, TService4>](/Specular/pr-preview/pr-353/api/specular/keyedserviceregistrationattribute-4.md) -- `class` `sealed` -- Attribute used to define the keyed service registration of a given type
- [KeyedServiceRegistrationAttribute<TService1, TService2, TService3>](/Specular/pr-preview/pr-353/api/specular/keyedserviceregistrationattribute-3.md) -- `class` `sealed` -- Attribute used to define the keyed service registration of a given type
- [KeyedServiceRegistrationAttribute<TService1, TService2>](/Specular/pr-preview/pr-353/api/specular/keyedserviceregistrationattribute-2.md) -- `class` `sealed` -- Attribute used to define the keyed service registration of a given type
- [KeyedServiceRegistrationBaseAttribute](/Specular/pr-preview/pr-353/api/specular/keyedserviceregistrationbaseattribute.md) -- `class` `abstract` -- Attribute used to define the keyed service registration of a given type. Behaves the same as [ServiceRegistrationAttribute](/Specular/pr-preview/pr-353/api/specular/serviceregistrationattribute.md) except the resulting registration is a [keyed service](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-10.0#keyed-services).
- [RegistrationLifetimeAttribute](/Specular/pr-preview/pr-353/api/specular/registrationlifetimeattribute.md) -- `class` -- Defines the lifetime that should be used service registrations created using the [ISpecularProvider](/Specular/pr-preview/pr-353/api/specular/ispecularprovider.md)
- [ServiceRegistrationAttribute](/Specular/pr-preview/pr-353/api/specular/serviceregistrationattribute.md) -- `class` `sealed` -- Attribute used to define the service registration of a given type
- [ServiceRegistrationAttribute<TService>](/Specular/pr-preview/pr-353/api/specular/serviceregistrationattribute-1.md) -- `class` `sealed` -- Attribute used to define the service registration of a given type
- [ServiceRegistrationAttribute<TService1, TService2, TService3, TService4, TService5, TService6, TService7, TService8>](/Specular/pr-preview/pr-353/api/specular/serviceregistrationattribute-8.md) -- `class` `sealed` -- Attribute used to define the service registration of a given type
- [ServiceRegistrationAttribute<TService1, TService2, TService3, TService4, TService5, TService6, TService7>](/Specular/pr-preview/pr-353/api/specular/serviceregistrationattribute-7.md) -- `class` `sealed` -- Attribute used to define the service registration of a given type
- [ServiceRegistrationAttribute<TService1, TService2, TService3, TService4, TService5, TService6>](/Specular/pr-preview/pr-353/api/specular/serviceregistrationattribute-6.md) -- `class` `sealed` -- Attribute used to define the service registration of a given type
- [ServiceRegistrationAttribute<TService1, TService2, TService3, TService4, TService5>](/Specular/pr-preview/pr-353/api/specular/serviceregistrationattribute-5.md) -- `class` `sealed` -- Attribute used to define the service registration of a given type
- [ServiceRegistrationAttribute<TService1, TService2, TService3, TService4>](/Specular/pr-preview/pr-353/api/specular/serviceregistrationattribute-4.md) -- `class` `sealed` -- Attribute used to define the service registration of a given type
- [ServiceRegistrationAttribute<TService1, TService2, TService3>](/Specular/pr-preview/pr-353/api/specular/serviceregistrationattribute-3.md) -- `class` `sealed` -- Attribute used to define the service registration of a given type
- [ServiceRegistrationAttribute<TService1, TService2>](/Specular/pr-preview/pr-353/api/specular/serviceregistrationattribute-2.md) -- `class` `sealed` -- Attribute used to define the service registration of a given type
- [ServiceRegistrationBaseAttribute](/Specular/pr-preview/pr-353/api/specular/serviceregistrationbaseattribute.md) -- `class` `abstract` -- Attribute used to define the service registration of a given type
- [SpecularProviderServiceCollectionExtensions](/Specular/pr-preview/pr-353/api/specular/specularproviderservicecollectionextensions.md) -- `class` `static` -- Extension methods for the service collection using the compiled type provider
- [SpecularSupport](/Specular/pr-preview/pr-353/api/specular/specularsupport.md) -- `class` `static` -- Support class for Specular, this is used to hash the argument expression for the caller.

### Specular.Abstractions

- [ExcludeFromSpecularAttribute](/Specular/pr-preview/pr-353/api/specular/excludefromspecularattribute.md) -- `class` -- Exclude the given assembly from compiled type provider resolution.
- [IReflectionAssemblySelector](/Specular/pr-preview/pr-353/api/specular/ireflectionassemblyselector.md) -- `interface` -- The compiled assembly selector
- [IReflectionTypeSelector](/Specular/pr-preview/pr-353/api/specular/ireflectiontypeselector.md) -- `interface` -- The Compiled Implementation Type Selector
- [IServiceDescriptorAssemblySelector](/Specular/pr-preview/pr-353/api/specular/iservicedescriptorassemblyselector.md) -- `interface` -- The compiled assembly selector
- [IServiceDescriptorTypeSelector](/Specular/pr-preview/pr-353/api/specular/iservicedescriptortypeselector.md) -- `interface` -- The Compiled Implementation Type Selector
- [IServiceLifetimeSelector](/Specular/pr-preview/pr-353/api/specular/iservicelifetimeselector.md) -- `interface` -- The Compiled Lifetime Selector
- [IServiceTypeSelector](/Specular/pr-preview/pr-353/api/specular/iservicetypeselector.md) -- `interface` -- The Compiled Service Type Selector
- [ITypeFilter](/Specular/pr-preview/pr-353/api/specular/itypefilter.md) -- `interface` -- The Compiled Implementation Type Filter
- [SpecularHashAttribute](/Specular/pr-preview/pr-353/api/specular/specularhashattribute.md) -- `class` `sealed` -- Attribute used to define the compiled type provider for a given assembly
- [TypeInfoFilter](/Specular/pr-preview/pr-353/api/specular/typeinfofilter.md) -- `enum` -- Enumeration for possible type information filters.
- [TypeKindFilter](/Specular/pr-preview/pr-353/api/specular/typekindfilter.md) -- `enum` -- Enumeration for possible kinds of type symbols.

### Specular.Diagnostics

- [AssemblyScanReport](/Specular/pr-preview/pr-353/api/specular/assemblyscanreport.md) -- `record` `sealed` -- Contains the result of an assembly scan expression.
- [AssemblyScanReportEntry](/Specular/pr-preview/pr-353/api/specular/assemblyscanreportentry.md) -- `record` `sealed` -- Identifies an assembly selected by an assembly scan expression.
- [ScanResults](/Specular/pr-preview/pr-353/api/specular/scanresults.md) -- `record` `sealed` -- Contains the compile-time results for all Specular scans in an assembly.
- [ServiceDescriptorAssemblyScanReportEntry](/Specular/pr-preview/pr-353/api/specular/servicedescriptorassemblyscanreportentry.md) -- `record` `sealed` -- Contains the registrations produced from one scanned assembly.
- [ServiceDescriptorScanReport](/Specular/pr-preview/pr-353/api/specular/servicedescriptorscanreport.md) -- `record` `sealed` -- Contains the registrations produced by a service descriptor scan expression.
- [ServiceDescriptorScanReportEntry](/Specular/pr-preview/pr-353/api/specular/servicedescriptorscanreportentry.md) -- `record` `sealed` -- Describes one service descriptor produced by a service descriptor scan.
- [TypeScanAssemblyReportEntry](/Specular/pr-preview/pr-353/api/specular/typescanassemblyreportentry.md) -- `record` `sealed` -- Contains the types selected from one assembly by a type scan expression.
- [TypeScanReport](/Specular/pr-preview/pr-353/api/specular/typescanreport.md) -- `record` `sealed` -- Contains the result of a type scan expression.
- [TypeScanReportEntry](/Specular/pr-preview/pr-353/api/specular/typescanreportentry.md) -- `record` `sealed` -- Identifies a type selected by a type scan expression.
