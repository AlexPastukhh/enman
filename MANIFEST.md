# MANIFEST — SL-APPL-003.client Full Draft Sync

Archive: `sl-appl-003-client-full-draft-sync.zip`  
Scope: documentation-only replacement package for full `SL-APPL-003.client` sidecar and minimal navigation/register synchronization.

## Add

| File | Why |
|---|---|
| `planning/slices/SL-APPL-003-select-current-default-applicant-party-template.client.md` | Adds full client command sidecar for same-page make current/default action. |

## Replace

| File | Why |
|---|---|
| `planning/client/README.md` | Adds `SL-APPL-003.client` to client navigation and remaining L1 client work. |
| `planning/slices/README.md` | Adds `SL-APPL-003.client` to active client sidecars and clarifies remaining L1 client gaps. |
| `planning/slices/slice-scenario-flow-behavior-register.md` | Adds source mapping for `SL-APPL-003.client`. |
| `planning/slices/slice-questions-register.md` | Adds client-side make-current/default questions/decisions. |
| `planning/slices/slice-extension-points-register.md` | Adds make-current/default client extension points while keeping lifecycle future. |
| `planning/slices/slice-implementation-notes-register.md` | Adds implementation notes for feature/action placement, read UI action slot and generated API workflow. |

## Delete

None.

## Current repo assumptions used

- Backend `SL-APPL-003` command endpoint is implemented as `POST /api/l1/applicant-parties/{applicantPartyId}/make-current-default`.
- Backend handler loads selected owned ApplicantParty, unsets same-type previous current/default parties, marks selected current/default and saves changes.
- Client shared API paths currently need make-current/default wrapper/path wiring if not already added in generated/client code.
- Existing ApplicantParty read/display UI remains read-focused and should not own command mutation logic.
- Command/user action belongs under `features/applicant-party/make-current-default`.
- Entity cards/lists may expose an optional action slot/render prop for command actions.

## Non-goals

```text
- no runtime code changes;
- no tests;
- no generated artifacts;
- no GitHub write/branch/commit/PR;
- no delete/archive/edit lifecycle behavior;
- no manual OpenAPI/generated type edits.
```
