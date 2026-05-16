$ErrorActionPreference = "Stop"

$files = @(
  "planning/slices/l1/L1-APPLICANT-PARTY-READ-CURRENT.md",
  "planning/slices/l1/L1-APPLICANT-PARTY-READ-CURRENT.client.md"
)

Write-Host "L1 applicant party read-current slice docs were unpacked. Review files:"
foreach ($file in $files) {
  if (Test-Path $file) {
    Write-Host " - $file"
  } else {
    throw "Expected file missing: $file"
  }
}
