# APPLY — SL-APPL-003.client Full Draft Sync

Run from repository root:

```powershell
Expand-Archive -Path "C:\Users\alexa\Downloads\sl-appl-003-client-full-draft-sync.zip" -DestinationPath . -Force
git status
git diff -- planning
```

If the diff is correct:

```powershell
git add planning
git status
```

This is a docs-only archive. It intentionally does not include runtime code, tests, generated artifacts or GitHub writes.
