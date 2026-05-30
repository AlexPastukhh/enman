# Batch F5 v2 — Candidate Field Kits Package

Status: replacement package created  
Scope: candidate-only field-kit/project-instance split for status, shared visibility and source usage cascade

## Included changes

This v2 package keeps the F5 v1 architecture and fixes review blockers:

- preserves the SC-13D / AgreementProposalExchange source-usage pilot knowledge in Enman candidate profile/example;
- restores compact reusable local/global operational rules for question status, assumptions, local-only reasons, back-references and preflight;
- fixes candidate examples README wording so it no longer says no filled examples exist after F5 examples are added.

It also includes the F5 v1 scope:

- candidate reusable field kits;
- narrowed candidate workflows/bridge;
- candidate Enman project-instance files;
- candidate examples;
- candidate navigation/notice/followups/adapter/examples index;
- active migration plan and action log updates.

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
planning/documentation-reusable-candidate/examples/SOURCE-USAGE-CASCADE-ENMAN-SCENARIO-EXAMPLE.md
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
