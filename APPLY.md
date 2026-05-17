# APPLY — Employee Request Read Slice Full Drafts Sync

Run from repository root:

```powershell
Expand-Archive -Path "C:\Users\alexa\Downloads\employee-request-read-slices-full-drafts-sync.zip" -DestinationPath . -Force
git status
git diff -- planning
```

If the diff is correct:

```powershell
git add planning
git status
```

This is a docs-only package. It must not change runtime code, tests or generated artifacts.
