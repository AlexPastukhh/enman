# Apply Batch F5 Candidate Field Kits Package

Run from repo root after Batch F4 is committed and pushed.

```powershell
git checkout my-changes
git pull --ff-only origin my-changes

Copy-Item -Path "$env:USERPROFILE\Downloads\batch-f5-candidate-field-kits-package.zip" -Destination ".\batch-f5-candidate-field-kits-package.zip" -Force

Expand-Archive -Path ".\batch-f5-candidate-field-kits-package.zip" -DestinationPath ".\" -Force

$pkg = "batch-f5-candidate-field-kits-package"
$src = Join-Path (Get-Location) "$pkg\replacement-files"

Copy-Item -Path "$src\planning\\documentation-reusable-candidate\\status-reconciliation-field-kit.md" -Destination ".\planning\\documentation-reusable-candidate\\status-reconciliation-field-kit.md" -Force
Copy-Item -Path "$src\planning\\documentation-reusable-candidate\\status-reconciliation-workflow.md" -Destination ".\planning\\documentation-reusable-candidate\\status-reconciliation-workflow.md" -Force
Copy-Item -Path "$src\planning\\documentation-reusable-candidate\\enman-status-evidence-profile.md" -Destination ".\planning\\documentation-reusable-candidate\\enman-status-evidence-profile.md" -Force
Copy-Item -Path "$src\planning\\documentation-reusable-candidate\\shared-visibility-map-field-kit.md" -Destination ".\planning\\documentation-reusable-candidate\\shared-visibility-map-field-kit.md" -Force
Copy-Item -Path "$src\planning\\documentation-reusable-candidate\\local-global-documentation-sync-workflow.md" -Destination ".\planning\\documentation-reusable-candidate\\local-global-documentation-sync-workflow.md" -Force
Copy-Item -Path "$src\planning\\documentation-reusable-candidate\\enman-shared-visibility-map.md" -Destination ".\planning\\documentation-reusable-candidate\\enman-shared-visibility-map.md" -Force
Copy-Item -Path "$src\planning\\documentation-reusable-candidate\\source-usage-cascade-field-kit.md" -Destination ".\planning\\documentation-reusable-candidate\\source-usage-cascade-field-kit.md" -Force
Copy-Item -Path "$src\planning\\documentation-reusable-candidate\\source-usage-cascade-governance-plan.md" -Destination ".\planning\\documentation-reusable-candidate\\source-usage-cascade-governance-plan.md" -Force
Copy-Item -Path "$src\planning\\documentation-reusable-candidate\\enman-source-usage-cascade-profile.md" -Destination ".\planning\\documentation-reusable-candidate\\enman-source-usage-cascade-profile.md" -Force
Copy-Item -Path "$src\planning\\documentation-reusable-candidate\\examples\\STATUS-RECONCILIATION-SCENARIO-PROJECT-EXAMPLE.md" -Destination ".\planning\\documentation-reusable-candidate\\examples\\STATUS-RECONCILIATION-SCENARIO-PROJECT-EXAMPLE.md" -Force
Copy-Item -Path "$src\planning\\documentation-reusable-candidate\\examples\\SHARED-VISIBILITY-SCENARIO-PROJECT-EXAMPLE.md" -Destination ".\planning\\documentation-reusable-candidate\\examples\\SHARED-VISIBILITY-SCENARIO-PROJECT-EXAMPLE.md" -Force
Copy-Item -Path "$src\planning\\documentation-reusable-candidate\\examples\\SOURCE-USAGE-CASCADE-GENERIC-EXAMPLE.md" -Destination ".\planning\\documentation-reusable-candidate\\examples\\SOURCE-USAGE-CASCADE-GENERIC-EXAMPLE.md" -Force
Copy-Item -Path "$src\planning\\documentation-reusable-candidate\\examples\\README.md" -Destination ".\planning\\documentation-reusable-candidate\\examples\\README.md" -Force
Copy-Item -Path "$src\planning\\documentation-reusable-candidate\\enman-docs-adapter.md" -Destination ".\planning\\documentation-reusable-candidate\\enman-docs-adapter.md" -Force
Copy-Item -Path "$src\planning\\documentation-reusable-candidate\\PORTABILITY-FOLLOWUPS.md" -Destination ".\planning\\documentation-reusable-candidate\\PORTABILITY-FOLLOWUPS.md" -Force
Copy-Item -Path "$src\planning\\documentation-reusable-candidate\\README.md" -Destination ".\planning\\documentation-reusable-candidate\\README.md" -Force
Copy-Item -Path "$src\planning\\documentation-reusable-candidate\\CANDIDATE-NOTICE.md" -Destination ".\planning\\documentation-reusable-candidate\\CANDIDATE-NOTICE.md" -Force
Copy-Item -Path "$src\planning\\documentation\\documentation-layer-portability-migration-plan.md" -Destination ".\planning\\documentation\\documentation-layer-portability-migration-plan.md" -Force
Copy-Item -Path "$src\planning\\documentation\\documentation-action-log.md" -Destination ".\planning\\documentation\\documentation-action-log.md" -Force

$files = @(
  "planning/documentation-reusable-candidate/status-reconciliation-field-kit.md",
  "planning/documentation-reusable-candidate/status-reconciliation-workflow.md",
  "planning/documentation-reusable-candidate/enman-status-evidence-profile.md",
  "planning/documentation-reusable-candidate/shared-visibility-map-field-kit.md",
  "planning/documentation-reusable-candidate/local-global-documentation-sync-workflow.md",
  "planning/documentation-reusable-candidate/enman-shared-visibility-map.md",
  "planning/documentation-reusable-candidate/source-usage-cascade-field-kit.md",
  "planning/documentation-reusable-candidate/source-usage-cascade-governance-plan.md",
  "planning/documentation-reusable-candidate/enman-source-usage-cascade-profile.md",
  "planning/documentation-reusable-candidate/examples/STATUS-RECONCILIATION-SCENARIO-PROJECT-EXAMPLE.md",
  "planning/documentation-reusable-candidate/examples/SHARED-VISIBILITY-SCENARIO-PROJECT-EXAMPLE.md",
  "planning/documentation-reusable-candidate/examples/SOURCE-USAGE-CASCADE-GENERIC-EXAMPLE.md",
  "planning/documentation-reusable-candidate/examples/README.md",
  "planning/documentation-reusable-candidate/enman-docs-adapter.md",
  "planning/documentation-reusable-candidate/PORTABILITY-FOLLOWUPS.md",
  "planning/documentation-reusable-candidate/README.md",
  "planning/documentation-reusable-candidate/CANDIDATE-NOTICE.md",
  "planning/documentation/documentation-layer-portability-migration-plan.md",
  "planning/documentation/documentation-action-log.md"
)

$newFiles = @(
  "planning/documentation-reusable-candidate/status-reconciliation-field-kit.md",
  "planning/documentation-reusable-candidate/enman-status-evidence-profile.md",
  "planning/documentation-reusable-candidate/shared-visibility-map-field-kit.md",
  "planning/documentation-reusable-candidate/enman-shared-visibility-map.md",
  "planning/documentation-reusable-candidate/source-usage-cascade-field-kit.md",
  "planning/documentation-reusable-candidate/enman-source-usage-cascade-profile.md",
  "planning/documentation-reusable-candidate/examples/STATUS-RECONCILIATION-SCENARIO-PROJECT-EXAMPLE.md",
  "planning/documentation-reusable-candidate/examples/SHARED-VISIBILITY-SCENARIO-PROJECT-EXAMPLE.md",
  "planning/documentation-reusable-candidate/examples/SOURCE-USAGE-CASCADE-GENERIC-EXAMPLE.md"
)

git add -N -- $newFiles

$pkgName = "batch-f5-candidate-field-kits-package"
$diffFile = Join-Path (Get-Location) "$pkgName.diff"

git status --short -- $files
git diff --stat -- $files
git --no-pager diff --no-color --output="$diffFile" -- $files
Get-Content -Path $diffFile -Raw -Encoding UTF8 | Set-Clipboard

Write-Host "Diff saved to: $diffFile"
Write-Host "Diff copied to clipboard. Paste it into chat for review before committing."
```

Do not use `git add .`.
