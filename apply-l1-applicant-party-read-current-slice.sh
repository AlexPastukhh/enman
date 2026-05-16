#!/usr/bin/env bash
set -euo pipefail

for file in \
  "planning/slices/l1/L1-APPLICANT-PARTY-READ-CURRENT.md" \
  "planning/slices/l1/L1-APPLICANT-PARTY-READ-CURRENT.client.md"; do
  test -f "$file" || { echo "Expected file missing: $file" >&2; exit 1; }
  echo " - $file"
done
