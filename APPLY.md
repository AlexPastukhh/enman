# Apply

From repo root:

```powershell
Expand-Archive -Path "C:\Users\alexa\Downloads\client-api-placement-architecture-sync.zip" -DestinationPath . -Force
git status
git diff -- planning
```

If diff is correct:

```powershell
git add planning
git status
```

Docs-only package. No runtime code, tests or generated artifacts.
