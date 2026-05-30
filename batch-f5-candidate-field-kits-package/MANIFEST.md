# Batch F5 — Candidate Field Kits Package

Status: replacement package created  
Scope: candidate-only field-kit/project-instance split for status, shared visibility and source usage cascade

## Included changes

This package:

- creates candidate reusable field kits:
  - `status-reconciliation-field-kit.md`
  - `shared-visibility-map-field-kit.md`
  - `source-usage-cascade-field-kit.md`
- narrows existing candidate workflows/plans:
  - `status-reconciliation-workflow.md`
  - `local-global-documentation-sync-workflow.md`
  - `source-usage-cascade-governance-plan.md`
- creates candidate Enman project-instance files:
  - `enman-status-evidence-profile.md`
  - `enman-shared-visibility-map.md`
  - `enman-source-usage-cascade-profile.md`
- creates candidate examples:
  - `STATUS-RECONCILIATION-SCENARIO-PROJECT-EXAMPLE.md`
  - `SHARED-VISIBILITY-SCENARIO-PROJECT-EXAMPLE.md`
  - `SOURCE-USAGE-CASCADE-GENERIC-EXAMPLE.md`
- updates candidate navigation/notice/followups/adapter/examples index;
- updates active migration plan and action log.

## Intentionally not included

This package does **not**:

- switch canonical documentation ownership;
- promote Enman project-instance files to active `planning/` root;
- rewrite active `planning/documentation/` workflows;
- delete the source usage governance bridge;
- modify active `planning/documentation/planning-docs-architecture-principles.md`;
- create scripts.

## Files

```text
planning/documentation-reusable-candidate/status-reconciliation-field-kit.md
planning/documentation-reusable-candidate/status-reconciliation-workflow.md
planning/documentation-reusable-candidate/enman-status-evidence-profile.md
planning/documentation-reusable-candidate/shared-visibility-map-field-kit.md
planning/documentation-reusable-candidate/local-global-documentation-sync-workflow.md
planning/documentation-reusable-candidate/enman-shared-visibility-map.md
planning/documentation-reusable-candidate/source-usage-cascade-field-kit.md
planning/documentation-reusable-candidate/source-usage-cascade-governance-plan.md
planning/documentation-reusable-candidate/enman-source-usage-cascade-profile.md
planning/documentation-reusable-candidate/examples/STATUS-RECONCILIATION-SCENARIO-PROJECT-EXAMPLE.md
planning/documentation-reusable-candidate/examples/SHARED-VISIBILITY-SCENARIO-PROJECT-EXAMPLE.md
planning/documentation-reusable-candidate/examples/SOURCE-USAGE-CASCADE-GENERIC-EXAMPLE.md
planning/documentation-reusable-candidate/examples/README.md
planning/documentation-reusable-candidate/enman-docs-adapter.md
planning/documentation-reusable-candidate/PORTABILITY-FOLLOWUPS.md
planning/documentation-reusable-candidate/README.md
planning/documentation-reusable-candidate/CANDIDATE-NOTICE.md
planning/documentation/documentation-layer-portability-migration-plan.md
planning/documentation/documentation-action-log.md
```

## Delivery safety

- Large/shared files: candidate-only workflow replacements plus active tracking files.
- Preferred delivery: complete replacement package.
- Script needed: no.
