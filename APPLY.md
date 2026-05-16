# Apply — enman-short-draft-example-extension-change-points-v1

From repository root:

```powershell
Expand-Archive -Path "C:\Users\alexa\Downloads\enman-short-draft-example-extension-change-points-v1.zip" -DestinationPath . -Force
git status
```

Then review:

```powershell
git diff -- planning/slices/examples/L1-APPLICANT-PARTY-READ-CURRENT-early-short-draft-example.md
git diff -- planning/slices/examples/README.md
git diff -- planning/slices/l1-slice-drafting-guide.md
git diff -- planning/slices/draft-driven-discovery-principles.md
git diff -- planning/slices/change-extension-points-principles.md
git diff -- planning/slices/slice-extension-points-register.md
git diff -- planning/slices/slice-implementation-notes-register.md
```
