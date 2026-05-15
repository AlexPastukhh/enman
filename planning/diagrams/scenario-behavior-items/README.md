# Scenario Behavior Items Index

Status: current behavior items index  
Scope: per-scenario behavior items and cross-cutting concern-derived behavior items

## 1. Purpose

Behavior items make requirements traceable into domain drafts, slice drafts, implementation flows and tests.

## 2. Behavior Item Source Types

Behavior items may be:

```text
scenario-derived
security-derived cross-cutting
API-contract-derived cross-cutting
tooling-derived cross-cutting
testing-derived cross-cutting
client-cross-cutting-derived
infrastructure-derived
```

Business slices primarily consume scenario-derived behavior items.

Cross-cutting/helper slices consume concern-derived behavior items.

## 3. Current Scenario-Derived Behavior Item Files

```text
SC-10-applicant-data-behavior-items.md
```

`SC-10-applicant-data-behavior-items.md` covers Applicant Data / Account page behavior items, including current applicant state, saved/read-only UI outcome and no create-request entry in the applicant create UI slice.

## 4. Cross-Cutting Behavior Item Rule

Cross-cutting behavior items are first-class behavior items.

They must:

```text
- state their source;
- be reflected in the cross-cutting/helper slice Concern Flow;
- be covered by implementation flow and tests or marked as not covered yet.
```

## 5. Current Cross-Cutting Behavior Item Files

```text
CC-CSRF-001-antiforgery-behavior-items.md
```

## 6. Related Cross-Cutting Slices

```text
planning/slices/cross-cutting/CC-CSRF-001-antiforgery-token-session-context.md
planning/slices/cross-cutting/CC-CONST-001-client-constants-generation-and-contract-testing.md
```
