# Apply

Small docs-only correction archive. Apply from repository root:

```powershell
Expand-Archive -Path "C:\Users\alexa\Downloads\archive-merge-workflow-qfix-v1.zip" -DestinationPath "." -Force
```

This archive corrects Markdown table formatting in:

```text
planning/slices/SLICE-QUESTIONS.md
```

It follows the safe archive workflow and preserves the original file in:

```text
_archive-review/2026-05-18-archive-merge-workflow-qfix-v1/original-files/planning/slices/SLICE-QUESTIONS.md
```

No runtime source code, OpenAPI artifacts, generated API types, routes, CSS implementation or app code are changed.
