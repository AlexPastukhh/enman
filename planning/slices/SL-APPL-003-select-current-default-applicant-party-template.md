# SL-APPL-003 — Select Current/Default ApplicantParty Template

Status: planned future slice / explicit default-selection behavior  
Package: `[L1]`  
Source scenario: `SC-10 Applicant Data`, `SC-10B My Applicant Parties`  
Slice type: backend/API command slice with Account page client sidecar

## 1. Slice Overview

Purpose:

```text
Allow the client to explicitly choose one saved ApplicantParty as current/default template for its applicant type.
```

This is separate from create ApplicantParty.

Rules:

```text
- creating first ApplicantParty of a type may initialize default;
- creating another ApplicantParty of same type does not silently replace default;
- changing default when one exists requires this explicit action;
- default affects future prefill/default selection only;
- existing requests remain unchanged.
```

## 2. Visual Scenario Flow

```text
[Client]
Views saved ApplicantParties
        ↓
[Client]
Chooses one ApplicantParty
        ↓
[System]
Checks it belongs to current account
        ↓
[System]
Sets it as current/default for its applicant type
        ↓
[System]
Unsets previous default for that type
        ↓
[Client UI]
Shows updated default marker
```

## 3. Visual Implementation Flow

```text
[API Controller]
POST /api/l1/applicant-parties/{applicantPartyId}/set-default
        ↓
[Application Handler]
Derive account id from auth context
Load ApplicantParty by id + account scope
        ↓
[Domain/Application Policy]
Set as default for applicant type
Unset previous default of same type
        ↓
[Persistence]
Save changes
        ↓
[API]
204 NoContent or updated state
```

## 4. Questions / Decisions

| ID | Status | Question | Current direction | Impact |
|---|---|---|---|---|
| SL-APPL-DEFAULT-Q-001 | accepted direction | Is default global or per type? | Per applicant type. | domain/API policy |
| SL-APPL-DEFAULT-Q-002 | accepted direction | Does default change affect existing requests? | No. Existing requests keep submitted applicant context. | history correctness |
| SL-APPL-DEFAULT-Q-003 | future review | Should API return updated state or 204? | Prefer 204 + client refetch unless UI needs immediate state. | API/client cache |
| SL-APPL-DEFAULT-Q-004 | future review | Can default be unset without replacement? | Decide later. | request creation empty/default state |

## 5. Behavior Coverage

| Behavior item | How draft covers it | Status |
|---|---|---|
| SC-10-BI-005 — default per type | Command selects default for the selected item's type. | covered as draft |
| SC-10-BI-008 — explicit future behavior | This slice owns explicit change. | covered as draft |
| SC-10B-BI-004 — select current/default | User chooses a saved ApplicantParty as default. | covered as draft |
| Existing requests unchanged | Flow states default affects future prefill only. | covered as draft |

## 6. Test / Verification Plan

| Test / check | Verifies | Layer | Status |
|---|---|---|---|
| Requires authentication | Guest cannot change default. | API integration | planned |
| Cannot set another account's ApplicantParty | Ownership enforced. | API integration/security | planned |
| Set default updates selected item | Selected item becomes default for its type. | API/domain integration | planned |
| Previous default of same type is unset | One default per type. | API/domain integration | planned |
| Different type default remains unchanged | Default is per type, not global. | API/domain integration | planned |
| Existing requests unchanged | Default change only affects future prefill. | integration/domain | planned |
