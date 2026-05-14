# Apply This Archive

Repository root on this machine:

```powershell
C:\enman\enman
```

Expected archive path after download:

```powershell
C:\Users\alexa\Downloads\enman-account-activation-addendum-v1.zip
```

## PowerShell / VS Code Terminal

```powershell
cd "C:\enman\enman"
Expand-Archive -Path "C:\Users\alexa\Downloads\enman-account-activation-addendum-v1.zip" -DestinationPath . -Force
git status
git diff
```

If the diff looks correct:

```powershell
git add planning
git commit -m "Add account activation policy planning"
```

## Git Bash / WSL

```bash
cd /c/enman/enman
unzip -o "/c/Users/alexa/Downloads/enman-account-activation-addendum-v1.zip" -d .
git status
git diff
```

If the diff looks correct:

```bash
git add planning
git commit -m "Add account activation policy planning"
```

## Files Included

```text
planning/scenario-specification-principles.md
planning/domain-draft-generation-guide.md
planning/diagrams/scenario-text-specs/README.md
planning/diagrams/scenario-text-specs/SC-01-guest-registration.md
planning/diagrams/scenario-text-specs/SC-02-login.md
planning/diagrams/scenario-text-specs/SC-15-security-text-specification.md
planning/diagrams/scenario-text-specs/scenario-account-activation-security-addendum.md
planning/tables/README.md
planning/tables/00-planning-tables-index.md
planning/tables/scenario-behavior-baseline-account-activation-addendum.md
planning/tables/domain-drafts/README.md
```

## Deletions

No files need to be deleted for this update.
