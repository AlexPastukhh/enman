# Current Planning Workflow

Status: current workflow

## 1. Current Point

```text
Client constants generation/testing is treated as a cross-cutting slice,
not merely as workflow notes or an API reference.
```

## 2. Cross-Cutting Slice Workflow

When work is not a business scenario slice but has observable support behavior, implementation flow and tests, use a cross-cutting/helper slice.

```text
cross-cutting/helper behavior
-> coverage table
-> implementation flow
-> involved classes/methods only where useful
-> test plan
-> consumer rule for business slices
-> ADR impact
```

## 3. Constants Gate

When a slice introduces client-facing constants/error codes:

```text
1. Check CC-CONST-001.
2. Classify code:
   - ordinary validation;
   - important domain;
   - critical behavioral.
3. Update parent slice API error table.
4. Regenerate Shared/*.json.
5. Add/adjust API integration test:
   - generated JSON expectation for ordinary codes;
   - literal expectation for critical behavioral codes.
6. If client work exists, map ErrorCode -> UI behavior in `.client.md`.
7. Run generate-client-constants --check.
```

## 4. Implementation Flow Detail Rule

Implementation flow must not become a full code listing.

Include class/method/code details only when they explain:

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

Routine implementation details stay high-level.

If details dominate the flow, extract them into a sibling `.impl.md`.

Do not create `.impl.md` in advance.

## 5. Current Next Step

```text
Implement or prompt implementation of CC-CONST-001 only after this docs package is applied.
```
