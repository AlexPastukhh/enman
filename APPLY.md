# Apply Instructions

Archive:

```text
enman-planning-doc-responsibility-map-v1.zip
```

Apply from repository root:

```powershell
Expand-Archive -Path "C:\Users\alexa\Downloads\enman-planning-doc-responsibility-map-v1.zip" -DestinationPath . -Force
git status
```

## Add

```text
planning/planning-doc-responsibility-map.md
```

## Replace

```text
planning/planning-agent-protocol.md
planning/planning-workflow-current.md
planning/README.md
planning/diagrams/README.md
planning/slices/README.md
planning/tables/README.md
planning/replacement-file-generation-guide.md
```

## Delete

```text
nothing
```

## Notes

This package adds document responsibility / workflow leakage rules and strengthens agent protocol.

It adds:
- Relevant Questions Rule;
- Assumptions With Questions Rule;
- Next Step Protocol;
- File Responsibility Rule.

It does not:
- create `.client.md`;
- change behavior items content;
- change domain/API/code/tests;
- perform the full workflow centralization audit.
