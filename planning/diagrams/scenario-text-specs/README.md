# Scenario Text Specifications Index

Status: current scenario text/specification index  
Scope: scenario text specifications and cross-scenario addenda

## 1. Purpose

Scenario text specs describe use cases and behavior requirements.

Some requirements affect many scenarios and are documented as addenda instead of being repeated in every scenario.

## 2. Security / Cross-Scenario Addenda

```text
SC-15-security-text-specification.md
scenario-account-activation-security-addendum.md
scenario-browser-security-addendum.md
scenario-server-domain-validation-addendum.md
```

## 3. Browser Security Addendum

Use:

```text
scenario-browser-security-addendum.md
```

for browser security requirements such as antiforgery/CSRF with cookie authentication.

Behavior items derived from it are security-derived cross-cutting behavior items.

## 4. Rule

Do not mix concrete implementation details directly into scenario text specs.

Implementation-ready behavior belongs in slice files, such as:

```text
planning/slices/cross-cutting/CC-CSRF-001-antiforgery-token-session-context.md
```
