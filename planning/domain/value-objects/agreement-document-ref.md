# Domain Value Object Draft — AgreementDocumentRef

Status: draft / extracted with AgreementProposalExchange pilot  
Scope: metadata reference to an accepted/stored agreement proposal document

## 1. Purpose

`AgreementDocumentRef` protects the value integrity of a proposal document reference. It is not the document bytes, not a file upload service and not a storage adapter.

## 2. Source Inputs

Scenario / behavior sources:
- `AGR-VI-001`
- `AGR-IBS-002`
- `L2-AGR-DOC-001`
- `L2-AGR-DOC-002`
- `SC-13D-DATA-01` attachment DATA

Existing domain sources checked in archive:
- `planning/tables/domain-drafts/domain-draft-02.md`
- `Domain.EnergyManagement/AgreementProposals/AgreementDocumentRef.cs`

Not checked:
- Full source/version/cascade metadata.

## 3. Used By

- `planning/domain/aggregates/agreement-proposal-exchange.md`
- `AgreementProposal.Document` child state

## 4. Shape / Fields

Fields:
- `StorageKey`
- `OriginalFileName`
- `ContentType`
- `SizeBytes`

Optional fields:
- none in current direction.

Forbidden fields:
- file bytes;
- storage adapter/service object;
- upload stream.

## 5. Invariants

| Invariant | Source | Failure/error |
|---|---|---|
| Storage key is required | Domain Draft 02 / implementation | `AgreementDocumentStorageKeyIsRequired` |
| Original file name is required | Domain Draft 02 / implementation | `AgreementDocumentFileNameIsRequired` |
| Content type is required | Domain Draft 02 / implementation | `AgreementDocumentContentTypeIsRequired` |
| Size must be positive | Domain Draft 02 / implementation | `AgreementDocumentSizeIsRequired` |
| Proposal cannot be created without document ref | `AGR-IBS-002`, `L2-AGR-DOC-001` | proposal creation fails/no-write |

## 6. Creation / Normalization Rules

Creation:
- application/file layer receives uploaded or previously accepted document metadata;
- domain creates `AgreementDocumentRef` from metadata only.

Normalization:
- trim string fields.

Rejected values:
- blank storage key;
- blank original file name;
- blank content type;
- non-positive size.

## 7. Equality Rule

Equality is based on:
- `StorageKey`
- `OriginalFileName`
- `ContentType`
- `SizeBytes`

Identity is not:
- DB id;
- file stream identity.

## 8. Validation Boundary

Belongs in value object:
- required metadata fields;
- positive size;
- metadata trimming.

Belongs in DTO/input validation:
- multipart/form shape;
- allowed upload transport constraints.

Belongs in application/infrastructure:
- storing bytes;
- generating storage key;
- deleting/downloading file.

## 9. Persistence / Serialization Notes

- Can be persisted as owned/value object data inside proposal child entity.
- Should serialize as metadata reference, not file contents.

## 10. Invalid Examples

| Invalid value/state | Why invalid | Source |
|---|---|---|
| blank `StorageKey` | no persisted document reference | implementation/domain draft |
| blank `OriginalFileName` | missing user-visible/reference metadata | implementation/domain draft |
| blank `ContentType` | missing document metadata | implementation/domain draft |
| `SizeBytes <= 0` | invalid document metadata | implementation/domain draft |

## 11. Questions / Decisions

Open:
- exact allowed content types/sizes are likely API/application concerns, not this value object.

Accepted:
- this value object is metadata-only;
- it is not bytes/storage adapter.

## 12. Source Delta / Change Log

- Extracted from `planning/tables/domain-drafts/domain-draft-02.md` and the current AgreementProposal implementation sources in the archive.
