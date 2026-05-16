# APPLY

Apply from repository root.

```powershell
cd "C:\enman\enman"
Expand-Archive -Path "C:\Users\alexa\Downloads\enman-vkr-section-draft-existing-solutions-v1.zip" -DestinationPath . -Force
git status
git diff
```

Suggested add/commit:

```powershell
git add vkr-clean/section-drafts/README.md `
        vkr-clean/section-drafts/section-draft-workflow.md `
        vkr-clean/section-drafts/chapter-1/README.md `
        vkr-clean/section-drafts/chapter-1/01-02-existing-solutions-and-own-development.md `
        MANIFEST.md `
        APPLY.md

git commit -m "Add VKR existing solutions section draft"
```

## After applying

Review:

```text
vkr-clean/section-drafts/chapter-1/01-02-existing-solutions-and-own-development.md
```

Then replace `TODO SOURCE` markers with concrete references from the literature/research materials.
