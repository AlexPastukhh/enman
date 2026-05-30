# Apply Batch F4 Candidate Principles Split Package

Run from repo root after Batch F3 is committed.

```powershell
git checkout my-changes
git pull --ff-only origin my-changes

Copy-Item -Path "$env:USERPROFILE\Downloads\batch-f4-candidate-principles-split-package.zip" -Destination ".\batch-f4-candidate-principles-split-package.zip" -Force

Expand-Archive -Path ".\batch-f4-candidate-principles-split-package.zip" -DestinationPath ".\" -Force

$pkg = "batch-f4-candidate-principles-split-package"
$src = Join-Path (Get-Location) "$pkg\replacement-files"

Copy-Item -Path "$src\planning\\documentation-reusable-candidate\\planning-docs-architecture-principles.md" -Destination ".\planning\\documentation-reusable-candidate\\planning-docs-architecture-principles.md" -Force
Copy-Item -Path "$src\planning\\documentation-reusable-candidate\\scenario-domain-slice-docs-profile.md" -Destination ".\planning\\documentation-reusable-candidate\\scenario-domain-slice-docs-profile.md" -Force
Copy-Item -Path "$src\planning\\documentation-reusable-candidate\\enman-docs-adapter.md" -Destination ".\planning\\documentation-reusable-candidate\\enman-docs-adapter.md" -Force
Copy-Item -Path "$src\planning\\documentation-reusable-candidate\\PORTABILITY-FOLLOWUPS.md" -Destination ".\planning\\documentation-reusable-candidate\\PORTABILITY-FOLLOWUPS.md" -Force
Copy-Item -Path "$src\planning\\documentation-reusable-candidate\\README.md" -Destination ".\planning\\documentation-reusable-candidate\\README.md" -Force
Copy-Item -Path "$src\planning\\documentation-reusable-candidate\\CANDIDATE-NOTICE.md" -Destination ".\planning\\documentation-reusable-candidate\\CANDIDATE-NOTICE.md" -Force
Copy-Item -Path "$src\planning\\documentation\\documentation-layer-portability-migration-plan.md" -Destination ".\planning\\documentation\\documentation-layer-portability-migration-plan.md" -Force
Copy-Item -Path "$src\planning\\documentation\\documentation-action-log.md" -Destination ".\planning\\documentation\\documentation-action-log.md" -Force

$files = @(
  "planning/documentation-reusable-candidate/planning-docs-architecture-principles.md",
  "planning/documentation-reusable-candidate/scenario-domain-slice-docs-profile.md",
  "planning/documentation-reusable-candidate/enman-docs-adapter.md",
  "planning/documentation-reusable-candidate/PORTABILITY-FOLLOWUPS.md",
  "planning/documentation-reusable-candidate/README.md",
  "planning/documentation-reusable-candidate/CANDIDATE-NOTICE.md",
  "planning/documentation/documentation-layer-portability-migration-plan.md",
  "planning/documentation/documentation-action-log.md"
)

$newFiles = @(
  "planning/documentation-reusable-candidate/scenario-domain-slice-docs-profile.md",
  "planning/documentation-reusable-candidate/enman-docs-adapter.md",
  "planning/documentation-reusable-candidate/PORTABILITY-FOLLOWUPS.md"
)

git add -N -- $newFiles

$pkgName = "batch-f4-candidate-principles-split-package"
$diffFile = Join-Path (Get-Location) "$pkgName.diff"

git status --short -- $files
git diff --stat -- $files
git --no-pager diff --no-color --output="$diffFile" -- $files
Get-Content -Path $diffFile -Raw -Encoding UTF8 | Set-Clipboard

Write-Host "Diff saved to: $diffFile"
Write-Host "Diff copied to clipboard. Paste it into chat for review before committing."
```

Do not use `git add .`.
