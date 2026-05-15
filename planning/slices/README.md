# Slice Planning Index

Status: current slice-planning navigation index  
Scope: slice boundary discovery, parent slice files, client sidecars, client architecture, component discovery, extension/change points, implementation notes and shared support

## 1. Purpose

This folder documents how to derive implementation slices from scenarios and plan implementation one slice at a time.

## 2. Current Slice Workflow

```text
scenario text spec
+ scenario DATA file
+ validation/security addendum
+ scenario UI spec when client-visible behavior is planned
-> per-scenario behavior items
-> UI behavior items
-> scenario questions register
-> L1 slice boundary draft
-> parent vertical slice file
-> .client.md sidecar when concrete client work starts
-> implementation
```

## 3. Responsibility

This folder owns slice discovery, parent vertical slice files, client sidecar files, client architecture planning principles, client component discovery guide, change/extension/extension pressure principles, extension points register, shared support docs, implementation principles near slice work and future implementation notes register.

## 4. Slice Support Files

```text
planning/slices/l1-slice-drafting-guide.md
planning/slices/implementation-principles.md
planning/slices/client-architecture-principles.md
planning/slices/client-component-discovery-guide.md
planning/slices/change-extension-points-principles.md
planning/slices/slice-extension-points-register.md
planning/slices/slice-implementation-notes-register.md
planning/slices/shared/README.md
```

## 5. Parent Slice Files

Parent slice files own:

```text
- vertical behavior;
- Scenario Slice Flow;
- behavior item coverage summary;
- API contract;
- cross-layer Implementation Flow;
- Server / Cross-Layer Extension Points;
- Server / Cross-Layer Change Points;
- Extension Pressure / Anti-Coupling Decisions;
- application/domain/persistence responsibilities;
- server/integration tests;
- link to `.client.md` sidecar when client work starts.
```

## 6. Client Sidecar Files

A `.client.md` file is created only when concrete client work starts.

It must include Client Behavior Coverage, UI Behavior Coverage, questions, Scenario / DATA / UI Spec Coverage, Client Architecture Mapping, Client Extension Points, Client Behavior Change Points, Client Extension Pressure / Anti-Coupling Decisions, Component / Layout Plan, Styling Change Points, Accessibility / ARIA Contract, API Contract Used By Client, Client Implementation Flow, Client Types and Client Tests.

## 7. Client Architecture / Component / A11Y

Use:

```text
planning/slices/client-architecture-principles.md
planning/slices/client-component-discovery-guide.md
planning/client/
```

## 8. Change / Extension Points

Use:

```text
planning/slices/change-extension-points-principles.md
planning/slices/slice-extension-points-register.md
```

Known future extension points must be reviewed during current slice planning.

## 9. Slice Intake Checklist

```text
1. Read target scenario text spec.
2. Read target DATA file.
3. Read validation/security addendum entries.
4. Read relevant behavior items if they exist.
5. Read relevant scenario UI spec if client-visible behavior is involved.
6. Check planning/diagrams/scenario-questions-register.md.
7. Check planning/slices/slice-implementation-notes-register.md.
8. Check planning/slices/slice-extension-points-register.md.
9. Check relevant shared support docs.
10. If client work is involved, check planning/client/ and planning/slices/client-architecture-principles.md.
11. If scenario-level ambiguity exists, stop and resolve it through scenario question loop.
```

## 10. Current Files

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
