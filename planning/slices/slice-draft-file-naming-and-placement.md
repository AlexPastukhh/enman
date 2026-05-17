# Slice Draft File Naming And Placement

Status: current / predictable slice draft storage rule

## 1. Server / Backend / API Slice Drafts

Server/backend/API slice drafts live directly under:

```text
planning/slices/
```

Filename pattern:

```text
SL-<AREA>-<SUBAREA>-<NNN>-<kebab-title>.md
```

Examples:

```text
planning/slices/SL-EMP-REQ-004-approve-request-review.md
planning/slices/SL-AGR-EXCH-001-start-agreement-exchange-with-initial-employee-proposal.md
```

## 2. Client Sidecar Drafts

Client sidecar drafts live under:

```text
planning/slices/l2/
```

Filename pattern:

```text
L2-<AREA>-<ACTION>-<NNN>-<kebab-title>.client.md
```

Examples:

```text
planning/slices/l2/L2-REVIEW-START-001-start-request-review.client.md
planning/slices/l2/L2-REVIEW-REJECT-001-reject-request-review.client.md
```

## 3. Family / Index Notes

Family/index notes live under:

```text
planning/slices/l2/
```

when they explain L2 family planning, for example:

```text
planning/slices/l2/L2-agreement-exchange-slice-family.md
planning/slices/l2/L2-agreement-exchange-domain-invariants-and-actor-access.md
```

They are not implementation slices and should not be named with `SL-*`.

## 4. Cross-Cutting Concerns

Cross-cutting concern docs live under:

```text
planning/slices/cross-cutting/
```

Filename pattern:

```text
CC-<AREA>-<NNN>-<kebab-title>.md
```

## 5. Agreement Exchange Canonical Server Draft Names

```text
planning/slices/SL-AGR-EXCH-001-start-agreement-exchange-with-initial-employee-proposal.md
planning/slices/SL-AGR-EXCH-002-send-agreement-counter-proposal-version.md
planning/slices/SL-AGR-EXCH-003-read-agreement-exchange.md
planning/slices/SL-AGR-EXCH-004-accept-active-agreement-proposal.md
planning/slices/SL-AGR-EXCH-005-final-refuse-agreement-exchange.md
```

## 6. Rule

Do not create duplicate slice drafts with alternate names or mixed placement.

If a slice changes scope, update the existing canonical file and registers rather than creating a new near-duplicate.
