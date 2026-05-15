# Draft-Driven Discovery Principles

Status: current slice discovery principle  
Scope: domain drafts, business slices, client sidecars, cross-cutting/helper slices and documentation/status drafts

## 1. Purpose

Draft-driven discovery means we do not try to fully design implementation in one perfect pass.

We create a draft, use it to discover missing behavior, questions, contract gaps, flow gaps and verification gaps, then refine the next draft or move to implementation when the remaining risk is acceptable.

This was used for domain planning and must also apply to all slice work.

## 2. Core Loop

```text
source requirements
-> draft
-> open questions and assumptions
-> visual flow maps
-> detailed flow
-> behavior coverage
-> implementation direction
-> test / verification planning
-> local/global register sync
-> status reconciliation
-> next draft or implementation step
```

The loop is intentionally iterative.

A draft can be useful before it is complete, as long as unresolved questions, assumptions and coverage gaps are visible.

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

## 4. Draft Formats

Slice drafts can use different working formats depending on stage.

Default early format:

```text
shortened working draft
```

Use it for:

```text
- early discussion;
- scenario logic checks;
- slice boundary checks;
- implementation direction;
- questions and coverage discovery.
```

Full slice format:

```text
full backend / business / cross-cutting / client sidecar draft
```

Use it for:

```text
- final documentation pass;
- detailed review;
- debugging complex logic;
- transfer into planning docs;
- preparation for implementation by another agent.
```

The practical slice drafting rules and templates live in:

```text
planning/slices/l1-slice-drafting-guide.md
```

## 5. Why It Matters

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

## 6. Business Slice Drafts

Business slice drafts start from accepted source behavior.

They use this discovery chain:

```text
scenario-derived behavior items
-> Visual Scenario Flow
-> Scenario Slice Flow
-> Visual Implementation Flow
-> Implementation Flow
-> Behavior Coverage
-> Test / Verification Plan
-> Local / Shared Register Sync
```

Visual flows are review maps.

Detailed flows are the source-linked explanation of scenario behavior and implementation responsibility.

Behavior Coverage and Test / Verification Plan are separate sections.

A business slice draft should make visible:

```text
- which scenario behavior it covers;
- which source behavior items are in scope;
- which behavior is delegated to dependent slices;
- which API contract is used or introduced;
- which implementation layers are responsible;
- which open questions can still change behavior, API, testing or client planning;
- which local questions are mirrored to the shared slice question register;
- how implementation will be verified after behavior coverage is defined.
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
slice question register update
extension/implementation notes register update
```

## 7. Client Sidecar Drafts

Client sidecar drafts are created only when concrete client work starts.

They use this discovery chain:

```text
client behavior to implement
-> Visual UI / Scenario Flow
-> Visual Client Implementation Flow
-> Questions / Decisions
-> Behavior Coverage
-> Client / Component / E2E Verification Plan
-> Covered Scenario / UI Behavior Items
-> Local / Shared Register Sync
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
- temporary handwritten DTOs, if any, until generated contract coverage is complete;
- open questions that block or affect implementation;
- shared register updates needed for future client work.
```

A `.client.md` file should not be created in advance.

Once client work starts, it becomes the draft/discovery file for that client slice.

Client architecture placement is not a behavior item.

Placement belongs to Visual Client Implementation Flow, component discovery or client architecture notes.

## 8. Cross-Cutting / Helper Drafts

Cross-cutting/helper drafts start from concern-derived behavior.

They use this discovery chain:

```text
concern-derived behavior items
-> Visual Concern / Scenario Flow, when useful
-> Concern Slice Flow
-> Visual Implementation Flow
-> Implementation Flow
-> Behavior Coverage
-> Test / Check Plan
-> Consumer Rule
-> Local / Shared Register Sync
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

Concern-derived behavior items are first-class behavior items.

Do not hide cross-cutting behavior only in prose or implementation notes.

## 9. Documentation Drafts

Documentation/status reconciliation drafts use the same idea:

```text
current repo facts
-> stale/missing docs list
-> questions/assumptions
-> local/global sync check
-> replacement file plan
-> archive
-> next reconciliation
```

Documentation drafts must not overclaim implementation status.

If docs and repo evidence disagree, record the disagreement before updating status wording.

## 10. Question Rule

If a draft exposes a question that can change behavior, API contract, client architecture, testing responsibility, E2E scope, scenario meaning or diagram interpretation, stop or record it prominently before implementation continues.

Each question should have:

```text
ID
area
question
current assumption/preferred answer
impact
status
shared register link, if mirrored
```

In slice drafts, open questions and unresolved risks should appear before accepted decisions.

Accepted decisions are still recorded, but they should not hide unresolved questions below them.

## 11. Local / Shared Register Rule

Local `Questions / Decisions` sections are required.

They keep local context.

Shared registers are also required when the question or note can affect future work.

Use:

```text
planning/slices/slice-questions-register.md
planning/slices/slice-extension-points-register.md
planning/slices/slice-implementation-notes-register.md
```

Rules:

```text
- Mirror currently relevant local slice/client/cross-cutting questions to `slice-questions-register.md`.
- Mirror extension/change pressure to `slice-extension-points-register.md`.
- Mirror concrete future implementation/client/testing notes to `slice-implementation-notes-register.md`.
- If a question remains local only, state why.
- If a shared register row becomes stale, update or supersede it.
```

## 12. Behavior Coverage Rule

Behavior Coverage answers:

```text
Does the draft description cover the required source behavior?
```

Behavior Coverage must link source behavior to draft explanation.

Preferred format:

| Scenario / concern behavior item | How draft covers it | Draft location | Status |
|---|---|---|---|

Behavior Coverage may reference:

```text
- source behavior item ID;
- scenario text spec;
- DATA/UI spec;
- validation/security addendum;
- concern-derived behavior item;
- specific visual/detailed flow section;
- decision or question that limits coverage.
```

Behavior Coverage is not Test Coverage.

A draft can have behavior coverage gaps even when the test plan is detailed.

A draft can also have good behavior coverage before final test details are known.

## 13. Test / Verification Plan Rule

Test / Verification Plan answers:

```text
How will implemented code or UI behavior be verified later?
```

Preferred format:

| Test / check | Verifies | Layer | Status |
|---|---|---|---|

Verification planning may include:

```text
- domain/unit tests;
- application/API integration tests;
- generated artifact checks;
- client/component tests;
- E2E tests only for completed cross-layer behavior;
- manual/documentation checks where appropriate.
```

Do not use the verification plan to replace Behavior Coverage.

Do not treat an E2E test as proof that every UI detail is covered.

## 14. Visual Flow Rule

Visual flows are diagram-like text maps used to make responsibility, branching and boundaries clear.

They should be used before detailed flow sections in full slice files.

Visual Scenario Flow shows:

```text
- actor/system behavior;
- success path;
- important failure/no-write branches;
- out-of-scope or dependent slices;
- visible outcome.
```

Visual Implementation Flow shows:

```text
- API or UI entry point;
- application/use-case boundary;
- domain responsibility;
- persistence responsibility;
- error/no-write path;
- generated contract/client convention boundary when relevant.
```

A linear arrow list can be acceptable for early shortened drafts.

Full slice files should use diagram-like maps when branching, boundaries or dependent slices matter.

## 15. Do Not

```text
- Do not treat the first draft as final.
- Do not hide questions in prose only.
- Do not implement through unresolved behavior/API questions.
- Do not create client sidecars before client work starts.
- Do not invent behavior items inside a slice draft.
- Do not mix Behavior Coverage with Test / Verification Plan.
- Do not skip visual flow maps in full slice files when branching or boundaries matter.
- Do not leave important local questions only in local files when they affect future work.
- Do not skip draft flow for technical/cross-cutting concerns.
- Do not skip docs/status reconciliation after implementation changes.
```
