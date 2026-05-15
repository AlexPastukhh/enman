# SC-13D — Employee Agreement Proposal Create / Send Version

## Status

Corrected scenario specification draft.

## Purpose

Employee starts agreement proposal exchange from an Approved request or sends a new employee version in response to a client-sent proposal.

## Actor / Screen

Actor: Employee  
Screen: Agreement Proposal Create / Employee Agreement Proposal Details  
Goal: Send agreement proposal version to client

## Entry Points

Entry A: Employee starts agreement proposal creation from Approved request details.

Entry B: Employee starts agreement proposal creation from Approved request list row/action.

Entry C: Employee opens client-sent proposal from Employee Agreements — SC-13C and sends a new employee version.

## Preconditions

- Employee is signed in.
- Employee has permission to manage agreement proposals.
- Related request status is Approved.
- For Entry C, client-sent proposal exists.

## DATA

Employee agreement proposal creation DATA  
`SC-13D-DATA-01`

Reference / Visible DATA:

```text
- related Approved request;
- client/applicant summary;
- previous agreement proposal, if responding to client-sent proposal.
```

Attachment DATA:

```text
- employee-selected agreement document/version to send.
```

Input DATA:

```text
- text details/comment sent with employee proposal.
```

Visible DATA after submit:

```text
- sender = employee;
- status = AwaitingClientConfirmation;
- attached agreement document/file;
- text details/comment.
```

## Main Flow

1. Employee starts agreement proposal creation from Approved request or from client-sent proposal details.
2. Related Approved request is visible.
3. Employee attaches agreement document/version.
4. Employee enters text details/comment.
5. Employee submits agreement proposal.
6. Employee-sent proposal is recorded.
7. Proposal status becomes AwaitingClientConfirmation.
8. Proposal becomes visible to client in My Agreements.
9. Proposal becomes visible to employee in Employee Agreements.

## Branches

### Initial employee proposal from Approved request

-> request status = Approved  
-> employee starts agreement proposal creation  
-> employee attaches document and text details/comment  
-> employee submits proposal  
-> status = AwaitingClientConfirmation

### Employee sends new version in response to client-sent proposal

-> previous proposal status = SentByClient  
-> employee attaches new document and text details/comment  
-> employee submits new employee version  
-> previous client-sent proposal becomes SupersededByCounterProposal  
-> new employee proposal status = AwaitingClientConfirmation

### Request is not Approved

-> request status is not Approved  
-> create agreement proposal action is not available

### Missing attachment or details

-> required agreement document/details missing  
-> errors are visible  
-> employee corrects proposal data  
-> back to editing proposal

## Invariants

Agreement proposal exchange can be started only by employee.

Initial agreement proposal can be created only from Approved request.

Client cannot start agreement proposal exchange without an employee-sent proposal.

Employee-sent proposal must include attached agreement document/version and text details/comment.

When employee sends a new version in response to a client-sent proposal, the previous client-sent proposal is superseded/replaced by the employee counterproposal.

The previous client-sent proposal must not be treated as ordinary `Rejected` unless there is a separate explicit rejection/decline action.

## Outcomes

- Employee can start agreement proposal exchange from an Approved request.
- Employee can send agreement document/version with text details/comment.
- Client can see employee-sent proposal in My Agreements.
- Employee can respond to client-sent proposal by sending a new version.
- Client-sent proposal is superseded/replaced by the employee counterproposal when employee sends a new version.
