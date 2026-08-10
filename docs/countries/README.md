# Country Index

This directory is the entry point for country-specific documentation. All country information has been consolidated into single Markdown files at this level.

## Country Files

- [Austria](./Austria.md) - Austria (at)
- [Belgium](./Belgium.md) - Belgium (be)
- [Bulgaria](./Bulgaria.md) - Bulgaria (bg)
- [Croatia](./Croatia.md) - Croatia (hr)
- [Cyprus](./Cyprus.md) - Cyprus (cy)
- [Czechia](./Czechia.md) - Czechia (cz)
- [Denmark](./Denmark.md) - Denmark (dk)
- [Estonia](./Estonia.md) - Estonia (ee)
- [Finland](./Finland.md) - Finland (fi)
- [France](./France.md) - France (fr)
- [Germany](./Germany.md) - Germany (de)
- [Greece](./Greece.md) - Greece (gr)
- [Hungary](./Hungary.md) - Hungary (hu)
- [Ireland](./Ireland.md) - Ireland (ie)
- [Italy](./Italy.md) - Italy (it)
- [Latvia](./Latvia.md) - Latvia (lv)
- [Lithuania](./Lithuania.md) - Lithuania (lt)
- [Luxembourg](./Luxembourg.md) - Luxembourg (lu)
- [Malta](./Malta.md) - Malta (mt)
- [Netherlands](./Netherlands.md) - Netherlands (nl)
- [Norway](./Norway.md) - Norway (no)
- [Poland](./Poland.md) - Poland (pl)
- [Portugal](./Portugal.md) - Portugal (pt)
- [Romania](./Romania.md) - Romania (ro)
- [Slovakia](./Slovakia.md) - Slovakia (sk)
- [Slovenia](./Slovenia.md) - Slovenia (si)
- [Spain](./Spain.md) - Spain (es)
- [Sweden](./Sweden.md) - Sweden (se)
- [Switzerland](./Switzerland.md) - Switzerland (ch)
- [United Kingdom](./United Kingdom.md) - United Kingdom (gb)

---

# Complete Country-to-Standard Matrix & Summary

This document uses the European Commission country factsheets as the primary source for the 27 EU member states. `EN 16931 relevance` describes the role of the European semantic model in the national implementation; it is not a claim that every country has the same mandate. B2G and B2B obligations are shown separately because Directive 2014/55/EU concerns public procurement and does not create a uniform EU-wide B2B issuance mandate.

Technical columns describe documented formats or profiles, not legal scope. `XRechnung Extension` and `XRechnung CVD` are project and XRechnung validation variations; they are not national adoption indicators outside Germany. `Hybrid PDF/A-3 (Factur-X/ZUGFeRD)` identifies one hybrid container family with embedded CII XML, not two independent formats.

Factur-X/ZUGFeRD is published as a Franco-German standard, but it is technically usable for cross-border B2B, B2G and B2C transactions where the recipient, portal and applicable rules accept it. France and Germany are the countries with the clearest national relevance in this matrix. `Verify` or `Unknown` in another country does not mean that the format is forbidden; it means that country-specific acceptance or usage has not been established by the cited evidence.

## Country-to-Profile-and-Syntax Matrix

| Country | ISO Code | B2G legal status | B2B legal status | UBL 2.1 | CII D16B | XRechnung CIUS | XRechnung Extension | XRechnung CVD | PEPPOL BIS 3 | Hybrid PDF/A-3 (Factur-X/ZUGFeRD) | ebInterface | National profile / syntax | Source | Verification |
|---------|----------|------------------|------------------|----------|----------|-----------------|----------------------|----------------|--------------|------------------------------------|--------------|--------------------------|--------|--------------|
| **Austria** | AT | Partial | No general mandate | Verify | Verify | No | No | No | Documented | Verify | Documented | ebInterface; UBL/Peppol options | [Commission factsheet](https://ec.europa.eu/digital-building-blocks/sites/spaces/DIGITAL/pages/467108876/eInvoicing+in+Austria) | Review scope |
| **Belgium** | BE | Partial | Planned | Documented | Verify | No | No | No | Documented | Verify | No | Peppol BIS Billing 3.0 over UBL | [Commission factsheet](https://ec.europa.eu/digital-building-blocks/sites/spaces/DIGITAL/pages/467108877/eInvoicing+in+Belgium) | Review scope |
| **Bulgaria** | BG | Reception only | No general mandate | Verify | Verify | No | No | No | Documented | Unknown | No | UBL 2.1/CII exchange options | [Commission factsheet](https://ec.europa.eu/digital-building-blocks/sites/spaces/DIGITAL/pages/467108878/eInvoicing+in+Bulgaria) | Review scope |
| **Croatia** | HR | Partial | Planned | Documented | Verify | No | No | No | Documented | Verify | No | Croatian CIUS/profile over UBL/CII | [Commission factsheet](https://ec.europa.eu/digital-building-blocks/sites/spaces/DIGITAL/pages/467108879/eInvoicing+in+Croatia) | Review scope |
| **Cyprus** | CY | Reception only | No general mandate | Verify | Verify | No | No | No | Documented | Unknown | No | Peppol BIS Billing 3.0 over UBL | [Commission factsheet](https://ec.europa.eu/digital-building-blocks/sites/spaces/DIGITAL/pages/467108880/eInvoicing+in+Cyprus) | Review scope |
| **Czechia** | CZ | Reception only | No general mandate | Verify | Verify | No | No | No | Documented | Verify | No | ISDOC; UBL 2.1/EDIFACT options | [Commission factsheet](https://ec.europa.eu/digital-building-blocks/sites/spaces/DIGITAL/pages/467108881/eInvoicing+in+Czech+Republic) | Review scope |
| **Denmark** | DK | Partial | No general mandate | Documented | Verify | No | No | No | Documented | Verify | No | OIOUBL national UBL profile | [Commission factsheet](https://ec.europa.eu/digital-building-blocks/sites/spaces/DIGITAL/pages/467108882/eInvoicing+in+Denmark) | Review scope |
| **Estonia** | EE | Reception only | No general mandate | Verify | Verify | No | No | No | Documented | Verify | No | EN 16931/Peppol; national XML option | [Commission factsheet](https://ec.europa.eu/digital-building-blocks/sites/spaces/DIGITAL/pages/467108883/eInvoicing+in+Estonia) | Review scope |
| **Finland** | FI | Partial | No general mandate | Verify | Verify | No | No | No | Documented | Verify | No | Finvoice 3.0; TEAPPSXML 3.0; UBL/CII/Peppol | [Commission factsheet](https://ec.europa.eu/digital-building-blocks/sites/spaces/DIGITAL/pages/467108884/eInvoicing+in+Finland) | Review scope |
| **France** | FR | Mandatory | Planned | Documented | Documented | No | No | No | Documented | Documented | No | French CIUS over UBL/CII | [Commission factsheet](https://ec.europa.eu/digital-building-blocks/sites/spaces/DIGITAL/pages/467108885/eInvoicing+in+France) | Review scope |
| **Germany** | DE | Mandatory | Reception / planned issuance | Documented | Documented | Current | Current | Current | Documented | Documented | No | XRechnung CIUS over UBL/CII | [Commission factsheet](https://ec.europa.eu/digital-building-blocks/sites/spaces/DIGITAL/pages/467108886/eInvoicing+in+Germany) | Review scope |
| **Greece** | GR | Partial | Planned | Verify | Verify | No | No | No | Documented | Verify | No | Peppol BIS Billing 3.0 over UBL | [Commission factsheet](https://ec.europa.eu/digital-building-blocks/sites/spaces/DIGITAL/pages/467108887/eInvoicing+in+Greece) | Review scope |
| **Hungary** | HU | Reception only | Reporting / no general mandate | Verify | Verify | No | No | No | Documented | Verify | No | Peppol/structured exchange; NAV XML is reporting | [Commission factsheet](https://ec.europa.eu/digital-building-blocks/sites/spaces/DIGITAL/pages/467108888/eInvoicing+in+Hungary) | Review scope |
| **Ireland** | IE | Reception only | No general mandate | Verify | Verify | No | No | No | Documented | Unknown | No | Peppol BIS Billing 3.0 over UBL/CII | [Commission factsheet](https://ec.europa.eu/digital-building-blocks/sites/spaces/DIGITAL/pages/467108889/eInvoicing+in+Ireland) | Review scope |
| **Italy** | IT | Mandatory | Mandatory | Verify | Verify | No | No | No | Documented | Unknown | No | FatturaPA national XML syntax | [Commission factsheet](https://ec.europa.eu/digital-building-blocks/sites/spaces/DIGITAL/pages/467108890/eInvoicing+in+Italy) | Review scope |
| **Latvia** | LV | Partial | Planned | Documented | Verify | No | No | No | Documented | Verify | No | EN 16931/Peppol structured exchange | [Commission factsheet](https://ec.europa.eu/digital-building-blocks/sites/spaces/DIGITAL/pages/467108891/eInvoicing+in+Latvia) | Review scope |
| **Lithuania** | LT | Mandatory | Planned | Documented | Verify | No | No | No | Documented | Verify | No | EN 16931/Peppol structured exchange; SABIS platform | [Commission factsheet](https://ec.europa.eu/digital-building-blocks/sites/spaces/DIGITAL/pages/467108892/eInvoicing+in+Lithuania) | Review scope |
| **Luxembourg** | LU | Reception only | No general mandate | Documented | Verify | No | No | No | Documented | Verify | No | Peppol BIS Billing 3.0 over UBL/CII | [Commission factsheet](https://ec.europa.eu/digital-building-blocks/sites/spaces/DIGITAL/pages/467108893/eInvoicing+in+Luxembourg) | Review scope |
| **Malta** | MT | Reception only | No general mandate | Verify | Verify | No | No | No | Documented | Unknown | No | Peppol BIS Billing 3.0 over UBL | [Commission factsheet](https://ec.europa.eu/digital-building-blocks/sites/spaces/DIGITAL/pages/467108894/eInvoicing+in+Malta) | Review scope |
| **Netherlands** | NL | Partial | No general mandate | Documented | Verify | No | No | No | Documented | Verify | No | NLCIUS / UBL-OHNL / SI-UBL | [Commission factsheet](https://ec.europa.eu/digital-building-blocks/sites/spaces/DIGITAL/pages/467108895/eInvoicing+in+The+Netherlands) | Review scope |
| **Poland** | PL | Partial | Planned | Verify | Verify | No | No | No | Documented | Unknown | No | Peppol BIS with Polish extensions | [Commission factsheet](https://ec.europa.eu/digital-building-blocks/sites/spaces/DIGITAL/pages/467108896/eInvoicing+in+Poland) | Review scope |
| **Portugal** | PT | Partial | Planned | Documented | Verify | No | No | No | Documented | Verify | No | CIUS-PT over UBL/CII | [Commission factsheet](https://ec.europa.eu/digital-building-blocks/sites/spaces/DIGITAL/pages/467108897/eInvoicing+in+Portugal) | Review scope |
| **Romania** | RO | Mandatory | Mandatory | Verify | Verify | No | No | No | Documented | Unknown | No | RO_CIUS over UBL/CII XML | [Commission factsheet](https://ec.europa.eu/digital-building-blocks/sites/spaces/DIGITAL/pages/467108898/eInvoicing+in+Romania) | Review scope |
| **Slovakia** | SK | Partial | Planned | Documented | Verify | No | No | No | Documented | Verify | No | UBL/CII; planned Peppol solution | [Commission factsheet](https://ec.europa.eu/digital-building-blocks/sites/spaces/DIGITAL/pages/467108899/eInvoicing+in+Slovakia) | Review scope |
| **Slovenia** | SI | Partial | Planned | Documented | Verify | No | No | No | Documented | Verify | No | e-SLOG 2.0; EN 16931/Peppol interoperability | [Commission factsheet](https://ec.europa.eu/digital-building-blocks/sites/spaces/DIGITAL/pages/467108900/eInvoicing+in+Slovenia) | Review scope |
| **Spain** | ES | Mandatory | Planned | Documented | Verify | No | No | No | Documented | Verify | No | Facturae national XML syntax | [Commission factsheet](https://ec.europa.eu/digital-building-blocks/sites/spaces/DIGITAL/pages/467108901/eInvoicing+in+Spain) | Review scope |
| **Sweden** | SE | Mandatory | No general mandate | Documented | Verify | No | No | No | Documented | Verify | No | Peppol BIS Billing 3.0 over UBL; SFTI guidance | [Commission factsheet](https://ec.europa.eu/digital-building-blocks/sites/spaces/DIGITAL/pages/467108902/eInvoicing+in+Sweden) | Review scope |

## National Projects, Platforms and Exchange Ecosystems

The profile-and-syntax matrix above is not sufficient to describe how an invoice is actually exchanged. The following projects are national formats, portals or exchange ecosystems; they are not additional semantic models and do not replace the applicable legal scope. The table covers all 27 EU member states; `No central platform` and `Decentralised` are explicit operating-model findings, not missing data.

| Country | National format, platform or exchange project | Main role | Structured representation / ecosystem | Source |
|---------|-----------------------------------------------|-----------|---------------------------------------|--------|
| **Austria** | e-Rechnung.gv.at, ebInterface, UBL | Federal B2G submission and invoice formats | ebInterface or UBL; Peppol also relevant | [Commission factsheet](https://ec.europa.eu/digital-building-blocks/sites/spaces/DIGITAL/pages/467108876/eInvoicing+in+Austria) |
| **Belgium** | Mercurius | Public-sector exchange and access point ecosystem | Peppol BIS Billing / UBL | [Commission factsheet](https://ec.europa.eu/digital-building-blocks/sites/spaces/DIGITAL/pages/467108877/eInvoicing+in+Belgium) |
| **Denmark** | NemHandel, OIOUBL | National B2G and business exchange network | OIOUBL and Peppol/XML exchange | [Commission factsheet](https://ec.europa.eu/digital-building-blocks/sites/spaces/DIGITAL/pages/467108882/eInvoicing+in+Denmark) |
| **Finland** | Finvoice, TEAPPSXML | Established business and public-sector e-invoice networks | Finvoice, TEAPPSXML, UBL/CII and Peppol | [Commission factsheet](https://ec.europa.eu/digital-building-blocks/sites/spaces/DIGITAL/pages/467108884/eInvoicing+in+Finland) |
| **France** | Chorus Pro, Factur-X | Public-sector gateway and hybrid e-invoice ecosystem | UBL/CII and PDF/A-3 with embedded CII | [Commission factsheet](https://ec.europa.eu/digital-building-blocks/sites/spaces/DIGITAL/pages/467108885/eInvoicing+in+France) |
| **Germany** | XRechnung, ZRE/OZG-RE, ZUGFeRD | Public-sector submission portals and private-sector hybrid ecosystem | UBL/CII and PDF/A-3 with embedded CII | [Commission factsheet](https://ec.europa.eu/digital-building-blocks/sites/spaces/DIGITAL/pages/467108886/eInvoicing+in+Germany) |
| **Lithuania** | SABIS | Central public-sector e-invoice system | Structured EN 16931 data; Peppol interoperability | [Commission factsheet](https://ec.europa.eu/digital-building-blocks/sites/spaces/DIGITAL/pages/467108892/eInvoicing+in+Lithuania) |
| **Netherlands** | Digipoort, NLCIUS, UBL-OHNL | Public-sector gateway and national profile ecosystem | Peppol BIS, UBL and national UBL profiles | [Commission factsheet](https://ec.europa.eu/digital-building-blocks/sites/spaces/DIGITAL/pages/467108895/eInvoicing+in+The+Netherlands) |
| **Poland** | KSeF, PEF | Tax e-invoice platform and public procurement exchange | KSeF XML, PEF and Peppol BIS | [Commission factsheet](https://ec.europa.eu/digital-building-blocks/sites/spaces/DIGITAL/pages/467108896/eInvoicing+in+Poland) |
| **Romania** | RO e-Factura | Central reporting and e-invoice exchange system | RO_CIUS / structured XML | [Commission factsheet](https://ec.europa.eu/digital-building-blocks/sites/spaces/DIGITAL/pages/467108898/eInvoicing+in+Romania) |
| **Spain** | Facturae, FACe | National invoice format and public-sector gateway | Facturae XML | [Commission factsheet](https://ec.europa.eu/digital-building-blocks/sites/spaces/DIGITAL/pages/467108901/eInvoicing+in+Spain) |
| **Bulgaria** | CAIS EPP | Central eProcurement and tender-related invoice platform | XML, including UBL 2.1 or CII; Peppol interoperability | [Commission factsheet](https://ec.europa.eu/digital-building-blocks/sites/spaces/DIGITAL/pages/467108878/eInvoicing+in+Bulgaria) |
| **Croatia** | Servis eRačun za državu / e-Račun | Central public-sector exchange platform operated by FINA | UBL 2.1 and CII; Peppol interoperability | [Commission factsheet](https://ec.europa.eu/digital-building-blocks/sites/spaces/DIGITAL/pages/467108879/eInvoicing+in+Croatia) |
| **Cyprus** | Cyprus Government Gateway Portal, Peppol Access Points | Centralised B2G access model; supplier submission is voluntary | Peppol BIS Billing 3.0 / UBL | [Commission factsheet](https://ec.europa.eu/digital-building-blocks/sites/spaces/DIGITAL/pages/467108880/eInvoicing+in+Cyprus) |
| **Czechia** | Národní elektronický nástroj (NEN), ISDOC | Public procurement lifecycle platform and national invoice format ecosystem | UBL 2.1, ISDOC and EDIFACT | [Commission factsheet](https://ec.europa.eu/digital-building-blocks/sites/spaces/DIGITAL/pages/467108881/eInvoicing+in+Czech+Republic) |
| **Estonia** | E-arveldaja, RIK, private service providers, Peppol | Decentralised exchange through service providers and roaming agreements | EN 16931, national XML still permitted, and Peppol | [Commission factsheet](https://ec.europa.eu/digital-building-blocks/sites/spaces/DIGITAL/pages/467108883/eInvoicing+in+Estonia) |
| **Greece** | KE.D, Peppol, myDATA | Central interoperability and public-sector routing; tax reporting through myDATA | Peppol BIS Billing 3.0 / UBL | [Commission factsheet](https://ec.europa.eu/digital-building-blocks/sites/spaces/DIGITAL/pages/467108887/eInvoicing+in+Greece) |
| **Hungary** | NAV Online Invoicing System | Central tax reporting and invoice-data platform | NAV XML; EN 16931-compatible structured data where used | [Commission factsheet](https://ec.europa.eu/digital-building-blocks/sites/spaces/DIGITAL/pages/467108888/eInvoicing+in+Hungary) |
| **Ireland** | Peppol Authority, NSSO and shared-service systems | Peppol exchange and sector-specific public financial shared services | Peppol BIS Billing 3.0, UBL and CII | [Commission factsheet](https://ec.europa.eu/digital-building-blocks/sites/spaces/DIGITAL/pages/467108889/eInvoicing+in+Ireland) |
| **Italy** | Sistema di Interscambio (SDI), FatturaPA | Central clearance, validation and routing platform | FatturaPA XML aligned with EN 16931 | [Commission factsheet](https://ec.europa.eu/digital-building-blocks/sites/spaces/DIGITAL/pages/467108890/eInvoicing+in+Italy) |
| **Latvia** | eAddress, VDAA, VID | Decentralised public-sector exchange and tax-data submission model | EN 16931 structured data; Peppol and commercial operators | [Commission factsheet](https://ec.europa.eu/digital-building-blocks/sites/spaces/DIGITAL/pages/467108891/eInvoicing+in+Latvia) |
| **Luxembourg** | Peppol, central government access point, SIGI, guichet.lu | Decentralised B2G exchange with central and municipal access points | Peppol BIS Billing 3.0 / UBL or CII | [Commission factsheet](https://ec.europa.eu/digital-building-blocks/sites/spaces/DIGITAL/pages/467108893/eInvoicing+in+Luxembourg) |
| **Malta** | Peppol eDelivery network | No central eInvoicing platform; exchange through Peppol-compatible software and access points | Peppol BIS Billing 3.0 / UBL | [Commission factsheet](https://ec.europa.eu/digital-building-blocks/sites/spaces/DIGITAL/pages/467108894/eInvoicing+in+Malta) |
| **Portugal** | Portal BASE, FE-AP, eSPap, CIUS-PT | Public procurement gateway and public administration invoice services | UBL/CII CIUS-PT; XML and SAF-T for tax reporting | [Commission factsheet](https://ec.europa.eu/digital-building-blocks/sites/spaces/DIGITAL/pages/467108897/eInvoicing+in+Portugal) |
| **Slovakia** | IS EFA, planned Peppol-based national solution | Current public-sector platform transitioning to a Peppol-based model | UBL 2.1 or CII; Peppol planned for the new model | [Commission factsheet](https://ec.europa.eu/digital-building-blocks/sites/spaces/DIGITAL/pages/467108899/eInvoicing+in+Slovakia) |
| **Slovenia** | PPA eInvoicing system, e-SLOG, Exchange Hub, BizBox | Central public-sector gateway with provider and bank-network access | e-SLOG 2.0, EN 16931 and Peppol interoperability | [Commission factsheet](https://ec.europa.eu/digital-building-blocks/sites/spaces/DIGITAL/pages/467108900/eInvoicing+in+Slovenia) |
| **Sweden** | SFTI, Peppol, provider access points | Decentralised public-sector exchange without a central platform | Peppol BIS Billing 3.0 / UBL | [Commission factsheet](https://ec.europa.eu/digital-building-blocks/sites/spaces/DIGITAL/pages/467108902/eInvoicing+in+Sweden) |

The table is intentionally separate from the syntax columns: `Peppol BIS` describes a profile and exchange ecosystem, `UBL` or `CII` describes an XML syntax, and a project such as `Mercurius`, `KSeF` or `FACe` describes a national gateway or operating system. These terms must not be counted as interchangeable invoice formats.

## File Representation

The hybrid column does not mean that all other countries send only XML or that PDF is prohibited. A structured XML invoice can be sent alone, accompanied by a separate PDF copy or attachment, or packaged as PDF/A-3 with embedded CII XML. The complete representation matrix and validation consequences are documented in the [validation layer matrix](../validation/README.md#invoice-representation-and-the-role-of-pdf).

## Status Legend

- `Mandatory` = current legal obligation for the stated scope according to the factsheet; `Partial` = limited entities, transactions, or phased scope.
- `Reception only` = public bodies must be able to receive/process eInvoices, without a general supplier issuance obligation being documented.
- `Planned` = announced or phased future measure; `No general mandate` = no general B2B issuance mandate documented in the factsheet.
- `Documented` = the factsheet or referenced national specification identifies the option; `Profile-dependent` = the option depends on the selected profile; `Unknown` and `Verify` deliberately avoid unsupported technical claims.

---

## Country Detail Files

The individual country files are reserved for evidence and implementation notes. The matrix above is the current cross-country summary; country-specific claims should be added only with a source link and an explicit B2G/B2B scope.