# Behavior Items Master Index

Status: current behavior items master index  
Scope: scenario-derived and concern-derived behavior item files

## 1. Scenario-Derived Behavior Items

Scenario-derived files are created per scenario when scenario behavior is prepared for domain/slice/client planning.

## 2. Cross-Cutting / Concern-Derived Behavior Items

| File | Source type | Primary slice |
|---|---|---|
| `CC-CSRF-001-antiforgery-behavior-items.md` | security-derived cross-cutting | `planning/slices/cross-cutting/CC-CSRF-001-antiforgery-token-session-context.md` |

## 3. Rule

Every behavior item file must identify whether its source is:

```text
scenario-derived
security-derived
API-contract-derived
tooling-derived
testing-derived
client-cross-cutting-derived
infrastructure-derived
```

Cross-cutting behavior items must be reflected in the corresponding cross-cutting/helper slice Concern Flow before Implementation Flow.
