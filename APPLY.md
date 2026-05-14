# Apply enman-vkr-formulation-guide-v1.zip

This archive adds an internal VKR formulation guide and updates planning navigation.

## Files

- `planning/README.md`
- `planning/planning-workflow-current.md`
- `planning/vkr-formulation-guide.md`
- `MANIFEST.md`
- `APPLY.md`

## PowerShell / VS Code Terminal

```powershell
cd "C:\enman\enman"
Expand-Archive -Path "C:\Users\alexa\Downloads\enman-vkr-formulation-guide-v1.zip" -DestinationPath . -Force
git status
git diff
```

If OK:

```powershell
git add planning/README.md planning/planning-workflow-current.md planning/vkr-formulation-guide.md
git commit -m "Add VKR formulation guide"
```

## Git Bash / WSL

```bash
cd /c/enman/enman
unzip -o "/c/Users/alexa/Downloads/enman-vkr-formulation-guide-v1.zip" -d .
git status
git diff
```

If OK:

```bash
git add planning/README.md planning/planning-workflow-current.md planning/vkr-formulation-guide.md
git commit -m "Add VKR formulation guide"
```

## Deletions

No deletions are required.
