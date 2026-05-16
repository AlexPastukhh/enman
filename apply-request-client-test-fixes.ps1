$ErrorActionPreference = "Stop"
$Root = Split-Path -Parent $MyInvocation.MyCommand.Path
Copy-Item -Path (Join-Path $Root "energymanagement.client") -Destination "." -Recurse -Force
Copy-Item -Path (Join-Path $Root "tests") -Destination "." -Recurse -Force
Write-Host "Applied request client test fixes."
