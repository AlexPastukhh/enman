# Slice Questions Register

Status: working register / update during drafting

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
