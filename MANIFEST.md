# MANIFEST — L2 Account / Employee TPH Domain Sync

Status: docs-only replacement archive  
Scope: domain decision addendum + L2 Employee request/review slice docs/register sync

## Files

```text
planning/tables/domain-drafts/domain-draft-02-account-employee-tph-decision.md
planning/diagrams/scenario-clarifications/L2-employee-review-agreement-domain-direction.md
planning/slices/SL-EMP-REQ-001-employee-request-list-read.md
planning/slices/SL-EMP-REQ-002-employee-request-details-read.md
planning/slices/SL-EMP-REQ-003-start-request-review.md
planning/slices/l2/L2-EMP-DASH-001-employee-request-dashboard.client.md
planning/slices/l2/L2-EMP-DETAILS-001-employee-request-details.client.md
planning/slices/l2/L2-REVIEW-START-001-start-request-review.client.md
planning/slices/l2/README.md
planning/slices/slice-extension-points-register.md
planning/slices/slice-implementation-notes-register.md
planning/slices/slice-questions-register.md
planning/slices/slice-scenario-flow-behavior-register.md
```

## Main decision

```text
Account
  -> ClientAccount
  -> Employee
```

```text
Persistence: L1Accounts TPH with AccountType / Role discriminator.
Identity: ClaimTypes.NameIdentifier = Account.Id.
For Employee sessions: Account.Id == Employee.Id.
```

Do not model target Employee as `EmployeeProfile(AccountId)`.
Do not use `EmployeeRef` in L2 target review/agreement drafts.

## Runtime scope

No runtime code, no tests, no generated artifacts.
```
