# Apply — VKR workflow block drafting v2

## Safe apply

```powershell
cd "C:\enman\enman"

$tmp = "$env:TEMP\enman-vkr-workflow-block-drafting-v2"
Remove-Item $tmp -Recurse -Force -ErrorAction SilentlyContinue
Expand-Archive -Path "C:\Users\alexa\Downloads\enman-vkr-workflow-block-drafting-v2.zip" -DestinationPath $tmp -Force

Copy-Item "$tmp\planning" "." -Recurse -Force
Copy-Item "$tmp\_archive-review" "." -Recurse -Force
Copy-Item "$tmp\MANIFEST.vkr-workflow-block-drafting-v2.md" "." -Force
Copy-Item "$tmp\APPLY.vkr-workflow-block-drafting-v2.md" "." -Force

git status
```

## Check

```powershell
git diff -- planning/thesis/VKR-WORKFLOW-SOURCE-OF-TRUTH.md
git diff -- planning/thesis/README.md
git diff -- planning/thesis/NEW-CHAT-ONBOARDING.md
git diff -- planning/thesis/vkr-topic-workbench/TOPIC-TO-SECTION-BLOCK-WORKFLOW.md
git diff -- planning/thesis/vkr-topic-workbench/00-workflow-and-rules/workflow.md
git diff -- planning/thesis/vkr-topic-workbench/00-workflow-and-rules/topic-card-template.md
git diff -- planning/thesis/vkr-clean/section-drafts/README.md
git diff -- planning/thesis/vkr-clean/section-drafts/fragment-bank.md
git diff -- planning/thesis/chat-action-algorithms/README.md
```

## Add

```powershell
git add planning/thesis `
        _archive-review/2026-05-19-vkr-workflow-block-drafting-v2 `
        MANIFEST.vkr-workflow-block-drafting-v2.md `
        APPLY.vkr-workflow-block-drafting-v2.md

git commit -m "Update VKR workflow to block-based drafting"
```

## Notes

No files are deleted by this archive. Legacy/support materials are marked but kept for harvest and later cleanup.
