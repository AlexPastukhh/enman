# L1-CONNECTION-REQUEST-CREATE — Early Short Draft Example

Status: example / valid shortened slice draft  
Example type: shortened backend command slice draft  
Source: adapted from working draft format provided for slice drafting workflow  
Not authoritative implementation evidence: check current repo and active slice file before use

## Header Example

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

Client success handling follows:

```text
planning/client/cross-cutting/CL-COMMAND-001-command-success-without-required-response-body.md
```

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

Note: verification plan should not become the behavior coverage table. It verifies implementation after behavior coverage is defined.

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
