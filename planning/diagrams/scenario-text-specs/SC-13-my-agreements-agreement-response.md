# SC-13 — My Agreements / Agreement Details / Agreement Response

## Status

Corrected scenario specification draft. Replaces old Contract Acknowledgement wording.

## Purpose

Client views agreement options/drafts and responds to an agreement option.

## Actor / Screen

Actor: Client  
Screen: My Agreements / Agreement Details  
Goal: Review agreement option and respond

## Entry Points

Entry A: Client opens My Agreements page.

Entry B: Client opens agreement details after agreement option became available.

Entry C [future]: Client starts sending own agreement option directly from My Agreements by selecting an Approved request.

## Preconditions

- Client is signed in.
- Client has agreement options/drafts or empty state is shown.
- For responding to employee-sent option, an agreement option exists.

## DETAIL

Agreement filter criteria  
`SC-13-DETAIL-01`

- agreement state;
- related Approved request;
- sent by employee/client;
- date/period;
- agreement option type if relevant;
- [VAR:EXPAND]

Agreement response details  
`SC-13-DETAIL-02`

- selected agreement option;
- accept/confirm action;
- attached own agreement version/counterproposal if provided;
- comment/message if user-visible;
- [VAR:EXPAND]

## Main Flow

1. Client opens My Agreements page.
2. Agreement list is visible.
3. Client can filter/search agreements.
4. Client selects agreement/option.
5. Agreement Details opens.
6. Client reviews option sent by employee.
7. Client chooses response: accept/confirm current option or attach own agreement version/counterproposal and send it back.
8. Response is recorded/visible.

## Branches

### Accept/confirm current option

-> client accepts/confirms current option
-> acceptance/confirmation is recorded
-> agreement response state is visible

### Attach own agreement version/counterproposal

-> client attaches own version/counterproposal
-> client submits response
-> response is recorded/visible

### Future send own option directly

-> client starts from My Agreements page
-> selects Approved request related to option
-> attaches agreement version
-> sends it

### Future electronic signature

-> future behavior
-> electronic signature flow replaces or extends confirmation

## Invariants

Client can view only own agreements/agreement options.

Attach to:

- agreement list loaded;
- agreement details opens;

Client can respond only to agreement options available to them.

Attach to:

- submit agreement response;

## Outcomes

- Client sees agreement list.
- Client sees agreement details.
- Client can accept/confirm employee-sent option.
- Client can send own agreement version/counterproposal.
- Future electronic signature is identified but not L1.

## ADR / Policy Candidates

ADR?: Agreements list/detail uses the same pattern as My Requests and Employee Dashboard: list page -> filter/search -> select item -> details page -> action from details.

ADR?: Future implementation may allow client to send own agreement option directly from My Agreements by selecting Approved request.

## Open Questions

Q: After approval/agreement feedback, should UX guide client to Request Details, My Agreements, Agreement Details, or multiple destinations?

Q: What exact states do agreement options have in L1?

## Diagram Notes

- Do not use Contract Acknowledgement wording.
- Use Agreement / Agreement Option / Agreement Details / Agreement Response.
- Do not model electronic signature as current behavior.
