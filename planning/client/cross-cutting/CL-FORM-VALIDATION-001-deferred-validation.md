# CL-FORM-VALIDATION-001 — Deferred Client Validation

Status: current client-wide convention  
Type: client cross-cutting behavior

## 1. Purpose

Define the shared convention for client-side deferred validation.

## 2. Behavior

```text
user edits input
-> validation waits for configured delay/debounce
-> field-level error appears near field if invalid
-> valid input clears field-level error
-> submit triggers immediate validation
-> global/form-level errors may appear in form error area
```

## 3. What Sidecars Must State

```text
- which fields validate after delay;
- which fields validate immediately;
- what submit does;
- where field-level errors appear;
- where global/form-level errors appear;
- how server errors combine with client errors;
- which tests cover delayed validation.
```

## 4. Change Points

| ID | Behavior aspect | Change point | Current decision |
|---|---|---|---|
| CP-CL-FORM-001 | When errors appear | validation delay / form hook config | deferred after input, immediate on submit |
| CP-CL-FORM-002 | Error placement | field/global error rendering | field-level near fields, global at form level |

## 5. Tests

Prefer tests that assert user-visible behavior.
