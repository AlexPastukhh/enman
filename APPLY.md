# Apply

Docs-only archive. Apply from repository root:

```powershell
Expand-Archive -Path "C:\Users\alexa\Downloads\implemented-slice-draft-sync-workflow-v1.zip" -DestinationPath "." -Force
```

This archive follows the safe archive workflow.

It adds implemented slice draft sync docs and updates navigation.

It preserves originals for replaced files in:

```text
_archive-review/2026-05-18-implemented-slice-draft-sync-v1/original-files/
```

No runtime source code, OpenAPI artifacts, generated API types, routes, CSS implementation or app code are changed.
