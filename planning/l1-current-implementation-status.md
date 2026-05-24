# L1 Current Implementation Status

> Historical/internal status note.
> This file is not implementation truth and may be stale.
> Do not use it as proof that a feature is implemented.
> For current implementation state, inspect the current branch, code, tests, migrations, generated API contracts and runtime screenshots.
> For VKR/thesis wording, use `planning/vkr-clean-reference.md`.

Status: current implementation inventory / L1 server and client status reconciled  
Scope: current L1 runtime state, remaining L1 implementation gaps and next-step guidance

## 1. Summary

Current L1 has moved beyond the older planning wording.

Treat these as implemented/current unless a later repo review proves otherwise:

```text
Backend:
- auth register/login/current-user/logout;
- create individual ApplicantParty;
- account ApplicantParties flat-list read;
- explicit make ApplicantParty current/default command;
- legacy/narrow current-individual read still present;
- My Requests list with status filter;
- My Request details;
- create connection request with Existing/New applicant context.

Client:
- register/login/current-user/logout flows;
- My Requests list/filter/details;
- request creation route and form;
- Existing/New applicant context UI;
- account ApplicantParties read used by request creation.
```

## 2. Implemented Backend L1

| Area | Current runtime status | Notes |
|---|---|---|
| Auth register | implemented | `POST /api/l1/auth/register` |
| Auth login | implemented | `POST /api/l1/auth/login`, cookie sign-in |
| Current user | implemented | `GET /api/l1/auth/current-user` |
| Logout | implemented | `POST /api/l1/auth/logout` |
| Create Individual ApplicantParty | implemented | first account+type initializes current/default through creation service |
| Account ApplicantParties read | implemented | `GET /api/l1/applicant-parties`, flat `applicantParties[]`, `isCurrentDefault` mapped from persisted marker |
| Make ApplicantParty current/default | implemented | `POST /api/l1/applicant-parties/{applicantPartyId}/make-current-default` |
| Current individual ApplicantParty | implemented compatibility/narrow endpoint | old `GET /api/l1/applicant-parties/current-individual`; not the target page model |
| Create connection request | implemented | `POST /api/l1/requests`, Existing/New applicant context |
| My Requests list | implemented | `GET /api/l1/requests`, status filter supported |
| My Request details | implemented | `GET /api/l1/requests/{requestId}` |

## 3. Implemented Client L1

| Area | Current client status | Notes |
|---|---|---|
| Register/login/current-user/logout | implemented | existing auth sidecars remain current |
| My Requests list/filter/details | implemented | filters/details are not merely planned anymore |
| Create request route | implemented | `/requests/create` route exists |
| Create request page/form | implemented | loads account ApplicantParties and submits request creation |
| Existing applicant branch | implemented | uses saved ApplicantParty selection and current/default as initial selection |
| New applicant branch | implemented | builds request DTO with `newApplicantParty` |
| Account ApplicantParties read foundation | implemented enough for request creation | target page replacement remains separate gap |

## 4. Remaining L1 Gaps Before New Domain Draft

These are the practical L1 cleanup/finish items before moving fully into a new domain draft:

```text
1. SL-APPL-003.client:
   add client make default/current button/action and shared API wrapper/path if still missing.

2. Applicant Parties target page/section:
   replace old AccountPage single-current/current-individual UI with flat account ApplicantParties page/section.

3. Tests:
   confirm or add coverage for make-current-default command:
   ownership rejection, same-type switching, idempotency, no request mutation.

4. Compatibility cleanup decision:
   decide later what to do with old current-individual endpoint after target page replacement.
```

Future but not current L1 finish work:

```text
- ApplicantParty delete/archive/edit lifecycle;
- LegalEntity / IndividualEntrepreneur ApplicantParty create flows;
- larger new domain scenario/draft work.
```

## 5. Next-Step Recommendation

Best immediate L1 work:

```text
SL-APPL-003.client — make default/current client action
```

Then:

```text
Applicant Parties page/section replacement for old AccountPage single-current flow
```

After that, L1 is reasonably ready to move toward the next domain draft.
