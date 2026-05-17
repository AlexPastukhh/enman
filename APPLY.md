# Apply — L2 Agreement Exchange Draft / Rules / Naming Sync

From repository root:

```powershell
Expand-Archive -Path "C:\Users\alexa\Downloads\l2-agr-exch-001-draft-rules-and-naming-sync.zip" -DestinationPath . -Force
.\APPLY-l2-agr-exch-001-rules-and-naming-sync.ps1
git status
git diff -- planning
```

If the diff is correct:

```powershell
git add planning
git status
```

This is a docs-only archive. It does not include runtime code, tests or generated artifacts.
