# L2 Agreement Exchange Domain Invariants / Actor Access

Status: current planning decision / applies to `SL-AGR-EXCH-*` drafts  
Scope: AgreementProposalExchange aggregate invariants, actor resolution, application-service orchestration and read access filtering

## 1. Core Decision

`AgreementProposalExchange` should own business transition invariants when it has the required data.

Target aggregate state:

```text
AgreementProposalExchange stores:
- RequestId;
- ClientAccountId;
- Status;
- ActiveProposalVersion;
- Proposals.
```

`ClientAccountId` is required because it identifies the client-side participant of the exchange.

Use the explicit name:

```csharp
public long ClientAccountId { get; private set; }
```

Do not use a vague `AccountId` for this purpose.

## 2. Client Participant Ownership

Client-side agreement actions must verify:

```text
current client.Id == AgreementProposalExchange.ClientAccountId
```

This applies to client actions such as:

```text
ClientSendOwnVersion(...)
ClientAcceptActiveProposal(...)
future client-side refusal/response actions, if added
```

Suggested domain error:

```text
Errors.L1Domain.ClientCannotActOnThisAgreementExchange
```

or another project-consistent name with the same meaning.

## 3. Employee Access Model

Do not introduce `ResponsibleEmployeeId` as a first-pass authorization guard.

Target first-pass rule:

```text
Any active Employee can service the exchange.
```

Employee authorship is still tracked per proposal version:

```text
AgreementProposalAuthor:
  Sender = Employee
  SenderId = employee.Id
```

Optional future audit is allowed, but it is not an access guard:

```csharp
public long? StartedByEmployeeId { get; private set; }
```

If added later, it means audit/history, not exclusive ownership.

## 4. Turn / Lifecycle Invariants

The domain aggregate should protect proposal exchange turn/lifecycle rules.

Client actions:

```text
- current client is the exchange client participant;
- current status expects client action;
- active proposal was authored by Employee;
- exchange is not accepted;
- exchange is not finally refused.
```

Employee actions:

```text
- employee is active / can perform agreement proposal action;
- current status expects Employee action;
- active proposal was authored by Client;
- exchange is not accepted;
- exchange is not finally refused.
```

Application/controller code must not duplicate these rules as scattered `if` checks except for coarse access and data loading.

## 5. StartByEmployee ClientAccountId Initialization

When `SL-AGR-EXCH-001` starts a new exchange, `ClientAccountId` must be populated from the approved request owner.

Preferred direction:

```csharp
ClientAccountId = approvedRequest.ClientAccountId;
```

If current `ConnectionRequest` does not expose `ClientAccountId` directly, use a domain method or temporary adapter:

```csharp
approvedRequest.GetOwnerClientAccountId()
```

or, only as a compatibility bridge:

```csharp
approvedRequest.ApplicantParty.ClientAccountId
```

The target domain should have a clear way to ask the request for its owner client account id.

## 6. Handler / Controller Responsibility

Controllers/handlers should stay thin.

They own:

```text
- authenticated role boundary;
- current actor id from claims/session;
- DTO shape validation trigger;
- request/command construction;
- success response;
- ProblemDetails mapping from application/domain errors.
```

They do not own:

```text
- whose turn it is;
- active proposal author checks;
- accepted/finally-refused lifecycle checks;
- client participant ownership logic beyond actor resolution.
```

## 7. Application Service Responsibility

Agreement exchange application service is an orchestration layer.

It owns:

```text
- load ClientAccount / Employee actor;
- load Request / AgreementProposalExchange;
- create AgreementDocumentRef;
- create optional ProposalComment;
- branch by actor;
- call actor-specific domain method;
- persist changes.
```

Branching by actor inside the application service is expected:

```text
Client   -> exchange.ClientSendOwnVersion(...)
Employee -> exchange.EmployeeSendNewVersion(...)
```

This avoids spreading actor/lifecycle branching across controllers and handlers.

## 8. Shared Endpoint / Actor Resolution Direction

Use shared endpoints/read models where practical, while preserving actor boundary and access.

For shared command endpoint shapes, resolve actor from authenticated session:

```text
role Client
  -> AgreementExchangeActor.Client(clientAccountId)

role Employee
  -> AgreementExchangeActor.Employee(employeeId)
```

Then delegate:

```csharp
await agreementExchangeService.SendProposalVersionAsync(
    actor,
    requestId,
    document,
    comment,
    cancellationToken);
```

The service calls the correct actor-specific domain method.

## 9. Read Access Direction

Read endpoints do not enforce transition invariants, but they do enforce access filters.

Client reads:

```text
return exchange only if exchange.ClientAccountId == current client account id
```

Employee reads:

```text
first pass: any active Employee can read/service review-relevant agreement exchanges
future: department/region/assignment filters may be added
```

This means read services may branch by actor/query filter even when the read model shape is shared.

## 10. Persistence Direction

Add `ClientAccountId` to the exchange table:

```text
L1AgreementProposalExchanges.ClientAccountId bigint NOT NULL
```

Recommended indexes:

```text
IX_L1AgreementProposalExchanges_ClientAccountId
UX_L1AgreementProposalExchanges_RequestId
```

`RequestId` uniqueness protects against duplicate exchanges for the same request, especially under race conditions.

## 11. Spec Wording

Use this wording in exchange slice drafts:

```text
Agreement exchange backend uses shared endpoints/read models where possible.

The controller resolves current actor from authenticated app session and delegates to AgreementExchangeApplicationService.

AgreementExchangeApplicationService branches by actor and calls actor-specific domain methods.

AgreementProposalExchange stores ClientAccountId so domain can enforce client participant ownership.

AgreementProposalExchange does not store ResponsibleEmployeeId as authorization guard in first pass, because any active Employee can service the exchange.

Proposal authors are tracked per AgreementProposal version.

FluentValidation validates DTO shape only.

Domain validates business transition and participant/lifecycle invariants.

Read services use actor-specific access filters; command services use actor-specific domain branches.
```
