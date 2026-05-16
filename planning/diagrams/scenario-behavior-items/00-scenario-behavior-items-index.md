# Behavior Items Master Index

Status: current behavior items master index  
Scope: scenario-derived, UI-scenario-derived and concern-derived behavior item files

## 1. Scenario-Derived / UI-S-Scenario-Derived Behavior Items

Scenario-derived files are created per scenario when scenario behavior is prepared for domain/slice/client planning.

| File | Scenario | Source type | Primary downstream use |
|---|---|---|---|
| `SC-04-request-creation-behavior-items.md` | SC-04 Request Creation | scenario-derived + UI-scenario-derived | request creation backend/client slices, applicant replacement/edit flow, My Requests/read flow |
| `SC-10-applicant-data-behavior-items.md` | SC-10 Applicant Data | scenario-derived + UI-scenario-derived | applicant data backend/client slices, current-applicant read slice, applicant replacement/edit slice |

## 2. Cross-Cutting / Concern-Derived Behavior Items

| File | Source type | Primary slice |
|---|---|---|
| `CC-CSRF-001-antiforgery-behavior-items.md` | security-derived cross-cutting | `planning/slices/cross-cutting/CC-CSRF-001-antiforgery-token-session-context.md` |

## 3. Rule

Every behavior item file must identify whether its source is:

```text
scenario-derived
UI-scenario-derived
security-derived
API-contract-derived
tooling-derived
testing-derived
client-cross-cutting-derived
infrastructure-derived
```

Cross-cutting behavior items must be reflected in the corresponding cross-cutting/helper slice Concern Flow before Implementation Flow.

Scenario-derived and UI-scenario-derived behavior items must trace back to scenario text, DATA, UI specs or validation/security addenda.

Slice/client chats must not use `slice-questions-register.md` as the source for Scenario Flow or Behavior Items.

Use:

```text
planning/slices/slice-scenario-flow-behavior-register.md
```
