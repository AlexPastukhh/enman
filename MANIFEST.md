# MANIFEST

Archive: applicantparty-stage2-docs-sync-v2.zip  
Scope: documentation-only planning cleanup

## Add

```text
planning/architecture/README.md
planning/architecture/backend-legacy-and-l1-boundaries.md
```

## Replace

```text
planning/README.md
planning/planning-workflow-current.md
planning/planning-doc-responsibility-map.md
planning/client/README.md
planning/slices/README.md
planning/slices/slice-scenario-flow-behavior-register.md
planning/slices/slice-questions-register.md
planning/slices/slice-extension-points-register.md
planning/slices/slice-implementation-notes-register.md
planning/diagrams/scenario-text-specs/SC-10-applicant-data.md
planning/diagrams/scenario-text-specs/SC-10B-my-applicant-parties.md
planning/diagrams/scenario-ui-specs/SC-10-applicant-data-ui.md
planning/diagrams/scenario-behavior-items/SC-10-applicant-data-behavior-items.md
planning/slices/SL-APPL-001-create-individual-applicant-party.md
planning/slices/SL-APPL-001-create-individual-applicant-party.client.md
planning/slices/SL-APPL-002-account-applicant-parties-read.md
planning/slices/SL-APPL-003-select-current-default-applicant-party-template.md
```

## Delete

```text
none
```

## Scope Notes

```text
- Adds backend legacy/L1 boundary map for Stage 2 cleanup.
- Synchronizes ApplicantParty direction to one Applicant Parties page / section.
- Treats SC-10B as same-page future management addendum, not a separate current page.
- Keeps delete/archive lifecycle as future extension/change pressure only.
- Removes one-page-vs-two-page correction from behavior item IDs; records it in scenario rules and registers instead.
- Updates early-read navigation/status docs and client planning index to avoid stale Account-page wording.
```

## Non-goals

```text
- no runtime code changes;
- no generated artifact changes;
- no domain model changes;
- no GitHub writes, commits, branches or PRs;
- no My Requests content changes except shared navigation/status context if needed.
```
