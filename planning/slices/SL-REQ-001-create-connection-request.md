# SL-REQ-001 - Create Connection Request From Current Applicant Party

Status: implemented backend/API/persistence slice  
Package: `[L1]`  
Source scenario: `SC-04 Client Request Creation`  
Slice type: backend / API / persistence slice with dependent Client/UI/read/extension slices

## 1. Visual Scenario Flow

```text
[Client]
Submits connection request data
        ↓
[System]
Uses current active applicant party of authenticated account
        ↓
[System]
Creates connection request for that applicant party
        ↓
[System]
Moves request into InReview state
        ↓
[System]
Reports successful request creation
        ↓
[Client]
Shows success message and navigates to My Requests
```

Backend scope implemented by this slice:

```text
submit request data
-> server selects current active applicant party
-> create request
-> request enters InReview
-> API returns success
```

Client UI, My Requests read view, documents, notifications and review workflows are separate slices.

## 2. Visual Implementation Flow

```text
[API Controller]
POST /api/l1/requests
Receives: details + address
        ↓
[API Controller]
Derives current client account id from auth context
        ↓
[Application Handler]
Finds current active individual applicant party for account
        ↓
[Application Handler]
Rejects request if current active applicant party is missing
        ↓
[Domain]
Creates Address value object
        ↓
[Domain]
Creates ConnectionRequest for selected applicant party
        ↓
[Persistence]
Stores request with server-selected ApplicantPartyId
        ↓
[API Controller]
Returns success without required body
```

Current API contract:

| Endpoint | Method | Request DTO | Response DTO | Statuses | Contract status | OpenAPI exposed? |
|---|---|---|---|---|---|---|
| `/api/l1/requests` | POST | `L1CreateConnectionRequestDto` with `details` and `address` | none required | 200, 401, 403, 422, 500 | target L1 | yes |

Contract rule:

```text
The request body must not include applicantPartyId or clientAccountId.
ClientAccountId comes from auth context.
ApplicantPartyId is selected by the server from the current active individual applicant party.
```

## 3. Questions / Decisions

```text
Decision:
Create request command treats HTTP success as confirmation and does not require response body for the initial command flow.

Reason:
The client does not need created entity data to continue this command flow.

Consequence:
Client UI can show a success message and navigate to My Requests.
The request read/list contract belongs to a separate My Requests/read slice.
```

```text
Decision:
Request creation uses the current active individual applicant party for the authenticated account.

Reason:
The client must not choose or spoof ApplicantPartyId in the command body.

Consequence:
Missing current active applicant party returns validation ProblemDetails and no request row is created.
```

```text
Decision:
A created request starts in InReview.

Reason:
A created request immediately enters the employee review queue.
```

Open questions:

| ID | Area | Question | Current direction | Status |
|---|---|---|---|---|
| SL-REQ-Q-001 | Read model | What exact My Requests list/detail response does the client need after navigation? | Separate read slice | open |
| SL-REQ-Q-002 | Applicant party versions | How are older applicant party versions made inactive when replacement/edit flow is implemented? | Keep current active lookup now; version management later | future |
| SL-REQ-Q-003 | UI | What concrete page/form implements request creation? | Dependent client sidecar when client work starts | open |
| SL-REQ-Q-004 | Security | When should unsafe browser commands enforce CSRF? | CC-CSRF-001 before broad client unsafe requests | open |

## 4. Coverage

Server integration coverage:

```text
- create request stores server-selected current active applicant party id;
- created request starts InReview;
- details and address are persisted;
- missing current active applicant party returns validation ProblemDetails;
- empty details returns validation ProblemDetails;
- request DTO does not expose applicantPartyId.
```

Domain unit coverage:

```text
- ConnectionRequest.Create creates InReview request;
- create fails without details;
- create fails without object address;
- transient applicant party is guarded.
```

Generated contract coverage:

```text
Shared/openapi.json and generated openapi-types.ts must show:
- POST /api/l1/requests request body has details + address;
- POST /api/l1/requests request body does not have applicantPartyId;
- command success has no required response body.
```

Client/UI coverage is deferred until the request creation UI sidecar starts.
