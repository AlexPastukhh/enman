# Domain Value Object Draft — AgreementProposalAuthor

Status: draft / extracted with AgreementProposalExchange pilot  
Scope: proposal author as sender type plus sender id

## 1. Purpose

`AgreementProposalAuthor` protects unambiguous proposal authorship without EmployeeRef, ClientRef or generic AggregateId.

## 2. Source Inputs

Scenario / behavior sources:
- `AGR-IBS-001`
- `L2-AGR-AUTHOR-001`
- L2 naming guardrails

Existing domain sources checked in archive:
- `planning/tables/domain-drafts/domain-draft-02.md`
- `Domain.EnergyManagement/AgreementProposals/AgreementProposalAuthor.cs`

Not checked:
- Full source/version/cascade metadata.

## 3. Used By

- `planning/domain/aggregates/agreement-proposal-exchange.md`
- `AgreementProposal.Author`

## 4. Shape / Fields

Fields:
- `Sender: AgreementProposalSender`
- `SenderId: long`

Optional fields:
- none.

Forbidden fields:
- `EmployeeRef`;
- `ClientRef`;
- generic `AggregateId`.

## 5. Invariants

| Invariant | Source | Failure/error |
|---|---|---|
| Sender type is explicit | `L2-AGR-AUTHOR-001` | factory requires Employee or Client path |
| Sender id is positive | implementation / domain draft | constructor rejects non-positive id |
| Proposal cannot exist without author | `AGR-IBS-001` | proposal factory requires author |

## 6. Creation / Normalization Rules

Creation:
- `AgreementProposalAuthor.Employee(employee)` from Employee domain actor.
- `AgreementProposalAuthor.Client(client)` from ClientAccount.

Normalization:
- none.

Rejected values:
- missing actor;
- non-positive sender id.

## 7. Equality Rule

Equality is based on:
- `Sender`
- `SenderId`

Identity is not:
- proposal id;
- account profile object reference.

## 8. Validation Boundary

Belongs in value object:
- sender type + positive sender id.

Belongs in aggregate:
- ensuring Employee can only respond to Client proposal and Client can only respond to Employee proposal.

Belongs in application/auth:
- resolving current Employee/ClientAccount from authenticated account.

## 9. Persistence / Serialization Notes

- Persist with proposal child entity.
- Use scalar sender id, not domain navigation requirement.

## 10. Invalid Examples

| Invalid value/state | Why invalid | Source |
|---|---|---|
| missing Employee/Client | cannot derive sender id | implementation/domain draft |
| `SenderId <= 0` | invalid identity reference | implementation/domain draft |
| generic `AggregateId` | ambiguous author model | L2 naming guardrails |

## 11. Questions / Decisions

Open:
- none for first extraction.

Accepted:
- author uses `Sender + SenderId`;
- do not use `EmployeeRef`, `ClientRef` or `AggregateId`.

## 12. Source Delta / Change Log

- Extracted from `planning/tables/domain-drafts/domain-draft-02.md` and the current AgreementProposal implementation sources in the archive.
