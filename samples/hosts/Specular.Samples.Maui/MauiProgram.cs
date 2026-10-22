using Specular.Samples;

namespace Specular.Samples.Maui;

// Specular.Samples.Maui — proves Specular's COMPILE-TIME scanning in a .NET MAUI app (Android, iOS, Mac Catalyst, Windows).
//
// While the app configures its services it scans the three sample libraries (SampleScan, shared by every host), writes the
// result to the console and shows it. The Mac Catalyst head can also check it and exit without opening a window
// (SPECULAR_SAMPLE_SMOKE=1, see Platforms/MacCatalyst/Program.cs).

/// <summary>Builds the MAUI app.</summary>
public static class MauiProgram
{
    /// <summary>Creates the app.</summary>
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder.UseMauiApp<App>();

        var result = builder.Services.AddSampleServices(SpecularProvider.Instance);
        result.WriteTo(Console.Out, Console.Error);
        builder.Services.AddSingleton(result);

        return builder.Build();
    }
}
