# Apply l2-validation-scenario-cleanup-sync

From repository root:

```powershell
Expand-Archive -Path "C:\Users\alexa\Downloads\l2-validation-scenario-cleanup-sync.zip" -DestinationPath . -Force
.\APPLY-l2-validation-cleanup-sync.ps1
git status
git diff -- planning
```

If the diff is correct:

```powershell
git add planning
git status
```

## Notes

This is a docs-only scenario/planning cleanup.

It does not change runtime code, tests, OpenAPI or generated TypeScript types.
