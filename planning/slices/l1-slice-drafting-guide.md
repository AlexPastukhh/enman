# L1 Slice Drafting Guide

Status: current slice drafting workflow  
Scope: business slices, cross-cutting/helper slices, client sidecars, implementation flow and tests

## 1. Business Slice Intake Checklist

```text
1. Read architecture decision notes and ADR candidates.
2. Read planning/testing/ if tests/E2E/client test responsibilities are involved.
3. Read planning/api/ if API/client contract work is involved.
4. Read target scenario text spec and DATA file.
5. Read relevant behavior items and scenario UI spec if client-visible behavior is involved.
6. Check scenario questions register.
7. Check slice implementation notes and extension register.
8. Check planning/slices/cross-cutting/ if the slice uses cross-cutting support.
9. Check planning/client/ and client architecture docs if client work is involved.
10. If scenario/API/constants/testing/security ambiguity exists, stop and resolve it first.
```

## 2. API Contract Section

Parent slice API section should include:

| Endpoint | Method | Request DTO | Response DTO | Statuses | Contract status | OpenAPI exposed? |
|---|---|---|---|---|---|---|

Contract status values:

```text
target L1
legacy/current support
temporary compatibility
internal/not client-facing
```

Also include:

```text
- ProblemDetails response statuses;
- client-facing error codes;
- generated constants used;
- OpenAPI generated types used by client;
- whether route is temporary legacy constants route or OpenAPI structural route.
```

## 3. Client Sidecar API Section

`.client.md` should include:

| Client API function | Endpoint | Generated OpenAPI type(s) used | Error constants used | Status |
|---|---|---|---|---|

## 4. Cross-Cutting / Helper Slice Intake Checklist

```text
1. Identify source requirement type:
   security / API contract / tooling / testing / infrastructure / client-cross-cutting.
2. Check whether a specification/addendum/source note already exists.
3. Create or update concern-derived behavior items.
4. Ensure every behavior item appears in Concern Slice Flow.
5. Only after the concern flow, write Implementation Flow.
6. Add coverage, test/check plan, consumer rule, local questions and ADR impact.
7. Update cross-cutting index and relevant navigation.
```

## 5. Test Coverage Sections

Parent slice and `.client.md` should separate:

```text
Client/component tests
Server integration/API tests
End-to-end tests
```

Use:

```text
planning/testing/testing-principles.md
planning/testing/e2e-testing-workflow.md
```

## 6. E2E Coverage Table

If E2E is relevant, include:

| E2E test | Scenario/UI items | Cross-layer purpose | Setup | User path | Server communication checked | Expected result | Status |
|---|---|---|---|---|---|---|---|

E2E is relevant when the behavior requires proof of:

```text
browser -> client -> HTTP API -> server/application/domain/persistence/session -> visible outcome
```

Do not use E2E to exhaustively test client-visible UI behavior.

## 7. Client Test Coverage Table

If client-visible UI behavior is involved, include:

| Client/component test | Behavior/UI items | Client behavior covered | Locator/accessibility focus | Status |
|---|---|---|---|---|

Detailed UI validation belongs here, not in happy-path E2E.

## 8. Business Slice Flow Rule

Business slices use:

```text
Scenario-derived behavior items
-> Scenario Slice Flow
-> Implementation Flow
```

## 9. Cross-Cutting / Helper Slice Flow Rule

Cross-cutting/helper slices use:

```text
Concern-derived behavior items
-> Concern Slice Flow
-> Implementation Flow
```

Concern-derived behavior item source can be:

```text
security-derived
API-contract-derived
tooling-derived
testing-derived
client-cross-cutting-derived
infrastructure-derived
```

These items are first-class behavior items and must be covered by the concern flow.

## 10. Cross-Cutting / Helper Slice Template

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
## 4. Concern-Derived Behavior Items
## 5. Coverage Overview
## 6. Concern Slice Flow
## 7. Implementation Flow
## 8. Target Types / Components
## 9. Test / Check Plan
## 10. Consumer Rule For Business Slices
## 11. Local Questions
## 12. ADR Impact
```

## 11. Implementation Flow Detail Rule

Implementation flow may include involved classes, methods and short code snippets.

Include them when they explain:

```text
- contract boundary;
- non-obvious behavior;
- behavior that was discussed/questioned;
- important trade-off;
- extension/change point;
- error handling;
- testability/checkability;
- no-write/no-side-effect guarantee;
- generated artifact shape;
- API/client boundary.
```

Keep routine mechanics high-level.

If details make the flow noisy, extract them into a sibling `.impl.md` file.

Do not create `.impl.md` in advance.

## 12. Constants Consumer Rule

If a business slice introduces client-facing error codes, read:

```text
planning/slices/cross-cutting/CC-CONST-001-client-constants-generation-and-contract-testing.md
```

## 13. OpenAPI Consumer Rule

If a business/client slice uses server API, read:

```text
planning/api/client-server-contract-principles.md
planning/slices/cross-cutting/CC-API-001-openapi-contract-artifacts-and-type-generation.md
```

## 14. CSRF Consumer Rule

If a business slice introduces browser unsafe API command, read:

```text
planning/slices/cross-cutting/CC-CSRF-001-antiforgery-token-session-context.md
```

Parent slice API/security note:

```text
Unsafe browser API requests are protected by CC-CSRF-001.
```
