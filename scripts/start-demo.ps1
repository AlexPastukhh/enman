param(
    [switch]$SkipDatabaseReset
)

$ErrorActionPreference = "Stop"

$repoRoot = Resolve-Path (Join-Path $PSScriptRoot "..")
$clientRoot = Join-Path $repoRoot "energymanagement.client"

function Invoke-Step {
    param(
        [string]$Title,
        [scriptblock]$Command
    )

    Write-Host ""
    Write-Host "==> $Title" -ForegroundColor Cyan
    & $Command
}

Invoke-Step "Checking required tools" {
    dotnet --version | Out-Host
    node --version | Out-Host
    npm --version | Out-Host
}

if (-not $SkipDatabaseReset) {
    Invoke-Step "Resetting LocalDB demo database" {
        dotnet run --project (Join-Path $repoRoot "EnergyManagement.Tools") -- reset-test-db
    }
}

Invoke-Step "Seeding demo accounts and sample data" {
    dotnet run --project (Join-Path $repoRoot "EnergyManagement.Tools") -- seed-e2e-demo-data
}

Invoke-Step "Restoring frontend packages when needed" {
    if (-not (Test-Path (Join-Path $clientRoot "node_modules"))) {
        npm.cmd --prefix $clientRoot ci
    }
    else {
        Write-Host "Frontend node_modules already exists."
    }
}

$backendCommand = @"
`$ErrorActionPreference = 'Stop'
Set-Location '$repoRoot'
`$env:ASPNETCORE_ENVIRONMENT = 'Development'
dotnet run --project EnergyManagement.Server/EnergyManagement.Server.csproj --no-launch-profile -- --urls https://localhost:7250
"@

$frontendCommand = @"
`$ErrorActionPreference = 'Stop'
Set-Location '$clientRoot'
npm.cmd run dev -- --host 127.0.0.1
"@

Invoke-Step "Starting backend in a separate PowerShell window" {
    Start-Process powershell.exe -ArgumentList "-NoExit", "-Command", $backendCommand
}

Invoke-Step "Starting frontend in a separate PowerShell window" {
    Start-Process powershell.exe -ArgumentList "-NoExit", "-Command", $frontendCommand
}

Write-Host ""
Write-Host "Demo is starting." -ForegroundColor Green
Write-Host "Frontend: https://127.0.0.1:5173"
Write-Host "Backend:  https://localhost:7250"
Write-Host ""
Write-Host "Demo accounts:"
Write-Host "  Client:   e2e.client@example.com / ValidPassword111!"
Write-Host "  Employee: e2e.employee@example.com / ValidPassword111!"
Write-Host ""
Write-Host "Close the two PowerShell windows to stop the app."
