# Draft-Driven Discovery Principles

Status: current slice discovery principle / scope-safe and validation-aware  
Scope: domain drafts, business slices, client sidecars, cross-cutting/helper slices and documentation/status drafts

## 1. Purpose

Draft-driven discovery means we do not try to fully design implementation in one perfect pass.

We create a draft, use it to discover missing behavior, questions, assumptions, contract gaps, flow gaps, extension/change pressure and verification gaps, then refine the next draft or move to implementation when the remaining risk is acceptable.

## 2. Core Loop

```text
source requirements
-> draft
-> open questions and assumptions
-> visual flow maps
-> extension/change point review
-> detailed flow
-> behavior coverage
-> implementation direction
-> test / verification planning
-> status reconciliation
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

```text
domain model drafts
business slice drafts
client sidecar drafts
cross-cutting/helper slice drafts
testing/support slice drafts
documentation/status reconciliation drafts
diagram planning drafts
```

## 5. Draft Formats

Default early format:

```text
shortened working draft
```

Full slice format:

```text
full backend / business / cross-cutting / client sidecar draft
```

The practical slice drafting rules and templates live in:

```text
planning/slices/l1-slice-drafting-guide.md
```

## 6. Business Slice Drafts

Business slice drafts use this discovery chain:

```text
scenario-derived behavior items
-> Visual Scenario Flow
-> Scenario Slice Flow
-> Visual Implementation Flow
-> Implementation Flow
-> Extension / Change Points, when relevant
-> Behavior Coverage
-> Test / Verification Plan
```

If server API input is involved, include request-level validation planning and read:

```text
planning/slices/cross-cutting/CC-VALIDATION-001-server-request-validation-and-fluentvalidation.md
```

## 7. Client Sidecar Drafts

Client sidecar drafts use this discovery chain:

```text
client behavior to implement
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

## 9. Behavior Coverage Rule

Behavior Coverage answers:

```text
Does the draft description cover the required source behavior?
```

Behavior Coverage is not Test Coverage.

## 10. Test / Verification Plan Rule

Test / Verification Plan answers:

```text
How will implemented code or UI behavior be verified later?
```

E2E tests assert visible cross-layer outcomes, not internal cache/refetch/query-key mechanics or backend ownership internals.

## 11. Visual Flow Rule

Visual flows are diagram-like text maps used to make responsibility, branching and boundaries clear.

Visual Scenario/UI Flow shows actor/system/user-visible behavior.

Visual Implementation Flow shows technical responsibility boundaries.

## 12. Do Not

```text
- Do not treat the first draft as final.
- Do not hide questions in prose only.
- Do not leave question status implicit.
- Do not proceed on an unresolved question without writing the assumption/current direction.
- Do not implement through unresolved behavior/API questions.
- Do not create client sidecars before client work starts.
- Do not invent behavior items inside a slice draft.
- Do not mix Behavior Coverage with Test / Verification Plan.
- Do not skip server validation planning for API input slices.
- Do not skip docs/status reconciliation after implementation changes.
- Do not tell another chat it can modify domain/docs/generated artifacts unless that scope is explicit.
```
