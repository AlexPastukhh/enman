# Diagramming Responsibility Map

Status: current diagramming-layer responsibility map / migration router  
Scope: where diagramming information belongs and how diagram workflows relate to scenario/domain/testing/thesis sources

## 1. Purpose

This file routes diagramming work to the correct owner.

Diagramming owns diagram work:

```text
source preflight for diagrams;
diagram request/prompt generation;
draw.io generation workflow;
diagram status marker usage;
diagram artifact packaging;
diagram legacy/generated artifact classification, later.
```

Diagramming does not own scenario, domain, slice, testing or thesis source truth.

## 2. Current Migration Rule

Current physical state:

```text
Scenario sources and diagram workflow files are still physically colocated under planning/diagrams/.
```

Current responsibility state:

```text
Scenario source ownership -> planning/diagrams/scenario-responsibility-map.md
Diagram workflow ownership -> planning/diagramming/diagramming-responsibility-map.md
```

Physical moves are deferred until a dedicated mechanical move/link-update batch.

## 3. Routing Table

| Information / task | Belongs in / read from | Does not belong in |
|---|---|---|
| Diagramming entrypoint and read order | `planning/diagramming/README.md` | scenario README only |
| Diagramming placement/routing | `planning/diagramming/diagramming-responsibility-map.md` | scenario responsibility map |
| Diagram source consistency / preflight | current physical file `planning/diagrams/scenario-diagram-consistency-report.md` | scenario text specs as workflow prose |
| Diagram prompt / request / batch plan workflow | current physical file `planning/diagrams/diagram-prompt-generation-workflow.md` | scenario drafting workflow |
| Draw.io XML generation workflow | current physical file `planning/diagrams/drawio-diagram-generation-workflow.md` | scenario text specs |
| Diagram status marker vocabulary | current physical file `planning/diagrams/scenario-status-marker-rules.md` during migration | individual diagrams only |
| Scenario source currentness | `planning/diagrams/scenario-artifact-map.md` | generated diagrams |
| Scenario source placement | `planning/diagrams/scenario-responsibility-map.md` | diagramming map |
| Domain diagrams source material | `planning/domain/domain-model-overview.md`, `planning/domain/scenario-to-aggregate-map.md`, aggregate/value-object drafts | old domain snapshots as current truth |
| Testing/evidence diagrams source material | `planning/testing/testing-responsibility-map.md` and selected testing docs only when in scope | screenshot runner as ordinary test proof |
| VKR-clean diagram wording/evidence | `planning/thesis/` and `planning/vkr-clean-reference.md` when in scope | internal planning labels in final wording |
| Historical/generated diagram packages | future `planning/diagramming/generated/` or `legacy/` after classification | current scenario/domain source truth |
| Root-level legacy diagram notes | future classification pass | current workflow until classified |

## 4. Diagram Use-Case Selector

When the user asks for diagrams, first classify the task:

```text
prepare diagram prompt / request / batch plan
  -> read diagram-prompt-generation-workflow.md
  -> read scenario-diagram-consistency-report.md

produce draw.io artifact
  -> read drawio-diagram-generation-workflow.md
  -> read diagram-prompt-generation-workflow.md
  -> read source layers required by the requested pages

check diagram consistency
  -> read scenario-diagram-consistency-report.md
  -> read scenario-artifact-map.md
  -> read domain/testing/thesis sources only if the diagrams include them

classify / move diagram files
  -> do not do inside ordinary diagram generation;
  -> use a dedicated documentation/diagramming cleanup batch.
```

## 5. Source Read Rules

For scenario-based diagrams, read:

```text
planning/diagrams/README.md
planning/diagrams/scenario-responsibility-map.md
planning/diagrams/scenario-artifact-map.md
planning/diagrams/scenario-text-specs/
planning/diagrams/scenario-behavior-items/
planning/diagrams/scenario-ui-specs/, when UI diagrams are requested
```

For domain-aware diagrams, also read:

```text
planning/domain/README.md
planning/domain/scenario-to-aggregate-map.md
planning/domain/domain-model-overview.md
planning/domain/aggregates/
planning/domain/value-objects/
```

For testing/evidence diagrams, also read:

```text
planning/testing/testing-responsibility-map.md
planning/testing/e2e-testing-workflow.md, if E2E is involved
planning/testing/playwright-e2e-and-screenshot-plan.md, only when screenshot/evidence is explicitly in scope
```

## 6. Guardrails

```text
Do not treat diagram files as source-of-truth requirements.
Do not generate all diagrams at once by default.
Do not use stale scenario variants without checking scenario-artifact-map.md.
Do not move diagram workflow files during ordinary diagram prompt/generation work.
Do not update scenario/domain/slice meaning just to make a diagram simpler.
Do not use screenshot/evidence planning as ordinary testing proof.
```
