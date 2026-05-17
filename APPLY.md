# APPLY — Employee Request Details Client Full Draft Sync

Run from repository root after downloading the archive:

```powershell
Expand-Archive -Path "C:\Users\alexa\Downloads\employee-details-client-full-draft-sync.zip" -DestinationPath . -Force
git status
git diff -- planning
```

If the diff is correct:

```powershell
git add planning
git status
```

This archive is documentation-only. It should not change runtime code, tests or generated artifacts.
