# Slice Draft File Naming and Placement

Status: current / agreement exchange numbering synchronized

## 1. Current-State Source Rule

When answering about the current project state, inspect the current GitHub branch/repository state.

Do not infer current implementation status from handoff archives alone. Archives are proposed patches or documentation sources until applied.

## 2. Canonical Placement

Server/backend/API slice drafts:

```text
planning/slices/SL-<AREA>-<ID>-<slug>.md
```

Client sidecar drafts:

```text
planning/slices/l2/L2-<AREA>-<ID>-<slug>.client.md
```

Shared cross-cutting slice docs:

```text
planning/slices/cross-cutting/CC-<AREA>-<ID>-<slug>.md
```

## 3. Agreement Exchange Canonical Names

```text
planning/slices/SL-AGR-EXCH-001-start-agreement-exchange-with-initial-employee-proposal.md
planning/slices/SL-AGR-EXCH-002-send-agreement-counter-proposal-version.md
planning/slices/SL-AGR-EXCH-003-agreement-exchange-list-read.md
planning/slices/SL-AGR-EXCH-004-agreement-exchange-details-read.md
planning/slices/SL-AGR-EXCH-005-accept-active-agreement-proposal.md
planning/slices/SL-AGR-EXCH-006-final-refuse-agreement-exchange.md

planning/slices/l2/L2-AGR-EXCH-LIST-001-agreement-exchange-list.client.md
planning/slices/l2/L2-AGR-EXCH-DETAILS-001-agreement-exchange-details.client.md
```

## 4. Superseded Agreement Exchange Names

```text
planning/slices/SL-AGR-EXCH-003-read-agreement-exchange.md
planning/slices/SL-AGR-EXCH-004-accept-active-agreement-proposal.md
planning/slices/SL-AGR-EXCH-005-final-refuse-agreement-exchange.md
```

Do not keep superseded names next to canonical names. Keeping both creates duplicate/conflicting slice numbers.

## 5. Full Draft Structure Rule

Full server and client drafts must include an `Implementation Checklist` near the end.

For full server command drafts using the 20-section shape, prefer:

```text
## 19. Implementation Checklist
```

Client sidecars may have a different section number if their canonical template is shorter, but they still need a checklist near the end.

Checklist items must be specific to the slice. Do not copy unrelated checklists mechanically.
