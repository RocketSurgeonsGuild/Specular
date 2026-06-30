// Indago.Samples.NegativeFixture — the permanent D2 regression detector (FR-020/FR-021, SC-003).
//
// Its sole purpose is to emit a STABLE, Indago-attributable trim/AOT warning under Native AOT
// publish. A separate inverted-assertion pipeline step (NegativeFixtureAotPublishModule) publishes
// this project WITHOUT warnings-as-errors and PASSES only if the expected warning is present —
// FAILING the build if the publish ever goes clean (which would mean the guardrail can no longer
// prove that failures are caught).
//
// The trigger: feed Indago's IIndagoProvider.GetTypes(...) results — whose element Type carries no
// [DynamicallyAccessedMembers] annotation by design — into Activator.CreateInstance(Type). The trim
// analyzer attributes the warning to Indago's GetTypes return value (IL2072), so the signature names
// an Indago API. This is intentionally NOT how the demonstration hosts use Indago.

using Indago.Samples.NegativeFixture;

var provider = IIndagoProvider.EntryAssembly;

foreach (var type in provider.GetTypes(s => s.EntryAssembly().GetTypes(f => f.AssignableTo<Marker>())))
{
    // IL2072: the 'type' argument to Activator.CreateInstance does not satisfy
    // 'DynamicallyAccessedMemberTypes.PublicParameterlessConstructor'; the return value of
    // 'IIndagoProvider.GetTypes(...)' does not have matching annotations.
    _ = Activator.CreateInstance(type);
}

return 0;

namespace Indago.Samples.NegativeFixture
{
    internal sealed class Marker;
}
