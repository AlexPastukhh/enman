# Slice Planning Index

Status: current slice-planning navigation index  
Scope: scenario-derived slice boundary discovery, per-slice implementation planning, Client/UI flow and shared support

## 1. Purpose

This folder documents how to derive implementation slices from scenarios and how to plan implementation one slice at a time.

A slice is not a controller, endpoint, repository, table, React component, aggregate, shared helper, or a random task.

Working definition:

```text
Slice = independently testable unit of observable behavior
        + implementation path needed to deliver/test that behavior.
```

A slice can be full-stack, backend/API/persistence-only, read/query, Client/UI-only, extension, dependent, plugin/external-integration, framework/auth-related, or shared-support-assisted.

## 2. Current Two-Level Slice Workflow

### Level 1 — General L1 slice boundary draft

Use:

```text
planning/slices/l1-slice-boundary-draft-01.md
```

Purpose:

```text
- discover real L1 slices from scenarios;
- prove why each candidate is a valid slice;
- show Scenario Slice Flow for each slice;
- embed DATA, behavior items, invariants and no-write rules into scenario flow steps;
- identify dependent/extension/read/Client-UI/plugin/shared-support-assisted behavior;
- split L1/L2/later package behavior;
- collect boundary-level decisions, questions and ADR candidates.
```

The general boundary draft answers:

```text
What are the slices and why?
```

It must not contain detailed implementation flow.

### Level 2 — Per-slice implementation files

Use per-slice files such as:

```text
planning/slices/SL-ACC-001-register-client-account.md
planning/slices/SL-APPL-001-create-individual-applicant-party.md
planning/slices/SL-REQ-001-create-connection-request.md
planning/slices/SL-REVIEW-001-approve-request-and-verify-applicant.md
planning/slices/SL-REVIEW-002-reject-request.md
```

Purpose:

```text
- duplicate/refine Scenario Slice Flow from the boundary draft;
- add Implementation Flow;
- put Client/UI Flow first when the slice has any user-visible client behavior;
- describe concrete pages/screens, components/forms, buttons/actions, fields, validation placement, success behavior and navigation;
- plan API/application/domain/persistence/read/auth/infra work after client flow;
- separate tests into a dedicated test-plan section;
- split tests into Client tests, Server tests and End-to-end tests;
- add detailed implementation notes, pseudocode or code snippets when needed;
- create an implementation checklist for that slice.
```

A per-slice file answers:

```text
How will this slice be implemented and tested?
```

## 3. Client/UI Meaning

`Client/UI` means more than visual markup.

It includes:

```text
- page/screen/component structure;
- form fields and buttons/actions;
- client-side state;
- deferred/client-side validation;
- API request DTO building;
- API client call;
- CSRF/antiforgery request token use for unsafe requests;
- server validation/problem mapping to field/global errors;
- success result handling;
- navigation after success;
- client tests.
```

When a slice has any client-visible behavior, `Implementation Flow` starts with:

```text
I01 — Client/UI Flow
```

End-to-end tests are placed at the end of the slice file, after Client and Server test planning.

## 4. Shared Support Artifacts

Reusable helpers used by multiple slices are documented under:

```text
planning/slices/shared/
```

Shared support is not automatically a business slice.

Use marker:

```text
[SHARED SUPPORT][CROSS-SLICE]
```

A helper/support artifact is not a slice when:

```text
- it has no independent observable business behavior;
- it exists to support multiple slices;
- it is tested through helper tests plus slice-specific client/server tests;
- it does not define a scenario boundary.
```

Current shared support docs:

```text
planning/slices/shared/README.md
planning/slices/shared/client-server-validation-error-mapping.md
planning/slices/shared/client-deferred-validation.md
planning/slices/shared/antiforgery-token-session-context.md
planning/slices/shared/client-applicant-data-prefill-notes.md
```

## 5. Relationship To L1 Domain Foundation

The L1 domain implementation cut is a domain-foundation cut, not a full scenario slice.

After the L1 domain foundation is green, slice planning becomes the main implementation planning unit.

Do not treat `planning/l1-domain-implementation-cut.md` as a replacement for slice planning.

## 6. Current Slice Rules

Slice-planning agents should:

```text
- start from scenarios, not endpoints;
- derive slices from observable behavior;
- use independent testability as a primary boundary criterion;
- embed DATA, behavior items, invariants and no-write rules into Scenario Slice Flow;
- split command/read/Client-UI/extension/dependent/plugin behavior when they can be implemented or tested separately;
- do not force all parts of one scenario into one slice;
- do not mix L1 and later-package behavior into one slice if they are separately testable;
- keep detailed implementation flow in per-slice files, not in the general boundary draft;
- put Client/UI Flow first in per-slice Implementation Flow when the slice has any client-visible behavior;
- describe concrete UI pages/screens/components/forms/buttons/actions/fields/error placement/navigation;
- collect boundary questions and decisions in the general boundary draft;
- collect slice-specific implementation questions in per-slice files;
- collect ADR candidates when decisions affect multiple slices or architecture boundaries.
```

## 7. Marker Model

Use these markers:

```text
[L1]              current L1 package slice
[L2]              later package slice
[EXTENSION]       adds behavior to an existing slice
[DEPENDENT]       depends on another slice
[CLIENT/UI]       client/UI-focused slice or client-visible behavior
[READ]            read/query slice
[AUTH/FRAMEWORK]  auth/framework/application guard concern
[PLUGIN]          external provider / replaceable integration
[CROSS-CUTTING]   concern used by multiple slices
[SHARED SUPPORT]  reusable support artifact, not a business slice by default
[PARTIAL]         partially implemented
[IMPLEMENTED]     implemented for its declared scope
```

Important:

```text
Protected active account guard is not a standalone business slice.
It is [AUTH/FRAMEWORK][CROSS-CUTTING] and is implemented through application service / auth boundary inside protected slices.
```

Antiforgery token/session-context support is also not a business slice:

```text
[SHARED SUPPORT][AUTH/FRAMEWORK][CROSS-SLICE]
```

It supports unsafe client requests across slices that use ASP.NET Core cookie auth.

## 8. Source Basis For Slice Drafts

Slice drafts are derived from:

```text
planning/diagrams/scenario-text-specs/
planning/diagrams/scenario-data/
planning/diagrams/scenario-text-specs/scenario-server-domain-validation-addendum.md
planning/tables/pre-domain-variants-input.md
planning/tables/scenario-behavior-baseline-account-activation-addendum.md
planning/tables/domain-drafts/domain-draft-01.md
planning/l1-domain-implementation-cut.md
planning/l1-domain-testing-rules.md
```

Client-side validation exists in the scenario validation addendum. If stable behavior IDs are missing, add client/UI candidate items inside the slice file first.

After implementation starts, also use actual L1 implementation result and tests.

## 9. Current Files

General boundary draft:

```text
planning/slices/l1-slice-boundary-draft-01.md
```

Implemented or partially implemented slice files:

```text
planning/slices/SL-ACC-001-register-client-account.md
planning/slices/SL-APPL-001-create-individual-applicant-party.md
planning/slices/SL-REQ-001-create-connection-request.md
planning/slices/SL-REVIEW-001-approve-request-and-verify-applicant.md
planning/slices/SL-REVIEW-002-reject-request.md
```

Guide:

```text
planning/slices/l1-slice-drafting-guide.md
```

Shared support:

```text
planning/slices/shared/README.md
planning/slices/shared/client-server-validation-error-mapping.md
planning/slices/shared/client-deferred-validation.md
planning/slices/shared/antiforgery-token-session-context.md
planning/slices/shared/client-applicant-data-prefill-notes.md
```

Expected future files:

```text
planning/slices/SL-REQ-READ-001-my-requests-visibility.md
planning/slices/SL-EMP-READ-001-employee-request-dashboard.md
planning/slices/SL-REQ-UI-001-request-creation-client-ui.md
planning/slices/SL-REVIEW-UI-001-employee-review-client-ui.md
planning/slices/l1-slice-delivery-plan.md
```

Do not create future files until explicitly requested.
