# Slice Questions Register

Status: working register / taxonomy, cross-cutting and testing decisions synchronized

Question statuses:

```text
blocked
accepted
assumption
future-review
docs-sync
```

| Question ID | Slice ID | Status | Question | Current direction | Impact | Owner/file |
|---|---|---|---|---|---|---|
| Q-CLIENT-UI-001 | client docs | accepted | Should app header include decorative public nav first pass? | No. Header is flow-based only. | App shell and UI foundation | `planning/slices/client/CLIENT-UI-STYLE-WORKFLOW.md` |
| Q-CLIENT-CSS-001 | client docs | accepted | Is CSS part of slice implementation ownership? | Yes. CSS ownership follows page/widget/entity/feature/shared boundaries. | All client slice drafts | `planning/slices/client/CLIENT-CSS-ARCHITECTURE-RULES.md` |
| Q-CLIENT-FLOW-001 | client docs | accepted | Should Visual Client Implementation Flow use `does`? | No. Use `needed to` to describe why a dependency is needed in this block. | Client slice template | `planning/slices/client/CLIENT-SLICE-TEMPLATE.md` |
| Q-CLIENT-FLOW-002 | client docs | accepted | Should every dependency have `visual`? | No. Only UI-rendering dependencies get `visual`. Hooks/API/query keys/helpers do not. | Client slice template | `planning/slices/client/CLIENT-SLICE-TEMPLATE.md` |
| Q-CLIENT-FLOW-003 | client docs | accepted | What should `visual` describe? | Only the block's role in the parent layout, not child internals. | CSS ownership and draft readability | `planning/slices/client/CLIENT-CSS-ARCHITECTURE-RULES.md` |
| Q-MIGRATION-001 | slice docs | accepted | Should all old `.client.md` drafts be moved now? | No. Add new structure and index first; migrate drafts gradually. | Avoid broken links / noisy diff | `planning/slices/SLICE-FOLDER-MAP.md` |
| Q-MIGRATION-002 | slice docs | accepted | Should future folders use L1/L2 grouping? | No. New docs are grouped by client/server/cross-cutting responsibility. L1/L2 is legacy only. | Folder structure and navigation | `planning/slices/SLICE-FOLDER-MAP.md` |
| Q-TAXONOMY-001 | slice docs | accepted | Are behavior items smaller than slices? | Yes. A slice may implement an independent chain of behavior items. | Scenario/behavior/slice explanations | `planning/diagrams/scenario-behavior-items/README.md` |
| Q-TAXONOMY-002 | cross-cutting docs | accepted | Do cross-cutting implementation docs still need scenario behavior sources? | Yes. Common behavior must be reflected in scenario-cross-cutting sources before slice drafting. | Deferred validation, CSRF, common feedback | `planning/diagrams/scenario-cross-cutting/README.md` |
| Q-TAXONOMY-003 | cross-cutting docs | accepted | Is `planning/slices/cross-cutting/` an implementation dump? | No. It is for umbrella/coordination docs. Side-specific implementation goes to client/server slice drafts. | CSRF and future server-client concerns | `planning/slices/cross-cutting/README.md` |
| Q-NAMING-001 | slice docs | accepted | How do we mark a client-only or server-only slice draft? | Use `SINGLE-` prefix at the start of the file name. | Navigation and pairing clarity | `planning/slices/README.md` |
| Q-TEST-001 | slice docs | accepted | What must slice tests prove? | Behavior items and scenario outcomes, not implementation flow. | All slice draft Test / Verification Plans | `planning/slices/slice-test-plan-workflow.md` |
| Q-TEST-002 | slice docs | accepted | Can implementation details appear in tests? | Yes, but only as setup/action/observation mechanisms. They must not be primary proof. | Test stability and behavior proof | `planning/slices/slice-test-plan-workflow.md` |
| Q-TEST-003 | slice docs | accepted | What extra test quality questions are required? | Escape risk and refactor risk must be stated for planned/actual tests. | Prevent weak and fragile tests | `planning/slices/slice-test-plan-workflow.md` |
| Q-TEST-004 | slice docs | accepted | Should direct DB setup be allowed in integration tests? | Yes, only for scenario preconditions; behavior proof still goes through public boundary. | Server integration tests | `planning/slices/slice-test-plan-workflow.md` |
| Q-ARCHIVE-001 | archive workflow | accepted | Should large archives replace many existing docs without preserving originals? | No. If an archive replaces an existing file, it must include a copy of the original under `_archive-review/<unique-archive-slug>/original-files/`. | Prevent accidental loss of guardrails/navigation | `planning/archive-workflow/SAFE-ARCHIVE-MERGE-WORKFLOW.md` |
| Q-ARCHIVE-002 | archive workflow | accepted | Should archive raw/derived notes use one shared project file? | No. Use archive-local unique folder names to prevent archives from overwriting each other. | Keeps author logs and audit notes isolated | `planning/archive-workflow/ARCHIVE-REVIEW-FOLDER-RULES.md` |
| Q-ARCHIVE-003 | archive workflow | accepted | How should high-risk replacement archives be applied? | Use two-step workflow: safe merge archive first, post-apply merge review second, then smaller correction archive if needed. | Reduces docs merge risk | `planning/archive-workflow/POST-APPLY-MERGE-REVIEW-WORKFLOW.md` |
| Q-ARCHIVE-004 | archive workflow | accepted | Should chat list the archive plan before creating a replacement archive? | Yes. New files, replacement files, original snapshots, high-risk files and post-apply review points must be listed. | Makes archive scope explicit before applying | `planning/archive-workflow/ARCHIVE-PLAN-TEMPLATE.md` |
