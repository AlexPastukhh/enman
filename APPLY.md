# APPLY — Cross-Cutting Concerns / Antiforgery Drafting Rules Sync

Run from repository root after downloading the archive:

```powershell
Expand-Archive -Path "C:\Users\alexa\Downloads\cross-cutting-concerns-drafting-rules-sync.zip" -DestinationPath . -Force
git status
git diff -- planning
```

If the diff is correct:

```powershell
git add planning
git status
```

This is a docs-only archive. It does not include code, tests or generated artifacts.
