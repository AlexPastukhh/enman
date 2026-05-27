# Scenario UI Specs Index

Status: current UI spec index / first-pass currentness cleanup  
Scope: existing and planned per-scenario UI specs

## 1. Purpose

This file tracks which scenario UI specs exist or are planned. Do not duplicate all UI behavior details here.

Concrete UI specs are created when needed for active client planning.

## 2. Current UI Specs

| Area / Scenario | File | Status | Notes |
|---|---|---|---|
| App shell / auth flow | `APP-UI-001-app-shell-home-auth-flow-ui.md` | current | App shell, home/auth entry UI behavior. |
| SC-04 Client Request Creation | `SC-04-request-creation-ui.md` | current | Request creation form UI. |
| SC-05 My Requests | `SC-05-my-requests-ui.md` | current | My requests list/details UI. |
| SC-10 Applicant Data | `SC-10-applicant-data-ui.md` | current | Applicant form UI. |
| SC-10B My Applicant Parties | `SC-10B-my-applicant-parties-ui.md` | future/current design | Same-page applicant party management UI. |

## 3. Planned UI Specs

| Scenario | Planned file | Status | Notes |
|---|---|---|---|
| SC-01 Guest Registration | `SC-01-guest-registration-ui.md` | planned | Auth UI when needed. |
| SC-02 Login | `SC-02-login-ui.md` | planned | Auth/session UI when needed. |
| SC-03A Password Recovery Request | `SC-03A-password-recovery-request-ui.md` | planned | Future auth UI. |
| SC-03B Account Owner Verified | `SC-03B-account-owner-verified-ui.md` | planned | Future auth UI. |
| SC-06 Employee Request Dashboard | `SC-06-employee-request-dashboard-ui.md` | planned | Employee read UI. |
| SC-07A Employee Request Details | `SC-07A-employee-request-details-ui.md` | planned | Employee details UI. |
| SC-07B Employee Request Review | `SC-07B-employee-request-review-ui.md` | planned | Review command UI. |
| SC-11 Request Documents | `SC-11-request-documents-ui.md` | planned | Future document UI. |
| SC-13A My Agreements | `SC-13A-my-agreements-ui.md` | planned | Agreement read UI. |
| SC-13B Agreement Proposal Details / Response | `SC-13B-agreement-proposal-details-response-ui.md` | planned | Agreement response UI. |
| SC-13C Employee Agreements | `SC-13C-employee-agreements-ui.md` | planned | Employee agreement read UI. |
| SC-13D Employee Agreement Proposal Create / Send Version | `SC-13D-employee-agreement-proposal-create-response-ui.md` | planned | Agreement proposal command UI. |
| SC-13E Agreement Final Refusal | `SC-13E-agreement-final-refusal-ui.md` | planned | Final refusal UI. |
| SC-14 Agreement Documents | `SC-14-agreement-documents-ui.md` | planned | Agreement document metadata/file-reference UI. |
| SC-15 Security | `SC-15-security-ui.md` | planned | Security/cross-cutting UI where needed. |

## 4. UI Spec Support Files

```text
UI-SCENARIO-CONVENTIONS.md
UI-SCENARIO-READINESS.md
UI-SCENARIO-TEMPLATE.md
```

## 5. Guardrails

```text
Scenario UI specs describe UI-visible requirements and accepted UI decisions.
They do not define React implementation, component structure or CSS architecture.
Client implementation details belong in client/slice docs.
```
