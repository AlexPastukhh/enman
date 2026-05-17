# L2 Employee / Review / Agreement Domain Direction Clarification

Status: current clarification / derived from Domain Draft 02

Primary source:

```text
planning/tables/domain-drafts/domain-draft-02.md
```

## 1. Employee terminology

```text
Use Employee.
Do not use Worker.
```

Domain methods receive `Employee` when employee capability matters.

Owned state stores scalar ids such as:

```text
EmployeeId
StartedByEmployeeId
CompletedByEmployeeId
SenderId
FinalRefusedByEmployeeId
```

Do not use `EmployeeRef` in L2 target model.

## 2. Review ownership

```text
Review is not aggregate.
Request owns RequestReview.
RequestReview has no repository.
Public API lives on ConnectionRequest:
  StartReview
  ApproveReview
  RejectReview
```

`ReviewDecisionRecord` is removed from target model.

## 3. AgreementProposalExchange aggregate boundary

```text
AgreementProposalExchange is separate aggregate from Request.
Exchange does not mutate Request directly.
Request does not hold navigation to Exchange.
Application service orchestrates cross-aggregate workflows.
```

Final refusal orchestration:

```text
exchange.FinalRefuseProposal(employee, reason, refusedAt)
request.MarkAgreementExchangeFailed(exchange.Id, failedAt)
save transaction
```

## 4. Proposal versions and authors

```text
AgreementProposal is child entity, not aggregate.
AgreementProposalVersion is local per exchange.
Version starts at 1.
Next version = max existing version + 1.
API/client cannot choose version.
ActiveProposalVersion points to domain version, not DB id.
```

Author model:

```text
AgreementProposalAuthor.Sender
AgreementProposalAuthor.SenderId
```

Do not use:

```text
AggregateId
EmployeeRef
ClientRef
```

## 5. Final refusal

```text
Only Employee can final-refuse.
Final refusal marks exchange FinallyRefused.
Final refusal does not create a proposal version.
No AgreementFinalRefusal entity/class.
Final refusal is direct exchange state:
  FinalRefusedByEmployeeId?
  FinalRefusedAt?
  FinalRefusalReason?
```

## 6. Documents/comments

```text
AgreementDocumentRef is metadata reference to accepted/stored document.
It is not bytes and not storage adapter.
ProposalComment is optional.
If present, ProposalComment is non-empty and max-length constrained.
Empty string means no comment.
```
