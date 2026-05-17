# APPLY — L2 Review Full Drafts Sync

From repo root:

```powershell
Expand-Archive -Path "C:\Users\alexa\Downloads\l2-review-full-drafts-sync.zip" -DestinationPath . -Force
git status
git diff -- planning
```

If the diff is expected:

```powershell
git add planning
git status
```

This is a docs-only archive.

No runtime code, tests or generated artifacts are included.
