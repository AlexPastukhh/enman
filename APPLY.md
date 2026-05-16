# APPLY

Run from repository root.

```powershell
Expand-Archive -Path "C:\Users\alexa\Downloads\sl-appl-002-full-draft-and-scope-rules.zip" -DestinationPath . -Force
git status
git diff -- planning
```

If the diff is correct:

```powershell
git add planning
git status
```

No generated artifacts or runtime code are included in this archive.
