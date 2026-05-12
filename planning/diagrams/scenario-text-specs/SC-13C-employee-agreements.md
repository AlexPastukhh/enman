# SC-13C — Employee Agreements

## Status

Corrected scenario specification draft.

## Purpose

Employee views agreement proposals across approved requests and selects one for details or follow-up action.

## Actor / Screen

Actor: Employee  
Screen: Employee Agreements page  
Goal: View agreement proposal exchange and open one proposal

## Entry Points

Entry A: Employee opens Employee Agreements page.

Entry B: Employee continues after client sent own proposal version.

## Preconditions

- Employee is signed in.
- Employee has permission to access employee agreement proposals.
- At least one agreement proposal exists, or empty state is shown.

## DATA

Employee agreement list visible DATA  
`SC-13C-DATA-01`

Visible DATA:

```text
- agreement proposal summary visible enough to identify the proposal;
- related Approved request;
- client/applicant summary;
- sender: employee or client;
- agreement proposal status.
```

Employee agreement filter DATA  
`SC-13C-DATA-02`

Filter DATA:

```text
- agreement proposal status;
- sender: employee or client.
```

Extension / Future DATA:

```text
[VAR:EXPAND]
- client/applicant search;
- related Approved request filter;
- assigned employee filter if assignment is introduced;
- signed/unsigned state after electronic signature is introduced;
- date/period only if UX later needs time-based filtering.
```

## Main Flow

1. Employee opens Employee Agreements page.
2. Agreement proposal list is visible.
3. Each item shows related request, sender and status.
4. Employee can filter/search agreement proposals.
5. Employee selects an agreement proposal.
6. Employee Agreement Proposal Create / Send Version — SC-13D opens in details/follow-up mode.

## Branches

### Employee-sent proposal awaits client confirmation

-> status = AwaitingClientConfirmation  
-> sender = employee  
-> item is visible  
-> employee can open details and see waiting state

### Proposal was sent by client

-> status = SentByClient  
-> sender = client  
-> item is visible  
-> employee can open details and send a new employee version

### Proposal accepted

-> status = Accepted  
-> item is visible  
-> employee can open details and see accepted state

### Proposal rejected/replaced

-> status = Rejected  
-> item remains visible  
-> employee can open details and see rejected/replaced state

## Invariants

Employee can access only employee-authorized agreement proposal list.

Agreement proposal must be related to an Approved request.

Only employee can start agreement proposal exchange from an Approved request.

## Outcomes

- Employee sees agreement proposal exchange.
- Employee sees proposal sender and status.
- Employee can filter proposals.
- Employee can open proposal details/follow-up action.
