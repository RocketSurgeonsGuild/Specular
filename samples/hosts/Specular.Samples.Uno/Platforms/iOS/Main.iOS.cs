using Uno.UI.Hosting;

namespace Specular.Samples.UnoPlatform.iOS;

public static class EntryPoint
{
    public static void Main(string[] args)
    {
        var host = UnoPlatformHostBuilder
                  .Create()
                  .App(() => new App())
                  .UseAppleUIKit()
                  .Build();

        host.Run();
    }
}
