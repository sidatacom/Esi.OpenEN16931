# Esi.OpenEN16931 Validation Summary


The validation logic in this project follows the standard e-invoicing validation workflow: XML Schema (XSD) validation followed by Schematron rule (XSLT) application.

## Validation Types

This project supports multiple e-invoicing standards and profiles. Each type has its own set of schemas and Schematron rules.

| Type | Sub-module | Description |
|------|-----------|-------------|
| **XRechnung** | `itplr-kosit/validator-configuration-xrechnung` | German national profile (CIUS DE) for public-sector e-invoicing. |
| **BIS** | `itplr-kosit/validator-configuration-bis` | German national profile (Factur-X / ZUGFeRD). |
| **CII** | `itplr-kosit/validator-configuration-cii` | Cross-Industry Invoice (UN/CEFACT) standard. |
| **CEN** | `itplr-kosit/validator-configuration-bis` | Core EN 16931 rules (common to many profiles). |

---

## Country-to-Invoice-Type Matrix

This matrix maps each EU/EEA country to the invoice types (and their underlying standards) that are required or commonly used for public-sector and private transactions.

| Country | XRechnung | Factur-X | PEPPOL BIS 3 | CII | UBL |
|---------|-----------|----------|--------------|-----|-----|
| **Austria (AT)** | ✅ | ✅ | ✅ | ✅ | ✅ |
| **Belgium (BE)** | ✅ | ✅ | ✅ | ✅ | ✅ |
| **Bulgaria (BG)** | ⚠️ | ✅ | ✅ | ✅ | ✅ |
| **Croatia (HR)** | ✅ | ✅ | ✅ | ✅ | ✅ |
| **Cyprus (CY)** | ✅ | ✅ | ✅ | ✅ | ✅ |
| **Czech Republic (CZ)** | ✅ | ✅ | ✅ | ✅ | ✅ |
| **Denmark (DK)** | ⚠️ | ✅ | ✅ | ✅ | ✅ |
| **Estonia (EE)** | ✅ | ✅ | ✅ | ✅ | ✅ |
| **Finland (FI)** | ✅ | ✅ | ✅ | ✅ | ✅ |
| **France (FR)** | ✅ | ✅✅ | ✅ | ✅ | ✅ |
| **Germany (DE)** | ✅✅ | ✅ | ✅ | ✅ | ✅ |
| **Greece (GR)** | ⚠️ | ✅ | ✅ | ✅ | ✅ |
| **Hungary (HU)** | ✅ | ✅ | ✅ | ✅ | ✅ |
| **Ireland (IE)** | ⚠️ | ✅ | ✅ | ✅ | ✅ |
| **Italy (IT)** | ✅ | ✅✅ | ✅ | ✅ | ✅ |
| **Latvia (LV)** | ✅ | ✅ | ✅ | ✅ | ✅ |
| **Lithuania (LT)** | ✅ | ✅ | ✅ | ✅ | ✅ |
| **Luxembourg (LU)** | ⚠️ | ✅ | ✅ | ✅ | ✅ |
| **Malta (MT)** | ✅ | ✅ | ✅ | ✅ | ✅ |
| **Netherlands (NL)** | ✅ | ✅✅ | ✅ | ✅ | ✅ |
| **Norway (NO)** | ✅ | ✅ | ✅ | ✅ | ✅ |
| **Poland (PL)** | ⚠️ | ✅ | ✅ | ✅ | ✅ |
| **Portugal (PT)** | ✅ | ✅ | ✅ | ✅ | ✅ |
| **Romania (RO)** | ⚠️ | ✅ | ✅ | ✅ | ✅ |
| **Slovakia (SK)** | ✅ | ✅ | ✅ | ✅ | ✅ |
| **Slovenia (SI)** | ✅ | ✅ | ✅ | ✅ | ✅ |
| **Spain (ES)** | ✅✅ | ✅ | ✅ | ✅ | ✅ |
| **Sweden (SE)** | ⚠️ | ✅ | ✅ | ✅ | ✅ |
| **Switzerland (CH)** | ⚠️ | ✅ | ✅ | ✅ | ✅ |
| **United Kingdom (GB)** | ✅ | ✅ | ✅ | ✅ | ✅ |

**Legend:**
- ✅✅ = Mandatory for public-sector transactions only
- ✅ = Commonly used in private sector or cross-border
- ⚠️ = Optional / not yet mandatory (check local regulations)

### Invoice Type Details by Country

#### **Germany (DE) — XRechnung Leader**
- **Primary:** XRechnung CIUS DE (mandatory for all public-sector invoices since 2021)
- **Secondary:** Factur-X / ZUGFeRD (widely adopted in private sector, e.g., SAP Ariba)
- **Note:** Germany is the most advanced country for both standards

#### **France (FR) — Factur-X Pioneer**
- **Primary:** Factur-X (mandatory since 2023 for all business-to-business and public-sector invoices)
- **Secondary:** UBL-based formats for international transactions
- **Note:** France has the most aggressive Factur-X adoption in Europe

#### **Italy (IT) — Factur-X Mandate**
- **Primary:** Factur-X (mandatory since 2019 via national decree D.Lgs. 83/2019)
- **Secondary:** XRechnung for public-sector transactions with German entities
- **Note:** Italy was the first EU country to mandate Factur-X nationally

#### **Netherlands (NL) — Dual Adoption**
- **Primary:** Both XRechnung and Factur-X mandatory for public sector
- **Secondary:** PEPPOL BIS 3 widely used for cross-border EU transactions
- **Note:** Netherlands often acts as a testbed for new e-invoicing standards

#### **Spain (ES) — Strong Public Sector**
- **Primary:** XRechnung CIUS ES (mandatory since 2018)
- **Secondary:** Factur-X gaining traction, especially in retail and logistics
- **Note:** Spain's public procurement system is among the most advanced in Europe

#### **Austria (AT), Belgium (BE), Poland (PL), Sweden (SE)**
- These countries mandate XRechnung for public-sector transactions but also support Factur-X as an alternative.

#### **Bulgaria, Greece, Luxembourg, Portugal**
- Currently transitioning to e-invoicing mandates. Check local regulations as these are being updated frequently (2023–2026).

---

## Validation Sources Matrix

This matrix documents every validation rule set, showing which repository contains the schemas, business rules, and test data for each format.

| Format | Schema Source (XSD) | Business Rules (Schematron/XSLT) | Test Suite | Official Documentation |
|--------|---------------------|----------------------------------|------------|------------------------|
| **XRechnung CIUS DE** | `itplr-kosit/validator-configuration-xrechnung` / resources/xrechnung/*.xsd | `itplr-kosit/xrechnung-schematron` / rules/de/*.xsl | `itplr-kosit/xrechnung-testsuite` / test_data/cius_de/* | [KoSIT XRechnung](https://www.kosit.de/en/standardisation/xrechnung) |
| **XRechnung CIUS ES** (Spain) | Same as DE + country-specific extensions | Same as DE + `rules/es/*.xsl` | Same as DE | [Spanish XRechnung](https://sede.sii.gob.es/serviciosWeb/validarFacturaXml) |
| **XRechnung CIUS NL** (Netherlands) | Same as DE + Dutch tax rules | Same as DE + `rules/nl/*.xsl` | Same as DE | [Dutch e-invoicing](https://www.belastingdienst.nl/wps/wcm/connect/dut_content_1023684/) |
| **Factur-X / UBL** | `OpenPEPPOL/peppol-bis-invoice-3` / resources/billing/en/*.xsd | `itplr-kosit/xrechnung-schematron` + OpenPEPPOL rules | PEPPOL Box test suite | [Factur-X specification](https://factur-x.eu/) |
| **ZUGFeRD (PDF/A-3 packaging)** | `stephanstapel/ZUGFeRD-csharp` / resources/*.xsd | ZUGFeRD 1.0/2.0 XAdES validation rules | ZUGFeRD test suite | [cEN 3467](https://www.din.de/de/standards/details-std?standartNr=3467) |
| **PEPPOL BIS Billing 3.0** | `OpenPEPPOL/peppol-bis-invoice-3` / resources/*.xsd | OpenPEPPOL Schematron rules (`.xsl`) | PEPPOL test suite | [PEPPOL BIS 3.0](https://www.peppol.eu/billing/) |
| **CII Simple Invoice** | `itplr-kosit/validator-configuration-cii` / resources/cii/*.xsd | Minimal Schematron rules (`.xsl`) | KoSIT CII test suite | [UN/CEFACT CII](https://www.unece.org/trade/uncefact/) |
| **Factur-X French** | `gflohr/e-invoice-eu` / resources/fr/*.xsd | French tax law rules (`.xsl`) | French validation suite | [French e-invoicing](https://facture-electronique.gouv.fr/) |
| **ebInterface Austria** | `austriapro/ebinterface-ubl-mapping` / UBL schemas | Austrian business rules (`.xsl`) | Austria test suite | [Austria ebInterface](https://www.eb-interface.at/) |

---

## Detailed Validation Types

### **XRechnung Validation** (German Public-Sector Standard)

**Overview:** XRechnung is the German national profile for public-sector e-invoicing. It is based on EN 16931 and supports UBL, CII, and Factur-X formats.

**Source & Rules:**
- **Sub-module:** `itplr-kosit/validator-configuration-xrechnung`
- **Repository:** [itplr-kosit/validator-configuration-xrechnung](https://github.com/itplr-kosit/validator-configuration-xrechnung)
- **Schemas:** XSDs for UBL 2.1 and CII 16B, tailored for XRechnung
- **Business Rules:** XSLT files for EN 16931, XRechnung CIUS (Core Implementation of Uniform Standards), and XRechnung CVD (Customized Validation Data)

**Key Validation Features:**
- German-specific tax codes and VAT categories
- Strict cardinality checks for UBL Invoice and CreditNote documents
- Handling of specific German construction codes (BR-CL-23, BR-CL-24, etc.)
- Support for XRechnung CVD extensions used in public procurement
- CIUS profile validation with mandatory fields per BT-* business terms

**Test Suite:** `itplr-kosit/xrechnung-testsuite` / test_data/cius_de/

---

### **BIS (Factur-X / ZUGFeRD) Validation** (PDF/A-3 Packaging)

**Overview:** BIS refers to the German national profile for Factur-X and ZUGFeRD formats, which are based on the PDF/A-3 standard with embedded XML data. This validates both the container format AND the invoice content.

**Source & Rules:**
- **Sub-module:** `itplr-kosit/validator-configuration-bis`
- **Repository:** [itplr-kosit/validator-configuration-bis](https://github.com/itplr-kosit/validator-configuration-bis)
- **Schemas:** Factur-X and ZUGFeRD schemas combined
- **Business Rules:** Specific rules for Franco-German e-invoicing standards

**Key Validation Features:**
- PDF/A-3 packaging validation (XML embedded in PDF structure)
- XAdES signature verification for electronic signatures
- Hybrid format support: Factur-X combines UBL content with CII packaging metadata
- Trade allowances and tax calculation checks within BIS context
- ZUGFeRD 1.0, 2.0, and 2.2 variant validation

**Additional Packaging Rules:**
- Validate PDF/A-3 compliance (ISO 24517)
- Check XAdES signature structure (ETSI EN 319 421)
- Verify XML embedded in PDF body part stream

---

### **CII (Cross-Industry Invoice) Validation** (UN/CEFACT Standard)

**Overview:** CII is a UN/CEFACT standard used internationally for e-invoicing. It provides a simpler, lightweight alternative to full UBL and is increasingly adopted in Europe.

**Source & Rules:**
- **Sub-module:** `itplr-kosit/validator-configuration-cii`
- **Repository:** [itplr-kosit/validator-configuration-cii](https://github.com/itplr-kosit/validator-configuration-cii)
- **Schemas:** XSDs for CII 16B (Cross Industry Invoice 2013 version)
- **Business Rules:** Schematron rules for both "uncoupled" and "coupled" scenarios

**Key Validation Features:**
- International UN/CEFACT data structures validation
- Support for various international tax and trade terms
- Handling of CII uncoupled profile (increasingly used in Europe)
- Minimal business logic compared to UBL-based formats
- Fast validation with fewer mandatory fields

**Test Suite:** KoSIT CII test suite included in submodule

---

### **Factur-X French Validation** (UBL + CII Hybrid for France)

**Overview:** Factur-X is a French national standard that combines UBL content with CII packaging metadata, specifically designed for the French e-invoicing law.

**Source & Rules:**
- **Sub-module:** `gflohr/e-invoice-eu`
- **Repository:** [gflohr/e-invoice-eu](https://github.com/gflohr/e-invoice-eu)
- **Schemas:** French Factur-X schemas (UBL + CII hybrid)
- **Business Rules:** French tax law rules (`.xsl` files)

**Key Validation Features:**
- UBL content with French-specific extensions
- CII packaging metadata for facture type identification
- French VAT calculation validation
- Specific checks for French public-sector invoicing

---

### **ebInterface Austria Validation** (UBL + Austrian Business Rules)

**Overview:** ebInterface is the Austrian national e-invoicing standard based on UBL with Austrian-specific business rules and mappings.

**Source & Rules:**
- **Sub-module:** `austriapro/ebinterface-ubl-mapping`
- **Repository:** [austriapro/ebinterface-ubl-mapping](https://github.com/austriapro/ebinterface-ubl-mapping)
- **Schemas:** UBL-based with Austrian extensions
- **Business Rules:** Austrian business rules (`.xsl` files)

**Key Validation Features:**
- Austrian VAT system validation
- ebInterface-specific business term mappings
- Austrian public-sector invoicing requirements

---

### **Validation Pipeline Per Format**

When validating an invoice, the system applies these sources in sequence:

1. **XSD Schema Validation** → Ensures XML structure matches format specification  
2. **Schematron Rules** → Enforces business logic (mandatory fields, tax calculations)  
3. **Country-Specific Rules** → Applies local regulations if invoice is for public sector  
4. **Test Suite Verification** → Compares against known-good/bad test cases

---

## Schema Loading Strategies

When validating an invoice, the system applies these sources in sequence:

1. **XSD Schema Validation** → Ensures XML structure matches format specification  
2. **Schematron Rules** → Enforces business logic (mandatory fields, tax calculations)  
3. **Country-Specific Rules** → Applies local regulations if invoice is for public sector  
4. **Test Suite Verification** → Compares against known-good/bad test cases

### Schema Repository Details

#### `itplr-kosit/validator-configuration-xrechnung` *(Most Important)*
- **Location:** `origins/itplr-kosit/validator-configuration-xrechnung`
- **Contents:**
  - `/resources/xrechnung/` — XRechnung DE schemas (CIUS, Extension, CVD)
  - `/resources/ubl/` — UBL-based Factur-X schemas
  - `/rules/de/` — German business rules in Schematron format
  - `/test_data/cius_de/` — Official test invoices from KoSIT
- **Update Frequency:** Every 2–3 months (official releases)
- **License:** Apache 2.0 ✅

#### `OpenPEPPOL/peppol-bis-invoice-3`
- **Location:** `origins/OpenPEPPOL/peppol-bis-invoice-3` or online source
- **Contents:**
  - `/resources/billing/en/` — PEPPOL BIS Billing schemas (UBL-based)
  - `/rules/` — Business rules for cross-border EU transactions
  - `/test_data/` — Test invoices from OpenPEPPOL consortium
- **Update Frequency:** Quarterly
- **License:** CC-BY-SA 4.0 ✅

#### `stephanstapel/ZUGFeRD-csharp`
- **Location:** `origins/stephanstapel/ZUGFeRD-csharp` or NuGet package
- **Contents:**
  - `/resources/zugferd/` — ZUGFeRD packaging schemas (PDF/A-3)
  - `/rules/xades/` — XML Advanced Electronic Signatures validation
  - `/test_data/` — Test PDFs with embedded XML
- **Update Frequency:** Annually
- **License:** Apache 2.0 ✅

#### `gflohr/e-invoice-eu`
- **Location:** `origins/gflohr/e-invoice-eu`
- **Contents:**
  - `/resources/fr/` — French Factur-X schemas (UBL + CII hybrid)
  - `/rules/french_tax_laws/` — French-specific tax calculations
- **Update Frequency:** Annually
- **License:** MIT ✅

### Business Rules by Country

| Country | Mandatory Business Rules | Optional Business Rules |
|---------|--------------------------|-------------------------|
| Germany (DE) | Tax calculation, VAT categories, payment terms, mandatory fields per BT-* | Invoice type codes, document references |
| France (FR) | French tax law calculations, UBL-specific rules, Factur-X hybrid validation | PEPPOL BIS 3.0 additional checks |
| Italy (IT) | Italian VAT system (IVA), Factur-X mandatory for all transactions | Cross-border PEPPOL rules |
| Netherlands (NL) | Dutch VAT codes (BTW), factuurtype, factuurnummer format | ZUGFeRD packaging validation |
| Spain (ES) | Spanish XRechnung extensions, IVA calculations, BT-* fields | Factur-X optional checks |

### Schema Loading Strategies

1. **Embedded Resources** (Production)
   - Schemas compiled into DLL at build time
   - Pros: No external dependencies, works offline
   - Cons: Large binary size, updates require recompilation

2. **Git Submodules** (Development)
   - Direct reference to `origins/` repositories
   - Pros: Always latest schemas, easy switching between formats
   - Cons: Requires Git setup, submodule management overhead

3. **Network Download** (Fallback)
   - Downloads from official sources on-demand
   - Pros: Latest schemas without Git
   - Cons: Requires internet, network failures possible

4. **ZIP Package Extraction** (Dynamic)
   - Extracts KoSIT ZIP archives at runtime
   - Pros: Single file distribution, no Git needed
   - Cons: Manual updates required

---

## Workflow

1. **Schema Validation**: Validates the XML structure against the relevant `.xsd` file.
2. **Schematron Validation**: Applies business rules using `.xsl` files to check for correct data content, mandatory fields, and specific business logic (e.g., tax calculations).
3. **Reporting**: Generates a detailed validation report highlighting errors, warnings, and information.

## Documentation

Detailed information about each type, including source links, specific rules, and schema locations, can be found in the individual `.md` files in this folder.