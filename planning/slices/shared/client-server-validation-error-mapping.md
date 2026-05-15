# Shared Support — Client/Server Validation Error Mapping

Status: support note  
Marker: `[SHARED SUPPORT][CLIENT/UI][CROSS-SLICE]`

## Purpose

Describe shared support for mapping server validation or problem responses into client-visible errors.

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

## Tests

```text
- helper tests for mapping known server error shape;
- per-slice client tests for field/global error rendering;
- E2E tests only for complete user flows.
```
