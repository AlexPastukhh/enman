# APPLY — enman-vkr-topic-draft-workflow-update-v1

## Simple apply

```powershell
cd "C:\enman\enman"

Expand-Archive -Path "C:\Users\alexa\Downloads\enman-vkr-topic-draft-workflow-update-v1.zip" -DestinationPath . -Force

git status
```

## Check diffs

```powershell
git diff -- planning/thesis/vkr-topic-workbench/00-workflow-and-rules/workflow.md
git diff -- planning/thesis/vkr-topic-workbench/00-workflow-and-rules/topic-card-template.md
git diff -- planning/thesis/vkr-topic-workbench/00-workflow-and-rules/visual-evidence-protocol.md
git diff -- planning/thesis/vkr-topic-workbench/00-workflow-and-rules/mock-and-extension-point-wording-protocol.md
git diff -- planning/thesis/vkr-topic-workbench/00-workflow-and-rules/previous-topic-example-usage-rule.md
git diff -- planning/thesis/vkr-topic-workbench/00-workflow-and-rules/author-materials/raw-author-message-log.md
```

## Add and commit

```powershell
git add planning/thesis/vkr-topic-workbench `
        _archive-review/2026-05-18-topic-draft-workflow-update-v1 `
        MANIFEST.md `
        APPLY.md

git commit -m "Update VKR topic draft workflow"
```

## Safer apply if MANIFEST.md / APPLY.md should not be overwritten

```powershell
cd "C:\enman\enman"

$tmp = "$env:TEMP\vkr-topic-draft-workflow-update-v1"
Remove-Item $tmp -Recurse -Force -ErrorAction SilentlyContinue
Expand-Archive -Path "C:\Users\alexa\Downloads\enman-vkr-topic-draft-workflow-update-v1.zip" -DestinationPath $tmp -Force

Copy-Item "$tmp\planning" "." -Recurse -Force
Copy-Item "$tmp\_archive-review" "." -Recurse -Force
Copy-Item "$tmp\MANIFEST.md" ".\MANIFEST.vkr-topic-draft-workflow-update-v1.md" -Force
Copy-Item "$tmp\APPLY.md" ".\APPLY.vkr-topic-draft-workflow-update-v1.md" -Force

git status
```

Then add:

```powershell
git add planning/thesis/vkr-topic-workbench `
        _archive-review/2026-05-18-topic-draft-workflow-update-v1 `
        MANIFEST.vkr-topic-draft-workflow-update-v1.md `
        APPLY.vkr-topic-draft-workflow-update-v1.md

git commit -m "Update VKR topic draft workflow"
```
