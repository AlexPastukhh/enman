# Apply Batch B Local Targeted Script Edit Package

Run from repository root after downloading the archive.

## 1. Copy replacement files

```powershell
Copy-Item -Path "$env:USERPROFILE\Downloads\batch-b-local-targeted-script-edit-package.zip" -Destination ".\batch-b-local-targeted-script-edit-package.zip" -Force

Expand-Archive -Path ".\batch-b-local-targeted-script-edit-package.zip" -DestinationPath ".\" -Force

$pkg = "batch-b-local-targeted-script-edit-package"
$src = Join-Path (Get-Location) "$pkg\replacement-files"

Copy-Item -Path "$src\planning\documentation\documentation-update-workflow.md" -Destination ".\planning\documentation\documentation-update-workflow.md" -Force
Copy-Item -Path "$src\planning\replacement-file-generation-guide.md" -Destination ".\planning\replacement-file-generation-guide.md" -Force
Copy-Item -Path "$src\planning\documentation\examples\README.md" -Destination ".\planning\documentation\examples\README.md" -Force
Copy-Item -Path "$src\planning\documentation\documentation-action-log.md" -Destination ".\planning\documentation\documentation-action-log.md" -Force
```

## 2. Capture scoped diff

```powershell
$files = @(
  "planning/documentation/documentation-update-workflow.md",
  "planning/replacement-file-generation-guide.md",
  "planning/documentation/examples/README.md",
  "planning/documentation/documentation-action-log.md"
)

$newFiles = @()

$pkgName = "batch-b-local-targeted-script-edit-package"
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
git add planning/documentation/documentation-update-workflow.md planning/replacement-file-generation-guide.md planning/documentation/examples/README.md planning/documentation/documentation-action-log.md
git commit -m "docs: add local targeted script edit mode"
git push origin my-changes
```

Do not use `git add .`.
