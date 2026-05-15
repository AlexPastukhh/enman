# Draft-Driven Discovery Principles

Status: current slice discovery principle  
Scope: domain drafts, business slices, client sidecars, cross-cutting/helper slices and documentation/status drafts

## 1. Purpose

Draft-driven discovery means we do not try to fully design implementation in one perfect pass.

We create a draft, use it to discover missing behavior/questions/contract gaps, resolve or record them, then refine the next draft.

This was used for domain planning and must also apply to all slice work.

## 2. Core Loop

```text
source requirements
-> draft
-> questions and assumptions
-> behavior coverage
-> flow coverage
-> implementation/test planning
-> status reconciliation
-> next draft or implementation step
```

## 3. Applies To

Draft-driven discovery applies to:

```text
domain model drafts
business slice drafts
client sidecar drafts
cross-cutting/helper slice drafts
testing/support slice drafts
documentation/status reconciliation drafts
diagram planning drafts
```

It is not domain-only.

## 4. Why It Matters

Drafts reveal:

```text
- missing scenario behavior;
- missing DATA / validation / UI details;
- API contract gaps;
- generated artifact gaps;
- client component placement questions;
- test responsibility boundaries;
- extension/change pressure;
- stale docs;
- implementation blockers.
```

## 5. Business Slice Drafts

Business slice drafts use:

```text
scenario-derived behavior items
-> Scenario Slice Flow
-> Implementation Flow
-> tests
```

Draft questions can lead back to:

```text
scenario spec update
DATA update
validation addendum
behavior item update
API contract update
client sidecar update
cross-cutting/helper slice update
```

## 6. Client Sidecar Drafts

Client sidecar drafts are created only when concrete client work starts.

They use:

```text
client behavior to implement
-> client questions/assumptions
-> contract used by client
-> client coverage matrix
-> client implementation flow
-> component/API/hook/form/test planning
```

Client draft-driven discovery must identify:

```text
- whether behavior is read context or command action;
- page / feature / entity / shared placement;
- generated OpenAPI types used;
- generated constants/error codes used;
- DTO field -> form field mapping;
- client validation vs server validation boundary;
- ProblemDetails parsing/mapping;
- component/client tests;
- E2E only for completed cross-layer flow;
- temporary handwritten DTOs, if any, until CC-API contract is complete;
- open questions that block or affect implementation.
```

A `.client.md` file should not be created in advance, but once client work starts it becomes the draft/discovery file for that client slice.

## 7. Cross-Cutting / Helper Drafts

Cross-cutting/helper drafts use:

```text
concern-derived behavior items
-> Concern Slice Flow
-> Implementation Flow
-> tests/checks
-> consumer rules
```

Concern-derived source types include:

```text
security-derived
API-contract-derived
tooling-derived
testing-derived
client-cross-cutting-derived
infrastructure-derived
```

## 8. Documentation Drafts

Documentation/status reconciliation drafts use the same idea:

```text
current repo facts
-> stale/missing docs list
-> questions/assumptions
-> replacement file plan
-> archive
-> next reconciliation
```

## 9. Question Rule

If a draft exposes a question that can change behavior/API/client architecture/testing, stop or record it prominently before implementation continues.

Each question should have:

```text
ID
area
question
current assumption/preferred answer
impact
status
```

## 10. Coverage Rule

Every draft should make coverage visible:

```text
behavior item
flow step
implementation step
test/check coverage
status
```

For client sidecars, coverage should also include:

```text
route/page
feature/component
API function
hook
form/validation
error handling
test coverage
```

## 11. Do Not

```text
- Do not treat the first draft as final.
- Do not hide questions in prose only.
- Do not implement through unresolved behavior/API questions.
- Do not create client sidecars before client work starts.
- Do not skip draft flow for technical/cross-cutting concerns.
- Do not skip docs/status reconciliation after implementation changes.
```
