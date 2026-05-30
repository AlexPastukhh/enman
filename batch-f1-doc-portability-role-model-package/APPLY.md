# Apply Batch F1 Package

Run from repo root.

```powershell
git checkout my-changes
git pull --ff-only origin my-changes

Copy-Item -Path "$env:USERPROFILE\Downloads\batch-f1-doc-portability-role-model-package.zip" -Destination ".\batch-f1-doc-portability-role-model-package.zip" -Force

Expand-Archive -Path ".\batch-f1-doc-portability-role-model-package.zip" -DestinationPath ".\" -Force

$pkg = "batch-f1-doc-portability-role-model-package"
$src = Join-Path (Get-Location) "$pkg\replacement-files"

Copy-Item -Path "$src\planning\\documentation\\documentation-layer-portability-migration-plan.md" -Destination ".\planning\\documentation\\documentation-layer-portability-migration-plan.md" -Force
Copy-Item -Path "$src\planning\\documentation\\documentation-responsibility-zone-review-workflow.md" -Destination ".\planning\\documentation\\documentation-responsibility-zone-review-workflow.md" -Force
Copy-Item -Path "$src\planning\\documentation\\planning-docs-architecture-principles.md" -Destination ".\planning\\documentation\\planning-docs-architecture-principles.md" -Force
Copy-Item -Path "$src\planning\\documentation\\documentation-responsibility-map.md" -Destination ".\planning\\documentation\\documentation-responsibility-map.md" -Force
Copy-Item -Path "$src\planning\\documentation\\README.md" -Destination ".\planning\\documentation\\README.md" -Force
Copy-Item -Path "$src\planning\\planning-use-case-map.md" -Destination ".\planning\\planning-use-case-map.md" -Force
Copy-Item -Path "$src\planning\\documentation\\documentation-update-workflow.md" -Destination ".\planning\\documentation\\documentation-update-workflow.md" -Force
Copy-Item -Path "$src\planning\\documentation\\examples\\README.md" -Destination ".\planning\\documentation\\examples\\README.md" -Force
Copy-Item -Path "$src\planning\\documentation\\documentation-action-log.md" -Destination ".\planning\\documentation\\documentation-action-log.md" -Force

$files = @(
  "planning/documentation/documentation-layer-portability-migration-plan.md",
  "planning/documentation/documentation-responsibility-zone-review-workflow.md",
  "planning/documentation/planning-docs-architecture-principles.md",
  "planning/documentation/documentation-responsibility-map.md",
  "planning/documentation/README.md",
  "planning/planning-use-case-map.md",
  "planning/documentation/documentation-update-workflow.md",
  "planning/documentation/examples/README.md",
  "planning/documentation/documentation-action-log.md"
)

$newFiles = @(
  "planning/documentation/documentation-layer-portability-migration-plan.md",
  "planning/documentation/documentation-responsibility-zone-review-workflow.md"
)

git add -N -- $newFiles

$pkgName = "batch-f1-doc-portability-role-model-package"
$diffFile = Join-Path (Get-Location) "$pkgName.diff"

git status --short -- $files
git diff --stat -- $files
git --no-pager diff --no-color --output="$diffFile" -- $files
Get-Content -Path $diffFile -Raw -Encoding UTF8 | Set-Clipboard

Write-Host "Diff saved to: $diffFile"
Write-Host "Diff copied to clipboard. Paste it into chat for review before committing."
```

Do not use `git add .`.
