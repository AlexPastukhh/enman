# CL-ERROR-HANDLING-001 — Client / Server Error Mapping

Status: current client-wide convention  
Type: client cross-cutting behavior

## 1. Purpose

Define how server validation/problem responses become client-visible field/global errors.

## 2. Client Responsibility

```text
- receive server validation/problem response;
- map known field errors to concrete fields;
- show unknown/global errors in form-level or page-level error area;
- preserve user input where appropriate;
- avoid hiding actionable server errors behind generic messages.
```

## 3. Error UI Placement

| Error type | UI placement | Notes |
|---|---|---|
| Field validation | near field | Use associated field label/error relation when possible |
| Form/global validation | form error area | Does not belong to one field |
| Stale/domain conflict | action/page error area | Example: request already processed |
| Forbidden | page-level forbidden state or action error | Depends on route/action context |
| Unexpected server failure | page/form error area | Avoid technical details |

## 4. Change Points

| ID | Behavior aspect | Change point | Current decision |
|---|---|---|---|
| CP-CL-ERR-001 | Domain/application error mapping | problem response parser | use stable mapper |
| CP-CL-ERR-002 | Field/global split | mapping rules | field errors near field, unknown errors global |
| CP-CL-ERR-003 | Auth/forbidden display | page/action-level handling | route or action context decides |
