# Slice Draft File Naming and Placement

Status: active convention  
Scope: predictable storage for slice planning drafts

## Canonical storage

Server/backend/API slice drafts live directly under:

```text
planning/slices/
```

Client sidecar drafts live under:

```text
planning/slices/l2/
```

Shared slice-family notes and indexes may live under:

```text
planning/slices/l2/
```

only when they are navigation/context notes, not implementation slice drafts.

## Naming pattern

Server/backend/API slices:

```text
planning/slices/SL-<AREA>-<SUBAREA>-<NNN>-<kebab-title>.md
```

Client sidecars:

```text
planning/slices/l2/L2-<AREA>-<ACTION>-<NNN>-<kebab-title>.client.md
```

Cross-cutting concerns:

```text
planning/slices/cross-cutting/CC-<AREA>-<NNN>-<kebab-title>.md
```

## Current L2 review slice naming

```text
planning/slices/SL-EMP-REQ-001-employee-request-list-read.md
planning/slices/SL-EMP-REQ-002-employee-request-details-read.md
planning/slices/SL-EMP-REQ-003-start-request-review.md
planning/slices/SL-EMP-REQ-004-approve-request-review.md
planning/slices/SL-EMP-REQ-005-reject-request-review.md

planning/slices/l2/L2-EMP-DASH-001-employee-request-dashboard.client.md
planning/slices/l2/L2-EMP-DETAILS-001-employee-request-details.client.md
planning/slices/l2/L2-REVIEW-START-001-start-request-review.client.md
planning/slices/l2/L2-REVIEW-APPROVE-001-approve-request-review.client.md
planning/slices/l2/L2-REVIEW-REJECT-001-reject-request-review.client.md
```

## Current AgreementProposalExchange slice naming

```text
planning/slices/SL-AGR-EXCH-001-start-agreement-exchange-with-initial-employee-proposal.md
planning/slices/SL-AGR-EXCH-002-send-agreement-counter-proposal-version.md
planning/slices/SL-AGR-EXCH-003-read-agreement-exchange.md
planning/slices/SL-AGR-EXCH-004-accept-active-agreement-proposal.md
planning/slices/SL-AGR-EXCH-005-final-refuse-agreement-exchange.md
```

## Rules

```text
- Do not create server slice drafts under planning/slices/l2.
- Do not put client sidecar drafts directly under planning/slices unless there is a deliberate migration decision.
- Do not create duplicate names such as SL-AGR-001 and SL-AGR-EXCH-001 for the same behavior.
- Keep file names stable once implementation prompts start referencing them.
- If a file is renamed, update README, source register, questions register, extension points and implementation notes in the same archive.
```

## Existing transitional notes

A family note such as:

```text
planning/slices/l2/L2-agreement-exchange-slice-family.md
```

is navigation/context only. The implementation slice drafts are the `SL-AGR-EXCH-001..005` files under `planning/slices/`.
