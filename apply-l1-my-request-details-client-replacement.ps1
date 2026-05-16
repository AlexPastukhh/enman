param(
    [string]$RepoRoot = (Get-Location).Path
)

$ErrorActionPreference = "Stop"
Write-Host "Applying L1-MY-REQUEST-DETAILS.client replacement into $RepoRoot"

$target = Join-Path $RepoRoot "planning\slices\l1"
New-Item -ItemType Directory -Force -Path $target | Out-Null

Copy-Item -Force -Path (Join-Path $PSScriptRoot "planning\slices\l1\L1-MY-REQUEST-DETAILS.client.md") -Destination $target

Write-Host "Done. Updated planning/slices/l1/L1-MY-REQUEST-DETAILS.client.md"
