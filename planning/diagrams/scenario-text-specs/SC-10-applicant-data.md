# SC-10 — Applicant Parties Page / Applicant Data

Status: current target scenario direction / one Applicant Parties page model  
Doc version: v0.1.0  
Source type: scenario text specification  
Related scenarios: `SC-04 Client Request Creation`, `SC-10B Applicant Parties Future Management`

## 1. Target Model

```text
A client account can store many ApplicantParty profiles over time.

The current planning model is one Applicant Parties page / section.

Do not split current user scenario direction into:
- Account page applicant section;
- separate My Applicant Parties page.

The same Applicant Parties page / section shows:
- current/default ApplicantParty templates;
- other saved ApplicantParties;
- add ApplicantParty action/form;
- future explicit make default/current action.
```

ApplicantParty default/current direction:

```text
For convenience, the account may have one current/default ApplicantParty template per applicant type:
- physical person / individual;
- individual entrepreneur;
- legal entity.

Current/default template means initial prefill/default selection for future request creation.

Creating a new ApplicantParty does not delete, hide, deactivate or overwrite existing ApplicantParties.

First ApplicantParty of a type may initialize the default/current template for that type.

Additional ApplicantParty of the same type must not silently replace the existing default/current template.

Explicit current/default selection belongs to a separate future behavior/slice on the same Applicant Parties page.
```

Existing requests rule:

```text
Existing requests are historical/current request records.

Creating ApplicantParty or changing default/current ApplicantParty must not rewrite, relink or otherwise change existing requests.
```

## 2. Desired Visual Page Model

```text
Applicant Parties Page
│
├─ Default/current templates
│   └─ outlined/highlighted cards
│
├─ Other saved ApplicantParties
│   └─ regular cards
│
└─ Actions
    ├─ Add ApplicantParty
    ├─ Make default/current
    └─ Future delete/archive lifecycle
```

`Future delete/archive lifecycle` is an extension point only. It is not current L1 behavior.

## 3. Core Scenario Flow

```text
Signed-in client opens Applicant Parties page / section
        ↓
Top area shows current/default templates by applicant type, when available
        ↓
Current/default templates are outlined/highlighted
        ↓
Other saved ApplicantParties are shown below as regular cards/list items
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
Existing requests remain unchanged
        ↓
If no current/default exists for the type,
new ApplicantParty may become initial current/default template
        ↓
If current/default already exists for the type,
existing current/default remains selected and new ApplicantParty is only added as another saved ApplicantParty
```

## 4. Scenario Rules

```text
- ApplicantParty management is represented as one Applicant Parties page / section.
- ApplicantParty creation is additive.
- New ApplicantParty starts NotVerified / Unverified.
- Current/default is a prefill/default-selection concept, not replacement.
- Current/default template cards are visually highlighted/outlined.
- Other saved ApplicantParties are shown separately from default/current templates.
- A separate ApplicantParty details page is not required by current scenario direction.
- Explicit make default/current is future behavior on the same page.
- Existing requests are not changed by ApplicantParty creation or default/current changes.
```

## 5. Relationship To Request Creation

```text
Request creation may use:
- Existing selected ApplicantPartyId;
- New applicant data entered during request creation.

Existing can use any owned saved ApplicantParty.

Default/current ApplicantParty is only an initial prefill/default-selection convenience.

When New applicant data is used, the system creates ApplicantParty and the request in one atomic server operation.

Existing requests are not updated if the default/current template later changes.
```

## 6. Downstream Use

```text
planning/slices/SL-APPL-001-create-individual-applicant-party.md
planning/slices/SL-APPL-001-create-individual-applicant-party.client.md
planning/slices/SL-APPL-002-account-applicant-parties-read.md
planning/slices/SL-APPL-003-select-current-default-applicant-party-template.md
planning/slices/SL-APPL-004-applicant-party-creation-application-service.md
planning/slices/slice-extension-points-register.md
```
