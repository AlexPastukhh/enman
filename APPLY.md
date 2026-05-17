# Apply — L2 Agreement Exchange Final Refuse Slices Sync

From repository root:

```powershell
Expand-Archive -Path "C:\Users\alexa\Downloads\l2-agr-exch-final-refuse-slices-sync.zip" -DestinationPath . -Force
.\APPLY-l2-agr-exch-final-refuse-slices-sync.ps1
git status
git diff -- planning
```

If diff is OK:

```powershell
git add planning
git status
```

Docs-only archive. No runtime code, tests, or generated artifacts.
