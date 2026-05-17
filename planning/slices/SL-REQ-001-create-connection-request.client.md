# SL-REQ-001.client — Create Connection Request UI with Applicant Context

Status: implemented first-stage client command sidecar  
Parent slice: `SL-REQ-001 — Create Connection Request With Applicant Context`  
Slice type: client command sidecar  
Architecture direction: command/user-action slice maps to `pages + features + entities`.

## 1. Sidecar Overview

This sidecar defines the implemented client UI for creating a connection request with explicit applicant context.

Runtime route:

```text
/requests/create
```

Backend command:

```text
POST /api/l1/requests
```

Applicant context branches:

```text
Existing saved ApplicantParty
or
New applicant data entered during the request creation journey
```

## 2. Implemented Scope

Implemented/current:

```text
- request creation route/page for signed-in L1 client;
- session/no-session page branch;
- load account ApplicantParties before rendering form;
- applicant parties loading/error states;
- request details fields;
- object address fields;
- applicant context section;
- Existing applicant context branch;
- saved ApplicantParty selector/list for Existing branch;
- current/default ApplicantParty selected first when available;
- user can choose another owned saved ApplicantParty;
- New applicant context branch;
- new applicant data fields entered during request creation;
- one submit command to POST /api/l1/requests;
- visible validation/error feedback;
- ProblemDetails-to-form errors;
- success invalidates My Requests and navigates to My Requests.
```

## 3. Out of Scope

| Out-of-scope item | Owner / destination |
|---|---|
| Backend endpoint implementation | parent `SL-REQ-001` backend |
| ApplicantParty management page | `SL-APPL-002.client` / AccountPage replacement |
| Explicit make default/current | future `SL-APPL-003.client` |
| ApplicantParty delete/archive/edit lifecycle | future lifecycle slices |
| Direct request details navigation requiring requestId response | future response/UX decision |
| My Requests list/details rendering | existing My Requests sidecars |
| Generated artifacts | API generation workflow, not this sidecar |

## 4. Client Implementation Flow

```text
[Route]
clientRoutes.createRequest = /requests/create
        ↓
[Page]
CreateConnectionRequestPage
  uses session
  loads account ApplicantParties
  handles no-session/loading/error
  passes applicantParties to form
        ↓
[Feature UI]
CreateConnectionRequestForm
  ApplicantContextSection
  RequestDetailsFields
  ObjectAddressFields
        ↓
[Feature Model]
useCreateConnectionRequestForm
  initial selected applicant = current/default or first saved
  manages Existing/New branch
  validates form
  builds DTO
  maps ProblemDetails to form errors
        ↓
[Shared API]
createConnectionRequest(request)
  POST /api/l1/requests
        ↓
[Success]
invalidate My Requests query
navigate to /requests
```

## 5. Scenario Flow

```text
Signed-in client opens request creation page
        ↓
Page loads saved ApplicantParties
        ↓
Client chooses Existing or New applicant context
        ↓
Existing:
  current/default selected first when available;
  user may choose another saved ApplicantParty

New:
  user enters applicant data in the request journey
        ↓
Client enters request details and object address
        ↓
Client submits one request creation command
        ↓
Accepted:
  request is created InReview and user is handed off to My Requests

Rejected:
  validation/error feedback is visible and user can correct input
```

## 6. Behavior Coverage

| Source behavior item | Current coverage |
|---|---|
| Signed-in client can create request | `/requests/create` route + form + POST command |
| Client provides details/address | request details and object address fields |
| Request creation uses one applicant context | Existing/New branch state and DTO mapping |
| Existing uses owned saved ApplicantParty | selector uses account ApplicantParties read |
| Existing can use non-default saved ApplicantParty | selector is not limited to default/current |
| New applicant data entered in journey | New branch applicant fields |
| New applicant + request are one server operation | client sends one POST DTO; backend owns atomicity |
| Created request enters InReview | backend behavior; client hands off to My Requests |
| Invalid data gives feedback | client validation + ProblemDetails mapping |

## 7. Client / Component / E2E Verification Plan

Expected client verification:

```text
- route /requests/create renders page;
- unauthenticated user sees sign-in required state;
- applicant parties loading/error states render;
- Existing branch selects current/default first when present;
- Existing branch allows selecting another saved ApplicantParty;
- New branch shows applicant fields and builds newApplicantParty DTO;
- required details/address errors show before submit;
- server ProblemDetails map to form/root errors;
- successful submit calls POST /api/l1/requests;
- success navigates to My Requests;
- My Requests query is invalidated as implementation behavior.
```

E2E should assert visible outcomes, not implementation internals.

## 8. Remaining Work

This first-stage client sidecar is implemented.

Possible future refinements:

```text
- direct details navigation if command later returns requestId;
- richer ApplicantParty selector search/filter/sort;
- integration with future ApplicantParty lifecycle/default action UI;
- broader E2E coverage after UI stabilizes.
```
