# SC-13D вЂ” Employee Agreement Proposal Create / Send Version

Status: L2 scenario draft / Start exchange and Employee send-version direction synchronized  
Source: `planning/tables/domain-drafts/domain-draft-02.md`

## 1. Purpose

Employee starts an `AgreementProposalExchange` by sending the first proposal for an approved request, or sends a new Employee proposal version after a Client counter-proposal.

These are related but distinct slice boundaries:

```text
SL-AGR-EXCH-001 вЂ” creates exchange + proposal version 1.
SL-AGR-EXCH-002 вЂ” sends next Employee/Client proposal version inside existing exchange.
```

## 2. Start Exchange Flow

```text
Employee opens Approved request details with no AgreementProposalExchange
        в†“
Employee chooses Start Agreement Exchange
        в†“
Employee provides AgreementDocumentRef-backed initial proposal document and optional ProposalComment
        в†“
System calls AgreementProposalExchange.StartByEmployee(approvedRequest, document, comment, employee, startedAt)
        в†“
Exchange is created
        в†“
Exchange stores ClientAccountId from approved request owner
        в†“
Exchange status becomes AwaitingClientConfirmation
        в†“
First AgreementProposalVersion is 1
        в†“
First proposal author is Sender=Employee, SenderId=employee.Id
```

Start exchange does not happen automatically during ApproveReview.

## 3. Employee Sends New Version Flow

```text
Employee opens existing exchange AwaitingEmployeeResponse
        в†“
Employee provides new AgreementDocumentRef and optional ProposalComment
        в†“
System calls exchange.EmployeeSendNewVersion(document, comment, employee, createdAt)
        в†“
Domain checks Employee capability and exchange turn/lifecycle
        в†“
Active Client proposal becomes SupersededByCounterProposal
        в†“
Exchange creates next local AgreementProposalVersion
        в†“
Employee proposal becomes active
        в†“
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
L2-AGR-EMP-START-001 вЂ” Employee can start exchange for Approved request by sending first proposal.
L2-AGR-EMP-START-002 вЂ” First proposal version is 1 and status is AwaitingClientConfirmation.
L2-AGR-EMP-START-003 вЂ” Start exchange stores ClientAccountId from approved request owner.
L2-AGR-EMP-NEW-001 вЂ” Employee can send new version when exchange is AwaitingEmployeeResponse.
L2-AGR-EMP-NEW-002 вЂ” Employee new version supersedes active Client proposal and returns exchange to AwaitingClientConfirmation.
L2-AGR-AUTHOR-001 вЂ” Proposal author uses Sender and SenderId.
```

## Diagram / Implementation Markers

These markers are for diagrams and diploma planning only. They do not replace current repo implementation evidence.

| Scenario element | Marker | Diagram / implementation meaning |
|---|---|---|
| Start exchange from Employee request details | `[PLANNED]` | Current L2 planned command: Approved request -> create exchange + initial Employee proposal. |
| Initial Employee proposal version 1 | `[DESIGNED]` | Accepted domain behavior: start is not empty; it creates proposal version 1. |
| Store `ClientAccountId` from approved request owner | `[DESIGNED]` | Accepted participant/ownership invariant for future client actions. |
| Employee sends new version after client counter-proposal | `[PLANNED]` | Current L2 planned Employee branch of `SL-AGR-EXCH-002`. |
| Proposal author `Sender` / `SenderId` | `[DESIGNED]` | Accepted proposal-author model; do not use `EmployeeRef`/`ClientRef`. |
| API/client choosing proposal version | `[DEFERRED]` | Not allowed in current domain; exchange assigns version. |

