# Documentation Reusable Candidate Notice

Status: temporary non-canonical migration workspace  
Scope: guardrail for `planning/documentation-reusable-candidate/`

## 1. Purpose

This folder is a baseline copy of the active documentation layer:

```text
planning/documentation/
```

It exists so the documentation layer can be reviewed, cleaned and genericized without breaking the active Enman/project documentation layer.

## 2. Source Of Truth Boundary

This folder is **not** the active documentation layer.

Active documentation remains:

```text
planning/documentation/
```

Until an explicit migration/switch batch is approved, ordinary project documentation updates must continue to use the active documentation layer.

## 3. Allowed Use

Use this candidate folder only for:

```text
- portability review;
- reusable-layer migration work;
- responsibility-zone classification;
- genericization experiments;
- future principles/profile/adapter split work.
```

## 4. Do Not

```text
- Do not treat this folder as the current documentation source of truth.
- Do not make ordinary Enman/project documentation updates here.
- Do not update both active documentation and this candidate for routine docs work.
- Do not delete or move project-specific material from the active docs layer because it exists here.
- Do not switch canonical references to this folder until a later approved migration batch.
```

## 5. Related Active Owners

```text
planning/documentation/documentation-layer-portability-migration-plan.md
planning/documentation/documentation-responsibility-zone-review-workflow.md
planning/documentation/planning-docs-architecture-principles.md
planning/documentation/documentation-responsibility-map.md
```

## 6. Candidate Review Artifacts

Current candidate review artifact:

```text
planning/documentation-reusable-candidate/PRINCIPLES-RESPONSIBILITY-CLASSIFICATION.md
```

Use it before splitting or rewriting the candidate principles file.

## 7. Candidate Split Outputs

F4 candidate split/genericization introduced:

```text
planning/documentation-reusable-candidate/planning-docs-architecture-principles.md
planning/documentation-reusable-candidate/scenario-domain-slice-docs-profile.md
planning/documentation-reusable-candidate/enman-docs-adapter.md
planning/documentation-reusable-candidate/PORTABILITY-FOLLOWUPS.md
```

Meaning:

```text
- planning-docs-architecture-principles.md = genericized candidate reusable principles;
- scenario-domain-slice-docs-profile.md = specialized reusable profile for scenario-driven app/product projects;
- enman-docs-adapter.md = concrete Enman/current-project mapping extracted from candidate principles;
- PORTABILITY-FOLLOWUPS.md = deferred workflow/field-kit/example/naming work that should not be dumped into the adapter.
```

These outputs are candidate-only and do not switch canonical documentation ownership.
