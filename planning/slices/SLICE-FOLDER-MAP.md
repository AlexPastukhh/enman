# Slice Folder Map

Status: current folder ownership map

## Canonical folders

```text
planning/slices/client/
  client rules, workflow, templates, examples and client sidecar drafts

planning/slices/server/
  server rules, workflow, templates, examples and server/API/backend drafts

planning/slices/cross-cutting/
  cross-cutting concerns shared by client/server or architecture-level decisions
```

## Common navigation

```text
planning/slices/README.md
  main entry point

planning/slices/SLICE-INDEX.md
  list of known slices and docs

planning/slices/SLICE-QUESTIONS.md
  register of decisions, blocked questions, assumptions and future-review items
```

## New slice file placement

New client drafts go directly to:

```text
planning/slices/client/
```

New server drafts go directly to:

```text
planning/slices/server/
```

Cross-cutting docs go to:

```text
planning/slices/cross-cutting/
```

Do not create new L1/L2 folders for future slice docs.

## Migration rule

Do not mass-move existing historical slice drafts unless a specific cleanup task does that.

Historical files under old paths may remain temporarily:

```text
planning/slices/l1/
planning/slices/l2/
planning/slices/SL-*.md
planning/client/
```

Use `SLICE-INDEX.md` to point to both legacy and new locations until migration is complete.

## Deprecated compatibility folders

```text
planning/client/
  deprecated compatibility entry point; use planning/slices/client/

planning/slices/l1/
planning/slices/l2/
  legacy historical slice draft grouping; do not add new files
```
