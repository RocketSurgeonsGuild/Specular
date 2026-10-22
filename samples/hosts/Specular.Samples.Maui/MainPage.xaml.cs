using Specular.Samples;

namespace Specular.Samples.Maui;

/// <summary>Shows what Specular registered.</summary>
public partial class MainPage : ContentPage
{
    internal MainPage(SampleScanResult result)
    {
        InitializeComponent();
        SummaryLabel.Text = result.Summary;
        ServicesLabel.Text = string.Join(Environment.NewLine, result.Discovered);
    }
}
