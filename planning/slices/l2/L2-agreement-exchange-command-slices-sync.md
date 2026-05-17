# L2 Agreement Exchange Command Slices Sync

Status: docs-only sync / command and client sidecar drafts added

This note records the canonical command slice additions after agreement exchange read slices:

```text
SL-AGR-EXCH-002 — Send Agreement Counter-Proposal Version
L2-AGR-EXCH-SEND-PROPOSAL-001.client — Send Agreement Proposal Version

SL-AGR-EXCH-005 — Client Accept Active Agreement Proposal
L2-AGR-EXCH-ACCEPT-001.client — Client Accept Active Agreement Proposal

SL-AGR-EXCH-006 — Final Refuse Agreement Exchange
```

## Current-state rule

Uploaded drafts are planning input, not proof of current repo implementation.

When answering “what is currently implemented?”, inspect GitHub/current branch runtime code, OpenAPI, generated types and tests. Do not infer current state from handoff archives or uploaded draft text.

## Numbering

Canonical agreement exchange numbering:

```text
SL-AGR-EXCH-001 — Start Agreement Exchange With Initial Employee Proposal
SL-AGR-EXCH-002 — Send Agreement Counter-Proposal Version
SL-AGR-EXCH-003 — Agreement Exchange List Page / Read List
SL-AGR-EXCH-004 — Agreement Exchange Details / Read Details
SL-AGR-EXCH-005 — Client Accept Active Agreement Proposal
SL-AGR-EXCH-006 — Final Refuse Agreement Exchange
```

## Placement

```text
Server/backend/API slices:
  planning/slices/SL-AGR-EXCH-*.md

Client sidecars:
  planning/slices/l2/L2-AGR-EXCH-*.client.md
```
