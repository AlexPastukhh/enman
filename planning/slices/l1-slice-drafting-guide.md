# L1 Slice Drafting Guide

Status: current slice drafting workflow  
Scope: business slices, cross-cutting/helper slices, client sidecars, implementation flow and tests

## 1. Draft-Driven Discovery Gate

Before drafting any slice, read:

```text
planning/slices/draft-driven-discovery-principles.md
```

All slice work uses draft-driven discovery:

```text
source requirements
-> draft
-> questions/assumptions
-> coverage
-> flow
-> implementation/test planning
-> next draft or implementation
```

This applies to:

```text
business slices
client sidecars
cross-cutting/helper slices
testing/support slices
documentation/status reconciliation drafts
```

## 2. Default Shortened Slice Draft

By default, slice drafts are created in a shortened working format.

A shortened draft is not the full final slice file.

It is an early working format for discussion, scenario logic checks, slice boundary checks, implementation direction, questions and coverage.

The default shortened draft includes:

```text
1. Visual Scenario Flow
2. Visual Implementation Flow
3. Questions / Decisions
4. Behavior Coverage
5. Test / Verification Plan, when useful
6. Covered Scenario Behavior Items
```

Generate a full draft file only on explicit request or when the work needs:

```text
- final documentation pass;
- detailed review;
- debugging complex logic;
- transfer into planning docs;
- preparation for implementation by another agent.
```

A shortened draft may use temporary source behavior labels such as:

```text
Source BI TBD
```

only when source behavior item IDs have not been attached yet.

That marker is temporary and must be replaced with real scenario behavior IDs before finalizing the slice draft.

## 3. Visual Scenario Flow Rule

Visual Scenario Flow shows the real scenario flow, or the part of the scenario flow, covered by the slice.

It must describe user/system behavior, not controller, handler, repository, mapper or DTO mechanics.

Good scenario flow:

```text
[Client]
Submits connection request data
        ↓
[System]
Creates connection request for current account applicant context
        ↓
[System]
Moves request into review
        ↓
[Client]
Sees success outcome
```

Bad scenario flow:

```text
[Controller]
Extracts claim
        ↓
[Repository]
Loads applicant party
        ↓
[DbContext]
Saves entity
```

That is implementation flow, not scenario flow.

## 4. Visual Implementation Flow Rule

Visual Implementation Flow shows how the slice implements the scenario behavior technically.

In a shortened draft, implementation flow should stay high-level, but each step should mark the layer or folder clearly.

Backend example:

```text
[API Controller]
        ↓
[Application Handler]
        ↓
[Domain]
        ↓
[Persistence]
```

Client sidecar example:

```text
[Page / Route]
        ↓
[Feature UI]
        ↓
[Feature Model / Hook]
        ↓
[Shared API]
        ↓
[Generated Contracts]
```

When useful, include folders or placement hints:

```text
[Page: pages/create-request]
        ↓
[Feature: features/create-connection-request]
        ↓
[Shared API: shared/api]
```

Code, DTO shapes and method names belong in the implementation flow only when they clarify:

```text
- contract boundary;
- disputed behavior;
- non-obvious implementation direction;
- API/client mapping;
- error handling;
- no-write/no-side-effect guarantee;
- testability/checkability.
```

Routine mechanics stay high-level.

## 5. Scenario Behavior Items Rule

Behavior items are not invented inside the slice draft.

The slice must select behavior items from scenario specs, scenario behavior registers, DATA/UI specs, validation/security addenda or other accepted source files.

Correct chain:

```text
Scenario specs / source behavior items
        ↓
slice selects relevant behavior items
        ↓
draft describes how slice covers them
        ↓
behavior coverage links required behavior to draft sections
```

If source behavior item IDs are not found yet, an early shortened draft may use:

```text
Source BI TBD
```

Rules for `Source BI TBD`:

```text
- mark it as temporary;
- describe the source behavior in plain language;
- do not treat it as a final ID;
- replace it with real scenario behavior item IDs before finalizing the slice.
```

Client architecture placement is not a behavior item.

For example, choosing `features/create-request` vs `shared/api` belongs to Visual Implementation Flow or component discovery, not to Scenario Behavior Items.

## 6. Behavior Coverage Is Not Test Coverage

Behavior Coverage answers:

```text
Does the draft implementation description cover the required scenario behavior?
```

Test / Verification Plan answers:

```text
How will implemented code be verified later?
```

Do not mix these tables.

Behavior Coverage format:

| Scenario behavior item | How draft covers it | Draft location | Status |
|---|---|---|---|

Test / Verification Plan format:

| Test / check | Verifies | Layer | Status |
|---|---|---|---|

Behavior Coverage can be complete while Test / Verification Plan is still partial.

A test plan can be detailed while Behavior Coverage is still blocked by unresolved source behavior.

## 7. Open Questions First Rule

In `Questions / Decisions`, list items in this order:

```text
1. open questions;
2. unresolved behavior / contract / design risks;
3. accepted decisions.
```

This keeps review focused on what can still change behavior, API contract, implementation direction or testing responsibility.

Accepted decisions should still be recorded, but they should not hide unresolved questions below them.

## 8. Client Sidecar Shortened Draft Rule

Client sidecars follow the same default shortened draft rule.

A client shortened draft contains:

```text
1. Visual UI / Scenario Flow
2. Visual Client Implementation Flow with layers/folders
3. Questions / Decisions
4. Behavior Coverage
5. Client/component/E2E verification plan
6. Covered Scenario/UI Behavior Items
```

Client Visual Implementation Flow should make placement explicit:

```text
[Page: pages/create-request]
Owns route layout and navigation outcome
        ↓
[Feature UI: features/create-request/ui]
Collects form data and displays errors
        ↓
[Feature Model: features/create-request/model]
Maps form values and runs mutation
        ↓
[Shared API: shared/api]
Sends typed request and parses ProblemDetails
        ↓
[Generated Contracts]
Uses openapi-types.ts and generated constants
```

Client sidecar drafts must still follow generated contract rules:

```text
- use generated OpenAPI types for structure;
- use generated constants for semantic error/field/extension names;
- do not guess routes, DTOs, response shapes or error-code strings;
- record temporary contract gaps explicitly.
```

Do not create `.client.md` in advance.

Create or update it when concrete client work starts.

## 9. Shortened Draft Template

Use this template for early backend, business, helper or cross-cutting slice drafts:

```text
# SLICE-ID — Title — Early Short Draft

Status:
Slice type:
Scope:
Contract direction:
Response direction:
Source behavior items:

## 1. Visual Scenario Flow
## 2. Visual Implementation Flow
## 3. Questions / Decisions
## 4. Behavior Coverage
## 5. Test / Verification Plan
## 6. Covered Scenario Behavior Items
## 7. Next Step
```

Use this template for early client sidecar drafts:

```text
# SLICE-ID.client — Title Client Sidecar — Early Short Draft

Status:
Slice type: client sidecar
Scope:
Source scenario/UI behavior items:
Contract sources:

## 1. Visual UI / Scenario Flow
## 2. Visual Client Implementation Flow
## 3. Questions / Decisions
## 4. Behavior Coverage
## 5. Client / Component / E2E Verification Plan
## 6. Covered Scenario / UI Behavior Items
## 7. Next Step
```

## 10. Example — Early Short Backend Draft

This is an example of a good early shortened draft.

It is intentionally not a full final slice file.

# L1-CONNECTION-REQUEST-CREATE — Early Short Draft

**Status:** early target draft  
**Slice type:** backend command slice  
**Scope:** create connection request for authenticated L1 client account  
**Contract direction:** client submits request data; server derives account/applicant context  
**Response direction:** HTTP success is enough for initial command confirmation  
**Source behavior items:** TBD from scenario behavior register

## 1. Visual Scenario Flow

```text
[Client]
Submits connection request data
        ↓
[System]
Determines applicant context for authenticated account
        ↓
[System]
Creates connection request
        ↓
[System]
Moves request into review
        ↓
[System]
Reports successful request creation
        ↓
[Client]
Shows success message and navigates to My Requests
```

Scenario note:

The client submits the data needed to create a connection request.

The system creates the request for the authenticated account's current applicant context.

The created request enters review.

The client does not need created request data for the initial command flow. HTTP success is enough to show a success message and move the user to My Requests.

## 2. Visual Implementation Flow

```text
[API Controller: L1 requests]
POST /api/l1/requests
Receives request data
        ↓
[API Controller]
Derives current client account from auth context
        ↓
[Application Handler]
Finds applicant context for current account
        ↓
[Application Handler]
Rejects command if required applicant context is missing
        ↓
[Domain]
Creates request address/value objects
        ↓
[Domain]
Creates ConnectionRequest in review state
        ↓
[Persistence]
Stores created request
        ↓
[API Controller]
Returns HTTP success without required response body
```

Implementation note:

Target API input contains request data only.

The client should not submit account identity.

The command response does not need to return `requestId`, `status`, applicant identity or account identity unless a later scenario explicitly needs those values.

## 3. Questions / Decisions

### Q-REQ-001 — Should applicant party be verified before request creation?

**Status:** open.

Current active applicant context and verification status answer different questions.

Current active context decides which applicant identity is used.

Verification status decides whether applicant data is trusted enough for request creation.

### Q-REQ-002 — How do we prevent ambiguous current applicant context?

**Status:** open.

The system should not silently create a request if the account has ambiguous active applicant state.

This can be handled as an application invariant first and strengthened with persistence constraints later if needed.

### Q-REQ-003 — What is the final My Requests destination?

**Status:** open.

Success UX direction is accepted: show success message and navigate to My Requests.

The final route belongs to the read/list requests slice.

### Q-REQ-004 — Should the create command return request data?

**Decision direction:** no for the initial command flow.

HTTP success is enough for the client to show a success message and navigate to My Requests.

### Q-REQ-005 — Should the client provide account identity?

**Decision:** no.

Account identity is derived from authenticated user context.

## 4. Behavior Coverage

| Scenario behavior item | How draft covers it | Draft location | Status |
|---|---|---|---|
| Source BI TBD — Client submits connection request data | Draft says API receives request data for creation. | Scenario Flow / Implementation Flow | covered |
| Source BI TBD — System creates connection request for authenticated account context | Draft says system derives account context and finds applicant context server-side. | Scenario Flow / Implementation Flow | covered, with open applicant-context questions |
| Source BI TBD — Request enters review | Draft says domain creates `ConnectionRequest` in review state. | Scenario Flow / Implementation Flow | covered |
| Source BI TBD — System reports successful request creation | Draft says API returns HTTP success without required response body. | Scenario Flow / Implementation Flow / Decisions | covered |
| Source BI TBD — Client sees success outcome | Draft says client shows success message and navigates to My Requests. | Scenario Flow / Decisions | partially covered; client sidecar needed |
| Source BI TBD — My Requests read context | Draft identifies My Requests as target but delegates final route/read behavior to read/list slice. | Questions / Decisions | open |

## 5. Test / Verification Plan

| Test / check | Verifies | Layer | Status |
|---|---|---|---|
| Successful create request integration test | Request is created for the authenticated account's resolved applicant context. | API + Application + Persistence | planned |
| Missing applicant context integration test | Request creation fails when required applicant context is absent. | Application + API error mapping | planned |
| No write on failed command | Failed creation does not persist request. | Persistence | planned |
| Request starts in review | Created request has `InReview` state. | Domain + Persistence | planned |
| Minimal success response | Client can treat HTTP success as command confirmation. | API contract | planned |
| OpenAPI/type regeneration | Generated contract reflects target request/response shape. | Tooling + client contract | planned |
| E2E create request happy path | UI submit leads to real API success and navigation outcome. | Browser + Client + API + Persistence | planned |
| Component/client tests | Form behavior, validation display, pending state, success message. | Client feature | client sidecar |

Note: verification plan must not become the behavior coverage table. It verifies implementation after behavior coverage is defined.

## 6. Covered Scenario Behavior Items

Temporary working list until source behavior IDs are attached:

### Source BI TBD — Client submits connection request data

The client submits data needed to create a connection request.

### Source BI TBD — System creates connection request for authenticated account context

The system creates the request using server-resolved account/applicant context.

### Source BI TBD — Request enters review

The created request enters review state.

### Source BI TBD — System reports successful request creation

The system reports command success through HTTP success.

### Source BI TBD — Client sees success outcome

The client shows a success message and navigates to My Requests.

## 7. Next Step

Before implementation, attach real scenario behavior item IDs.

Then update backend contract and implementation:

```text
Update request DTO to contain request data only
Update command to use server-derived account/applicant context
Update handler to resolve applicant context server-side
Return HTTP success without required response body
Update integration tests
Regenerate OpenAPI and TypeScript types
```

Then create/update client sidecar with:

```text
HTTP success -> success message -> navigate to My Requests
```

## 11. Business Slice Intake Checklist

```text
1. Read architecture decision notes and ADR candidates.
2. Read draft-driven discovery principles.
3. Read planning/testing/ if tests/E2E/client test responsibilities are involved.
4. Read planning/api/ if API/client contract work is involved.
5. Read target scenario text spec and DATA file.
6. Read relevant behavior items and scenario UI spec if client-visible behavior is involved.
7. Check scenario questions register.
8. Check slice implementation notes and extension register.
9. Check planning/slices/cross-cutting/ if the slice uses cross-cutting support.
10. Check planning/client/ and client architecture docs if client work is involved.
11. If scenario/API/constants/testing/security ambiguity exists, stop and resolve it first.
```

## 12. Client Sidecar Draft Rule

Client sidecar drafts are created only when concrete client work starts.

A `.client.md` sidecar is the draft-driven discovery file for client implementation.

It must discover and track:

```text
- client behavior to implement;
- client questions/assumptions;
- contract used by client;
- route/page/feature/entity/shared mapping;
- generated OpenAPI types used;
- generated constants/error codes used;
- component placement;
- form/validation behavior;
- ProblemDetails/error mapping;
- client/component tests;
- E2E boundaries.
```

## 13. API Contract Section

Parent slice API section should include:

| Endpoint | Method | Request DTO | Response DTO | Statuses | Contract status | OpenAPI exposed? |
|---|---|---|---|---|---|---|

Contract status values:

```text
target L1
legacy/current support
temporary compatibility
internal/not client-facing
```

Also include:

```text
- ProblemDetails response statuses;
- client-facing error codes;
- generated constants used;
- OpenAPI generated types used by client;
- whether route is temporary legacy constants route or OpenAPI structural route.
```

## 14. Client Sidecar API Section

`.client.md` should include:

| Client API function | Endpoint | Generated OpenAPI type(s) used | Error constants used | Status |
|---|---|---|---|---|

## 15. Cross-Cutting / Helper Slice Intake Checklist

```text
1. Identify source requirement type:
   security / API contract / tooling / testing / infrastructure / client-cross-cutting.
2. Check whether a specification/addendum/source note already exists.
3. Create or update concern-derived behavior items.
4. Ensure every behavior item appears in Concern Slice Flow.
5. Only after the concern flow, write Implementation Flow.
6. Add coverage, test/check plan, consumer rule, local questions and ADR impact.
7. Update cross-cutting index and relevant navigation.
```

## 16. Test Coverage Sections

Parent slice and `.client.md` should separate:

```text
Client/component tests
Server integration/API tests
End-to-end tests
```

Use:

```text
planning/testing/testing-principles.md
planning/testing/e2e-testing-workflow.md
```

## 17. E2E Coverage Table

If E2E is relevant, include:

| E2E test | Scenario/UI items | Cross-layer purpose | Setup | User path | Server communication checked | Expected result | Status |
|---|---|---|---|---|---|---|---|

E2E is relevant when the behavior requires proof of:

```text
browser -> client -> HTTP API -> server/application/domain/persistence/session -> visible outcome
```

Do not use E2E to exhaustively test client-visible UI behavior.

## 18. Client Test Coverage Table

If client-visible UI behavior is involved, include:

| Client/component test | Behavior/UI items | Client behavior covered | Locator/accessibility focus | Status |
|---|---|---|---|---|

Detailed UI validation belongs here, not in happy-path E2E.

## 19. Business Slice Flow Rule

Business slices use:

```text
Scenario-derived behavior items
-> Scenario Slice Flow
-> Implementation Flow
```

For early shortened drafts, Scenario Slice Flow may be represented as Visual Scenario Flow.

## 20. Cross-Cutting / Helper Slice Flow Rule

Cross-cutting/helper slices use:

```text
Concern-derived behavior items
-> Concern Slice Flow
-> Implementation Flow
```

Concern-derived behavior item source can be:

```text
security-derived
API-contract-derived
tooling-derived
testing-derived
client-cross-cutting-derived
infrastructure-derived
```

These items are first-class behavior items and must be covered by the concern flow.

For early shortened drafts, Concern Slice Flow may be represented as a visual concern/scenario flow when that is clearer.

## 21. Cross-Cutting / Helper Slice Template

```text
# CC-XXX — Title

Status:
Slice type: cross-cutting slice / helper slice
Layers:
Depends on:
Used by:

## 1. Purpose
## 2. Why This Is A Cross-Cutting/Helper Slice
## 3. Inputs / Sources
## 4. Concern-Derived Behavior Items
## 5. Coverage Overview
## 6. Concern Slice Flow
## 7. Implementation Flow
## 8. Target Types / Components
## 9. Test / Check Plan
## 10. Consumer Rule For Business Slices
## 11. Local Questions
## 12. ADR Impact
```

For early cross-cutting/helper discovery, use the shortened draft format first unless the user explicitly asks for the full template.

## 22. Implementation Flow Detail Rule

Implementation flow may include involved classes, methods and short code snippets.

Include them when they explain:

```text
- contract boundary;
- non-obvious behavior;
- behavior that was discussed/questioned;
- important trade-off;
- extension/change point;
- error handling;
- testability/checkability;
- no-write/no-side-effect guarantee;
- generated artifact shape;
- API/client boundary.
```

Keep routine mechanics high-level.

If details make the flow noisy, extract them into a sibling `.impl.md` file.

Do not create `.impl.md` in advance.

## 23. Constants Consumer Rule

If a business slice introduces client-facing error codes, read:

```text
planning/slices/cross-cutting/CC-CONST-001-client-constants-generation-and-contract-testing.md
```

## 24. OpenAPI Consumer Rule

If a business/client slice uses server API, read:

```text
planning/api/client-server-contract-principles.md
planning/slices/cross-cutting/CC-API-001-openapi-contract-artifacts-and-type-generation.md
```

## 25. CSRF Consumer Rule

If a business slice introduces browser unsafe API command, read:

```text
planning/slices/cross-cutting/CC-CSRF-001-antiforgery-token-session-context.md
```

Parent slice API/security note:

```text
Unsafe browser API requests are protected by CC-CSRF-001.
```
