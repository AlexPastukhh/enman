# APPLY — review-start-client-full-draft-sync

From repository root:

```powershell
Expand-Archive -Path "C:\Users\alexa\Downloads\review-start-client-full-draft-sync.zip" -DestinationPath . -Force
git status
git diff -- planning
```

If the diff is correct:

```powershell
git add planning
git status
```

This package is docs-only.
