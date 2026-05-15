# Scenario UI Specs Index

Status: index / scaffold  
Scope: global index of per-scenario UI specs

## 1. Purpose

This file tracks which scenario UI specs exist or are planned. Do not duplicate all UI behavior details here.

## 2. Current Rule

Create concrete UI specs only when needed for active client planning.

## 3. Planned UI Spec Files

| Scenario | Planned file | Status | Notes |
|---|---|---|---|
| SC-01 Guest Registration | SC-01-guest-registration-ui.md | planned | Auth UI when needed |
| SC-02 Login | SC-02-login-ui.md | planned | Auth/session UI when needed |
| SC-03A Password Recovery Request | SC-03A-password-recovery-request-ui.md | planned | Future auth UI |
| SC-03B Account Owner Verified | SC-03B-account-owner-verified-ui.md | planned | Future auth UI |
| SC-04 Client Request Creation | SC-04-client-request-creation-ui.md | planned | Likely first concrete UI spec |
| SC-05 My Requests / Own Request Details | SC-05-my-requests-own-request-details-ui.md | planned | Read UI |
| SC-06 Employee Request Dashboard | SC-06-employee-request-dashboard-ui.md | planned | Employee read UI |
| SC-07A Employee Request Details | SC-07A-employee-request-details-ui.md | planned | Employee details UI |
| SC-07B Employee Request Review | SC-07B-employee-request-review-ui.md | planned | Review command UI |
| SC-10 Applicant Data | SC-10-applicant-data-ui.md | planned | Applicant form UI |
| SC-11 Request Documents | SC-11-request-documents-ui.md | planned | Future document UI |
| SC-13A My Agreements | SC-13A-my-agreements-ui.md | planned | Agreement read UI |
| SC-13B Agreement Proposal Details / Response | SC-13B-agreement-proposal-details-response-ui.md | planned | Agreement response UI |
| SC-13C Employee Agreements | SC-13C-employee-agreements-ui.md | planned | Employee agreement read UI |
| SC-13D Employee Agreement Proposal Create / Send Version | SC-13D-employee-agreement-proposal-create-response-ui.md | planned | Agreement proposal command UI |
| SC-14 Client Data Verification | SC-14-client-data-verification-ui.md | planned | Verification UI/plugin |
| SC-15 Security Text Specification | SC-15-security-ui.md | planned | Auth/security UI |
| SC-17 Anonymous Request | SC-17-anonymous-request-ui.md | planned | Future anonymous UI |

## 4. Lightweight UI Item Index

| UI item ID | Scenario | Short name | Source UI spec | Downstream sidecars | Status |
|---|---|---|---|---|---|
| TBD | TBD | TBD | TBD | TBD | planned |

## 5. Migration Rule

When scenario/client planning discovers UI-visible behavior:

```text
1. If it is scenario-visible, add/update scenario UI spec.
2. If it changes scenario behavior/DATA/security, use scenario question loop.
3. If it is implementation-only, keep it in `.client.md`.
4. If it is a client-wide convention, move it to planning/client/cross-cutting/.
```
