# Germany (de) — Country Profile

This folder collects Germany-specific references for electronic invoicing, including XRechnung (CIUS), Factur‑X/ZUGFeRD, and mappings to EN 16931.

Contents
- `ISO_Norms.md` — relevant ISO/CEN standards
- `Validation_Rules.md` — national validation and business rules (KoSIT)
- `Origins_References.md` — pointers to upstream `origins/` projects
- `Examples.md` — sample invoices and test data
- `Tools_Implementations.md` — validators, generators, and helpful tooling

Guidance
- Use `origins/itplr-kosit/validator-configuration-xrechnung` as the authoritative source for XRechnung Schematron/XSD artifacts.
- Prefer runtime ZIP loading for KoSIT releases until licensing permits bundling.
