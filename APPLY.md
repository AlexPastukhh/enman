# APPLY — enman-vkr-section-1-1-full-draft-v1.zip

Apply from repository root:

```powershell
cd "C:\enman\enman"
Expand-Archive -Path "C:\Users\alexa\Downloads\enman-vkr-section-1-1-full-draft-v1.zip" -DestinationPath . -Force
git status
git diff
```

If the diff looks correct:

```powershell
git add vkr-clean/section-drafts/README.md `
        vkr-clean/section-drafts/chapter-1/01-01-problem-domain.full-v1.md `
        vkr-clean/section-drafts/chapter-1/01-01-problem-domain.fragment-candidates.md `
        vkr-clean/section-drafts/chapter-1/01-01-problem-domain.full-v1-review-notes.md `
        MANIFEST.md `
        APPLY.md

git commit -m "Add VKR problem domain full draft v1"
```
