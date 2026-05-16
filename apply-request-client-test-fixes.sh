#!/usr/bin/env bash
set -euo pipefail
SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
cp -R "$SCRIPT_DIR/energymanagement.client" .
cp -R "$SCRIPT_DIR/tests" .
echo "Applied request client test fixes."
