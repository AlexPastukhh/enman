# Status Reconciliation Scenario Project Example

Status: active reusable example  
Scope: scenario-driven software project example for using the status reconciliation field kit

## 1. Purpose

This example demonstrates how a scenario-driven software project can apply:

```text
planning/documentation/field-kits/status-reconciliation-field-kit.md
```

It is an example only. It does not own rules.

## 2. Scenario

A slice document says:

```text
Agreement proposal exchange is planned.
```

Current repo evidence shows:

```text
- API route exists;
- generated OpenAPI contract exists;
- baseline tests exist;
- hardening and edge cases remain.
```

## 3. Status Decision

| Area | Claim | Evidence checked | Status | Docs to update | Next action |
|---|---|---|---|---|---|
| Agreement proposal exchange | planned | API route, generated contract, baseline tests | first-stage implemented | slice status, related README/register rows | create hardening follow-up |

## 4. Why This Is Not Universal

This example uses software evidence.

A study notes project, research project or day planning system would define a different current-reality model through the field kit.
