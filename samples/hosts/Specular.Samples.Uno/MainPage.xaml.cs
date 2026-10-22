using Specular.Samples;

namespace Specular.Samples.UnoPlatform;

/// <summary>Shows what Specular registered.</summary>
public sealed partial class MainPage : Page
{
    internal MainPage(SampleScanResult result)
    {
        InitializeComponent();
        SummaryText.Text = result.Summary;
        ServicesList.ItemsSource = result.Discovered;
    }
}
