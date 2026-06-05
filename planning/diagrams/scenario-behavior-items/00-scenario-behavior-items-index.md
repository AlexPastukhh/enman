# Scenario Behavior Items Index

Status: current behavior item index / first-pass currentness cleanup  
Doc version: v0.1.0  
Scope: per-scenario and cross-cutting behavior item files

## 1. Current Behavior Item Files

| File | Scenario / concern | Status | Notes |
|---|---|---|---|
| `SC-01-guest-registration-behavior-items.md` | SC-01 Guest Registration | current | Auth/account behavior. |
| `SC-02-login-behavior-items.md` | SC-02 Login | current | Login/session behavior. |
| `SC-03A-password-recovery-request-behavior-items.md` | SC-03A Password Recovery | current | Recovery behavior. |
| `SC-03B-account-owner-verified-behavior-items.md` | SC-03B Account Owner Verified | current | Verification/activation behavior. |
| `SC-04-client-request-creation-behavior-items.md` | SC-04 Client Request Creation | current primary | Prefer over older generic request-creation naming. |
| `SC-04-request-creation-behavior-items.md` | SC-04 Request Creation | legacy/compat variant | Keep as context until merged/superseded. |
| `SC-05-my-requests-behavior-items.md` | SC-05 My Requests | current primary | List/details/filter behavior. |
| `SC-05-my-requests-own-request-details-behavior-items.md` | SC-05 Own Request Details | variant/context | Keep as context until merged/superseded. |
| `SC-06-employee-request-dashboard-behavior-items.md` | SC-06 Employee Dashboard | current | Employee request list behavior. |
| `SC-07A-employee-request-details-behavior-items.md` | SC-07A Employee Details | current | Employee details behavior. |
| `SC-07B-employee-request-review-behavior-items.md` | SC-07B Employee Review | current | Review command behavior. |
| `SC-10-applicant-data-behavior-items.md` | SC-10 Applicant Data | current | ApplicantParty behavior. |
| `SC-10B-my-applicant-parties-behavior-items.md` | SC-10B My Applicant Parties | future/current design | Future applicant management behavior. |
| `SC-11-request-documents-behavior-items.md` | SC-11 Request Documents | planned/future | Document behavior. |
| `SC-13A-my-agreements-behavior-items.md` | SC-13A My Agreements | current | Client agreements list behavior. |
| `SC-13B-agreement-proposal-details-response-behavior-items.md` | SC-13B Proposal Details / Response | current | Proposal response behavior. |
| `SC-13C-employee-agreements-behavior-items.md` | SC-13C Employee Agreements | current | Employee agreement list behavior. |
| `SC-13D-employee-agreement-proposal-create-response-behavior-items.md` | SC-13D Create / Send Proposal Version | current | Agreement proposal command behavior. |
| `SC-14-client-data-verification-behavior-items.md` | SC-14 old Client Data Verification | stale/deferred context | Current SC-14 is Agreement Documents / AgreementDocumentRef; do not use this as current agreement-document behavior. |
| `SC-15-security-text-specification-behavior-items.md` | SC-15 Security | current cross-cutting | Security behavior. |
| `SC-17-anonymous-request-behavior-items.md` | SC-17 Anonymous Request | future/deferred | Anonymous request behavior. |
| `SC-19-document-template-management-behavior-items.md` | SC-19 Document Template Management | future/planned | Employee template creation MVP and future governance boundary. |
| `SC-20-employee-proposal-document-generation-from-template-behavior-items.md` | SC-20 Employee Proposal Document Generation From Template | future/planned | Employee generates proposal document from active template; upload remains alternative. |
| `SC-21-admin-controlled-document-templates-behavior-items.md` | SC-21 Admin-Controlled Document Templates | future/deferred | Admin governance and client-visible options after employee MVP. |
| `L2-employee-review-agreement-behavior-items.md` | L2 employee review/agreement family | current derived / cross-scenario | Derived from L2 scenario/domain sources; use until per-scenario files fully split. |
| `CC-CSRF-001-antiforgery-behavior-items.md` | CSRF / unsafe command protection | current cross-cutting | Cross-cutting security behavior. |

## 2. Behavior Item Categories

```text
CMD  command behavior
LC   lifecycle / state / condition behavior
IBS  impossible business state
VI   value integrity
UCQ  use-case coordination
READ read/access/listing behavior
INT  integration/side-effect expectation
FUT  future/deferred behavior
NW   no-write / failure preservation behavior
```

## 3. Behavior-Source Rule

```text
Implementation details are not behavior items.
Behavior items are scenario/source behavior consumed by domain, slice, client and testing docs.
L2 behavior items are derived cross-scenario behavior until more detailed per-scenario behavior files split them further.
```

## 4. Artifact Map

For currentness, variants and downstream links, read:

```text
planning/diagrams/scenario-artifact-map.md
```
