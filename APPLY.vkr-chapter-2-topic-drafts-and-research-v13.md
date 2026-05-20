# APPLY: vkr-chapter-2-topic-drafts-and-research-v13

## Simple apply

```powershell
cd "C:\enman\enman"

Expand-Archive -Path "C:\Users\alexa\Downloads\enman-vkr-chapter-2-topic-drafts-and-research-v13.zip" -DestinationPath . -Force

git status
```

## Verify

```powershell
Test-Path "planning\thesis\vkr-topic-workbench\03-chapter-2-design\01-general-application-algorithm\01-general-application-algorithm.topic.md"
Test-Path "planning\thesis\vkr-topic-workbench\03-chapter-2-design\09-cad-backup\01-cad-backup.topic.md"
Test-Path "planning\thesis\vkr-topic-workbench\03-chapter-2-design\CHAPTER-2-SOURCE-MAP.md"
Test-Path "planning\thesis\vkr-topic-workbench\00-research-materials\chapter-2\architecture-and-ddd-approaches.md"
```

## Commit

```powershell
git add planning/thesis MANIFEST.vkr-chapter-2-topic-drafts-and-research-v13.md APPLY.vkr-chapter-2-topic-drafts-and-research-v13.md
git commit -m "Add VKR chapter 2 topic drafts and architecture research"
git push origin my-changes
```
