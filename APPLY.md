# Apply Instructions

Archive:

```text
enman-scenario-behavior-items-migration-v1.zip
```

Apply from repository root:

```powershell
Expand-Archive -Path "C:\Users\alexa\Downloads\enman-scenario-behavior-items-migration-v1.zip" -DestinationPath . -Force
git status
```

## Add / replace if missing

```text
planning/diagrams/scenario-behavior-items/README.md
planning/diagrams/scenario-behavior-items/00-scenario-behavior-items-index.md
planning/diagrams/scenario-behavior-items/SC-*.md
planning/diagrams/scenario-questions-register.md
```

## Replace

```text
planning/README.md
planning/planning-workflow-current.md
planning/diagrams/README.md
planning/diagrams/scenario-text-specs/README.md
planning/diagrams/scenario-data/README.md
planning/tables/README.md
```

## Delete

```text
nothing
```

## Notes

This package migrates existing behavior items into per-scenario files and updates navigation.

It does not create `.client.md` sidecars.

It does not change code, API contracts, domain model or tests.

Known corrections are marked in migrated item notes:
- InReview is preserved as current request creation status.
- Agreement replacement wording is flagged away from legacy Rejected wording.
- Rejection feedback is optional in current domain direction; UI warning is separate.
