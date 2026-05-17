$ErrorActionPreference = "Stop"

$scriptRoot = Split-Path -Parent $MyInvocation.MyCommand.Path
$repoRoot = Get-Location

Write-Host "Applying L2 agreement exchange draft/rules/naming sync..." -ForegroundColor Cyan

# Copy new/replacement files.
$paths = @(
  "planning/slices/SL-AGR-EXCH-001-start-agreement-exchange-with-initial-employee-proposal.md",
  "planning/slices/drafting-current-state-source-rule.md",
  "planning/slices/drafting-implementation-checklist-rule.md",
  "planning/slices/slice-draft-file-naming-and-placement.md"
)

foreach ($rel in $paths) {
  $src = Join-Path $scriptRoot $rel
  $dst = Join-Path $repoRoot $rel
  New-Item -ItemType Directory -Force -Path (Split-Path -Parent $dst) | Out-Null
  Copy-Item -Force $src $dst
}

python "$scriptRoot\tools\sync_l2_agr_exch_001_docs.py" "$repoRoot"

Write-Host "Done. Review with: git diff -- planning" -ForegroundColor Green
