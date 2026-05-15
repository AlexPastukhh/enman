# Apply Instructions

Archive:

```text
enman-adr-decision-capture-workflow-v1.zip
```

Apply from repository root:

```powershell
Expand-Archive -Path "C:\Users\alexa\Downloads\enman-adr-decision-capture-workflow-v1.zip" -DestinationPath . -Force
git status
```

## Add

```text
planning/adr/adr-workflow.md
planning/adr/architecture-decision-notes.md
```

## Replace

```text
planning/adr/README.md
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

This package strengthens ADR workflow and captures accepted architecture decisions with rationale.

It adds:
- ADR workflow levels: candidate, accepted decision note, full ADR;
- accepted architecture decision notes for discussed/current decisions;
- expanded ADR candidates;
- ADR capture rule in agent protocol;
- ADR gate in current workflow;
- ADR responsibility rules.

It does not:
- create full numbered ADRs;
- change code/API/domain/tests;
- create `.client.md`;
- change scenario/domain behavior.
