# APPLY — L2 Account / Employee TPH Domain Sync

From repo root:

```powershell
Expand-Archive -Path "C:\Users\alexa\Downloads\l2-account-employee-tph-domain-sync.zip" -DestinationPath . -Force
git status
git diff -- planning
```

If diff is OK:

```powershell
git add planning
git status
```

This is a docs-only package.

It does not include runtime source files, tests, OpenAPI or generated TypeScript artifacts.
