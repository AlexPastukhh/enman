# Domain Drafts Index

Status: current folder for gradual domain discovery drafts

## 1. Purpose

This folder stores iterative domain drafts.

A domain draft is a complete snapshot of current domain understanding.

A domain draft is not a competing alternative design.

Each next draft should refine the same domain direction and improve:

```text
- class/aggregate boundaries;
- owned state;
- methods/commands;
- state/condition handling;
- impossible state prevention;
- value integrity coverage;
- use-case coordination decisions;
- scenario behavior coverage;
- open question reduction.
```

## 2. Inputs

Each draft must use:

```text
planning/diagrams/scenario-text-specs/
planning/diagrams/scenario-data/
planning/diagrams/scenario-text-specs/scenario-server-domain-validation-addendum.md
planning/tables/pre-domain-variants-input.md
planning/domain-draft-generation-guide.md
```

## 3. Expected files

```text
domain-draft-01.md
domain-draft-02.md
domain-draft-03.md
...
final-domain-model-candidate.md
```

Do not use names like:

```text
applicant-party-centric-balanced-boundaries.md
```

for current active drafts unless explicitly archived as old exploratory notes.

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
12. Open Questions / Gaps For Next Draft
13. What Changed Since Previous Draft
```

Early drafts may be rough and partial.

Later drafts should cover more baseline items and have fewer questions.

## 5. Coverage rule

Each draft must reference stable item IDs from:

```text
planning/tables/pre-domain-variants-input.md
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

## 6. Current next file

```text
planning/tables/domain-drafts/domain-draft-01.md
```
