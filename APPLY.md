# APPLY

Apply from repository root.

```powershell
cd "C:\enman\enman"
Expand-Archive -Path "C:\Users\alexa\Downloads\enman-vkr-sync-after-l1-client-baseline-v1.zip" -DestinationPath . -Force
git status
git diff
```

Review the diff. If it looks correct:

```powershell
git add planning/vkr-work-context-current.md `
        vkr-clean/README.md `
        vkr-clean/vkr-materials-index.md `
        vkr-clean/vkr-outline.md `
        vkr-clean/clean-requirements.md `
        vkr-clean/functional-specification.md `
        vkr-clean/use-case-diagrams-plan.md `
        vkr-clean/clean-architecture.md `
        vkr-clean/api-contract-and-client-server-sync.md `
        vkr-clean/clean-ui-description.md `
        vkr-clean/clean-testing.md `
        vkr-clean/visuals-and-diagrams-plan.md `
        vkr-clean/planning-to-vkr-extraction-map.md `
        MANIFEST.md `
        APPLY.md

git commit -m "Sync VKR docs with L1 client baseline"
```
