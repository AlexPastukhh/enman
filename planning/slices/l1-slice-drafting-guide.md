# L1 Slice Drafting Guide

Status: current slice drafting workflow  
Scope: L1 slice boundary discovery, parent slice files, client sidecars, API contract, UI specs, component discovery, extension/change points, shared support and registers

## Purpose

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

## Slice Intake Checklist

```text
1. Read architecture decision notes and ADR candidates.
2. Read target scenario text spec and DATA file.
3. Read validation/security addendum entries.
4. Read relevant behavior items.
5. Read scenario UI spec if client-visible behavior is involved.
6. Check scenario questions register.
7. Check slice implementation notes and extension register.
8. Check planning/api/ for API contract rules if API/client work is involved.
9. Check planning/client/ and client architecture docs if client work is involved.
10. Promote relevant notes/questions/extension pressure/ADR decisions.
11. If scenario-level/API ambiguity exists, stop and resolve it first.
```

## Parent Slice File

Parent slice files include:

```text
- Slice overview;
- questions overview near the beginning;
- flow coverage overview near the beginning;
- Scenario Slice Flow;
- behavior item coverage summary;
- Implementation Flow;
- API contract;
- API / Error Contract Check;
- Server / Cross-Layer Extension Points;
- Server / Cross-Layer Change Points;
- Extension Pressure / Anti-Coupling Decisions;
- application/domain/persistence responsibilities;
- server/integration test plan;
- decisions;
- implementation checklist.
```

### API Layer

Parent slice API layer should include:

```text
- endpoint path and method;
- request DTO;
- response DTO;
- OpenAPI exposure expectations;
- status codes;
- native ProblemDetails contract;
- ServerError / ServerValidationError shape;
- client-facing error codes;
- DTO field names used by validation errors;
- internal/server-only errors explicitly out of client contract.
```

Client-facing error table:

| Error code | FieldName | HTTP status | Source | Client handling |
|---|---|---:|---|---|

Only include errors intentionally returned to the client.

## Client Sidecar Structure

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
API contract sources:
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
## 13. API / Error Contract Usage
## 14. API Contract Check
## 15. Server DTO Field To Client Form Field Mapping
## 16. Client Implementation Flow
## 17. Client Types
## 18. Client Tests
## 19. Out Of Scope
## 20. Scenario / DATA / UI / API Questions To Register
```

## API / Error Contract Usage

Client sidecar must describe:

```text
- generated OpenAPI DTO/types used by client;
- generated shared constants JSON used by client;
- native ProblemDetails parsing;
- shared errors extension usage;
- FieldName / ErrorCode key usage;
- DTO field -> form field mapping;
- known error code -> UI behavior;
- stale state / refetch behavior;
- generic fallback for unknown/internal errors.
```

API Contract Check table:

| Contract item | Source of truth | Used by client? | Client handling | Status |
|---|---|---|---|---|

## Accessibility

Accessibility / ARIA Contract is mandatory for `.client.md`.
