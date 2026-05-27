# Domain Value Object Draft — ProposalComment

Status: draft / extracted with AgreementProposalExchange pilot  
Scope: optional proposal text/comment attached to a proposal version

## 1. Purpose

`ProposalComment` protects non-empty bounded text when a proposal comment is provided. Missing comment is represented by `null` on the proposal.

## 2. Source Inputs

Scenario / behavior sources:
- `AGR-VI-002`
- `L2-AGR-COMMENT-001`
- `SC-13D-DATA-01` input DATA

Existing domain sources checked in archive:
- `planning/tables/domain-drafts/domain-draft-02.md`
- `Domain.EnergyManagement/AgreementProposals/ProposalComment.cs`

Not checked:
- Full source/version/cascade metadata.

## 3. Used By

- `planning/domain/aggregates/agreement-proposal-exchange.md`
- `AgreementProposal.Comment?`

## 4. Shape / Fields

Fields:
- `Value`

Optional fields:
- none inside the value object.

Forbidden fields:
- request details;
- document metadata;
- sender identity.

## 5. Invariants

| Invariant | Source | Failure/error |
|---|---|---|
| Comment is non-empty when present | `L2-AGR-COMMENT-001` / implementation | `ProposalCommentIsRequired` |
| Comment max length is 2000 | implementation / domain draft | `ProposalCommentIsTooLong` |

## 6. Creation / Normalization Rules

Creation:
- create only when caller has non-blank text.

Normalization:
- trim text.

Rejected values:
- blank value;
- value longer than max length.

## 7. Equality Rule

Equality is based on:
- normalized `Value`

Identity is not:
- proposal id;
- sender id.

## 8. Validation Boundary

Belongs in value object:
- non-empty when present;
- max length;
- trimming.

Belongs in DTO/input validation:
- raw form field presence/shape.

Belongs in aggregate/application:
- whether a missing comment is acceptable for a given command/scenario.

## 9. Persistence / Serialization Notes

- Persist as optional owned/string value on proposal version/child.
- `null` means no comment; empty string should be normalized at application boundary and not stored as a value object.

## 10. Invalid Examples

| Invalid value/state | Why invalid | Source |
|---|---|---|
| blank value passed to `ProposalComment.Create` | value object represents present comment; present comment must be non-empty | implementation/domain draft |
| length > 2000 | exceeds domain max length | implementation/domain draft |

## 11. Questions / Decisions

Open:
- `Q-SC-13D-002` still asks whether proposal text details/comment are required or optional.

Accepted:
- current domain direction treats comment as optional;
- if present, it must be non-empty and max-length constrained.

## 12. Source Delta / Change Log

- Extracted from `planning/tables/domain-drafts/domain-draft-02.md` and the current AgreementProposal implementation sources in the archive.
