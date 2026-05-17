# Manifest — L2 Scenario / Slice Follow-up Cleanup

Docs-only archive.

## Replaced / added files

```text
planning/slices/README.md
planning/slices/l2/README.md
planning/slices/slice-scenario-flow-behavior-register.md
planning/slices/slice-questions-register.md
planning/slices/slice-implementation-notes-register.md
planning/diagrams/scenario-text-specs/SC-07B-employee-request-review.md
planning/diagrams/scenario-behavior-items/SC-07B-employee-request-review-behavior-items.md
planning/diagrams/scenario-text-specs/SC-13A-client-agreements.md
planning/diagrams/scenario-text-specs/SC-13B-client-agreement-proposal-details-response.md
planning/diagrams/scenario-text-specs/SC-13C-employee-agreements.md
planning/diagrams/scenario-text-specs/SC-13D-employee-agreement-proposal-create-send-version.md
planning/diagrams/scenario-text-specs/SC-13E-agreement-final-refusal.md
planning/diagrams/scenario-text-specs/SC-14-agreement-documents.md
planning/diagrams/scenario-clarifications/L2-agreement-scenario-slice-followup-cleanup.md
APPLY-l2-scenario-slice-followup-cleanup.ps1
APPLY-l2-scenario-slice-followup-cleanup.md
MANIFEST-l2-scenario-slice-followup-cleanup.md
```

## Purpose

- Finish the follow-up left after validation scenario cleanup.
- Synchronize canonical Agreement Exchange slice numbering.
- Update SC-13A/B/C/D/E and SC-14 with current validation/domain guardrails.
- Mark RejectReview feedback optional.
- Remove stale global validation addendum as source-of-truth dependency from active behavior source set.
- Avoid root APPLY.md / MANIFEST.md overwrite by using unique handoff filenames.
