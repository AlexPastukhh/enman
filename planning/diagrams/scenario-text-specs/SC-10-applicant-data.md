# SC-10 — Applicant Data

Status: current target scenario direction / reconciled with applicant-template-per-type model  
Source type: scenario text specification  
Related scenarios: `SC-04 Client Request Creation`, `SC-10B My Applicant Parties`

## 1. Target Model

```text
A client account can store many ApplicantParty profiles over time.

For convenience, the account may have one current/default ApplicantParty template per applicant type:
- physical person / individual;
- individual entrepreneur;
- legal entity.

Current/default template means initial prefill/default selection for future request creation.

Creating a new ApplicantParty does not delete, hide, deactivate or overwrite existing ApplicantParties.

First ApplicantParty of a type may initialize the default/current template for that type.

Additional ApplicantParty of the same type must not silently replace the existing default/current template.

Explicit current/default selection belongs to a separate future behavior/slice.
```

## 2. Core Scenario Flow

```text
Signed-in client opens Account page
        ↓
Account page shows Applicant Parties section
        ↓
Top area shows current/default templates grouped by applicant type, when available
        ↓
Saved list area shows all saved ApplicantParties, including non-default ones
        ↓
Client adds a new individual ApplicantParty by entering applicant data
        ↓
System validates applicant data
        ↓
 ┌──────────────────────────────┬──────────────────────────────┐
 │ accepted                     │ not accepted                 │
 ▼                              ▼
New ApplicantParty appears       User sees validation/error
in saved ApplicantParties        feedback and can correct data
        ↓
Existing ApplicantParties remain visible and unchanged
        ↓
If no current/default exists for the type,
new ApplicantParty becomes initial current/default template
        ↓
If current/default already exists for the type,
existing current/default remains selected and new ApplicantParty is only added to saved list
```

## 3. Scenario Rules

```text
- ApplicantParty creation is additive.
- New ApplicantParty starts NotVerified / Unverified.
- Current/default is a future prefill/default-selection concept, not replacement.
- ApplicantParty details are small enough to show inline in Account page cards/list.
- A separate ApplicantParty details page is not required by current scenario direction.
```

## 4. Relationship To Request Creation

```text
Request creation may use:
- Existing selected ApplicantPartyId;
- New applicant data entered during request creation.

When New applicant data is used, the system creates ApplicantParty and the request in one atomic server operation.
```

## 5. Downstream Use

```text
planning/slices/SL-APPL-001-create-individual-applicant-party.md
planning/slices/SL-APPL-001-create-individual-applicant-party.client.md
planning/slices/SL-APPL-002-account-applicant-parties-read.md
planning/slices/SL-APPL-003-select-current-default-applicant-party-template.md
planning/slices/SL-APPL-004-applicant-party-creation-application-service.md
```
