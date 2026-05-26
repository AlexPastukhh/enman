# Client Form Validation Implementation Principles

Status: current client form validation implementation principles  
Scope: how client slices apply and verify deferred form validation behavior

## 1. Behavior Source

Canonical behavior source:

```text
planning/diagrams/scenario-cross-cutting/client-behavior/CC-CLIENT-FORM-VALIDATION-001-deferred-validation.behavior.md
```

This file does not define deferred validation behavior. It defines implementation principles for applying and verifying that behavior in client slices.

Future dedicated implementation draft:

```text
planning/slices/client/cross-cutting/SINGLE-CC-CLIENT-FORM-VALIDATION-001-deferred-validation.client.md
```

## 2. Core Implementation Principles

```text
client validation is UX support;
field-level validation may be delayed/debounced while the user types;
submit validation is immediate;
field-level errors appear near fields;
root/domain/server errors appear in a form-level error area;
server ProblemDetails should be shown in actionable UI;
auth fields use correct autocomplete values;
tests assert user-visible behavior, not hook internals.
```

## 3. Client Slice Drafts Must State

```text
which fields validate after delay;
which fields validate immediately;
what submit does;
where field-level errors appear;
where global/form-level errors appear;
how server ProblemDetails combine with client errors;
which behavior items from CC-CLIENT-FORM-VALIDATION-001 are covered;
which tests cover delayed validation and server-error mapping.
```

## 4. Auth Autocomplete

```text
email: autocomplete="email"
login password: autocomplete="current-password"
register password: autocomplete="new-password"
confirm password: autocomplete="new-password"
```

## 5. Error Display

Do not show only generic error text when server provides useful field/domain messages.

Use:

```text
field error near field
root/domain error in form alert area
```

## 6. Tests

Prefer tests that assert user-visible behavior:

```text
field-level error appears after delay;
submit validates immediately;
server field error maps to field;
server root/domain error appears in form alert area.
```

Use:

```text
planning/slices/slice-test-plan-workflow.md
```
