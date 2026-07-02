# General Copilot Instructions

## 1. Fundamental Principles
- **Purpose:** Provide clear, actionable rules for AI-assisted contributions and human reviewers to ensure code quality, security, and maintainability.
- **Scope:** Applies to contributors, reviewers, CI jobs, and automated agents that generate or modify repository code.
- **Language:** All comments and generated documentation must be in English.
- **Research:** Copilot must actively search online for the most recent and relevant information. Every implementation must be based on the latest best practices, using official documentation and reputable sources.
- **No Outdated Data:** Never suggest or use deprecated or obsolete methods, APIs, or libraries.

## 2. Output Format (Mandatory) 
- **No Full Blocks:** Do **not** output complete files or “full code blocks” of files.
- **No Markdown:** Do **not** wrap output in Markdown code fences.
- **Edge Case:** If no changes are needed, output exactly: `No changes required.`

## 3. C# & General Code Quality
- **Nullability:**
  - Use C# nullable reference types.
  - Always add explicit guard clauses and null checking for all nullable fields, properties, parameters, and return values.
- **Type Usage:** Always use the global alias `EsiKey` instead of `Guid?` or `Nullable<Guid>` for unique identifiers.
- **Documentation:** Public classes, structs, interfaces, enums, and public members require concise XML doc comments describing intent, parameters, return values, and exceptions.
- **Error Handling:**
  - Implement robust error handling and meaningful messages.
  - Do not swallow exceptions. Throw specific exception types.
  - Log unexpected states without exposing secrets.
- **Security:** Code must be secure by design. Validate external inputs, never commit secrets, and follow the least-privilege principle.
- **Testing:** Ensure all code is syntactically correct and include unit tests for new features or behavior changes.

## 3.5 Documentation Synchronization
- All current structural documentation lives under `docs/Development`.
- When code, widgets, workflows, or test-relevant structures change, update the matching documentation in `docs/Development` alongside the code and tests.
- Keep the documentation in sync with the current implementation and do not leave structural docs stale after behavior changes.
- When adding or renaming icon settings, icon editor entries, or icon-setting cards, also update the matching localization resources in `src/Clients/Esi.Web/Esi.Web/App_Data/Localization/Esi.Settings.en.xml` and `src/Clients/Esi.Web/Esi.Web/App_Data/Localization/Esi.Settings.de.xml` in the same change, and increment the `<Language ... Version>` value in each touched localization file.

## 4. AI Usage & Verification
- **Context:** Provide relevant files, interfaces, and constraints when requesting AI assistance.
- **Verification:** Always verify non-trivial AI suggestions against authoritative sources before merging.
- **Source References:** For complex or critical solutions, include links to official documentation **inside the diff** (e.g., as a short `// References:` comment near the relevant code or in test comments). Do not add references outside the diff.
- **Review:** Prefer small, reviewable AI-generated changes. Always include a human reviewer for architectural or security-impacting changes.

## 5. CI & Enforcement
- **Automated Checks:** Enforce analyzers, formatting, and tests in CI.
- **Failure Conditions:** Fail CI for missing XML docs on public API, failing tests, or analyzer violations.
- **PR Checklist Requirement:** Build success, tests, XML docs, null-checks for nullable inputs, no secrets, `EsiKey` usage, and AI verification evidence (captured via code comments/links where applicable).

Copilot Instructions
Guideline (critical): NEVER modify or commit files under origins/.

## 6. Short Explanation
The origins/ directory contains cloned/prepared copies of external repositories and reference artifacts. Modifying them would alter local mirrors of the original projects and is strictly prohibited.
Behavior for Humans and Automated Agents
Reading/Scanning: Allowed. Tools may analyze origins/ or extract evidence from it.
Writing/Committing: Prohibited. Never fork, modify files under origins/, create new branches in these paths, or open pull requests against repositories under origins/.
If a change to an origin is necessary: apply the change upstream in the source repository first, or obtain explicit approval from the maintainer/owner before working within origins/.
Automated Scripts, Scanners and analysis scripts are allowed to read origins/, but they must exclude write access or require explicit confirmation.
