# Scenario Text Specs Index

Status: current scenario text-spec navigation index

## 1. Purpose

This folder contains corrected textual scenario specifications.

They are the primary scenario source of truth together with DATA specs, validation/security addenda, scenario questions and per-scenario behavior items.

## 2. Read First

```text
00-scenario-text-specs-index.md
scenario-server-domain-validation-addendum.md
scenario-account-activation-security-addendum.md
../scenario-questions-register.md
../scenario-behavior-items/README.md
../scenario-behavior-items/00-scenario-behavior-items-index.md
```

## 3. Active Scenario Specs

```text
SC-01-guest-registration.md
SC-02-login.md
SC-03A-password-recovery-request.md
SC-03B-account-owner-verified.md
SC-04-client-request-creation.md
SC-05-my-requests-own-request-details.md
SC-06-employee-request-dashboard.md
SC-07A-employee-request-details.md
SC-07B-employee-request-review.md
SC-10-applicant-data.md
SC-11-request-documents.md
SC-13A-my-agreements.md
SC-13B-agreement-proposal-details-response.md
SC-13C-employee-agreements.md
SC-13D-employee-agreement-proposal-create-response.md
SC-14-client-data-verification.md
SC-15-security-text-specification.md
SC-17-anonymous-request.md
```

## 4. Validation / Security Addenda

Use:

```text
scenario-server-domain-validation-addendum.md
scenario-account-activation-security-addendum.md
```

Do not move these validation/security rules into DATA files.

## 5. Scenario Questions

If scenario text specs are underspecified, add or update:

```text
planning/diagrams/scenario-questions-register.md
```

Use the scenario question loop before continuing implementation planning.

## 6. Behavior Items

Scenario text specs feed per-scenario behavior items:

```text
planning/diagrams/scenario-behavior-items/
```

Behavior items must not invent new behavior. If behavior is missing, update the scenario spec first.

## 7. Rule

Scenario text specs describe behavior and domain-relevant rules.

They may contain mandatory observable UI requirements.

They do not define controllers, endpoints, database schema, ORM mappings, React components, final aggregate implementation or final visual design.
