# Apply

Docs-only archive. Apply from repository root:

```powershell
Expand-Archive -Path "C:\Users\alexa\Downloads\slice-client-docs-ui-css-workflow-v3.zip" -DestinationPath "." -Force
```

This archive intentionally changes planning markdown files only.

No runtime source code, OpenAPI artifacts, generated API types, routes, CSS implementation or app code are changed.

Existing slice drafts are not rewritten in this archive. Historical drafts may stay where they are until a dedicated migration/update task handles them.
