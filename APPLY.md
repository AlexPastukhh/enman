# APPLY — Employee Request Read Slice Docs Sync

Run from repository root:

```powershell
Expand-Archive -Path "C:\Users\alexa\Downloads\employee-request-read-docs-sync.zip" -DestinationPath . -Force
git status
git diff -- planning
```

If the diff is correct:

```powershell
git add planning
git status
```

This archive is documentation-only.

It must not change:

```text
- backend runtime code;
- client runtime code;
- tests;
- generated OpenAPI/constants/TypeScript artifacts.
```
