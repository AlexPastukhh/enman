# Slice Planning Index

Status: current slice-planning navigation index / ApplicantParty template-per-type model synchronized  
Scope: business slices, client sidecars, cross-cutting/helper slices, source-flow/behavior register, examples, shared registers, API contract artifacts, client architecture, extension/change points and implementation notes

## 1. Purpose

This folder documents how to derive implementation slices from scenarios/concerns and how to plan implementation one slice at a time.

It is also the entry point for slice-wide shared registers.

## 2. Draft-Driven Discovery

All slice families use draft-driven discovery:

```text
planning/slices/draft-driven-discovery-principles.md
planning/slices/l1-slice-drafting-guide.md
```

## 3. Scenario Flow / Behavior Source Rule

Before writing Scenario Flow, Visual Scenario Flow, Behavior Coverage or Covered Behavior Items, read:

```text
planning/slices/slice-scenario-flow-behavior-register.md
```

That register points to actual source artifacts:

```text
planning/diagrams/scenario-text-specs/
planning/diagrams/scenario-data/
planning/diagrams/scenario-ui-specs/
planning/diagrams/scenario-behavior-items/
```

Do not use `slice-questions-register.md`, `slice-extension-points-register.md` or `slice-implementation-notes-register.md` as the source for scenario flow or behavior items.

## 4. Current Backend Slice Files

Current implemented or first-stage backend/API/persistence/session slice docs:

```text
planning/slices/SL-ACC-001-register-client-account.md
planning/slices/SL-AUTH-001-login-client-account.md
planning/slices/SL-AUTH-002-current-user.md
planning/slices/SL-AUTH-003-logout.md
planning/slices/SL-APPL-001-create-individual-applicant-party.md
planning/slices/SL-REQ-001-create-connection-request.md
planning/slices/SL-REQ-002-my-requests-list.md
planning/slices/SL-REQ-003-own-request-details.md
```

Applicant target-model / planned implementation slices:

```text
planning/slices/SL-APPL-002-account-applicant-parties-read.md
planning/slices/SL-APPL-003-select-current-default-applicant-party-template.md
planning/slices/SL-APPL-004-applicant-party-creation-application-service.md
```

Request/applicant target redesign slice:

```text
planning/slices/SL-REQ-004-create-request-with-applicant-context.md
```

Status note:

```text
SL-APPL-001 is implemented as a narrow create endpoint but has target behavior reconciliation notes.
The target model is many saved ApplicantParties plus one current/default template per applicant type.
Do not claim target default-template/request-applicant-context behavior is implemented until repo evidence proves it.
```

## 5. Current Client Sidecar Files

Implemented/first-stage or implementation-ready client sidecars currently documented:

```text
planning/slices/SL-ACC-001-register-client-account.client.md
planning/slices/SL-AUTH-001-login-client-account.client.md
planning/slices/SL-AUTH-002-current-user.client.md
planning/slices/SL-AUTH-003-logout.client.md
planning/slices/SL-APPL-001-create-individual-applicant-party.client.md
```

Do not create new `.client.md` files before concrete client work starts or implemented client logic must be documented and reconciled.

## 6. Slice Support Files

```text
planning/slices/draft-driven-discovery-principles.md
planning/slices/l1-slice-drafting-guide.md
planning/slices/implementation-principles.md
planning/slices/client-architecture-principles.md
planning/slices/client-component-discovery-guide.md
planning/slices/change-extension-points-principles.md
planning/slices/slice-scenario-flow-behavior-register.md
planning/slices/slice-questions-register.md
planning/slices/slice-extension-points-register.md
planning/slices/slice-implementation-notes-register.md
planning/slices/shared/README.md
planning/slices/cross-cutting/README.md
planning/slices/examples/README.md
```

## 7. Slice Registers

| Register | Responsibility |
|---|---|
| `slice-scenario-flow-behavior-register.md` | maps slices/client sidecars to scenario flow, DATA, UI scenario and behavior item source files |
| `slice-questions-register.md` | shared overview of currently relevant local questions, including questions from implemented slices |
| `slice-extension-points-register.md` | extension points, change pressure, anti-coupling decisions and extension/change questions |
| `slice-implementation-notes-register.md` | future implementation/client/testing notes not yet assigned or already promoted |

Implemented slices can still have open/future/assumption questions.

Those questions must remain in local `Questions / Decisions` sections and be mirrored to the shared registers when relevant.

## 8. ApplicantParty Target Model

```text
A client account may store many ApplicantParties over time.
One current/default ApplicantParty template may exist per applicant type.
Creating first ApplicantParty of a type may initialize default.
Creating another ApplicantParty of the same type does not silently change default.
Changing default when one exists is explicit future behavior.
Request creation with new applicant data should be atomic in one server call.
```

Use scenario sources:

```text
planning/diagrams/scenario-text-specs/SC-10-applicant-data.md
planning/diagrams/scenario-text-specs/SC-10B-my-applicant-parties.md
planning/diagrams/scenario-text-specs/SC-04-client-request-creation.md
```

## 9. Cross-Cutting / Helper Slices

Use:

```text
planning/slices/cross-cutting/
planning/slices/shared/
```

Current cross-cutting/helper directions include OpenAPI, constants, CSRF, Maybe optional results and ApplicantParty creation service extraction.

## 10. Testing Support

Testing workflow lives in:

```text
planning/testing/
```

Browser E2E should wait until concrete client/read UI exists and the cross-layer behavior is stable.
