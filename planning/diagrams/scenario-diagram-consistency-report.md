# Scenario Diagram Consistency Report

Status: current diagram/source consistency note  
Doc version: v0.1.0  
Scope: scenario source currentness, diagram source preflight and domain-aware diagram readiness

## 1. Purpose

This report records the current scenario source route for diagram work.

It prevents planning/diagram agents from using stale diagram package summaries, stale `.drawio` pages, deprecated validation addenda or old pre-domain wording as current source truth.

## 2. Current Source Route

Before diagram work, read:

```text
planning/README.md
planning/workflow-activation-map.md
planning/planning-use-case-map.md
planning/planning-doc-responsibility-map.md

planning/scenario-specification-principles.md
planning/scenario-domain-validation-principles.md

planning/diagrams/README.md
planning/diagrams/scenario-responsibility-map.md
planning/diagrams/scenario-artifact-map.md
planning/diagrams/scenario-drafting-workflow.md
```

Then read selected scenario artifacts:

```text
planning/diagrams/scenario-text-specs/
planning/diagrams/scenario-data/
planning/diagrams/scenario-ui-specs/
planning/diagrams/scenario-behavior-items/
planning/diagrams/scenario-clarifications/
planning/diagrams/scenario-questions-register.md
```

For domain-aware diagrams, read:

```text
planning/domain/README.md
planning/domain/scenario-to-aggregate-map.md
planning/domain/domain-model-overview.md
planning/domain/aggregates/
planning/domain/value-objects/
planning/domain/decisions/
```

## 3. Deprecated / Historical Sources

The deprecated global validation addendum is historical context only.

Current validation/domain route is:

```text
planning/scenario-domain-validation-principles.md
scenario-local behavior items
scenario clarifications/questions
planning/domain/, when domain interpretation is needed
```

Do not reintroduce old global validation addenda as current source of truth.

## 4. Scenario Artifact Currentness

Use:

```text
planning/diagrams/scenario-artifact-map.md
```

for:

```text
- current primary scenario text/DATA/UI/behavior files;
- legacy/compat variants;
- deprecated/stale variants;
- merged scenarios;
- downstream domain/slice links.
```

Do not infer currentness from filenames only.

## 5. Diagram Responsibility Boundary

Diagram generation is a separate responsibility from scenario source drafting.

During migration, diagram workflows still live under `planning/diagrams/`, but they do not own scenario source truth.

Use:

```text
planning/diagrams/diagram-prompt-generation-workflow.md
planning/diagrams/drawio-diagram-generation-workflow.md
```

only for diagram request/preflight/output behavior.

## 6. Stale Package Summary Status

Old generated package summaries and old draw.io pages are output artifacts, not semantic source truth.

If a generated diagram conflicts with current scenario/domain docs, the current docs win and the diagram should be marked stale or regenerated.

## 7. Visual Diagram Note

Diagram pages should show unresolved questions or accepted clarifications where they affect interpretation.

Do not make diagrams look more complete than current scenario/domain evidence supports.
