# CC-CLIENT-FORM-VALIDATION-001 — Deferred Form Validation Behavior

Status: current first-pass behavior source  
Type: cross-cutting client behavior  
Applies to: client forms across auth, request, applicant, review and agreement flows

## 1. User Goal

The user can fill forms without immediate noisy errors on every keypress, but invalid input still becomes visible and actionable before/at submit.

## 2. Behavior Flow

```text
User edits a field
        ↓
UI waits briefly before showing validation error
        ↓
If field is invalid, field-level error appears near the field
        ↓
If field becomes valid, field-level error clears
        ↓
User submits form
        ↓
Client validates immediately
        ↓
If client-side invalid, visible errors appear
        ↓
If server rejects, server field/root errors are visible and actionable
```

## 3. Behavior Items

| ID | Behavior |
|---|---|
| `CC-CLIENT-FORM-VALIDATION-001-B01` | Field-level validation may be deferred while the user is typing. |
| `CC-CLIENT-FORM-VALIDATION-001-B02` | Submit validation is immediate. |
| `CC-CLIENT-FORM-VALIDATION-001-B03` | Field-level errors appear near the relevant field. |
| `CC-CLIENT-FORM-VALIDATION-001-B04` | Root/domain/server errors appear in a form-level error area. |
| `CC-CLIENT-FORM-VALIDATION-001-B05` | Server remains source of truth; client validation is UX support only. |
| `CC-CLIENT-FORM-VALIDATION-001-B06` | Auth forms use correct autocomplete values. |

## 4. Out of Scope

```text
specific React hook implementation
CSS file ownership
exact debounce duration unless a client slice draft chooses it
server validation implementation
```

## 5. Related Implementation Drafts

Future:

```text
planning/slices/client/cross-cutting/SINGLE-CC-CLIENT-FORM-VALIDATION-001-deferred-validation.client.md
```
