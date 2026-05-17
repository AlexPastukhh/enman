# SL-AGR-EXCH-002 — Send Agreement Counter-Proposal Version

Status: planned slice boundary / full implementation draft pending  
Package: `[AgreementProposalExchange]`  
Slice type: backend/API command slice with two actor branches + future client command sidecars  
Primary purpose: current actor sends the next proposal version inside an existing exchange

## 1. Scope Boundary

This slice implements both directions of counter-proposal exchange.

```text
Active exchange
  -> current actor sends agreement document + optional comment
  -> active proposal is superseded
  -> next proposal version is created
  -> turn switches to the other side
```

## 2. Actor Branches

Client branch:

```text
AwaitingClientConfirmation
active proposal author = Employee
  -> ClientSendOwnVersion(...)
  -> AwaitingEmployeeResponse
```

Employee branch:

```text
AwaitingEmployeeResponse
active proposal author = Client
  -> EmployeeSendNewVersion(...)
  -> AwaitingClientConfirmation
```

## 3. Endpoint Direction

Actor boundaries may use separate endpoints:

```http
POST /api/l1/requests/{requestId}/agreement-exchange/proposals
POST /api/employee/requests/{requestId}/agreement-exchange/proposals
```

The implementation slice remains one because the lifecycle pattern is the same.

## 4. Split Guardrail

```text
Do not split client send / employee send unless UI, permissions, document handling or validation diverge materially.
```

## 5. Out of Scope

```text
- initial exchange creation/version 1 -> SL-AGR-EXCH-001;
- exchange read model -> SL-AGR-EXCH-003;
- accept active proposal -> SL-AGR-EXCH-004;
- final refusal -> SL-AGR-EXCH-005;
- document bytes/storage adapter -> future document/storage slice.
```
