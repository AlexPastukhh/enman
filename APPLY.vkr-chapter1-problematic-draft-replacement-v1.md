# APPLY: vkr-chapter1-problematic-draft-replacement-v1

## Safe apply from repository root

```powershell
cd "C:\enman\enman"

$zip = "C:\Users\alexa\Downloads\vkr-chapter1-problematic-draft-replacement-v1.zip"
$tmp = "$env:TEMP\vkr-chapter1-problematic-draft-replacement-v1"
$review = ".\_archive-review\vkr-chapter1-problematic-draft-replacement-v1\original-files"

Remove-Item $tmp -Recurse -Force -ErrorAction SilentlyContinue
Expand-Archive -Path $zip -DestinationPath $tmp -Force

$files = @(
  "planning/thesis/vkr-topic-workbench/02-chapter-1-analysis/03-problematic/01-manual-process-problems.topic.md"
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
Copy-Item "$tmp\MANIFEST.vkr-chapter1-problematic-draft-replacement-v1.md" ".\MANIFEST.vkr-chapter1-problematic-draft-replacement-v1.md" -Force
Copy-Item "$tmp\APPLY.vkr-chapter1-problematic-draft-replacement-v1.md" ".\APPLY.vkr-chapter1-problematic-draft-replacement-v1.md" -Force

git status
```

## Check diff

```powershell
git diff -- planning/thesis/vkr-topic-workbench/02-chapter-1-analysis/03-problematic/01-manual-process-problems.topic.md
```

## Add and commit

```powershell
git add planning/thesis/vkr-topic-workbench/02-chapter-1-analysis/03-problematic/01-manual-process-problems.topic.md `
        _archive-review/vkr-chapter1-problematic-draft-replacement-v1/ `
        MANIFEST.vkr-chapter1-problematic-draft-replacement-v1.md `
        APPLY.vkr-chapter1-problematic-draft-replacement-v1.md

git commit -m "Replace VKR Chapter 1 problematic topic draft"
```
