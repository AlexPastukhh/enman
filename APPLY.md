# Apply Instructions

Archive:

```text
enman-adr-decision-notes-audit-v1.zip
```

Apply from repository root:

```powershell
Expand-Archive -Path "C:\Users\alexa\Downloads\enman-adr-decision-notes-audit-v1.zip" -DestinationPath . -Force
git status
```

## Add

```text
planning/adr/decision-capture-audit.md
```

## Replace

```text
planning/adr/README.md
planning/adr/adr-workflow.md
planning/adr/architecture-decision-notes.md
planning/adr/adr-candidates.md
planning/planning-agent-protocol.md
planning/README.md
planning/planning-workflow-current.md
planning/planning-doc-responsibility-map.md
```

## Delete

```text
nothing
```

## Notes

This package does not create full numbered ADRs.

It makes the distinction explicit:

```text
architecture-decision-notes.md = accepted/current decisions guiding planning.
adr-candidates.md = backlog of possible future full ADRs.
```

It also performs a decision-capture audit over current planning docs and expands accepted decision notes.
