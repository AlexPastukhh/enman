# SC-04 — Client Request Creation

## Status

Corrected scenario specification draft.

## Purpose

Client creates and submits a connection/request for review.

## Actor / Screen

Actor: Client  
Screen: Request creation page  
Goal: Create request

## Entry Points

Entry A: Client opens request creation page.

Entry B: Client starts a new request after rejected request feedback.

## Preconditions

- Client is signed in.
- Client can access request creation page.

## DATA

Request creation DATA  
`SC-04-DATA-01`

Input DATA:

```text
- requested service / request subject information;
- request details/description;
- object address;
- applicant DATA copied/prefilled from previously provided matching applicant DATA, or entered inline.
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

Applicant prefill / inline behavior:

```text
- if previously provided applicant DATA exists and applicant type matches the request needs, the request form may copy/prefill applicant fields from it;
- if no matching applicant DATA exists, client enters applicant DATA inline in request fields;
- if client wants different applicant DATA for this request, client edits the prefilled request fields;
- future UX may add clear-prefilled-data and restore-prefilled-data buttons, but those buttons are not current core behavior;
- clearing prefilled fields for the request does not delete saved applicant DATA.
```

## Main Flow

1. Client opens request creation page.
2. Client enters request creation data.
3. If matching applicant DATA exists, applicant fields may be prefilled/copied.
4. Client edits applicant/request fields if needed.
5. Client-side validation runs automatically.
6. Client corrects request data if client-side validation fails.
7. Client submits request.
8. System checks whether request is accepted.
9. If accepted, request is created.
10. Request status becomes InReview.
11. Request appears in client's My Requests list.
12. Request appears in employee review queue.

## Branches

### Matching applicant DATA exists

-> saved applicant DATA type matches request needs  
-> applicant fields are prefilled/copied into request form  
-> client can keep or edit fields for this request

### No matching applicant DATA exists

-> no saved matching applicant DATA  
-> client enters applicant DATA inline in request fields

### Client wants different applicant DATA for this request

-> client edits prefilled applicant fields  
-> saved applicant DATA is not deleted  
-> request uses edited fields after accepted submit

### Client-side validation errors

-> request form has basic validation issues  
-> validation errors visible  
-> client corrects request data  
-> back to entering request data

### Request accepted

-> request accepted  
-> request is created  
-> status becomes InReview  
-> request appears in My Requests  
-> request appears in employee review queue

### Request rejected by validation/business rules

-> request not accepted  
-> validation/business errors visible  
-> client corrects request data  
-> back to entering request data

## Invariants

Invalid request is not accepted.

Clearing or editing prefilled applicant fields for this request does not delete saved applicant DATA.

## Outcomes

- Client can create request.
- Client sees validation errors for invalid request data.
- Created request appears in My Requests and employee review queue.
- Initial status is InReview.
- Matching saved applicant DATA can be reused without forcing it.
