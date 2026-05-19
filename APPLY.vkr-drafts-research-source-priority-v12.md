# APPLY: vkr-drafts-research-source-priority-v12

## Simple apply

```powershell
cd "C:\enman\enman"

Expand-Archive -Path "C:\Users\alexa\Downloads\enman-vkr-drafts-research-source-priority-v12.zip" -DestinationPath . -Force

git status
```

## Verify important files

```powershell
git diff -- planning/thesis/vkr-topic-workbench/02-chapter-1-analysis/02-document-agreement-feedback/01-document-agreement-feedback.topic.md
git diff -- planning/thesis/vkr-topic-workbench/02-chapter-1-analysis/03-manual-process-problems/01-manual-process-problems.topic.md
git diff -- planning/thesis/vkr-topic-workbench/00-research-materials/research-index.md
git diff -- planning/thesis/chat-action-algorithms/evidence-and-materials/source-material-harvest-for-topic.md
git diff -- planning/thesis/VKR-RESOURCE-MAP.md
```

## Verify research files exist

```powershell
Test-Path "planning\thesis\vkr-topic-workbench\00-research-materials\chapter-1\literature-and-theory-sources.md"
Test-Path "planning\thesis\vkr-topic-workbench\00-research-materials\chapter-1\automation-solutions-comparison.md"
```

## Commit

```powershell
git add planning/thesis MANIFEST.vkr-drafts-research-source-priority-v12.md APPLY.vkr-drafts-research-source-priority-v12.md
git commit -m "Add VKR chapter 1 drafts and research source priority"
git push origin my-changes
```
