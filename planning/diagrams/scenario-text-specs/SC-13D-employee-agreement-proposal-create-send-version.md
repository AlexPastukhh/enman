# SC-13D — Employee Agreement Proposal Create / Send Version

Status: L2 scenario draft / Start exchange and Employee send-version direction synchronized  
Source: `planning/tables/domain-drafts/domain-draft-02.md`

## 1. Purpose

Employee starts an `AgreementProposalExchange` by sending the first proposal for an approved request, or sends a new Employee proposal version after a Client counter-proposal.

These are related but distinct slice boundaries:

```text
SL-AGR-EXCH-001 — creates exchange + proposal version 1.
SL-AGR-EXCH-002 — sends next Employee/Client proposal version inside existing exchange.
```

## 2. Start Exchange Flow

```text
Employee opens Approved request details with no AgreementProposalExchange
        ↓
Employee chooses Start Agreement Exchange
        ↓
Employee provides AgreementDocumentRef-backed initial proposal document and optional ProposalComment
        ↓
System calls AgreementProposalExchange.StartByEmployee(approvedRequest, document, comment, employee, startedAt)
        ↓
Exchange is created
        ↓
Exchange stores ClientAccountId from approved request owner
        ↓
Exchange status becomes AwaitingClientConfirmation
        ↓
First AgreementProposalVersion is 1
        ↓
First proposal author is Sender=Employee, SenderId=employee.Id
```

Start exchange does not happen automatically during ApproveReview.

## 3. Employee Sends New Version Flow

```text
Employee opens existing exchange AwaitingEmployeeResponse
        ↓
Employee provides new AgreementDocumentRef and optional ProposalComment
        ↓
System calls exchange.EmployeeSendNewVersion(document, comment, employee, createdAt)
        ↓
Domain checks Employee capability and exchange turn/lifecycle
        ↓
Active Client proposal becomes SupersededByCounterProposal
        ↓
Exchange creates next local AgreementProposalVersion
        ↓
Employee proposal becomes active
        ↓
Exchange returns to AwaitingClientConfirmation
```

## 4. Domain Direction

```text
AgreementProposalExchange owns proposal versions.
AgreementProposal is child entity, not aggregate.
AgreementProposalVersion is local per exchange.
ActiveProposalVersion points to domain version, not DB id.
AgreementProposalAuthor uses Sender + SenderId.
Do not use AggregateId, EmployeeRef or ClientRef.
No ResponsibleEmployeeId guard first pass.
```

## 5. Validation / Scenario-Local Guardrails

DTO/request-shape validation:

```text
AgreementDocumentRef input is required.
ProposalComment is optional.
If ProposalComment is present, it must be non-empty and max-length constrained.
```

Domain validation/invariants:

```text
Start requires Approved request and no existing exchange.
Start stores ClientAccountId from approved request owner.
Employee new version requires AwaitingEmployeeResponse.
Active proposal author must be Client for EmployeeSendNewVersion.
Accepted or FinallyRefused exchange cannot continue.
Counter-proposal replacement uses SupersededByCounterProposal, not Rejected.
```

Do not put ownership/lifecycle/turn rules in FluentValidation.

## 6. Behavior Items

```text
L2-AGR-EMP-START-001 — Employee can start exchange for Approved request by sending first proposal.
L2-AGR-EMP-START-002 — First proposal version is 1 and status is AwaitingClientConfirmation.
L2-AGR-EMP-START-003 — Start exchange stores ClientAccountId from approved request owner.
L2-AGR-EMP-NEW-001 — Employee can send new version when exchange is AwaitingEmployeeResponse.
L2-AGR-EMP-NEW-002 — Employee new version supersedes active Client proposal and returns exchange to AwaitingClientConfirmation.
L2-AGR-AUTHOR-001 — Proposal author uses Sender and SenderId.
```
