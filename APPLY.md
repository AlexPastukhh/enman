# APPLY — enman-vkr-chapter1-workbench-refine-v1

This is a safe merge archive with replacement files and original snapshots.

## Apply safely

```powershell
cd "C:\enman\enman"

$tmp = "$env:TEMP\enman-vkr-chapter1-workbench-refine-v1"
Remove-Item $tmp -Recurse -Force -ErrorAction SilentlyContinue

Expand-Archive -Path "C:\Users\alexa\Downloads\enman-vkr-chapter1-workbench-refine-v1.zip" -DestinationPath $tmp -Force

Copy-Item -Path "$tmp\planning" -Destination "." -Recurse -Force
Copy-Item -Path "$tmp\_archive-review" -Destination "." -Recurse -Force
Copy-Item "$tmp\MANIFEST.md" ".\MANIFEST.vkr-chapter1-workbench-refine-v1.md"
Copy-Item "$tmp\APPLY.md" ".\APPLY.vkr-chapter1-workbench-refine-v1.md"

git status
```

## Review after applying

```powershell
git diff -- planning/thesis/vkr-topic-workbench/02-chapter-1-analysis/README.md
git diff -- planning/thesis/vkr-topic-workbench/02-chapter-1-analysis/topic-index.md
git diff -- planning/thesis/vkr-topic-workbench/00-workflow-and-rules/author-materials/raw-author-message-log.md
git diff -- planning/thesis/vkr-topic-workbench/02-chapter-1-analysis/01-domain-process/01-client-request-process.topic.md
```

## Add to git

```powershell
git add planning/thesis/vkr-topic-workbench `
        _archive-review/2026-05-18-chapter-1-workbench-refine-v1 `
        MANIFEST.vkr-chapter1-workbench-refine-v1.md `
        APPLY.vkr-chapter1-workbench-refine-v1.md

git commit -m "Refine VKR Chapter 1 topic workbench"
```

## Important

Do not delete legacy Chapter 1 folders from the first workbench version yet. They are intentionally left for a later cleanup/merge review.
