namespace Specular.Samples.HandlerRegistry;

/// <summary>Listed: declared in the app itself, which the generator scans on every run (only referenced assemblies are cached).</summary>
public sealed class WelcomeHandler : IHandler
{
    /// <inheritdoc />
    public string Handle(string message) => $"welcome: {message}";
}

/// <summary>Listed: internal, but declared in the app, so the generated list can name it.</summary>
internal sealed class FarewellHandler : IHandler
{
    /// <inheritdoc />
    public string Handle(string message) => $"farewell: {message}";
}
