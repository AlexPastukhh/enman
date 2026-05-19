# APPLY: vkr-semantic-point-coverage-v9

## Simple apply

```powershell
cd "C:\enman\enman"

Expand-Archive -Path "C:\Users\alexa\Downloads\enman-vkr-semantic-point-coverage-v9.zip" -DestinationPath . -Force

git status
```

## Verify

```powershell
git diff -- planning/thesis/vkr-topic-workbench/00-workflow-and-rules/topic-card-template.md
git diff -- planning/thesis/chat-action-algorithms/drafting/semantic-point-discovery.md
git diff -- planning/thesis/vkr-topic-workbench/VKR-DRAFTING-ROADMAP.md
git diff -- planning/thesis/vkr-topic-workbench/02-chapter-1-analysis/CHAPTER-1-ROADMAP.md
git diff -- planning/thesis/VKR-WORKFLOW-SOURCE-OF-TRUTH.md
```

## Commit

```powershell
git add planning/thesis MANIFEST.vkr-semantic-point-coverage-v9.md APPLY.vkr-semantic-point-coverage-v9.md
git commit -m "Add VKR semantic point desired-result coverage workflow"
git push origin my-changes
```
