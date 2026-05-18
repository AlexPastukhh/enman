# Client Form Validation Workflow

Status: current client-wide validation convention

## 1. Behavior

```text
user edits input
  -> validation waits for configured delay/debounce
  -> field-level error appears near field if invalid
  -> valid input clears field-level error
  -> submit triggers immediate validation
  -> global/form-level errors appear in form error area
```

## 2. Client slice drafts must state

```text
which fields validate after delay
which fields validate immediately
what submit does
where field-level errors appear
where global/form-level errors appear
how server ProblemDetails combine with client errors
which tests cover delayed validation
```

## 3. Auth autocomplete

```text
email:
  autocomplete="email"

login password:
  autocomplete="current-password"

register password:
  autocomplete="new-password"

confirm password:
  autocomplete="new-password"
```

## 4. Error display

Do not show only:

```text
Something went wrong
```

when server provides useful field/domain messages.

Use:

```text
field error near field
root/domain error in form alert area
```

## 5. Server errors

Server remains source of truth.

Client validation improves UX, but server ProblemDetails must be shown in a way the user can act on.

## 6. Tests

Prefer tests that assert user-visible behavior:

```text
field-level error appears after delay
submit validates immediately
server field error maps to field
server root/domain error appears in form alert area
```
