# Apply — enman-maybe-optional-result-repository-convention-v1

From repository root:

```powershell
Expand-Archive -Path "C:\Users\alexa\Downloads\enman-maybe-optional-result-repository-convention-v1.zip" -DestinationPath . -Force
git status
```

Review:

```powershell
git diff -- planning/README.md
git diff -- planning/planning-workflow-current.md
git diff -- planning/slices/README.md
git diff -- planning/slices/l1-slice-drafting-guide.md
git diff -- planning/slices/implementation-principles.md
git diff -- planning/slices/shared/README.md
git diff -- planning/slices/shared/maybe-for-optional-results.md
git diff -- planning/slices/slice-implementation-notes-register.md
```
