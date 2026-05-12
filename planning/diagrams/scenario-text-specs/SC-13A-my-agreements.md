# SC-13A — My Agreements

## Status

Corrected scenario specification draft.

## Purpose

Client views agreement proposals related to approved requests and selects one for details/response.

## Actor / Screen

Actor: Client  
Screen: My Agreements page  
Goal: View agreement proposals and open one proposal

## Entry Points

Entry A: Client opens My Agreements page.

Entry B: Client continues after request approval when agreement proposal may be available.

## Preconditions

- Client is signed in.
- Client has at least one approved request or agreement proposal, or empty state is shown.

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

### Agreement proposal awaits client confirmation

-> status = AwaitingClientConfirmation  
-> item is visible in My Agreements  
-> client can open details and respond

### Agreement proposal was sent by client

-> status = SentByClient  
-> item is visible in My Agreements  
-> client can open details and see sent proposal state

### Agreement proposal accepted

-> status = Accepted  
-> item is visible in My Agreements  
-> client can open details and see accepted state

## Invariants

Client can view only own agreement proposals.

Attach to:

- My Agreements list loaded;
- Agreement Proposal Details opens.

Agreement proposal must be related to an approved request accessible to this client.

Attach to:

- list item visibility;
- details open transition.

## Outcomes

- Client sees own agreement proposals.
- Client sees proposal sender and status.
- Client can select one proposal and open details.
- Client cannot see another client's agreement proposals.

## Open Questions

Q: Should My Agreements include only proposals or also final accepted/signed agreements after future signature flow?

Q: Is date/period filtering needed in L1, or future only?

Q: Does approval always create an agreement proposal, or does employee send it separately?

## Diagram Notes

- Do not use Contract Acknowledgement wording.
- Agreement proposal list follows the same list/filter/select pattern as My Requests.
- Keep filtering separate from selecting/opening details.
