# Scenario Artifact Map

Status: current scenario artifact map / first-pass cleanup scaffold  
Scope: map scenario IDs to text, DATA, UI, behavior, clarification, domain and slice sources

## 1. Purpose

This file shows which scenario artifacts exist, which variants are current, and where downstream domain/slice docs should look first.

It is not a replacement for scenario files. It is a map for discovery and cleanup.

## 2. Source Model

Primary scenario artifacts:

```text
planning/diagrams/scenario-text-specs/
planning/diagrams/scenario-data/
planning/diagrams/scenario-ui-specs/
planning/diagrams/scenario-behavior-items/
planning/diagrams/scenario-clarifications/
planning/diagrams/scenario-questions-register.md
```

Downstream maps:

```text
planning/domain/scenario-to-aggregate-map.md
planning/slices/slice-scenario-flow-behavior-register.md
```

Historical / deprecated context:

```text
planning/diagrams/scenario-text-specs/deprecated/
planning/tables/pre-domain-variants-input.md
planning/tables/domain-drafts/
```

## 3. Scenario Artifact Matrix

| Scenario | Current text spec | DATA | UI spec | Behavior items | Clarifications / notes | Domain / slice link | Status |
|---|---|---|---|---|---|---|---|
| SC-01 Guest Registration | `scenario-text-specs/SC-01-guest-registration.md` | `scenario-data/SC-01-registration-data.md` | planned | `scenario-behavior-items/SC-01-guest-registration-behavior-items.md` | account/security addenda as context | Account/auth later | current source |
| SC-02 Login | `scenario-text-specs/SC-02-login.md` | `scenario-data/SC-02-login-data.md` | planned | `scenario-behavior-items/SC-02-login-behavior-items.md` | browser/security addenda as context | Account/auth later | current source |
| SC-03A Password Recovery Request | `scenario-text-specs/SC-03A-password-recovery-request.md` | `scenario-data/SC-03A-password-recovery-data.md` | planned | `scenario-behavior-items/SC-03A-password-recovery-request-behavior-items.md` | security addenda as context | Account/auth later | current source |
| SC-03B Account Owner Verified | `scenario-text-specs/SC-03B-account-owner-verified.md` | `scenario-data/SC-03B-account-owner-verified-data.md` | planned | `scenario-behavior-items/SC-03B-account-owner-verified-behavior-items.md` | account activation guardrails | Account/auth later | current source |
| SC-04 Client Request Creation | `scenario-text-specs/SC-04-client-request-creation.md` | `scenario-data/SC-04-request-creation-data.md` | `scenario-ui-specs/SC-04-request-creation-ui.md` | `scenario-behavior-items/SC-04-client-request-creation-behavior-items.md` | `SC-04-request-creation-behavior-items.md` is legacy/compat variant | `planning/domain/aggregates/connection-request.md`, `planning/domain/aggregates/applicant-party.md` | current primary with legacy behavior variant |
| SC-05 My Requests / Own Request Details | `scenario-text-specs/SC-05-my-requests-own-request-details.md` | `scenario-data/SC-05-my-requests-data.md` | `scenario-ui-specs/SC-05-my-requests-ui.md` | `scenario-behavior-items/SC-05-my-requests-behavior-items.md` | `SC-05-my-requests-own-request-details-behavior-items.md` is details-specific/legacy variant | ConnectionRequest read side / slices | current primary with variant |
| SC-06 Employee Request Dashboard | `scenario-text-specs/SC-06-employee-request-dashboard.md` | `scenario-data/SC-06-employee-dashboard-data.md` | planned | `scenario-behavior-items/SC-06-employee-request-dashboard-behavior-items.md` | L2 agreement clarifications may affect filters/status wording | ConnectionRequest read side / slices | current source |
| SC-07A Employee Request Details | `scenario-text-specs/SC-07A-employee-request-details.md` | `scenario-data/SC-07A-employee-request-details-data.md` | planned | `scenario-behavior-items/SC-07A-employee-request-details-behavior-items.md` | L2 agreement clarifications may affect review/exchange wording | ConnectionRequest read side / slices | current source |
| SC-07B Employee Request Review | `scenario-text-specs/SC-07B-employee-request-review.md` | `scenario-data/SC-07B-employee-request-review-data.md` | planned | `scenario-behavior-items/SC-07B-employee-request-review-behavior-items.md` | `L2-employee-review-agreement-domain-direction.md` | `planning/domain/aggregates/connection-request.md` | current source |
| SC-08 Approved Result | `scenario-text-specs/SC-08-merged-approved-result.md` | included in review/request sources | planned | covered by L2 behavior sources | merged result scenario | ConnectionRequest status/result | merged/historical source |
| SC-09 Rejected Result | `scenario-text-specs/SC-09-merged-rejected-result.md` | included in review/request sources | planned | covered by L2 behavior sources | merged result scenario | ConnectionRequest status/result | merged/historical source |
| SC-10 Applicant Data | `scenario-text-specs/SC-10-applicant-data.md` | `scenario-data/SC-10-applicant-data.md` | `scenario-ui-specs/SC-10-applicant-data-ui.md` | `scenario-behavior-items/SC-10-applicant-data-behavior-items.md` | `SC-10-applicant-data-pending.md` is older/pending context | `planning/domain/aggregates/applicant-party.md` | current source |
| SC-10B My Applicant Parties | `scenario-text-specs/SC-10B-my-applicant-parties.md` | `scenario-data/SC-10B-my-applicant-parties-data.md` | `scenario-ui-specs/SC-10B-my-applicant-parties-ui.md` | `scenario-behavior-items/SC-10B-my-applicant-parties-behavior-items.md` | future same-page management | ApplicantParty future management | future/current design source |
| SC-11 Request Documents | `scenario-text-specs/SC-11-request-documents.md` | later document DATA | planned | `scenario-behavior-items/SC-11-request-documents-behavior-items.md` | document behavior partly overlaps agreement document refs | Request documents / agreement documents later | planned/future |
| SC-12 Review Feedback Correction Navigation | `scenario-text-specs/SC-12-merged-review-feedback-correction-navigation.md` | included in review/request sources | planned | covered by review/request behavior | merged correction navigation | ConnectionRequest review feedback | merged/future |
| SC-13 Pending Agreement Proposal Model | `scenario-text-specs/SC-13-pending-agreement-proposal-model.md` | `scenario-data/SC-13-agreement-proposal-data-pending.md` | planned | L2 behavior items | historical setup for SC-13 family | AgreementProposalExchange | historical/pending context |
| SC-13A Client Agreements / My Agreements | `scenario-text-specs/SC-13A-my-agreements.md` is preferred current name; `SC-13A-client-agreements.md` is legacy naming variant | `scenario-data/SC-13A-my-agreements-data.md` | planned | `scenario-behavior-items/SC-13A-my-agreements-behavior-items.md` | naming cleanup needed | AgreementProposalExchange read side | current primary with legacy text variant |
| SC-13B Proposal Details / Response | `scenario-text-specs/SC-13B-agreement-proposal-details-response.md` preferred; `SC-13B-client-agreement-proposal-details-response.md` legacy naming variant | `scenario-data/SC-13B-agreement-proposal-details-response-data.md` | planned | `scenario-behavior-items/SC-13B-agreement-proposal-details-response-behavior-items.md` | `AGR-001-agreement-proposal-replacement-terminology.md` | AgreementProposalExchange | current primary with legacy text variant |
| SC-13C Employee Agreements | `scenario-text-specs/SC-13C-employee-agreements.md` | `scenario-data/SC-13C-employee-agreements-data.md` | planned | `scenario-behavior-items/SC-13C-employee-agreements-behavior-items.md` | L2 agreement clarifications | AgreementProposalExchange read side | current source |
| SC-13D Employee Create / Send Version | `scenario-text-specs/SC-13D-employee-agreement-proposal-create-response.md` preferred current file; `SC-13D-employee-agreement-proposal-create-send-version.md` terminology variant | `scenario-data/SC-13D-employee-agreement-proposal-create-response-data.md` | planned | `scenario-behavior-items/SC-13D-employee-agreement-proposal-create-response-behavior-items.md` | `AGR-001`, L2 cleanup clarifications | `planning/domain/aggregates/agreement-proposal-exchange.md` | current primary with terminology variant |
| SC-13E Agreement Final Refusal | `scenario-text-specs/SC-13E-agreement-final-refusal.md` | L2 agreement DATA | planned | L2 behavior items | final refusal is exchange state | AgreementProposalExchange | current source |
| SC-14 Agreement Documents | `scenario-text-specs/SC-14-agreement-documents.md` preferred current file; `SC-14-client-data-verification.md` is stale/deferred wording | agreement document DATA later; `SC-14-client-data-verification-data.md` stale/deferred context | planned | `SC-14-client-data-verification-behavior-items.md` is stale/deferred context | SC-14 means Agreement Documents / AgreementDocumentRef | AgreementDocumentRef / documents | current primary with stale/deferred old variant |
| SC-15 Security | `scenario-text-specs/SC-15-security-text-specification.md` | security-specific DATA later | planned | `scenario-behavior-items/SC-15-security-text-specification-behavior-items.md`, `CC-CSRF-001-antiforgery-behavior-items.md` | browser/security addenda, cross-cutting security behavior | Cross-cutting security / slices | current cross-cutting source |
| SC-16 Removed Notification Navigation | `scenario-text-specs/SC-16-removed-notification-navigation.md` | none | planned | none | removed/deferred navigation | none | historical/removed |
| SC-17 Anonymous Request | `scenario-text-specs/SC-17-anonymous-request.md` | later | planned | `scenario-behavior-items/SC-17-anonymous-request-behavior-items.md` | anonymous request deferred | future domain/slice | future/deferred |
| SC-18 Archive Audit Deferred | `scenario-text-specs/SC-18-archive-audit-deferred.md` | later | planned | none | archive/audit deferred | future domain/slice/testing | future/deferred |

## 4. Variant / Staleness Notes

```text
SC-04:
  Prefer SC-04-client-request-creation naming for current text/behavior route.
  Keep SC-04-request-creation behavior file as legacy/compat context until merged or superseded.

SC-05:
  Prefer SC-05-my-requests behavior file as current list/details source.
  Details-specific behavior file remains variant/context until merged.

SC-13A / SC-13B / SC-13D:
  Prefer current agreement naming files listed in the matrix.
  Keep older client/send-version naming variants as historical/terminology context until explicitly superseded.

SC-14:
  Current SC-14 means Agreement Documents / AgreementDocumentRef.
  Client Data Verification wording is stale/deferred and must not drive current agreement-document diagrams.

Deprecated global validation addendum:
  `scenario-server-domain-validation-addendum.deprecated.md` is historical context only.
  Current validation/domain route is scenario-local behavior items + clarifications + `planning/scenario-domain-validation-principles.md` + domain docs.
```

## 5. Next Cleanup Tasks

```text
- Merge/supersede duplicate SC-04 and SC-05 behavior item variants.
- Decide whether SC-13A/13B/13D legacy naming files should be renamed, archived or kept as historical variants.
- Replace current references to deprecated global validation addendum with current route wording.
- Currentize or mark historical scenario-diagram-consistency-report.md.
- Fill UI specs for SC-06/07A/07B/13A/13B/13C/13D when client planning needs them.
```
