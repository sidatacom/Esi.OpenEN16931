# .NET Version Upgrade Plan

## Overview

**Target**: Extend `OutSmart.DAXon.csproj` from .NET Framework 4.7.2 to support both .NET Framework 4.7.2 and .NET 10 (`net472;net10.0`) through multi-targeting.
**Scope**: One large SDK-style class library with 1,530 code files, approximately 298,011 lines of code, no project references, and 582+ estimated lines affected by compatibility changes.

## Tasks

### 01-daxon-framework-upgrade: Upgrade the DAXon project and compatibility-sensitive code

Verify the .NET 10 SDK/toolchain, then configure the SDK-style DAXon project to multi-target `net472` and `net10.0` without dropping existing .NET Framework support. The assessment reports no third-party runtime package inventory but identifies one binary-incompatible API, 414 source-incompatible API findings, and 167 behavioral-change findings. Most findings are concentrated in `System.Numerics.BigInteger` source compatibility and `System.Uri` behavior; investigate the affected files and resolve all required changes inline under the confirmed Fix Inline policy.

Research should begin with the project file, its build properties, target-specific package references, generated or linked source inputs, and every incident reported in the assessment. Preserve the .NET Framework reference-assemblies package for `net472`, keep embedded resources intact, and use minimal target-specific conditionals only where the API surface genuinely differs. Preserve existing behavior where the .NET 10 API semantics differ, adding focused tests when behavior changes are observable.

**Done when**: The project targets both `net472` and `net10.0`, both targets restore successfully, all identified compatibility changes are resolved without temporary stubs or new warning suppressions, and both target builds have zero errors and zero warnings.

---

### 02-final-validation: Validate the upgraded project and tests

Validate the completed multi-target .NET upgrade independently of the code-editing task. Build both target frameworks and any directly associated test projects, execute the available automated tests, and review the final dependency and target-framework state. Document any environment-limited validation explicitly rather than treating it as successful.

**Done when**: Both target builds and applicable tests pass with zero build errors, zero warnings, no unresolved dependency conflicts, and the final validation results are recorded.
