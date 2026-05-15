# Slice Implementation Principles

Status: current common implementation principles  
Scope: general implementation rules near slice planning

## 1. Purpose

This file stores stable implementation principles.

Future concrete notes belong in `planning/slices/slice-implementation-notes-register.md`.

Cross-slice extension/change decisions belong in `planning/slices/slice-extension-points-register.md`.

Client-wide conventions belong in `planning/client/`.

## 2. Parent Slice And Client Sidecar

Parent slice file owns:

```text
- vertical behavior;
- Scenario Slice Flow;
- behavior item coverage summary;
- API contract;
- server/client shared expectations;
- Server / Cross-Layer Extension Points;
- Server / Cross-Layer Change Points;
- Extension Pressure / Anti-Coupling Decisions;
- application/domain/persistence responsibilities;
- server/integration tests.
```

Client sidecar owns detailed frontend/client implementation when client work starts:

```text
- Client Behavior Coverage;
- UI Behavior Coverage;
- Client Implementation Questions Register;
- Scenario / DATA / UI Spec Coverage;
- Client Architecture Mapping;
- Client Extension Points;
- Client Behavior Change Points;
- Client Extension Pressure / Anti-Coupling Decisions;
- Component / Layout Plan;
- Styling Change Points;
- Accessibility / ARIA Contract;
- API Contract Used By Client;
- routes/pages/entities/features/widgets/components/hooks;
- form state;
- DTO mapping;
- cache invalidation;
- UI states;
- client tests.
```

## 3. API Contract Ownership

The API contract belongs in the parent slice file.

Client sidecar must reference the parent slice API section and must not invent a different backend contract.

## 4. Change / Extension Points

Use:

```text
planning/slices/change-extension-points-principles.md
planning/slices/slice-extension-points-register.md
```

Known future extension points must influence current implementation planning.

For each extension pressure case, decide explicit seam now, anti-coupling only, convention-first and accept possible future refactor, ignore because certainty is low, or revisit later.

## 5. Client Architecture And Component Planning

Use:

```text
planning/slices/client-architecture-principles.md
planning/slices/client-component-discovery-guide.md
planning/client/
```

A `.client.md` file starts with coverage/questions tables, then architecture, extension/change points, component/layout plan, styling and accessibility, then implementation flow.

## 6. Form Values vs API DTO

Client form values may differ from API DTOs.

Use separate FormValues and DTO mapping when there is trim/normalization, empty string to null, confirmation fields, UI-only state, select conversion, nested DTO shape, File/FormData, filters/search values, or warning/confirmation state that should not be sent to server.

## 7. Client-Wide Conventions

Use:

```text
planning/client/cross-cutting/CL-FORM-VALIDATION-001-deferred-validation.md
planning/client/cross-cutting/CL-ERROR-HANDLING-001-client-server-errors.md
planning/client/cross-cutting/CL-STYLING-001-css-modules-tokens.md
planning/client/cross-cutting/CL-A11Y-001-accessibility-and-aria.md
```

## 8. CSRF / Antiforgery With Cookie Auth

Unsafe requests using ASP.NET Core cookie auth need antiforgery support.

Detailed reusable note may remain in `planning/slices/shared/antiforgery-token-session-context.md`.

## 9. Applicant Data Prefill

Applicant data may be prefilled into request forms.

Editing request-local fields must not mutate saved ApplicantParty.

## 10. Tests

Separate tests from implementation narrative:

```text
Client tests
Server tests
End-to-end tests
```
