# Apply Instructions

Apply from the repository root.

## PowerShell / VS Code Terminal

```powershell
Expand-Archive -Path "C:\Users\alexa\Downloads\enman-l1-domain-testing-rules-v1.zip" -DestinationPath . -Force
git status
```

## Git Bash / WSL

```bash
unzip -o /mnt/c/Users/alexa/Downloads/enman-l1-domain-testing-rules-v1.zip -d .
git status
```

## Expected changes

Add:

```text
planning/l1-domain-testing-rules.md
```

Replace:

```text
planning/README.md
planning/planning-workflow-current.md
planning/tables/README.md
planning/l1-domain-implementation-cut.md
```

Delete:

```text
nothing
```

## After applying

Review:

```text
planning/l1-domain-testing-rules.md
planning/l1-domain-implementation-cut.md
```

Then use both files when preparing the L1 implementation agent prompt.
