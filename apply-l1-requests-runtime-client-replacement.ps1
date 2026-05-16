$ErrorActionPreference = "Stop"

$archive = Join-Path $PSScriptRoot "enman-l1-requests-runtime-client-replacement.zip"
if (-not (Test-Path $archive)) {
  Write-Host "Run this script from the extracted archive directory or use Expand-Archive manually."
  exit 1
}

Expand-Archive $archive -DestinationPath . -Force
Write-Host "Applied L1 requests runtime client replacement."
