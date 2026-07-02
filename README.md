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

## Extended ecosystem catalog — Submodules (104)
This list is generated directly from the repository's `.gitmodules` file and reflects the upstream repositories included as git submodules under the `origins/` directory. The list is presented in the order found in `.gitmodules` and deduplicated.
MASTER PROJECT LIST (1-104)
1. Securibox/facturx
2. koozala/pacioli
3. Docentric/docentric-e-invoice-validator
4. BartVertongen/Peppol.NETCoreLib
5. BartVertongen/UBL21.NETCoreLib
6. itext/i7n-pdfinvoice
7. ipax77/pax.XRechnung.NET
8. stephanstapel/ZUGFeRD-csharp
9. itplr-kosit/validator
10. itplr-kosit/validator-configuration-xrechnung
11. ConnectingEurope/eInvoicing-EN16931
12. gflohr/e-invoice-eu
13. akretion/factur-x
14. alyf-de/eu_einvoice
15. xSentry/xrechnungs-generator
16. phax/en16931-visualization
17. klst-de/e-invoice
18. ZUGFeRD/mustangproject
19. horstoeko/zugferd
20. easybill/e-invoicing
21. josemmo/einvoicing
22. easybill/e-invoice-validator
23. backoffice-plus/e-invoice-validator
24. phax/phase4
25. phax/phoss-smp
26. pretix/python-drafthorse
27. jcthiele/OpenXRechnungToolbox
28. atgp/factur-x
29. phax/ph-ubl
30. num-num/ubl-invoice
31. itplr-kosit/xrechnung-visualization
32. phax/phase2
33. easybill/zugferd-php
34. OpenIndex/ZUGFeRD-Manager
35. itplr-kosit/xrechnung-testsuite
36. tiehfood/xpferd
37. jslno/node-zugferd
38. ZUGFeRD/corpus
39. itplr-kosit/xrechnung-schematron
40. phax/phive
41. phax/phive-rules
42. phax/peppol-commons
43. OxalisCommunity/oxalis
44. horstoeko/zugferd-laravel
45. phax/phoss-directory
46. horstoeko/zugferdvisualizer
47. ZUGFeRD/ZUV
48. LandrixSoftware/XRechnung-for-Delphi
49. recommand/recommand-peppol
50. phax/en16931-cii2ubl
51. konik-io/konik
52. ZUGFeRD/quba-viewer
53. bitbetterde/paperless-ngx-erechnung
54. zfutura/pycheval
55. esvit/einvoicing
56. digineo/xrechnung
57. horstoeko/zugferdublbridge
58. drbrnn/XFakturist
59. stafyniaksacha/facturx
60. lka/excel2zugferd
61. speedata/einvoice
62. SimonWaldherr/InvoiceInspector
63. LandrixSoftware/validator-configuration-zugferd
64. Tiime-Software/Factur-X
65. kyr0/easy-erechnung
66. horstoeko/zugferdmail
67. itplr-kosit/validator-configuration-bis
68. austriapro/ebinterface-standards
69. austriapro/ebinterface-ubl-mapping
70. austriapro/ebinterface-xrechnung-mapping
71. jcthiele/xrechnung-visualization-codelist-resolve
72. markusbegerow/zugpferd-xrechnung-peppol-generator
73. horstoeko/invoicesuite
74. InvoiceXML/facturx-api-examples
75. AlexZeitler/zugpferd
76. armin11/xrechnung_light
77. Youniwemi/digital-invoice
78. svanteschubert/Saxon-HE-enhanced-accuracy
79. CenPC434/java-tools
80. holodeck-b2b/Holodeck-SMP
81. OxalisCommunity/vefa-peppol
82. horstoeko/ubl
83. Selia-AI/peppol-bis-3-typescript
84. vartur/facturelibre
85. pikaju/js-e-invoice-codes
86. BSchneppe/einvoice-rs
87. hydrogen602/zugferd-code-lists
88. valitoolorg/zebra
89. easybill/en16931-validator
90. easybill/peppol-bis-billing-validator
91. VartikaG02/en16931-ubl2cii
92. phax/en16931-registry
93. billingcat/crm
94. microscaler/rerp
95. NikolaiMe/factur-x-kit
96. Mavengence/einvoice-mcp
97. LASTRADA-Software/XRechnung
98. inbridgeio/open-invoice-format
99. ZUGFeRD/einvoice-anonymizer
100. ZUGFeRD/REST-Converter
101. mahdiabderraouf/facturx-php
102. stannapp/factur-x-php
103. LandrixSoftware/ZUGFeRD-for-Delphi
104. facturx-engine/facturx-engine

Next steps
- The raw machine-readable list is `docs/external-projects-repos.txt` if you want to re-run or edit the list.
- To add these projects as git submodules under `origins/`, run the helper script:

```powershell
.\scripts\add_submodules.ps1 -Commit
```

The script will attempt shallow clones first and fall back to a full clone when necessary. If any repositories fail to add, the script will list them for manual retry.

## Documentation & Country Profiles

We maintain per-country documentation under [docs/countries/](docs/countries/). The structure is intended to scale to all European countries, with the first country folders created for Germany and Austria.

See [docs/countries/README.md](docs/countries/README.md) for the full country index and rollout plan.

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
