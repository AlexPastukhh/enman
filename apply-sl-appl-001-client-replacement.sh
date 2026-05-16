#!/usr/bin/env bash
set -euo pipefail

echo "Applying SL-APPL-001.client replacement..."

target="planning/slices/SL-APPL-001-create-individual-applicant-party.client.md"

if [ ! -f "$target" ]; then
  echo "Warning: target file does not exist yet and will be created: $target"
fi

echo "Replacement file is already placed at: $target"
echo "Run git diff to review changes."
