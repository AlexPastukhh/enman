# L1 Slice Drafting Guide

Status: current slice drafting workflow  
Scope: business slices, cross-cutting/helper slices, client sidecars, implementation flow and tests

## 1. Business Slice Intake Checklist

```text
1. Read architecture decision notes and ADR candidates.
2. Read planning/testing/ if tests/E2E/client test responsibilities are involved.
3. Read target scenario text spec and DATA file.
4. Read relevant behavior items and scenario UI spec if client-visible behavior is involved.
5. Check scenario questions register.
6. Check slice implementation notes and extension register.
7. Check planning/api/ if API/client work is involved.
8. Check planning/slices/cross-cutting/ if the slice uses cross-cutting support.
9. Check planning/client/ and client architecture docs if client work is involved.
10. If scenario/API/constants/testing ambiguity exists, stop and resolve it first.
```

## 2. Test Coverage Sections

Parent slice and `.client.md` should separate:

```text
Client/component tests
Server integration/API tests
End-to-end tests
```

Use:

```text
planning/testing/testing-principles.md
planning/testing/e2e-playwright-workflow.md
```

## 3. E2E Coverage Table

If E2E is relevant, include:

| E2E test | Scenario/UI items | Cross-layer purpose | Setup | User path | Server communication checked | Expected result | Status |
|---|---|---|---|---|---|---|---|

E2E is relevant when the behavior requires proof of:

```text
browser -> client -> HTTP API -> server/application/domain/persistence/session -> visible outcome
```

Do not use E2E to exhaustively test client-visible UI behavior.

## 4. Client Test Coverage Table

If client-visible UI behavior is involved, include:

| Client/component test | Behavior/UI items | Client behavior covered | Locator/accessibility focus | Status |
|---|---|---|---|---|

Detailed UI validation belongs here, not in happy-path E2E.

## 5. Cross-Cutting / Helper Slice Template

```text
# CC-XXX — Title

Status:
Slice type: cross-cutting slice / helper slice
Layers:
Depends on:
Used by:

## 1. Purpose
## 2. Why This Is A Cross-Cutting/Helper Slice
## 3. Inputs / Sources
## 4. Pseudo Behavior Items
## 5. Coverage Overview
## 6. Implementation Flow
## 7. Target Types / Components
## 8. Test Plan
## 9. Consumer Rule For Business Slices
## 10. Local Questions
## 11. ADR Impact
```

## 6. Implementation Flow Detail Rule

Implementation flow may include involved classes, methods and short code snippets.

Include them when they explain:

```text
- contract boundary;
- non-obvious behavior;
- behavior that was discussed/questioned;
- important trade-off;
- extension/change point;
- error handling;
- testability;
- no-write/no-side-effect guarantee;
- generated artifact shape;
- API/client boundary.
```

Keep routine mechanics high-level.

If details make the flow noisy, extract them into a sibling `.impl.md` file.

Do not create `.impl.md` in advance.

## 7. Constants Consumer Rule

If a business slice introduces client-facing error codes, read:

```text
planning/slices/cross-cutting/CC-CONST-001-client-constants-generation-and-contract-testing.md
```

Then classify each code as:

```text
ordinary validation
important domain
critical behavioral
```

Parent slice API table:

| Error code | FieldName | HTTP status | Stability | Client handling | Literal test? |
|---|---|---:|---|---|---|
