#!/usr/bin/env bash
set -euo pipefail

SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
REPO_ROOT="$(pwd)"

mkdir -p "$REPO_ROOT/planning/slices/l1"
cp "$SCRIPT_DIR/planning/slices/l1/L1-MY-REQUESTS-LIST-FILTERS.client.md" \
   "$REPO_ROOT/planning/slices/l1/L1-MY-REQUESTS-LIST-FILTERS.client.md"

echo "Applied L1-MY-REQUESTS-LIST-FILTERS.client.md"
