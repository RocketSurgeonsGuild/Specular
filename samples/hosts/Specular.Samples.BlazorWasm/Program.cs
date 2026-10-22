using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Specular.Samples;
using Specular.Samples.BlazorWasm;

// Specular.Samples.BlazorWasm — proves Specular's COMPILE-TIME scanning in the browser (Mono WebAssembly).
//
// The app scans the three sample libraries (SampleScan, shared by every host) while it configures its services, writes the
// result to the browser console (CI waits for the SPECULAR-SAMPLE line, see samples/smoke.mjs) and renders it.

var builder = WebAssemblyHostBuilder.CreateDefault(args);

var result = builder.Services.AddSampleServices(SpecularProvider.Instance);
result.WriteTo(Console.Out, Console.Error);
builder.Services.AddSingleton(result);

builder.RootComponents.Add<App>("#app");

await builder.Build().RunAsync();
