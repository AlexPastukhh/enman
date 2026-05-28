# Diagramming Planning Index

Status: current diagramming-layer entrypoint / conceptual routing layer  
Scope: diagram request preparation, source consistency checks, draw.io generation workflow and diagram artifact governance

## 1. Purpose

This folder owns diagramming responsibility.

Use it for:

```text
diagram prompt / request preparation
diagram source preflight and consistency checks
draw.io generation workflow
diagram status marker usage
diagram artifact packaging and future classification
```

This folder does not own scenario source truth. Scenario sources are currently under:

```text
planning/diagrams/
```

During migration, the current diagram workflow files still physically live under `planning/diagrams/`. Use this folder as the routing layer until the physical move is performed.

## 2. Current Diagram Workflow Files

Current physical files during migration:

```text
planning/diagrams/scenario-diagram-consistency-report.md
planning/diagrams/diagram-prompt-generation-workflow.md
planning/diagrams/drawio-diagram-generation-workflow.md
planning/diagrams/scenario-status-marker-rules.md
```

Target future location:

```text
planning/diagramming/diagram-source-consistency-report.md
planning/diagramming/diagram-prompt-generation-workflow.md
planning/diagramming/drawio-diagram-generation-workflow.md
planning/diagramming/diagram-status-marker-rules.md
```

Do not move files until the dedicated physical move/link-update batch.

## 3. Read Order

For any diagramming task, read:

```text
planning/README.md
planning/workflow-activation-map.md
planning/planning-use-case-map.md
planning/planning-doc-responsibility-map.md
planning/diagramming/README.md
planning/diagramming/diagramming-responsibility-map.md
```

Then choose the task path:

```text
Prompt / batch plan:
  planning/diagrams/diagram-prompt-generation-workflow.md
  planning/diagrams/scenario-diagram-consistency-report.md

Draw.io XML generation:
  planning/diagrams/drawio-diagram-generation-workflow.md
  planning/diagrams/diagram-prompt-generation-workflow.md
  planning/diagrams/scenario-diagram-consistency-report.md

Diagram consistency audit:
  planning/diagrams/scenario-diagram-consistency-report.md
  planning/diagrams/scenario-status-marker-rules.md, when status markers matter
```

## 4. Source Layers Read By Diagramming

Diagramming reads but does not own:

```text
planning/diagrams/                         # current scenario source layer during migration
planning/domain/                           # domain model and aggregate/value-object maps
planning/slices/                           # slice maps/drafts when diagrams show slice scope
planning/testing/                          # only when testing/evidence is explicitly in scope
planning/thesis/                           # VKR/evidence-safe wording and target diagram artifacts
```

## 5. Guardrails

```text
Do not treat generated diagrams as scenario source truth.
Do not use historical .drawio packages as current requirements without checking scenario/domain sources.
Do not let diagram convenience rewrite core scenario, domain or slice source meaning.
Do not mix screenshot/evidence placement cleanup into ordinary diagram workflow routing.
Do not physically move diagram files in this conceptual routing batch.
```
