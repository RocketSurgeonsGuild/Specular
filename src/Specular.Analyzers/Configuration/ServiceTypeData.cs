namespace Specular.Analyzers.Configuration;

public record ServiceTypeData(char Identifier, AnyTypeData? TypeData = null, TypeFilterData? TypeFilter = null);
