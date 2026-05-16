# Apply — enman-fv-validation-docs-v1

From repository root:

```powershell
Expand-Archive -Path "C:\Users\alexa\Downloads\enman-fv-validation-docs-v1.zip" -DestinationPath . -Force
git status
```

Review:

```powershell
git diff -- planning/slices/cross-cutting/CC-VALIDATION-001-server-request-validation-and-fluentvalidation.md
git diff -- planning/slices/l1-slice-drafting-guide.md
git diff -- planning/planning-agent-protocol.md
git diff -- planning/api/client-server-contract-principles.md
git diff -- planning/api/api-error-contract.md
git diff -- planning/slices/slice-questions-register.md
```
