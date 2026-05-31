# Domain Value Object Draft — FinalRefusalReason

Status: draft / extracted with AgreementProposalExchange pilot  
Doc version: v0.1.0  
Scope: optional explanation for Employee final refusal of agreement exchange

## 1. Purpose

`FinalRefusalReason` protects bounded non-empty text when a final refusal reason is provided. The final refusal itself is exchange state, not a separate entity.

## 2. Source Inputs

Scenario / behavior sources:
- `L2-AGR-FINAL-001`
- `L2-AGR-FINAL-002`
- `L2-AGR-FINAL-003`
- L2 domain direction clarification

Existing domain sources checked in archive:
- `planning/tables/domain-drafts/domain-draft-02.md`
- `Domain.EnergyManagement/AgreementProposals/FinalRefusalReason.cs`

Not checked:
- Full source/version/cascade metadata.

## 3. Used By

- `planning/domain/aggregates/agreement-proposal-exchange.md`
- `AgreementProposalExchange.FinalRefusalReason?`

## 4. Shape / Fields

Fields:
- `Value`

Optional fields:
- none inside the value object.

Forbidden fields:
- refusing employee id;
- refused timestamp;
- proposal version id.

## 5. Invariants

| Invariant | Source | Failure/error |
|---|---|---|
| Reason is non-empty when present | implementation / domain draft | `FinalRefusalReasonIsRequired` |
| Reason max length is 2000 | implementation / domain draft | `FinalRefusalReasonIsTooLong` |

## 6. Creation / Normalization Rules

Creation:
- create only when caller has non-blank final refusal reason.

Normalization:
- trim text.

Rejected values:
- blank value;
- value longer than max length.

## 7. Equality Rule

Equality is based on:
- normalized `Value`

Identity is not:
- final refusal event id;
- employee id.

## 8. Validation Boundary

Belongs in value object:
- reason text integrity.

Belongs in aggregate:
- whether final refusal is allowed;
- who refused;
- when refusal happened;
- status transition to `FinallyRefused`.

Belongs in application:
- orchestrating Request `AgreementExchangeFailed` after exchange final refusal.

## 9. Persistence / Serialization Notes

- Persist as optional value object on `AgreementProposalExchange` final-refusal state.
- Do not create a separate `AgreementFinalRefusal` entity/class for current target direction.

## 10. Invalid Examples

| Invalid value/state | Why invalid | Source |
|---|---|---|
| blank reason passed to factory | present reason must be non-empty | implementation/domain draft |
| length > 2000 | exceeds domain max length | implementation/domain draft |

## 11. Questions / Decisions

Open:
- whether UI/API requires reason is not owned by this value object.

Accepted:
- reason is optional on aggregate state;
- final refusal does not create proposal version;
- no separate final refusal entity/class.

## 12. Source Delta / Change Log

- Extracted from `planning/tables/domain-drafts/domain-draft-02.md` and the current AgreementProposal implementation sources in the archive.
