# SC-13A — Client Agreements

Status: L2 scenario draft / derived from Domain Draft 02  
Source: `planning/tables/domain-drafts/domain-draft-02.md`

## 1. Purpose

Client sees agreement proposal exchanges that are awaiting client confirmation or have final outcomes.

## 2. Scenario Flow

```text
Client opens Agreements area
        ↓
System shows agreement proposal exchanges related to client's approved requests
        ↓
Client sees current exchange status:
  AwaitingClientConfirmation
  AwaitingEmployeeResponse
  Accepted
  FinallyRefused
        ↓
Client opens active proposal details when action is needed
```

## 3. Domain Direction

```text
AgreementProposalExchange is separate aggregate from Request.
Request approval enables exchange start but does not create it automatically.
AgreementProposalExchange owns proposal versions.
Request does not navigate to Exchange.
```

## 4. Behavior Items

```text
L2-AGR-CLIENT-LIST-001 — Client can see agreement proposal exchanges for their requests.
L2-AGR-CLIENT-LIST-002 — Client sees exchange status and active proposal version summary.
```

## 5. Out of Scope

```text
- accepting proposal / sending client version -> SC-13B;
- employee proposal creation -> SC-13D;
- final refusal by Employee -> SC-13E;
- document storage implementation -> SC-14.
```
