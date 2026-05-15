# Client Planning Index

Status: current client planning navigation  
Scope: client-wide UI/client conventions and cross-cutting client behavior

## 1. Purpose

This folder contains client-wide planning docs that are broader than a single slice sidecar.

It complements:

```text
planning/slices/client-architecture-principles.md
planning/slices/client-component-discovery-guide.md
planning/slices/l1-slice-drafting-guide.md
planning/diagrams/scenario-ui-specs/
```

## 2. Responsibility

This folder owns:

```text
- client-wide UI conventions;
- client-wide accessibility conventions;
- client-wide styling conventions;
- client-wide form validation conventions;
- client-wide error mapping conventions;
- client behavior conventions reused by multiple `.client.md` sidecars.
```

It does not own concrete scenario behavior, concrete slice implementation flow, backend API contracts or domain rules.

## 3. Current Files

```text
planning/client/cross-cutting/README.md
planning/client/cross-cutting/CL-FORM-VALIDATION-001-deferred-validation.md
planning/client/cross-cutting/CL-ERROR-HANDLING-001-client-server-errors.md
planning/client/cross-cutting/CL-STYLING-001-css-modules-tokens.md
planning/client/cross-cutting/CL-A11Y-001-accessibility-and-aria.md
```

## 4. Relationship To `planning/slices/shared`

`planning/client/cross-cutting/` owns client-wide conventions.

`planning/slices/shared/` may keep slice-near reusable support notes, especially when directly referenced by slice planning.

If a rule applies to all client sidecars, prefer `planning/client/cross-cutting/`.
