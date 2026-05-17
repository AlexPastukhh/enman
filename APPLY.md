# Apply L2 Agreement Exchange Invariants / Draft Rules Sync

From repo root:

```powershell
Expand-Archive -Path "C:\Users\alexa\Downloads\l2-agreement-exchange-invariants-and-draft-rules-sync.zip" -DestinationPath . -Force
.\APPLY-l2-agreement-exchange-invariants-sync.ps1
git status
git diff -- planning
```

If the diff is OK:

```powershell
git add planning
git status
```

No runtime code, tests or generated artifacts are included.
