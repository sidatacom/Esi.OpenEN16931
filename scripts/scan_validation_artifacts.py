#!/usr/bin/env python3
"""
Simple scanner to enumerate validation artefacts in the workspace.

Usage:
  python scripts/scan_validation_artifacts.py --root . --out docs/validation/scan_output.md

Options:
  --root PATH    Root to search (default: current directory)
  --out PATH     Output markdown file
  --exts LIST    Comma-separated extensions to look for (default: .sch,.xsd,.xslt,.csproj)
  --verbose      Print progress

This script is intentionally small and dependency-free so maintainers can run it
without additional setup. For full GitHub ownership checks, provide a GITHUB_TOKEN
and extend the script.
"""
from pathlib import Path
import argparse
import sys
import json


def find_files(root: Path, exts):
    for p in root.rglob("*"):
        if p.is_file() and any(p.name.lower().endswith(ext) for ext in exts):
            yield p


def main():
    ap = argparse.ArgumentParser()
    ap.add_argument("--root", default=".", help="Root folder to search")
    ap.add_argument("--out", default="docs/validation/scan_output.md")
    ap.add_argument("--exts", default=".sch,.xsd,.xslt,.csproj,.xml,.xsl",
                    help="Comma-separated extensions to look for")
    ap.add_argument("--verbose", action="store_true")
    args = ap.parse_args()

    root = Path(args.root).resolve()
    out = Path(args.out)
    exts = [e.strip().lower() for e in args.exts.split(",") if e.strip()]

    results = {}
    if args.verbose:
        print(f"Scanning {root} for {exts}")

    for f in find_files(root, exts):
        rel = f.relative_to(root)
        # project key: first two path components if possible
        parts = rel.parts
        if len(parts) >= 2:
            project = f"{parts[0]}/{parts[1]}"
        else:
            project = parts[0]
        results.setdefault(project, []).append(str(rel))

    # write markdown
    out.parent.mkdir(parents=True, exist_ok=True)
    with out.open("w", encoding="utf-8") as fh:
        fh.write("# Validation Scan Output\n\n")
        fh.write(f"Root: {root}\n\n")
        fh.write(f"Extensions: {', '.join(exts)}\n\n")
        fh.write(f"Total projects found: {len(results)}\n\n")
        for proj, files in sorted(results.items(), key=lambda x: -len(x[1])):
            fh.write(f"- {proj} — {len(files)} hits\n")
            for p in files:
                fh.write(f"  - {p}\n")

    # also dump a compact json for further processing
    json_out = out.with_suffix('.json')
    with json_out.open('w', encoding='utf-8') as jh:
        json.dump(results, jh, ensure_ascii=False, indent=2)

    if args.verbose:
        print(f"Wrote {out} and {json_out}")


if __name__ == '__main__':
    main()
