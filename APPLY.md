# Apply — enman-applicant-party-template-model-and-slices-v1

From repository root:

```powershell
Expand-Archive -Path "C:\Users\alexa\Downloads\enman-applicant-party-template-model-and-slices-v1.zip" -DestinationPath . -Force
git status
```

Review suggested diffs:

```powershell
git diff -- planning/diagrams/scenario-text-specs/SC-10-applicant-data.md
git diff -- planning/diagrams/scenario-text-specs/SC-04-client-request-creation.md
git diff -- planning/slices/SL-APPL-001-create-individual-applicant-party.md
git diff -- planning/slices/SL-APPL-002-account-applicant-parties-read.md
git diff -- planning/slices/SL-APPL-003-select-current-default-applicant-party-template.md
git diff -- planning/slices/SL-APPL-004-applicant-party-creation-application-service.md
git diff -- planning/slices/SL-REQ-004-create-request-with-applicant-context.md
```
