# Domain Value Objects Index

Status: current value object draft folder index  
Scope: reusable/non-trivial value objects and value-integrity concepts

## 1. Purpose

This folder contains value object drafts.

Create a value object draft when the candidate is:

```text
- reused by multiple aggregates;
- backed by scenario behavior item value-integrity evidence;
- non-trivial enough to need invariants/normalization/equality rules;
- important enough to review separately from one aggregate draft.
```

Do not create a value object file for every primitive wrapper by default.

## 2. Current Drafts

| File | Value object | Status | Used by |
|---|---|---|---|
| `agreement-proposal-version.md` | `AgreementProposalVersion` | draft / extracted pilot | `AgreementProposalExchange` |
| `agreement-proposal-author.md` | `AgreementProposalAuthor` | draft / extracted pilot | `AgreementProposalExchange` |
| `agreement-document-ref.md` | `AgreementDocumentRef` | draft / extracted pilot | `AgreementProposalExchange` |
| `proposal-comment.md` | `ProposalComment` | draft / extracted pilot | `AgreementProposalExchange` |
| `final-refusal-reason.md` | `FinalRefusalReason` | draft / extracted pilot | `AgreementProposalExchange` |

## 3. Drafting

Use:

```text
planning/domain/value-object-drafting-workflow.md
planning/domain/value-object-draft-template.md
planning/domain/scenario-to-aggregate-map.md
```

## 4. Relationship To Aggregates

Aggregate drafts should reference value object files in their `Value Objects Used` section.

Value object files own actual value shape, invariants, normalization, equality and persistence/serialization notes.
