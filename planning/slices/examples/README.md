# Slice Examples

Status: current examples index  
Scope: example slice drafts used by slice drafting workflow docs

## Purpose

This folder contains examples only.

Example files are not authoritative implementation plans unless explicitly copied into an active slice file and reconciled with current repo state.

Use examples to understand formatting, flow structure, question/assumption style, extension/change point handling, coverage style and verification planning.

## Current Examples

| Example | Type | Use when |
|---|---|---|
| `L1-APPLICANT-PARTY-READ-CURRENT-early-short-draft-example.md` | current primary shortened backend read/API slice draft example | You need an early discussion draft that includes visual flows, assumptions, extension/change points, register sync, behavior coverage and test planning |
| `L1-CONNECTION-REQUEST-CREATE-early-short-draft-example.md` | older/simple shortened backend command slice draft example | You need a compact early command-slice example; prefer the read-current example when extension/change points matter |
| `SL-ACC-001-register-client-account-full-slice-example.md` | valid full backend slice draft example | You need a full parent backend slice file with visual maps before detailed flows |

## Rules

```text
- Do not treat example content as current implementation evidence.
- Do not copy example behavior into a slice without checking current scenario docs and current repo code.
- Keep visual flow maps before detailed flow sections in full slice examples.
- Keep shortened examples clearly marked as early working drafts.
- Prefer the read-current early short example when a draft needs extension points, change points, assumptions or shared register sync.
- Keep local questions/status/assumptions synchronized with shared registers when the example pattern is copied into a real slice.
```

Primary workflow:

```text
planning/slices/l1-slice-drafting-guide.md
```
