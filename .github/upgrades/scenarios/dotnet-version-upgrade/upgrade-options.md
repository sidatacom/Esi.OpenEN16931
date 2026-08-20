# Upgrade Options — OutSmart.DAXon

Assessment: 1 SDK-style class library on .NET Framework 4.7.2, with 1 binary-incompatible API, 414 source-incompatible API findings, and 167 behavioral-change findings.

## Strategy

### Upgrade Strategy
A single project has no dependency graph to stage, so the upgrade can be performed as one atomic operation.

| Value | Description |
|-------|-------------|
| **All-at-Once** (selected) | Upgrade the project simultaneously in one atomic pass, followed by a full build and test validation. |

## Project Structure

### Project Approach
The project must continue supporting existing .NET Framework consumers while adding .NET 10, so both target frameworks are retained through multi-targeting.

| Value | Description |
|-------|-------------|
| **Multi-targeting** (selected) | Build the existing `net472` target and the new `net10.0` target from the same project. |
| In-place | Replace the existing target framework with .NET 10 and remove .NET Framework support. |

## Compatibility

### Unsupported API Handling
The assessment identified 582 API findings, primarily involving BigInteger source compatibility and Uri behavioral changes. The single-project scope favors resolving changes directly rather than leaving stubs for later.

| Value | Description |
|-------|-------------|
| **Fix Inline** (selected) | Resolve simple and complex API changes as part of the upgrade task so no deferred stubs remain. |
| Defer Complex Changes | Apply simple replacements immediately and defer complex changes to follow-up resolution tasks using temporary compilable stubs. |
