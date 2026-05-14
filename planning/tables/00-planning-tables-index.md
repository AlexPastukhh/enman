# Planning Tables Index

Status: current table index for gradual domain discovery and L1 implementation readiness

## 1. Current Active Files

| Order | File | Status | Purpose |
|---:|---|---|---|
| 1 | `pre-domain-variants-input.md` | current pre-domain coverage baseline | Collect scenario-derived behavior items with stable IDs. |
| 2 | `scenario-behavior-baseline-account-activation-addendum.md` | active baseline addendum | Add account activation / protected-use-case coverage items and questions. |
| 3 | `domain-drafts/README.md` | current draft folder index | Explains where iterative domain drafts are stored. |
| 4 | `domain-drafts/domain-draft-01.md` | current first saved domain draft | ApplicantParty-centric domain model snapshot with coverage against scenario behavior baseline. |
| 5 | `../l1-domain-implementation-cut.md` | current implementation-readiness guide | Defines the narrow first L1 domain implementation cut and file strategy. |

## 2. Current Read Order

```text
1. planning/diagrams/scenario-text-specs/
2. planning/diagrams/scenario-data/
3. planning/diagrams/scenario-text-specs/scenario-server-domain-validation-addendum.md
4. planning/diagrams/scenario-text-specs/scenario-account-activation-security-addendum.md
5. planning/tables/pre-domain-variants-input.md
6. planning/tables/scenario-behavior-baseline-account-activation-addendum.md
7. planning/domain-draft-generation-guide.md
8. planning/tables/domain-drafts/README.md
9. planning/tables/domain-drafts/domain-draft-01.md
10. planning/l1-domain-implementation-cut.md
```

## 3. Current Next Step

```text
Review / refine planning/tables/domain-drafts/domain-draft-01.md for L1 implementation readiness.
```

Then:

```text
define/confirm L1 implementation cut
-> implement domain classes + unit tests
-> run tests
-> split/normalize files after each green mini-cut
-> then continue with application/API/persistence slices
```

## 4. Replacement File Generation

Manual replacement-package generation is documented in:

```text
planning/replacement-file-generation-guide.md
```

## 5. Why No More Intermediate Tables Now

Do not add more intermediate files such as:

```text
domain-discovery-core.md
aggregate-boundary-candidates-core.md
domain-model-options-core.md
scenario-domain-responsibility-core.md
scenario-domain-design-input-core.md
scenario-domain-design-input-gate.md
```

The current pre-domain control files are:

```text
pre-domain-variants-input.md
scenario-behavior-baseline-account-activation-addendum.md
```

The current draft folder is:

```text
domain-drafts/
```

The current implementation-readiness file is:

```text
planning/l1-domain-implementation-cut.md
```

## 6. Superseded / Historical

If old workflow files exist historically, treat them as stale/superseded notes, not active workflow.
