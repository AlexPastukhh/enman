# APPLY: vkr-roadmap-capture-v8

## Simple apply

```powershell
cd "C:\enman\enman"

Expand-Archive -Path "C:\Users\alexa\Downloads\enman-vkr-roadmap-capture-v8.zip" -DestinationPath . -Force

git status
```

## Verify

```powershell
git diff -- planning/thesis/vkr-topic-workbench/VKR-DRAFTING-ROADMAP.md
git diff -- planning/thesis/vkr-topic-workbench/02-chapter-1-analysis/CHAPTER-1-ROADMAP.md
git diff -- planning/thesis/chat-action-algorithms/drafting/raw-notes-capture-and-distribution.md
git diff -- planning/thesis/VKR-WORKFLOW-SOURCE-OF-TRUTH.md
git diff -- planning/thesis/VKR-RESOURCE-MAP.md
```

## Commit

```powershell
git add planning/thesis MANIFEST.vkr-roadmap-capture-v8.md APPLY.vkr-roadmap-capture-v8.md
git commit -m "Add VKR drafting roadmap and raw notes capture workflow"
git push origin my-changes
```
