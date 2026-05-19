# APPLY — VKR drafting algorithms v4

## Simple apply command

```powershell
cd "C:\enman\enman"

$tmp = "$env:TEMP\enman-vkr-drafting-algorithms-v4"
Remove-Item $tmp -Recurse -Force -ErrorAction SilentlyContinue
Expand-Archive -Path "C:\Users\alexa\Downloads\enman-vkr-drafting-algorithms-v4.zip" -DestinationPath $tmp -Force

Copy-Item "$tmp\planning" "." -Recurse -Force
Copy-Item "$tmp\_archive-review" "." -Recurse -Force
Copy-Item "$tmp\MANIFEST.vkr-drafting-algorithms-v4.md" "." -Force
Copy-Item "$tmp\APPLY.vkr-drafting-algorithms-v4.md" "." -Force
Copy-Item "$tmp\DELETIONS.vkr-drafting-algorithms-v4.md" "." -Force

Remove-Item ".\planning\thesis\vkr-clean\section-drafts\fragment-bank.md" -Force -ErrorAction SilentlyContinue
Remove-Item ".\planning\thesis\chat-action-algorithms\topic-draft-generation.md" -Force -ErrorAction SilentlyContinue
Remove-Item ".\planning\thesis\chat-action-algorithms\section-draft-generation.md" -Force -ErrorAction SilentlyContinue
Remove-Item ".\planning\thesis\chat-action-algorithms\visual-material-review.md" -Force -ErrorAction SilentlyContinue
Remove-Item ".\planning\thesis\chat-action-algorithms\repo-check-before-implementation-text.md" -Force -ErrorAction SilentlyContinue

git status
```

## Suggested diff checks

```powershell
git diff -- planning/thesis/VKR-WORKFLOW-SOURCE-OF-TRUTH.md
git diff -- planning/thesis/VKR-RESOURCE-MAP.md
git diff -- planning/thesis/NEW-CHAT-ONBOARDING.md
git diff -- planning/thesis/chat-action-algorithms/README.md
git diff -- planning/thesis/chat-action-algorithms/drafting/question-priority.md
git diff -- planning/thesis/chat-action-algorithms/cleanup-and-legacy/existing-chapter-draft-review.md
git diff -- planning/thesis/vkr-clean/README.md
git diff -- planning/thesis/vkr-clean/existing-chapter-drafts/README.md
git diff -- planning/thesis/vkr-topic-workbench/00-workflow-and-rules/topic-card-template.md
```

## Add and commit

```powershell
git add planning/thesis `
        _archive-review/2026-05-19-vkr-drafting-algorithms-v4 `
        MANIFEST.vkr-drafting-algorithms-v4.md `
        APPLY.vkr-drafting-algorithms-v4.md `
        DELETIONS.vkr-drafting-algorithms-v4.md

git add -u planning/thesis

git commit -m "Add VKR existing chapter draft resource workflow"
```
