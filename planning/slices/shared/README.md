# Shared Slice Support Index

Status: current shared support index  
Scope: reusable client/server support used by multiple slices

## 1. Purpose

This folder documents reusable support mechanisms used by multiple slices.

Shared support is not automatically a business slice.

Use marker:

```text
[SHARED SUPPORT][CROSS-SLICE]
```

## 2. Current Shared Support Documents

```text
planning/slices/shared/client-deferred-validation.md
planning/slices/shared/client-server-validation-error-mapping.md
planning/slices/shared/client-form-values-to-api-dto-mapping.md
planning/slices/shared/antiforgery-token-session-context.md
planning/slices/shared/client-applicant-data-prefill-notes.md
```

## 3. Current Support Areas

| Support artifact | Marker | Used by | Why separate |
|---|---|---|---|
| Deferred client validation | [SHARED SUPPORT][CLIENT/UI] | form-based Client/UI sidecars | Repeated delayed validation behavior |
| Client/server validation error mapping | [SHARED SUPPORT][CLIENT/UI] | form/API slices | Consistent field/global error display |
| FormValues to API DTO mapping | [SHARED SUPPORT][CLIENT/UI] | form/mutation slices | Prevents coupling between UI form state and API contract |
| Antiforgery token/session context | [SHARED SUPPORT][AUTH/FRAMEWORK] | unsafe cookie-auth requests | Cross-slice CSRF protection |
| Applicant data prefill notes | [SHARED SUPPORT][CLIENT/UI] | request creation Client/UI | Reusable notes before concrete sidecar implementation |

## 4. Rule

Do not turn shared support into a business slice unless it becomes independently observable behavior.

Describe concrete usage inside each parent slice file or `.client.md`.
