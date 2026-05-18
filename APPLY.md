# Apply

Docs-only archive. Apply from repository root:

```powershell
Expand-Archive -Path "C:\Users\alexa\Downloads\scenario-ui-specs-workflow-and-readiness.zip" -DestinationPath "." -Force
```

This archive intentionally changes planning markdown files only.

No runtime source code, OpenAPI artifacts, generated API types, routes, CSS implementation or app code are changed.

Existing client/server slice drafts are not rewritten by this archive. This archive prepares UI scenario sources and workflow so slice drafts can be updated later.
