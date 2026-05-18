# Apply

Docs-only archive. Apply from repository root:

```powershell
Expand-Archive -Path "C:\Users\alexa\Downloads\archive-merge-workflow-with-originals-v1.zip" -DestinationPath "." -Force
```

This archive follows the safe archive workflow it introduces.

It includes replacement files and also preserves originals for those replacements in:

```text
_archive-review/2026-05-18-archive-merge-workflow-v1/original-files/
```

After applying, run the post-apply review described in:

```text
planning/archive-workflow/POST-APPLY-MERGE-REVIEW-WORKFLOW.md
```

No runtime source code, OpenAPI artifacts, generated API types, routes, CSS implementation or app code are changed.
