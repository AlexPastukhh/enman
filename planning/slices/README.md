# Slice Planning Index

Status: current slice-planning navigation index  
Scope: scenario-derived slice boundary discovery, parent slice files, client sidecars, client architecture, implementation notes and shared support

## 1. Purpose

This folder documents how to derive implementation slices from scenarios and how to plan implementation one slice at a time.

It does not own global planning/agent/replacement workflow rules.

Use central files for global rules:

```text
planning/planning-workflow-current.md
planning/planning-agent-protocol.md
planning/planning-doc-responsibility-map.md
```

## 2. Current Slice Workflow

```text
scenario text spec
+ scenario DATA file
+ validation/security addendum
-> per-scenario behavior items
-> scenario questions register
-> L1 slice boundary draft
-> parent vertical slice file
-> .client.md sidecar when concrete client work starts
-> implementation
```

## 3. Responsibility

This folder owns:

```text
- slice discovery;
- parent vertical slice files;
- client sidecar files;
- client architecture planning principles;
- shared support docs;
- implementation principles near slice work;
- future implementation notes register.
```

It does not own:

```text
- global agent protocol;
- replacement package rules;
- scenario source-of-truth rules;
- document responsibility map.
```

## 4. Slice Support Files

```text
planning/slices/l1-slice-drafting-guide.md
planning/slices/implementation-principles.md
planning/slices/client-architecture-principles.md
planning/slices/slice-implementation-notes-register.md
planning/slices/shared/README.md
```

## 5. General Boundary Draft

Use:

```text
planning/slices/l1-slice-boundary-draft-01.md
```

It discovers real slices, proves boundaries and includes Scenario Slice Flow.

It must not contain detailed implementation flow.

## 6. Parent Slice Files

Parent slice files own:

```text
- vertical behavior;
- Scenario Slice Flow;
- behavior item coverage summary;
- API contract;
- cross-layer Implementation Flow;
- application/domain/persistence responsibilities;
- server/integration tests;
- link to .client.md sidecar when client work starts.
```

## 7. Client Sidecar Files

A `.client.md` file is created only when concrete client work starts.

Example:

```text
planning/slices/SL-REQ-001-create-connection-request.client.md
```

Client sidecar owns detailed client implementation and client tests.

It must include:

```text
Client Behavior Coverage
Client Implementation Questions Register
Scenario / DATA Coverage
Client Architecture Mapping
API Contract Used By Client
Client Implementation Flow
Client Types
Client Tests
```

The sidecar must use the API contract from the parent slice file.

## 8. Client Architecture Mapping

Use:

```text
planning/slices/client-architecture-principles.md
```

Core rule:

```text
Planning slice and frontend feature are not 1:1.
```

Default mapping:

```text
Read slice    -> pages + entities (+ widgets if reused)
Command slice -> pages + features + entities
Shared concern -> app / shared
```

## 9. Slice Intake Checklist

Before starting any slice/client sidecar:

```text
1. Read target scenario text spec.
2. Read target DATA file.
3. Read validation/security addendum entries.
4. Read relevant behavior items if they exist.
5. Check planning/diagrams/scenario-questions-register.md.
6. Check planning/slices/slice-implementation-notes-register.md.
7. Check relevant shared support docs.
8. If client work is involved, check planning/slices/client-architecture-principles.md.
9. If scenario-level ambiguity exists, stop and resolve it through scenario question loop.
```

## 10. Shared Support Artifacts

Reusable helpers used by multiple slices are documented under:

```text
planning/slices/shared/
```

Current shared support docs:

```text
planning/slices/shared/client-deferred-validation.md
planning/slices/shared/client-server-validation-error-mapping.md
planning/slices/shared/client-form-values-to-api-dto-mapping.md
planning/slices/shared/antiforgery-token-session-context.md
planning/slices/shared/client-applicant-data-prefill-notes.md
```

## 11. Current Files

General boundary draft:

```text
planning/slices/l1-slice-boundary-draft-01.md
```

Implemented or partially implemented parent slice files:

```text
planning/slices/SL-ACC-001-register-client-account.md
planning/slices/SL-APPL-001-create-individual-applicant-party.md
planning/slices/SL-REQ-001-create-connection-request.md
planning/slices/SL-REVIEW-001-approve-request-and-verify-applicant.md
planning/slices/SL-REVIEW-002-reject-request.md
```

Expected future client sidecars are not created until concrete client work starts.

## 12. Current Next Step

Get or prepare concrete UI plan for the next client layer.

Then create the relevant `.client.md` sidecar and implement the client work.
