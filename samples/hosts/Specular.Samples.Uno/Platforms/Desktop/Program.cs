using Specular.Samples;
using Uno.UI.Hosting;

namespace Specular.Samples.UnoPlatform;

internal static class Program
{
    [STAThread]
    public static int Main(string[] args)
    {
        // SPECULAR_SAMPLE_SMOKE=1: check the scan and exit before any window opens, so CI runs this on headless runners.
        if (SampleScan.RunSmokeTest(SpecularProvider.Instance) is { } exitCode) return exitCode;

        var host = UnoPlatformHostBuilder
                  .Create()
                  .App(() => new App())
                  .UseX11()
                  .UseLinuxFrameBuffer()
                  .UseMacOS()
                  .UseWin32()
                  .Build();

        host.Run();
        return 0;
    }
}
