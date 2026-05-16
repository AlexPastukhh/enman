$ErrorActionPreference = "Stop"

$scriptRoot = Split-Path -Parent $MyInvocation.MyCommand.Path
$repoRoot = Get-Location

$source = Join-Path $scriptRoot "planning\slices\l1\L1-MY-REQUESTS-LIST-FILTERS.client.md"
$target = Join-Path $repoRoot "planning\slices\l1\L1-MY-REQUESTS-LIST-FILTERS.client.md"

New-Item -ItemType Directory -Force -Path (Split-Path -Parent $target) | Out-Null
Copy-Item -Force $source $target

Write-Host "Applied L1-MY-REQUESTS-LIST-FILTERS.client.md"
