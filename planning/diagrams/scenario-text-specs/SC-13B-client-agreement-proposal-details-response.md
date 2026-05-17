# SC-13B — Client Agreement Proposal Details / Response

Status: L2 scenario draft / Client ownership and counter-proposal terminology synchronized  
Source: `planning/tables/domain-drafts/domain-draft-02.md`

## 1. Purpose

Client reviews an active Employee proposal and either accepts it or sends their own counter-proposal version.

## 2. Accept Active Proposal Flow

```text
Client opens active proposal details
        ↓
System shows active Employee proposal document and optional comment
        ↓
Client accepts active proposal
        ↓
System calls exchange.ClientAcceptActiveProposal(client, acceptedAt)
        ↓
Domain checks client.Id == exchange.ClientAccountId
        ↓
Active proposal becomes Accepted
        ↓
Exchange becomes Accepted
```

Accept does not create a new proposal version.

## 3. Client Sends Own Version Flow

```text
Client opens active Employee proposal
        ↓
Client provides AgreementDocumentRef-backed document and optional ProposalComment
        ↓
System calls exchange.ClientSendOwnVersion(document, comment, client, createdAt)
        ↓
Domain checks client.Id == exchange.ClientAccountId
        ↓
Previous active Employee proposal becomes SupersededByCounterProposal
        ↓
Exchange creates next local AgreementProposalVersion
        ↓
Client proposal becomes active
        ↓
Exchange status becomes AwaitingEmployeeResponse
```

## 4. Domain Direction

```text
AgreementProposalExchange stores ClientAccountId.
AgreementProposalVersion is generated only by exchange.
API/client cannot choose version.
Final refusal does not create proposal version.
SupersededByCounterProposal is replacement, not rejection/final refusal.
AgreementProposal.Author uses Sender and SenderId.
```

## 5. Validation / Scenario-Local Guardrails

DTO/request-shape validation:

```text
AgreementDocumentRef input is required for counter-proposal.
ProposalComment is optional.
If ProposalComment is present, it must be non-empty and max-length constrained.
```

Domain validation/invariants:

```text
client.Id == exchange.ClientAccountId.
Exchange status allows Client response.
Active proposal author is Employee for Client accept/send-own-version.
Accepted or FinallyRefused exchange cannot continue.
Counter-proposal replacement uses SupersededByCounterProposal, not Rejected.
```

Do not put ownership/lifecycle/turn rules in FluentValidation.

## 6. Behavior Items

```text
L2-AGR-CLIENT-ACCEPT-001 — Client can accept active Employee proposal while exchange is AwaitingClientConfirmation.
L2-AGR-CLIENT-SEND-001 — Client can send own proposal version in response to active Employee proposal.
L2-AGR-VERSION-001 — Client does not choose proposal version; exchange assigns next local version.
L2-AGR-SUPERSEDE-001 — Previous proposal is SupersededByCounterProposal, not Rejected, when replaced by counter-proposal.
L2-AGR-CLIENT-OWNERSHIP-001 — Client actions are allowed only for exchange.ClientAccountId.
```
