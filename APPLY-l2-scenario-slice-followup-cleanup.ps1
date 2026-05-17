$ErrorActionPreference = "Stop"

Write-Host "L2 scenario/slice follow-up cleanup files have been extracted." -ForegroundColor Green

# Remove superseded agreement slice files only when the canonical replacements exist locally.
$canonicalList = "planning/slices/SL-AGR-EXCH-003-agreement-exchange-list-read.md"
$canonicalDetails = "planning/slices/SL-AGR-EXCH-004-agreement-exchange-details-read.md"
$canonicalAccept = "planning/slices/SL-AGR-EXCH-005-client-accept-active-agreement-proposal.md"
$canonicalFinal = "planning/slices/SL-AGR-EXCH-006-final-refuse-agreement-exchange.md"

if ((Test-Path $canonicalList) -and (Test-Path $canonicalDetails)) {
  Remove-Item "planning/slices/SL-AGR-EXCH-003-read-agreement-exchange.md" -ErrorAction SilentlyContinue
}

if (Test-Path $canonicalAccept) {
  Remove-Item "planning/slices/SL-AGR-EXCH-004-accept-active-agreement-proposal.md" -ErrorAction SilentlyContinue
}

if (Test-Path $canonicalFinal) {
  Remove-Item "planning/slices/SL-AGR-EXCH-005-final-refuse-agreement-exchange.md" -ErrorAction SilentlyContinue
}

# Targeted stale reference cleanup in SL-AGR-EXCH-001 if local file exists.
$startSlice = "planning/slices/SL-AGR-EXCH-001-start-agreement-exchange-with-initial-employee-proposal.md"
if (Test-Path $startSlice) {
  $text = Get-Content $startSlice -Raw
  $text = $text.Replace('Client read exchange | `SL-AGR-EXCH-003`', 'Agreement exchange list/details reads | `SL-AGR-EXCH-003` / `SL-AGR-EXCH-004`')
  $text = $text.Replace('Client accept proposal | `SL-AGR-EXCH-004`', 'Client accept proposal | `SL-AGR-EXCH-005`')
  $text = $text.Replace('Final refusal | `SL-AGR-EXCH-005`', 'Final refusal | `SL-AGR-EXCH-006`')
  $text = $text.Replace('SL-AGR-EXCH-003 — Read Agreement Exchange', 'SL-AGR-EXCH-003 — Agreement Exchange List Page / Read List`nSL-AGR-EXCH-004 — Agreement Exchange Details / Read Details')
  $text = $text.Replace('SL-AGR-EXCH-004 — Accept Active Agreement Proposal', 'SL-AGR-EXCH-005 — Client Accept Active Agreement Proposal')
  $text = $text.Replace('SL-AGR-EXCH-005 — Final Refuse Agreement Exchange', 'SL-AGR-EXCH-006 — Final Refuse Agreement Exchange')
  Set-Content -Path $startSlice -Value $text -Encoding UTF8
}

Write-Host "Run verification greps:" -ForegroundColor Yellow
Write-Host 'git grep -n "scenario-server-domain-validation-addendum"'
Write-Host 'git grep -n "DocumentFileRef\|ReviewerRef\|ProposalAttachment\|EmployeeRef"'
Write-Host 'git grep -n "SL-AGR-EXCH-003.*Read Agreement Exchange\|SL-AGR-EXCH-004.*Accept Active\|SL-AGR-EXCH-005.*Final Refuse"'
Write-Host 'git grep -n "ResponsibleEmployeeId" planning'
Write-Host 'git grep -n "Client Data Verification" planning'
