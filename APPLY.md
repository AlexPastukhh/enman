# Apply — enman-validation-transition-and-my-requests-filters-docs-v1

From repository root:

```powershell
Expand-Archive -Path "C:\Users\alexa\Downloads\enman-validation-transition-and-my-requests-filters-docs-v1.zip" -DestinationPath . -Force
git status
```

Recommended review:

```powershell
git diff -- planning/slices/cross-cutting/CC-VALIDATION-001-server-request-validation-and-fluentvalidation.md
git diff -- planning/slices/l1/L1-MY-REQUESTS-LIST-FILTERS.client.md
git diff -- planning/slices/slice-scenario-flow-behavior-register.md
git diff -- planning/slices/slice-questions-register.md
git diff -- planning/diagrams/scenario-behavior-items/SC-05-my-requests-behavior-items.md
```
