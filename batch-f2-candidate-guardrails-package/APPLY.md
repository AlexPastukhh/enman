# Apply Batch F2 Candidate Guardrails Package

Run from repo root **after** the baseline copy folder already exists.

```powershell
git checkout my-changes
git pull --ff-only origin my-changes

Copy-Item -Path "$env:USERPROFILE\Downloads\batch-f2-candidate-guardrails-package.zip" -Destination ".\batch-f2-candidate-guardrails-package.zip" -Force

Expand-Archive -Path ".\batch-f2-candidate-guardrails-package.zip" -DestinationPath ".\" -Force

$pkg = "batch-f2-candidate-guardrails-package"
$src = Join-Path (Get-Location) "$pkg\replacement-files"

Copy-Item -Path "$src\planning\\documentation-reusable-candidate\\CANDIDATE-NOTICE.md" -Destination ".\planning\\documentation-reusable-candidate\\CANDIDATE-NOTICE.md" -Force
Copy-Item -Path "$src\planning\\documentation\\documentation-layer-portability-migration-plan.md" -Destination ".\planning\\documentation\\documentation-layer-portability-migration-plan.md" -Force
Copy-Item -Path "$src\planning\\documentation\\README.md" -Destination ".\planning\\documentation\\README.md" -Force
Copy-Item -Path "$src\planning\\documentation\\documentation-responsibility-map.md" -Destination ".\planning\\documentation\\documentation-responsibility-map.md" -Force
Copy-Item -Path "$src\planning\\documentation\\documentation-action-log.md" -Destination ".\planning\\documentation\\documentation-action-log.md" -Force

$files = @(
  "planning/documentation-reusable-candidate/CANDIDATE-NOTICE.md",
  "planning/documentation/documentation-layer-portability-migration-plan.md",
  "planning/documentation/README.md",
  "planning/documentation/documentation-responsibility-map.md",
  "planning/documentation/documentation-action-log.md"
)

$newFiles = @(
  "planning/documentation-reusable-candidate/CANDIDATE-NOTICE.md"
)

git add -N -- $newFiles

$pkgName = "batch-f2-candidate-guardrails-package"
$diffFile = Join-Path (Get-Location) "$pkgName.diff"

git status --short -- $files
git diff --stat -- $files
git --no-pager diff --no-color --output="$diffFile" -- $files
Get-Content -Path $diffFile -Raw -Encoding UTF8 | Set-Clipboard

Write-Host "Diff saved to: $diffFile"
Write-Host "Diff copied to clipboard. Paste it into chat for review before committing."
```

Do not use `git add .`.
