# APPLY — vkr-chapter2-request-review-ui-flow-draft-replacement-v2

## PowerShell apply command

```powershell
cd "C:\enman\enman"

$zip = "C:\Users\alexa\Downloads\vkr-chapter2-request-review-ui-flow-draft-replacement-v2.zip"
$tmp = "$env:TEMP\vkr-chapter2-request-review-ui-flow-draft-replacement-v2"
$review = ".\_archive-review\vkr-chapter2-request-review-ui-flow-draft-replacement-v2\original-files"

Remove-Item $tmp -Recurse -Force -ErrorAction SilentlyContinue
Expand-Archive -Path $zip -DestinationPath $tmp -Force

$files = @(
  "planning/thesis/vkr-topic-workbench/03-chapter-2-design/06-user-interface/02-request-and-review-ui-flow.topic.md"
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
Copy-Item "$tmp\MANIFEST.vkr-chapter2-request-review-ui-flow-draft-replacement-v2.md" ".\MANIFEST.vkr-chapter2-request-review-ui-flow-draft-replacement-v2.md" -Force
Copy-Item "$tmp\APPLY.vkr-chapter2-request-review-ui-flow-draft-replacement-v2.md" ".\APPLY.vkr-chapter2-request-review-ui-flow-draft-replacement-v2.md" -Force

git status
```

## Check

```powershell
git diff -- planning/thesis/vkr-topic-workbench/03-chapter-2-design/06-user-interface/02-request-and-review-ui-flow.topic.md
```

## Commit

```powershell
git add planning/thesis/vkr-topic-workbench/03-chapter-2-design/06-user-interface/02-request-and-review-ui-flow.topic.md `
        _archive-review/vkr-chapter2-request-review-ui-flow-draft-replacement-v2/ `
        MANIFEST.vkr-chapter2-request-review-ui-flow-draft-replacement-v2.md `
        APPLY.vkr-chapter2-request-review-ui-flow-draft-replacement-v2.md

git commit -m "Replace VKR Chapter 2 request review UI flow topic draft"
```
