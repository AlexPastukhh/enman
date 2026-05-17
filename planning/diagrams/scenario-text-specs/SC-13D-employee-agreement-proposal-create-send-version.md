# SC-13D — Employee Agreement Proposal Create / Send Version

Status: L2 scenario draft / derived from Domain Draft 02  
Source: `planning/tables/domain-drafts/domain-draft-02.md`

## 1. Purpose

Employee starts an AgreementProposalExchange by sending the first proposal for an approved request, or sends a new employee proposal version after a client counter-proposal.

## 2. Start Exchange Flow

```text
Employee opens Approved request without AgreementProposalExchange
        ↓
Employee attaches/selects accepted agreement document reference and optional comment
        ↓
System calls AgreementProposalExchange.StartByEmployee(approvedRequest, document, comment, employee, startedAt)
        ↓
Exchange is created with status AwaitingClientConfirmation
        ↓
First AgreementProposalVersion is 1
        ↓
First proposal author is Sender=Employee, SenderId=employee.Id
```

## 3. Employee Sends New Version Flow

```text
Employee opens exchange AwaitingEmployeeResponse
        ↓
Employee provides new AgreementDocumentRef and optional ProposalComment
        ↓
System calls exchange.EmployeeSendNewVersion(document, comment, employee, createdAt)
        ↓
Active client proposal becomes SupersededByCounterProposal
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
```

## 5. Behavior Items

```text
L2-AGR-EMP-START-001 — Employee can start exchange for Approved request by sending first proposal.
L2-AGR-EMP-START-002 — First proposal version is 1 and status is AwaitingClientConfirmation.
L2-AGR-EMP-NEW-001 — Employee can send new version when exchange is AwaitingEmployeeResponse.
L2-AGR-EMP-NEW-002 — Employee new version supersedes active client proposal and returns exchange to AwaitingClientConfirmation.
L2-AGR-AUTHOR-001 — Proposal author uses Sender and SenderId.
```
