# APPLY — Applicant Current Active Scenario Sync

Run from repository root after downloading the archive:

```powershell
Expand-Archive -Path "C:\Users\alexa\Downloads\enman-scenario-applicant-current-active-sync.zip" -DestinationPath . -Force
git status
```

Recommended review after applying:

```powershell
git diff -- planning/diagrams/scenario-text-specs/00-scenario-text-specs-index.md
git diff -- planning/diagrams/scenario-text-specs/SC-10-applicant-data.md
git diff -- planning/diagrams/scenario-text-specs/SC-04-client-request-creation.md
git diff -- planning/diagrams/scenario-data/00-scenario-data-index.md
git diff -- planning/diagrams/scenario-data/SC-10-applicant-data.md
git diff -- planning/diagrams/scenario-data/SC-04-request-creation-data.md
git diff -- planning/diagrams/scenario-questions-register.md
git diff -- planning/slices/slice-questions-register.md
git diff -- planning/slices/slice-implementation-notes-register.md
```

Expected status:

```text
modified: planning/diagrams/scenario-text-specs/00-scenario-text-specs-index.md
modified: planning/diagrams/scenario-text-specs/SC-10-applicant-data.md
modified: planning/diagrams/scenario-text-specs/SC-04-client-request-creation.md
modified: planning/diagrams/scenario-data/00-scenario-data-index.md
modified: planning/diagrams/scenario-data/SC-10-applicant-data.md
modified: planning/diagrams/scenario-data/SC-04-request-creation-data.md
modified: planning/diagrams/scenario-questions-register.md
modified: planning/slices/slice-questions-register.md
modified: planning/slices/slice-implementation-notes-register.md
```
