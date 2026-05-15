# Scenario / Diagram Planning Index

Status: current scenario/specification planning index  
Scope: scenario text specs, DATA, UI specs, validation/security addenda, behavior items and questions

## 1. Purpose

This folder contains scenario and specification source artifacts.

## 2. Main Artifact Types

```text
scenario text specs
scenario DATA files
scenario UI specs
validation/security addenda
scenario questions register
scenario behavior items
```

## 3. Security Addenda

Scenario/security addenda may record rules that affect many scenarios without editing every scenario file.

Current security addenda:

```text
planning/diagrams/scenario-text-specs/SC-15-security-text-specification.md
planning/diagrams/scenario-text-specs/scenario-account-activation-security-addendum.md
planning/diagrams/scenario-text-specs/scenario-browser-security-addendum.md
```

## 4. Behavior Items

Behavior items may be:

```text
scenario-derived
security-derived cross-cutting
API-contract-derived cross-cutting
tooling/testing-derived cross-cutting
```

Cross-cutting behavior items must clearly state their source type.

## 5. CSRF / Antiforgery

CSRF requirements and behavior items:

```text
planning/diagrams/scenario-text-specs/scenario-browser-security-addendum.md
planning/diagrams/scenario-behavior-items/CC-CSRF-001-antiforgery-behavior-items.md
```

Implementation-ready cross-cutting slice:

```text
planning/slices/cross-cutting/CC-CSRF-001-antiforgery-token-session-context.md
```
