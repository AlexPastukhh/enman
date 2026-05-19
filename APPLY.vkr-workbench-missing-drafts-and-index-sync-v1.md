# APPLY — vkr-workbench-missing-drafts-and-index-sync-v1

## PowerShell apply command

```powershell
cd "C:\enman\enman"

$zip = "C:\Users\alexa\Downloads\vkr-workbench-missing-drafts-and-index-sync-v1.zip"
$tmp = "$env:TEMP\vkr-workbench-missing-drafts-and-index-sync-v1"
$review = ".\_archive-review\vkr-workbench-missing-drafts-and-index-sync-v1\original-files"

Remove-Item $tmp -Recurse -Force -ErrorAction SilentlyContinue
Expand-Archive -Path $zip -DestinationPath $tmp -Force

$files = @(
  "planning/thesis/vkr-topic-workbench/04-chapter-3-implementation/01-solution-structure-and-implementation-overview.topic.md",
  "planning/slices/L2-APPL-VER-RUN-001.client-applicant-party-verification-panel-run-action.md",
  "planning/thesis/vkr-topic-workbench/README.md",
  "planning/thesis/vkr-topic-workbench/03-chapter-2-design/topic-index.md",
  "planning/thesis/vkr-topic-workbench/04-chapter-3-implementation/README.md",
  "planning/thesis/vkr-topic-workbench/04-chapter-3-implementation/topic-index.md"
)

foreach ($rel in $files) {
  $relWin = $rel -replace '/', '\'
  if (Test-Path $relWin) {
    $backupPath = Join-Path $review $relWin
    New-Item -ItemType Directory -Force -Path (Split-Path $backupPath) | Out-Null
    Copy-Item $relWin $backupPath -Force
  }
}

Copy-Item "$tmp\planning" "." -Recurse -Force
Copy-Item "$tmp\_archive-review" "." -Recurse -Force
Copy-Item "$tmp\MANIFEST.vkr-workbench-missing-drafts-and-index-sync-v1.md" ".\MANIFEST.vkr-workbench-missing-drafts-and-index-sync-v1.md" -Force
Copy-Item "$tmp\APPLY.vkr-workbench-missing-drafts-and-index-sync-v1.md" ".\APPLY.vkr-workbench-missing-drafts-and-index-sync-v1.md" -Force

git status
```

## Check

```powershell
git diff -- planning/thesis/vkr-topic-workbench/README.md
git diff -- planning/thesis/vkr-topic-workbench/03-chapter-2-design/topic-index.md
git diff -- planning/thesis/vkr-topic-workbench/04-chapter-3-implementation
git diff -- planning/slices/L2-APPL-VER-RUN-001.client-applicant-party-verification-panel-run-action.md
```

## Commit

```powershell
git add planning/thesis/vkr-topic-workbench/README.md `
        planning/thesis/vkr-topic-workbench/03-chapter-2-design/topic-index.md `
        planning/thesis/vkr-topic-workbench/04-chapter-3-implementation `
        planning/slices/L2-APPL-VER-RUN-001.client-applicant-party-verification-panel-run-action.md `
        _archive-review/vkr-workbench-missing-drafts-and-index-sync-v1/ `
        MANIFEST.vkr-workbench-missing-drafts-and-index-sync-v1.md `
        APPLY.vkr-workbench-missing-drafts-and-index-sync-v1.md

git commit -m "Sync VKR workbench indexes and add missing Chapter 3 drafts"
```
