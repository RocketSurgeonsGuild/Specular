namespace Specular.Analyzers.Configuration;

internal sealed record ServiceTypeData(char Identifier, AnyTypeData? TypeData = null, TypeFilterData? TypeFilter = null);
