# Apply Batch D Table Итог And Delivery Safety Package

Run from repository root.

## 1. Apply replacement files

```powershell
git checkout my-changes
git pull --ff-only origin my-changes

Copy-Item -Path "$env:USERPROFILE\Downloads\batch-d-table-itog-delivery-safety-package.zip" -Destination ".\batch-d-table-itog-delivery-safety-package.zip" -Force

Expand-Archive -Path ".\batch-d-table-itog-delivery-safety-package.zip" -DestinationPath ".\" -Force

$pkg = "batch-d-table-itog-delivery-safety-package"
$src = Join-Path (Get-Location) "$pkg\replacement-files"

Copy-Item -Path "$src\planning\documentation\FILE-UPDATE-OVERVIEW-TEMPLATE.md" -Destination ".\planning\documentation\FILE-UPDATE-OVERVIEW-TEMPLATE.md" -Force
Copy-Item -Path "$src\planning\documentation\file-update-overview-workflow.md" -Destination ".\planning\documentation\file-update-overview-workflow.md" -Force
Copy-Item -Path "$src\planning\documentation\reviewable-agent-output-and-commands-workflow.md" -Destination ".\planning\documentation\reviewable-agent-output-and-commands-workflow.md" -Force
Copy-Item -Path "$src\planning\documentation\documentation-update-workflow.md" -Destination ".\planning\documentation\documentation-update-workflow.md" -Force
Copy-Item -Path "$src\planning\documentation\examples\README.md" -Destination ".\planning\documentation\examples\README.md" -Force
Copy-Item -Path "$src\planning\documentation\documentation-action-log.md" -Destination ".\planning\documentation\documentation-action-log.md" -Force
```

## 2. Capture scoped diff

```powershell
$files = @(
  "planning/documentation/FILE-UPDATE-OVERVIEW-TEMPLATE.md",
  "planning/documentation/file-update-overview-workflow.md",
  "planning/documentation/reviewable-agent-output-and-commands-workflow.md",
  "planning/documentation/documentation-update-workflow.md",
  "planning/documentation/examples/README.md",
  "planning/documentation/documentation-action-log.md"
)

$newFiles = @()

$pkgName = "batch-d-table-itog-delivery-safety-package"
$diffFile = Join-Path (Get-Location) "$pkgName.diff"

git status --short -- $files
git diff --stat -- $files
git --no-pager diff --no-color --output="$diffFile" -- $files
Get-Content -Path $diffFile -Raw -Encoding UTF8 | Set-Clipboard

Write-Host "Diff saved to: $diffFile"
Write-Host "Diff copied to clipboard. Paste it into chat for review before committing."
```

## 3. Commit only after review

```powershell
git add planning/documentation/FILE-UPDATE-OVERVIEW-TEMPLATE.md planning/documentation/file-update-overview-workflow.md planning/documentation/reviewable-agent-output-and-commands-workflow.md planning/documentation/documentation-update-workflow.md planning/documentation/examples/README.md planning/documentation/documentation-action-log.md
git commit -m "docs: make итог table based and expose delivery safety"
git push origin my-changes
```

Do not use `git add .`.
