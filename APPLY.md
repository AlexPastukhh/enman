# Apply — L2 Agreement Exchange Slice Family Sync

From repository root:

```powershell
Expand-Archive -Path "C:\Users\alexa\Downloads\l2-agreement-exchange-slice-family-sync.zip" -DestinationPath . -Force

git status
git diff -- planning/slices
```

If diff is correct:

```powershell
git add planning/slices
git status
```

This is docs-only. Do not expect runtime code, tests or generated OpenAPI/type changes.
