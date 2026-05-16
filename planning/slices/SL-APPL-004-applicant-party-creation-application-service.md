# SL-APPL-004 — ApplicantParty Creation Application Service

Status: implementation-ready helper slice / not runtime-implemented unless repo evidence later proves otherwise  
Package: `[L1]`  
Source scenario: `SC-10 Applicant Data`, `SC-04 Client Request Creation`  
Slice type: application helper / shared use-case support slice

## 1. Purpose

Extract reusable ApplicantParty creation logic so both standalone applicant creation and request creation with new applicant data can share validation/domain creation without sharing transaction commits.

## 2. Decision

```text
ApplicantParty creation logic should be extracted into a reusable application service.
```

Used by:

```text
- standalone create ApplicantParty handler;
- create request handler when applicantContextType = New.
```

The service should not call `SaveChanges`.

Outer use-case handlers own transaction/commit boundary.

## 3. Reason

The project has two related but different user intents:

```text
1. Standalone ApplicantParty creation:
   user adds an applicant profile to Account page / My Applicant Parties.
   Returning ApplicantPartyId is useful for list/cache/refetch/selection/future actions.

2. Request creation with new applicant data:
   user creates a request and enters applicant data in the same journey.
   This should be atomic: either both ApplicantParty and ConnectionRequest are created, or neither is created.
```

Two client calls are not preferred:

```text
POST /api/l1/applicant-parties/individual succeeds
POST /api/l1/requests fails
        ↓
ApplicantParty was created but request was not created
```

Calling one command handler from another is also not preferred because command handlers own use-case boundaries and may save independently.

## 4. Target Shape

```text
ApplicantPartyCreationService
  validate applicant data
  load/check account or accept checked account context
  create IndividualApplicantParty
  add to repository/context
  return created ApplicantParty result
  does not call SaveChanges
```

Standalone use:

```text
L1CreateIndividualApplicantPartyHandler
  service.Create(...)
  SaveChanges
  return ApplicantPartyId
```

Request use:

```text
L1CreateConnectionRequestHandler
  if Existing:
    load ApplicantParty by id + verify ownership
  if New:
    service.Create(...)
  create ConnectionRequest
  SaveChanges once
```

## 5. Questions / Decisions

| ID | Status | Question | Current direction | Impact |
|---|---|---|---|---|
| SL-APPL-SVC-Q-001 | accepted direction | Does service own SaveChanges? | No. Outer handler owns SaveChanges. | transaction boundary |
| SL-APPL-SVC-Q-002 | accepted direction | Why keep ApplicantPartyId response? | Standalone creation needs stable identity/cache/future actions. | API contract |
| SL-APPL-SVC-Q-003 | accepted direction | Should request flow use two client calls? | No. One request endpoint should create applicant + request atomically when applicantContextType = New. | UX/data consistency |
| SL-APPL-SVC-Q-004 | future review | Does service initialize current/default template? | It may return created entity; default policy may belong to caller/policy component. | service boundary |

## 6. Test / Verification Plan

Do not test exact service invocation. Test observable use-case behavior.

| Test / check | Verifies | Layer | Status |
|---|---|---|---|
| Standalone create still commits ApplicantParty | Direct applicant endpoint persists applicant independently. | API integration | planned |
| Request-create with new applicant commits applicant + request together | Atomic user intent. | API integration | future request redesign |
| Request-create failure does not leave orphan applicant | No partial state if request creation fails. | API integration | future request redesign |
| Shared validation produces same applicant rules | Standalone and request flow validate applicant consistently. | application/integration | planned |
