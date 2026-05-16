# APPLY

Apply from repository root.

```powershell
Expand-Archive -Path "C:\Users\alexa\Downloads\applicantparty-stage2-docs-sync-v2.zip" -DestinationPath . -Force
git status
git diff -- planning
```

Recommended review order after apply:

```text
planning/README.md
planning/planning-workflow-current.md
planning/architecture/README.md
planning/architecture/backend-legacy-and-l1-boundaries.md
planning/diagrams/scenario-text-specs/SC-10-applicant-data.md
planning/diagrams/scenario-text-specs/SC-10B-my-applicant-parties.md
planning/diagrams/scenario-ui-specs/SC-10-applicant-data-ui.md
planning/diagrams/scenario-behavior-items/SC-10-applicant-data-behavior-items.md
planning/slices/slice-scenario-flow-behavior-register.md
planning/slices/slice-questions-register.md
planning/slices/SL-APPL-001-create-individual-applicant-party.md
planning/slices/SL-APPL-001-create-individual-applicant-party.client.md
planning/slices/SL-APPL-002-account-applicant-parties-read.md
planning/slices/SL-APPL-003-select-current-default-applicant-party-template.md
planning/client/README.md
```

Do not run code generation for this archive. It is documentation-only.
