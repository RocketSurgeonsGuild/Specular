using Specular.Samples;

namespace Specular.Samples.UnoPlatform;

// Specular.Samples.Uno — proves Specular's COMPILE-TIME scanning in an Uno Platform app (desktop, WebAssembly, Android, iOS).
//
// On launch the app scans the three sample libraries (SampleScan, shared by every host), writes the result to the console (the
// browser console on WebAssembly, where CI waits for the SPECULAR-SAMPLE line, see samples/smoke.mjs) and shows it. The desktop
// head can also check it and exit without opening a window (SPECULAR_SAMPLE_SMOKE=1, see Platforms/Desktop/Program.cs).

/// <summary>The Uno Platform application.</summary>
public partial class App : Application
{
    /// <summary>Creates the application.</summary>
    public App() => InitializeComponent();

    /// <summary>The application's window.</summary>
    protected Window? MainWindow { get; private set; }

    /// <inheritdoc />
    protected override void OnLaunched(LaunchActivatedEventArgs args)
    {
        var result = new ServiceCollection().AddSampleServices(SpecularProvider.Instance);
        result.WriteTo(Console.Out, Console.Error);

        MainWindow = new Window { Content = new MainPage(result) };
        MainWindow.Activate();
    }
}
