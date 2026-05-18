# APPLY: vkr-chapter2-architecture-overview-draft-addition-v1

## Apply from repository root

```powershell
cd "C:\enman\enman"

$zip = "C:\Users\alexa\Downloads\vkr-chapter2-architecture-overview-draft-addition-v1.zip"
$tmp = "$env:TEMP\vkr-chapter2-architecture-overview-draft-addition-v1"
$review = ".\_archive-review\vkr-chapter2-architecture-overview-draft-addition-v1\original-files"

Remove-Item $tmp -Recurse -Force -ErrorAction SilentlyContinue
Expand-Archive -Path $zip -DestinationPath $tmp -Force

$files = @(
  "planning/thesis/vkr-topic-workbench/03-chapter-2-design/03-architecture/01-application-architecture-overview.topic.md"
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
Copy-Item "$tmp\MANIFEST.vkr-chapter2-architecture-overview-draft-addition-v1.md" ".\MANIFEST.vkr-chapter2-architecture-overview-draft-addition-v1.md" -Force
Copy-Item "$tmp\APPLY.vkr-chapter2-architecture-overview-draft-addition-v1.md" ".\APPLY.vkr-chapter2-architecture-overview-draft-addition-v1.md" -Force

git status
```

## Check diff

```powershell
git diff -- planning/thesis/vkr-topic-workbench/03-chapter-2-design/03-architecture/01-application-architecture-overview.topic.md
```

## Commit

```powershell
git add planning/thesis/vkr-topic-workbench/03-chapter-2-design/03-architecture/01-application-architecture-overview.topic.md `
        _archive-review/vkr-chapter2-architecture-overview-draft-addition-v1/ `
        MANIFEST.vkr-chapter2-architecture-overview-draft-addition-v1.md `
        APPLY.vkr-chapter2-architecture-overview-draft-addition-v1.md

git commit -m "Add VKR Chapter 2 architecture overview topic draft"
```
