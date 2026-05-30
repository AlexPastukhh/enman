# Shared Visibility Scenario Project Example

Status: active reusable example  
Scope: scenario-driven project example for local detail to shared visibility mapping

## 1. Purpose

This example demonstrates how a scenario-driven project can apply:

```text
planning/documentation/field-kits/shared-visibility-map-field-kit.md
```

It is an example only. It does not own rules.

## 2. Scenario

A local slice file contains this question:

```text
Should proposal recipient permissions be checked at invitation creation time or only when proposal is accepted?
```

This question affects:

```text
- future slice behavior;
- domain rule;
- API contract;
- testing responsibility.
```

## 3. Shared Visibility Decision

| Local file | Local detail | Detail type | Shared target | Action | Notes |
|---|---|---|---|---|---|
| one slice doc | recipient permission timing question | slice open question | slice questions register | mirror | affects domain/API/testing, not local-only |

## 4. Local-Only Counterexample

A wording note that only improves one local paragraph and does not affect future work can stay local-only with no shared register row.
