# L1-APPLICANT-PARTY-CREATE-INDIVIDUAL.client — Short Draft

Status: implementation started
Slice type: client sidecar / client command slice
Scope: Account page UI for creating and displaying current individual applicant party
Backend command contract: `POST /api/l1/applicant-parties/individual`
Related read slice: `L1-APPLICANT-PARTY-READ-CURRENT`
Behavior item source: readable working behavior items are used for now; final scenario/UI IDs can be attached later.

## 1. Visual UI Flow

```text
[Authenticated Client]
Opens Account page
        ↓
[Client UI]
Shows editable applicant form
        ↓
[Client]
Fills full name, email, phone
        ↓
[Client]
Submits applicant data
        ↓
[System]
Creates applicant party for authenticated account
        ↓
[Client UI]
Shows applicant data as filled read-only fields
        ↓
[Client UI]
Shows Edit action
        ↓
[Client UI]
Shows self-dismissing success notification
```

This command-sidecar does not introduce a create request entry.

## 2. Visual Client Implementation Flow

```text
[Page: pages/account]
Composes Account page
        ↓
[Feature UI]
Create applicant form
        ↓
[Feature Model]
Schema + form state + DTO mapping
        ↓
[Shared API]
POST /api/l1/applicant-parties/individual
        ↓
[Feature UI]
Switches to read-only filled state
Shows success notification
```

Stable Account page state after refresh requires the future `L1-APPLICANT-PARTY-READ-CURRENT` slice.

## 3. Questions / Decisions

### Q-APPL-CLIENT-001 — Where does the form live?

Status: decided.

Decision: for now, the form lives on Account page.

Working assumption: Account page is the current place where the client manages applicant party data.

### Q-APPL-CLIENT-002 — What happens after successful creation?

Status: decided.

Decision: fields stay filled, become read-only, Edit button appears, and self-dismissing success notification is shown.

Working assumption: the first implementation may switch to read-only state using submitted form values after successful HTTP response.

### Q-APPL-CLIENT-003 — Should client use returned applicantPartyId?

Status: decided.

Decision: no.

Working assumption: HTTP success is enough for this UI step. The client does not store `applicantPartyId` and does not pass it into create request.

### Q-APPL-CLIENT-004 — How does Account page know whether applicant party exists?

Status: open, with target direction.

Working assumption: add a separate read slice `L1-APPLICANT-PARTY-READ-CURRENT` with a current-account endpoint such as `GET /api/l1/applicant-parties/current-individual`.

### Q-APPL-CLIENT-005 — How much validation should client do?

Status: decided.

Decision: client validates obvious field shape and requiredness for good UX. Server remains source of truth.

### Q-APPL-CLIENT-006 — Should applicant party creation refetch session?

Status: decided.

Decision: no session refetch is required. Applicant party state belongs to applicant party read model, not session.

### Q-APPL-CLIENT-007 — Should UI include verified/unverified status?

Status: future convention.

Decision direction: future Account page should display applicant party verification status when the read model exposes it.

### Q-APPL-CLIENT-008 — Should create request entry be visible before applicant party exists?

Status: decided.

Decision: no. This slice does not introduce create request entry.

## 4. Behavior Coverage

Behavior Coverage is not Test Coverage. This table explains how the draft implementation covers the intended behavior.

| Behavior item | How draft covers it | Status |
|---|---|---|
| Client provides applicant data on Account page | Account page hosts applicant party form for authenticated client | covered |
| Client submits full name | Form contains first name, middle name, last name and maps them into `fullName` DTO | covered |
| Client submits contact data | Form contains email and phone number | covered |
| Applicant party is created for authenticated account | Client sends applicant data only; server derives account from auth context | covered |
| Client sees saved applicant data | After success, fields stay filled and become read-only | covered |
| Client can edit applicant data later | Draft shows Edit action after saved/read-only state | partially covered; edit behavior is a future slice |
| Success notification is shown | Feature shows self-dismissing success notification | covered |
| Validation errors are visible | Client validation handles obvious cases; ProblemDetails maps server errors to field/root errors | covered |
| Account page can load applicant state | Future read slice is identified | partially covered; read slice required |

## 5. Testing / Verification Plan

Component/client tests should cover field rendering, obvious validation, DTO mapping, ProblemDetails mapping, success read-only state, Edit action visibility, and success notification behavior.

E2E checks the cross-layer happy path:

```text
authenticated browser session
        ↓
open Account page
        ↓
submit applicant party form
        ↓
wait for POST /api/l1/applicant-parties/individual
        ↓
assert response ok
        ↓
assert saved applicant data and success outcome are visible
```

Detailed validation stays in component/client tests.

## 6. Scenario Flow

```text
Authenticated client opens Account page
        ↓
Account page shows editable applicant form
        ↓
Client fills full name, email, and phone number
        ↓
Client submits applicant data
        ↓
System creates applicant party for authenticated account
        ↓
Applicant data stays visible and becomes read-only
        ↓
Edit action appears
        ↓
Self-dismissing success notification is shown
```

## 7. Behavior Items

### Client opens Account page

The authenticated client opens Account page.

### Client sees applicant form

The client sees an editable applicant party form.

### Client submits full name

The client submits first name, middle name, and last name.

### Client submits contact data

The client submits email and phone number.

### Applicant party is created for authenticated account

The system creates applicant party for the authenticated account.

The client does not submit account identity.

### Client sees saved applicant data

After successful creation, applicant data remains visible and becomes read-only.

### Client can start editing saved applicant data

After applicant data is saved, the UI shows an Edit action.

The actual edit behavior belongs to a future slice.

### Client sees success notification

After successful creation, the UI shows a self-dismissing success notification.

## 8. Next Step

Implement `L1-APPLICANT-PARTY-READ-CURRENT` so Account page can fetch and display current active applicant party after refresh, including verification status when available.
