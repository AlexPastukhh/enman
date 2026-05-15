# APPLY

Run from the repository root.

```powershell
Expand-Archive -Path "C:\Users\alexa\Downloads\enman-slice-questions-register-sync.zip" -DestinationPath . -Force
git status
```

Recommended review after applying:

```powershell
git diff -- planning/slices/slice-questions-register.md
```

No code, generated artifacts, branch, commit or PR changes are included in this archive.
