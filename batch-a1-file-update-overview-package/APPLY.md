# Apply Batch A1 File Update Overview Package

Run these commands from repository root after extracting the package.

## 1. Copy replacement/add files

```powershell
$pkg = "batch-a1-file-update-overview-package"
$src = Join-Path (Get-Location) "$pkg\replacement-files"

Copy-Item -Path "$src\planning\documentation\file-update-overview-workflow.md" -Destination ".\planning\documentation\file-update-overview-workflow.md" -Force
Copy-Item -Path "$src\planning\documentation\FILE-UPDATE-OVERVIEW-TEMPLATE.md" -Destination ".\planning\documentation\FILE-UPDATE-OVERVIEW-TEMPLATE.md" -Force
Copy-Item -Path "$src\planning\documentation\README.md" -Destination ".\planning\documentation\README.md" -Force
Copy-Item -Path "$src\planning\documentation\documentation-responsibility-map.md" -Destination ".\planning\documentation\documentation-responsibility-map.md" -Force
Copy-Item -Path "$src\planning\documentation\examples\README.md" -Destination ".\planning\documentation\examples\README.md" -Force
Copy-Item -Path "$src\planning\documentation\documentation-action-log.md" -Destination ".\planning\documentation\documentation-action-log.md" -Force
```

## 2. Review status and diff

```powershell
$files = @(
  "planning/documentation/file-update-overview-workflow.md",
  "planning/documentation/FILE-UPDATE-OVERVIEW-TEMPLATE.md",
  "planning/documentation/README.md",
  "planning/documentation/documentation-responsibility-map.md",
  "planning/documentation/examples/README.md",
  "planning/documentation/documentation-action-log.md"
)

$newFiles = @(
  "planning/documentation/file-update-overview-workflow.md",
  "planning/documentation/FILE-UPDATE-OVERVIEW-TEMPLATE.md"
)

foreach ($file in $newFiles) {
  if (Test-Path $file) {
    git add -N -- $file
  } else {
    Write-Host "Missing expected new file: $file"
  }
}

$pkgName = "batch-a1-file-update-overview-package"
$diffFile = Join-Path (Get-Location) "$pkgName.diff"

git status --short -- $files
git diff --stat -- $files
git --no-pager diff --no-color --output="$diffFile" -- $files
Get-Content -Path $diffFile -Raw -Encoding UTF8 | Set-Clipboard

Write-Host "Diff saved to: $diffFile"
Write-Host "Diff copied to clipboard. Paste it into chat for review before committing."
```

## 3. Commit only after review

Do not commit before the pasted diff is reviewed.

Expected commit command after review:

```powershell
git add planning/documentation/file-update-overview-workflow.md planning/documentation/FILE-UPDATE-OVERVIEW-TEMPLATE.md planning/documentation/README.md planning/documentation/documentation-responsibility-map.md planning/documentation/examples/README.md planning/documentation/documentation-action-log.md
git commit -m "docs: add file update overview workflow"
git push origin my-changes
```
