# Scenario Text Specs Index

Status: current scenario text-spec navigation index

## 1. Purpose

This folder contains corrected textual scenario specifications.

They are the primary scenario source of truth together with DATA specs, validation/security addenda, scenario questions register and behavior items.

## 2. Read First

```text
00-scenario-text-specs-index.md
scenario-server-domain-validation-addendum.md
scenario-account-activation-security-addendum.md
../scenario-questions-register.md
../scenario-behavior-items/README.md
```

## 3. Scenario Questions

If scenario text specs are underspecified, add or update:

```text
planning/diagrams/scenario-questions-register.md
```

Use the scenario question loop:

```text
question
-> clarify
-> update scenario spec / DATA / validation if needed
-> update behavior items if needed
-> continue implementation planning
```

## 4. Behavior Items

Scenario text specs feed per-scenario behavior items:

```text
planning/diagrams/scenario-behavior-items/
```

Behavior items must not invent new behavior. If behavior is missing, update the scenario spec first.

Behavior item migration / cleanup is a separate future step.

## 5. Current Downstream Use

Current downstream consumers:

```text
scenario DATA files
validation/security addenda
scenario questions register
per-scenario behavior items
domain drafts
slice boundary drafts
parent vertical slice files
.client.md sidecars when concrete client work starts
```

The compiled downstream baseline remains:

```text
planning/tables/pre-domain-variants-input.md
```

## 6. Rule

Scenario text specs describe behavior and domain-relevant rules.

They may contain mandatory observable UI requirements.

They do not define controllers, endpoints, database schema, ORM mappings, React components, final aggregate implementation or final visual design.
