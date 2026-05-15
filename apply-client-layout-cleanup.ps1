$ErrorActionPreference = "Stop"

$oldLayout = "energymanagement.client/src/Components/Layout"

if (Test-Path $oldLayout) {
    Remove-Item $oldLayout -Recurse -Force
    Write-Host "Removed $oldLayout"
} else {
    Write-Host "$oldLayout does not exist; nothing to remove"
}

Write-Host "Layout cleanup files are in place."
Write-Host "Recommended checks:"
Write-Host "  npm.cmd --prefix energymanagement.client run build"
Write-Host "  npm.cmd --prefix energymanagement.client run test"
Write-Host "  npm.cmd run check:api"
Write-Host "  npm.cmd run test:e2e -- --list"
Write-Host "  npm.cmd run test:e2e"
