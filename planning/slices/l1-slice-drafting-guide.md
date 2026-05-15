# L1 Slice Drafting Guide

Status: current slice drafting workflow  
Scope: business slices, cross-cutting/helper slices, client sidecars, implementation flow and tests

## 1. Business Slice Intake Checklist

```text
1. Read architecture decision notes and ADR candidates.
2. Read target scenario text spec and DATA file.
3. Read relevant behavior items and scenario UI spec if client-visible behavior is involved.
4. Check scenario questions register.
5. Check slice implementation notes and extension register.
6. Check planning/api/ if API/client work is involved.
7. Check planning/slices/cross-cutting/ if the slice uses cross-cutting support.
8. Check planning/client/ and client architecture docs if client work is involved.
9. If scenario/API/constants ambiguity exists, stop and resolve it first.
```

## 2. Cross-Cutting / Helper Slice Template

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

## 3. Implementation Flow Detail Rule

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

## 4. Constants Consumer Rule

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
