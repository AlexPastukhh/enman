# Apply — enman-vkr-sync-after-employee-agreement-flow-v1

```powershell
cd "C:\enman\enman"
Expand-Archive -Path "C:\Users\alexa\Downloads\enman-vkr-sync-after-employee-agreement-flow-v1.zip" -DestinationPath . -Force
git status
```

Recommended diffs:

```powershell
git diff -- planning/vkr-work-context-current.md
git diff -- planning/thesis/vkr-clean/vkr-materials-index.md
git diff -- planning/thesis/vkr-clean/clean-results-and-future-work.md
git diff -- planning/thesis/vkr-clean/clean-ui-description.md
git diff -- planning/thesis/vkr-clean/clean-domain-model.md
git diff -- planning/thesis/vkr-clean/clean-database-design.md
git diff -- planning/thesis/vkr-clean/clean-testing.md
git diff -- planning/thesis/vkr-clean/visuals-and-diagrams-plan.md
git diff -- planning/thesis/vkr-clean/playwright-screenshot-plan.md
git diff -- planning/thesis/vkr-clean/section-drafts/section-draft-register.md
```

Add and commit:

```powershell
git add planning/vkr-work-context-current.md `
        planning/thesis/vkr-clean/vkr-materials-index.md `
        planning/thesis/vkr-clean/clean-architecture.md `
        planning/thesis/vkr-clean/clean-requirements.md `
        planning/thesis/vkr-clean/clean-domain-model.md `
        planning/thesis/vkr-clean/clean-database-design.md `
        planning/thesis/vkr-clean/clean-ui-description.md `
        planning/thesis/vkr-clean/clean-testing.md `
        planning/thesis/vkr-clean/clean-results-and-future-work.md `
        planning/thesis/vkr-clean/visuals-and-diagrams-plan.md `
        planning/thesis/vkr-clean/playwright-screenshot-plan.md `
        planning/thesis/vkr-clean/section-drafts/section-draft-register.md

git commit -m "Sync VKR materials with employee and agreement flow"
```

Optional: do not commit `MANIFEST.md` and `APPLY.md` if you treat archive service files as disposable.
