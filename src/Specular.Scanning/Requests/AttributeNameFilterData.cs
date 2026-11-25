namespace Specular.Scanning;

/// <summary>An attribute clause naming attributes by full metadata name, without resolving them.</summary>
/// <param name="Include">True: the type must have any of the attributes. False: none of them.</param>
/// <param name="AttributeNames">Full metadata names of the attribute classes.</param>
#if SPECULAR_SCANNING_SOURCES
internal
#else
public
#endif
sealed record AttributeNameFilterData(bool Include, EquatableArray<string> AttributeNames);
