# APPLY — L1 Current Status Documentation Sync

Run from repository root:

```powershell
Expand-Archive -Path "C:\Users\alexa\Downloads\l1-current-status-docs-sync.zip" -DestinationPath . -Force
git status
git diff -- planning
```

If the diff is correct:

```powershell
git add planning
git status
```

This archive is docs-only. It must not change backend/client/runtime code, tests or generated artifacts.
