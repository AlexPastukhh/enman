# Diagram Scenario Specification

Status: source of truth for user-facing use-case/scenario diagram representation  
Scope: scenario/use-case diagrams only, not domain/aggregate/DB/CQRS diagrams

## Current Project Scenario Decisions For Diagrams

Use request statuses:

```text
InReview
Approved
Rejected
```

Request object location means object address.

SC-04 Request Creation applicant DATA behavior:

```text
- matching previously provided applicant DATA may prefill/copy into request form;
- if no matching applicant DATA exists, client enters applicant DATA inline;
- client can edit prefilled fields for this request;
- clear/restore prefill controls are future UX.
```

Do not keep standalone:

```text
SC-08 Approved Result
SC-09 Rejected Result
SC-16 Reliable Notification Delivery
```

SC-12 is merged into SC-05 + SC-04.

SC-18 Archive / Audit is deferred.

SC-14 Client Data Verification is future employee-started behavior, not triggered-by.

Agreement proposal scenarios:

```text
SC-13A Client My Agreements
SC-13B Client Agreement Proposal Details / Response
SC-13C Employee Agreements
SC-13D Employee Agreement Proposal Create / Send Version
```

Agreement proposal statuses:

```text
AwaitingClientConfirmation
SentByClient
Accepted
Rejected
```

Agreement proposal exchange starts only by employee action on an Approved request. Client can only send a proposal in response to an employee-sent proposal.
