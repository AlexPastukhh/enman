# SC-13B — Agreement Proposal Details / Response

## Status

Corrected scenario specification draft.  
Doc version: v0.1.0

## Purpose

Client views one agreement proposal and either accepts an employee-sent proposal or sends one own agreement proposal version back.

## Actor / Screen

Actor: Client  
Screen: Agreement Proposal Details  
Goal: Review and respond to agreement proposal

## Entry Points

Entry A: Client selects agreement proposal from My Agreements — SC-13A.

Entry B: Client continues after request approval when a specific agreement proposal is available.

## Preconditions

- Client is signed in.
- Agreement proposal belongs to the client.
- Agreement proposal is related to an Approved request.

## DATA

Agreement proposal details visible DATA  
`SC-13B-DATA-01`

Visible DATA:

```text
- related Approved request;
- agreement proposal summary/name;
- sender: employee or client;
- agreement proposal status / lifecycle term;
- text details/comment sent with proposal;
- attached agreement document/file;
- available client action for current status.
```

Client agreement proposal submission DATA  
`SC-13B-DATA-02`

Attachment DATA:

```text
- client-selected agreement document/version to send back.
```

Input DATA:

```text
- text details/comment sent with client proposal.
```

Extension / Future DATA:

```text
[VAR:EXPAND]
- comment-only message without sending a new proposal;
- electronic signature status;
- signed agreement document;
- signature timestamp;
- final agreement number.
```

## Main Flow

1. Client opens Agreement Proposal Details.
2. Related Approved request is visible.
3. Agreement proposal document/summary is visible.
4. Sender, status/lifecycle term and text details/comment are visible.
5. If proposal awaits client confirmation, client can accept current proposal or send one own version.
6. Client chooses action.
7. If accepted, agreement proposal status becomes Accepted.
8. If client sends own version, client attaches agreement document/version, enters text details/comment and submits it.
9. Client-sent proposal becomes visible with status SentByClient.

## Branches

### Employee proposal awaits client confirmation

-> status = AwaitingClientConfirmation  
-> proposal was sent by employee  
-> client can accept proposal  
-> client can attach/send one own version with text details/comment

### Client accepts proposal

-> client accepts current proposal  
-> proposal status becomes Accepted  
-> accepted state is visible

### Client sends own proposal version

-> client attaches own agreement document/version  
-> client enters text details/comment  
-> client submits own version  
-> new/client-sent agreement proposal is recorded  
-> status = SentByClient  
-> sent proposal state is visible

### Proposal already sent by client

-> status = SentByClient  
-> client sees sent proposal state  
-> no duplicate client response action in core

### Proposal accepted

-> status = Accepted  
-> client sees accepted state  
-> no response action is needed

### Proposal explicitly rejected

-> status = Rejected  
-> client sees explicit rejection/decline state  
-> no response action is needed

### Proposal superseded/replaced by counterproposal

-> status/lifecycle term = SupersededByCounterProposal  
-> client sees that this proposal version was superseded/replaced by a newer counterproposal  
-> newer employee version should be available separately if employee sent one

## Invariants

Client can open only own agreement proposal details.

Client can respond only to employee-sent proposal awaiting client confirmation.

Client can send only one own proposal version in response to an employee proposal in core.

Client-sent own version must include attached agreement document/version.

`Rejected` means explicit rejection/decline only.

A proposal version replaced by a counterproposal is superseded/replaced, not ordinary `Rejected`.

## Outcomes

- Client can review agreement proposal.
- Client can accept employee-sent proposal.
- Client can send one own agreement document/version back with text details/comment.
- Client sees current proposal status/lifecycle term.
- Client can distinguish explicit rejection from superseded/replaced-by-counterproposal state.
