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

Esi.OpenEN16931 is a community-driven collection of implementations, validators, generators, and tools for European e-invoicing standards (EN 16931, XRechnung, Factur‑X / ZUGFeRD, UBL, CII, PEPPOL). Its goal is to provide a central, open reference platform that:

- provides developers, integrators, and public authorities with ready reference implementations, validation tools, and test data;
- promotes interoperability through shared mappings, rules, and examples;
- makes upstream projects discoverable and consolidates their usage via git submodules under `origins/`.

The repository tracks upstream projects as git submodules, ships documentation and helper scripts (for example, to add submodules), and simplifies finding, comparing, and reusing existing solutions.

## Submodule catalog - 62 projects

The country mapping below is the primary catalog view. It makes the relationship explicit for every country. Shared repositories are repeated on each applicable country row; they are not separate national implementations. `No country-specific submodule` means that the syntax or profile is documented, but this catalog does not currently contain a dedicated implementation for it.

| Country | Syntax/profile | Schemas, rules and test data | Codebases, validators and tools |
|---------|----------------|------------------------------|----------------------------------|
| Austria | `ebInterface`; UBL/Peppol | `phax/phive-rules` | `phax/ph-ubl` |
|  |  | `phax/phive-rules-foundations` | `itplr-kosit/validator` |
|  |  | `austriapro/ebinterface-ubl-mapping` | — |
|  |  | `austriapro/ebinterface-xrechnung-mapping` | — |
| Belgium | UBL 2.1; Peppol BIS / UBL.BE | `ConnectingEurope/eInvoicing-EN16931` | `itplr-kosit/validator` |
|  |  | `itplr-kosit/validator-configuration-bis` | `phax/phase4` |
|  |  | `phax/phive-rules` (module `phive-rules-ublbe`) | `phax/ph-ubl` |
| Bulgaria | UBL 2.1 and CII 16B/D16B | `ConnectingEurope/eInvoicing-EN16931` | `itplr-kosit/validator` |
|  |  | `itplr-kosit/validator-configuration-bis` | `phax/phase4` |
|  |  | `phax/phive-rules` | `phax/ph-ubl` |
|  |  | — | `phax/en16931-cii2ubl` |
| Croatia | UBL 2.1 and CII 16B/D16B; Croatian CIUS | `ConnectingEurope/eInvoicing-EN16931` | `itplr-kosit/validator` |
|  |  | `itplr-kosit/validator-configuration-bis` | `phax/phase4` |
|  |  | `phax/phive-rules` | `phax/ph-ubl` |
|  |  | — | `phax/en16931-cii2ubl` |
| Cyprus | UBL 2.1; Peppol BIS | `ConnectingEurope/eInvoicing-EN16931` | `itplr-kosit/validator` |
|  |  | `itplr-kosit/validator-configuration-bis` | `phax/phase4` |
|  |  | `phax/phive-rules` | `phax/ph-ubl` |
| Czechia | ISDOC; UBL 2.1; EDIFACT | `phax/phive-rules-foundations` | `deltazero-cz/node-isdoc` |
|  |  | — | `isdoc/isdoc.pdf` |
|  |  | — | `adawolfa/isdoc` |
|  |  | — | `itplr-kosit/validator` |
|  |  | — | `phax/ph-ubl` |
| Denmark | OIOUBL; UBL 2.1; Peppol BIS | `ibistic/oioubl-schematron` | `itplr-kosit/validator` |
|  |  | `ConnectingEurope/eInvoicing-EN16931` | `phax/phase4` |
|  |  | `phax/phive-rules` | `phax/ph-ubl` |
| Estonia | UBL 2.1; national XML option | `ConnectingEurope/eInvoicing-EN16931` | `thorgate/estonian_e_invoice` |
|  |  | `phax/phive-rules` | `internetee/e_invoice` |
|  |  | — | `itplr-kosit/validator` |
| Finland | Finvoice; TEAPPSXML; UBL 2.1 and CII 16B/D16B | `phax/phive-rules-foundations` | `codemasteroy/py-finvoice` |
|  |  | `ConnectingEurope/eInvoicing-EN16931` | `samiljin/finvoice` |
|  |  | `phax/phive-rules` | `phax/en16931-cii2ubl` |
|  |  | — | `itplr-kosit/validator` |
| France | French CIUS over UBL/CII; Factur-X | `atgp/factur-x` | `mahdiabderraouf/facturx-php` |
|  |  | `Tiime-Software/Factur-X` | `facturx` |
|  |  | `ConnectingEurope/eInvoicing-EN16931` | `factur-x` |
|  |  | `phax/phive-rules` | `phax/en16931-cii2ubl` |
| Germany | XRechnung over UBL/CII; Factur-X/ZUGFeRD | `itplr-kosit/validator-configuration-xrechnung` | `itplr-kosit/validator` |
|  |  | `itplr-kosit/xrechnung-testsuite` | `stephanstapel/ZUGFeRD-csharp` |
|  |  | `itplr-kosit/xrechnung-schematron` | `horstoeko/zugferd` |
|  |  | `LandrixSoftware/validator-configuration-zugferd` | `easybill/e-invoicing` |
|  |  | `ConnectingEurope/eInvoicing-EN16931` | `itplr-kosit/xrechnung-visualization` |
|  |  | — | `easybill/zugferd-php` |
|  |  | — | `LandrixSoftware/XRechnung-for-Delphi` |
|  |  | — | `zfutura/pycheval` |
|  |  | — | `digineo/xrechnung` |
|  |  | — | `horstoeko/zugferdublbridge` |
|  |  | — | `kyr0/easy-erechnung` |
|  |  | — | `koozala/pacioli` |
|  |  | — | `horstoeko/invoicesuite` |
|  |  | — | `easybill/en16931-validator` |
|  |  | — | `NikolaiMe/factur-x-kit` |
|  |  | — | `LandrixSoftware/ZUGFeRD-for-Delphi` |
|  |  | — | `mustangproject` |
|  |  | — | `gflohr/e-invoice-eu` |
|  |  | — | `pretix/python-drafthorse` |
|  |  | — | `ZUGFeRD/REST-Converter` |
|  |  | — | `eu_einvoice` |
| Greece | UBL 2.1; Peppol BIS | `ConnectingEurope/eInvoicing-EN16931` | `itplr-kosit/validator` |
|  |  | `itplr-kosit/validator-configuration-bis` | `phax/phase4` |
|  |  | `phax/phive-rules` | `phax/ph-ubl` |
| Hungary | UBL 2.1; Peppol/exchange options; NAV XML reporting | `ConnectingEurope/eInvoicing-EN16931` | `itplr-kosit/validator` |
|  |  | `itplr-kosit/validator-configuration-bis` | `phax/phase4` |
|  |  | `phax/phive-rules` | `phax/ph-ubl` |
| Ireland | UBL 2.1 and CII 16B/D16B; Peppol BIS | `ConnectingEurope/eInvoicing-EN16931` | `itplr-kosit/validator` |
|  |  | `itplr-kosit/validator-configuration-bis` | `phax/phase4` |
|  |  | `phax/phive-rules` | `phax/ph-ubl` |
|  |  | — | `phax/en16931-cii2ubl` |
| Italy | FatturaPA | `phax/phive-rules-foundations` | `italia/fatturapa-testsdi` |
|  |  | — | `italia/fatturapa-php-sdk` |
|  |  | — | `italia/fatturapa-python` |
| Latvia | UBL 2.1; EN 16931/Peppol | `ConnectingEurope/eInvoicing-EN16931` | `itplr-kosit/validator` |
|  |  | `itplr-kosit/validator-configuration-bis` | `phax/phase4` |
|  |  | `phax/phive-rules` | `phax/ph-ubl` |
| Lithuania | UBL 2.1; EN 16931/Peppol; SABIS | `ConnectingEurope/eInvoicing-EN16931` | `itplr-kosit/validator` |
|  |  | `itplr-kosit/validator-configuration-bis` | `phax/phase4` |
|  |  | `phax/phive-rules` | `phax/ph-ubl` |
| Luxembourg | UBL 2.1 and CII 16B/D16B; Peppol BIS | `ConnectingEurope/eInvoicing-EN16931` | `itplr-kosit/validator` |
|  |  | `itplr-kosit/validator-configuration-bis` | `phax/phase4` |
|  |  | `phax/phive-rules` | `phax/ph-ubl` |
|  |  | — | `phax/en16931-cii2ubl` |
| Malta | UBL 2.1; Peppol BIS | `ConnectingEurope/eInvoicing-EN16931` | `itplr-kosit/validator` |
|  |  | `itplr-kosit/validator-configuration-bis` | `phax/phase4` |
|  |  | `phax/phive-rules` | `phax/ph-ubl` |
| Netherlands | UBL 2.1; NLCIUS, UBL-OHNL and SI-UBL | `ibistic/oioubl-schematron` | `itplr-kosit/validator` |
|  |  | `ConnectingEurope/eInvoicing-EN16931` | `phax/phase4` |
|  |  | `phax/phive-rules` | `phax/ph-ubl` |
| Poland | UBL 2.1; Peppol Polish extensions; KSeF/PEF | `ConnectingEurope/eInvoicing-EN16931` | `itplr-kosit/validator` |
|  |  | `itplr-kosit/validator-configuration-bis` | `phax/phase4` |
|  |  | `phax/phive-rules` | `phax/ph-ubl` |
| Portugal | UBL 2.1 and CII 16B/D16B; CIUS-PT | `ConnectingEurope/eInvoicing-EN16931` | `itplr-kosit/validator` |
|  |  | `itplr-kosit/validator-configuration-bis` | `phax/phase4` |
|  |  | `phax/phive-rules` | `phax/ph-ubl` |
|  |  | — | `phax/en16931-cii2ubl` |
| Romania | UBL 2.1 and CII 16B/D16B; RO_CIUS | `ConnectingEurope/eInvoicing-EN16931` | `itplr-kosit/validator` |
|  |  | `itplr-kosit/validator-configuration-bis` | `phax/phase4` |
|  |  | `phax/phive-rules` | `phax/ph-ubl` |
|  |  | — | `phax/en16931-cii2ubl` |
| Slovakia | UBL 2.1 and CII 16B/D16B; Peppol | `ConnectingEurope/eInvoicing-EN16931` | `itplr-kosit/validator` |
|  |  | `itplr-kosit/validator-configuration-bis` | `phax/phase4` |
|  |  | `phax/phive-rules` | `phax/ph-ubl` |
|  |  | — | `phax/en16931-cii2ubl` |
| Slovenia | e-SLOG; UBL 2.1; EN 16931/Peppol | `phax/phive-rules` | `MPrtenjak/MNetESlog` |
|  |  | `ConnectingEurope/eInvoicing-EN16931` | `itplr-kosit/validator` |
|  |  | — | `phax/phase4` |
|  |  | — | `phax/ph-ubl` |
| Spain | Facturae | `phax/phive-rules-foundations` | `itplr-kosit/validator` |
|  |  | — | `phax/ph-ubl` |
| Sweden | UBL 2.1; Peppol BIS; SFTI guidance | `ConnectingEurope/eInvoicing-EN16931` | `itplr-kosit/validator` |
|  |  | `itplr-kosit/validator-configuration-bis` | `phax/phase4` |
|  |  | `phax/phive-rules` | `phax/ph-ubl` |
| International / multi-country | Cross-border Factur-X, EN 16931, UBL and CII tooling | `ConnectingEurope/eInvoicing-EN16931` | `sashokfestival/DAXon` |
|  |  | `phax/phive-rules` | `num-num/ubl-invoice` |
|  |  | `phax/phive-rules-foundations` | `phax/phase2` |
|  |  | — | `phax/phive` |
|  |  | — | `phax/peppol-commons` |
|  |  | — | `stafyniaksacha/facturx` |
|  |  | — | `speedata/einvoice` |
|  |  | — | `InvoiceXML/facturx-api-examples` |
|  |  | — | `Youniwemi/digital-invoice` |
|  |  | — | `facturx-engine/facturx-engine` |
|  |  | — | `en16931-visualization` |
|  |  | — | `phax/en16931-cii2ubl` |

The repository catalog above contains each configured submodule exactly once.

## Validation

The validation model separates the EN 16931 semantic model, the invoice syntax, profile-specific rules, containers and transport. These layers must not be treated as interchangeable invoice types.

| Layer | Meaning | Examples |
|-------|---------|----------|
| Semantic model | Business terms, groups, cardinalities and core rules | EN 16931, `BT-*` |
| Syntax | XML or EDI representation of the invoice | UBL 2.1, UN/CEFACT CII 16B/D16B |
| Profile and rules | Constraints applied to the semantic model and syntax | XRechnung, Peppol BIS Billing 3.0, CIUS |
| Container | Packaging of readable and structured invoice data | PDF/A-3 with embedded CII, Factur-X/ZUGFeRD |
| Transport and exchange | Delivery, reporting or clearance channel | Peppol eDelivery, SDI, KSeF, FACe, national portals |

A Peppol BIS invoice is an EN 16931 invoice serialized as UBL and validated with Peppol rules. Factur-X and ZUGFeRD are PDF/A-3 containers with embedded CII; they are not additional XML syntaxes. `UBL` and `CII` therefore identify the syntax, while the selected profile determines the applicable rules.

### Syntax families and country qualification

| Syntax family | Countries or documented use | Project API status |
|---------------|----------------------------|--------------------|
| **UBL 2.1** | Austria, Belgium, Bulgaria, Croatia, Cyprus, Czechia, Denmark, Estonia, Finland, France, Germany, Greece, Hungary, Ireland, Latvia, Lithuania, Luxembourg, Malta, Netherlands, Poland, Portugal, Romania, Slovakia, Slovenia and Sweden | Exposed as `UBL` |
| **UN/CEFACT CII 16B/D16B** | Bulgaria, Croatia, Finland, France, Germany, Ireland, Luxembourg, Portugal, Romania and Slovakia | Exposed as `CII` |
| **ebInterface** | Austria | Not exposed as a separate syntax |
| **Finvoice 3.0 / TEAPPSXML 3.0** | Finland | Not exposed as separate syntaxes |
| **ISDOC** | Czechia | Not exposed as a separate syntax |
| **EDIFACT** | Czechia and legacy/interoperability scenarios | Not exposed as a separate syntax |
| **FatturaPA** | Italy | Not exposed as a separate syntax |
| **Facturae** | Spain | Not exposed as a separate syntax |
| **e-SLOG 2.0** | Slovenia | Not exposed as a separate syntax |
| **Estonian national XML** | Estonia; the cited inventory does not name one single vocabulary | Not exposed as a separate syntax |

The following are profiles or rule layers, not additional syntax families: Peppol BIS Billing 3.0, OIOUBL, XRechnung, French CIUS, Croatian CIUS, NLCIUS, UBL-OHNL, SI-UBL, CIUS-PT, RO_CIUS and Polish Peppol extensions. `NAV XML` and `KSeF XML` are reporting or platform schemas. Factur-X/ZUGFeRD is a PDF/A-3 container with embedded CII.

### Validation evidence and scope

The detailed validation evidence, country-specific qualification and validation pipeline are maintained in [VALIDATION.md](VALIDATION.md).



## License

This project is licensed under the **Apache License 2.0**. See [LICENSE](LICENSE) for the full license text.

The upstream projects referenced via submodules in `origins/` retain their own separate licenses. See [NOTICE.md](NOTICE.md) for details.

---

## Contributing

Contributions are welcome! Please read [CONTRIBUTING.md](CONTRIBUTING.md) for guidelines on how to contribute to this project.

---

## Contact

This project is maintained by [sidatacom](https://github.com/sidatacom).
