# Apply

Docs-only draft-refactor archive. Apply from repository root:

```powershell
Expand-Archive -Path "C:\Users\alexa\Downloads\sl-emp-req-003-draft-refactor-v1.zip" -DestinationPath "." -Force
```

This archive replaces only:

```text
planning/slices/SL-EMP-REQ-003-start-request-review.md
```

It follows the safe archive workflow and preserves the original draft in:

```text
_archive-review/2026-05-19-sl-emp-req-003-draft-refactor-v1/original-files/planning/slices/SL-EMP-REQ-003-start-request-review.md
```

No runtime source code, tests, OpenAPI artifacts, generated API types, routes, CSS implementation or app code are changed.
