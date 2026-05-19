# APPLY: vkr-source-pass-full-text-v11

## Simple apply

```powershell
cd "C:\enman\enman"

Expand-Archive -Path "C:\Users\alexa\Downloads\enman-vkr-source-pass-full-text-v11.zip" -DestinationPath . -Force

git status
```

## Verify

```powershell
git diff -- planning/thesis/chat-action-algorithms/full-text-generation/vkr-full-text-version-generation.md
git diff -- planning/thesis/chat-action-algorithms/drafting/topic-clarify-and-recheck-flow.md
git diff -- planning/thesis/vkr-topic-workbench/02-chapter-1-analysis/01-domain-process/01-client-request-process.full-topic-draft-v12.md
git diff -- planning/thesis/vkr-topic-workbench/VKR-DRAFTING-ROADMAP.md
git diff -- planning/thesis/VKR-WORKFLOW-SOURCE-OF-TRUTH.md
```

## Commit

```powershell
git add planning/thesis MANIFEST.vkr-source-pass-full-text-v11.md APPLY.vkr-source-pass-full-text-v11.md
git commit -m "Add VKR source-pass and full-text generation workflow"
git push origin my-changes
```
