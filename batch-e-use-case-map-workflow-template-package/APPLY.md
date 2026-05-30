# Apply Batch E — Reusable Use-Case Map Workflow And Template

Run from repository root on branch `my-changes`.

```powershell
git checkout my-changes
git pull --ff-only origin my-changes

Copy-Item -Path "$env:USERPROFILE\Downloads\batch-e-use-case-map-workflow-template-package.zip" -Destination ".\batch-e-use-case-map-workflow-template-package.zip" -Force

Expand-Archive -Path ".\batch-e-use-case-map-workflow-template-package.zip" -DestinationPath ".\" -Force

$pkg = "batch-e-use-case-map-workflow-template-package"
$src = Join-Path (Get-Location) "$pkg\replacement-files"

Copy-Item -Path "$src\planning\documentation\use-case-map-workflow.md" -Destination ".\planning\documentation\use-case-map-workflow.md" -Force
Copy-Item -Path "$src\planning\documentation\USE-CASE-MAP-TEMPLATE.md" -Destination ".\planning\documentation\USE-CASE-MAP-TEMPLATE.md" -Force
Copy-Item -Path "$src\planning\documentation\README.md" -Destination ".\planning\documentation\README.md" -Force
Copy-Item -Path "$src\planning\documentation\documentation-responsibility-map.md" -Destination ".\planning\documentation\documentation-responsibility-map.md" -Force
Copy-Item -Path "$src\planning\planning-use-case-map.md" -Destination ".\planning\planning-use-case-map.md" -Force
Copy-Item -Path "$src\planning\documentation\examples\README.md" -Destination ".\planning\documentation\examples\README.md" -Force
Copy-Item -Path "$src\planning\documentation\documentation-action-log.md" -Destination ".\planning\documentation\documentation-action-log.md" -Force

$files = @(
  "planning/documentation/use-case-map-workflow.md",
  "planning/documentation/USE-CASE-MAP-TEMPLATE.md",
  "planning/documentation/README.md",
  "planning/documentation/documentation-responsibility-map.md",
  "planning/planning-use-case-map.md",
  "planning/documentation/examples/README.md",
  "planning/documentation/documentation-action-log.md"
)

$newFiles = @(
  "planning/documentation/use-case-map-workflow.md",
  "planning/documentation/USE-CASE-MAP-TEMPLATE.md"
)

git add -N -- $newFiles

$diffFile = Join-Path (Get-Location) "batch-e-use-case-map-workflow-template-package.diff"

git status --short -- $files
git diff --stat -- $files
git --no-pager diff --no-color --output="$diffFile" -- $files
Get-Content -Path $diffFile -Raw -Encoding UTF8 | Set-Clipboard

Write-Host "Diff saved to: $diffFile"
Write-Host "Diff copied to clipboard. Paste it into chat for review before committing."
```

After review, commit only the intended files.
