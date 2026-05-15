# Apply — enman-slice-visual-flow-docs-v1.zip

From the repository root, run:

```powershell
Expand-Archive -Path "C:\Users\alexa\Downloads\enman-slice-visual-flow-docs-v1.zip" -DestinationPath . -Force
git status
```

Recommended checks:

```powershell
git diff -- planning/slices/l1-slice-drafting-guide.md
git diff -- planning/slices/README.md
git diff -- planning/slices/SL-ACC-001-register-client-account.md
git diff -- planning/slices/SL-APPL-001-create-individual-applicant-party.md
git diff -- planning/slices/SL-REQ-001-create-connection-request.md
git diff -- planning/slices/examples/README.md
git diff -- planning/README.md
git diff -- planning/planning-doc-responsibility-map.md
```

Do not commit `MANIFEST.md` or `APPLY.md` unless explicitly requested.
