# Shared Support — Client Deferred Validation

Status: support note  
Marker: `[SHARED SUPPORT][CLIENT/UI][CROSS-SLICE]`

## Purpose

Document the recurring client-side validation pattern where validation happens after a short delay following user input.

This is important because current client tests already cover delayed/deferred validation behavior.

## Why This Is Not A Slice

Deferred validation is not independent business behavior.

It supports Client/UI slices such as:

```text
SL-REQ-UI-001 — Request creation Client/UI
SL-APPL-UI-001 — Applicant data Client/UI
SL-REVIEW-UI-001 — Employee review Client/UI
```

## Expected Behavior

```text
- user changes an input;
- client waits for configured delay/debounce;
- validation runs;
- field-level errors appear near the relevant field;
- global/form-level errors appear in a shared location if needed;
- submit can trigger immediate validation;
- valid input clears the relevant error.
```

## Slice Usage Notes

Per-slice files should specify:

```text
- which page/form uses deferred validation;
- which fields validate after delay;
- which errors are field-level;
- which errors are global/form-level;
- what happens on submit;
- which client tests cover delayed validation.
```

## Tests

Shared helper tests may verify debounce/timing behavior.

Slice client tests should verify concrete form behavior.
