using Specular.Samples;

// Specular.Samples.Console — proves compile-time scanning of the three sample libraries end-to-end.
//
// The host performs Specular's COMPILE-TIME scanning (no runtime reflection): the Scan calls in SampleScan (shared by every
// host) are resolved by the source generator at build time into a concrete ISpecularProvider. It prints the discovered
// services and exits nonzero on a mismatch, so CI can treat the exit code as pass/fail.

var result = new ServiceCollection().AddSampleServices(SpecularProvider.Instance);
result.WriteTo(Console.Out, Console.Error);
return result.IsMatch ? 0 : 1;
