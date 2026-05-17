# APPLY — Full Slices + Server Test Draft Rules Sync

Run from repository root:

```powershell
Expand-Archive -Path "C:\Users\alexa\Downloads\full-slices-server-test-rules-sync.zip" -DestinationPath . -Force
git status
git diff -- planning
```

If the diff is correct:

```powershell
git add planning
git status
```

This is docs-only. Do not run code generation for this archive unless you separately implement API changes.
