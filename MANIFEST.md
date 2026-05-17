# MANIFEST — L1 Current Status Documentation Sync

Archive: `l1-current-status-docs-sync.zip`  
Scope: documentation-only status reconciliation after current L1 implementation review  
Repo: `AlexPastukhh/enman`  
Branch baseline checked: `my-changes`

## Why this archive exists

The current repo implementation advanced beyond the latest planning status wording.
Several docs still describe implemented L1 work as planned/draft/future.

This package synchronizes planning docs to the current state:

```text
Implemented backend/current:
- GET /api/l1/applicant-parties
- POST /api/l1/applicant-parties/{applicantPartyId}/make-current-default
- POST /api/l1/requests Existing/New applicant context
- My Requests list/filter/details

Implemented client/current:
- /requests/create route
- CreateConnectionRequestPage
- Existing/New request creation form
- account ApplicantParties read used by request creation
- My Requests list/filter/details
```

Remaining L1 gaps are also explicit:

```text
- SL-APPL-003.client make default/current button/action;
- replacement of old AccountPage single-current applicant UI with the target Applicant Parties page/section;
- test coverage confirmation/additions for make-current-default;
- ApplicantParty delete/archive/edit lifecycle future;
- multi-type ApplicantParty creation future.
```

## Add

| File | Why |
|---|---|
| `planning/l1-current-implementation-status.md` | Single current inventory of implemented/not-implemented L1 server/client state and next-step guidance. |

## Replace

| File | Why |
|---|---|
| `planning/README.md` | Central navigation/current snapshot must say SL-APPL-002/003 backend and SL-REQ-001.client are now implemented. |
| `planning/planning-workflow-current.md` | Current workflow baseline was stale for ApplicantParty read/default and request creation UI. |
| `planning/client/README.md` | Request creation UI is implemented; remaining client gaps need updating. |
| `planning/slices/README.md` | Active slice index/status needs current implemented/planned distinction. |
| `planning/slices/SL-APPL-002-account-applicant-parties-read.md` | Backend read endpoint is implemented, not unconfirmed/planned. |
| `planning/slices/SL-APPL-003-select-current-default-applicant-party-template.md` | Backend make-current/default command is implemented, not planned. |
| `planning/slices/SL-REQ-001-create-connection-request.client.md` | Client request creation sidecar is implemented, not just draft/handoff. |
| `planning/slices/slice-questions-register.md` | Status questions updated: implemented vs remaining client gaps. |
| `planning/slices/slice-extension-points-register.md` | Restored full register shape and keeps future work separate from current implementation. |
| `planning/slices/slice-implementation-notes-register.md` | Notes updated to current implementation and remaining gaps. |

## Delete

None.

## Non-goals

```text
- no runtime/backend/client code;
- no tests;
- no generated artifacts;
- no GitHub writes/commit/branch/PR;
- no new domain slice draft.
```
