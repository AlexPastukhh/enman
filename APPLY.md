# APPLY

Package: `enman-domain-draft-01-package-v1.zip`

Purpose:

```text
Add the first saved domain draft and update planning-table navigation.
```

## Files to add / replace

Add:

```text
planning/tables/domain-drafts/domain-draft-01.md
```

Replace:

```text
planning/tables/domain-drafts/README.md
planning/tables/00-planning-tables-index.md
```

Delete:

```text
Nothing.
```

## Apply with PowerShell / VS Code Terminal

From the repository root:

```powershell
Expand-Archive -Path .\enman-domain-draft-01-package-v1.zip -DestinationPath . -Force
git status
```

Then review:

```powershell
code planning\tables\domain-drafts\domain-draft-01.md
code planning\tables\domain-drafts\README.md
code planning\tables\00-planning-tables-index.md
```

Commit manually when ready:

```powershell
git add planning/tables/domain-drafts/domain-draft-01.md `
        planning/tables/domain-drafts/README.md `
        planning/tables/00-planning-tables-index.md

git commit -m "Add first domain draft"
```

## Apply with Git Bash / WSL

From the repository root:

```bash
unzip -o enman-domain-draft-01-package-v1.zip
git status
```

Then review:

```bash
code planning/tables/domain-drafts/domain-draft-01.md
code planning/tables/domain-drafts/README.md
code planning/tables/00-planning-tables-index.md
```

Commit manually when ready:

```bash
git add planning/tables/domain-drafts/domain-draft-01.md \
        planning/tables/domain-drafts/README.md \
        planning/tables/00-planning-tables-index.md

git commit -m "Add first domain draft"
```

## Next step after applying

```text
Review / refine planning/tables/domain-drafts/domain-draft-01.md
```

Then:

```text
coverage review
-> domain-draft-02.md
```
