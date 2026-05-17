# APPLY — Employee Dashboard + Start Review Full Drafts

Run from repository root after downloading the archive:

```powershell
Expand-Archive -Path "C:\Users\alexa\Downloads\employee-dashboard-start-review-full-drafts-sync.zip" -DestinationPath . -Force
git status
git diff -- planning
```

If the diff is correct:

```powershell
git add planning
git status
```

This is a docs-only package. It should not change backend/client runtime code, tests or generated artifacts.
