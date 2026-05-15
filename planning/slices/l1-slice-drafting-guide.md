# L1 Slice Drafting Guide

Status: current slice drafting workflow  
Scope: L1 slice boundary discovery, parent slice files, client sidecars, client architecture mapping, shared support and notes register

## 1. Purpose

This guide defines how to move from scenarios to independently testable implementation slices.

```text
scenario specs / DATA / validation-security
-> per-scenario behavior items
-> scenario questions register
-> L1 slice boundary draft
-> parent slice file
-> .client.md sidecar when concrete client work starts
-> implement one slice/client layer
-> update coverage / questions / decisions / ADR candidates
```

## 2. Working Definition

```text
Slice = independently testable unit of observable behavior
        + implementation path needed to deliver/test that behavior.
```

A slice is not a controller method, endpoint alone, repository, DB table, React component, aggregate alone, shared helper, or arbitrary technical task.

## 3. Scenario Question Rule

If a question affects scenario behavior, DATA, validation/security, API contract because scenario meaning is unclear, or user-visible outcome, stop and use the scenario question loop.

## 4. Slice Intake Checklist

Before starting any parent slice or `.client.md`:

```text
1. Read target scenario text spec.
2. Read target DATA file.
3. Read validation/security addendum entries.
4. Read relevant per-scenario behavior items if they exist.
5. Check planning/diagrams/scenario-questions-register.md.
6. Check planning/slices/slice-implementation-notes-register.md.
7. Check relevant shared support docs.
8. Check planning/slices/client-architecture-principles.md if client work is involved.
9. Promote relevant notes/questions.
10. If scenario-level ambiguity exists, stop and resolve it first.
```

## 5. General Boundary File

The general boundary file is:

```text
planning/slices/l1-slice-boundary-draft-01.md
```

It contains slice discovery, Scenario Slice Flow, boundary questions/decisions and dependencies.

It does not contain detailed implementation flow.

## 6. Parent Slice File

Parent slice files contain vertical implementation planning.

They include:

```text
- Slice overview;
- questions overview near the beginning;
- flow coverage overview near the beginning;
- Scenario Slice Flow;
- behavior item coverage summary;
- Implementation Flow;
- API contract;
- application/domain/persistence responsibilities;
- server/integration test plan;
- decisions;
- implementation checklist.
```

Parent slice owns API contract.

## 7. Client Sidecar Rule

If a slice has non-trivial client/UI work, create a `.client.md` file next to the parent slice file when client work starts.

Do not create `.client.md` in advance.

Client sidecar owns:

```text
- client behavior coverage table;
- client implementation questions register;
- scenario/DATA coverage table;
- client architecture mapping;
- API contract usage from parent slice;
- detailed client implementation flow;
- client types;
- client tests.
```

Client sidecar must not invent a backend API contract. It references the parent slice API section.

## 8. Client Sidecar Structure

```text
# SL-XXX Client Layer — Title

Status:
Parent slice:
Slice type: read / command / mixed
Client architecture type: pages + entities / features + entities + pages / mixed
Scenario sources:
DATA sources:
Behavior item sources:

## 1. Client Behavior Coverage
## 2. Client Implementation Questions Register
## 3. Scenario / DATA Coverage
## 4. Client Architecture Mapping
## 5. API Contract Used By Client
## 6. Client Implementation Flow
## 7. Client Types
## 8. Client Tests
## 9. Out Of Scope
## 10. Scenario / DATA Questions To Register
```

Client Behavior Coverage columns:

```text
Behavior item
Category
Source
Required client behavior
DATA required by client
Client target
Status
Test coverage
```

Client Implementation Questions Register columns:

```text
ID
Related behavior item
Question
Type
Options
Current preference
Promote to scenario register?
Status
```

Question types may include:

```text
architecture
read-vs-command
feature-vs-page
entity-vs-feature
page-vs-widget
entity-query-hook
component-placement
client/API
cache
presentation
scenario-level
```

## 9. Client Architecture Mapping Rule

Planning slice and frontend feature are not 1:1.

Use:

```text
planning/slices/client-architecture-principles.md
```

Default mapping:

```text
Read slice    -> pages + entities (+ widgets only if reused)
Command slice -> pages + features + entities
Shared concern -> app / shared
```

Examples:

```text
Employee request dashboard/details
-> pages/employee-requests
-> pages/employee-request-details
-> entities/request
-> entities/applicant-party
```

```text
Approve request
-> pages/employee-request-review
-> features/approve-request
-> entities/request
-> entities/applicant-party
```

The `.client.md` file must explain which client architecture part covers which behavior item.

## 10. Client Implementation Flow

After quick-access tables and architecture mapping, describe:

```text
C01 — Routes
C02 — Pages
C03 — Entity Read Modules
C04 — Widgets, if needed
C05 — Command Features
C06 — Components
C07 — Query Hooks
C08 — Mutation Hooks
C09 — Form State / DTO Mapping
C10 — Action Availability
C11 — Submit Flow
C12 — Success Handling
C13 — Failure Handling
C14 — Cache / Invalidation
```

## 11. Shared Support Rule

Shared helpers/support artifacts used by multiple slices should be documented under:

```text
planning/slices/shared/
```

Examples:

```text
client/server validation error mapping
deferred client validation pattern
form values to API DTO mapping
antiforgery token/session-context support
applicant data prefill notes
```

## 12. Implementation Notes Register Rule

Before starting any slice/client sidecar, check:

```text
planning/slices/slice-implementation-notes-register.md
```

Promote relevant notes into the target slice, `.client.md`, shared support, scenario questions register or ADR candidates.

## 13. Test Planning Rule

Use groups:

```text
Client tests
Server tests
End-to-end tests
```

End-to-end tests verify the assembled full flow and go last.
