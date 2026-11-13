using Android.Runtime;
using Uno.UI.Hosting;

namespace Specular.Samples.UnoPlatform.Droid;

[global::Android.App.Application(Label = "@string/ApplicationName", LargeHeap = true, HardwareAccelerated = true, Theme = "@style/AppTheme")]
public class Application : Uno.UI.Runtime.Android.NativeApplication
{
    public Application(IntPtr javaReference, JniHandleOwnership transfer)
        : base(javaReference, transfer) { }

    protected override UnoPlatformHost CreateHost() =>
        UnoPlatformHostBuilder
           .Create()
           .App(() => new App())
           .UseAndroid()
           .Build();
}
