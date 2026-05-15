# Slice Implementation Principles

Status: current common implementation principles  
Scope: general implementation rules near slice planning

## 1. Purpose

This file stores stable implementation principles.

It is not a backlog of future implementation thoughts. Future concrete notes belong in:

```text
planning/slices/slice-implementation-notes-register.md
```

Reusable mechanisms belong in:

```text
planning/slices/shared/
```

## 2. Parent Slice And Client Sidecar

Parent slice file owns:

```text
- vertical behavior;
- Scenario Slice Flow;
- behavior item coverage summary;
- API contract;
- server/client shared expectations;
- application/domain/persistence responsibilities;
- server/integration tests.
```

Client sidecar owns detailed frontend/client implementation when client work starts:

```text
- route constants;
- routes;
- views;
- components;
- query functions;
- mutation functions;
- hooks;
- client state;
- form state;
- DTO mapping;
- cache invalidation;
- UI states;
- client tests.
```

Do not create `.client.md` before work starts on the concrete client layer.

## 3. API Contract Ownership

The API contract belongs in the parent slice file.

Client sidecar must reference the parent slice API section and must not invent a different backend contract.

## 4. Client Sidecar Starts With Coverage And Questions

A `.client.md` file starts with fast-access planning tables:

```text
1. Client Behavior Coverage
2. Client Implementation Questions Register
3. Scenario / DATA Coverage
4. API Contract Used By Client
```

Only then it describes routes, views, components, query functions, mutations, hooks, DTO mapping, action availability, success/failure handling, cache and tests.

## 5. Form Values vs API DTO

Client form values may differ from API DTOs.

Use separate FormValues and DTO mapping when there is:

```text
trim / normalization
empty string -> null
confirmation fields
UI-only checkbox/state
select string -> number conversion
flat form -> nested DTO
File/FormData upload
filters/search values
warning/confirmation state that should not be sent to server
```

Detailed reusable note:

```text
planning/slices/shared/client-form-values-to-api-dto-mapping.md
```

## 6. Deferred Validation

Deferred validation is current client-side behavior and may be tested.

Detailed reusable note:

```text
planning/slices/shared/client-deferred-validation.md
```

## 7. Server Errors To Client Errors

Server validation/problem responses should map to client-readable field/global errors.

Detailed reusable note:

```text
planning/slices/shared/client-server-validation-error-mapping.md
```

## 8. CSRF / Antiforgery With Cookie Auth

Unsafe requests using ASP.NET Core cookie auth need antiforgery support.

This is shared support, not a business slice.

Detailed reusable note:

```text
planning/slices/shared/antiforgery-token-session-context.md
```

## 9. Applicant Data Prefill

Applicant data may be prefilled into request forms.

Editing request-local fields must not mutate saved ApplicantParty.

Detailed reusable note:

```text
planning/slices/shared/client-applicant-data-prefill-notes.md
```

## 10. Tests

Separate tests from implementation narrative:

```text
Client tests
Server tests
End-to-end tests
```

End-to-end tests go last because they verify the assembled full flow.
