# APPLY

Apply from repository root.

```powershell
cd "C:\enman\enman"
Expand-Archive -Path "C:\Users\alexa\Downloads\enman-vkr-methodical-and-intro-pack-v1.zip" -DestinationPath . -Force
git status
git diff
```

If the diff is acceptable:

```powershell
git add vkr-clean/format-and-methodical-requirements.md `
        vkr-clean/definitions-abbreviations.md `
        vkr-clean/introduction-draft.md `
        vkr-clean/existing-solutions-analysis.md `
        vkr-clean/literature-plan.md `
        vkr-clean/vkr-materials-index.md `
        MANIFEST.md `
        APPLY.md

git commit -m "Add VKR methodical and introduction materials"
```
