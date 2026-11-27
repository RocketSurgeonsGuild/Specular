# Specular

Specular resolves assembly and type scans at build time, so an application can register services and find types without reflecting over assemblies at runtime.

## Language

**Scan request**:
One `GetAssemblies`, `GetTypes` or `Scan` call, described as the assemblies and types it selects.
_Avoid_: Query, selector (for the whole call)

**Local request**:
A scan request written in the source of the project being compiled.
_Avoid_: Own request

**Inherited request**:
A scan request that a referenced library declares, which every project consuming that library resolves against its own view of the assemblies.
_Avoid_: Internal request
