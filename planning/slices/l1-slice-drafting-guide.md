# L1 Slice Drafting Guide

Status: current slice drafting workflow  
Scope: deriving L1 scenario/application slices after the first green L1 domain foundation

## 1. Purpose

This guide defines how to create slice drafts after the L1 domain foundation is implemented and unit-tested.

The goal is to move from:

```text
scenarios
-> scenario-derived behavior items
-> independently testable behavior slices
-> implementation path by layer
-> coverage / tests / ADR candidates
```

A slice draft is not an endpoint list.

A slice draft explains how scenario behavior becomes separately testable implementation work.

## 2. Working Definition

```text
Slice = independently testable unit of observable behavior
        + implementation path needed to deliver/test that behavior.
```

A slice has both:

```text
1. observable behavior;
2. implementation path.
```

Observable behavior examples:

```text
accepted applicant data creates current active unverified ApplicantParty
valid request creates persisted InReview request
employee approval makes request Approved and verifies ApplicantParty
employee rejection makes request Rejected with optional feedback
client sees only own requests
employee sees requests available for review
```

Implementation path examples:

```text
domain classes
application service / orchestration
persistence transaction
read model / query
API boundary
UI behavior
auth/framework guard
external adapter / plugin
unit/integration/UI tests
```

## 3. Slice Discovery Questions

Ask these questions for each scenario before naming slices.

### 3.1 Behavior Questions

```text
1. What are the separate observable behavior units in this scenario?
2. What does the user or system observe when each behavior succeeds?
3. What failure/no-write behavior is observable?
4. Which behavior is command-like and changes state?
5. Which behavior is read/visibility-only?
6. Which behavior is UI-only or UX-only?
7. Which behavior extends an existing flow rather than creating a new core flow?
8. Which behavior depends on another slice being implemented first?
9. Which behavior could be a plugin/replaceable module or external-integration slice?
10. Which behavior is cross-cutting across multiple scenarios?
```

### 3.2 Testability Questions

```text
1. Can this behavior be tested independently?
2. Can it be tested without full UI?
3. What is the lowest useful test level?
4. What domain unit tests are enough?
5. What application tests are needed?
6. What integration tests are needed?
7. What UI/component/E2E tests are needed?
8. Does it need a database?
9. Does it need an external adapter fake?
10. What no-write behavior must be tested?
```

Independent testability is a primary slice boundary criterion.

If the behavior cannot be tested independently, the slice is probably too large, too vague, or missing dependencies.

### 3.3 Implementation Path Questions

```text
1. Which domain classes are touched?
2. Which application service or orchestration is needed?
3. Which persistence transaction is needed?
4. Is this write persistence or read model/query work?
5. Is an API boundary needed?
6. Is UI needed for this slice, or is UI deferred?
7. Is auth/framework behavior needed?
8. Is infrastructure/external dependency needed?
9. Which existing implementation must remain compatible?
10. Which old/current implementation names are legacy background, not target design?
```

### 3.4 Dependency / Extension Questions

```text
1. Does this slice depend on another slice?
2. Does this slice extend another slice?
3. Can this slice be implemented as a plugin or replaceable module?
4. What can be postponed without breaking the core behavior?
5. What must be implemented together to make the slice testable?
6. Which future changes should be marked EXT / VAR / RISK / ADR?
```

### 3.5 ADR / Decision Questions

```text
1. Does this slice force a decision that affects multiple slices?
2. Does it change a layer boundary?
3. Does it introduce or avoid a port/adapter?
4. Does it choose read projection instead of write-model duplication?
5. Does it decide transaction boundaries between aggregates?
6. Is there a meaningful alternative?
7. Would this decision be useful to explain in the diploma?
8. Is it expensive to change later?
```

If yes, add an ADR candidate.

## 4. Coverage Source Model

Before writing scenario sections, prepare a small coverage source table.

Use this format in slice drafts:

```markdown
| Scenario | Scenario ref | Text spec / DATA source | Existing behavior items | Candidate missing items | Notes |
|---|---|---|---|---|---|
```

Sources:

```text
scenario text spec
scenario DATA file
pre-domain-variants-input.md
scenario-behavior-baseline-account-activation-addendum.md
domain-draft-01.md
l1-domain-implementation-cut.md
l1-domain-testing-rules.md
```

The existing behavior baseline may not contain every UI/read/UX behavior needed for slice planning.

When a needed behavior item is missing, add it as a candidate item in the slice draft.

Candidate item examples:

```text
UI-CAND-REQ-001: Request creation screen shows validation feedback without creating request.
UI-CAND-REVIEW-001: Employee sees warning when rejecting without feedback.
READ-CAND-REQ-001: Employee dashboard indicates which requests can be reviewed.
AUTH-CAND-001: Protected action fails for non-active account before business command execution.
```

Candidate items are not official baseline items until reviewed.

## 5. Scenario Section Structure

Each scenario section should use this structure.

```text
Scenario:
Scenario ref:
Short behavior summary:
Relevant DATA facts:
Existing scenario-derived behavior items:
Candidate missing behavior items:
Slice discovery answers:
Derived slices:
Questions for this scenario:
Notes:
```

Do not start from controllers, endpoints, repositories, tables, or UI components.

Start from observable behavior.

## 6. Slice Card Structure

Inside each scenario section, describe each derived slice using this structure.

```text
Slice:
Slice ref:
Type:
Source scenario(s):
Scenario-derived behavior items:
Additional candidate items:
Relevant DATA facts:
Observable behavior:
Why this is a separate slice:
Independent testability:
User-visible result:
Domain classes:
Application service / orchestration:
Persistence:
Read model:
API:
UI:
Auth / framework:
Infrastructure / external:
Tests:
Dependencies:
Out of scope:
Extension / plugin points:
Risks / ADR candidates:
Questions for implementation:
Notes:
```

## 7. Slice Types

Allowed slice types:

```text
CORE behavior slice
BACKEND / APPLICATION slice
FULL-STACK slice
READ / QUERY slice
UI-only slice
EXTENSION slice
DEPENDENT slice
CROSS-CUTTING slice
PLUGIN / EXTERNAL-INTEGRATION slice
DOMAIN-FOUNDATION cut
```

The current L1 domain implementation cut is a `DOMAIN-FOUNDATION cut`, not a full scenario slice.

## 8. Example Slice Card

```text
Slice:
Create ConnectionRequest from ApplicantParty

Slice ref:
SL-REQ-001

Type:
Backend/application slice, later full-stack

Source scenario(s):
SC-04 Client Request Creation

Scenario-derived behavior items:
REQ-CMD-CREATE-001
REQ-LC-001
REQ-IBS-003
REQ-VI-001
REQ-UCQ-001

Additional candidate items:
UI-CAND-REQ-001: Request creation screen shows validation feedback without creating request.

Relevant DATA facts:
request object location = object address
accepted request starts InReview
request is created from ApplicantParty

Observable behavior:
Valid client request creates persisted ConnectionRequest with status InReview.
Invalid request data creates no request.

Why this is a separate slice:
It is the core command behavior for request creation and can be tested separately from UI, documents, notifications and agreements.

Independent testability:
Domain unit tests cover request factory rules.
Application/integration tests later verify persistence and ownership guard.

User-visible result:
Client can submit valid request and later see it as InReview.

Domain classes:
ConnectionRequest
ApplicantParty
RequestStatus
RequestDetails
ObjectAddress

Application service / orchestration:
Load current account context.
Ensure protected action is allowed.
Load ApplicantParty.
Check ApplicantParty belongs to current account.
Call ConnectionRequest.Create(...).
Persist request.

Persistence:
ApplicantParty lookup.
ConnectionRequest write.

Read model:
Not part of command slice.
My Requests belongs to a separate read/query slice.

API:
Later command endpoint or equivalent server action.

UI:
Later request creation form.

Auth / framework:
Authenticated active client account required.

Infrastructure / external:
None in core.

Tests:
Domain unit tests first.
Application/integration tests after domain green.
UI tests later.

Dependencies:
SL-APPL-001 Create ApplicantParty.
SL-ACC-002 Protected use requires active account.

Out of scope:
Documents.
Notifications.
Anonymous request.
Agreement proposal.

Extension / plugin points:
Document attachment can extend request creation later.

Risks / ADR candidates:
Read projection should not force ClientAccountId into ConnectionRequest.

Questions for implementation:
Should request creation require current active ApplicantParty or allow selected historical verified ApplicantParty?

Notes:
Keep write aggregate applicant-centric.
```

## 9. Per-Slice Coverage

Every slice draft should include per-slice coverage.

Use:

```markdown
| Slice ref | Slice name | Source scenario(s) | Behavior items | Candidate items | Covered behavior | Not covered / deferred | Notes |
|---|---|---|---|---|---|---|---|
```

This shows what each slice covers and what it intentionally defers.

## 10. Behavior Item Coverage

Use this table near the end of the slice draft.

```markdown
| Behavior item | Readable requirement | Source scenario | Covered by slice | Coverage status | Layer placement | Notes |
|---|---|---|---|---|---|---|
```

Coverage statuses:

```text
Covered by domain foundation
Planned in slice
Partial
Deferred
Out of scope for current L1
Needs candidate item
Needs clarification
```

## 11. Non-Domain / Deferred Coverage

Use this table for behavior that was not covered in domain classes.

```markdown
| Item | Requirement | Not covered in domain because | Planned slice | Layer | Test strategy | Notes |
|---|---|---|---|---|---|---|
```

Examples:

```text
Client sees only own requests -> read/query slice, not aggregate method.
Protected action fails for non-active account -> auth/application guard, not every business aggregate.
Employee sees warning when rejecting without feedback -> UI slice, not domain invariant.
```

## 12. Test Coverage

Use this table near the end of the slice draft.

```markdown
| Slice | Behavior | Domain unit tests | Application tests | Integration tests | UI/E2E tests | Status | Notes |
|---|---|---|---|---|---|---|---|
```

Testing rule:

```text
Unit tests first for domain foundation.
Application/integration tests after domain behavior is green.
UI/E2E tests only when the backend/API/UI flow is stable enough.
```

## 13. Questions Sections

Questions must appear both locally and centrally.

### 13.1 Local Questions

Each scenario section should include:

```text
Questions for this scenario:
```

Each slice card should include:

```text
Questions for implementation:
```

### 13.2 Consolidated Questions

At the end of the draft, include all questions in one place.

Recommended format:

```markdown
| Question | Source scenario / slice | Why it matters | Blocks implementation? | Candidate decision | ADR? | Notes |
|---|---|---|---|---|---|---|
```

Categories:

```text
Scenario clarification
Domain/application boundary
Persistence/read model
API contract
UI/UX
Auth/framework
Infrastructure/external
Testing
ADR / architecture decision
```

## 14. ADR Candidate Collection

Each slice may produce ADR candidates.

Do not write full ADRs immediately unless explicitly requested.

Collect candidates in:

```text
planning/adr/adr-candidates.md
```

Add an ADR candidate when a decision:

```text
- affects multiple slices;
- changes layer boundaries;
- has meaningful alternatives;
- is useful for diploma architecture explanation;
- is expensive to change later;
- introduces a plugin/external provider boundary;
- decides read model vs write model duplication;
- decides transaction boundaries between aggregates.
```

## 15. Draft Iteration Pattern

Slice drafts should evolve like domain drafts.

Suggested sequence:

```text
l1-slice-draft-01.md
  initial scenario-driven slice discovery
  candidate slices
  questions
  rough coverage

l1-slice-draft-02.md
  refined boundaries
  improved test strategy
  clearer dependencies
  fewer open questions

l1-scenario-to-slice-map.md
  stable mapping from scenarios/items to slices

l1-slice-delivery-plan.md
  implementation order and delivery plan
```

Do not create all files at once unless explicitly requested.

## 16. Done Criteria For A Slice Draft

A slice draft is acceptable when:

```text
- scenarios are the primary organizing unit;
- behavior items are listed with readable meanings;
- candidate missing UI/read/UX items are not hidden;
- each slice has observable behavior;
- each slice has independent testability notes;
- each slice maps to domain/application/persistence/API/UI/auth/infra as needed;
- per-slice coverage is present;
- item coverage is present;
- non-domain/deferred coverage is present;
- test coverage is present;
- questions are captured locally and consolidated centrally;
- ADR candidates are collected.
```
