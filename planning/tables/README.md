# Planning Tables Index

Status: current planning tables navigation

## 1. Current stage

The current saved domain draft is:

```text
planning/tables/domain-drafts/domain-draft-01.md
```

The current L1 implementation planning entry points are:

```text
planning/l1-domain-implementation-cut.md
planning/l1-domain-testing-rules.md
```

The current slice and ADR planning entry points are:

```text
planning/slices/README.md
planning/slices/l1-slice-drafting-guide.md
planning/slices/l1-slice-boundary-draft-01.md
planning/adr/README.md
planning/adr/adr-candidates.md
```

## 2. Current read order

```text
1. planning/diagrams/scenario-text-specs/
2. planning/diagrams/scenario-data/
3. planning/tables/pre-domain-variants-input.md
4. planning/tables/scenario-behavior-baseline-account-activation-addendum.md
5. planning/tables/domain-drafts/domain-draft-01.md
6. planning/l1-domain-implementation-cut.md
7. planning/l1-domain-testing-rules.md
8. planning/slices/README.md
9. planning/slices/l1-slice-drafting-guide.md
10. planning/slices/l1-slice-boundary-draft-01.md
11. planning/adr/README.md
12. planning/adr/adr-candidates.md
```

## 3. Slice planning

The L1 domain cut is not a full scenario slice.

It is a domain foundation.

Current slice planning is split into:

```text
General boundary draft:
planning/slices/l1-slice-boundary-draft-01.md

Per-slice implementation files:
planning/slices/SL-ACC-001-register-client-account.md
planning/slices/SL-APPL-001-create-individual-applicant-party.md
planning/slices/SL-REQ-001-create-connection-request.md
planning/slices/SL-REVIEW-001-approve-request-and-verify-applicant.md
planning/slices/SL-REVIEW-002-reject-request.md
```

General boundary file identifies and justifies slices.

Per-slice files plan implementation flow, UI blueprint, tests and checklist.

## 4. Testing rules

The current testing guide is:

```text
planning/l1-domain-testing-rules.md
```

## 5. Current next step

```text
Use planning/slices/l1-slice-boundary-draft-01.md to confirm current L1 slice boundaries.
Then use/refine the relevant per-slice file before implementing the next slice.
```

Current urgent slice issue:

```text
SL-REQ-001 Submitted vs InReview integration/API expectation conflict.
```

## 6. Avoid

Do not add or use old intermediate files such as:

```text
domain-discovery-core.md
aggregate-boundary-candidates-core.md
domain-model-options-core.md
scenario-domain-responsibility-core.md
scenario-domain-design-input-core.md
scenario-domain-design-input-gate.md
```
