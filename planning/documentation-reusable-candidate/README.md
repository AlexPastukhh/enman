# Documentation Reusable Candidate Index

Status: temporary non-canonical reusable candidate index  
Scope: candidate navigation, read order and owner links for reusable documentation-layer migration

## 1. Purpose

This folder is a temporary reusable candidate workspace for the documentation layer.

It is used to review, split, genericize and prepare reusable documentation architecture without changing the active Enman documentation source of truth.

Active documentation remains under:

```text
planning/documentation/
```

until a later explicit migration/switch batch approves otherwise.

## 2. Candidate Source-Of-Truth Boundary

This README is candidate navigation only.

It does not make the candidate folder canonical.

Use this folder for:

```text
- portability review;
- reusable-layer migration work;
- responsibility-zone classification;
- candidate principles/profile/adapter cleanup;
- candidate field-kit/project-instance review;
- examples and follow-up tracking for future migration batches.
```

Do not use this folder for ordinary active Enman documentation updates.

## 3. Candidate Read Order

For candidate portability/split work, read:

```text
1. planning/documentation-reusable-candidate/CANDIDATE-NOTICE.md
2. planning/documentation-reusable-candidate/PRINCIPLES-RESPONSIBILITY-CLASSIFICATION.md
3. planning/documentation-reusable-candidate/planning-docs-architecture-principles.md
4. planning/documentation-reusable-candidate/scenario-domain-slice-docs-profile.md
5. planning/documentation-reusable-candidate/enman-docs-adapter.md
6. planning/documentation-reusable-candidate/PORTABILITY-FOLLOWUPS.md
```

For candidate field-kit/project-instance review, read the relevant kit group:

```text
status:
  planning/documentation-reusable-candidate/status-reconciliation-field-kit.md
  planning/documentation-reusable-candidate/status-reconciliation-workflow.md
  planning/documentation-reusable-candidate/enman-status-evidence-profile.md

shared visibility:
  planning/documentation-reusable-candidate/shared-visibility-map-field-kit.md
  planning/documentation-reusable-candidate/local-global-documentation-sync-workflow.md
  planning/documentation-reusable-candidate/enman-shared-visibility-map.md

source usage / cascade:
  planning/documentation-reusable-candidate/source-usage-cascade-field-kit.md
  planning/documentation-reusable-candidate/source-usage-cascade-governance-plan.md
  planning/documentation-reusable-candidate/enman-source-usage-cascade-profile.md
```

## 4. Candidate Core Architecture Files

| File | Candidate role |
|---|---|
| `CANDIDATE-NOTICE.md` | Guardrail: candidate is temporary, non-canonical and not active source of truth. |
| `PRINCIPLES-RESPONSIBILITY-CLASSIFICATION.md` | F3 section-by-section classification artifact for candidate principles. |
| `planning-docs-architecture-principles.md` | Genericized candidate reusable documentation architecture principles. |
| `scenario-domain-slice-docs-profile.md` | Specialized reusable profile for scenario/domain/slice app/product projects. |
| `enman-docs-adapter.md` | Candidate Enman/current-project adapter preserving concrete project mappings. |
| `PORTABILITY-FOLLOWUPS.md` | Deferred migration/follow-up tracking after F4/F5/F6A. |

## 5. Candidate Field Kits, Workflows And Project Instances

| Area | Field kit / setup owner | Repeated workflow / bridge | Candidate Enman project instance |
|---|---|---|---|
| Status / evidence | `status-reconciliation-field-kit.md` | `status-reconciliation-workflow.md` | `enman-status-evidence-profile.md` |
| Shared visibility | `shared-visibility-map-field-kit.md` | `local-global-documentation-sync-workflow.md` | `enman-shared-visibility-map.md` |
| Source usage / cascade | `source-usage-cascade-field-kit.md` | `source-usage-cascade-governance-plan.md` | `enman-source-usage-cascade-profile.md` |

Responsibility split:

```text
principles
  -> invariant / why the boundary exists;

field kit
  -> setup questions, applicability gate and project artifact shape;

workflow
  -> repeated process after project config exists;

project instance / adapter
  -> concrete Enman answers, paths and mappings;

examples
  -> demonstrations only.
```

## 6. Candidate Examples

Candidate examples index:

```text
planning/documentation-reusable-candidate/examples/README.md
```

Current candidate F5 examples:

```text
planning/documentation-reusable-candidate/examples/STATUS-RECONCILIATION-SCENARIO-PROJECT-EXAMPLE.md
planning/documentation-reusable-candidate/examples/SHARED-VISIBILITY-SCENARIO-PROJECT-EXAMPLE.md
planning/documentation-reusable-candidate/examples/SOURCE-USAGE-CASCADE-GENERIC-EXAMPLE.md
planning/documentation-reusable-candidate/examples/SOURCE-USAGE-CASCADE-ENMAN-SCENARIO-EXAMPLE.md
```

Examples do not own rules. They demonstrate how field kits, workflows, profiles or adapters are used.

## 7. Related Active Owners

The active documentation layer still owns current project documentation behavior.

Related active files:

```text
planning/documentation/README.md
planning/documentation/documentation-layer-portability-migration-plan.md
planning/documentation/documentation-responsibility-zone-review-workflow.md
planning/documentation/planning-docs-architecture-principles.md
planning/documentation/documentation-responsibility-map.md
planning/documentation/documentation-action-log.md
```

Related active/root files:

```text
planning/README.md
planning/planning-doc-responsibility-map.md
planning/planning-use-case-map.md
planning/replacement-file-generation-guide.md
```

Use active owners for ordinary Enman documentation updates until a migration/switch batch says otherwise.

## 8. Current Candidate Migration State

Completed candidate migration steps:

```text
F1: reusable responsibility model and long-decision capture
F2: candidate baseline copy and guardrails
F3: candidate principles responsibility classification
F4: candidate principles/profile/adapter split
F5: candidate field kits / Enman project-instance files / examples split
F6A: candidate navigation and read-order cleanup
```

Remaining future decisions include:

```text
- Enman project-instance files were promoted to active planning/ root in F6C;
- whether to delete/archive the source usage governance bridge;
- whether to rename candidate principles before final reusable switch;
- whether active planning/documentation/ is replaced, archived or kept as Enman-specific docs;
- whether to create a generic project adapter template.
```

## 8A. Active Project-Instance Promotion State

F6C promotes the three Enman project-instance files from candidate into active planning root:

```text
planning/status-evidence-profile.md
planning/shared-visibility-map.md
planning/source-usage-cascade-profile.md
```

Candidate source files remain in this folder as migration history and reusable-candidate references:

```text
planning/documentation-reusable-candidate/enman-status-evidence-profile.md
planning/documentation-reusable-candidate/enman-shared-visibility-map.md
planning/documentation-reusable-candidate/enman-source-usage-cascade-profile.md
```

F6C does not promote reusable field kits, candidate workflows, candidate principles or examples.

## 9. Do Not

```text
- Do not treat this candidate folder as active documentation.
- Do not make routine Enman docs updates here.
- Do not update both active docs and candidate docs for ordinary changes.
- Do not promote Enman project-instance files to active planning/ root without an explicit migration batch.
- Do not switch canonical references to this folder without an explicit migration/switch batch.
- Do not delete bridge/historical candidate files just because newer field kits exist.
```
