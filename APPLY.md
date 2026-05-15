# Apply — enman-agreement-proposal-replacement-docs-consistency-v2.zip

From repository root on branch `my-changes`, run:

```powershell
Expand-Archive -Path "C:\Users\alexa\Downloads\enman-agreement-proposal-replacement-docs-consistency-v2.zip" -DestinationPath . -Force
git status
```

Then inspect:

```powershell
git diff -- planning/diagrams/scenario-text-specs/SC-13B-agreement-proposal-details-response.md
git diff -- planning/diagrams/scenario-text-specs/SC-13D-employee-agreement-proposal-create-response.md
git diff -- planning/diagrams/scenario-data/SC-13B-agreement-proposal-details-response-data.md
git diff -- planning/diagrams/scenario-data/SC-13D-employee-agreement-proposal-create-response-data.md
git diff -- planning/diagrams/scenario-text-specs/scenario-server-domain-validation-addendum.md
git diff -- planning/tables/pre-domain-variants-input.md
git diff -- planning/diagrams/scenario-clarifications/AGR-001-agreement-proposal-replacement-terminology.md
```
