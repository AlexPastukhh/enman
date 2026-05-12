# SC-13B — Agreement Proposal Details / Response

## Status

Corrected scenario specification draft.

## Purpose

Client views one agreement proposal and either confirms/accepts it or sends own agreement proposal version back.

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
- agreement proposal status;
- attached agreement document/file;
- available client action for current status.
```

Client agreement attachment DATA  
`SC-13B-DATA-02`

Attachment DATA:

```text
- client-selected agreement document/version to send back.
```

Extension / Future DATA:

```text
[VAR:EXPAND]
- optional comment/message;
- electronic signature status;
- signed agreement document;
- signature timestamp;
- final agreement number.
```

## Main Flow

1. Client opens Agreement Proposal Details.
2. Related Approved request is visible.
3. Agreement proposal document/summary is visible.
4. Sender and status are visible.
5. If proposal awaits client confirmation, client can accept current proposal or send own version.
6. Client chooses action.
7. If accepted, agreement proposal status becomes Accepted.
8. If client sends own version, client attaches agreement document/version and submits it.
9. Client-sent proposal becomes visible with status SentByClient.

## Branches

### Proposal awaits client confirmation

-> status = AwaitingClientConfirmation  
-> proposal was sent by employee  
-> client can accept proposal  
-> client can attach/send own version

### Client accepts proposal

-> client accepts current proposal  
-> proposal status becomes Accepted  
-> accepted state is visible

### Client sends own proposal version

-> client attaches own agreement document/version  
-> client submits own version  
-> new/client-sent agreement proposal is recorded  
-> status = SentByClient  
-> sent proposal state is visible

### Proposal already sent by client

-> status = SentByClient  
-> client sees sent proposal state  
-> no duplicate response action unless future workflow allows it

### Proposal already accepted

-> status = Accepted  
-> client sees accepted state  
-> no response action is needed

## Invariants

Client can open only own agreement proposal details.

Attach to:

- Agreement Proposal Details opens.

Client can respond only to proposal available for client response.

Attach to:

- action availability;
- accept/send own version transitions.

Client-sent own version must include an attached agreement document/version.

Attach to:

- send own version transition.

## Step Postconditions

- Agreement proposal status becomes Accepted after client accepts.
- Client-sent proposal/version becomes visible after client submits own version.

## Outcomes

- Client can review agreement proposal.
- Client can accept employee-sent proposal.
- Client can send own agreement document/version back.
- Client sees current proposal status.

## ADR / Policy Candidates

ADR?: Agreement Proposal is a document/version sent by one side to the other side.

ADR?: Core statuses are AwaitingClientConfirmation, SentByClient and Accepted.

ADR?: Electronic signature is future behavior, not current core.

## Open Questions

Q: Is accept/confirm legally meaningful acceptance or only confirmation of the current proposal?

Q: Can client send multiple versions, or only one response per awaiting proposal?

Q: Does employee see and accept/reject client-sent proposal in a future employee agreement scenario?

Q: Should final accepted proposal become a separate final Agreement entity later?

## Diagram Notes

- Key flow DATA is sender + status.
- Do not model template generation, PDF generation, provider, filesystem or DB storage.
- Do not include electronic signature as core behavior.
