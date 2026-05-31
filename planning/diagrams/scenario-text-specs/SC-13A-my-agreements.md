# SC-13A — My Agreements

## Status

Corrected scenario specification draft.  
Doc version: v0.1.0

## Purpose

Client views all own agreement proposals and selects one for details/response.

## Actor / Screen

Actor: Client  
Screen: My Agreements page  
Goal: View agreement proposals and open one proposal

## Entry Points

Entry A: Client opens My Agreements page.

Entry B: Client continues after request approval when agreement proposal may be available.

## Preconditions

- Client is signed in.
- Client has at least one agreement proposal, or empty state is shown.

## DATA

Agreement list visible DATA  
`SC-13A-DATA-01`

Visible DATA:

```text
- agreement proposal summary visible enough to identify the proposal;
- related Approved request;
- sender: employee or client;
- agreement proposal status.
```

Agreement filter DATA  
`SC-13A-DATA-02`

Filter DATA:

```text
- agreement proposal status;
- sender: employee or client.
```

Extension / Future DATA:

```text
[VAR:EXPAND]
- related Approved request filter;
- signed/unsigned state after electronic signature is introduced;
- date/period only if UX later needs time-based filtering.
```

## Main Flow

1. Client opens My Agreements page.
2. Own agreement proposal list is visible.
3. Each item shows sender and status.
4. Client can filter/search agreement proposals.
5. Client selects an agreement proposal.
6. Agreement Proposal Details / Response — SC-13B opens.

## Branches

### Employee-sent proposal awaits client confirmation

-> status = AwaitingClientConfirmation  
-> sender = employee  
-> item is visible in My Agreements  
-> client can open details and respond

### Proposal was sent by client

-> status = SentByClient  
-> sender = client  
-> item is visible in My Agreements  
-> client can open details and see sent proposal state

### Proposal accepted

-> status = Accepted  
-> item is visible in My Agreements  
-> client can open details and see accepted state

### Proposal rejected/replaced by new employee version

-> status = Rejected  
-> item remains visible in My Agreements  
-> client can open details and see that newer employee version replaced/rejected it

## Invariants

Client can view only own agreement proposals.

Agreement proposal must be related to an Approved request accessible to this client.

Client cannot start agreement proposal exchange without an employee-sent proposal.

## Outcomes

- Client sees all own agreement proposals.
- Client sees proposal sender and status.
- Client can filter proposals.
- Client can select one proposal and open details.
