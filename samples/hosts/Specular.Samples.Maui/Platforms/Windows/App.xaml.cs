namespace Specular.Samples.Maui.WinUI;

/// <summary>The WinUI application that hosts the MAUI app.</summary>
public partial class App : MauiWinUIApplication
{
    /// <summary>Creates the application.</summary>
    public App() => InitializeComponent();

    /// <inheritdoc />
    protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
}
