# Scenario Behavior Items Index

Status: current behavior items index  
Scope: per-scenario behavior items, UI-scenario behavior items and cross-cutting concern-derived behavior items

## 1. Purpose

Behavior items make requirements traceable into domain drafts, slice drafts, implementation flows and tests.

They are source behavior items.

Slice/client chats should consume them, not invent replacements.

## 2. Behavior Item Source Types

Behavior items may be:

```text
scenario-derived
UI-scenario-derived
security-derived cross-cutting
API-contract-derived cross-cutting
tooling-derived cross-cutting
testing-derived cross-cutting
client-cross-cutting-derived
infrastructure-derived
```

Business slices primarily consume scenario-derived behavior items.

Client sidecars consume scenario-derived and `[UI-SCENARIO]` behavior items.

Cross-cutting/helper slices consume concern-derived behavior items.

## 3. Current Scenario-Derived Behavior Item Files

| File | Scenario | Scope |
|---|---|---|
| `SC-04-request-creation-behavior-items.md` | SC-04 Request Creation | request creation scenario/UI behavior, applicant data fields/prefill/clear behavior and request submit outcome |
| `SC-10-applicant-data-behavior-items.md` | SC-10 Applicant Data | Account page / applicant data behavior items, saved/read-only UI outcome and current-applicant read future work |

## 4. Source Register

Slice-facing source mapping lives in:

```text
planning/slices/slice-scenario-flow-behavior-register.md
```

Before writing Behavior Coverage, a slice/client chat must read that register and then read the linked behavior item files.

## 5. Cross-Cutting Behavior Item Rule

Cross-cutting behavior items are first-class behavior items.

They must:

```text
- state their source;
- be reflected in the cross-cutting/helper slice Concern Flow;
- be covered by implementation flow and tests or marked as not covered yet.
```

## 6. Current Cross-Cutting Behavior Item Files

```text
CC-CSRF-001-antiforgery-behavior-items.md
```

## 7. Related Cross-Cutting Slices

```text
planning/slices/cross-cutting/CC-CSRF-001-antiforgery-token-session-context.md
planning/slices/cross-cutting/CC-CONST-001-client-constants-generation-and-contract-testing.md
```
