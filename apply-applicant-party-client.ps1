$ErrorActionPreference = "Stop"

Write-Host "Applying L1 applicant party client replacement..."

# Files are already expanded into the repository tree by Expand-Archive.
# This script intentionally does not delete runtime code.

Write-Host "Applied. Run:"
Write-Host "npm.cmd --prefix energymanagement.client run build"
Write-Host "npm.cmd --prefix energymanagement.client run test"
Write-Host "npm.cmd run check:api"
Write-Host "npm.cmd run test:e2e -- --list"
Write-Host "npm.cmd run test:e2e"
