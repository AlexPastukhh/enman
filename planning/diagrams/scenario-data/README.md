# Scenario DATA Specs Index

Status: current scenario DATA navigation index

## 1. Purpose

This folder contains per-scenario DATA specs.

DATA means only what actor:

```text
- enters;
- sees;
- selects;
- filters/searches by;
- attaches/uploads;
- references as visible/selectable business item.
```

## 2. Read First

```text
00-scenario-data-index.md
```

## 3. DATA Is Not Validation

DATA files must not contain:

```text
- validation/rules sections;
- testable behavior sections;
- invariants;
- preconditions;
- branches;
- access rules;
- security policy.
```

Validation belongs to:

```text
planning/scenario-domain-validation-principles.md
planning/diagrams/scenario-text-specs/scenario-server-domain-validation-addendum.md
```

The post-scenario bridge uses DATA together with validation-related files:

```text
planning/tables/pre-domain-variants-input.md
```

## 4. Active DATA Files

See:

```text
00-scenario-data-index.md
```

## 5. Downstream Use

DATA specs feed:

```text
scenario-server-domain-validation-addendum.md
pre-domain-variants-input.md
domain model variants
```

DATA fields are candidates for:

```text
- value objects discovered during domain variant generation;
- visible page data;
- command/input objects later;
- persistence fields later.
```

But DATA files themselves are not DB schemas or DTO contracts.

## 6. Current Next Step

After DATA and validation-related files, use:

```text
planning/tables/pre-domain-variants-input.md
```

Then generate:

```text
domain model variant 1
```
