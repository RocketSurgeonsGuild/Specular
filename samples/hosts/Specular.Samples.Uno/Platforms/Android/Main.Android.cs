using Android.Runtime;

namespace Specular.Samples.UnoPlatform.Droid;

[global::Android.App.Application(Label = "@string/ApplicationName", LargeHeap = true, HardwareAccelerated = true, Theme = "@style/AppTheme")]
public class Application : Microsoft.UI.Xaml.NativeApplication
{
    public Application(IntPtr javaReference, JniHandleOwnership transfer)
        : base(() => new App(), javaReference, transfer) { }
}
