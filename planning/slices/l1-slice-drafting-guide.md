# L1 Slice Drafting Guide

Status: current slice drafting workflow  
Scope: L1 slice boundary discovery, parent slice files, client sidecars, UI specs, component discovery, extension/change points, shared support and registers

## 1. Purpose

```text
scenario specs / DATA / UI specs / validation-security
-> per-scenario behavior items / UI behavior items
-> scenario questions register
-> L1 slice boundary draft
-> parent slice file
-> .client.md sidecar when concrete client work starts
-> implement one slice/client layer
-> update coverage / questions / decisions / ADR candidates / extension register
```

## 2. Working Definition

```text
Slice = independently testable unit of observable behavior
        + implementation path needed to deliver/test that behavior.
```

## 3. Scenario Question Rule

If a question affects scenario behavior, DATA, UI-visible requirement, validation/security, API contract or user-visible outcome, stop and use the scenario question loop.

## 4. Slice Intake Checklist

```text
1. Read target scenario text spec.
2. Read target DATA file.
3. Read validation/security addendum entries.
4. Read relevant per-scenario behavior items if they exist.
5. Read relevant scenario UI spec if client-visible behavior is involved.
6. Check planning/diagrams/scenario-questions-register.md.
7. Check planning/slices/slice-implementation-notes-register.md.
8. Check planning/slices/slice-extension-points-register.md.
9. Check relevant shared support docs.
10. Check planning/client/ and planning/slices/client-architecture-principles.md if client work is involved.
11. Promote relevant notes/questions/extension pressure decisions.
12. If scenario-level ambiguity exists, stop and resolve it first.
```

## 5. Parent Slice File

Parent slice files include:

```text
- Slice overview;
- questions overview near the beginning;
- flow coverage overview near the beginning;
- Scenario Slice Flow;
- behavior item coverage summary;
- Implementation Flow;
- API contract;
- Server / Cross-Layer Extension Points;
- Server / Cross-Layer Change Points;
- Extension Pressure / Anti-Coupling Decisions;
- application/domain/persistence responsibilities;
- server/integration test plan;
- decisions;
- implementation checklist.
```

Parent slice owns API contract.

### 5.1 Server / Cross-Layer Extension Points

| ID | Future extension slice | Current seam/source | Current decision | What current slice must avoid | Status |
|---|---|---|---|---|---|

### 5.2 Server / Cross-Layer Change Points

| ID | Behavior aspect | Change point | Owner | Current decision | Configurable now? | Tests affected |
|---|---|---|---|---|---|---|

### 5.3 Extension Pressure / Anti-Coupling Decisions

| ID | Related extension point | Pressure source | Probability/certainty | Time horizon | Current decision | Anti-coupling constraint | Trade-off | Status |
|---|---|---|---|---|---|---|---|---|

If empty:

```text
No planned extension/change points in current draft.
```

## 6. Client Sidecar Rule

If a slice has non-trivial client/UI work, create a `.client.md` file next to the parent slice file when client work starts.

Do not create `.client.md` in advance.

## 7. Client Sidecar Structure

```text
# SL-XXX Client Layer — Title

Status:
Parent slice:
Slice type: read / command / mixed
Client architecture type:
Scenario sources:
DATA sources:
Scenario UI spec sources:
Behavior item sources:
Client cross-cutting sources:

## 1. Client Behavior Coverage
## 2. UI Behavior Coverage
## 3. Client Implementation Questions Register
## 4. Scenario / DATA / UI Spec Coverage
## 5. Client Architecture Mapping
## 6. Client Extension Points
## 7. Client Behavior Change Points
## 8. Client Extension Pressure / Anti-Coupling Decisions
## 9. Component / Layout Plan
## 10. Styling Change Points
## 11. Accessibility / ARIA Contract
## 12. API Contract Used By Client
## 13. Client Implementation Flow
## 14. Client Types
## 15. Client Tests
## 16. Out Of Scope
## 17. Scenario / DATA / UI Questions To Register
```

## 8. Client Coverage Tables

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

UI Behavior Coverage columns:

```text
UI item
Source
Required UI behavior
User-visible result
Client target
Status
Test coverage
```

Question types may include architecture, read-vs-command, feature-vs-page, entity-vs-feature, page-vs-widget, entity-query-hook, component-placement, client/API, cache, presentation, accessibility, styling, extension-point, change-point, extension-pressure and scenario-level.

## 9. Client Architecture / Component / A11Y

Use:

```text
planning/slices/client-architecture-principles.md
planning/slices/client-component-discovery-guide.md
planning/client/cross-cutting/
```

## 10. Client Implementation Flow

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

## 11. Test Planning Rule

Use groups:

```text
Client tests
Server tests
End-to-end tests
```
