# Apply — enman-agreement-proposal-replacement-source-cleanup-v1

From the repository root, run:

```powershell
Expand-Archive -Path "C:\Users\alexa\Downloads\enman-agreement-proposal-replacement-source-cleanup-v1.zip" -DestinationPath . -Force
git status
```

Review:

```powershell
git diff -- planning/diagrams/scenario-text-specs/00-scenario-text-specs-index.md
git diff -- planning/diagrams/scenario-data/00-scenario-data-index.md
git diff -- planning/diagrams/scenario-questions-register.md
git diff -- planning/diagrams/scenario-clarifications/AGR-001-agreement-proposal-replacement-terminology.md
```
