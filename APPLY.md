# Apply — L2 Agreement Exchange Read Slices Sync

From repo root:

```powershell
Expand-Archive -Path "C:\Users\alexa\Downloads\l2-agr-exch-read-slices-sync.zip" -DestinationPath . -Force
.\APPLY-l2-agr-exch-read-slices-sync.ps1
git status
git diff -- planning
```

If diff is acceptable:

```powershell
git add planning
git status
```

## What this package does

- Adds canonical server slice drafts for Agreement Exchange list/details reads.
- Adds canonical client sidecar drafts for Agreement Exchange list/details pages.
- Renumbers Accept and Final Refuse to avoid conflict:
  - Accept becomes `SL-AGR-EXCH-005`.
  - Final Refuse becomes `SL-AGR-EXCH-006`.
- Removes superseded conflicting file names through the apply script.
- Appends navigation/register addenda with stable markers.

No runtime code, no tests, no generated artifacts.
