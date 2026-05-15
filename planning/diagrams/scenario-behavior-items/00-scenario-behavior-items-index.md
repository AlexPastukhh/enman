# Scenario Behavior Items Index

Status: migrated index v1  
Scope: lightweight global index of migrated behavior items

## 1. Purpose

This file is a lightweight global index.

Detailed cards/tables live in per-scenario behavior item files.

## 2. Source Baselines

```text
planning/tables/pre-domain-variants-input.md
planning/tables/scenario-behavior-baseline-account-activation-addendum.md
```

## 3. Item Index

| Item ID | Category | Owning scenario | Source | Short name | Migrated to | Status |
|---|---|---|---|---|---|---|
| ACC-CMD-REGISTER-001 | CMD | SC-01 | SC-01 | Register account | `SC-01-guest-registration-behavior-items.md` | migrated |
| ACC-CMD-RECOVERY-001 | CMD | SC-03A | SC-03A | Request password recovery | `SC-03A-password-recovery-request-behavior-items.md` | migrated |
| ACC-CMD-RESET-001 | CMD | SC-03B | SC-03B | Set new password | `SC-03B-account-owner-verified-behavior-items.md` | migrated |
| APPL-CMD-SAVE-001 | CMD | SC-10 | SC-10 | Save applicant data | `SC-10-applicant-data-behavior-items.md` | migrated |
| REQ-CMD-CREATE-001 | CMD | SC-04 | SC-04 | Create request | `SC-04-client-request-creation-behavior-items.md` | migrated |
| REQ-CMD-APPROVE-001 | CMD | SC-07B | SC-07B | Approve request | `SC-07B-employee-request-review-behavior-items.md` | migrated |
| REQ-CMD-REJECT-001 | CMD | SC-07B | SC-07B | Reject request | `SC-07B-employee-request-review-behavior-items.md` | migrated |
| DOC-CMD-ATTACH-001 | CMD | SC-11 | SC-11 | Attach request document | `SC-11-request-documents-behavior-items.md` | migrated |
| AGR-CMD-EMP-SEND-001 | CMD | SC-13D | SC-13D | Employee sends first agreement proposal | `SC-13D-employee-agreement-proposal-create-response-behavior-items.md` | migrated |
| AGR-CMD-CLIENT-ACCEPT-001 | CMD | SC-13B | SC-13B | Client accepts proposal | `SC-13B-agreement-proposal-details-response-behavior-items.md` | migrated |
| AGR-CMD-CLIENT-SEND-001 | CMD | SC-13B | SC-13B | Client sends own proposal version | `SC-13B-agreement-proposal-details-response-behavior-items.md` | migrated |
| AGR-CMD-EMP-NEW-001 | CMD | SC-13D | SC-13D | Employee sends new version after client proposal | `SC-13D-employee-agreement-proposal-create-response-behavior-items.md` | migrated |
| VER-CMD-START-001 | CMD | SC-14 | SC-14 | Start request-context verification | `SC-14-client-data-verification-behavior-items.md` | migrated |
| ANON-CMD-SUBMIT-001 | CMD | SC-17 | SC-17 | Submit anonymous request/contact | `SC-17-anonymous-request-behavior-items.md` | migrated |
| REQ-LC-001 | LC | SC-04 | SC-04 | Request creation creates InReview | `SC-04-client-request-creation-behavior-items.md` | migrated |
| REQ-LC-002 | LC | SC-07B | SC-07B | InReview request can be approved | `SC-07B-employee-request-review-behavior-items.md` | migrated |
| REQ-LC-003 | LC | SC-07B | SC-07B | InReview request can be rejected | `SC-07B-employee-request-review-behavior-items.md` | migrated |
| REQ-LC-004 | LC | SC-07B | SC-07A / SC-07B | Approved request cannot be reviewed again | `SC-07B-employee-request-review-behavior-items.md` | migrated |
| REQ-LC-005 | LC | SC-07B | SC-07B | Rejected request cannot be approved in core | `SC-07B-employee-request-review-behavior-items.md` | migrated |
| REQ-LC-006 | LC | SC-07B | SC-07B | Rejected request cannot be rejected again in core | `SC-07B-employee-request-review-behavior-items.md` | migrated |
| AGR-LC-001 | LC | SC-13D | SC-13D | Employee first proposal creates awaiting state | `SC-13D-employee-agreement-proposal-create-response-behavior-items.md` | migrated |
| AGR-LC-002 | LC | SC-13B | SC-13B | Awaiting proposal can be accepted | `SC-13B-agreement-proposal-details-response-behavior-items.md` | migrated |
| AGR-LC-003 | LC | SC-13B | SC-13B | Awaiting proposal can receive client version | `SC-13B-agreement-proposal-details-response-behavior-items.md` | migrated |
| AGR-LC-004 | LC | SC-13B | SC-13B | Accepted proposal has no response actions in core | `SC-13B-agreement-proposal-details-response-behavior-items.md` | migrated |
| AGR-LC-005 | LC | SC-13B | SC-13B | Rejected proposal has no response actions in core | `SC-13B-agreement-proposal-details-response-behavior-items.md` | migrated |
| AGR-LC-006 | LC | SC-13B | SC-13B | Client own version can be sent only once in core | `SC-13B-agreement-proposal-details-response-behavior-items.md` | migrated |
| AGR-LC-006B | LC | SC-13B | SC-13B | Duplicate client own proposal is rejected | `SC-13B-agreement-proposal-details-response-behavior-items.md` | migrated |
| AGR-LC-007 | LC | SC-13D | SC-13D | Employee new version supersedes previous client version | `SC-13D-employee-agreement-proposal-create-response-behavior-items.md` | migrated |
| REQ-IBS-001 | IBS | SC-07B | SC-07B | Approved request has review decision | `SC-07B-employee-request-review-behavior-items.md` | migrated |
| REQ-IBS-002 | IBS | SC-07B | SC-07B / SC-05 | Rejected request feedback policy | `SC-07B-employee-request-review-behavior-items.md` | migrated |
| REQ-IBS-003 | IBS | SC-04 | SC-04 | Request has object address | `SC-04-client-request-creation-behavior-items.md` | migrated |
| AGR-IBS-001 | IBS | SC-13D | SC-13A..SC-13D | Proposal has sender | `SC-13D-employee-agreement-proposal-create-response-behavior-items.md` | migrated |
| AGR-IBS-002 | IBS | SC-13D | SC-13B / SC-13D | Proposal has document/file | `SC-13D-employee-agreement-proposal-create-response-behavior-items.md` | migrated |
| AGR-IBS-003 | IBS | SC-13B | SC-13B / SC-13D | Client does not start exchange | `SC-13B-agreement-proposal-details-response-behavior-items.md` | migrated |
| REQ-VI-001 | VI | SC-04 | SC-04-DATA | Object address value integrity | `SC-04-client-request-creation-behavior-items.md` | migrated |
| APPL-VI-001 | VI | SC-10 | SC-10-DATA | Applicant data by applicant type | `SC-10-applicant-data-behavior-items.md` | migrated |
| AGR-VI-001 | VI | SC-13D | SC-13B / SC-13D-DATA | Agreement document reference | `SC-13D-employee-agreement-proposal-create-response-behavior-items.md` | migrated |
| AGR-VI-002 | VI | SC-13D | SC-13B / SC-13D-DATA | Proposal text details/comment | `SC-13D-employee-agreement-proposal-create-response-behavior-items.md` | migrated |
| REQ-UCQ-001 | UCQ | SC-04 | SC-04 / SC-10 | Request uses saved ApplicantParty without mutating it | `SC-04-client-request-creation-behavior-items.md` | migrated |
| AGR-UCQ-001 | UCQ | SC-13D | SC-13D / SC-07B | Proposal creation requires Approved request | `SC-13D-employee-agreement-proposal-create-response-behavior-items.md` | migrated |
| AGR-UCQ-002 | UCQ | SC-07B | SC-07B / SC-13D | Approval enables but does not create proposal | `SC-07B-employee-request-review-behavior-items.md` | migrated |
| VER-UCQ-001 | UCQ | SC-14 | SC-10 / SC-14 | Verification is request-context-only | `SC-14-client-data-verification-behavior-items.md` | migrated |
| REQ-READ-001 | READ | SC-05 | SC-05 / SC-15 | Client sees only own requests | `SC-05-my-requests-own-request-details-behavior-items.md` | migrated |
| AGR-READ-001 | READ | SC-13A | SC-13A / SC-13B / SC-15 | Client sees only own agreements/proposals | `SC-13A-my-agreements-behavior-items.md` | migrated |
| EMP-READ-001 | READ | SC-06 | SC-06 / SC-07A | Employee request dashboard visibility | `SC-06-employee-request-dashboard-behavior-items.md` | migrated |
| AUTH-INT-001 | INT | SC-03A | SC-03A | Password recovery email side effect | `SC-03A-password-recovery-request-behavior-items.md` | migrated |
| FILE-INT-001 | INT | SC-11 | SC-11 / SC-13B / SC-13D | File/blob storage is infrastructure | `SC-11-request-documents-behavior-items.md` | migrated |
| ACC-CMD-LOGIN-001 | CMD | SC-02 | SC-02 / SC-15 | Login with activation requirement | `SC-02-login-behavior-items.md` | migrated |
| ACC-LC-001 | LC | SC-01 | SC-01 | Registration creates Active account in core | `SC-01-guest-registration-behavior-items.md` | migrated |
| ACC-LC-002 | LC | SC-15 | SC-02 / SC-15 | Non-active account cannot use protected functionality | `SC-15-security-text-specification-behavior-items.md` | migrated |
| ACC-LC-003 | LC | SC-15 | SC-01 / SC-15 | Future PendingActivation to Active flow | `SC-15-security-text-specification-behavior-items.md` | migrated |
| ACC-UCQ-001 | UCQ | SC-15 | SC-02 / SC-15 | Protected use cases require active account without polluting aggregates | `SC-15-security-text-specification-behavior-items.md` | migrated |
| ACC-SEC-001 | SEC | SC-15 | SC-02 / SC-15 | Activated account required for protected functionality | `SC-15-security-text-specification-behavior-items.md` | migrated |
| ACC-SQ-001 | SQ | SC-15 | SC-02 / SC-15 | Non-active account login behavior | `SC-15-security-text-specification-behavior-items.md` | migrated |
| ACC-SQ-002 | SQ | SC-15 | SC-02 / SC-15 | Claim-based activation policy refresh | `SC-15-security-text-specification-behavior-items.md` | migrated |
| ACC-SQ-003 | SQ | SC-15 | SC-15 | Activation scope for client vs employee accounts | `SC-15-security-text-specification-behavior-items.md` | migrated |

## 4. Migration Rules

```text
- Preserve item IDs where possible.
- Preserve category style.
- Do not invent behavior.
- If source scenario/DATA is missing, update source artifacts through scenario question loop.
- If an item is cross-scenario, choose one owning scenario and list related scenarios in the per-scenario file.
```

## 5. Known Follow-Up

```text
- Review cross-scenario ownership after first client-sidecar work.
- Resolve open scenario questions before implementing affected behavior.
- Promote stable per-scenario behavior files as primary source for domain/slice/client coverage.
```
