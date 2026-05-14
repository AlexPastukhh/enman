# Domain Drafts Index

Status: current folder for gradual domain discovery drafts

## 1. Purpose

This folder stores iterative domain drafts.

A domain draft is a complete snapshot of current domain understanding.

A domain draft is not a competing alternative design.

Each next draft should refine the same domain direction and improve coverage and reduce open questions.

## 2. Inputs

Each draft must use:

```text
planning/diagrams/scenario-text-specs/
planning/diagrams/scenario-data/
planning/diagrams/scenario-text-specs/scenario-server-domain-validation-addendum.md
planning/diagrams/scenario-text-specs/scenario-account-activation-security-addendum.md
planning/tables/pre-domain-variants-input.md
planning/tables/scenario-behavior-baseline-account-activation-addendum.md
planning/domain-draft-generation-guide.md
```

## 3. Current draft files

| File | Status | Purpose |
|---|---|---|
| `domain-draft-01.md` | current first saved draft | ApplicantParty-centric domain model snapshot with coverage against scenario behavior baseline. |

Expected future files:

```text
domain-draft-02.md
domain-draft-03.md
...
final-domain-model-candidate.md
```

## 4. Required draft sections

Each draft should include:

```text
1. Draft Goal
2. Source Inputs
3. Current Domain Direction
4. Aggregate / Class Candidates
5. Class-By-Class Model
6. Class / Aggregate State Machines
7. Impossible States Covered By Current Model
8. Value Objects / Value Integrity Coverage
9. Use-Case Coordination Decisions
10. Coverage Against Scenario Behavior Baseline
11. Cross-Layer Placement Notes
12. Scenario Questions / Gaps For Next Draft
13. What Changed Since Previous Draft
```

## 5. Coverage rule

Each draft must reference stable item IDs from:

```text
planning/tables/pre-domain-variants-input.md
planning/tables/scenario-behavior-baseline-account-activation-addendum.md
```

Coverage statuses:

```text
Missing
Partial
Covered
Resolved outside current domain model
Deferred
Question
```

Coverage tables should include both:

```text
- stable item ID;
- readable requirement / invariant explanation.
```

## 6. Account activation coverage

Current direction:

```text
- current core registration creates Active account;
- protected client/employee functionality requires activated account;
- current implementation: application service guard + Account.EnsureActivated;
- future implementation: AccountActivated authorization policy, possibly backed by account_activated claim.
```

Business aggregates such as Request, AgreementProposal and ApplicantParty should not duplicate account activation checks internally unless the draft explicitly justifies that decision.

## 7. Current next step

```text
Review / refine planning/tables/domain-drafts/domain-draft-01.md for L1 implementation readiness.
```

Then:

```text
planning/l1-domain-implementation-cut.md
-> L1 domain classes + unit tests
-> coverage review
-> domain-draft-02.md if more domain refinement is needed
-> final-domain-model-candidate.md later
```
