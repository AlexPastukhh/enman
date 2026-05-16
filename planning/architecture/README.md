# Architecture Planning Index

Status: current / backend cleanup boundary navigation  
Scope: backend architecture boundary maps, legacy/L1 transition notes, cleanup safety maps and thesis/diploma architecture explanations

## 1. Purpose

This folder contains architecture-level planning notes that are broader than one slice, API contract or test workflow.

Use these files when a task needs to understand:

```text
- which backend parts are current L1 runtime;
- which backend parts are legacy runtime;
- which domain primitives are shared/current/future;
- which tests are current L1, legacy or shared;
- how cleanup should be staged without deleting shared primitives or changing runtime behavior accidentally.
```

## 2. Current Files

| File | Purpose | Status |
|---|---|---|
| `backend-legacy-and-l1-boundaries.md` | Backend legacy/L1 boundary map for Stage 2 cleanup, handler validation cleanup and thesis/diploma architecture explanation. | current boundary map |

## 3. Relationship To Other Planning Docs

```text
planning/README.md
planning/planning-workflow-current.md
planning/slices/cross-cutting/CC-VALIDATION-001-server-request-validation-and-fluentvalidation.md
planning/api/api-error-contract.md
planning/testing/README.md
```

Architecture notes do not replace slice docs, scenario sources, API contract docs or testing workflow docs.

They provide boundary context for cleanup and implementation prompts.
