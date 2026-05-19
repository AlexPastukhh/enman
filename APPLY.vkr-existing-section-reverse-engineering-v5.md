# APPLY: vkr-existing-section-reverse-engineering-v5

## Simple apply

```powershell
cd "C:\enman\enman"

$tmp = "$env:TEMP\enman-vkr-existing-section-reverse-engineering-v5"
Remove-Item $tmp -Recurse -Force -ErrorAction SilentlyContinue
Expand-Archive -Path "C:\Users\alexa\Downloads\enman-vkr-existing-section-reverse-engineering-v5.zip" -DestinationPath $tmp -Force

Copy-Item "$tmp\planning" "." -Recurse -Force
Copy-Item "$tmp\_archive-review" "." -Recurse -Force
Copy-Item "$tmp\MANIFEST.vkr-existing-section-reverse-engineering-v5.md" "." -Force
Copy-Item "$tmp\APPLY.vkr-existing-section-reverse-engineering-v5.md" "." -Force

git status
```

## Check diffs

```powershell
git diff -- planning/thesis/VKR-WORKFLOW-SOURCE-OF-TRUTH.md
git diff -- planning/thesis/VKR-RESOURCE-MAP.md
git diff -- planning/thesis/NEW-CHAT-ONBOARDING.md
git diff -- planning/thesis/chat-action-algorithms/cleanup-and-legacy/existing-section-draft-reverse-engineering.md
git diff -- planning/thesis/chat-action-algorithms/cleanup-and-legacy/existing-chapter-draft-review.md
git diff -- planning/thesis/vkr-topic-workbench/00-workflow-and-rules/topic-card-template.md
git diff -- planning/thesis/vkr-topic-workbench/TOPIC-TO-SECTION-BLOCK-WORKFLOW.md
git diff -- planning/thesis/vkr-clean/section-drafts/README.md
```

## Commit

```powershell
git add planning/thesis `
        _archive-review/2026-05-19-vkr-existing-section-reverse-engineering-v5 `
        MANIFEST.vkr-existing-section-reverse-engineering-v5.md `
        APPLY.vkr-existing-section-reverse-engineering-v5.md

git commit -m "Add VKR existing section draft reverse-engineering workflow"
git push origin my-changes
```
