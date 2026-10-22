using Specular.Samples;

// Specular.Samples.Web — proves Specular's COMPILE-TIME scanning in a minimal-API host.
//
// At startup the host scans the three sample libraries (SampleScan, shared by every host; no runtime reflection — each Scan
// call is resolved by the source generator at build time), and FAILS FAST (nonzero exit) before the server starts if the
// registrations don't equal the expected set. The `/services` endpoint reports the discovered registrations. Native-AOT
// publishable with zero warnings (the AotPublishModule pipeline step).

var builder = WebApplication.CreateSlimBuilder(args);

var result = builder.Services.AddSampleServices(SpecularProvider.Instance);
result.WriteTo(Console.Out, Console.Error);
if (!result.IsMatch)
{
    // Fail fast BEFORE serving so a CI smoke test / health probe sees a non-zero exit on mismatch.
    return 1;
}

var app = builder.Build();

// Demonstration endpoints (plain text keeps this AOT/trim-clean without a source-generated JSON context).
app.MapGet("/services", () => string.Join('\n', result.Discovered));
app.MapGet("/", () => $"Specular.Samples.Web — {result.Summary} See /services.");

app.Run();

return 0;
