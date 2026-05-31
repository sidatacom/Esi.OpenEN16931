# Esi.OpenEN16931

> **Open source foundation for European e-invoicing across EN 16931, XRechnung, Factur-X, ZUGFeRD, UBL, CII and PEPPOL.**

---

## What is this project?

**Esi.OpenEN16931** is an open-source foundation and reference platform for European electronic invoicing based on the EN 16931 standard and its related national and international profiles, including:

- **EN 16931** – European standard for the semantic data model of the core elements of an electronic invoice
- **XRechnung** – German national profile for public-sector e-invoicing (CIUS DE)
- **Factur-X / ZUGFeRD** – Franco-German hybrid PDF/A-3 e-invoice format
- **UBL** – Universal Business Language (OASIS)
- **CII** – Cross-Industry Invoice (UN/CEFACT)
- **PEPPOL BIS** – Pan-European Public Procurement Online Business Interoperability Specifications

This repository consolidates upstream reference implementations, validators, generators, and tooling for these standards into a single, coherent, multi-language open-source platform.

---

## Why does this project exist?

The European e-invoicing landscape is fragmented: validators, generators, and compliance libraries are scattered across dozens of separate repositories in multiple programming languages, with no shared coordination layer.

**Esi.OpenEN16931** exists to:

1. **Unify the fragmented landscape** – provide a single entry point for all major EN 16931-related open-source projects
2. **Provide a unified compliance foundation** – aggregate reference implementations that can be used as the basis for multi-language validation and generation libraries
3. **Enable cross-standard interoperability** – make it easy to understand how XRechnung, Factur-X, ZUGFeRD, UBL, and CII relate to each other and to the core EN 16931 semantic data model
4. **Lower the barrier to entry** – give developers, integrators, and ERP vendors a clear starting point instead of rediscovering the same upstream sources independently

---

## What problem does it solve?

There is currently no unified, native, multi-language platform for:
- **Validating** electronic invoices against EN 16931 and national profiles
- **Generating** compliant invoices in UBL, CII, XRechnung, or Factur-X/ZUGFeRD formats
- **Understanding** the relationships between standards and their national variants

Existing solutions are either language-specific, tied to a single standard, or maintained in isolation. This project aggregates the best available open-source implementations and provides a foundation for building comprehensive compliance tooling.

---

## Repository structure

```
Esi.OpenEN16931/
├── origins/                  # Git submodules: upstream reference projects
│   ├── validator/
│   ├── validator-configuration-xrechnung/
│   ├── eInvoicing-EN16931/
│   ├── e-invoice-eu/
│   ├── factur-x/
│   ├── eu_einvoice/
│   ├── xrechnungs-generator/
│   ├── en16931-visualization/
│   ├── e-invoice/
│   ├── mustangproject/
│   ├── facturx/
│   ├── zugferd/
│   ├── e-invoicing/
│   ├── einvoicing/
│   ├── e-invoice-validator/
│   └── e-invoice-validator-backoffice/
├── README.md
├── CONTRIBUTING.md
├── NOTICE.md
└── LICENSE
```

---

## The `origins` folder

The `origins/` directory contains **Git submodules** pointing to upstream open-source projects that are relevant to EN 16931 and European e-invoicing. These are **not** part of this project's own source code; they are reference implementations and tooling from the broader ecosystem.

Each submodule tracks the upstream repository independently. The purpose of including them as submodules is to:
- Provide a single place to find and audit upstream dependencies
- Enable reproducible builds and compliance checks against specific upstream versions
- Document the open-source ecosystem surrounding EN 16931

> ⚠️ **Important:** Each project in `origins/` has its **own separate license**. The Apache License 2.0 that governs this repository does **not** apply to the submodule contents. See [NOTICE.md](NOTICE.md) for details.

To clone the repository including all submodules:

```bash
git clone --recurse-submodules https://github.com/sidatacom/Esi.OpenEN16931.git
```

To initialize submodules after a regular clone:

```bash
git submodule update --init --recursive
```

---

## Included open-source sources

| Submodule | Organization | Description |
|---|---|---|
| [validator](https://github.com/itplr-kosit/validator) | KoSIT | Official German KoSIT XML validator framework for XRechnung and other standards |
| [validator-configuration-xrechnung](https://github.com/itplr-kosit/validator-configuration-xrechnung) | KoSIT | KoSIT validator configuration and Schematron rules for XRechnung |
| [eInvoicing-EN16931](https://github.com/ConnectingEurope/eInvoicing-EN16931) | Connecting Europe Facility | EU CEF reference implementation and Schematron rules for EN 16931 |
| [e-invoice-eu](https://github.com/gflohr/e-invoice-eu) | Guido Flohr | Multi-format European e-invoice generation library (Node.js/TypeScript) |
| [factur-x](https://github.com/akretion/factur-x) | Akretion | Python library for Factur-X / ZUGFeRD hybrid PDF invoice creation and parsing |
| [eu_einvoice](https://github.com/alyf-de/eu_einvoice) | Alyf GmbH | ERPNext/Frappe app for EU e-invoicing compliance |
| [xrechnungs-generator](https://github.com/xSentry/xrechnungs-generator) | xSentry | XRechnung invoice generator |
| [en16931-visualization](https://github.com/phax/en16931-visualization) | Philip Helger | Visualization tool for the EN 16931 data model |
| [e-invoice](https://github.com/klst-de/e-invoice) | klst-de | Java library for EN 16931 e-invoice creation and validation |
| [mustangproject](https://github.com/ZUGFeRD/mustangproject) | ZUGFeRD/Mustang | Leading Java library for ZUGFeRD and Factur-X invoice generation and parsing |
| [facturx](https://github.com/Securibox/facturx) | Securibox | Go library for Factur-X invoice creation and parsing |
| [zugferd](https://github.com/horstoeko/zugferd) | horstoeko | PHP library for ZUGFeRD and Factur-X invoice handling |
| [e-invoicing](https://github.com/easybill/e-invoicing) | easybill | PHP library for generating XRechnung and ZUGFeRD invoices |
| [einvoicing](https://github.com/josemmo/einvoicing) | josemmo | PHP library for creating and parsing EN 16931-compliant invoices |
| [e-invoice-validator](https://github.com/easybill/e-invoice-validator) | easybill | PHP e-invoice validator using KoSIT validator under the hood |
| [e-invoice-validator-backoffice](https://github.com/backoffice-plus/e-invoice-validator) | backoffice-plus | JavaScript/Node.js e-invoice validator for XRechnung and EN 16931 |

### Extended ecosystem catalog (100 projects)

For the full extended 100-project ecosystem index, see:

- [`/docs/github-open-source-e-invoicing-catalog.md`](docs/github-open-source-e-invoicing-catalog.md)

---

## License

This project is licensed under the **Apache License 2.0**. See [LICENSE](LICENSE) for the full license text.

The upstream projects referenced via submodules in `origins/` retain their own separate licenses. See [NOTICE.md](NOTICE.md) for details.

---

## Contributing

Contributions are welcome! Please read [CONTRIBUTING.md](CONTRIBUTING.md) for guidelines on how to contribute to this project.

---

## Contact

This project is maintained by [sidatacom](https://github.com/sidatacom).
