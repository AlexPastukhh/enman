# SC-13E — Agreement Final Refusal

Status: L2 scenario draft / Final refusal optional reason and cross-aggregate orchestration synchronized  
Source: `planning/tables/domain-drafts/domain-draft-02.md`

## 1. Purpose

Employee can final-refuse an active agreement proposal exchange, and the related approved request is marked as agreement exchange failed by application-service orchestration.

## 2. Scenario Flow

```text
Employee opens active AgreementProposalExchange
        ↓
Exchange is AwaitingClientConfirmation or AwaitingEmployeeResponse
        ↓
Employee chooses final refusal
        ↓
Employee may optionally provide FinalRefusalReason
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

## 3. Reason Direction

```text
FinalRefusalReason is optional first pass.
Missing/null reason is allowed.
Blank/whitespace-only reason is invalid if a reason field is provided.
```

This is Employee final refusal, not Client rejection and not review rejection.

## 4. Domain Direction

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
No ResponsibleEmployeeId guard first pass.
```

## 5. Blocked Flows

```text
Client final refusal -> blocked first pass.
Final refusal after Accepted exchange -> blocked.
Final refusal of Already FinallyRefused exchange -> blocked.
Request MarkAgreementExchangeFailed from non-Approved status -> blocked.
```

## 6. Validation / Scenario-Local Guardrails

DTO/request-shape validation:

```text
Reason field/body is optional.
Blank/whitespace-only reason is invalid when provided.
Reason max length follows FinalRefusalReason value object.
```

Domain validation/invariants:

```text
Employee capability.
Exchange active status.
Not Accepted.
Not FinallyRefused.
Related request can be marked AgreementExchangeFailed.
No new proposal version is created.
```

## 7. Behavior Items

```text
L2-AGR-FINAL-001 — Employee can final-refuse exchange in AwaitingClientConfirmation or AwaitingEmployeeResponse.
L2-AGR-FINAL-002 — Final refusal marks exchange FinallyRefused and records employee/time/reason fields.
L2-AGR-FINAL-003 — Final refusal does not create a new proposal version.
L2-REQ-AGR-FAIL-001 — Application service marks approved request AgreementExchangeFailed after exchange final refusal.
L2-AGR-BOUNDARY-001 — Exchange and Request stay separate aggregates; application service orchestrates both.
L2-AGR-FINAL-REASON-001 — Final refusal reason is optional; blank provided reason is invalid.
```
