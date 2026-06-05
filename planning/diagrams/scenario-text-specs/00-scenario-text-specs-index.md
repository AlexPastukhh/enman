# Scenario Text Specifications Index

Status: current scenario text-spec index / first-pass currentness cleanup  
Doc version: v0.1.0  
Scope: current, variant, historical and deprecated scenario text specs

## 0. Template / Policy

Use the canonical template for new core/business scenario specs and intentional rewrites:

```text
SCENARIO-TEXT-SPEC-TEMPLATE.md
```

The template uses inline DATA by default.

Existing scenario specs may keep older structures during migration. Do not rewrite existing files only to match the template.


## 1. Current Primary Scenario Text Specs

| Scenario | Current primary file | Status | Notes |
|---|---|---|---|
| SC-01 Guest Registration | `SC-01-guest-registration.md` | current | Auth/account source. |
| SC-02 Login | `SC-02-login.md` | current | Auth/session source. |
| SC-03A Password Recovery Request | `SC-03A-password-recovery-request.md` | current | Auth recovery source. |
| SC-03B Account Owner Verified | `SC-03B-account-owner-verified.md` | current | Account activation/verification source. |
| SC-04 Client Request Creation | `SC-04-client-request-creation.md` | current primary | Uses explicit Existing/New applicant context. |
| SC-05 My Requests / Own Request Details | `SC-05-my-requests-own-request-details.md` | current primary | Own request list/details source. |
| SC-06 Employee Request Dashboard | `SC-06-employee-request-dashboard.md` | current | Employee request list/dashboard. |
| SC-07A Employee Request Details | `SC-07A-employee-request-details.md` | current | Employee read/details scenario. |
| SC-07B Employee Request Review | `SC-07B-employee-request-review.md` | current | Employee approve/reject review scenario. |
| SC-10 Applicant Data | `SC-10-applicant-data.md` | current | ApplicantParty/default applicant source. |
| SC-10B My Applicant Parties | `SC-10B-my-applicant-parties.md` | future/current design | Same-page applicant party management. |
| SC-11 Request Documents | `SC-11-request-documents.md` | planned/future | Request documents source. |
| SC-13A My Agreements | `SC-13A-my-agreements.md` | current primary | Preferred over older client-agreements naming. |
| SC-13B Agreement Proposal Details / Response | `SC-13B-agreement-proposal-details-response.md` | current primary | Preferred over older client-agreement naming. |
| SC-13C Employee Agreements | `SC-13C-employee-agreements.md` | current | Employee agreement list/source. |
| SC-13D Employee Agreement Proposal Create / Send Version | `SC-13D-employee-agreement-proposal-create-response.md` | current primary | Preferred current file; send-version file is terminology variant/context. |
| SC-13E Agreement Final Refusal | `SC-13E-agreement-final-refusal.md` | current | Final refusal is exchange state. |
| SC-14 Agreement Documents | `SC-14-agreement-documents.md` | current primary | SC-14 means Agreement Documents / AgreementDocumentRef. |
| SC-15 Security | `SC-15-security-text-specification.md` | current cross-cutting | Security behavior source. |
| SC-17 Anonymous Request | `SC-17-anonymous-request.md` | future/deferred | Anonymous request source. |
| SC-18 Archive/Audit Deferred | `SC-18-archive-audit-deferred.md` | future/deferred | Archive/audit deferred source. |
| SC-19 Document Template Management | `SC-19-document-template-management.md` | future/planned | Employee template creation MVP; future admin ownership. |
| SC-20 Employee Proposal Document Generation From Template | `SC-20-employee-proposal-document-generation-from-template.md` | future/planned | Employee chooses active template when sending proposal version; upload remains alternative. |
| SC-21 Admin-Controlled Document Templates And Client-Visible Options | `SC-21-admin-controlled-document-templates.md` | future/deferred | Admin controls official templates; client-visible options are later and restricted. |

## 2. Merged / Historical Text Specs

| File | Status | Notes |
|---|---|---|
| `SC-08-merged-approved-result.md` | merged/historical | Approved result behavior is covered by request/review sources. |
| `SC-09-merged-rejected-result.md` | merged/historical | Rejected result behavior is covered by request/review sources. |
| `SC-12-merged-review-feedback-correction-navigation.md` | merged/future | Review feedback/correction navigation context. |
| `SC-13-pending-agreement-proposal-model.md` | historical/pending context | Early agreement proposal model source. |
| `SC-16-removed-notification-navigation.md` | historical/removed | Removed notification navigation. |

## 3. Variant / Legacy Naming Files

| File | Status | Current route |
|---|---|---|
| `SC-13A-client-agreements.md` | legacy naming variant | Prefer `SC-13A-my-agreements.md`. |
| `SC-13B-client-agreement-proposal-details-response.md` | legacy naming variant | Prefer `SC-13B-agreement-proposal-details-response.md`. |
| `SC-13D-employee-agreement-proposal-create-send-version.md` | terminology variant/context | Prefer `SC-13D-employee-agreement-proposal-create-response.md` until explicitly renamed/superseded. |
| `SC-14-client-data-verification.md` | stale/deferred old meaning | Current SC-14 is `SC-14-agreement-documents.md`. |

## 4. Addenda / Deprecated Context

| File | Status | Current route |
|---|---|---|
| `scenario-account-activation-security-addendum.md` | context/current where relevant | Use with auth/account scenarios. |
| `scenario-browser-security-addendum.md` | context/current where relevant | Use with browser/security scenarios. |
| `deprecated/scenario-server-domain-validation-addendum.deprecated.md` | deprecated historical source | Use `planning/scenario-domain-validation-principles.md`, scenario-local behavior items, clarifications and domain docs instead. |

## 5. Current L1 Applicant Decision

```text
many saved ApplicantParties;
one current/default template per applicant type;
current/default is prefill/default only;
request creation uses explicit Existing/New applicant context.
```

## 6. Current L2 / Agreement Guardrails

```text
Use Employee, Start/Started, StartReview, StartByEmployee,
EmployeeSendNewVersion, AwaitingEmployeeResponse.

Do not use Worker, Open/Opened, EmployeeRef, WorkerRef,
StartByWorker or AwaitingWorkerResponse.

Counter-proposal replacement is SupersededByCounterProposal,
not ordinary Rejected.

Rejected is only explicit rejection/decline.

Current SC-14 is Agreement Documents / AgreementDocumentRef.
Do not use stale SC-14 Client Data Verification wording for current agreement diagrams.

Document generation future scenarios:
SC-19/SC-20/SC-21 are future/planned sources only. They do not mean template generation is implemented.
FUT-DOCGEN-1 is employee template creation/use MVP; FUT-DOCGEN-2 is later admin governance; FUT-DOCGEN-3 is later client-visible options.
Client must not create templates; client template/option selection is deferred and must be admin-controlled.
```

## 7. Artifact Map

For full currentness and cross-layer mapping, read:

```text
planning/diagrams/scenario-artifact-map.md
```
