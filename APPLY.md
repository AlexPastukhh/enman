# Apply Instructions

Archive:

```text
enman-diagram-scenario-clarifications-v1.zip
```

Apply from repository root:

```powershell
Expand-Archive -Path "C:\Users\alexa\Downloads\enman-diagram-scenario-clarifications-v1.zip" -DestinationPath . -Force
git status
```

## Add

```text
planning/diagrams/scenario-clarifications/README.md
planning/diagrams/scenario-clarifications/AGR-001-agreement-proposal-replacement-terminology.md
planning/diagrams/scenario-clarifications/diagram-generation-readiness-guardrails.md
planning/diagrams/scenario-clarifications/scenario-spec-pre-diagram-review.md
```

## Replace

```text
nothing
```

## Delete

```text
nothing
```

## Why this archive is non-invasive

This package does not overwrite scenario specs because agreement proposal files were not reliably located through the connector search in this pass.

Instead it adds a clear diagram-generation guardrail:

```text
agreement proposal replacement/counterproposal must not be drawn as ordinary Rejected.
Use superseded/replaced by counterproposal / SupersededByCounterProposal.
```

Later cleanup can update SC-13B/SC-13D/index files directly after their exact current paths are confirmed.

## Notes

This package:
- adds a scenario clarification area;
- records the agreement proposal replacement conflict;
- gives diagram-generation guardrails;
- marks the clarification as blocking only for agreement proposal lifecycle diagrams.

It does not:
- change code;
- overwrite scenario specs;
- create or modify diagrams;
- decide final enum name beyond current planning assumption.
