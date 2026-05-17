# APPLY — L2 Employee / Review / Agreement Scenario Sync v2

Run from repository root:

```powershell
Expand-Archive -Path "C:\Users\alexa\Downloads\l2-employee-review-agreement-scenarios-sync-v2.zip" -DestinationPath . -Force
git status
git diff -- planning
```

If the diff is correct:

```powershell
git add planning
git status
```

This is docs-only. It intentionally does not include code, tests, generated artifacts, GitHub branch/commit/PR or implementation slice drafts.
