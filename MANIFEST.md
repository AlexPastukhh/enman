# MANIFEST — Cross-Cutting Antiforgery Marker Sync

Archive: `cross-cutting-antiforgery-marker-sync.zip`  
Scope: docs-only addendum for antiforgery failure normalization and drafting concerns  
Source input: latest user-provided antiforgery marker clarification

## Replace / Add

| File | Why |
|---|---|
| `planning/slices/cross-cutting/CC-CSRF-001-antiforgery-token-session-context.md` | Adds explicit internal server marker vs public client marker rule, result filter detection guidance, top-level ProblemDetails `code`, no generic 400 inference, no auto replay, and required tests. |
| `planning/slices/cross-cutting/cross-cutting-concerns-drafting-checklist.md` | Adds antiforgery marker considerations to the cross-cutting concerns checklist used by slice drafters. |
| `planning/slices/l1-slice-drafting-guide.md` | Adds a short pointer that cross-cutting concerns in drafts must distinguish internal framework markers from public API/client markers when relevant. |

## Not included

- No runtime code.
- No tests.
- No generated artifacts.
- No GitHub writes.
