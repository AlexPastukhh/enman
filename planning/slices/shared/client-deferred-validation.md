# Shared Support — Client Deferred Validation

Status: support note  
Marker: `[SHARED SUPPORT][CLIENT/UI][CROSS-SLICE]`

## Purpose

Document the recurring client-side validation pattern where validation happens after a short delay following user input.

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

## Sidecar Usage

A `.client.md` file should specify:

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

Slice client tests verify concrete form behavior.
