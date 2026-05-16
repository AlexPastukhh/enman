# Apply — enman-scenario-feedback-logout-sync-v1

```powershell
Expand-Archive -Path "C:\Users\alexa\Downloads\enman-scenario-feedback-logout-sync-v1.zip" -DestinationPath . -Force
git status
```

Review especially:

```powershell
git diff -- planning/diagrams/scenario-text-specs/SC-04-client-request-creation.md
git diff -- planning/diagrams/scenario-ui-specs/SC-04-request-creation-ui.md
git diff -- planning/diagrams/scenario-behavior-items/SC-04-request-creation-behavior-items.md
git diff -- planning/client/cross-cutting/CL-FEEDBACK-001-client-feedback-messages.md
git diff -- planning/slices/SL-AUTH-003-logout.client.md
git diff -- planning/slices/slice-scenario-flow-behavior-register.md
```
