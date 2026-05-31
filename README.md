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
1. itplr-kosit/validator
2. itplr-kosit/validator-configuration-xrechnung
3. ConnectingEurope/eInvoicing-EN16931
4. gflohr/e-invoice-eu
5. akretion/factur-x
6. alyf-de/eu_einvoice
7. xSentry/xrechnungs-generator
8. phax/en16931-visualization
9. klst-de/e-invoice
10. ZUGFeRD/mustangproject
11. Securibox/facturx
12. horstoeko/zugferd
13. easybill/e-invoicing
14. josemmo/einvoicing
15. easybill/e-invoice-validator
16. backoffice-plus/e-invoice-validator
17. phax/phase4
18. phax/phoss-smp
19. pretix/python-drafthorse
20. jcthiele/OpenXRechnungToolbox
21. atgp/factur-x
22. phax/ph-ubl
23. num-num/ubl-invoice
24. itplr-kosit/xrechnung-visualization
25. phax/phase2
26. easybill/zugferd-php
27. OpenIndex/ZUGFeRD-Manager
28. itplr-kosit/xrechnung-testsuite
29. tiehfood/xpferd
30. jslno/node-zugferd
31. ZUGFeRD/corpus
32. itplr-kosit/xrechnung-schematron
33. phax/phive
34. phax/phive-rules
35. phax/peppol-commons
36. OxalisCommunity/oxalis
37. horstoeko/zugferd-laravel
38. phax/phoss-directory
39. horstoeko/zugferdvisualizer
40. ZUGFeRD/ZUV
41. LandrixSoftware/XRechnung-for-Delphi
42. recommand/recommand-peppol
43. phax/en16931-cii2ubl
44. konik-io/konik
45. ZUGFeRD/quba-viewer
46. bitbetterde/paperless-ngx-erechnung
47. zfutura/pycheval
48. esvit/einvoicing
49. digineo/xrechnung
50. horstoeko/zugferdublbridge
51. drbrnn/XFakturist
52. stafyniaksacha/facturx
53. lka/excel2zugferd
54. speedata/einvoice
55. SimonWaldherr/InvoiceInspector
56. LandrixSoftware/validator-configuration-zugferd
57. Tiime-Software/Factur-X
58. kyr0/easy-erechnung
59. koozala/pacioli
60. horstoeko/zugferdmail
61. itplr-kosit/validator-configuration-bis
62. austriapro/ebinterface-standards
63. austriapro/ebinterface-ubl-mapping
64. austriapro/ebinterface-xrechnung-mapping
65. jcthiele/xrechnung-visualization-codelist-resolve
66. markusbegerow/zugpferd-xrechnung-peppol-generator
67. horstoeko/invoicesuite
68. InvoiceXML/facturx-api-examples
69. AlexZeitler/zugpferd
70. Docentric/docentric-e-invoice-validator
71. armin11/xrechnung_light
72. Youniwemi/digital-invoice
73. svanteschubert/Saxon-HE-enhanced-accuracy
74. CenPC434/java-tools
75. BartVertongen/Peppol.NETCoreLib
76. BartVertongen/UBL21.NETCoreLib
77. holodeck-b2b/Holodeck-SMP
78. OxalisCommunity/vefa-peppol
79. horstoeko/ubl
80. Selia-AI/peppol-bis-3-typescript
81. vartur/facturelibre
82. pikaju/js-e-invoice-codes
83. BSchneppe/einvoice-rs
84. hydrogen602/zugferd-code-lists
85. valitoolorg/zebra
86. easybill/en16931-validator
87. easybill/peppol-bis-billing-validator
88. VartikaG02/en16931-ubl2cii
89. phax/en16931-registry
90. itext/i7n-pdfinvoice
91. billingcat/crm
92. microscaler/rerp
93. NikolaiMe/factur-x-kit
94. Mavengence/einvoice-mcp
95. ipax77/pax.XRechnung.NET
96. LASTRADA-Software/XRechnung
97. inbridgeio/open-invoice-format
98. ZUGFeRD/einvoice-anonymizer
99. ZUGFeRD/REST-Converter
100. mahdiabderraouf/facturx-php
101. stannapp/factur-x-php
102. LandrixSoftware/ZUGFeRD-for-Delphi
103. facturx-engine/facturx-engine
104. stephanstapel/ZUGFeRD-csharp

Next steps
- The raw machine-readable list is `docs/external-projects-repos.txt` if you want to re-run or edit the list.
- To add these projects as git submodules under `origins/`, run the helper script:

```powershell
.\scripts\add_submodules.ps1 -Commit
```

The script will attempt shallow clones first and fall back to a full clone when necessary. If any repositories fail to add, the script will list them for manual retry.

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
