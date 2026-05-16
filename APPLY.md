# APPLY

Apply from repository root.

```powershell
cd "C:\enman\enman"
Expand-Archive -Path "C:\Users\alexa\Downloads\enman-vkr-section-drafting-workflow-v2.zip" -DestinationPath . -Force
git status
git diff
```

If the diff is correct:

```powershell
git add vkr-clean/vkr-materials-index.md `
        vkr-clean/section-drafts/README.md `
        vkr-clean/section-drafts/vkr-section-drafting-workflow.md `
        vkr-clean/section-drafts/short-draft-template.md `
        vkr-clean/section-drafts/full-draft-template.md `
        vkr-clean/section-drafts/full-draft-review-checklist.md `
        vkr-clean/section-drafts/fragment-bank.md `
        vkr-clean/section-drafts/section-draft-register.md `
        MANIFEST.md `
        APPLY.md

git commit -m "Update VKR section drafting workflow"
```

## After Apply

Do not immediately create a full draft.

Next normal action:

```text
Write the short draft for section 1.1 in chat.
Discuss/revise it.
Then create the first full draft attempt if the short draft is accepted.
```
