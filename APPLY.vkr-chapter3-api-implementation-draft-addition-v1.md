# APPLY — vkr-chapter3-api-implementation-draft-addition-v1

## PowerShell apply command

```powershell
cd "C:\enman\enman"

$zip = "C:\Users\alexa\Downloads\vkr-chapter3-api-implementation-draft-addition-v1.zip"
$tmp = "$env:TEMP\vkr-chapter3-api-implementation-draft-addition-v1"
$review = ".\_archive-review\vkr-chapter3-api-implementation-draft-addition-v1\original-files"

Remove-Item $tmp -Recurse -Force -ErrorAction SilentlyContinue
Expand-Archive -Path $zip -DestinationPath $tmp -Force

$files = @(
  "planning/thesis/vkr-topic-workbench/04-chapter-3-implementation/03-api-implementation.topic.md"
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
Copy-Item "$tmp\MANIFEST.vkr-chapter3-api-implementation-draft-addition-v1.md" ".\MANIFEST.vkr-chapter3-api-implementation-draft-addition-v1.md" -Force
Copy-Item "$tmp\APPLY.vkr-chapter3-api-implementation-draft-addition-v1.md" ".\APPLY.vkr-chapter3-api-implementation-draft-addition-v1.md" -Force

git status
```

## Check

```powershell
git diff -- planning/thesis/vkr-topic-workbench/04-chapter-3-implementation/03-api-implementation.topic.md
```

## Commit

```powershell
git add planning/thesis/vkr-topic-workbench/04-chapter-3-implementation/03-api-implementation.topic.md `
        _archive-review/vkr-chapter3-api-implementation-draft-addition-v1/ `
        MANIFEST.vkr-chapter3-api-implementation-draft-addition-v1.md `
        APPLY.vkr-chapter3-api-implementation-draft-addition-v1.md

git commit -m "Add VKR Chapter 3 API implementation topic draft"
```
