# ...existing code...
param(
  [string] $TestDbConnection = $env:TEST_DB_CONNECTION  # optional override
)

$ErrorActionPreference = "Stop"

# env for test run
$env:ASPNETCORE_ENVIRONMENT = "Test"
$env:DbName__Name = "ManagementDb"

if ($TestDbConnection) {
  $env:ConnectionStrings__ManagementDb = $TestDbConnection
} elseif ($env:ConnectionStrings__Test) {
  $env:ConnectionStrings__ManagementDb = $env:ConnectionStrings__Test
}

Write-Host "DbName:Name = $($env:DbName__Name)"
Write-Host "ConnectionStrings__ManagementDb = **** (redacted)"

# server project folder (relative to this script) - go up two levels from src/scripts
$candidate1 = Join-Path $PSScriptRoot "..\..\EnergyManagement.Server"
$serverFolder = $null
if (Test-Path $candidate1) {
  $serverFolder = Resolve-Path $candidate1
} else {
  # fallback: try sibling folder from repo root
  $candidate2 = Join-Path $PSScriptRoot "..\..\..\EnergyManagement.Server"
  if (Test-Path $candidate2) {
    $serverFolder = Resolve-Path $candidate2
  }
}

if (-not $serverFolder) {
  Write-Error "Cannot find EnergyManagement.Server folder. Checked: `"$candidate1`" and `"$candidate2`"."
  exit 1
}

Write-Host "Using server folder: $serverFolder"

# run EF commands from the server project directory to avoid scanning other projects
Push-Location $serverFolder
try {
  Write-Host "Running EF commands in server folder: $PWD"

  # prefer local tool if configured, otherwise try global dotnet-ef
  try {
    dotnet tool run dotnet-ef --version > $null
    $efCmd = "dotnet tool run dotnet-ef"
  } catch {
    $efCmd = "dotnet ef"
  }

  Write-Host "Using EF command: $efCmd"

  try {
    & $efCmd database drop --force
    & $efCmd database update
  } catch {
    Write-Warning "EF commands failed: $_. Continuing to start the server."
  }
} finally {
  Pop-Location
}

# start backend (foreground) — use resolved server folder for project path
$serverCsproj = Join-Path $serverFolder "EnergyManagement.Server.csproj"
if (-not (Test-Path $serverCsproj)) {
  Write-Error "Server csproj not found at: $serverCsproj"
  exit 1
}

Write-Host "Starting backend (foreground)..."
dotnet run --project $serverCsproj --urls "https://localhost:7250"
