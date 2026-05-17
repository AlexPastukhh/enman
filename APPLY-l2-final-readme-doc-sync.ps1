# L2 final README / docs sync
# Run from repository root.

$ErrorActionPreference = "Stop"

Write-Host "Applying L2 final README/docs sync..."

# Remove superseded agreement placeholder files only when canonical replacements exist.
$oldFiles = @(
  "planning/slices/SL-AGR-EXCH-003-read-agreement-exchange.md",
  "planning/slices/SL-AGR-EXCH-004-accept-active-agreement-proposal.md",
  "planning/slices/SL-AGR-EXCH-005-final-refuse-agreement-exchange.md"
)

$canonicalFiles = @(
  "planning/slices/SL-AGR-EXCH-003-agreement-exchange-list-read.md",
  "planning/slices/SL-AGR-EXCH-004-agreement-exchange-details-read.md",
  "planning/slices/SL-AGR-EXCH-005-client-accept-active-agreement-proposal.md",
  "planning/slices/SL-AGR-EXCH-006-final-refuse-agreement-exchange.md"
)

$canClean = $true
foreach ($file in $canonicalFiles) {
  if (-not (Test-Path $file)) {
    $canClean = $false
  }
}

if ($canClean) {
  foreach ($file in $oldFiles) {
    if (Test-Path $file) {
      Remove-Item $file
      Write-Host "Removed superseded file: $file"
    }
  }
} else {
  Write-Host "Skipped superseded file cleanup because not all canonical replacement files exist locally."
}

Write-Host "L2 final README/docs sync applied."
Write-Host "Recommended checks:"
Write-Host "  git status"
Write-Host "  git diff -- planning"
Write-Host "  git grep -n 'SL-AGR-EXCH-003.*Read Agreement Exchange'"
Write-Host "  git grep -n 'DocumentFileRef'"
Write-Host "  git grep -n 'EmployeeRef'"
Write-Host "  git grep -n 'ResponsibleEmployeeId'"
