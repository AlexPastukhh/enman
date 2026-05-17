# SC-13A вЂ” Client Agreements

Status: L2 scenario draft / Agreement Exchange list direction synchronized  
Source: `planning/tables/domain-drafts/domain-draft-02.md`

## 1. Purpose

Client sees agreement proposal exchanges related to their approved requests.

This scenario is about existing `AgreementProposalExchange` records. It does not create an exchange and does not include proposal command behavior.

## 2. Scenario Flow

```text
Client opens Agreements area
        в†“
System shows agreement proposal exchanges where Client is the exchange participant
        в†“
Client sees current exchange status:
  AwaitingClientConfirmation
  AwaitingEmployeeResponse
  Accepted
  FinallyRefused
        в†“
Client opens agreement exchange details when action or review is needed
```

## 3. Access / Ownership Direction

```text
AgreementProposalExchange stores ClientAccountId.
Client list/read access is limited to exchanges where:
  exchange.ClientAccountId == current ClientAccount.Id
```

UI visibility is not authorization. Server-side read filtering remains authoritative.

## 4. Domain Direction

```text
AgreementProposalExchange is separate aggregate from Request.
Request approval enables exchange start but does not create it automatically.
AgreementProposalExchange owns proposal versions.
Request does not navigate to Exchange.
```

## 5. Validation / Scenario-Local Guardrails

```text
Client list/read filters by ClientAccountId.
Counter-proposal replacement is SupersededByCounterProposal, not ordinary Rejected.
AgreementDocumentRef is document metadata reference, not file bytes/storage adapter.
```

## 6. Behavior Items

```text
L2-AGR-CLIENT-LIST-001 вЂ” Client can see agreement proposal exchanges for their requests.
L2-AGR-CLIENT-LIST-002 вЂ” Client sees exchange status and active proposal version summary.
L2-AGR-CLIENT-LIST-003 вЂ” Client cannot see another ClientAccount agreement exchange.
```

## 7. Out of Scope

```text
- accepting proposal / sending client version -> SC-13B;
- employee proposal creation -> SC-13D;
- final refusal by Employee -> SC-13E;
- document storage implementation -> SC-14.
```

## Diagram / Implementation Markers

These markers are for diagrams and diploma planning only. They do not replace current repo implementation evidence.

| Scenario element | Marker | Diagram / implementation meaning |
|---|---|---|
| Client agreement exchange list | `[PLANNED]` | Current L2 planned read surface for existing AgreementProposalExchange records. |
| Client filtering by `AgreementProposalExchange.ClientAccountId` | `[DESIGNED]` | Accepted ownership/participant rule for client access. |
| Exchange status summary | `[PLANNED]` | List shows AwaitingClientConfirmation / AwaitingEmployeeResponse / Accepted / FinallyRefused summary. |
| Proposal version history | `[DEFERRED]` | Full history belongs to details (`SC-13B` / `SC-13D`), not list summary. |
| Document bytes/download | `[DEFERRED]` | Belongs to future document/storage slice, not agreement list. |

