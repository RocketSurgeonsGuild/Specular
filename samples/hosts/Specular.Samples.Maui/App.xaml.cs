using Specular.Samples;

namespace Specular.Samples.Maui;

/// <summary>The MAUI application.</summary>
public partial class App : Application
{
    /// <summary>Creates the application.</summary>
    public App() => InitializeComponent();

    /// <inheritdoc />
    protected override Window CreateWindow(IActivationState? activationState)
    {
        var services = activationState?.Context.Services ?? IPlatformApplication.Current!.Services;
        return new(new MainPage(services.GetRequiredService<SampleScanResult>()));
    }
}
