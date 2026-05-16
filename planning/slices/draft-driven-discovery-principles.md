# Draft-Driven Discovery Principles

Status: current slice discovery principle  
Scope: domain drafts, business slices, client sidecars, cross-cutting/helper slices and documentation/status drafts

## 1. Purpose

Draft-driven discovery means we do not try to fully design implementation in one perfect pass.

We create a draft, use it to discover missing behavior, questions, assumptions, contract gaps, flow gaps, extension/change pressure and verification gaps, then refine the next draft or move to implementation when the remaining risk is acceptable.

This was used for domain planning and must also apply to all slice work.

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

The loop is intentionally iterative.

A draft can be useful before it is complete, as long as unresolved questions, assumptions, coverage gaps and extension/change pressure are visible.

When a draft proceeds on a working assumption, the assumption must be explicit enough for review.

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
- questions and coverage discovery;
- early extension/change point review.
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

Primary shortened example:

```text
planning/slices/examples/L1-APPLICANT-PARTY-READ-CURRENT-early-short-draft-example.md
```

Use this example when a short draft needs assumptions, extension/change points and shared register sync.

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
-> Extension / Change Points, when relevant
-> Behavior Coverage
-> Test / Verification Plan
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
- which assumptions are being used until questions are answered;
- which extension/change points affect current design;
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
shared question/register update
extension points register update
implementation notes register update
```

## 7. Client Sidecar Drafts

Client sidecar drafts are created only when concrete client work starts.

They use this discovery chain:

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
- assumptions being used until those questions are resolved;
- extension/change points and anti-coupling constraints.
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
-> Extension / Change Points, when relevant
-> Behavior Coverage
-> Test / Check Plan
-> Consumer Rule
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
-> replacement file plan
-> archive
-> next reconciliation
```

Documentation drafts must not overclaim implementation status.

If docs and repo evidence disagree, record the disagreement before updating status wording.

## 10. Question Rule

If a draft exposes a question that can change behavior, API contract, client architecture, testing responsibility, E2E scope, scenario meaning, extension/change pressure or diagram interpretation, stop or record it prominently before implementation continues.

Open questions and unresolved risks should appear before accepted decisions.

Every non-trivial question should have:

```text
ID
area
question status
question
assumption / current direction
impact
shared register or local-only reason
```

Use clear question statuses:

```text
open
blocked
assumption
accepted direction
future review
resolved
superseded
local only
```

Assumption rule:

```text
If work proceeds before a final answer exists, write a reasonable assumption/current direction.
The assumption should be clear enough for the user to confirm, reject or refine.
Do not hide assumptions in prose.
Do not mark assumptions as resolved.
```

Accepted decisions are still recorded, but they should not hide unresolved questions below them.

## 11. Extension / Change Point Rule

Known extension/change pressure must be reviewed during slice drafting.

Use:

```text
planning/slices/change-extension-points-principles.md
planning/slices/slice-extension-points-register.md
```

A short draft may include a compact extension/change point table.

A full draft may include detailed extension/change point sections.

Use this classification:

```text
hard invariant
change point
extension point
extension pressure
anti-coupling decision
implementation note
```

For each relevant item, decide one current handling:

```text
explicit seam now
anti-coupling only
convention-first
ignore for now
revisit when future slice starts
```

Mirror future-facing items to the extension register or implementation notes register.

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
- decision or question that limits coverage;
- extension/change point that constrains scope.
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
- Do not leave question status implicit.
- Do not proceed on an unresolved question without writing the assumption/current direction.
- Do not mark assumptions as resolved.
- Do not implement through unresolved behavior/API questions.
- Do not create client sidecars before client work starts.
- Do not invent behavior items inside a slice draft.
- Do not mix Behavior Coverage with Test / Verification Plan.
- Do not skip visual flow maps in full slice files when branching or boundaries matter.
- Do not skip extension/change point review when future behavior can affect current design.
- Do not skip draft flow for technical/cross-cutting concerns.
- Do not skip docs/status reconciliation after implementation changes.
```
