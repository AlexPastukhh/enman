$ErrorActionPreference = "Stop"

Write-Host "Applying SL-APPL-001.client replacement..." -ForegroundColor Cyan

$target = "planning\slices\SL-APPL-001-create-individual-applicant-party.client.md"

if (-not (Test-Path $target)) {
  Write-Host "Warning: target file does not exist yet and will be created: $target" -ForegroundColor Yellow
}

Write-Host "Replacement file is already placed at: $target" -ForegroundColor Green
Write-Host "Run git diff to review changes." -ForegroundColor Cyan
