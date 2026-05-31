# Contributing to Esi.OpenEN16931

Thank you for your interest in contributing to **Esi.OpenEN16931**! This document provides guidelines to help you get started.

---

## Table of Contents

1. [Code of Conduct](#code-of-conduct)
2. [How to Contribute](#how-to-contribute)
3. [Reporting Issues](#reporting-issues)
4. [Proposing New Submodules](#proposing-new-submodules)
5. [Documentation Contributions](#documentation-contributions)
6. [Pull Request Process](#pull-request-process)
7. [Style Guidelines](#style-guidelines)
8. [License](#license)

---

## Code of Conduct

This project follows the [Contributor Covenant Code of Conduct](https://www.contributor-covenant.org/version/2/1/code_of_conduct/). By participating, you agree to uphold a respectful and inclusive environment for everyone.

---

## How to Contribute

There are several ways to contribute:

- **Report bugs or issues** via GitHub Issues
- **Suggest new submodules** to include in `origins/`
- **Improve documentation** (README, CONTRIBUTING, NOTICE, guides)
- **Add or improve tooling** (build scripts, CI workflows, utilities)
- **Open discussions** about the project direction, architecture, or roadmap

---

## Reporting Issues

Before opening an issue, please:

1. Search existing issues to avoid duplicates
2. Include a clear title and description
3. Provide steps to reproduce (if applicable)
4. Mention the relevant standard (EN 16931, XRechnung, Factur-X, ZUGFeRD, UBL, CII, PEPPOL) if the issue is standard-specific

Open an issue at: [https://github.com/sidatacom/Esi.OpenEN16931/issues](https://github.com/sidatacom/Esi.OpenEN16931/issues)

---

## Proposing New Submodules

The `origins/` directory contains Git submodules pointing to relevant upstream open-source projects. To propose adding a new submodule:

1. Open an issue titled **"[Submodule] Add `<repository-name>`"**
2. In the issue body, include:
   - **Repository URL**: e.g., `https://github.com/org/repo`
   - **Description**: what the project does and why it is relevant to EN 16931 or European e-invoicing
   - **License**: the SPDX license identifier of the upstream project (e.g., `Apache-2.0`, `MIT`, `GPL-3.0`)
   - **Maintenance status**: is the upstream project actively maintained?
   - **Relevance**: which standards or profiles does it support?

Criteria for acceptance:
- The project must be directly relevant to EN 16931 or a recognized European e-invoicing standard
- It must be open source with a clearly stated license
- It must be hosted on a publicly accessible Git hosting platform

---

## Documentation Contributions

Documentation improvements are always welcome. This includes:

- Fixing typos, grammar, or formatting in `README.md`, `CONTRIBUTING.md`, or `NOTICE.md`
- Adding examples or usage instructions
- Improving explanations of how standards relate to each other
- Translating documentation (though English is the primary language)

For small documentation fixes, you can open a pull request directly without creating an issue first.

---

## Pull Request Process

1. **Fork** the repository and create a branch from `main`:
   ```bash
   git checkout -b feature/my-contribution
   ```

2. **Make your changes**, keeping them focused and atomic.

3. **Update documentation** if your change affects the README, NOTICE, or other docs.

4. **Commit** with a clear and descriptive message:
   ```bash
   git commit -m "Add submodule: horstoeko/zugferd-laravel"
   ```

5. **Open a Pull Request** against the `main` branch with:
   - A clear title
   - A description of what was changed and why
   - Reference to any related issue (e.g., `Closes #42`)

6. A maintainer will review your PR. Please respond to feedback promptly.

---

## Style Guidelines

- **Language**: All documentation and commit messages must be in **English**
- **Markdown**: Use standard GitHub-flavored Markdown; keep line lengths reasonable
- **Commit messages**: Use the imperative mood (e.g., "Add submodule X", "Fix typo in README")
- **Submodule names**: Use the upstream repository name as the submodule directory name where possible; disambiguate with the organization name suffix (e.g., `e-invoice-validator-easybill`) if there is a naming collision

---

## License

By contributing to this project, you agree that your contributions will be licensed under the **Apache License 2.0**. See [LICENSE](LICENSE) for the full text.

Note: Contributions to the `origins/` submodules are governed by the respective upstream project licenses, not by this project's license.
