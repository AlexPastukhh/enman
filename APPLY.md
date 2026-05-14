# APPLY

Archive: `enman-l1-readiness-update-v1.zip`

This package contains complete replacement files with repository-relative paths.

## PowerShell / VS Code Terminal

From the repository root:

```powershell
Expand-Archive -Path "C:\Users\alexa\Downloads\enman-l1-readiness-update-v1.zip" -DestinationPath . -Force
git status
```

## Git Bash / WSL

From the repository root:

```bash
unzip -o /mnt/c/Users/alexa/Downloads/enman-l1-readiness-update-v1.zip -d .
git status
```

## Files to add

```text
planning/l1-domain-implementation-cut.md
```

## Files to replace

```text
planning/README.md
planning/planning-workflow-current.md
planning/tables/README.md
planning/tables/00-planning-tables-index.md
planning/tables/domain-drafts/README.md
planning/tables/domain-drafts/domain-draft-01.md
planning/current-state.md
planning/domain-model.md
```

## Files to delete

```text
Nothing.
```

## Verification

After applying, check that these files exist:

```text
planning/tables/domain-drafts/domain-draft-01.md
planning/l1-domain-implementation-cut.md
```

Then read:

```text
planning/l1-domain-implementation-cut.md
```

before starting L1 implementation with an agent.
