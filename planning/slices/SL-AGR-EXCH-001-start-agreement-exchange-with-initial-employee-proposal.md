# SL-AGR-EXCH-001 — Start Agreement Exchange With Initial Employee Proposal

Status: planned slice boundary / full implementation draft pending  
Package: `[AgreementProposalExchange]`  
Slice type: backend/API command slice + future client command sidecar  
Primary purpose: Employee creates AgreementProposalExchange by sending the initial proposal document for an Approved request

## 1. Scope Boundary

```text
Approved request
  -> Employee sends initial agreement document + optional comment
  -> AgreementProposalExchange is created
  -> proposal version 1 is created by Employee
  -> exchange status = AwaitingClientConfirmation
```

This slice is separate because it creates the exchange aggregate and first proposal version.

## 2. Preconditions

```text
- request exists;
- request is Approved;
- current actor is Employee;
- request has no existing AgreementProposalExchange;
- agreement document reference is valid metadata reference;
- optional ProposalComment is valid if present.
```

## 3. Domain Direction

Target domain entry:

```csharp
AgreementProposalExchange.StartByEmployee(
    approvedRequest,
    document,
    comment,
    employee,
    startedAt)
```

There is no standalone “start exchange without document” behavior in the current domain direction.

## 4. Out of Scope

```text
- client/employee counter-proposal after exchange exists -> SL-AGR-EXCH-002;
- exchange read model -> SL-AGR-EXCH-003;
- accept active proposal -> SL-AGR-EXCH-004;
- final refusal -> SL-AGR-EXCH-005;
- document bytes/storage adapter -> future document/storage slice;
- Request approval -> SL-EMP-REQ-004.
```

## 5. Notes For Full Draft

```text
- success response should not be used as long-lived read source if exchange read endpoint exists;
- API/client must not choose proposal version;
- first version is always domain version 1;
- command must not create duplicate exchanges for the same approved request.
```
