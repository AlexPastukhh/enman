# SC-13B — Client Agreement Proposal Details / Response

Status: L2 scenario draft / derived from Domain Draft 02  
Source: `planning/tables/domain-drafts/domain-draft-02.md`

## 1. Purpose

Client reviews an active employee proposal and either accepts it or sends their own counter-proposal version.

## 2. Accept Active Proposal Flow

```text
Client opens active proposal details
        ↓
System shows active employee proposal document and optional comment
        ↓
Client accepts active proposal
        ↓
System calls exchange.ClientAcceptActiveProposal(client, acceptedAt)
        ↓
Active proposal becomes Accepted
        ↓
Exchange becomes Accepted
```

## 3. Client Sends Own Version Flow

```text
Client opens active employee proposal
        ↓
Client provides AgreementDocumentRef-backed document and optional ProposalComment
        ↓
System calls exchange.ClientSendOwnVersion(document, comment, client, createdAt)
        ↓
Previous active employee proposal becomes SupersededByCounterProposal
        ↓
Exchange creates next local AgreementProposalVersion
        ↓
Client proposal becomes active
        ↓
Exchange status becomes AwaitingEmployeeResponse
```

## 4. Domain Direction

```text
AgreementProposalVersion is generated only by exchange.
API/client cannot choose version.
Final refusal does not create proposal version.
SupersededByCounterProposal is replacement, not rejection/final refusal.
```

## 5. Behavior Items

```text
L2-AGR-CLIENT-ACCEPT-001 — Client can accept active employee proposal while exchange is AwaitingClientConfirmation.
L2-AGR-CLIENT-SEND-001 — Client can send own proposal version in response to active employee proposal.
L2-AGR-VERSION-001 — Client does not choose proposal version; exchange assigns next local version.
L2-AGR-SUPERSEDE-001 — Previous proposal is SupersededByCounterProposal, not Rejected, when replaced by counter-proposal.
```
