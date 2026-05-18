# MANIFEST

Docs-only archive for scenario-level UI requirements, UI scenario workflow, UI readiness, and current UI migration notes.

## Purpose

Fix the source-of-truth split:

```text
planning/diagrams/scenario-ui-specs/
  scenario-level UI requirements:
    what the user sees, screen composition, visible states, actions and actor differences.

planning/slices/client/
  client implementation rules:
    layering, CSS ownership, validation, accessibility, handoff and draft template.

planning/slices/client/<slice>.client.md
  concrete implementation translation for one client slice.
```

## Important decisions captured

```text
- UI requirements must exist in scenario UI specs, not only in client slice drafts.
- The accepted UI scenario template is now fixed.
- Existing UI scenarios are reviewed for completeness/readiness.
- SC-10B My Applicant Parties UI is not current/canonical.
- Applicant parties UI belongs to the Account page section.
- Future ApplicantParty edit/delete/archive/current actions happen in that Account section unless a later decision changes it.
- Current edit/delete/archive behavior is not implemented.
- The current UI migration problem is documented as a temporary implementation goal, not an application scenario.
- Archive-local raw author message logs are used when preserving user's wording for thesis/writing work.
```

## Files included

```text
planning/diagrams/scenario-ui-specs/README.md
planning/diagrams/scenario-ui-specs/UI-SCENARIO-CONVENTIONS.md
planning/diagrams/scenario-ui-specs/UI-SCENARIO-TEMPLATE.md
planning/diagrams/scenario-ui-specs/UI-SCENARIO-READINESS.md
planning/diagrams/scenario-ui-specs/APP-UI-001-app-shell-home-auth-flow-ui.md
planning/diagrams/scenario-ui-specs/SC-04-request-creation-ui.md
planning/diagrams/scenario-ui-specs/SC-05-my-requests-ui.md
planning/diagrams/scenario-ui-specs/SC-10-applicant-data-ui.md
planning/diagrams/scenario-ui-specs/SC-10B-my-applicant-parties-ui.md

planning/slices/slice-scenario-flow-behavior-register.md
planning/slices/cross-cutting/CC-CLIENT-UI-MIGRATION-001-current-ui-to-slice-owned-ui.md
planning/slices/cross-cutting/CC-AUTHOR-MESSAGE-LOG-001-archive-local-author-message-capture.md

_archive-notes/scenario-ui-specs-workflow-and-readiness/raw-author-message-log.md
_archive-notes/scenario-ui-specs-workflow-and-readiness/derived-decisions.md
```

## Not included

```text
- no runtime UI fixes
- no CSS implementation changes
- no slice draft rewrites
- no mass movement of legacy L1/L2 slice files
- no OpenAPI/generated file changes
```
