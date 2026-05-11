# Diagram Domain And DB Brief

This file defines content rules for aggregate/domain diagrams and DB schema diagrams.

It is separate from user-facing scenario diagrams. For scenario/use-case logic, read `diagram-scenario-spec.md`.

It is also separate from visual draw.io rules. For palette, typography, card construction and edge rendering, read `diagram-generation-rules-with-example.md`.

## Diagram Families Covered Here

### Domain / Aggregate Boundary Diagrams

Purpose:

- show ownership boundaries;
- show aggregate roots;
- show child entities;
- show stored ID references;
- show cross-aggregate rules;
- show which rules belong in domain vs application coordination.

These diagrams may mention domain names such as:

- `ClientAccount`;
- `IndividualApplicantParty`;
- `ConnectionRequest`;
- `ContractDraft`;
- `RequestReview`;
- `EmailNotification`;
- `OutboxMessage`.

Domain diagrams are allowed to use domain terminology because they are not user-facing scenario diagrams.

### DB Schema Diagrams

Purpose:

- show storage shape only;
- show tables;
- show primary keys and foreign keys;
- show discriminator columns;
- show owned value-object column groups;
- show L1/L2/L3 schema additions.

DB diagrams should not show domain behavior.

### Technical CQRS / Application Flow Appendix

Purpose:

- show implementation flow when explicitly requested;
- separate command path from query path;
- show where `SaveChangesAsync` belongs;
- show that query side uses projections instead of aggregate hydration.

This is an appendix diagram family. Do not merge it into user-facing scenario diagrams.

## Source Documents

Use these source documents when creating this diagram family:

- `domain-model.md` for domain decisions;
- `api-plan.md` for L1 application/use-case boundary rules;
- `testing-strategy.md` for integration-test boundaries if a testing diagram is requested;
- `current-state.md` for current implementation status.

Do not convert `domain-model.md` directly into a use-case diagram. Domain terminology belongs in aggregate/domain and DB diagram families.

## Aggregate Boundary Diagram Rules

Show:

- aggregate root name;
- owned child entities;
- stored reference IDs;
- rules that protect aggregate boundaries;
- cross-aggregate references as ID-reference arrows;
- short notes explaining that DB FKs do not mean domain ownership.

Do not show:

- one aggregate root owning another aggregate root;
- one-side collection navigations as domain ownership;
- primitive ID collections as aggregate relationships;
- EF convenience mappings as domain behavior.

Current implemented L1 aggregate roots:

```text
ClientAccount
IndividualApplicantParty
ConnectionRequest
```

Current implemented L1 stored references:

```text
IndividualApplicantParty.ClientAccountId
ConnectionRequest.ApplicantPartyId
```

Important visual rule:

```text
Do not draw ClientAccount as owning ApplicantParties.
Do not draw ApplicantParty as owning Requests.
Draw separate aggregate boxes connected by ID reference arrows.
```

Recommended aggregate card sections:

```text
[root]
[owns]
[stored refs]
[rules]
[used by]
```

## DB Schema Diagram Rules

Show:

- tables;
- PK/FK columns;
- discriminator columns for TPH;
- persisted scalar columns;
- owned value-object column groups;
- level-specific additions.

Do not show:

- domain factory methods;
- domain behavior methods;
- controller/handler/repository flow;
- user-facing scenario steps.

Recommended DB card sections:

```text
[identity]
[L1 core]
[L2 additions]
[L3 additions]
[relations]
```

Current L1 storage shape:

```text
L1Accounts / Account TPH
L1ApplicantParties / ApplicantParty TPH
L1ClientRequests / ClientRequest TPH
```

Current L1 relationships:

```text
L1Accounts 1 -> many L1ApplicantParties
label: ClientAccountId

L1ApplicantParties 1 -> many L1ClientRequests
label: ApplicantPartyId
```

DB note:

```text
Storage FKs do not imply aggregate ownership.
Domain avoids one-side navigation collections between aggregate roots.
```

## Technical CQRS Appendix Rules

Use only when explicitly requested.

Recommended lanes:

```text
HTTP
Controller
Application / MediatR
Domain
Persistence
Database
```

Command path:

```text
HTTP POST
Controller maps DTO -> command
Command handler loads required aggregates
Domain factory/method executes
Repository Add/Update
SaveChangesAsync in handler
SQL INSERT/UPDATE
```

Query path:

```text
HTTP GET
Controller maps query params -> query
Query handler executes SQL/Dapper projection
Read DTO returned
No aggregate hydration
No SaveChangesAsync
```

Rules note:

```text
Controllers are thin HTTP boundaries.
Command handlers own normal commits.
Queries use read projections and do not hydrate aggregates.
```

## Boundary Between Scenario And Domain Diagrams

Scenario diagram wording:

```text
Client submits request
Outcome: request is stored and visible with Submitted status
```

Aggregate diagram wording:

```text
ConnectionRequest
[stored refs]
ApplicantPartyId
```

DB diagram wording:

```text
L1ClientRequests
PK Id
FK ApplicantPartyId
Status
Details
ObjectAddress_*
```

Keep those concerns separate.
