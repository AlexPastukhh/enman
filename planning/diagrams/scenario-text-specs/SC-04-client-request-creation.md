# SC-04 — Client Request Creation

Status: corrected scenario specification draft / synchronized with per-type ApplicantParty template direction  
Source family: scenario text + DATA + UI scenario + behavior items  
Related scenarios: `SC-10 Applicant Data`, `SC-10B My Applicant Parties`, `SC-05 My Requests / Own Request Details`

## 1. Purpose

Client creates and submits a connection/request for review.

Request creation uses one ApplicantParty context for the request.

Target scenario direction for applicant data in request creation:

```text
- if a current/default ApplicantParty template exists for the relevant applicant type, applicant fields are prefilled from it;
- client may keep the prefilled data and use that existing ApplicantParty for the request;
- client may clear the prefilled fields and enter new applicant data;
- if no current/default ApplicantParty exists, applicant fields are empty and the same new-applicant-data path is used without needing a clear action;
- accepted new applicant data creates a new ApplicantParty, adds it to saved applicant profiles and uses it for this request;
- after creating the new ApplicantParty, UI should offer to make it current/default template for that applicant type;
- existing ApplicantParties remain stored and unchanged;
- request submission must not let the client spoof an arbitrary ApplicantParty outside the account.
```

Current implementation note:

```text
Current implemented L1 backend still follows the narrower current-active individual ApplicantParty direction.
This scenario describes target/future request creation UX and should not be overclaimed as implemented.
```

## 2. Actor / Screen

Actor: Client  
Screen: Request creation page  
Goal: Create request

## 3. Entry Points

Entry A: Client opens request creation page.

Entry B: Client starts a new request after rejected request feedback.

Entry C: Client opens request creation with existing current/default applicant template available.

Entry D: Client opens request creation when no current/default applicant template exists.

Entry E [future]: Client selects a saved ApplicantParty from all ApplicantParties using a dropdown/list.

## 4. Preconditions

- Client is signed in.
- Client can access request creation page.
- Current/default ApplicantParty for the selected/relevant applicant type may or may not exist.

## 5. DATA

Request creation DATA: `SC-04-DATA-01`.

Input DATA:

```text
- requested service / request subject information;
- request details/description;
- object address.
```

Applicant DATA in the request journey: `SC-04-DATA-02`.

```text
- applicant type;
- prefilled applicant data from current/default ApplicantParty, when available;
- empty applicant fields when no current/default template is available;
- clear action when fields are prefilled;
- new applicant data entered by client;
- option/offer to make newly created ApplicantParty current/default template for its type;
- future selection from all saved ApplicantParties.
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
- richer applicant data fields for entrepreneur/legal-entity applicant types;
- dropdown/list of all saved ApplicantParties.
```

## 6. Main Flow

1. Client opens request creation page.
2. Client selects or is given a relevant applicant type for request applicant data.
3. System checks whether current/default ApplicantParty exists for that applicant type.
4. System shows request creation fields.
5. System shows applicant data fields/section.
6. If current/default ApplicantParty exists, applicant fields are prefilled.
7. If current/default ApplicantParty is missing, applicant fields are empty and ready for input.
8. Client either keeps prefilled applicant data or enters new applicant data.
9. Client enters request creation data: request details and object address.
10. Client-side validation runs for visible request/applicant fields.
11. Client corrects visible data if validation fails.
12. If existing saved ApplicantParty is kept, request uses that ApplicantParty.
13. If new applicant data is entered, system creates a new ApplicantParty and uses it for this request.
14. After new ApplicantParty creation, UI offers to make it current/default template for that applicant type.
15. Client submits request.
16. System checks whether request is accepted.
17. If accepted, request is created for the selected/new ApplicantParty context.
18. Request status becomes InReview.
19. Request appears in client's My Requests list.
20. Request appears in employee review queue.

## 7. Branches

### Current/default ApplicantParty for type exists

```text
-> applicant fields are prefilled from current/default applicant template
-> client can keep the data
-> request creation proceeds with request details/object address
-> accepted submit creates request for that existing ApplicantParty context
```

### Client clears prefilled applicant data

```text
-> applicant fields become editable/empty
-> client enters new applicant data
-> accepted applicant data creates a new ApplicantParty
-> new ApplicantParty is used for this request
-> UI offers to make new ApplicantParty current/default template for this applicant type
-> previous ApplicantParties remain stored and unchanged
```

### No current/default ApplicantParty exists for type

```text
-> applicant fields are empty by default
-> no clear action is needed
-> client enters new applicant data
-> accepted applicant data creates a new ApplicantParty
-> new ApplicantParty is used for this request
-> UI offers to make new ApplicantParty current/default template for this applicant type
```

### Future dropdown/list of all saved ApplicantParties

```text
-> request creation may show all saved ApplicantParties that can be used for the selected applicant type/context
-> current/default ApplicantParty remains the initial prefill/default selection
-> client can choose another saved ApplicantParty deliberately
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
-> request is created for selected/new ApplicantParty context
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

Request creation uses one ApplicantParty context selected/created for that request.

The client does not spoof ApplicantPartyId outside of allowed account-owned ApplicantParties.

If new applicant data is entered during request creation, accepted data creates a new ApplicantParty and uses it for the request.

Creating a new ApplicantParty does not delete, overwrite or deactivate existing ApplicantParties.

Current/default template per applicant type affects future prefill behavior only.

Existing requests keep their submitted applicant context.

ApplicantParty verification happens during employee request review.

## 9. Outcomes

- Client can create request when acceptable applicant data and request data are available.
- Existing current/default applicant data can be reused through prefilled applicant fields.
- Client can clear prefilled applicant fields and enter new applicant data before request submission.
- If there is no current/default applicant template, client enters new applicant data directly.
- New applicant data creates a new ApplicantParty and uses it for the request.
- Newly created ApplicantParty is offered as current/default template for its applicant type.
- Created request appears in My Requests and employee review queue.
- Initial status is InReview.

## 10. Questions / Decisions

Open / future-review questions:

```text
Q: Is requested service type a fixed list or free description?
Q: When should RequestedPowerKw become core scenario DATA?
Q: Should historical requests store applicant snapshot in addition to ApplicantParty reference/version?
Q: Should new ApplicantParty become current/default automatically when no template exists for the type, or should the UI still ask?
Q: What exact dropdown/list behavior should future request creation use for selecting from all saved ApplicantParties?
Q: Can an existing saved ApplicantParty be edited in place if it is already used by requests?
```

Accepted direction:

```text
Decision:
Request creation uses one account-owned ApplicantParty context.

Reason:
Applicant identity is a saved account-owned profile, not ad hoc hidden request-local data.

Consequence:
If new applicant data is entered, it creates a new ApplicantParty and uses it for the request.
```

```text
Decision:
Current/default ApplicantParty per type is used as prefill/default selection, not as global single active applicant.

Reason:
Users may have different applicant profiles over time, and historical requests must keep their submitted applicant context.
```

```text
Decision:
New applicant data entered during request creation creates a new ApplicantParty and does not replace old profiles.

Reason:
Existing ApplicantParties and old requests remain meaningful.
```

```text
Decision:
After creating a new ApplicantParty from request flow, UI should offer to make it the current/default template for that applicant type.

Reason:
The current/default template remains useful for future prefill, but changing it should be visible to the user.
```

Superseded direction:

```text
Superseded:
Accepted applicant data replaces the account-level current active ApplicantParty and makes the previous ApplicantParty non-current globally.
```

## 11. Source Links

```text
planning/diagrams/scenario-data/SC-04-request-creation-data.md
planning/diagrams/scenario-ui-specs/SC-04-request-creation-ui.md
planning/diagrams/scenario-behavior-items/SC-04-request-creation-behavior-items.md
planning/diagrams/scenario-text-specs/SC-10-applicant-data.md
planning/diagrams/scenario-text-specs/SC-10B-my-applicant-parties.md
```
