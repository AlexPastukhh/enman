# Apply enman-slice-and-adr-workflow-v1.zip

## Purpose

This archive adds slice drafting workflow documentation and ADR candidate tracking, and updates planning navigation.

It does not modify source code.

## Files

Add:

```text
planning/slices/README.md
planning/slices/l1-slice-drafting-guide.md
planning/adr/README.md
planning/adr/adr-candidates.md
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

## PowerShell / VS Code Terminal

Run from repository root:

```powershell
Expand-Archive -Path "C:\Users\alexa\Downloads\enman-slice-and-adr-workflow-v1.zip" -DestinationPath . -Force
git status
```

## Git Bash / WSL

Run from repository root:

```bash
unzip -o /mnt/c/Users/alexa/Downloads/enman-slice-and-adr-workflow-v1.zip
git status
```

## Expected result

After applying, the repo should contain:

```text
planning/slices/README.md
planning/slices/l1-slice-drafting-guide.md
planning/adr/README.md
planning/adr/adr-candidates.md
```

and navigation should mention that after green L1 domain foundation, the next planning step is L1 slice drafting before application/API/persistence/UI work.
