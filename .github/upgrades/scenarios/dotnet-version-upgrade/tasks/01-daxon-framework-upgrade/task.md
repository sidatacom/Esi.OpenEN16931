# 01-daxon-framework-upgrade: Upgrade the DAXon project and compatibility-sensitive code

Configure the SDK-style DAXon project to support both `net472` and `net10.0` through multi-targeting. The assessment reports one binary-incompatible API, 414 source-incompatible API findings, and 167 behavioral-change findings. Most findings are concentrated in `System.Numerics.BigInteger` source compatibility and `System.Uri` behavior; investigate the affected files and resolve all required changes inline under the confirmed Fix Inline policy.

## Research Findings

- **Project**: `origins/sashokfestival/DAXon/src/OutSmart.DAXon/OutSmart.DAXon.csproj`.
- **Project shape**: One SDK-style `Microsoft.NET.Sdk` class library with no project references, dependants, or imported local MSBuild targets.
- **Targeting decision**: Use `<TargetFrameworks>net10.0;net472</TargetFrameworks>`; preserve `net472` and add `net10.0`.
- **Package reference**: Keep `Microsoft.NETFramework.ReferenceAssemblies` 1.0.3 with `PrivateAssets=All`, conditioned to `net472` only.
- **Current properties**: `LangVersion` is `11.0`, nullable is disabled, unsafe blocks are enabled, and the existing warning configuration predates this upgrade.
- **Resources**: Three XML resources under `data/` use explicit logical names and must remain unchanged.
- **Compatibility signals**: 412 source findings involve `System.Numerics.BigInteger`; `System.Uri` is the main behavioral-change family. Other findings include `DirectoryInfo.FullName`, `TimeSpan.FromMinutes`, and `WebRequest.Create`.
- **Stubs**: No `// STUB:` markers were found.
- **SDK**: A compatible .NET 10 SDK is installed.
- **Current code state**: The project file has already been changed to multi-targeting and the conditional reference-assemblies package has been restored in the project file. The prior temporary single-target experiment was corrected after the user's multi-targeting clarification.

## Execution Scope

1. Restore and build both target frameworks after the project-file correction.
2. Resolve all source and behavioral API compatibility errors inline, adding target-specific conditionals only where `net472` and `net10.0` genuinely differ.
3. Review and fix all warnings in the touched project without adding warning suppressions.

**Done when**: The project targets both `net472` and `net10.0`, both targets restore successfully, all identified compatibility changes are resolved without temporary stubs or new warning suppressions, and both target builds have zero errors and zero warnings.
