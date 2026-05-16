# Apply — enman-vkr-writing-protocol-and-pilot-section-v1

From repository root:

```powershell
cd "C:\enman\enman"
Expand-Archive -Path "C:\Users\alexa\Downloads\enman-vkr-writing-protocol-and-pilot-section-v1.zip" -DestinationPath . -Force
git status
git diff
```

Recommended add/commit command:

```powershell
git add vkr-clean/vkr-materials-index.md `
        vkr-clean/writing-protocol/README.md `
        vkr-clean/writing-protocol/source-provenance-protocol.md `
        vkr-clean/writing-protocol/section-card-template.md `
        vkr-clean/writing-protocol/research-usage-rules.md `
        vkr-clean/writing-protocol/chapter-section-question-map.md `
        vkr-clean/writing-protocol/page-fragment-checklist.md `
        vkr-clean/writing-protocol/pilot-section-existing-solutions.md `
        MANIFEST.md `
        APPLY.md

git commit -m "Add VKR writing protocol and pilot section draft"
```

## After Applying

1. Open `vkr-clean/writing-protocol/README.md`.
2. Review `pilot-section-existing-solutions.md`.
3. Use the pilot section to create a cleaner final subsection for chapter 1.
4. Add sources from the literature/research package where `TODO SOURCE` markers appear.
