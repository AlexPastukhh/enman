#!/usr/bin/env bash
set -euo pipefail

repo_root="${1:-$(pwd)}"
script_dir="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"

echo "Applying L1-MY-REQUEST-DETAILS.client replacement into ${repo_root}"
mkdir -p "${repo_root}/planning/slices/l1"
cp -f "${script_dir}/planning/slices/l1/L1-MY-REQUEST-DETAILS.client.md" "${repo_root}/planning/slices/l1/"
echo "Done. Updated planning/slices/l1/L1-MY-REQUEST-DETAILS.client.md"
