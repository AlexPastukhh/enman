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

A support artifact is not a slice when:

```text
- it has no independent observable business behavior;
- it supports multiple slices;
- it does not define a scenario boundary;
- it is tested through helper tests plus slice-specific tests.
```

## 2. Current Shared Support Documents

```text
planning/slices/shared/client-deferred-validation.md
planning/slices/shared/client-server-validation-error-mapping.md
planning/slices/shared/antiforgery-token-session-context.md
planning/slices/shared/client-applicant-data-prefill-notes.md
```

## 3. Current Support Areas

| Support artifact | Marker | Used by | Why separate |
|---|---|---|---|
| Deferred client validation | [SHARED SUPPORT][CLIENT/UI] | form-based Client/UI slices | Repeated delayed validation behavior |
| Client/server validation error mapping | [SHARED SUPPORT][CLIENT/UI] | form/API slices | Consistent field/global error display |
| Antiforgery token/session context | [SHARED SUPPORT][AUTH/FRAMEWORK] | unsafe cookie-auth requests | Cross-slice CSRF protection |
| Applicant data prefill notes | [SHARED SUPPORT][CLIENT/UI] | request creation Client/UI | Reusable notes before concrete slice implementation |

## 4. Rule

Do not turn shared support into a business slice unless it becomes independently observable behavior.

Describe concrete usage inside each per-slice file.
