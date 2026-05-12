# Scenario Specification Principles

Status: source of truth for general scenario specification principles  
Scope: textual scenario specifications, scenario diagrams and scenario DATA blocks

## Current Project-Wide Scenario Decisions

### Request statuses

Use:

```text
InReview
Approved
Rejected
```

Do not use `Submitted`.

### Request object location

Request object location means:

```text
object address
```

### Request creation applicant DATA reuse

When request creation needs applicant DATA:

```text
- if previously provided applicant DATA exists and its applicant type matches the request needs, the request form may be prefilled/copied from it;
- if no matching applicant DATA exists, client enters applicant DATA inline in request fields;
- if client wants different applicant DATA for this request, client edits the prefilled fields;
- future UX may add clear/restore-prefill controls, but those controls are not current core behavior;
- clearing request-local applicant fields does not delete saved applicant DATA.
```

### Approved / Rejected result scenarios

Do not keep standalone client scenarios:

```text
SC-08 Approved Result
SC-09 Rejected Result
```

Current mapping:

```text
SC-07B = employee produces Approved/Rejected decision.
SC-05 = client views own request details with InReview/Approved/Rejected state.
SC-13A/SC-13B/SC-13C/SC-13D = agreement proposal exchange after approval.
```

### Agreement proposal model

Use:

```text
Agreement Proposal
```

Meaning:

```text
Agreement Proposal = concrete agreement document/version sent by one side to the other side in the context of an Approved request.
```

Core proposal statuses:

```text
AwaitingClientConfirmation
SentByClient
Accepted
Rejected
```

Future statuses/behaviors:

```text
Signed
Expired
Superseded
Cancelled
restore/return to an older proposal version
comment-only discussion without sending a new proposal
reject/close an approved request when agreement cannot be reached
```

Core agreement proposal rules:

```text
- agreement proposal exchange starts only from employee action on an Approved request;
- employee can start agreement proposal creation from Approved request details or from an Approved request row/list action;
- approval does not automatically create agreement proposal;
- both employee and client proposal submissions include attached agreement document/file and text details/comment;
- client can send only one own proposal version in response to an employee-sent proposal in core;
- client cannot start agreement proposal exchange without an existing employee-sent proposal;
- employee responds to a client-sent proposal by sending a new employee version;
- when employee sends a new version in response to a client-sent proposal, the previous client-sent proposal becomes Rejected.
```
