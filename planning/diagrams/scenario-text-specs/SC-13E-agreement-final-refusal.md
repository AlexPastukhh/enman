# SC-13E — Agreement Final Refusal

Status: L2 scenario draft / derived from Domain Draft 02  
Source: `planning/tables/domain-drafts/domain-draft-02.md`

## 1. Purpose

Employee can final-refuse an active agreement proposal exchange, and the related approved request is marked as agreement exchange failed by application-service orchestration.

## 2. Scenario Flow

```text
Employee opens active AgreementProposalExchange
        ↓
Exchange is AwaitingClientConfirmation or AwaitingEmployeeResponse
        ↓
Employee chooses final refusal and may provide FinalRefusalReason
        ↓
Application service calls exchange.FinalRefuseProposal(employee, reason, refusedAt)
        ↓
Exchange becomes FinallyRefused
        ↓
Application service calls request.MarkAgreementExchangeFailed(exchange.Id, failedAt)
        ↓
Request becomes AgreementExchangeFailed
        ↓
System saves both aggregate changes in one application transaction
```

## 3. Domain Direction

```text
AgreementProposalExchange and Request are separate aggregates.
Exchange does not mutate Request directly.
Request does not hold navigation to Exchange.
Application service orchestrates cross-aggregate final refusal.
Final refusal is not a separate entity/class.
Final refusal is direct exchange state:
  FinalRefusedByEmployeeId?
  FinalRefusedAt?
  FinalRefusalReason?
Final refusal does not create a new proposal version.
```

## 4. Blocked Flows

```text
Client final refusal -> blocked.
Final refusal after Accepted exchange -> blocked.
Final refusal of Already FinallyRefused exchange -> blocked.
Request MarkAgreementExchangeFailed from non-Approved status -> blocked.
```

## 5. Behavior Items

```text
L2-AGR-FINAL-001 — Employee can final-refuse exchange in AwaitingClientConfirmation or AwaitingEmployeeResponse.
L2-AGR-FINAL-002 — Final refusal marks exchange FinallyRefused and records employee/time/reason fields.
L2-AGR-FINAL-003 — Final refusal does not create a new proposal version.
L2-REQ-AGR-FAIL-001 — Application service marks approved request AgreementExchangeFailed after exchange final refusal.
L2-AGR-BOUNDARY-001 — Exchange and Request stay separate aggregates; application service orchestrates both.
```
