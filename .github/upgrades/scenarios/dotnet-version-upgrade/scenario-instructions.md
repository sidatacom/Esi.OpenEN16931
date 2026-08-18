# .NET Version Upgrade

## Preferences
- **Flow Mode**: Automatic
- **Target Framework**: net472;net10.0
- **Scope**: `origins/sashokfestival/DAXon/src/OutSmart.DAXon/OutSmart.DAXon.csproj`
- **Submodule reset**: Local changes were explicitly reset before assessment.

## Source Control
- **Source Branch**: main
- **Working Branch**: main
- **Commit Strategy**: Manual
- **Branch Sync**: Disabled

## User Preferences
### Technical Preferences
- The user explicitly authorized modifying the DAXon project under `origins/sashokfestival/` for this upgrade, despite the repository's general origins protection.
- Keep both `net472` and `net10.0` as target frameworks; use multi-targeting rather than replacing `net472`.
- Commit messages for the DAXon project should be written in English.

## Upgrade Options
**Source**: .github/upgrades/scenarios/dotnet-version-upgrade/upgrade-options.md

### Strategy
- Upgrade Strategy: All-at-Once

### Project Structure
- Project Approach: Multi-targeting

### Compatibility
- Unsupported API Handling: Fix Inline

## Strategy
**Selected**: All-at-Once
**Rationale**: The scope contains one SDK-style class library with no project dependencies or dependants, so there is no dependency graph to stage.

### Execution Constraints
- Keep the project multi-targeted as `net472;net10.0`; do not remove the existing target.
- Restore and build both target frameworks after project changes.
- Resolve API compatibility findings inline where shared code is affected; use minimal target-specific conditionals only where APIs genuinely differ.
- Preserve the private .NET Framework reference assemblies dependency for the `net472` target.
- Run the available tests after the build succeeds, then perform final validation.
