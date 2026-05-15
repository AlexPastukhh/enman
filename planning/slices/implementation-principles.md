# Slice Implementation Principles

Status: current common implementation principles  
Scope: general implementation rules near slice planning

## 1. Cross-Cutting And Helper Slices

Cross-cutting/helper slices are allowed.

They are not business scenario slices, but they must still have:

```text
- observable/support behavior;
- implementation flow;
- test plan;
- consumers / used-by slices;
- coverage table;
- local questions;
- ADR impact when relevant.
```

Use:

```text
planning/slices/cross-cutting/
```

## 2. Implementation Flow Detail Filter

Implementation flow is behavior-first.

Detailed class/method/code explanations are included only when they clarify behavior, boundary, trade-off, error handling, testability, no-write/no-side-effect guarantees, generated artifact shape or API/client contract.

Routine code mechanics should be described high-level.

If flow becomes too noisy, extract detailed class/method reference into a sibling `.impl.md`.

Do not create `.impl.md` files in advance.

## 3. Constants Generation / Testing

Primary source:

```text
planning/slices/cross-cutting/CC-CONST-001-client-constants-generation-and-contract-testing.md
```

Core rules:

```text
- Generate Shared/constants.json and Shared/errorcodes.json by explicit Tools command.
- `--check` compares generated output with committed files and does not write.
- Client must not hardcode error code strings.
- API integration tests should read generated artifact for ordinary codes.
- Critical behavioral codes get literal integration contract tests.
```
