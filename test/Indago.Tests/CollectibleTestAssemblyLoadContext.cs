using System.Reflection;
using System.Runtime.Loader;

namespace Indago.Tests;

internal class CollectibleTestAssemblyLoadContext : AssemblyLoadContext, IDisposable
{
    protected override Assembly? Load(AssemblyName assemblyName) => null;

    public void Dispose()
    {
#if NETCOREAPP3_1
            Unload();
#endif
    }
}
