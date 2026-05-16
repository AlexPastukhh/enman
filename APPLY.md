# Apply — enman-implemented-client-full-slice-docs-v1

Run from repository root:

```powershell
Expand-Archive -Path "C:\Users\alexa\Downloads\enman-implemented-client-full-slice-docs-v1.zip" -DestinationPath . -Force
git status
```

Review suggested diffs:

```powershell
git diff -- planning/README.md
git diff -- planning/planning-workflow-current.md
git diff -- planning/client/README.md
git diff -- planning/slices/README.md
git diff -- planning/slices/SL-ACC-001-register-client-account.client.md
git diff -- planning/slices/SL-AUTH-001-login-client-account.client.md
git diff -- planning/slices/SL-AUTH-002-current-user.client.md
git diff -- planning/slices/SL-APPL-001-create-individual-applicant-party.client.md
git diff -- planning/slices/slice-questions-register.md
git diff -- planning/slices/slice-implementation-notes-register.md
```
