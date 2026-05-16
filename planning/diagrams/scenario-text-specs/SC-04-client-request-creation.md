# SC-04 — Client Request Creation

Status: corrected scenario specification draft / synchronized with applicant-data-in-request-journey UI direction  
Source family: scenario text + DATA + UI scenario + behavior items  
Related scenarios: `SC-10 Applicant Data`, `SC-05 My Requests / Own Request Details`

## 1. Purpose

Client creates and submits a connection/request for review.

Request creation uses the account-level current active ApplicantParty as the applicant context.

The request creation journey also contains an applicant data section:

```text
- if current applicant data already exists, applicant fields are prefilled;
- the client can clear the prefilled applicant fields and enter new applicant data before request submission;
- accepted new applicant data becomes the account-level current active ApplicantParty through SC-10 / applicant replacement behavior;
- request submission must not let the client spoof ApplicantPartyId.
```

This keeps the user journey simple while preserving the domain/API boundary:

```text
UI journey may collect or refresh applicant data before submit.
Request creation command uses the current active ApplicantParty selected by the server.
```

## 2. Actor / Screen

Actor: Client  
Screen: Request creation page  
Goal: Create request

## 3. Entry Points

Entry A: Client opens request creation page.

Entry B: Client starts a new request after rejected request feedback.

Entry C: Client returns from Applicant Data flow after creating or updating current active applicant data.

Entry D: Client opens request creation page with existing applicant data that can be reused, cleared, or replaced before submission.

## 4. Preconditions

- Client is signed in.
- Client can access request creation page.
- Current active applicant data may already exist.
- If applicant data is missing or the client clears/replaces applicant data, the request journey must collect acceptable applicant data before request submission can complete.

## 5. DATA

Request creation DATA: `SC-04-DATA-01`.

Input DATA:

```text
- requested service / request subject information;
- request details/description;
- object address.
```

Applicant DATA in the request journey:

```text
- applicant type, for the current supported applicant flow;
- applicant display/full name;
- applicant contact email;
- applicant contact phone;
- future applicant type-specific fields when those applicant types enter the scenario.
```

Applicant data behavior:

```text
- if current active applicant data exists, the applicant fields are prefilled;
- the client can clear the applicant fields to enter new applicant data;
- accepted new applicant data replaces or updates the account-level current active ApplicantParty through SC-10 / applicant replacement flow;
- request creation itself does not create a request-local applicant identity.
```

Visible DATA after accepted submit:

```text
- request status = InReview;
- request appears in My Requests;
- request appears in employee review queue.
```

Extension / Future DATA:

```text
[VAR:EXPAND]
- requested maximum power, kW;
- data prefilled from rejected request feedback;
- document references if document flow becomes part of request creation;
- richer applicant data fields for entrepreneur/legal-entity applicant types.
```

## 6. Main Flow

1. Client opens request creation page.
2. System loads the current active ApplicantParty state for the account, when available.
3. System shows request creation fields.
4. System shows applicant data fields/section.
5. If current active applicant data exists, applicant fields are prefilled from it.
6. Client may keep the prefilled applicant data or clear the applicant fields and enter new applicant data.
7. Client enters request creation data: request details and object address.
8. Client-side validation runs for visible request/applicant fields.
9. Client corrects visible data if validation fails.
10. Before request submission is accepted, applicant data must be available as account-level current active ApplicantParty.
11. Client submits request.
12. System checks whether request is accepted.
13. If accepted, request is created for the current active ApplicantParty.
14. Request status becomes InReview.
15. Request appears in client's My Requests list.
16. Request appears in employee review queue.

## 7. Branches

### Current active ApplicantParty exists

```text
-> applicant fields are prefilled from current active applicant data
-> client can keep the data
-> request creation can proceed with request details/object address
-> accepted submit creates request for that applicant context
```

### Client clears prefilled applicant data

```text
-> applicant fields become editable/empty
-> client enters new applicant data
-> new applicant data must be accepted through SC-10 / applicant replacement behavior
-> accepted applicant data becomes current active for the account
-> request creation uses the new current active applicant context
```

### No current active ApplicantParty exists

```text
-> applicant context is missing
-> request journey guides client to provide applicant data before submit
-> after accepted applicant data, request creation can continue
```

### Client wants different applicant data before submit

```text
-> client clears or updates applicant data fields in the request journey
-> accepted applicant data becomes current active for the account
-> request creation uses the new current active applicant context
```

### Client-side validation errors

```text
-> request/applicant form has basic validation issues
-> validation errors are visible
-> client corrects visible data
-> flow returns to entering request/applicant data
```

### Request accepted

```text
-> request is accepted
-> request is created for current active ApplicantParty
-> status becomes InReview
-> request appears in My Requests
-> request appears in employee review queue
```

### Request rejected by validation/business rules

```text
-> request is not accepted
-> validation/business errors are visible
-> client corrects request data or applicant data as needed
-> flow returns to entering request/applicant data
```

## 8. Invariants

Invalid request is not accepted.

Request creation uses the account's current active ApplicantParty at submit time.

The client does not select or spoof ApplicantPartyId during request creation.

Applicant data fields in the request journey are not a separate request-local applicant identity.

Accepted applicant data changes update/replace the account-level current active ApplicantParty through SC-10 / applicant replacement behavior.

If applicant data changes after a request is created, historical request behavior depends on the stable applicant reference/snapshot policy chosen in future domain planning.

## 9. Outcomes

- Client can create request when acceptable applicant data and request data are available.
- Existing applicant data can be reused through prefilled applicant fields.
- Client can clear prefilled applicant fields and enter new applicant data before request submission.
- Created request appears in My Requests and employee review queue.
- Initial status is InReview.
- Current active applicant data is reused as the request applicant context.

## 10. Questions / Decisions

Open questions:

```text
Q: Is requested service type a fixed list or free description?
Q: When should RequestedPowerKw become core scenario DATA?
Q: Should historical requests store applicant snapshot in addition to applicant reference?
Q: Does inline applicant data editing in request creation use the same SC-10 endpoint or a dedicated replacement endpoint?
```

Accepted direction:

```text
Decision:
Request creation uses one account-level current active ApplicantParty.

Reason:
Applicant identity is account-level scenario data, not per-request ad hoc applicant data.

Consequence:
If applicant data is missing or wrong, the user provides/replaces applicant data before request submit.
```

```text
Decision:
Request creation may show applicant data fields as part of the request journey.

Reason:
The user needs to see and correct applicant data before submission.

Consequence:
Prefilled applicant fields may be cleared and replaced, but accepted applicant changes update the account-level applicant profile before the request is created.
```

```text
Decision:
Request creation does not mutate saved ApplicantParty data as a hidden side effect of request submit.

Reason:
Applicant data editing/replacement must be explicit and traceable to SC-10 / applicant replacement behavior.
```

## 11. Source Links

```text
planning/diagrams/scenario-data/SC-04-request-creation-data.md
planning/diagrams/scenario-ui-specs/SC-04-request-creation-ui.md
planning/diagrams/scenario-behavior-items/SC-04-request-creation-behavior-items.md
planning/diagrams/scenario-text-specs/SC-10-applicant-data.md
```
