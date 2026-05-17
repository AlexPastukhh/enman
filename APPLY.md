# Apply — L2 Agreement Exchange Command Slices Sync

From repository root:

```powershell
Expand-Archive -Path "C:\Users\alexa\Downloads\l2-agr-exch-command-slices-sync.zip" -DestinationPath . -Force
.\APPLY-l2-agr-exch-command-slices-sync.ps1
git status
git diff -- planning
```

If the diff is correct:

```powershell
git add planning
git status
```

This is a docs-only sync.
