# SC-04 — Client Request Creation

Status: target scenario specification draft / synchronized with ApplicantParty template-per-type model  
Source family: scenario text + DATA + UI scenario + behavior items  
Related scenarios: `SC-10 Applicant Data`, `SC-10B My Applicant Parties`, `SC-05 My Requests / Own Request Details`

## 1. Purpose

Client creates and submits a connection request for review.

Request creation uses one applicant context for the request.

Target applicant-context model:

```text
- the account may have saved ApplicantParties;
- one current/default ApplicantParty may exist per applicant type;
- current/default is the initial prefill/default selection for request creation;
- client may keep the prefilled applicant data;
- client may clear fields and enter new applicant data;
- if new applicant data is entered, the system creates a new ApplicantParty and uses it for this request;
- creating new ApplicantParty does not overwrite existing ApplicantParties;
- if no current/default exists for the type, the newly created ApplicantParty becomes initial current/default;
- if current/default already exists, UI may offer to make the new ApplicantParty current/default for future requests.
```

Current implementation note:

```text
Current implemented L1 request creation is narrower: it creates a request from server-selected current active individual ApplicantParty.
Target scenario direction requires future request applicant-context redesign.
Do not overclaim the target model as already implemented.
```

## 2. Actor / Screen

Actor: Client  
Screen: Request creation page  
Goal: Create request

## 3. Entry Points

Entry A: Client opens request creation page.

Entry B: Client starts a new request after rejected request feedback.

Entry C: Client opens request creation page with a current/default ApplicantParty template available for the selected applicant type.

Entry D: Client opens request creation page when no current/default ApplicantParty exists for the selected applicant type.

Entry E [future]: Client chooses an ApplicantParty from a dropdown/list of all saved ApplicantParties.

## 4. Preconditions

- Client is signed in.
- Client can access request creation page.
- Current/default ApplicantParty may exist for the selected applicant type, but is not required.
- Request creation must have an accepted applicant context before submit can complete.

## 5. DATA

Request creation DATA: `SC-04-DATA-01`.

Applicant DATA in request creation journey: `SC-04-DATA-02`.

Related applicant DATA:

```text
planning/diagrams/scenario-data/SC-10-applicant-data.md
planning/diagrams/scenario-data/SC-10B-my-applicant-parties-data.md
```

## 6. Main Flow

1. Client opens request creation page.
2. Client selects or arrives with an applicant type context.
3. System checks whether a current/default ApplicantParty exists for that applicant type.
4. System shows request creation fields.
5. System shows applicant data fields/section.
6. If current/default ApplicantParty exists, applicant fields are prefilled from it.
7. If current/default ApplicantParty is missing, applicant fields are empty and ready for input.
8. Client either keeps prefilled applicant data or enters new applicant data.
9. Client enters request creation data: request details and object address.
10. Client-side validation runs for visible request/applicant fields.
11. Client corrects visible data if validation fails.
12. If existing ApplicantParty is kept, request uses that ApplicantParty.
13. If new applicant data is entered and accepted, system creates a new ApplicantParty and uses it for this request.
14. If no current/default existed for the type, the newly created ApplicantParty becomes the initial current/default template for that type.
15. If current/default already existed, UI may offer to make the new ApplicantParty current/default for future requests.
16. Client submits request.
17. System accepts or rejects request creation.
18. If accepted, request status becomes InReview.
19. Request appears in My Requests.
20. Request appears in employee review queue.

## 7. Visual Scenario Flow

```text
┌──────────────────────────────────────────────┐
│ Signed-in Client                             │
│ opens request creation page                  │
└──────────────┬───────────────────────────────┘
               ▼
┌──────────────────────────────────────────────┐
│ System checks current/default ApplicantParty │
│ for selected applicant type                  │
└──────────────┬───────────────────────────────┘
               │
       ┌───────┴────────┐
       │                │
 default exists     default missing
       │                │
       ▼                ▼
┌──────────────────────┐   ┌──────────────────────────────┐
│ Applicant fields      │   │ Applicant fields are empty    │
│ are prefilled         │   │ and ready for input           │
└──────────┬───────────┘   └──────────────┬───────────────┘
           │                              │
           ▼                              ▼
┌──────────────────────┐        ┌──────────────────────────┐
│ Client keeps default  │        │ Client enters new        │
│ or clears fields      │        │ applicant data           │
└──────────┬───────────┘        └──────────────┬───────────┘
           │                                   │
    ┌──────┴──────┐                            │
    │             │                            │
 keep existing   clear + new data              │
    │             │                            │
    ▼             ▼                            ▼
┌──────────────┐  ┌────────────────────────────────────────┐
│ Request uses │  │ System creates new ApplicantParty      │
│ existing     │  │ and uses it for this request           │
│ ApplicantParty│ │ New party starts NotVerified           │
└──────┬───────┘  └───────────────────┬────────────────────┘
       │                              │
       ▼                              ▼
┌──────────────────────────────────────────────┐
│ Request submit continues with request data   │
│ and accepted applicant context               │
└──────────────────────────────────────────────┘
```

## 8. Branches

### Current/default ApplicantParty exists and is kept

```text
-> applicant fields are prefilled from current/default template
-> client keeps the data
-> request uses that saved ApplicantParty
-> existing ApplicantParties remain unchanged
```

### Current/default ApplicantParty exists but client clears fields

```text
-> client clears prefilled applicant fields
-> client enters new applicant data
-> accepted applicant data creates a new ApplicantParty
-> new ApplicantParty is used for this request
-> existing current/default remains unchanged unless explicit future action changes it
-> UI may offer to make new ApplicantParty current/default for future requests
```

### Current/default ApplicantParty missing

```text
-> applicant fields are empty by default
-> client enters new applicant data
-> accepted applicant data creates a new ApplicantParty
-> new ApplicantParty is used for this request
-> new ApplicantParty becomes initial current/default template for that type
```

### Future saved ApplicantParty dropdown/list

```text
-> UI may let client choose from all saved ApplicantParties
-> current/default template remains the initial prefill/default selection
-> selected existing ApplicantParty is used for the request
```

### Request accepted

```text
-> request is created for the accepted applicant context
-> status becomes InReview
-> request appears in My Requests
-> request appears in employee review queue
```

### Request rejected by validation/business rules

```text
-> request is not accepted
-> validation/business errors are visible
-> client corrects request data or applicant data as needed
```

## 9. Invariants

Invalid request is not accepted.

Request creation uses one accepted applicant context.

Request creation must not let the client spoof ApplicantParty ownership.

Creating a new ApplicantParty inside request creation is atomic with request creation in the target implementation direction.

Do not use two separate client calls for the single user intent “create request with new applicant data”.

Existing ApplicantParties remain stored and unchanged when a new ApplicantParty is created.

Current/default changes affect future prefill only, not existing requests.

## 10. Outcomes

- Client can create request when acceptable request data and applicant context are available.
- Existing current/default ApplicantParty can be reused through prefilled fields.
- Missing current/default and cleared fields both lead to the same new-applicant-data path.
- New applicant data creates a new ApplicantParty and uses it for this request.
- Created request appears in My Requests and employee review queue.
- Initial request status is InReview.

## 11. Questions / Decisions

Accepted direction:

```text
Decision:
Request creation with new applicant data should be one server call.

Reason:
Two client calls can leave a partial state: ApplicantParty created but request creation failed.

Consequence:
Request creation target contract should support explicit applicant context: existing ApplicantParty or new ApplicantParty data.
```

```text
Decision:
Shared ApplicantParty creation logic should live in an application service.

Reason:
Standalone create ApplicantParty and request creation with new applicant data share validation/domain creation logic, but own different use-case transaction boundaries.

Consequence:
The service does not call SaveChanges by itself. Outer command handler owns commit/transaction.
```

```text
Decision:
Creating new ApplicantParty in request creation uses the new ApplicantParty for that request.

Reason:
The new data is part of the user's request creation intent.
```

```text
Decision:
If an existing default for the type already exists, creating new ApplicantParty does not silently change it.

Reason:
Changing future prefill behavior should be visible/explicit.
```

Future review:

```text
Q: Should request store ApplicantParty reference, applicant snapshot, or both?
Q: What exact applicantContextType shape should the API use?
Q: How should UI ask whether to make a newly created ApplicantParty current/default?
Q: When should saved ApplicantParty dropdown/list be introduced?
```

## 12. Source Links

```text
planning/diagrams/scenario-data/SC-04-request-creation-data.md
planning/diagrams/scenario-ui-specs/SC-04-request-creation-ui.md
planning/diagrams/scenario-behavior-items/SC-04-request-creation-behavior-items.md
planning/diagrams/scenario-text-specs/SC-10-applicant-data.md
planning/diagrams/scenario-text-specs/SC-10B-my-applicant-parties.md
```
