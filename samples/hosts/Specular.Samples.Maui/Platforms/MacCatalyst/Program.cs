using Specular.Samples;
using UIKit;

namespace Specular.Samples.Maui;

public static class Program
{
    // This is the main entry point of the application.
    private static int Main(string[] args)
    {
        // SPECULAR_SAMPLE_SMOKE=1: check the scan and exit before UIKit starts, so CI can run the app bundle's executable.
        if (SampleScan.RunSmokeTest(SpecularProvider.Instance) is { } exitCode) return exitCode;

        UIApplication.Main(args, null, typeof(AppDelegate));
        return 0;
    }
}
