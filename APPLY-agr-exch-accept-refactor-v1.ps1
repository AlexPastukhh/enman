$ErrorActionPreference = "Stop"

$Slug = "agr-exch-accept-refactor-v1"
$ReplacementRoot = Join-Path $PSScriptRoot "_replacement-files/$Slug"
$ReviewRoot = Join-Path $PSScriptRoot "_archive-review/$Slug"

$Targets = @(
    "planning/slices/SL-AGR-EXCH-005-client-accept-active-agreement-proposal.md",
    "planning/slices/l2/L2-AGR-EXCH-ACCEPT-001-client-accept-active-agreement-proposal.client.md"
)

function Ensure-DirectoryForFile {
    param([string]$Path)
    $Directory = Split-Path -Parent $Path
    if ($Directory -and -not (Test-Path $Directory)) {
        New-Item -ItemType Directory -Force -Path $Directory | Out-Null
    }
}

if (-not (Test-Path $ReplacementRoot)) {
    throw "Replacement root not found: $ReplacementRoot"
}

foreach ($Target in $Targets) {
    $ProjectFile = Join-Path $PSScriptRoot $Target
    $ReplacementFile = Join-Path $ReplacementRoot $Target
    $OriginalSnapshot = Join-Path $ReviewRoot (Join-Path "original-files" $Target)

    if (-not (Test-Path $ProjectFile)) {
        throw "Target project file not found: $Target"
    }

    if (-not (Test-Path $ReplacementFile)) {
        throw "Replacement file not found: $ReplacementFile"
    }

    Ensure-DirectoryForFile $OriginalSnapshot

    if (-not (Test-Path $OriginalSnapshot)) {
        Copy-Item -Path $ProjectFile -Destination $OriginalSnapshot -Force
        Write-Host "Captured original: $OriginalSnapshot"
    } else {
        Write-Host "Original snapshot already exists, not overwriting: $OriginalSnapshot"
    }

    Copy-Item -Path $ReplacementFile -Destination $ProjectFile -Force
    Write-Host "Replaced: $Target"
}

$AppliedFile = Join-Path $ReviewRoot "APPLIED-FILES.md"
$AppliedContent = @"
# Applied Files — $Slug

Applied at: $(Get-Date -Format o)

## Replaced files

```text
planning/slices/SL-AGR-EXCH-005-client-accept-active-agreement-proposal.md
planning/slices/l2/L2-AGR-EXCH-ACCEPT-001-client-accept-active-agreement-proposal.client.md
```

## Original snapshots

```text
_archive-review/$Slug/original-files/planning/slices/SL-AGR-EXCH-005-client-accept-active-agreement-proposal.md
_archive-review/$Slug/original-files/planning/slices/l2/L2-AGR-EXCH-ACCEPT-001-client-accept-active-agreement-proposal.client.md
```
"@
Set-Content -Path $AppliedFile -Value $AppliedContent -Encoding UTF8

# Remove staged replacement files after safe copy to avoid committing duplicate replacement payloads.
$StageDir = Join-Path $PSScriptRoot "_replacement-files/$Slug"
if (Test-Path $StageDir) {
    Remove-Item -Path $StageDir -Recurse -Force
}

Write-Host "Done. Review with:"
Write-Host "git diff -- planning/slices/SL-AGR-EXCH-005-client-accept-active-agreement-proposal.md"
Write-Host "git diff -- planning/slices/l2/L2-AGR-EXCH-ACCEPT-001-client-accept-active-agreement-proposal.client.md"
Write-Host "git diff -- _archive-review/$Slug"
