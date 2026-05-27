# Scenario Responsibility Map

Status: current scenario-layer responsibility map  
Scope: where scenario-layer information belongs and which files are current vs historical during scenario/source cleanup

## 1. Purpose

This map routes scenario-layer information to the correct owner.

Use it after the root responsibility map selects the scenario layer.

This file separates:

```text
scenario text specs;
scenario DATA specs;
scenario UI specs;
scenario behavior items;
scenario questions;
scenario clarifications;
scenario cross-cutting behavior;
diagram workflows and diagram readiness;
historical/deprecated scenario sources.
```

## 2. Routing Table

| Information type | Belongs in | Does not belong in |
|---|---|---|
| Scenario layer entrypoint and read order | `planning/diagrams/README.md` | individual scenario files |
| Scenario placement/routing rules | `planning/diagrams/scenario-responsibility-map.md` | root responsibility map details |
| Scenario artifact inventory/currentness | `planning/diagrams/scenario-artifact-map.md` | scattered README prose only |
| User/system behavior, actors, goal, preconditions, main flow, alternatives, outcomes | `planning/diagrams/scenario-text-specs/<SC>.md` | DATA/UI/behavior item indexes |
| Data entered, seen, selected, filtered, attached or referenced | `planning/diagrams/scenario-data/<SC>-data.md` | scenario text spec prose only |
| UI-visible requirements and accepted UI decisions | `planning/diagrams/scenario-ui-specs/<SC>-ui.md` | React/client implementation files |
| Scenario-derived behavior items | `planning/diagrams/scenario-behavior-items/<SC>-behavior-items.md` | domain aggregate docs or slice drafts |
| Cross-cutting scenario behavior | `planning/diagrams/scenario-cross-cutting/` or behavior item files with `CC-*` IDs | one scenario file unless scenario-local only |
| Scenario questions that can change behavior/DATA/UI/domain interpretation | `planning/diagrams/scenario-questions-register.md` | local prose only |
| Temporary accepted clarifications / diagram guardrails | `planning/diagrams/scenario-clarifications/` | permanent scenario specs after correction |
| Diagram prompt workflow | `planning/diagrams/diagram-prompt-generation-workflow.md` | scenario text specs |
| Draw.io XML generation workflow | `planning/diagrams/drawio-diagram-generation-workflow.md` | scenario text specs |
| Status marker vocabulary | `planning/diagrams/scenario-status-marker-rules.md` | individual diagrams only |
| Deprecated global validation addendum | `planning/diagrams/scenario-text-specs/deprecated/` | current source read order |

## 3. Source Priority

When scenario files conflict, use this priority:

```text
1. Accepted clarification / scenario cleanup note, if explicitly current.
2. Current scenario text spec, DATA spec, UI spec and behavior items.
3. Scenario artifact map currentness/status notes.
4. Historical/deprecated sources only as context.
```

If the conflict affects domain, slice, testing or diagram work, record it in the scenario questions register or a clarification file.

## 4. Currentness Labels

Use these labels in scenario indexes and artifact maps:

```text
current
current primary
current derived
current cross-cutting
partial
planned
future
merged
historical
deprecated
superseded
needs review
open question
```

## 5. Guardrails

```text
Do not use old global validation addenda as current source of truth.
Do not treat scenario clarifications as permanent replacements for corrected scenario specs.
Do not duplicate complete scenario details in indexes.
Do not hide stale/current variant decisions only in chat.
Do not infer implementation status from scenario status markers.
```
