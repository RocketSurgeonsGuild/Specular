using Android.App;
using Android.Views;

namespace Specular.Samples.UnoPlatform.Droid;

[Activity(
    MainLauncher = true,
    ConfigurationChanges = global::Uno.UI.ActivityHelper.AllConfigChanges,
    WindowSoftInputMode = SoftInput.AdjustNothing | SoftInput.StateHidden,
    Exported = true
)]
public class MainActivity : Uno.UI.Runtime.Android.ApplicationActivity;
