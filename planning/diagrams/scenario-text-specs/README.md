# Scenario Text Specifications Index

Status: current scenario text/specification index  
Doc version: v0.1.0  
Scope: scenario text specifications, core/business scenario template and cross-scenario addenda

## 1. Purpose

Scenario text specs describe core/business use cases and behavior requirements.

They own the business capability, scenario-local meaning and inline scenario DATA by default.

Use the canonical template for new core/business scenario specs and intentional rewrites:

```text
planning/diagrams/scenario-text-specs/SCENARIO-TEXT-SPEC-TEMPLATE.md
```

Existing scenario specs may keep older structures during migration. Do not rewrite existing `SC-*` files only to match the new template.

## 2. Inline DATA Default

Scenario DATA starts inline in the core/business scenario text spec.

Use `planning/diagrams/scenario-data/` only for:

```text
- reusable business DATA concepts;
- large DATA concepts that make one scenario hard to read;
- DATA concepts needing separate review/audit;
- stable cross-scenario references for downstream domain/slice/client/testing work;
- existing transitional sidecars not yet merged/reclassified.
```

A scenario text spec should keep a readable DATA summary even when reusable DATA concepts are extracted.

## 3. Security / Cross-Scenario Addenda

```text
SC-15-security-text-specification.md
scenario-account-activation-security-addendum.md
scenario-browser-security-addendum.md
```

Deprecated global validation addenda are historical context only. Use current validation/domain routing from:

```text
planning/scenario-domain-validation-principles.md
planning/diagrams/scenario-questions-register.md
planning/diagrams/scenario-clarifications/
planning/domain/
```

## 4. Browser Security Addendum

Use:

```text
scenario-browser-security-addendum.md
```

for browser security requirements such as antiforgery/CSRF with cookie authentication.

Behavior items derived from it are security-derived cross-cutting behavior items.

## 5. Rule

Do not mix concrete implementation details directly into scenario text specs.

Implementation-ready behavior belongs in slice files, such as:

```text
planning/slices/cross-cutting/CC-CSRF-001-antiforgery-token-session-context.md
```

Do not move or rewrite stale/legacy scenario files as part of template/policy updates.
