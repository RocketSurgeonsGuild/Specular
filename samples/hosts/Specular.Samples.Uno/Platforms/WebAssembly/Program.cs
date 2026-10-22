using Uno.UI.Hosting;

namespace Specular.Samples.UnoPlatform;

internal static class Program
{
    public static async Task Main(string[] args)
    {
        var host = UnoPlatformHostBuilder
                  .Create()
                  .App(() => new App())
                  .UseWebAssembly()
                  .Build();

        await host.RunAsync();
    }
}
