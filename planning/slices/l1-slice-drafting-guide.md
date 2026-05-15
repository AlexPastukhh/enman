# L1 Slice Drafting Guide

Status: current slice drafting workflow  
Scope: business slices, cross-cutting/helper slices, client sidecars, implementation flow and tests

## 1. Draft-Driven Discovery Gate

Before drafting any slice, read:

```text
planning/slices/draft-driven-discovery-principles.md
```

All slice work uses draft-driven discovery:

```text
source requirements
-> draft
-> questions/assumptions
-> coverage
-> flow
-> implementation/test planning
-> next draft or implementation
```

This applies to:

```text
business slices
client sidecars
cross-cutting/helper slices
testing/support slices
documentation/status reconciliation drafts
```

## 2. Business Slice Intake Checklist

```text
1. Read architecture decision notes and ADR candidates.
2. Read draft-driven discovery principles.
3. Read planning/testing/ if tests/E2E/client test responsibilities are involved.
4. Read planning/api/ if API/client contract work is involved.
5. Read target scenario text spec and DATA file.
6. Read relevant behavior items and scenario UI spec if client-visible behavior is involved.
7. Check scenario questions register.
8. Check slice implementation notes and extension register.
9. Check planning/slices/cross-cutting/ if the slice uses cross-cutting support.
10. Check planning/client/ and client architecture docs if client work is involved.
11. If scenario/API/constants/testing/security ambiguity exists, stop and resolve it first.
```

## 3. Client Sidecar Draft Rule

Client sidecar drafts are created only when concrete client work starts.

A `.client.md` sidecar is the draft-driven discovery file for client implementation.

It must discover and track:

```text
- client behavior to implement;
- client questions/assumptions;
- contract used by client;
- route/page/feature/entity/shared mapping;
- generated OpenAPI types used;
- generated constants/error codes used;
- component placement;
- form/validation behavior;
- ProblemDetails/error mapping;
- client/component tests;
- E2E boundaries.
```

## 4. API Contract Section

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

## 5. Client Sidecar API Section

`.client.md` should include:

| Client API function | Endpoint | Generated OpenAPI type(s) used | Error constants used | Status |
|---|---|---|---|---|

## 6. Cross-Cutting / Helper Slice Intake Checklist

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

## 7. Test Coverage Sections

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

## 8. E2E Coverage Table

If E2E is relevant, include:

| E2E test | Scenario/UI items | Cross-layer purpose | Setup | User path | Server communication checked | Expected result | Status |
|---|---|---|---|---|---|---|---|

E2E is relevant when the behavior requires proof of:

```text
browser -> client -> HTTP API -> server/application/domain/persistence/session -> visible outcome
```

Do not use E2E to exhaustively test client-visible UI behavior.

## 9. Client Test Coverage Table

If client-visible UI behavior is involved, include:

| Client/component test | Behavior/UI items | Client behavior covered | Locator/accessibility focus | Status |
|---|---|---|---|---|

Detailed UI validation belongs here, not in happy-path E2E.

## 10. Business Slice Flow Rule

Business slices use:

```text
Scenario-derived behavior items
-> Scenario Slice Flow
-> Implementation Flow
```

## 11. Cross-Cutting / Helper Slice Flow Rule

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

## 12. Cross-Cutting / Helper Slice Template

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

## 13. Implementation Flow Detail Rule

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

## 14. Constants Consumer Rule

If a business slice introduces client-facing error codes, read:

```text
planning/slices/cross-cutting/CC-CONST-001-client-constants-generation-and-contract-testing.md
```

## 15. OpenAPI Consumer Rule

If a business/client slice uses server API, read:

```text
planning/api/client-server-contract-principles.md
planning/slices/cross-cutting/CC-API-001-openapi-contract-artifacts-and-type-generation.md
```

## 16. CSRF Consumer Rule

If a business slice introduces browser unsafe API command, read:

```text
planning/slices/cross-cutting/CC-CSRF-001-antiforgery-token-session-context.md
```

Parent slice API/security note:

```text
Unsafe browser API requests are protected by CC-CSRF-001.
```
