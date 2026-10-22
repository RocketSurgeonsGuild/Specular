using System.Reflection;
using System.Runtime.Loader;

namespace Specular.Tests;

internal class CollectibleTestAssemblyLoadContext() : AssemblyLoadContext(isCollectible: true), IDisposable
{
    protected override Assembly? Load(AssemblyName assemblyName) => null;

    public void Dispose() => Unload();
}
