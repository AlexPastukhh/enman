# Apply Batch F6A Candidate Navigation Cleanup Package

Run from repo root after Batch F5 v2 is committed and pushed.

```powershell
git checkout my-changes
git pull --ff-only origin my-changes

Copy-Item -Path "$env:USERPROFILE\Downloads\batch-f6a-candidate-navigation-cleanup-package.zip" -Destination ".\batch-f6a-candidate-navigation-cleanup-package.zip" -Force

Expand-Archive -Path ".\batch-f6a-candidate-navigation-cleanup-package.zip" -DestinationPath ".\" -Force

$pkg = "batch-f6a-candidate-navigation-cleanup-package"
$src = Join-Path (Get-Location) "$pkg\replacement-files"

Copy-Item -Path "$src\planning\\documentation-reusable-candidate\\README.md" -Destination ".\planning\\documentation-reusable-candidate\\README.md" -Force
Copy-Item -Path "$src\planning\\documentation-reusable-candidate\\CANDIDATE-NOTICE.md" -Destination ".\planning\\documentation-reusable-candidate\\CANDIDATE-NOTICE.md" -Force
Copy-Item -Path "$src\planning\\documentation-reusable-candidate\\PORTABILITY-FOLLOWUPS.md" -Destination ".\planning\\documentation-reusable-candidate\\PORTABILITY-FOLLOWUPS.md" -Force
Copy-Item -Path "$src\planning\\documentation\\documentation-layer-portability-migration-plan.md" -Destination ".\planning\\documentation\\documentation-layer-portability-migration-plan.md" -Force
Copy-Item -Path "$src\planning\\documentation\\documentation-action-log.md" -Destination ".\planning\\documentation\\documentation-action-log.md" -Force

$files = @(
  "planning/documentation-reusable-candidate/README.md",
  "planning/documentation-reusable-candidate/CANDIDATE-NOTICE.md",
  "planning/documentation-reusable-candidate/PORTABILITY-FOLLOWUPS.md",
  "planning/documentation/documentation-layer-portability-migration-plan.md",
  "planning/documentation/documentation-action-log.md"
)

$pkgName = "batch-f6a-candidate-navigation-cleanup-package"
$diffFile = Join-Path (Get-Location) "$pkgName.diff"

git status --short -- $files
git diff --stat -- $files
git --no-pager diff --no-color --output="$diffFile" -- $files
Get-Content -Path $diffFile -Raw -Encoding UTF8 | Set-Clipboard

Write-Host "Diff saved to: $diffFile"
Write-Host "Diff copied to clipboard. Paste it into chat for review before committing."
```

Do not use `git add .`.
