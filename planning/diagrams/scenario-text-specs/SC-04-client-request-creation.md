# SC-04 — Client Request Creation

## Status

Corrected scenario specification draft / synchronized with current-active ApplicantParty policy.

## Purpose

Client creates and submits a connection/request for review.

Request creation uses the client's current active ApplicantParty as the applicant context.

## Actor / Screen

Actor: Client  
Screen: Request creation page  
Goal: Create request

## Entry Points

Entry A: Client opens request creation page.

Entry B: Client starts a new request after rejected request feedback.

Entry C: Client returns from Applicant Data flow after creating or updating current active applicant data.

## Preconditions

- Client is signed in.
- Client can access request creation page.
- Current active applicant data exists before request submission, or the user is guided to SC-10 Applicant Data before submit.

## DATA

Request creation DATA  
`SC-04-DATA-01`

Input DATA:

```text
- requested service / request subject information;
- request details/description;
- object address.
```

Reference / Visible DATA:

```text
- current active ApplicantParty summary for the account;
- applicant type;
- applicant display name;
- applicant contact summary.
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
- document references if document flow becomes part of request creation.
```

Applicant context behavior:

```text
- request creation references the account's current active ApplicantParty;
- if current active ApplicantParty is missing, client provides applicant data through SC-10 before request submission;
- if client wants different applicant data, client updates/replaces applicant data through SC-10 before request submission;
- request creation does not create a separate request-local applicant identity;
- future UX may embed applicant-data editing in the request journey, but accepted applicant changes still update the account-level current active ApplicantParty.
```

## Main Flow

1. Client opens request creation page.
2. System shows or references the current active ApplicantParty summary for the account.
3. Client enters request creation data: request details and object address.
4. Client-side validation runs automatically.
5. Client corrects request data if client-side validation fails.
6. Client submits request.
7. System checks whether request is accepted.
8. If accepted, request is created for the current active ApplicantParty.
9. Request status becomes InReview.
10. Request appears in client's My Requests list.
11. Request appears in employee review queue.

## Branches

### Current active ApplicantParty exists

-> current active applicant summary is available  
-> request creation can proceed with request details/object address  
-> accepted submit creates request for that applicant context

### No current active ApplicantParty exists

-> applicant context is missing  
-> client must provide applicant data through SC-10 Applicant Data before submit  
-> after accepted applicant data, request creation can continue

### Client wants different applicant data before submit

-> client updates/replaces applicant data through SC-10 Applicant Data  
-> accepted applicant data becomes current active for the account  
-> request creation uses the new current active applicant context

### Client-side validation errors

-> request form has basic validation issues  
-> validation errors visible  
-> client corrects request data  
-> back to entering request data

### Request accepted

-> request accepted  
-> request is created for current active ApplicantParty  
-> status becomes InReview  
-> request appears in My Requests  
-> request appears in employee review queue

### Request rejected by validation/business rules

-> request not accepted  
-> validation/business errors visible  
-> client corrects request data or applicant context  
-> back to entering request data / applicant data as needed

## Invariants

Invalid request is not accepted.

Request creation uses the account's current active ApplicantParty.

The client does not select or spoof ApplicantPartyId during request creation.

Editing/replacing applicant data is an applicant-data concern, not a request-local applicant override.

If applicant data changes after a request is created, historical request behavior depends on the stable applicant reference/snapshot policy chosen in future domain planning.

## Outcomes

- Client can create request when current active applicant data exists.
- Client sees validation errors for invalid request data or missing applicant context.
- Created request appears in My Requests and employee review queue.
- Initial status is InReview.
- Current active applicant data is reused as the request applicant context.

## Questions / Decisions

Accepted direction:

```text
Decision:
Request creation uses one account-level current active ApplicantParty.

Reason:
Applicant identity is account-level scenario data, not per-request ad hoc applicant data.

Consequence:
If applicant data is missing or wrong, the user goes through SC-10 Applicant Data / future replacement flow before submit.
```

```text
Decision:
Request creation does not mutate saved ApplicantParty data.

Reason:
The request creation flow references the current active applicant context and stores request-local request details/object address.
Applicant data editing/replacement belongs to SC-10.
```

Open questions:

```text
Q: Is requested service type a fixed list or free description?
Q: When should RequestedPowerKw become core scenario DATA?
Q: Should historical requests store applicant snapshot in addition to applicant reference?
```
