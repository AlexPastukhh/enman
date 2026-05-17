# SL-APPL-003 — Select Current/Default ApplicantParty Template

Status: implemented backend/API command slice / client action still pending  
Package: `[L1]`  
Slice type: backend/API command slice  
Source scenario: `SC-10 Applicant Parties Page`, `SC-10B Applicant Parties Future Management`  
Current implementation status: backend endpoint implemented as `POST /api/l1/applicant-parties/{applicantPartyId}/make-current-default`.

## 1. Slice Overview

This slice owns explicit current/default switching for saved ApplicantParties.

Implemented server behavior:

```text
Client explicitly selects one saved ApplicantParty as current/default template for its ApplicantPartyType.
Server verifies selected ApplicantParty belongs to current account.
Server unsets previous same-type current/default ApplicantParty if different.
Server marks selected ApplicantParty as current/default.
Other ApplicantPartyTypes remain unchanged.
Existing requests remain unchanged.
```

This is not first-of-type initialization during create. First-of-type initialization is handled by:

```text
SL-APPL-001 — Create Individual ApplicantParty
ApplicantPartyCreationService
```

## 2. Implemented API

```text
POST /api/l1/applicant-parties/{applicantPartyId}/make-current-default
```

Request body:

```text
none
```

Success:

```text
200 OK with no required response body
```

## 3. Scope

Implemented backend scope:

```text
- authenticate current L1 client account;
- derive current account id from auth/session claims;
- verify selected ApplicantParty belongs to current account;
- determine selected ApplicantPartyType from selected ApplicantParty;
- affect only ApplicantParties of selected ApplicantPartyType;
- unset previous same-type current/default ApplicantParty if different;
- mark selected ApplicantParty as current/default;
- idempotent success when selected is already current/default;
- keep other ApplicantPartyType defaults unchanged;
- keep existing requests unchanged;
- SaveChanges once;
- return command success.
```

## 4. Out of Scope

| Out-of-scope item | Owner / destination |
|---|---|
| First-of-type default/current initialization | Already `SL-APPL-001` / `ApplicantPartyCreationService` |
| Account-level ApplicantParty read model | `SL-APPL-002` |
| Client button/UI/action | Future `SL-APPL-003.client` |
| Request creation applicant picker | `SL-REQ-001.client` implemented/current |
| Delete/archive lifecycle | Future ApplicantParty lifecycle slice |
| Edit/version lifecycle | Future ApplicantParty edit/version slice |
| Renaming `IsCurrentActiveVersion` | Cleanup/default-template naming task |

## 5. Scenario Flow

```text
Client opens Applicant Parties page / section
        ↓
Client sees current/default templates highlighted
        ↓
Client sees other saved ApplicantParties below
        ↓
Client explicitly chooses make default/current on one saved ApplicantParty
        ↓
System verifies ownership
        ↓
System marks selected ApplicantParty as current/default for its type
        ↓
Previous same-type current/default ApplicantParty is unset
        ↓
Existing requests remain unchanged
```

## 6. Backend Implementation Flow

```text
[HTTP]
POST /api/l1/applicant-parties/{applicantPartyId}/make-current-default
        ↓
[Authorize]
authenticated L1 client account required
        ↓
[Controller]
parse route applicantPartyId
derive current account id
        ↓
[Command]
L1MakeApplicantPartyCurrentDefaultCommand(accountId, applicantPartyId)
        ↓
[Handler]
load selected ApplicantParty owned by current account
        ↓
[List]
load account ApplicantParties
        ↓
[State change]
for same type, mark other current/default parties inactive
if selected is not current/default, mark selected current/default
        ↓
[Persistence]
SaveChanges
        ↓
[Response]
200 OK command success
```

## 7. Behavior Coverage

| Source behavior item | Current coverage |
|---|---|
| One current/default ApplicantParty template per applicant type | same-type previous defaults are unset and selected is marked current/default |
| Additional same-type create does not switch implicitly | switching happens only through explicit endpoint |
| Explicit current/default selection same-page behavior | backend command supports future/current client action |
| Existing requests unchanged | command only updates ApplicantParty marker |

## 8. Test / Verification Plan

Primary proof should be API/integration tests with DB state assertions.

Required coverage:

```text
- unauthenticated request returns 401;
- missing selected ApplicantParty is rejected and state unchanged;
- not-owned selected ApplicantParty is rejected and state unchanged;
- owned non-default ApplicantParty becomes current/default;
- previous same-type current/default becomes non-default;
- other type current/default remains unchanged when supported by fixtures;
- selecting already current/default is safe/idempotent;
- existing requests remain unchanged;
- create of second same-type party still does not switch default implicitly.
```

What not to test as behavior proof:

```text
- repository method call order;
- handler private implementation details;
- generated route constant existence;
- React Query invalidation.
```

## 9. Remaining Work

Backend slice is implemented.

Remaining adjacent work:

```text
- SL-APPL-003.client make default/current action/button;
- shared API wrapper/path if still missing;
- visible UI refresh/grouping on Applicant Parties page;
- test coverage confirmation/addition if not already present.
```
