# Domain Draft Generation Guide

Status: current guide for gradual domain discovery  
Scope: how to create iterative domain drafts from scenario behavior coverage baseline

## 1. Purpose

This guide describes how to create domain drafts.

The goal is gradual domain discovery, not comparison of competing alternatives.

Each draft is a complete snapshot of current domain understanding.

Each next draft should:

```text
- make the model more explicit;
- cover more scenario-derived behavior items;
- reduce Missing / Partial / Question items;
- refine class/aggregate boundaries;
- refine value objects;
- refine methods and state handling;
- record what is intentionally placed outside the current domain model.
```

## 2. Inputs

Use:

```text
planning/diagrams/scenario-text-specs/
planning/diagrams/scenario-data/
planning/diagrams/scenario-text-specs/scenario-server-domain-validation-addendum.md
planning/diagrams/scenario-text-specs/scenario-account-activation-security-addendum.md
planning/tables/pre-domain-variants-input.md
planning/tables/scenario-behavior-baseline-account-activation-addendum.md
```

Optional context:

```text
planning/diagrams/scenario-diagram-consistency-report.md
planning/scenario-specification-principles.md
planning/scenario-domain-validation-principles.md
```

## 3. Important Terminology

Use:

```text
domain draft
iterative domain draft
gradual domain discovery
scenario behavior coverage baseline
```

Avoid using “domain variant” to mean competing alternatives.

If historical files still say `variant`, interpret it as:

```text
an iterative draft toward the final domain model
```

unless the file is explicitly marked historical/superseded.

## 4. What A Draft Must Do

A draft must answer baseline item IDs from:

```text
planning/tables/pre-domain-variants-input.md
planning/tables/scenario-behavior-baseline-account-activation-addendum.md
```

A baseline item is not “something the domain class must implement.”

A baseline item is:

```text
something the system must explain/cover.
```

The draft may cover it with:

```text
- domain class / aggregate;
- value object;
- domain service;
- application/use-case orchestration;
- read/query/access placement;
- auth/security policy;
- DB constraint;
- infrastructure/integration;
- future/deferred decision.
```

## 5. Required Draft Structure

```text
# Domain Draft N

## 1. Draft Goal

## 2. Source Inputs

## 3. Current Domain Direction

## 4. Aggregate / Class Candidates

## 5. Class-By-Class Model

## 6. Class / Aggregate State Machines

## 7. Impossible States Covered By Current Model

## 8. Value Objects / Value Integrity Coverage

## 9. Use-Case Coordination Decisions

## 10. Coverage Against Scenario Behavior Baseline

## 11. Cross-Layer Placement Notes

## 12. Scenario Questions / Gaps For Next Draft

## 13. What Changed Since Previous Draft
```

Each draft must contain all sections.

Early drafts may have `Partial`, `Question` or rough answers.

Later drafts should improve detail and coverage.

## 6. Class-By-Class Model

For each candidate class/aggregate, describe:

```text
- purpose;
- state it owns;
- commands/methods it may expose;
- invariants it protects;
- value objects it uses;
- lifecycle/status if any;
- impossible states it prevents;
- baseline item IDs it covers;
- open questions.
```

Do not force every baseline item into a domain class.

If an item is better handled elsewhere, record that in coverage and cross-layer notes.

## 7. State / Condition Handling

Use class-level state machines in the draft only after a draft proposes the owning class/aggregate.

The pre-domain baseline contains scenario state/condition matrices.

The draft answers them with concrete model decisions.

A state/condition matrix is useful when command availability depends on:

```text
- status/state/stage/phase;
- exists / does not exist;
- null / not null;
- used / unused;
- valid / expired;
- count = 0 / count >= 1 / limit reached;
- already done / not done;
- final / not final.
```

Account activation can be modeled as account/auth lifecycle state if the draft includes account/auth boundary.

If current draft keeps account/auth mostly outside business domain, account activation still must be covered as policy/guard placement.

Do not create state machines for pure value objects such as:

```text
EmailAddress
PhoneNumber
ObjectAddress
PassportData
SNILS / INN / OGRN
```

Those belong to value integrity.

## 8. Account Activation Placement

Domain drafts must explicitly answer account activation items from:

```text
planning/tables/scenario-behavior-baseline-account-activation-addendum.md
```

Current implementation direction:

```text
- Account has activation marker/state.
- Current core registration creates Active account.
- Protected use cases are guarded by application service.
- Application service calls account/domain method such as Account.EnsureActivated.
```

Future implementation direction:

```text
- Use authorization policy such as AccountActivatedPolicy / RequireActivatedAccount.
- Policy may be backed by an additional claim, e.g. account_activated = true, to avoid database lookup on every protected request.
- Draft should record claim-refresh/staleness as scenario/security question if claim-based enforcement is chosen.
```

Important boundary:

```text
Request, AgreementProposal and ApplicantData should not all own account activation logic.
They may assume active actor context if application/auth policy enforces it before protected use case execution.
```

Coverage examples:

```text
ACC-SEC-001 | Resolved outside current business aggregate | Protected use cases require active account; current enforcement by application service + Account.EnsureActivated. Future enforcement by AccountActivated authorization policy / claim. | Cross-layer placement note | Decide inactive login UX.
ACC-LC-001 | Covered | Registration creates Active account in current core. | Account.Register / Account activation state | Future PendingActivation flow deferred.
ACC-SQ-001 | Question | Non-active login behavior is not fixed. | Scenario question | Choose fail-login vs limited-session vs policy-blocked actions.
```

## 9. Impossible States

Impossible states are not the same as state transitions.

State/condition matrix asks:

```text
Can command X happen under condition/state Y?
```

Impossible state asks:

```text
What business-invalid combination of data/state must never exist?
```

Examples:

```text
Approved request without review decision.
Rejected request without rejection feedback if feedback is required.
Request without object address.
Agreement proposal without attached document.
Client own proposal version created twice in core.
```

A draft should show how current classes/value objects/use cases prevent or place each relevant impossible state item.

## 10. Value Integrity / Anti-Primitive-Obsession

Do not call this broad “validation” in the draft.

Use:

```text
Value Integrity / Anti-Primitive-Obsession
```

This covers data/value shape and domain value concepts such as:

```text
EmailAddress is not just string.
PhoneNumber is not just string.
ObjectAddress is not just string.
PassportData is not random fields.
SNILS / INN / OGRN / OGRNIP have value rules.
ApplicantType changes required data shape.
Agreement document reference must be present when proposal is sent.
Proposal text/comment may be required.
```

State transition validation belongs to command/lifecycle sections.

## 11. Use-Case Coordination Decisions

A use-case coordination item is scenario-derived behavior where one user action depends on consistency between several scenario concepts, lifecycles or stored data areas.

Draft must decide how the current model handles it.

Examples:

```text
Request creation may use saved ApplicantData, but request-local edits must not mutate saved ApplicantData.

Agreement proposal can start only from Approved request.

Approval enables agreement proposal creation but does not create proposal automatically.

Verification is request-context-only and not triggered by standalone ApplicantData save.

Client sends own agreement version only in response to employee proposal.

Protected use cases require activated account, while business aggregates should not all depend directly on Account.
```

A draft may answer with:

```text
- one class/aggregate owns both concepts;
- several classes coordinated by use case/application service;
- domain service;
- read/check from another model;
- auth/security policy;
- DB constraint;
- domain event;
- deferred decision.
```

## 12. Coverage Against Scenario Behavior Baseline

Each draft must include a coverage table:

```text
Item ID | Status | Draft answer / placement | Covered by | Gap / next action
```

Coverage statuses:

```text
Missing
Partial
Covered
Resolved outside current domain model
Deferred
Question
```

Examples:

```text
REQ-LC-APPROVE-001 | Covered | Request candidate owns approval transition. | Request.Approve(...) | -
REQ-READ-OWN-001 | Resolved outside current domain model | Ownership reference exists; enforcement belongs to query/application access. | Cross-layer note | Verify during slice planning.
REQ-VI-OBJADDR-001 | Partial | ObjectAddress candidate exists. | ObjectAddress.Create(...) | Exact fields still need final check.
ACC-SEC-001 | Resolved outside current domain model | Active-account guard belongs to application/auth policy around protected use cases. | Account.EnsureActivated + future AccountActivatedPolicy | Decide inactive login UX.
```

## 13. Cross-Layer Placement Notes

This is a parking lot for behavior items discovered during domain modeling that should not be owned by current domain classes.

It is not full implementation planning.

Use it for:

```text
- read/query/access behavior;
- DB uniqueness/check constraints;
- application/use-case orchestration;
- integration/external side effects;
- infrastructure/file storage;
- security/framework behavior;
- auth policy behavior such as AccountActivatedPolicy.
```

Format:

```text
Item ID
Behavior
Why not owned by current domain class
Likely later placement
Coverage implication
```

## 14. Scenario Questions / Gaps For Next Draft

Use this section for questions that block better modeling or scenario completeness.

Questions can come from baseline `SQ` items or from draft work.

Examples:

```text
If user created account but did not activate it and then tries to log in, what should happen?

Does account activation apply to both client and employee accounts?

If account_activated claim is used, how is stale claim refreshed when activation status changes?

Is rejection feedback mandatory in core?
```

## 15. What Changed Since Previous Draft

Every draft after draft 1 should list:

```text
- newly covered baseline items;
- items changed from Missing to Partial;
- items changed from Partial to Covered;
- questions resolved;
- new questions introduced;
- class/state/method ownership changes.
```

## 16. Draft Quality Checklist

Before finishing a draft, check:

```text
- it references baseline item IDs;
- it does not silently invent scenario behavior;
- it explains what is covered by domain model and what is placed elsewhere;
- state/condition behavior is explicit for lifecycle entities;
- impossible states are addressed or marked as gaps;
- value integrity items are addressed or marked as gaps;
- use-case coordination items are answered or left as explicit questions;
- account activation items are covered through Account/application/auth policy placement;
- coverage table has fewer vague statements than previous draft.
```

## 17. Current Output Folder

Use:

```text
planning/tables/domain-drafts/
```

First draft:

```text
planning/tables/domain-drafts/domain-draft-01.md
```
