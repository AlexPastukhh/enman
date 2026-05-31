# Domain Decisions Index

Status: current domain decisions folder index  
Doc version: v0.1.0  
Scope: accepted/proposed domain modeling decisions

## 1. Purpose

This folder owns domain decisions that are stronger than loose notes and not owned by one aggregate draft.

Use it for decisions such as:

```text
- account/client/employee hierarchy direction;
- aggregate boundary choices;
- cross-aggregate ownership decisions;
- value object extraction decisions;
- domain/persistence boundary decisions.
```

If a decision becomes a broader architecture decision, promote or mirror it to ADR files.

## 2. Current Decisions

| File | Status | Decision |
|---|---|---|
| `account-employee-tph-decision.md` | accepted | Employee is a concrete Account subtype; target persistence direction is Account/ClientAccount/Employee TPH. |

## 3. Decision Note Shape

```text
# Domain Decision — <Decision Name>

Status: proposed / accepted / superseded
Scope:

## 1. Decision
## 2. Context
## 3. Sources
## 4. Options Considered
## 5. Chosen Direction
## 6. Consequences
## 7. Affected Files
## 8. Questions / Follow-ups
## 9. Change Log
```

## 4. Historical Source Material

Extracted source material:

```text
planning/tables/domain-drafts/domain-draft-02-account-employee-tph-decision.md
```

The historical source file remains in place for traceability.
