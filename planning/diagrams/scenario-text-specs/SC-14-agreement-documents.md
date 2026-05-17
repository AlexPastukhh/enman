# SC-14 вЂ” Agreement Documents

Status: L2 scenario draft / AgreementDocumentRef naming conflict guard synchronized  
Source: `planning/tables/domain-drafts/domain-draft-02.md`

## 1. Purpose

Agreement proposal versions reference accepted/stored agreement document metadata through `AgreementDocumentRef`.

Current SC-14 meaning is Agreement Documents / AgreementDocumentRef.

Older вЂњSC-14 Client Data VerificationвЂќ wording, if found in archived/security/validation addenda, is stale for current agreement-document diagrams and must be renamed, deferred or deprecated rather than reused as current SC-14.

## 2. Scenario Direction

```text
Actor provides or selects agreement document for proposal version
        в†“
Application/infrastructure stores or resolves file metadata
        в†“
Application creates AgreementDocumentRef
        в†“
AgreementProposal stores document reference
```

## 3. Domain Direction

`AgreementDocumentRef` is:

```text
reference to already accepted agreement/proposal document;
metadata value object;
required for AgreementProposal creation.
```

It is not:

```text
file bytes;
storage adapter;
file upload service;
read/delete storage workflow;
DocumentFileRef;
ProposalAttachment.
```

Fields:

```text
StorageKey / FileId
OriginalFileName
ContentType
SizeBytes
```

## 4. ProposalComment

```text
ProposalComment is optional.
If present, it must be non-empty and max-length constrained.
Empty string means no comment unless a concrete command intentionally treats blank as validation error.
```

## 5. Validation / Scenario-Local Guardrails

DTO/request-shape validation examples:

```text
storageKey required;
originalFileName required;
contentType required;
sizeBytes > 0;
comment max length if provided.
```

Domain validation/invariants:

```text
AgreementDocumentRef value object remains authoritative.
ProposalComment value object remains authoritative.
No binary file upload/storage adapter belongs to these agreement exchange command slices unless a document/storage slice explicitly adds it.
```

## 6. Behavior Items

```text
L2-AGR-DOC-001 вЂ” Agreement proposal version requires AgreementDocumentRef.
L2-AGR-DOC-002 вЂ” AgreementDocumentRef stores metadata reference only, not bytes/storage adapter.
L2-AGR-COMMENT-001 вЂ” ProposalComment is optional; non-empty if provided.
L2-AGR-DOC-NAMING-001 вЂ” Use AgreementDocumentRef, not DocumentFileRef or ProposalAttachment, in current L2 docs/diagrams.
```

## Diagram / Implementation Markers

These markers are for diagrams and diploma planning only. They do not replace current repo implementation evidence.

| Scenario element | Marker | Diagram / implementation meaning |
|---|---|---|
| `AgreementDocumentRef` metadata reference | `[DESIGNED]` | Accepted value object for proposal document metadata. |
| Proposal stores document metadata, not bytes | `[DESIGNED]` | Current domain/scenario direction. |
| ProposalComment optional value object | `[DESIGNED]` | Optional comment; non-empty/max-length when present. |
| Binary upload/storage adapter | `[DEFERRED]` | Future infrastructure/document slice, not current scenario behavior. |
| Old `SC-14 Client Data Verification` meaning | `[DEFERRED]` | Not current `SC-14`; current `SC-14` means Agreement Documents. |

