# Apply — enman-ui-command-success-convention-v1

Apply from the repository root:

```powershell
Expand-Archive -Path "C:\Users\alexa\Downloads\enman-ui-command-success-convention-v1.zip" -DestinationPath . -Force
git status
```

Review the affected files:

```powershell
git diff -- planning/client/cross-cutting/CL-COMMAND-001-command-success-without-required-response-body.md
git diff -- planning/client/cross-cutting/README.md
git diff -- planning/client/README.md
git diff -- planning/diagrams/scenario-ui-specs/README.md
```
