#!/usr/bin/env bash
set -euo pipefail

old_layout="energymanagement.client/src/Components/Layout"

if [ -d "$old_layout" ]; then
  rm -rf "$old_layout"
  echo "Removed $old_layout"
else
  echo "$old_layout does not exist; nothing to remove"
fi

echo "Layout cleanup files are in place."
echo "Recommended checks:"
echo "  npm --prefix energymanagement.client run build"
echo "  npm --prefix energymanagement.client run test"
echo "  npm run check:api"
echo "  npm run test:e2e -- --list"
echo "  npm run test:e2e"
