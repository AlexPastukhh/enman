# Draft-Driven Discovery Principles

Status: current slice-layer discovery principle / scope-safe and validation-aware  
Scope: business slice drafts, server slice drafts, client sidecars, cross-cutting/helper slice drafts and slice verification planning

## 1. Purpose

Draft-driven discovery means we do not try to fully design a slice implementation in one perfect pass.

We create a slice draft, use it to discover missing behavior, questions, assumptions, contract gaps, flow gaps, extension/change pressure and verification gaps, then refine the next draft or move to implementation when the remaining risk is acceptable.

This file owns the discovery loop for slice-layer work only.

Detailed slice draft section-authoring principles live in:

```text
planning/slices/slice-draft-authoring-principles.md
```

Domain drafting, scenario drafting and documentation/status reconciliation have their own layer owners and should not be governed by this file.

## 2. Core Loop

```text
source requirements
-> slice draft
-> open questions and assumptions
-> visual flow maps
-> extension/change point review
-> detailed flow
-> behavior coverage
-> implementation direction
-> test / verification planning
-> sync/status check when implementation already exists
-> next draft or implementation step
```

When work proceeds on an assumption, the assumption must be explicit enough for review.

## 3. Scope Safety Rule

Drafts and prompts must not broaden implementation scope silently.

Use:

```text
planning/agent-scope-boundaries-and-prompt-safety.md
```

A slice implementation prompt may require reading docs, but must not allow changing docs/domain/generated artifacts unless explicitly requested.

## 4. Applies To

This file applies to slice-layer planning artifacts:

```text
business slice drafts;
server/backend/API slice drafts;
client sidecar drafts;
cross-cutting/helper slice drafts;
slice verification/test planning inside a slice draft;
implemented-slice draft sync when a slice already has code/tests.
```

This file does not own:

```text
domain model drafting;
scenario text/UI/DATA/behavior item drafting;
documentation/status reconciliation workflows;
global planning-doc architecture.
```

Use these layer owners instead:

```text
Domain drafting:
  planning/domain-draft-generation-guide.md
  planning/tables/domain-drafts/README.md

Scenario drafting:
  planning/diagrams/scenario-drafting-workflow.md
  planning/scenario-specification-principles.md

Documentation/status reconciliation:
  planning/documentation/status-reconciliation-workflow.md
  planning/documentation/documentation-update-workflow.md
```

## 5. Draft Formats

Default early format:

```text
shortened working draft
```

Full slice format:

```text
full server / business / cross-cutting / client sidecar draft
```

Concrete templates and workflows live in:

```text
planning/slices/server/SERVER-SLICE-TEMPLATE.md
planning/slices/client/CLIENT-SLICE-TEMPLATE.md
planning/slices/cross-cutting/cross-cutting-umbrella-template.md
planning/slices/server/SERVER-SLICE-DRAFTING-WORKFLOW.md
planning/slices/client/CLIENT-SLICE-DRAFTING-WORKFLOW.md
```

Legacy practical guidance has been superseded by:

```text
planning/slices/slice-draft-authoring-workflow.md
planning/slices/slice-draft-authoring-principles.md
planning/slices/server/SERVER-SLICE-TEMPLATE.md
planning/slices/client/CLIENT-SLICE-TEMPLATE.md
planning/slices/cross-cutting/cross-cutting-umbrella-template.md
```

Do not route new work through removed legacy drafting guides.

## 6. Business / Server Slice Drafts

Business and server slice drafts use this discovery chain:

```text
scenario/source mapping
-> behavior subset implemented by this slice
-> scenario scope / slice boundary
-> behavior not implemented by this slice
-> slice relations and future-change check
-> domain/application/implementation responsibilities
-> implementation flow
-> extension / change points, when relevant
-> behavior coverage
-> test / verification plan
```

For server/API input validation, read:

```text
planning/slices/cross-cutting/CC-VALIDATION-001-server-request-validation-and-fluentvalidation.md
```

For server implementation responsibility boundaries, read:

```text
planning/slices/server/server-implementation-principles.md
```

## 7. Client Sidecar Drafts

Client sidecar drafts use this discovery chain:

```text
client behavior to implement
-> source/UI behavior mapping
-> Visual UI / Scenario Flow
-> Visual Client Implementation Flow
-> Questions / Decisions
-> Client Extension / Change Points, when relevant
-> Behavior Coverage
-> Client / Component / E2E Verification Plan
-> Covered Scenario / UI Behavior Items
```

Client sidecars must identify:

```text
- read context or command action;
- page / feature / entity / shared placement;
- generated OpenAPI types used;
- generated constants/error codes used;
- DTO field -> form field mapping;
- ProblemDetails parsing/mapping;
- component/client tests;
- E2E only for completed cross-layer behavior;
- assumptions and extension/change points.
```

## 8. Cross-Cutting / Helper Drafts

Cross-cutting/helper drafts start from concern-derived behavior.

They use this discovery chain:

```text
concern-derived behavior items
-> Visual Concern / Scenario Flow, when useful
-> Concern Slice Flow
-> Visual Implementation Flow
-> Implementation Flow
-> Extension / Change Points, when relevant
-> Behavior Coverage
-> Test / Check Plan
-> Consumer Rule
```

Cross-cutting/helper slices are still slices. They must not skip behavior/flow/test planning just because the concern is technical.

## 9. Behavior Coverage Rule

Behavior Coverage answers:

```text
Does the draft description cover the required source behavior?
```

Behavior Coverage is not Scope and not Test Coverage.

For section-level meaning, use:

```text
planning/slices/slice-draft-authoring-principles.md
```

## 10. Test / Verification Plan Rule

Test / Verification Plan answers:

```text
How will implemented code or UI behavior be verified later?
```

E2E tests assert visible cross-layer outcomes, not internal cache/refetch/query-key mechanics or backend ownership internals.

Use:

```text
planning/slices/slice-test-plan-workflow.md
```

## 11. Visual Flow Rule

Visual flows are diagram-like text maps used to make responsibility, branching and boundaries clear.

Visual Scenario/UI Flow shows actor/system/user-visible behavior.

Visual Implementation Flow shows technical responsibility boundaries.

## 12. Implemented Slice Sync Rule

If a slice draft already has implementation, discovery must include current code and tests.

Use:

```text
planning/documentation/status-reconciliation-workflow.md
```

Do not rewrite an implemented slice draft only to match a newer template without checking source mapping, domain docs, implementation files, generated artifacts and tests.

## 13. Do Not

```text
- Do not treat the first draft as final.
- Do not hide questions in prose only.
- Do not leave question status implicit.
- Do not proceed on an unresolved question without writing the assumption/current direction.
- Do not implement through unresolved behavior/API/client/testing questions.
- Do not create client sidecars before client work starts.
- Do not invent behavior items inside a slice draft.
- Do not mix Behavior Coverage with Test / Verification Plan.
- Do not skip server validation planning for API input slices.
- Do not use this file as the owner for domain drafting, scenario drafting or documentation/status reconciliation.
- Do not tell another chat it can modify domain/docs/generated artifacts unless that scope is explicit.
```
