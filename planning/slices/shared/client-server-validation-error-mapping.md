# Shared Support — Client/Server Validation Error Mapping

Status: support note  
Marker: `[SHARED SUPPORT][CLIENT/UI][CROSS-SLICE]`

## Purpose

Describe shared support for mapping server validation or problem responses into client-visible errors.

## Why This Is Not A Slice

Users do not perform “error mapping” as an independent business action.

This support is used by multiple Client/UI slices and should be tested through helper tests plus per-slice client tests.

## Client Responsibility

```text
- receive server validation/problem response;
- map known field errors to concrete form fields;
- show unknown or global errors in form-level/global error area;
- preserve user input where appropriate;
- avoid hiding server errors behind generic messages unless required by security.
```

## Server Responsibility

```text
- return stable enough validation/problem shape;
- keep field names/error codes predictable enough for client mapping;
- distinguish validation problems from auth/session/server failures where possible.
```

## Used By

```text
SL-REQ-UI-001 — request creation form
SL-APPL-UI-001 — applicant data form
SL-REVIEW-UI-001 — employee review form/actions
SL-AUTH-UI-001 — auth forms
```

## Tests

```text
- helper tests for mapping known server error shape;
- per-slice client tests for field/global error rendering;
- E2E tests only for complete user flows.
```
