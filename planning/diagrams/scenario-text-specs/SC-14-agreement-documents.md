# SC-14 — Agreement Documents

Status: L2 scenario draft / derived from Domain Draft 02  
Source: `planning/tables/domain-drafts/domain-draft-02.md`

## 1. Purpose

Agreement proposal versions reference accepted/stored agreement document metadata through `AgreementDocumentRef`.

## 2. Scenario Direction

```text
Actor provides or selects agreement document for proposal version
        ↓
Application/infrastructure stores or resolves file metadata
        ↓
Application creates AgreementDocumentRef
        ↓
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
read/delete storage workflow.
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
Empty string means no comment.
```

## 5. Behavior Items

```text
L2-AGR-DOC-001 — Agreement proposal version requires AgreementDocumentRef.
L2-AGR-DOC-002 — AgreementDocumentRef stores metadata reference only, not bytes/storage adapter.
L2-AGR-COMMENT-001 — ProposalComment is optional; non-empty if provided.
```
