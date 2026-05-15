# Scenario Behavior Items Index

Status: scaffold / lightweight index  
Scope: global index of per-scenario behavior item files

## 1. Purpose

This file is a lightweight index.

It should not duplicate all behavior item details.

Detailed cards/tables belong in per-scenario behavior item files.

## 2. Current Position

The current compiled downstream baseline remains:

```text
planning/tables/pre-domain-variants-input.md
```

Behavior item migration into per-scenario files is a separate future step.

## 3. Planned Per-Scenario Files

| Scenario | Planned file | Status | Notes |
|---|---|---|---|
| SC-01 Guest Registration | SC-01-guest-registration-behavior-items.md | planned | Account registration / activation questions |
| SC-02 Login | SC-02-login-behavior-items.md | planned | Auth/session behavior |
| SC-04 Client Request Creation | SC-04-client-request-creation-behavior-items.md | planned | Request creation / request Client/UI |
| SC-05 My Requests / Own Request Details | SC-05-my-requests-own-request-details-behavior-items.md | planned | Read/client behavior |
| SC-06 Employee Request Dashboard | SC-06-employee-request-dashboard-behavior-items.md | planned | Employee read/client behavior |
| SC-07A Employee Request Details | SC-07A-employee-request-details-behavior-items.md | planned | Details/read behavior |
| SC-07B Employee Request Review | SC-07B-employee-request-review-behavior-items.md | planned | Approve/reject behavior |
| SC-10 Applicant Data | SC-10-applicant-data-behavior-items.md | planned | Applicant form/request dependency |
| SC-11 Request Documents | SC-11-request-documents-behavior-items.md | planned | Future extension |
| SC-13A My Agreements | SC-13A-my-agreements-behavior-items.md | planned | Agreement read behavior |
| SC-13B Agreement Proposal Details / Response | SC-13B-agreement-proposal-details-response-behavior-items.md | planned | Agreement response behavior |
| SC-13C Employee Agreements | SC-13C-employee-agreements-behavior-items.md | planned | Employee agreement read behavior |
| SC-13D Employee Agreement Proposal Create / Send Version | SC-13D-employee-agreement-proposal-create-response-behavior-items.md | planned | Agreement proposal creation |
| SC-14 Client Data Verification | SC-14-client-data-verification-behavior-items.md | planned | Verification plugin/support |
| SC-15 Security Text Specification | SC-15-security-text-specification-behavior-items.md | planned | Cross-cutting security |
| SC-17 Anonymous Request | SC-17-anonymous-request-behavior-items.md | planned | Future extension |

## 4. Lightweight Item Index

When behavior items are migrated, use:

| Item ID | Category | Scenario | Short name | Source file | Downstream users | Status |
|---|---|---|---|---|---|---|
| TBD | TBD | TBD | TBD | TBD | TBD | pending migration |

## 5. Migration Rule

```text
1. Preserve item IDs where possible.
2. Preserve category style.
3. Do not invent new behavior.
4. If source scenario/DATA is missing, update source artifacts through scenario question loop.
5. Update this lightweight index after creating each per-scenario file.
```
