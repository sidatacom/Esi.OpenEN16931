# Validation Mapping (vorläufig)

Stand: 2026-07-02

Dieses Dokument enthält ein vorläufiges Mapping der im Workspace vorhandenen Origin‑Projekte zu den von ihnen verwendeten Validierungsarten und Regelquellen. Einträge sind initial automatisiert / heuristisch erstellt (Repo‑Namen, README‑Hinweise, gefundene Artefakte wie `.xsd`, Schematron, `validator-configuration*`). Die C#‑Projekte stehen am Anfang der Liste. Bitte Maintainer/Code‑Owner bestätigen und ergänzen.

## Legende
- Validation types: XSD = Schema/structural, Schematron = business rules, PDF = PDF/A or embedded XML checks (Factur‑X/ZUGFeRD), Sign = XML signature checks, Config = validator configuration packages
- Rule sources: offizielle Spezifikationen oder Profile (EN16931, XRechnung, Peppol BIS, UBL, Factur‑X/ZUGFeRD, Projekt‑Schematron)

## Vorläufiges Mapping

| Projekt | Workspace‑Pfad | Validierungsarten | Regelquellen | Maintainer / Owner | Hinweise |
|---|---:|---|---|---|---|
| BartVertongen / UBL21.NETCoreLib | origins/BartVertongen/UBL21.NETCoreLib | XSD (UBL libraries) | UBL 2.1 schemas | BartVertongen | UBL schema helpers
| BartVertongen / Peppol.NETCoreLib | origins/BartVertongen/Peppol.NETCoreLib | XSD / Peppol helpers | Peppol BIS | BartVertongen | 
| Docentric / docentric‑e‑invoice‑validator | origins/Docentric/docentric-e-invoice-validator | Validator engine, business checks | EN16931 / XRechnung | Docentric | kommerzielle/Enterprise‑Komponenten
| itext / i7n‑pdfinvoice | origins/itext/i7n-pdfinvoice | PDF validation, PDF/A | PDF/A, Factur‑X | itext | PDF‑Werkzeuge
| itplr‑kosit / validator | origins/itplr-kosit/validator | Engine, Schematron, XSD orchestration | XRechnung Schematron, Peppol configs, EN16931 | itplr-kosit | zentrale Java/Validator‑Engine, viele Konfigurationen
| itplr‑kosit / validator‑configuration‑xrechnung | origins/itplr-kosit/validator-configuration-xrechnung | Config (Schematron sets) | XRechnung, EN16931 | itplr-kosit | Regeln für XRechnung‑Profile
| itplr‑kosit / xrechnung‑schematron | origins/itplr-kosit/xrechnung-schematron | Schematron | XRechnung (offizielle Schematron‑Regeln) | itplr-kosit | primäre Schematron‑Regelsammlung
| easybill / en16931‑validator | origins/easybill/en16931-validator | Schematron, XSD | EN16931 profiles | easybill | EN16931‑orientiert
| easybill / e‑invoice‑validator | origins/easybill/e-invoice-validator | XSD, business checks | EN16931, länderspezifische Regeln | easybill | 
| backoffice‑plus / e‑invoice‑validator | origins/backoffice-plus/e-invoice-validator | XSD, Schematron | EN16931 / country profiles | backoffice-plus | 
| digineo / xrechnung | origins/digineo/xrechnung | Schematron, XSD | XRechnung, EN16931 | digineo | 
| atgp / factur‑x | origins/atgp/factur-x | PDF/A (PDF/A‑3), embedded XML, packaging | Factur‑X / ZUGFeRD profiles | atgp | Factur‑X validator/checks
| LandrixSoftware / validator‑configuration‑zugferd | origins/LandrixSoftware/validator-configuration-zugferd | Configs, PDF/XML checks | ZUGFeRD / Factur‑X | LandrixSoftware | 
| horstoeko / zugferd | origins/horstoeko/zugferd | PDF/A + embedded XML validation (ZUGFeRD) | ZUGFeRD profiles | horstoeko | PDF‑erste Prüfungen
| ZUGFeRD / mustangproject | origins/ZUGFeRD/mustangproject | PDF/A + embedded XML | ZUGFeRD / Factur‑X | ZUGFeRD | verbreiteter Factur‑X Validator
| phax / en16931‑visualization | origins/phax/en16931-visualization | Mapping, codelist checks, visualization | EN16931, CEN codelists | phax | visualisierung / Referenzdaten
| phax / en16931‑registry | origins/phax/en16931-registry | Registry / metadata | EN16931 | phax | unterstützende Dienste
| OpenPEPPOL / peppol‑bis‑invoice‑3 | origins/OpenPEPPOL/peppol-bis-invoice-3 | XSDs, BIS rules | Peppol BIS 3 | OpenPEPPOL | normative XSDs und Beispiele
| stannapp / factur‑x‑php | origins/stannapp/factur-x-php | PHP Factur‑X checks (PDF/XML) | Factur‑X | stannapp | 
| stafyniaksacha / facturx | origins/stafyniaksacha/facturx | Factur‑X helpers | Factur‑X | stafyniaksacha | 
| num‑num / ubl‑invoice | origins/num-num/ubl-invoice | UBL helpers / XSD usage | UBL | num-num | 

## Methodik / Anmerkungen
- Dieses Mapping ist ein erster, manueller Entwurf basierend auf vorhandenen Repo‑Namen, README‑Hinweisen und bekannten Artefakten. Es ist nicht vollständig und kann falsch positive/negative Treffer enthalten.
- Empfohlen: automatisches Scan‑Skript ausführen (suche nach `*.xsd`, `*.sch|schematron`, `validator-configuration*`, `README` mit ‚EN16931|XRechnung|Factur|ZUGFeRD|Peppol‘) und Ergebnisse als CSV/MD vorbefüllen.
- Nächste Schritte: Maintainer ansprechen, `docs/validation/README.md` mit Struktur & Vorgehen anlegen, Scanner skripten und Mapping verifizieren.

----

Wenn du willst, führe ich jetzt den Scan automatisiert aus und fülle die Tabelle fein granular mit Pfaden und Belegen (z.B. welche `.xsd`/`.sch`‑Datei die Zuordnung begründet).
