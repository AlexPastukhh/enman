# Apply Batch C Response Format And Large-File Preference Package

Run from repository root after downloading the archive.

## 1. Copy replacement files

```powershell
Copy-Item -Path "$env:USERPROFILE\Downloads\batch-c-response-format-large-file-preference-package.zip" -Destination ".\batch-c-response-format-large-file-preference-package.zip" -Force

Expand-Archive -Path ".\batch-c-response-format-large-file-preference-package.zip" -DestinationPath ".\" -Force

$pkg = "batch-c-response-format-large-file-preference-package"
$src = Join-Path (Get-Location) "$pkg\replacement-files"

Copy-Item -Path "$src\planning\documentation\reviewable-agent-output-and-commands-workflow.md" -Destination ".\planning\documentation\reviewable-agent-output-and-commands-workflow.md" -Force
Copy-Item -Path "$src\planning\planning-use-case-map.md" -Destination ".\planning\planning-use-case-map.md" -Force
Copy-Item -Path "$src\planning\documentation\file-update-overview-workflow.md" -Destination ".\planning\documentation\file-update-overview-workflow.md" -Force
Copy-Item -Path "$src\planning\documentation\FILE-UPDATE-OVERVIEW-TEMPLATE.md" -Destination ".\planning\documentation\FILE-UPDATE-OVERVIEW-TEMPLATE.md" -Force
Copy-Item -Path "$src\planning\documentation\documentation-update-workflow.md" -Destination ".\planning\documentation\documentation-update-workflow.md" -Force
Copy-Item -Path "$src\planning\replacement-file-generation-guide.md" -Destination ".\planning\replacement-file-generation-guide.md" -Force
Copy-Item -Path "$src\planning\documentation\planning-docs-architecture-principles.md" -Destination ".\planning\documentation\planning-docs-architecture-principles.md" -Force
Copy-Item -Path "$src\planning\documentation\examples\README.md" -Destination ".\planning\documentation\examples\README.md" -Force
Copy-Item -Path "$src\planning\documentation\documentation-action-log.md" -Destination ".\planning\documentation\documentation-action-log.md" -Force
```

## 2. Capture scoped diff

```powershell
$files = @(
  "planning/documentation/reviewable-agent-output-and-commands-workflow.md",
  "planning/planning-use-case-map.md",
  "planning/documentation/file-update-overview-workflow.md",
  "planning/documentation/FILE-UPDATE-OVERVIEW-TEMPLATE.md",
  "planning/documentation/documentation-update-workflow.md",
  "planning/replacement-file-generation-guide.md",
  "planning/documentation/planning-docs-architecture-principles.md",
  "planning/documentation/examples/README.md",
  "planning/documentation/documentation-action-log.md"
)

$newFiles = @()

$pkgName = "batch-c-response-format-large-file-preference-package"
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
git add planning/documentation/reviewable-agent-output-and-commands-workflow.md planning/planning-use-case-map.md planning/documentation/file-update-overview-workflow.md planning/documentation/FILE-UPDATE-OVERVIEW-TEMPLATE.md planning/documentation/documentation-update-workflow.md planning/replacement-file-generation-guide.md planning/documentation/planning-docs-architecture-principles.md planning/documentation/examples/README.md planning/documentation/documentation-action-log.md
git commit -m "docs: clarify response blocks and large file updates"
git push origin my-changes
```

Do not use `git add .`.
