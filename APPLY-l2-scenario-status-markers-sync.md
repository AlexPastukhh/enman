# Apply L2 Scenario Status Markers Sync

Docs-only sync.

From repo root:

```powershell
Expand-Archive -Path "C:\Users\alexa\Downloads\l2-scenario-status-markers-sync.zip" -DestinationPath . -Force
.\APPLY-l2-scenario-status-markers-sync.ps1
git status
git diff -- planning
```

If the diff is OK:

```powershell
git add planning APPLY-l2-scenario-status-markers-sync.ps1 APPLY-l2-scenario-status-markers-sync.md MANIFEST-l2-scenario-status-markers-sync.md
git status
```

No runtime code, tests or generated artifacts are changed.
