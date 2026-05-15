# Scenario / Diagram Planning Index

Status: current scenario/specification and diagram workflow index  
Scope: scenario text specs, DATA, UI specs, validation/security addenda, behavior items, questions, scenario drafting workflow, diagram prompt workflow and draw.io diagram generation workflow

## 1. Purpose

This folder contains scenario and specification source artifacts and diagram-generation workflow docs.

It supports three related but separate activities:

```text
scenario/specification drafting
repo-grounded diagram prompt preparation
repo-grounded draw.io diagram generation for VKR-clean artifacts
```

## 2. Main Artifact Types

```text
scenario drafting workflow
scenario text specs
scenario DATA files
scenario UI specs
validation/security addenda
scenario questions register
scenario behavior items
scenario clarifications
diagram request/prompt and preflight workflow
draw.io diagram-generation workflow
```

## 3. Scenario Drafting Workflow

Use this file when creating or updating scenarios:

```text
planning/diagrams/scenario-drafting-workflow.md
```

Scenario drafting must keep related artifacts synchronized:

```text
scenario text spec
scenario DATA spec
scenario UI spec, when relevant
validation/security addendum, when relevant
scenario behavior items
scenario questions register
scenario clarifications, when needed
```

Scenario Draft Chat may prepare a diagram request/prompt when diagrams are requested from scenario sources.

It must use:

```text
planning/diagrams/diagram-prompt-generation-workflow.md
planning/diagrams/drawio-diagram-generation-workflow.md
```

It does not draw diagrams itself.

## 4. Diagram Workflow Files

Use these files when preparing a diagram request or running the Diagram Chat:

```text
planning/diagrams/diagram-prompt-generation-workflow.md
planning/diagrams/drawio-diagram-generation-workflow.md
```

Responsibilities:

| File | Responsibility |
|---|---|
| `scenario-drafting-workflow.md` | How a Scenario Draft Chat maintains scenario text, DATA, UI, behavior items, questions and diagram prompt handoff together. |
| `diagram-prompt-generation-workflow.md` | How a scenario/documentation/planning chat prepares a repo-grounded diagram request/prompt for the single Diagram Chat, and how the Diagram Chat runs preflight. |
| `drawio-diagram-generation-workflow.md` | Target diagram format, diagram book structure, draw.io XML rules and diagram archive rules. |

Core rule:

```text
scenario sources -> diagram prompt workflow -> draw.io XML workflow -> navigation
```

The scenario/documentation/prompt chat does not draw diagrams itself.

The Diagram Chat should first run preflight, then generate only a selected diagram batch.

## 5. Target Diagram Format

Target format:

```text
draw.io XML
```

Preferred artifact:

```text
one multi-page `.drawio` file, diagram book style
```

Preferred output path when `vkr-clean/` is active:

```text
vkr-clean/diagrams/enman-vkr-diagrams.drawio
```

Fallback path while `vkr-clean/` is not active:

```text
planning/diagrams/vkr-clean-drafts/enman-vkr-diagrams.drawio
```

Do not use PlantUML as the primary deliverable unless the user explicitly asks.

## 6. Scenario Source Read Order

For scenario/diagram work, read:

```text
planning/scenario-specification-principles.md
planning/scenario-domain-validation-principles.md
planning/diagrams/scenario-drafting-workflow.md
planning/diagrams/scenario-text-specs/README.md
planning/diagrams/scenario-data/README.md
planning/diagrams/scenario-ui-specs/README.md, if exists
planning/diagrams/scenario-behavior-items/README.md
planning/diagrams/scenario-clarifications/README.md, if exists
planning/diagrams/scenario-questions-register.md, if exists
```

Actual index files currently include:

```text
planning/diagrams/scenario-text-specs/00-scenario-text-specs-index.md
planning/diagrams/scenario-data/00-scenario-data-index.md
planning/diagrams/scenario-behavior-items/00-scenario-behavior-items-index.md
```

Do not guess index filenames. Check the current repository.

## 7. Security Addenda

Scenario/security addenda may record rules that affect many scenarios without editing every scenario file.

Current security addenda:

```text
planning/diagrams/scenario-text-specs/SC-15-security-text-specification.md
planning/diagrams/scenario-text-specs/scenario-account-activation-security-addendum.md
planning/diagrams/scenario-text-specs/scenario-browser-security-addendum.md
```

## 8. Behavior Items

Behavior items may be:

```text
scenario-derived
security-derived cross-cutting
API-contract-derived cross-cutting
tooling/testing-derived cross-cutting
client-cross-cutting-derived
infrastructure-derived
```

Cross-cutting behavior items must clearly state their source type.

Scenario behavior items should be updated together with scenario text/DATA/UI changes when required behavior changes.

## 9. Scenario Clarifications Before Diagram Generation

Diagram generation must check scenario clarifications before drawing.

Current clarification index:

```text
planning/diagrams/scenario-clarifications/README.md
```

Important current guardrail:

```text
Do not draw agreement proposal replacement as Rejected.
Use superseded/replaced by counterproposal, or SupersededByCounterProposal if a domain state is needed.
Rejected is only for explicit rejection/decline.
```

Primary clarification:

```text
planning/diagrams/scenario-clarifications/AGR-001-agreement-proposal-replacement-terminology.md
```

## 10. Status Markers For Diagrams

Diagram prompts must require status markers when status may be misunderstood:

```text
[CORE]
[IMPLEMENTED]
[DESIGNED]
[PLANNED]
[DEFERRED]
[QUESTION]
```

Do not overclaim implementation status.

A diagram element is `[IMPLEMENTED]` only when current repo implementation evidence confirms it in the stated scope.

## 11. VKR-Clean Diagram Language

Final draw.io diagrams and VKR-clean companion docs must not mention:

```text
AI
ChatGPT
prompt
agent
internal workflow
planning chat
```

They may mention normal engineering concepts such as:

```text
OpenAPI
generated TypeScript types
generated semantic constants
ProblemDetails
client API layer
ASP.NET Core API
Application layer
Domain layer
Persistence
external provider
```

## 12. CSRF / Antiforgery

CSRF requirements and behavior items:

```text
planning/diagrams/scenario-text-specs/scenario-browser-security-addendum.md
planning/diagrams/scenario-behavior-items/CC-CSRF-001-antiforgery-behavior-items.md
```

Implementation-ready cross-cutting slice:

```text
planning/slices/cross-cutting/CC-CSRF-001-antiforgery-token-session-context.md
```
