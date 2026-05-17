# MANIFEST — Cross-Cutting Concerns / Antiforgery Drafting Rules Sync

Archive: `cross-cutting-concerns-drafting-rules-sync.zip`  
Scope: documentation-only planning update  
Purpose: strengthen antiforgery cross-cutting slice and add mandatory cross-cutting concerns checks to slice/client draft rules.

## Add

| File | Why |
|---|---|
| `planning/slices/cross-cutting/cross-cutting-concerns-drafting-checklist.md` | Canonical checklist for draft authors: auth/session, authorization/ownership, antiforgery, validation/ProblemDetails, OpenAPI/generated artifacts, transactions/no-mutation, concurrency/idempotency, file/document boundaries, clock/audit, privacy/security, testing and client feedback. |

## Replace

| File | Why |
|---|---|
| `planning/slices/cross-cutting/README.md` | Adds the checklist to cross-cutting navigation and clarifies consumer rules for business/client slices. |
| `planning/slices/cross-cutting/CC-CSRF-001-antiforgery-token-session-context.md` | Updates antiforgery/CSRF slice with clearer consumer considerations, browser unsafe request scope, file/multipart considerations, generated-contract boundary and testing guidance. |
| `planning/slices/l1-slice-drafting-guide.md` | Adds mandatory `Cross-Cutting Concerns / Considerations` section to draft rules/templates and explains not to pollute Scenario Flow or Behavior Items with implementation concerns. |
| `planning/slices/client-slice-short-draft-rules-and-example.md` | Adds the cross-cutting concerns section to the canonical short client draft shape and example. |
| `planning/slices/README.md` | Links the new checklist from slice planning navigation. |

## Delete

None.

## Non-goals

```text
- no runtime/backend/client code changes;
- no tests;
- no generated artifacts;
- no GitHub branch/commit/PR;
- no implementation of antiforgery;
- no changes to scenario behavior sources.
```

## Notes

Cross-cutting concerns are drafting considerations and/or concern-derived behavior only when a cross-cutting source defines behavior items. They must not be inserted into Scenario Flow as if they were business scenario behavior.
