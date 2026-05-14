# Scenario Text Specs Index

Status: current scenario text-spec navigation index

## 1. Purpose

This folder contains corrected textual scenario specifications.

They are the primary scenario source of truth together with DATA specs and validation/security addenda.

## 2. Read First

```text
00-scenario-text-specs-index.md
scenario-server-domain-validation-addendum.md
scenario-account-activation-security-addendum.md
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

## 4. Merged / Removed / Deferred Specs

```text
SC-08-merged-approved-result.md
SC-09-merged-rejected-result.md
SC-12-merged-review-feedback-correction-navigation.md
SC-16-removed-notification-navigation.md
SC-18-archive-audit-deferred.md
```

## 5. Validation / Security Addenda

Use:

```text
scenario-server-domain-validation-addendum.md
scenario-account-activation-security-addendum.md
```

They record:

```text
- client-side validation;
- server-side / domain validation;
- value object / domain validation hints per scenario;
- account activation / protected access requirements.
```

Do not move these validation/security rules into DATA files.

## 6. Current Downstream Use

Current pre-domain coverage baseline:

```text
planning/tables/pre-domain-variants-input.md
```

Account activation baseline addendum:

```text
planning/tables/scenario-behavior-baseline-account-activation-addendum.md
```

Current domain draft branch:

```text
planning/domain-draft-generation-guide.md
planning/tables/domain-drafts/
```

Current UI planning branch:

```text
planning/ui/test-site-ui-plan.md
planning/ui/ui-questions-register.md
```

Current next domain step:

```text
Create / refine domain draft 1.
```

Current next UI step:

```text
Create/fill test-site-ui-plan.md and ui-questions-register.md.
```

Do not use the old downstream path as current workflow:

```text
planning/tables/scenario-domain-design-input-core.md
planning/tables/domain-discovery-core.md
planning/tables/domain-variants/
```

## 7. Rule

Scenario text specs describe behavior and domain-relevant rules.

They may contain mandatory observable UI requirements.

They do not define:

```text
- controllers;
- endpoints;
- database schema;
- ORM mappings;
- React components;
- final aggregate implementation;
- final visual design.
```
