# 01.01-daxon-target-and-restore: Retarget the DAXon project and restore dependencies

## Objective
Configure the SDK-style DAXon class library to build both `net472` and `net10.0`, preserving existing .NET Framework support while adding .NET 10.

## Scope
- `origins/sashokfestival/DAXon/src/OutSmart.DAXon/OutSmart.DAXon.csproj`
- Target-specific framework reference package and MSBuild properties

## Research Findings

- The target framework is defined directly in `OutSmart.DAXon.csproj`; no `Directory.Build.props`, `Directory.Build.targets`, `global.json`, or `nuget.config` was found in the submodule search.
- The project is SDK-style and must use plural `TargetFrameworks` for multi-targeting, ordered newest to oldest as `net10.0;net472`.
- `Microsoft.NETFramework.ReferenceAssemblies` 1.0.3 is required only for the `net472` target and must be conditioned to that target after multi-targeting.
- The three explicit `EmbeddedResource` entries under `data/` must remain unchanged.
- The .NET 10 SDK is installed and compatible.
- A previous single-target `net10.0` experiment restored successfully but did not build cleanly; it removed the framework reference package and therefore must be corrected before validation.

**Done when**: The project targets `net10.0;net472`, both target restores succeed, and project metadata and embedded resources remain valid.
