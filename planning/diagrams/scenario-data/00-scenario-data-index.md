# Scenario DATA Index

Status: current scenario DATA index / first-pass currentness cleanup  
Doc version: v0.1.0  
Scope: scenario DATA files and stale/pending DATA variants

## 1. Current DATA Files

| Scenario | DATA file | Status | Notes |
|---|---|---|---|
| SC-01 Guest Registration | `SC-01-registration-data.md` | current | Registration/auth DATA. |
| SC-02 Login | `SC-02-login-data.md` | current | Login/session DATA. |
| SC-03A Password Recovery | `SC-03A-password-recovery-data.md` | current | Password recovery request DATA. |
| SC-03B Account Owner Verified | `SC-03B-account-owner-verified-data.md` | current | Verification/activation DATA. |
| SC-04 Client Request Creation | `SC-04-request-creation-data.md` | current | Request creation + applicant context DATA. |
| SC-05 My Requests | `SC-05-my-requests-data.md` | current | List/details/filter DATA. |
| SC-06 Employee Dashboard | `SC-06-employee-dashboard-data.md` | current | Employee request list DATA. |
| SC-07A Employee Request Details | `SC-07A-employee-request-details-data.md` | current | Employee details DATA. |
| SC-07B Employee Request Review | `SC-07B-employee-request-review-data.md` | current | Review command DATA. |
| SC-10 Applicant Data | `SC-10-applicant-data.md` | current | ApplicantParty DATA. |
| SC-10B My Applicant Parties | `SC-10B-my-applicant-parties-data.md` | future/current design | Same-page applicant management DATA. |
| L2 Employee Review Agreement | `L2-employee-review-agreement-data.md` | current derived / cross-scenario | Employee review/agreement DATA derived from L2 scenario/domain sources. |
| SC-13A My Agreements | `SC-13A-my-agreements-data.md` | current | Client agreement list DATA. |
| SC-13B Proposal Details / Response | `SC-13B-agreement-proposal-details-response-data.md` | current | Proposal response DATA. |
| SC-13C Employee Agreements | `SC-13C-employee-agreements-data.md` | current | Employee agreement list DATA. |
| SC-13D Create / Send Proposal Version | `SC-13D-employee-agreement-proposal-create-response-data.md` | current | Proposal create/send version DATA. |

## 2. Pending / Stale / Deferred DATA Files

| File | Status | Notes |
|---|---|---|
| `SC-10-applicant-data-pending.md` | historical/pending context | Prefer `SC-10-applicant-data.md`. |
| `SC-13-agreement-proposal-data-pending.md` | historical/pending context | Prefer SC-13A/B/C/D DATA files and L2 agreement DATA. |
| `SC-14-client-data-verification-data.md` | stale/deferred context | Current SC-14 means Agreement Documents / AgreementDocumentRef, not Client Data Verification. |

## 3. Key Notes

```text
SC-05-DATA-02 status is the first My Requests filter data item.
Future request type/date/search filters must be added to SC-05 DATA before client sidecars implement them.
L2 employee/review/agreement DATA is derived from L2 scenario/domain sources and should be replaced by per-scenario DATA where available.
```

## 4. Artifact Map

For full currentness and cross-layer mapping, read:

```text
planning/diagrams/scenario-artifact-map.md
```
