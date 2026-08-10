# Esi.OpenEN16931 Validation Summary


The validation logic in this project follows the standard e-invoicing validation workflow: XML Schema (XSD) validation followed by Schematron rule (XSLT) application.

## Conceptual Layers

The terms used in this document belong to different layers. They must not be read as interchangeable invoice types:

| Layer | Meaning / main question | Examples | Does not define |
|-------|-------------------------|----------|-----------------|
| **Semantic model** | Which business terms and core rules are represented? | EN 16931 | XML elements, PDF packaging, or transport |
| **Syntax** | How is the invoice data serialized? | UBL 2.1, UN/CEFACT CII | The selected profile or delivery channel |
| **Profile and rules** | Which constraints and business rules apply to the semantic model and syntax? | XRechnung CIUS, PEPPOL BIS Billing 3.0 | Whether the invoice is wrapped in PDF/A-3 |
| **Container** | How are a readable representation and structured data packaged together? | PDF/A-3 with embedded CII XML, as used by Factur-X/ZUGFeRD | A new semantic model or UBL content |
| **Transport and exchange** | How does the invoice reach the recipient? | Peppol eDelivery, Mercurius, NemHandel, KSeF, FACe, national portals, email or other agreed channels | The invoice syntax or business rules |

For example, a PEPPOL BIS invoice is an EN 16931 invoice serialized in UBL and validated with PEPPOL rules. A Factur-X or ZUGFeRD document is a PDF/A-3 container with embedded CII XML; it is not a UBL document. UBL and CII therefore do not require separate country columns as if they were competing profiles. They are syntaxes selected by a profile or implementation.

## Options and Variations by Layer

### Semantic Model

- **EN 16931 core model:** Defines the business terms (`BT-*`), business groups, cardinalities, and core business rules shared by conforming invoice profiles.
- **Core versus constrained model:** A profile such as XRechnung or PEPPOL BIS constrains the EN 16931 model by making selected terms more specific or mandatory. It does not create a new XML syntax.
- **Current project scope:** The public model does not expose an EN 16931 edition selector. The concrete edition is determined by the schema and rule package selected for validation.

### Syntax

The following table consolidates the publicly documented invoice syntaxes referenced for the 27 EU member states. Each syntax family is listed once, with the countries in which the cited inventory identifies it. The project currently exposes only `UBL` and `CII` through `Esi.OpenEN16931.Models.Syntax`; the project-status column must not be read as complete implementation support.

| Invoice syntax family | XML or EDI representation | Countries / documented use | Classification | Project API status |
|-----------------------|--------------------------|---------------------------|----------------|--------------------|
| **UBL 2.1** | UBL 2.1 `Invoice` or `CreditNote` XML | Austria, Belgium, Bulgaria, Croatia, Cyprus, Czechia, Denmark, Estonia, Finland, France, Germany, Greece, Hungary, Ireland, Latvia, Lithuania, Luxembourg, Malta, Netherlands, Poland, Portugal, Romania, Slovakia, Slovenia, Sweden | XML syntax; used by national and EN 16931 profiles | Exposed as `UBL` |
| **UN/CEFACT CII 16B/D16B** | Cross Industry Invoice XML | Bulgaria, Croatia, Finland, France, Germany, Ireland, Luxembourg, Portugal, Romania, Slovakia | XML syntax; used by EN 16931 profiles | Exposed as `CII` |
| **ebInterface** | Austrian ebInterface XML | Austria | National XML invoice syntax | Not exposed as a separate syntax |
| **Finvoice 3.0** | Finvoice XML | Finland | National XML invoice syntax | Not exposed as a separate syntax |
| **TEAPPSXML 3.0** | TEAPPSXML XML | Finland | National XML invoice syntax | Not exposed as a separate syntax |
| **ISDOC** | ISDOC XML | Czechia | National XML invoice syntax | Not exposed as a separate syntax |
| **EDIFACT** | UN/EDIFACT message syntax | Czechia and legacy/interoperability scenarios | EDI syntax; not an XML syntax | Not exposed as a separate syntax |
| **FatturaPA** | FatturaPA XML | Italy | National XML invoice syntax; SDI is the clearance platform | Not exposed as a separate syntax |
| **Facturae** | Facturae XML | Spain | National XML invoice syntax; FACe is a gateway | Not exposed as a separate syntax |
| **e-SLOG 2.0** | e-SLOG XML | Slovenia | National XML invoice syntax with EN 16931/Peppol interoperability | Not exposed as a separate syntax |
| **Estonian national XML** | National XML representation; the cited factsheet does not name one single vocabulary | Estonia | National syntax option alongside EN 16931/Peppol | Not exposed as a separate syntax |

The following are profiles or rules applied to a syntax rather than additional syntaxes: Peppol BIS Billing 3.0, OIOUBL, XRechnung, French CIUS, Croatian CIUS, NLCIUS, UBL-OHNL, SI-UBL, CIUS-PT, RO_CIUS and Polish Peppol extensions. Platforms and exchange/reporting systems such as Peppol eDelivery, SDI, KSeF, PEF, FACe, Mercurius, SABIS, NAV Online Invoice and RO e-Factura are also not syntax families. `NAV XML` and `KSeF XML` belong to those reporting/platform schemas, not to this invoice-syntax table.

Factur-X/ZUGFeRD is deliberately excluded from this syntax table: it is one PDF/A-3 hybrid container family with embedded CII XML. The CII is the invoice syntax; PDF/A-3 is the container. Its details are documented in [Containers and Hybrid Formats](#containers-and-hybrid-formats).

The [detailed national inventory](#public-national-profile-and-syntax-inventory) records the source and country-specific qualification for each syntax, profile and platform entry.

UBL and CII can represent the EN 16931 business model. XSD validation checks the selected XML vocabulary; profile-specific Schematron rules are applied separately. Choosing UBL or CII alone therefore does not select the business rules.

### Profiles and Rules

The project declares the following conformance values and validation-routing targets:

This is not a complete catalogue of all national profiles used across the 27 EU member states. It covers the rule packages and configuration names currently represented by this project. The public inventory below separates documented national formats and profiles from this project's validator support. National formats and profiles such as `ebInterface`, `OIOUBL`, `Finvoice`, `ISDOC`, `FatturaPA`, `Facturae`, `KSeF`, `PEF`, `RO_CIUS`, `CIUS-PT` and `e-SLOG` are not automatically supported or validated by the configurations listed below.

### Public National Profile and Syntax Inventory

The European Commission country factsheets are the primary public source for this inventory. `No separate national CIUS identified` is deliberately conservative: it does not mean that a country has no local implementation, only that the cited factsheet does not identify one as a distinct EN 16931 profile. Platforms and tax-reporting systems are named as such and are not classified as invoice syntaxes. The project-status column describes the configurations documented in this repository, not the capabilities of the national systems.

| Country | Publicly identified profile or syntax | Layer / type | EN 16931 relationship | Public source | Project status |
|---------|--------------------------------------|--------------|-----------------------|---------------|----------------|
| Austria | ebInterface; UBL/Peppol options | National XML syntax and exchange profiles | Separate national syntax; mapping or profile compatibility must be checked | [Commission factsheet](https://ec.europa.eu/digital-building-blocks/sites/spaces/DIGITAL/pages/467108876/eInvoicing+in+Austria) | No national configuration listed |
| Belgium | Peppol BIS Billing 3.0 over UBL | Cross-border profile and syntax | EN 16931 profile; no separate national CIUS identified | [Commission factsheet](https://ec.europa.eu/digital-building-blocks/sites/spaces/DIGITAL/pages/467108877/eInvoicing+in+Belgium) | No national configuration listed |
| Bulgaria | UBL 2.1 and CII exchange options | XML syntaxes; CAIS EPP is a platform | EN 16931-based exchange options; platform is not a profile | [Commission factsheet](https://ec.europa.eu/digital-building-blocks/sites/spaces/DIGITAL/pages/467108878/eInvoicing+in+Bulgaria) | No national configuration listed |
| Croatia | Croatian national CIUS/profile over UBL/CII | National profile and XML syntaxes | EN 16931 CIUS/profile | [Commission factsheet](https://ec.europa.eu/digital-building-blocks/sites/spaces/DIGITAL/pages/467108879/eInvoicing+in+Croatia) | No national configuration listed |
| Cyprus | Peppol BIS Billing 3.0 over UBL | Cross-border profile and syntax | EN 16931 profile; no separate national CIUS identified | [Commission factsheet](https://ec.europa.eu/digital-building-blocks/sites/spaces/DIGITAL/pages/467108880/eInvoicing+in+Cyprus) | No national configuration listed |
| Czechia | ISDOC; UBL 2.1 and EDIFACT options | National XML syntax and alternative syntaxes | ISDOC is separate from the EN 16931 syntax; UBL option is EN 16931-compatible where profiled | [Commission factsheet](https://ec.europa.eu/digital-building-blocks/sites/spaces/DIGITAL/pages/467108881/eInvoicing+in+Czech+Republic) | No national configuration listed |
| Denmark | OIOUBL; Peppol BIS | National UBL profile and exchange ecosystem | OIOUBL is a national UBL profile; NemHandel is transport infrastructure | [Commission factsheet](https://ec.europa.eu/digital-building-blocks/sites/spaces/DIGITAL/pages/467108882/eInvoicing+in+Denmark) | No national configuration listed |
| Estonia | EN 16931/Peppol; national XML remains permitted | Profile/ecosystem and national syntax option | EN 16931 is used for structured exchange; no separate national CIUS identified | [Commission factsheet](https://ec.europa.eu/digital-building-blocks/sites/spaces/DIGITAL/pages/467108883/eInvoicing+in+Estonia) | No national configuration listed |
| Finland | Finvoice 3.0; TEAPPSXML 3.0; UBL/CII/Peppol | National syntaxes and exchange profiles | National syntaxes coexist with EN 16931-compatible UBL/CII exchange | [Commission factsheet](https://ec.europa.eu/digital-building-blocks/sites/spaces/DIGITAL/pages/467108884/eInvoicing+in+Finland) | No national configuration listed |
| France | French CIUS over UBL/CII; Factur-X | National profile and hybrid container | EN 16931 CIUS; Factur-X embeds CII in PDF/A-3 | [Commission factsheet](https://ec.europa.eu/digital-building-blocks/sites/spaces/DIGITAL/pages/467108885/eInvoicing+in+France) | No national configuration listed |
| Germany | XRechnung over UBL/CII; Factur-X/ZUGFeRD | National CIUS and hybrid container | XRechnung is an EN 16931 CIUS; Factur-X/ZUGFeRD embeds CII in PDF/A-3 | [Commission factsheet](https://ec.europa.eu/digital-building-blocks/sites/spaces/DIGITAL/pages/467108886/eInvoicing+in+Germany) | XRechnung configurations listed |
| Greece | Peppol BIS Billing 3.0 over UBL; myDATA | Profile/syntax and tax-reporting system | Peppol is EN 16931-based; myDATA is reporting, not an invoice syntax | [Commission factsheet](https://ec.europa.eu/digital-building-blocks/sites/spaces/DIGITAL/pages/467108887/eInvoicing+in+Greece) | No national configuration listed |
| Hungary | Structured exchange/Peppol options; NAV XML | Exchange profile and tax-reporting syntax | NAV Online Invoice is reporting; no separate EN 16931 CIUS identified | [Commission factsheet](https://ec.europa.eu/digital-building-blocks/sites/spaces/DIGITAL/pages/467108888/eInvoicing+in+Hungary) | No national configuration listed |
| Ireland | Peppol BIS Billing 3.0 over UBL/CII | Cross-border profile and syntaxes | EN 16931 profile; no separate national CIUS identified | [Commission factsheet](https://ec.europa.eu/digital-building-blocks/sites/spaces/DIGITAL/pages/467108889/eInvoicing+in+Ireland) | No national configuration listed |
| Italy | FatturaPA | National XML syntax and clearance ecosystem | National syntax aligned with EN 16931 concepts; SDI is the transport/clearance platform | [Commission factsheet](https://ec.europa.eu/digital-building-blocks/sites/spaces/DIGITAL/pages/467108890/eInvoicing+in+Italy) | No national configuration listed |
| Latvia | EN 16931/Peppol structured exchange | Profile/ecosystem | No separate national CIUS identified | [Commission factsheet](https://ec.europa.eu/digital-building-blocks/sites/spaces/DIGITAL/pages/467108891/eInvoicing+in+Latvia) | No national configuration listed |
| Lithuania | EN 16931/Peppol structured exchange; SABIS | Profile/ecosystem and platform | EN 16931-based structured data; SABIS is a platform | [Commission factsheet](https://ec.europa.eu/digital-building-blocks/sites/spaces/DIGITAL/pages/467108892/eInvoicing+in+Lithuania) | No national configuration listed |
| Luxembourg | Peppol BIS Billing 3.0 over UBL/CII | Cross-border profile and syntaxes | EN 16931 profile; no separate national CIUS identified | [Commission factsheet](https://ec.europa.eu/digital-building-blocks/sites/spaces/DIGITAL/pages/467108893/eInvoicing+in+Luxembourg) | No national configuration listed |
| Malta | Peppol BIS Billing 3.0 over UBL | Cross-border profile and syntax | EN 16931 profile; no separate national CIUS identified | [Commission factsheet](https://ec.europa.eu/digital-building-blocks/sites/spaces/DIGITAL/pages/467108894/eInvoicing+in+Malta) | No national configuration listed |
| Netherlands | NLCIUS, UBL-OHNL, SI-UBL | National profiles over UBL | EN 16931-compatible national UBL profiles | [Commission factsheet](https://ec.europa.eu/digital-building-blocks/sites/spaces/DIGITAL/pages/467108895/eInvoicing+in+The+Netherlands) | No national configuration listed |
| Poland | Peppol BIS with Polish extensions; KSeF/PEF | Profile/extensions and platforms | Peppol is EN 16931-based; KSeF and PEF are exchange platforms/specifications | [Commission factsheet](https://ec.europa.eu/digital-building-blocks/sites/spaces/DIGITAL/pages/467108896/eInvoicing+in+Poland) | No national configuration listed |
| Portugal | CIUS-PT over UBL/CII | National profile and XML syntaxes | EN 16931 CIUS/profile | [Commission factsheet](https://ec.europa.eu/digital-building-blocks/sites/spaces/DIGITAL/pages/467108897/eInvoicing+in+Portugal) | No national configuration listed |
| Romania | RO_CIUS over UBL/CII XML | National profile and XML syntaxes | EN 16931 CIUS/profile; RO e-Factura is the exchange/reporting system | [Commission factsheet](https://ec.europa.eu/digital-building-blocks/sites/spaces/DIGITAL/pages/467108898/eInvoicing+in+Romania) | No national configuration listed |
| Slovakia | UBL/CII and planned Peppol solution | XML syntaxes and planned exchange profile | EN 16931-compatible options; no named national CIUS identified | [Commission factsheet](https://ec.europa.eu/digital-building-blocks/sites/spaces/DIGITAL/pages/467108899/eInvoicing+in+Slovakia) | No national configuration listed |
| Slovenia | e-SLOG 2.0; EN 16931/Peppol | National syntax and exchange profiles | e-SLOG interoperates with EN 16931/Peppol where the selected profile supports it | [Commission factsheet](https://ec.europa.eu/digital-building-blocks/sites/spaces/DIGITAL/pages/467108900/eInvoicing+in+Slovenia) | No national configuration listed |
| Spain | Facturae | National XML syntax; FACe is a platform | Separate national syntax; EN 16931 compatibility depends on the selected implementation | [Commission factsheet](https://ec.europa.eu/digital-building-blocks/sites/spaces/DIGITAL/pages/467108901/eInvoicing+in+Spain) | No national configuration listed |
| Sweden | Peppol BIS Billing 3.0 over UBL; SFTI guidance | Cross-border profile and sector guidance | EN 16931 profile; no separate national CIUS identified | [Commission factsheet](https://ec.europa.eu/digital-building-blocks/sites/spaces/DIGITAL/pages/467108902/eInvoicing+in+Sweden) | No national configuration listed |

This inventory does not imply that the project validates any listed national syntax. The declared project conformance values and their implementation status are documented in the table below and in the [validation configuration matrix](#validation-configurations).

| Variation | Role | Declared project value / status |
## Detailed Validation Configurations and Formats

The repositories in the country blocks below are evidence sources, not a claim that `Esi.OpenEN16931` already executes every listed artefact. A country-specific entry requires an identifiable national syntax, CIUS, extension, schema, Schematron/XSLT rule set or national executable rule implementation. CEN, OASIS UBL/CII and Peppol BIS are listed once as shared dependencies and are deliberately not repeated as national projects.

### Common dependencies

- [ConnectingEurope/eInvoicing-EN16931](https://github.com/ConnectingEurope/eInvoicing-EN16931) supplies shared EN 16931 UBL/CII schemas and rules. The current public release is 1.3.16; examples include `ubl/schematron/EN16931-UBL-validation.sch` and `cii/schematron/EN16931-CII-validation.sch`.
- [OpenPEPPOL/peppol-bis-invoice-3](https://github.com/OpenPEPPOL/peppol-bis-invoice-3) supplies shared Peppol BIS Billing rules. A Peppol repository is not a national validator merely because a country uses Peppol.
- [itplr-kosit/validator-configuration-bis](https://github.com/itplr-kosit/validator-configuration-bis) is a shared BIS execution configuration. Its UBL XSD and `CEN-EN16931-UBL.xslt`/`PEPPOL-EN16931-UBL.xslt` files are common dependencies, not country artefacts.
- [phax/phive-rules-foundations](https://github.com/phax/phive-rules-foundations) now contains the XSD-only PHIVE foundation modules `phive-rules-ebinterface`, `phive-rules-facturae`, `phive-rules-fatturapa`, `phive-rules-finvoice`, `phive-rules-ksef`, `phive-rules-osa` and `phive-rules-teapps`. They were extracted from `phax/phive-rules` in v4.5.0; their Maven and VES coordinates are unchanged. These modules provide structural validation, not national Schematron business rules.
- `origins/` is read-only. Paths below are references to checked-out upstream material and must not be modified.

### Austria (AT)

- **Syntax/profile:** Austrian `ebInterface`; Austrian B2G rules; UBL/Peppol is a separate shared route.
- **Country-specific projects:** [Stoicera/einvoice_at](https://github.com/Stoicera/einvoice_at) contains `validation/src/main/resources/schematron/at-b2g-ebinterface-6.1.sch`, `validation/src/main/java/com/stoicera/einvoice/validation/stage/BusinessRuleStage.java` and `AT-B2G-01` through `AT-B2G-05`. The XSD-only PHIVE module is now in [phax/phive-rules-foundations](https://github.com/phax/phive-rules-foundations), under `phive-rules-ebinterface`.
- **Schemas/rules/tests:** ebInterface XSDs are supplied by the `ph-ebinterface` dependency; foundation-module tests are under `phive-rules-foundations/phive-rules-ebinterface/src/test/resources/external/test-files/v61/`. Austrian project tests are under `validation/src/test/java/.../SchematronStageTest.java` and `validation/src/test/resources/corpus/`.
- **Status:** National ebInterface/AT-B2G evidence is available. The PHIVE module is structural/XSD-only; the AT-B2G Schematron in `einvoice_at` is a separate project-specific B2G rule layer. `austriapro/ebinterface-ubl-mapping` remains mapping only; Peppol artefacts are shared dependencies.

### Belgium (BE)

- **Syntax/profile:** Belgian e-FFF / UBL.BE over UBL 2.1.
- **Country-specific project/module:** The shared [phax/phive-rules](https://github.com/phax/phive-rules) repository contains `phive-rules-ublbe`; it is not itself a Belgian-only repository.
- **Rules/tests:** `phive-rules-ublbe/src/main/resources/external/schematron/ublbe/en16931/v1.31/GLOBALUBL.BE.xslt`; Belgian fixtures are under `phive-rules-ublbe/src/test/resources/external/test-files/en16931/v1.31/`.
- **Status:** Belgian compiled XSLT and fixtures identified. No separate Belgian validator repository was identified.

### Bulgaria (BG)

- **Syntax/profile:** UBL/CII or Peppol routes documented for structured exchange; platform rules are separate.
- **Country-specific GitHub project:** No country-specific GitHub validator identified after checking the local submodules and the online candidates reviewed.
- **Schemas/rules/tests:** No Bulgarian national XSD, `.sch` or `.xsl`/`.xslt` path was verified in this workspace.
- **Status:** Use the shared EN 16931 and selected Peppol layers only; do not label them Bulgarian rules.

### Croatia (HR)

- **Syntax/profile:** UBL 2.1 under Croatian CIUS/EXT 2025 and Fiskalizacija 2.0.
- **Country-specific projects:** [verifaktura/verifaktura](https://github.com/verifaktura/verifaktura) registers `@verifaktura/cius-hr` and compiles the official `HR-CIUS-EXT-EN16931-UBL.sch`; [stboris/laravel-eracun](https://github.com/stboris/laravel-eracun) implements the Croatian overlay with `HR-BR-*` rules. The shared PHIVE module is `phive-rules-eracun`.
- **Schemas/rules/tests:** PHIVE compiled rule: `phive-rules-eracun/src/main/resources/external/schematron/1.0.3/HR-CIUS-EXT-EN16931-UBL.xslt`. The online validator uses `packages/cius-hr/sef/hr-cius-ext-ubl.sef.json` generated from the official source. The PHP project records `research/schematron/HR-CIUS-EXT-EN16931-UBL.sch`, `research/schematron/HR-CIUS-EXT-EN16931-UBL-codes.sch`, `research/xsd/`, `research/fixtures/` and `src/Validation/Rules/`.
- **Status:** Strong national evidence exists, but the Croatian overlay is an additional layer and does not replace EN 16931 base validation.

### Cyprus (CY)

- **Syntax/profile:** Peppol BIS Billing 3.0 over UBL is the identified structured route.
- **Country-specific GitHub project:** No country-specific GitHub validator identified after local and reviewed online searches.
- **Schemas/rules/tests:** No Cypriot national XSD or Schematron/XSLT path was verified.
- **Status:** Shared EN 16931/Peppol rules only; no national validator claim.

### Czechia (CZ)

- **Syntax/profile:** ISDOC 6.0.2; UBL/Peppol is a separate route.
- **Country-specific project/module:** The shared [phax/phive-rules](https://github.com/phax/phive-rules) repository contains `phive-rules-isdoc`.
- **Schemas/rules/tests:** `phive-rules-isdoc/src/main/resources/external/schemas/isdoc/6.0.2/isdoc-invoice-6.0.2.xsd`, `isdoc-commondocument-6.0.2.xsd`, `isdoc-invoice-dsig-6.0.2.xsd`, `isdoc-commondocument-dsig-6.0.2.xsd` and `isdoc-manifest-6.0.2.xsd`; Schematron source: `phive-rules-isdoc/src/test/resources/external/rule-source/6.0.2/isdoc-6.0.2.sch`.
- **Status:** National syntax, schema, rule source and tests identified. No separate Czech validator repository was identified.

### Denmark (DK)

- **Syntax/profile:** OIOUBL, a Danish UBL profile; NemHandel and Peppol transport rules are separate.
- **Country-specific project/module:** The shared [phax/phive-rules](https://github.com/phax/phive-rules) repository contains `phive-rules-oioubl`.
- **Rules:** `phive-rules-oioubl/src/main/resources/external/schematron/oioubl/2.0.2/OIOUBL_Invoice_Schematron.xsl` and `.../3.0.1/xslt/OIOUBL-Invoice.xslt`; source rules for 3.0.1 are under `phive-rules-oioubl/src/test/resources/external/rule-source/oioubl/3.0.1/`.
- **Status:** Danish profile rules identified. The current PHIVE documentation marks OIOUBL as a legacy/discontinued government profile, so the exact accepted version and authority route must be confirmed before using it for new integrations. The invoice XSD path was not verified in the checked-out module, so schema completeness remains partial.

### Estonia (EE)

- **Syntax/profile:** Estonian national XML alongside EN 16931/Peppol routes.
- **Country-specific project/module:** The shared PHIVE repository lists `phive-rules-estonian`.
- **Schemas/rules:** Local evidence is documentation and syntax material under `phive-rules-estonian/docs/v1.2/`, including `e-invoice_ver1.2.EN.xsd.xml` and the Estonian syntax-binding PDFs. No executable `.sch`, `.xsl` or `.xslt` was found in the initial module search.
- **Status:** National syntax documentation identified, but no executable national validator artefact was verified. No separate Estonian validator repository was identified.

### Finland (FI)

- **Syntax/profile:** Finvoice 3.0 and TEAPPSXML 3.0; UBL/CII/Peppol is a separate shared route.
- **Country-specific project/module:** The XSD-only modules are now in [phax/phive-rules-foundations](https://github.com/phax/phive-rules-foundations), under `phive-rules-finvoice` and `phive-rules-teapps`.
- **Schemas/tests:** `phive-rules-foundations/phive-rules-finvoice/src/main/resources/external/schemas/Finvoice3.0.xsd`; `phive-rules-foundations/phive-rules-teapps/src/main/resources/external/schemas/teappsxmlv30_schema_invoices_0.xsd`. The modules contain test data; no validation Schematron was found, and `docs/3.0/FinvoiceEnglanti.xsl` is a presentation stylesheet.
- **Status:** National XSD validation evidence is available; business-rule artefacts are incomplete. No separate Finnish validator repository was identified.

### France (FR)

- **Syntax/profile:** French CTC/Flux 2 CIUS over UBL/CII; Factur-X is a PDF/A-3 container with embedded CII.
- **Country-specific project/module:** The shared [phax/phive-rules](https://github.com/phax/phive-rules) repository contains `phive-rules-france`.
- **Rules/tests:** Raw sources are under `phive-rules-france/src/test/resources/external/rule-source/ctc/1.4.0/`, including `20260630_EXTENDED-CTC-FR-UBL-V1.4.0.sch` and the CII, Flux2 and CDV sources. Compiled rules are under `phive-rules-france/src/main/resources/external/schematron/ctc/1.4.0/xslt/`; tests are under `phive-rules-france/src/test/resources/external/test-files/ctc/1.4.0/`.
- **Status:** French national Schematron/XSLT and fixtures identified. The [atgp/factur-x](https://github.com/atgp/factur-x) repository supplies container/CII profile schemas, not the French CTC validator.

### Germany (DE)

- **Syntax/profile:** XRechnung CIUS over UBL/CII, including German extensions and CVD data.
- **Country-specific projects/modules:** The shared [phax/phive-rules](https://github.com/phax/phive-rules) repository contains the German `phive-rules-xrechnung` module. The [itplr-kosit/validator-configuration-xrechnung](https://github.com/itplr-kosit/validator-configuration-xrechnung) repository provides the KOSIT execution configuration, and its [xrechnung-testsuite](https://github.com/itplr-kosit/xrechnung-testsuite) provides the official test suite.
- **Rules/tests:** PHIVE contains compiled `phive-rules-xrechnung/src/main/resources/external/schematron/3.0.2/XRechnung-UBL-validation.xslt` and `XRechnung-CII-validation.xslt`, with separate rule patterns for XRechnung, the UBL extension and CVD. Fixtures are grouped under `phive-rules-xrechnung/src/test/resources/external/test-files/3.0.2/ubl-inv/`, `ubl-inv-ext/`, `cii/` and `cii-ext/`; the fixtures carry the German `urn:xeinkauf.de:kosit:xrechnung_3.0` customization identifiers.
- **Status:** German national validation rules are verified in the PHIVE module and in the KOSIT configuration/test ecosystem. The shared EN 16931 and UBL/CII schema layers remain prerequisites. Factur-X/ZUGFeRD remains a container and is not an alternative German XML syntax.

### Greece (GR)

- **Syntax/profile:** Peppol BIS Billing 3.0 over UBL; myDATA is reporting, not a national invoice validator.
- **Country-specific GitHub project:** No country-specific GitHub validator identified after local and reviewed online searches.
- **Schemas/rules/tests:** No Greek national XSD or Schematron/XSLT path was verified.
- **Status:** Shared EN 16931/Peppol rules only; myDATA integration must not be counted as invoice-rule evidence.

### Hungary (HU)

- **Syntax/profile:** NAV Online Számla (OSA) reporting XML; Peppol/UBL is a separate invoice route.
- **Country-specific project/module:** The XSD-only module is now in [phax/phive-rules-foundations](https://github.com/phax/phive-rules-foundations), under `phive-rules-osa`.
- **Schemas:** `phive-rules-foundations/phive-rules-osa/src/main/resources/external/schemas/v3.0/invoiceData.xsd`, `invoiceBase.xsd`, `invoiceApi.xsd`, `invoiceAnnulment.xsd`, `common.xsd` and `serviceMetrics.xsd`; version 2.0 equivalents are under `.../schemas/v2.0/`.
- **Status:** Hungarian reporting schemas are verified, but this is not evidence of an EN 16931 national invoice validator. No separate Hungarian invoice validator repository was identified.

### Ireland (IE)

- **Syntax/profile:** Peppol BIS Billing 3.0 over UBL/CII.
- **Country-specific GitHub project:** No country-specific GitHub validator identified after local and reviewed online searches.
- **Schemas/rules/tests:** No Irish national XSD or Schematron/XSLT path was verified.
- **Status:** Shared EN 16931/Peppol rules only.

### Italy (IT)

- **Syntax/profile:** FatturaPA XML; Italian Peppol extensions are a separate UBL profile.
- **Country-specific project/module:** The XSD-only `phive-rules-fatturapa` module is now in [phax/phive-rules-foundations](https://github.com/phax/phive-rules-foundations); Italian Peppol extensions remain in `phax/phive-rules` as `phive-rules-peppol-italy`.
- **Rules/tests:** Italian Peppol rules include `phive-rules-peppol-italy/src/main/resources/external/schematron/peppol-italy/3.2.1/invoice/AGID-EN16931-UBL - PEPPOL ITA.xslt`, `AGID-PEPPOL-T01.xslt` and related order/despatch rules; invoice fixtures are under `phive-rules-peppol-italy/src/test/resources/external/test-files/3.2.1/invoice/`. The foundation module supplies FatturaPA structural validation; it does not establish a complete FatturaPA business-rule validator.
- **Status:** Italian Peppol national rules are verified. The PHIVE README records that the 3.2.1 AGID package was updated in place and that the committed XSLT was not regenerated from the 2026-08-03 snapshot, so consumers must pin and verify the exact artefact version. Do not substitute an unrelated converter XSD.

### Latvia (LV)

- **Syntax/profile:** EN 16931/Peppol structured exchange.
- **Country-specific GitHub project:** No country-specific GitHub validator identified after local and reviewed online searches.
- **Schemas/rules/tests:** No Latvian national XSD or Schematron/XSLT path was verified.
- **Status:** Shared EN 16931/Peppol rules only.

### Lithuania (LT)

- **Syntax/profile:** EN 16931/Peppol and SABIS exchange requirements.
- **Country-specific GitHub project:** No country-specific GitHub validator identified after local and reviewed online searches.
- **Schemas/rules/tests:** No Lithuanian national XSD or Schematron/XSLT path was verified.
- **Status:** SABIS is a platform; it is not counted as a validator without executable national rule artefacts.

### Luxembourg (LU)

- **Syntax/profile:** Peppol BIS Billing 3.0 over UBL/CII.
- **Country-specific GitHub project:** No country-specific GitHub validator identified after local and reviewed online searches.
- **Schemas/rules/tests:** No Luxembourg national XSD or Schematron/XSLT path was verified.
- **Status:** Shared EN 16931/Peppol rules only.

### Malta (MT)

- **Syntax/profile:** Peppol BIS Billing 3.0 over UBL.
- **Country-specific GitHub project:** No country-specific GitHub validator identified after local and reviewed online searches.
- **Schemas/rules/tests:** No Maltese national XSD or Schematron/XSLT path was verified.
- **Status:** Shared EN 16931/Peppol rules only.

### Netherlands (NL)

- **Syntax/profile:** NLCIUS, UBL-OHNL, SI-UBL and energy-sector profiles over UBL.
- **Country-specific project/module:** The shared PHIVE repository contains national/sector modules such as `phive-rules-energieefactuur`; this is not a general Dutch CIUS validator repository.
- **Schemas:** `phive-rules-energieefactuur/src/main/resources/external/schemas/energieefactuur/SEeF_UBLExtension_v3.1.0.xsd` and earlier versioned schemas.
- **Status:** Dutch sector extension evidence exists. No general NLCIUS/UBL-OHNL/SI-UBL validator repository was identified.

### Poland (PL)

- **Syntax/profile:** KSeF XML and Peppol with Polish extensions; these are distinct routes.
- **Country-specific project/module:** The XSD-only `phive-rules-ksef` module is now in [phax/phive-rules-foundations](https://github.com/phax/phive-rules-foundations).
- **Schemas/tests:** `phive-rules-foundations/phive-rules-ksef/src/main/resources/external/schemas/3.0.0/StrukturyDanych_v10-0E.xsd` and `schemat.xsd`; versions 2.0.0 and 1.0.0 are also present. Version 1.0.0 includes `KodyKrajow_v9-0E.xsd` and `ElementarneTypyDanych_v9-0E.xsd`; fixtures are under `phive-rules-foundations/phive-rules-ksef/src/test/resources/external/test-files/fa1`, `fa2` and `fa3`.
- **Status:** Polish KSeF schema validation is verified. It must not be confused with generic Peppol validation.

### Portugal (PT)

- **Syntax/profile:** CIUS-PT over UBL/CII.
- **Country-specific project/module:** The shared [phax/phive-rules](https://github.com/phax/phive-rules) repository contains `phive-rules-cius-pt`.
- **Rules:** `phive-rules-cius-pt/src/main/resources/external/schematron/2.1.1/urn_feap.gov.pt_CIUS-PT_2.1.1.xslt` and the 2.0.0 equivalent.
- **Status:** Portuguese compiled CIUS XSLT is verified. No separate Portuguese validator repository was identified.

### Romania (RO)

- **Syntax/profile:** RO_CIUS / RO e-Factura over UBL.
- **Country-specific project/module:** The shared [phax/phive-rules](https://github.com/phax/phive-rules) repository contains `phive-rules-cius-ro`.
- **Rules/tests:** Raw sources include `phive-rules-cius-ro/src/test/resources/external/rule-source/1.0.9/cius-ro/RO16931-rules.sch` and `EN16931-CIUS_RO-UBL-validation.sch`; compiled rules include `phive-rules-cius-ro/src/main/resources/external/schematron/1.0.9/EN16931-CIUS_RO-UBL-validation.xslt` and `ROeFactura-UBL-validation-Invoice_v1.0.9.xslt`; XML fixtures are under `phive-rules-cius-ro/src/test/resources/external/test-files/1.0.9/`.
- **Status:** Romanian national Schematron/XSLT and test data are verified. The overlay still requires the shared EN 16931 base layer.

### Slovakia (SK)

- **Syntax/profile:** UBL/CII EN 16931-compatible route; Peppol deployment is separate and evolving.
- **Country-specific GitHub project:** No country-specific GitHub validator identified after local and reviewed online searches.
- **Schemas/rules/tests:** No Slovak national XSD or Schematron/XSLT path was verified.
- **Status:** Shared EN 16931/Peppol rules only.

### Slovenia (SI)

- **Syntax/profile:** e-SLOG 2.0 and EN 16931/Peppol interoperability.
- **Country-specific GitHub project:** No country-specific GitHub validator identified after local and reviewed online searches.
- **Schemas/rules/tests:** No e-SLOG XSD plus executable Schematron/XSLT repository was verified.
- **Status:** e-SLOG is documented as a national syntax, but no qualifying GitHub validator was identified.

### Spain (ES)

- **Syntax/profile:** Facturae XML; FACe is a gateway, not the syntax.
- **Country-specific project/module:** The XSD-only `phive-rules-facturae` module is now in [phax/phive-rules-foundations](https://github.com/phax/phive-rules-foundations).
- **Schemas/tests:** `phive-rules-foundations/phive-rules-facturae/src/main/resources/external/schemas/Facturaev3_2.xsd`, `Facturaev3_2_1.xsd`, `Facturaev3_2_2.xsd` and older versions; XML and `.xsig` fixtures are under `phive-rules-foundations/phive-rules-facturae/src/test/resources/external/test-files/`.
- **Status:** Spanish national schema and test evidence is verified; no validation Schematron was found in the module.

### Sweden (SE)

- **Syntax/profile:** Svefaktura 1.0; Peppol BIS is a separate shared route.
- **Country-specific project/module:** The shared [phax/phive-rules](https://github.com/phax/phive-rules) repository contains `phive-rules-svefaktura`.
- **Schemas/rules:** `phive-rules-svefaktura/src/main/resources/external/schemas/1.0/maindoc/SFTI-BasicInvoice-1.0.xsd`, `SFTI-ObjectEnvelope-1.0.xsd` and `svenfaktura-1.0-sch.xslt`, with supporting common and code-list XSDs in the same directory.
- **Status:** Swedish national syntax schemas and compiled rules are verified. No separate Swedish validator repository was identified.

### Validation Pipeline Per Format
When validating an invoice, apply the available artefacts in this order:

1. **XSD schema validation**: check the selected XML vocabulary and version.
2. **EN 16931 semantic rules**: apply the shared model, syntax and code-list rules.
3. **Profile or CIUS rules**: apply Peppol BIS or the country-specific rules documented in the relevant country block.
4. **National extension or reporting rules**: apply these only when the selected national route requires them; reporting schemas are not invoice syntax rules.
5. **Container and signature checks**: for Factur-X/ZUGFeRD, validate PDF/A-3, the embedded CII relationship and signatures separately from the XML invoice rules.
6. **Test fixtures**: verify the implementation against the documented positive and negative samples.