# MANIFEST — L2 Employee / Review / Agreement Scenario Sync v2

Archive: `l2-employee-review-agreement-scenarios-sync-v2.zip`  
Scope: documentation-only scenario/source sync for transition after L1 foundation into Employee, RequestReview, AgreementProposalExchange and agreement documents.  
Primary source of truth: `planning/tables/domain-drafts/domain-draft-02.md`.

## Why v2

The previous scenario archive was based mainly on session summary. This v2 is rebuilt from the actual repository source file:

```text
planning/tables/domain-drafts/domain-draft-02.md
```

This matters because Draft 02 is the final-style L2 target domain draft and contains required decisions, state machines, impossible states, use-case coordination and code sketches.

## Add

```text
planning/diagrams/scenario-text-specs/SC-06-employee-request-dashboard.md
planning/diagrams/scenario-text-specs/SC-07A-employee-request-details.md
planning/diagrams/scenario-text-specs/SC-07B-employee-request-review.md
planning/diagrams/scenario-text-specs/SC-13A-client-agreements.md
planning/diagrams/scenario-text-specs/SC-13B-client-agreement-proposal-details-response.md
planning/diagrams/scenario-text-specs/SC-13C-employee-agreements.md
planning/diagrams/scenario-text-specs/SC-13D-employee-agreement-proposal-create-send-version.md
planning/diagrams/scenario-text-specs/SC-13E-agreement-final-refusal.md
planning/diagrams/scenario-text-specs/SC-14-agreement-documents.md
planning/diagrams/scenario-data/L2-employee-review-agreement-data.md
planning/diagrams/scenario-behavior-items/L2-employee-review-agreement-behavior-items.md
planning/diagrams/scenario-clarifications/L2-employee-review-agreement-domain-direction.md
```

## Replace

```text
planning/diagrams/README.md
planning/diagrams/scenario-text-specs/00-scenario-text-specs-index.md
planning/diagrams/scenario-data/00-scenario-data-index.md
planning/diagrams/scenario-behavior-items/00-scenario-behavior-items-index.md
planning/slices/slice-scenario-flow-behavior-register.md
```

## Delete

```text
none
```

## Required decisions reflected

```text
- Use Employee, not Worker.
- EmployeeRef is removed from L2 target model.
- Domain methods receive Employee domain object when employee capability matters.
- Owned records store scalar ids: EmployeeId, StartedByEmployeeId, CompletedByEmployeeId, SenderId.
- Review is not an aggregate; Request owns RequestReview.
- Request exposes StartReview / ApproveReview / RejectReview.
- RequestReview has no repository.
- ReviewDecisionRecord is removed.
- Review started state is visible in employee dashboard/details.
- Another employee cannot start/approve/reject a review already started by someone else.
- Approve/reject requires started review.
- AgreementProposalExchange is a separate aggregate from Request.
- AgreementProposal is a child entity, not aggregate.
- AgreementProposalVersion is local per-exchange value object, starts at 1, next=max+1.
- API/client cannot choose proposal version.
- ActiveProposalVersion points to domain version, not DB id.
- Sender/SenderId replace AggregateId, EmployeeRef and ClientRef.
- AgreementDocumentRef is metadata reference, not bytes or storage adapter.
- ProposalComment is optional; if present, non-empty with max length.
- Final refusal is direct exchange state, not separate entity/class.
- Final refusal does not create a new proposal version.
- Application service orchestrates exchange.FinalRefuseProposal(...) + request.MarkAgreementExchangeFailed(...).
- Do not use Worker/Open/Opened/AwaitingWorkerResponse.
```

## Non-goals

```text
- no domain/runtime code;
- no tests;
- no generated artifacts;
- no OpenAPI artifacts;
- no GitHub branch/commit/PR;
- no implementation slice drafts for these scenarios yet.
```
