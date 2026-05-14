# Apply This Archive

Repository root on this machine:

```powershell
C:\enman\enman
```

Expected archive path after download:

```powershell
C:\Users\alexa\Downloads\enman-gradual-domain-discovery-workflow-v1.zip
```

## PowerShell / VS Code Terminal

```powershell
cd "C:\enman\enman"
Expand-Archive -Path "C:\Users\alexa\Downloads\enman-gradual-domain-discovery-workflow-v1.zip" -DestinationPath . -Force
git status
git diff
```

If the diff looks correct:

```powershell
git add planning
git commit -m "Update planning workflow for gradual domain discovery"
```

## Git Bash / WSL

```bash
cd /c/enman/enman
unzip -o "/c/Users/alexa/Downloads/enman-gradual-domain-discovery-workflow-v1.zip" -d .
git status
git diff
```

If the diff looks correct:

```bash
git add planning
git commit -m "Update planning workflow for gradual domain discovery"
```

## Files Included

```text
planning/README.md
planning/planning-workflow-current.md
planning/scenario-specification-principles.md
planning/domain-draft-generation-guide.md
planning/diagrams/README.md
planning/diagrams/scenario-text-specs/README.md
planning/diagrams/scenario-data/README.md
planning/tables/README.md
planning/tables/00-planning-tables-index.md
planning/tables/pre-domain-variants-input.md
planning/tables/domain-drafts/README.md
```

## Optional cleanup if files from older domain-variant package exist

If you applied an older package that introduced competing-domain-variant artifacts, remove or archive these files/folders because the current workflow uses gradual domain drafts instead:

```powershell
cd "C:\enman\enman"
Remove-Item -Force "planning\domain-model-variant-generation-guide.md" -ErrorAction SilentlyContinue
Remove-Item -Recurse -Force "planning\tables\domain-variants" -ErrorAction SilentlyContinue
git status
git diff
```

Only run the cleanup if those files/folders exist and you do not want to keep them as historical notes.
