# 01.02-daxon-api-compatibility: Resolve DAXon API compatibility changes

## Objective
Resolve the API compatibility findings exposed after the project is retargeted to net10.0, following the confirmed Fix Inline policy.

## Scope
- Source files reported for `System.Numerics.BigInteger` source incompatibilities
- Source files reported for `System.Uri` behavioral changes
- Remaining binary/source findings including DirectoryInfo.FullName, TimeSpan.FromMinutes, and WebRequest.Create

## Research already completed
The assessment reports 1 binary-incompatible, 414 source-incompatible, and 167 behavioral-change findings. The dominant source family is BigInteger (412 findings); Uri is the dominant behavioral family. No temporary `// STUB:` markers exist.

**Done when**: All required compatibility changes compile on net10.0, behavior-sensitive changes are preserved or covered by focused tests where applicable, no stubs or new warning suppressions are introduced, and the project builds warning-free.

