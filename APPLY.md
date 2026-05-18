# Apply

Docs-only archive. Apply from repository root:

```powershell
Expand-Archive -Path "C:\Users\alexa\Downloads\slice-taxonomy-cross-cutting-testing-workflow-v3.zip" -DestinationPath "." -Force
```

This archive intentionally changes planning markdown files only.

No runtime source code, OpenAPI artifacts, generated API types, routes, CSS implementation or app code are changed.

Existing concrete slice drafts are not rewritten by this archive. Historical drafts may stay where they are until a dedicated migration/update task handles them.

Prefer this v3 archive over earlier taxonomy archives because it also includes the slice testing workflow and preserves the historical L2 guardrails instead of replacing them with a short stub.
