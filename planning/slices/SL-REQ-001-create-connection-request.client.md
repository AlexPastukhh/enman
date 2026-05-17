# SL-REQ-001.client — Create Connection Request UI with Applicant Context

Status: full client command sidecar draft / backend contract implemented / saved ApplicantParty selector in scope  
Parent slice: `SL-REQ-001 — Create Connection Request With Applicant Context`  
Slice type: client command sidecar  
Architecture direction: command/user-action slice maps to `pages + features + entities`.

## 1. Sidecar Overview

This sidecar defines the client UI for creating a connection request with explicit applicant context.

The backend command already supports:

```text
POST /api/l1/requests
```

with explicit applicant context:

```text
Existing saved ApplicantParty
or
New applicant data entered during the request creation journey
```

This sidecar owns the request creation page/form and the visible command behavior.

It does not own backend behavior, ApplicantParty management, My Requests rendering, generated artifacts or future lifecycle work.

## 2. Scope

This client sidecar owns:

```text
- request creation route/page for signed-in L1 client;
- request details fields;
- object address fields;
- applicant context section;
- Existing applicant context branch;
- saved ApplicantParty selector/list for Existing branch;
- current/default ApplicantParty selected first when available;
- user can choose another owned saved ApplicantParty;
- New applicant context branch;
- new applicant data fields entered during request creation;
- one submit command to POST /api/l1/requests;
- visible accepted/rejected submit outcomes;
- visible validation/error feedback;
- success outcome and handoff to My Requests.
```

## 3. Out of Scope

```text
- backend endpoint implementation -> parent SL-REQ-001 backend;
- server validation rules -> backend / CC-VALIDATION;
- ApplicantParty read page/list implementation -> SL-APPL-002.client;
- standalone add/manage ApplicantParty page behavior -> SL-APPL-001.client and future lifecycle slices;
- explicit make default/current -> SL-APPL-003;
- delete/archive/edit ApplicantParty lifecycle -> future ApplicantParty lifecycle slice;
- search/filter/sort/pagination inside saved ApplicantParty selector;
- direct request details navigation requiring requestId response;
- employee review queue UI;
- My Requests list/details implementation;
- manual OpenAPI/generated artifact edits.
```

## 4. Related Slices / Owners

```text
SL-REQ-001 backend
  owns POST /api/l1/requests and Existing/New applicant context persistence.

SL-APPL-002.client
  owns account ApplicantParties read/query/display foundation.

SL-APPL-001.client
  owns standalone add Individual ApplicantParty behavior outside request creation.

SL-APPL-003
  owns explicit make default/current behavior.

L1-MY-REQUESTS-READ-LIST.client
  owns My Requests list where created request becomes visible.

L1-MY-REQUEST-DETAILS.client
  owns details page after request exists.
```

## 5. Sources / Source Behavior Items

Read scenario source files via:

```text
planning/slices/slice-scenario-flow-behavior-register.md
```

Primary sources:

```text
planning/diagrams/scenario-text-specs/SC-04-client-request-creation.md
planning/diagrams/scenario-ui-specs/SC-04-request-creation-ui.md
planning/diagrams/scenario-behavior-items/SC-04-request-creation-behavior-items.md
planning/slices/SL-REQ-001-create-connection-request.md
planning/api/client-server-contract-principles.md
```

Source behavior items expected for this sidecar:

```text
SC-04-BI-001 — Signed-in client can create a connection request.
SC-04-BI-002 — Client provides request details and object address.
SC-04-BI-003 — Request creation uses one applicant context.
SC-04-BI-004 — Request can use existing owned saved ApplicantParty.
SC-04-BI-005 — Existing ApplicantParty can be current/default or any other owned saved ApplicantParty.
SC-04-BI-006 — Request can use new applicant data entered in journey.
SC-04-BI-007 — New applicant data creates ApplicantParty and uses it for request.
SC-04-BI-008 — New ApplicantParty + ConnectionRequest are one atomic user intent.
SC-04-BI-009 — Creating request with new applicant data does not replace older ApplicantParties.
SC-04-BI-010 — Created request enters InReview.
SC-04-BI-011 — Request appears in My Requests and employee review queue.
SC-04-BI-012 — Invalid request/applicant data produces feedback and no partial write.
```

## 6. Visual UI / Scenario Flow

```text
[Signed-in Client]
opens request creation journey
        ↓
[Request Creation Page]
shows request details fields,
object address fields,
and applicant section
        ↓
[Applicant Section]
current/default ApplicantParty is selected first
when available
        ↓
Client can keep current/default
or choose another saved ApplicantParty
        ↓
 ┌──────────────────────────────┬──────────────────────────────┐
 │ Existing saved ApplicantParty│ New applicant data           │
 ▼                              ▼
Request uses selected            New ApplicantParty is created
owned ApplicantParty             and used for this request
        ↓                              ↓
[Request Fields]
Client provides request details
and object address
        ↓
[Submit]
Client submits request creation form
        ↓
 ┌──────────────────────────────┬──────────────────────────────┐
 │ accepted                     │ rejected                     │
 ▼                              ▼
Client sees success outcome      Client sees validation/error feedback
Created request is InReview      Client can correct input
and appears in My Requests
```

In ordinary words:

A signed-in client opens the request creation page. The page asks for request details, object address and applicant context. In the Existing branch, the current/default ApplicantParty is selected first when available, but the user can choose any other saved owned ApplicantParty from the simple selector. In the New branch, the user enters applicant data inside the request journey. On submit, the client sends one request command. If accepted, the request is created as `InReview` and the user is handed off to My Requests. If rejected, validation/error feedback is shown and the user can correct the form.

Scenario flow table:

| Step | UI / Scenario layer | User-visible responsibility |
|---|---|---|
| S01 | Signed-in client | Opens request creation journey. |
| S02 | Request creation page | Shows request details, object address and applicant section. |
| S03 | Applicant section | Shows Existing/New applicant context choice. |
| S04 | Existing branch | Shows saved ApplicantParty selector. |
| S05 | Current/default selection | Selects current/default first when available. |
| S06 | Other saved choice | Allows choosing another owned saved ApplicantParty. |
| S07 | New branch | Allows entering new applicant data for this request. |
| S08 | Submit | Submits one request creation command. |
| S09 | Accepted outcome | Shows success/handoff; request appears in My Requests as `InReview`. |
| S10 | Rejected outcome | Shows validation/error feedback and allows correction. |

## 7. Visual Client Implementation Flow

```text
[Route / Page Layer]
pages/requests/create/CreateConnectionRequestPage.tsx
or equivalent request creation page

Lives here:
  CreateConnectionRequestPage

Page-level branch logic:
  session exists / no session
  applicant parties loading / error / success
  command success / command error handoff

Page-level composition:
  CreateConnectionRequestForm

Uses:
  useSession()
  useAccountApplicantPartiesQuery({ enabled: Boolean(session) })

Owns:
  route/page composition
  session branch
  passing saved ApplicantParties into command feature
  success navigation or success handoff to My Requests

Does not own:
  request DTO mapping internals
  mutation internals
  low-level fetchJson
  ApplicantParty read query implementation
```

```text
        ↓

[ApplicantParty Entity Read Layer]
entities/applicant-party/model/useAccountApplicantPartiesQuery.ts
entities/applicant-party/model/applicantPartyTypes.ts

Lives here:
  useAccountApplicantPartiesQuery()
  ApplicantPartySummary
  helper to find current/default ApplicantParty if needed

Uses:
  account ApplicantParties read endpoint from SL-APPL-002.client

Owns:
  reading saved ApplicantParties for applicant context UI
  exposing current/default and other saved ApplicantParties to page/feature

Does not own:
  request creation mutation
  request form validation
  submit feedback
```

```text
        ↓

[Command Feature UI Layer]
features/request/create-connection-request/ui/CreateConnectionRequestForm.tsx
features/request/create-connection-request/ui/ApplicantContextSection.tsx
features/request/create-connection-request/ui/ExistingApplicantSelector.tsx
features/request/create-connection-request/ui/NewApplicantFields.tsx
features/request/create-connection-request/ui/RequestDetailsFields.tsx
features/request/create-connection-request/ui/ObjectAddressFields.tsx

Lives here:
  CreateConnectionRequestForm
  ApplicantContextSection
  ExistingApplicantSelector
  NewApplicantFields
  RequestDetailsFields
  ObjectAddressFields
  requestCreationConst
  requestCreation.css

Props:
  applicantParties: ApplicantPartySummary[]
  defaultApplicantParty?: ApplicantPartySummary

Owns:
  visible command form
  Existing/New branch UI
  saved ApplicantParty selector
  current/default marker in selector
  selected ApplicantParty state
  request details fields
  object address fields
  applicant data fields for New branch
  visible submit button
  visible feedback placeholders

Does not own:
  loading ApplicantParties
  low-level HTTP wrapper
  generated contract
  global route definitions
  ApplicantParty management actions
  make-default action
```

```text
        ↓

[Command Feature Model Layer]
features/request/create-connection-request/model/*
features/request/create-connection-request/api/createConnectionRequest.ts

Lives here:
  useCreateConnectionRequestForm()
  createConnectionRequestSchema
  createConnectionRequestFieldNames
  createConnectionRequestServerFieldMap
  CreateConnectionRequestFormValues
  buildCreateConnectionRequestDto()
  createConnectionRequest(values)

Uses:
  useMutation()
  shared/api createConnectionRequest wrapper
  applyApiErrorToForm()
  generated ProblemDetails/error handling conventions

Owns:
  form state
  client-side form schema
  Existing/New branch DTO mapping
  applicantContextType mapping
  selected existingApplicantPartyId mapping
  new applicant data mapping
  request details/address mapping
  mutation submit
  ProblemDetails-to-form error mapping

Does not own:
  page route/session branch
  ApplicantParty read query
  My Requests list rendering
```

```text
        ↓

[Shared API Layer]
shared/api/l1RequestApi.ts
shared/api/l1ApiPaths.ts

Lives here:
  L1CreateConnectionRequestRequest =
    components["schemas"]["L1CreateConnectionRequestDto"]

  createConnectionRequest(request)

Uses:
  fetchJson()
  l1ApiPaths.requests
  generated OpenAPI types

Owns:
  low-level HTTP call:
    POST /api/l1/requests
  generated request type alias

Does not own:
  form state
  Existing/New UI
  React Query mutation
  route navigation
```

```text
        ↓

[Generated Contract Layer]
shared/api/generated/openapi-types.ts

Lives here:
  generated operation for POST /api/l1/requests
  components["schemas"]["L1CreateConnectionRequestDto"]
  components["schemas"]["L1AddressDto"]
  components["schemas"]["L1CreateIndividualApplicantPartyDto"]
  components["schemas"]["ProblemDetails"]

Owns:
  generated structural API contract

Does not own:
  handwritten client logic
  manual edits
  page/UI decisions
```

## 8. Client Implementation Flow

| Step | Layer | Responsibility |
|---|---|---|
| I01 | Route/page | Request creation page renders the create journey for signed-in client. |
| I02 | Page | Page loads saved ApplicantParties for applicant context choices. |
| I03 | ApplicantParty entity read | Entity query provides saved ApplicantParties and current/default candidate. |
| I04 | Command feature UI | Form renders Existing/New applicant context, saved applicant selector, request details and address fields. |
| I05 | Command feature model/API | Feature maps form values into `L1CreateConnectionRequestDto`. |
| I06 | Shared API | Wrapper posts `POST /api/l1/requests`. |
| I07 | Page/feature feedback | Accepted submit shows success/handoff; rejected submit shows visible feedback. |

## 9. Client API / Server Contract

Server endpoint:

```text
POST /api/l1/requests
```

Server request shape:

```ts
type L1CreateConnectionRequestDto = {
  applicantContextType: "Existing" | "New";
  existingApplicantPartyId?: number | null;
  newApplicantParty?: L1CreateIndividualApplicantPartyDto | null;
  details: string;
  address: L1AddressDto;
};
```

Existing branch:

```text
applicantContextType = "Existing"
existingApplicantPartyId = selected saved ApplicantParty id
newApplicantParty = null / omitted according to generated contract and server validation expectations
```

New branch:

```text
applicantContextType = "New"
existingApplicantPartyId = null / omitted
newApplicantParty = entered applicant data
```

Target shared API wrapper:

```ts
export type L1CreateConnectionRequestRequest =
  components["schemas"]["L1CreateConnectionRequestDto"];

export const createConnectionRequest = (
  request: L1CreateConnectionRequestRequest,
): Promise<void> =>
  fetchJson<void>(l1ApiPaths.requests, {
    method: "POST",
    body: JSON.stringify(request),
  });
```

## 10. Questions / Decisions

| ID | Status | Question | Current direction | Impact |
|---|---|---|---|---|
| `Q-SL-REQ-001-CLIENT-001` | accepted | Is this a read or command sidecar? | Command/user action sidecar. Form/mutation lives in `features/request/create-connection-request`. | Architecture placement. |
| `Q-SL-REQ-001-CLIENT-002` | accepted | Does client use implicit current ApplicantParty? | No. Current/default is only initial selection/prefill. Submit uses explicit Existing/New applicant context. | DTO mapping and UI behavior. |
| `Q-SL-REQ-001-CLIENT-003` | assumption | What is first route? | Add request creation route such as `/requests/create` unless route naming is decided otherwise. | Route/page setup. |
| `Q-SL-REQ-001-CLIENT-004` | accepted | Existing branch can use only current/default? | No. Existing branch shows saved ApplicantParty selector and allows any owned saved ApplicantParty returned by account ApplicantParties read model. | Applicant context UI, DTO mapping, E2E. |
| `Q-SL-REQ-001-CLIENT-005` | accepted | Does New branch manage ApplicantParty page/list? | No. New branch only enters applicant data for this request journey; management belongs to ApplicantParty slices. | Scope boundary. |
| `Q-SL-REQ-001-CLIENT-006` | accepted | After success, navigate to details or My Requests? | My Requests handoff/list is preferred because command success has no required requestId body. | Success UX and E2E. |
| `Q-SL-REQ-001-CLIENT-007` | accepted | Does this sidecar implement make-default/current? | No. Future `SL-APPL-003`. | Avoids scope creep. |

## 11. Extension / Change Points

```text
- richer saved ApplicantParty search/filter/sort -> future selector refinement;
- explicit make default/current -> SL-APPL-003;
- standalone ApplicantParty create/manage page -> SL-APPL-001.client / lifecycle slices;
- request draft autosave -> future request creation extension;
- request type selection beyond connection request -> future request type expansion;
- post-create direct details navigation -> revisit only if API returns requestId;
- employee review queue visibility -> employee-side read/review slices.
```

## 12. Behavior Coverage

Behavior Coverage is not Test Coverage.

| Source behavior item | How sidecar covers it | Status |
|---|---|---|
| `SC-04-BI-001` Signed-in client can create a connection request | Adds signed-in request creation page/form. | covered |
| `SC-04-BI-002` Client provides request details and object address | Form includes details and address fields. | covered |
| `SC-04-BI-003` Request creation uses one applicant context | Form submits exactly one Existing/New applicant context branch. | covered |
| `SC-04-BI-004` Request can use existing owned saved ApplicantParty | Existing branch selects saved ApplicantParty. | covered |
| `SC-04-BI-005` Existing ApplicantParty can be current/default or any other owned saved ApplicantParty | Existing selector lists saved ApplicantParties; current/default is initially selected, but user can choose another saved one. | covered |
| `SC-04-BI-006` Request can use new applicant data entered in journey | New branch includes applicant data fields. | covered |
| `SC-04-BI-007` New applicant data creates ApplicantParty and uses it for request | Client sends New branch DTO; backend owns atomic creation. | covered by command contract |
| `SC-04-BI-008` New ApplicantParty + ConnectionRequest are one atomic user intent | Single submit command; no separate client create ApplicantParty call. | covered |
| `SC-04-BI-009` Creating request with new applicant data does not replace older ApplicantParties | Client does not issue ApplicantParty replace/manage command. | respected |
| `SC-04-BI-010` Created request enters InReview | Success handoff verifies request visible in My Requests as `InReview`. | covered |
| `SC-04-BI-011` Request appears in My Requests and employee review queue | My Requests visibility is verified as dependent read behavior; employee queue is separate. | partial/dependent |
| `SC-04-BI-012` Invalid request/applicant data produces feedback and no partial write | Form maps validation/ProblemDetails feedback; backend owns no partial write. | covered as visible feedback |

Not behavior coverage:

```text
- DTO nullable mechanics;
- React Query invalidation;
- service extraction;
- SaveChanges boundary.
```

## 13. Client / Component / E2E Verification Plan

Testing follows existing L1 client sidecar style: component/client tests for visible branches and form behavior, shared API/model tests for contract mapping, E2E for visible user outcomes. E2E should assert visible request creation outcome and My Requests visibility, not internal cache/refetch mechanics.

### Component/client tests

| Test / check | Verifies |
|---|---|
| Request creation page renders for signed-in client | Create journey entry point is visible |
| Signed-out/auth-required branch renders without session | Safe unauthenticated state |
| Details field renders and accepts input | Request details data entry |
| Object address fields render and accept input | Address data entry |
| Applicant context section renders | Applicant context is part of request form |
| Existing branch renders saved ApplicantParty options | Existing owned saved ApplicantParty can be selected |
| Current/default ApplicantParty is initially selected when available | Default/current is initial selection only |
| Existing branch allows selecting non-default saved ApplicantParty | Existing branch is not limited to default/current |
| New branch renders applicant fields | New applicant data can be entered in journey |
| Switching Existing/New updates submitted DTO branch | One applicant context is sent |
| Submit accepted shows success/handoff | Accepted outcome visible |
| Submit rejected maps field/root errors | Validation feedback visible |

### Shared API / feature model tests

| Test / check | Verifies |
|---|---|
| `createConnectionRequest()` posts to `/api/l1/requests` | Shared API uses correct endpoint |
| Existing branch DTO includes `applicantContextType = "Existing"` and selected id | Existing branch mapping |
| Existing branch DTO does not include new applicant data | Mutually exclusive branch mapping |
| New branch DTO includes `applicantContextType = "New"` and applicant data | New branch mapping |
| New branch DTO does not include selected existing id | Mutually exclusive branch mapping |
| Address form values map to `L1AddressDto` | Address contract mapping |
| ProblemDetails maps to visible field/root errors | Rejected submit feedback |

### E2E Existing current/default path

```text
register/login client
        ↓
setup saved current/default ApplicantParty
        ↓
open request creation page
        ↓
keep current/default ApplicantParty selected
        ↓
enter request details and object address
        ↓
submit request
        ↓
assert success/handoff visible
        ↓
open My Requests
        ↓
assert created request is visible with InReview status
```

### E2E Existing non-default saved ApplicantParty path

```text
register/login client
        ↓
setup current/default ApplicantParty
and another saved ApplicantParty
        ↓
open request creation page
        ↓
choose non-default saved ApplicantParty
        ↓
enter request details and object address
        ↓
submit request
        ↓
assert success/handoff visible
        ↓
open My Requests
        ↓
assert created request is visible with InReview status
```

### E2E New applicant path

```text
register/login client
        ↓
open request creation page
        ↓
choose New applicant context
        ↓
enter applicant data
        ↓
enter request details and object address
        ↓
submit request
        ↓
assert success/handoff visible
        ↓
open My Requests
        ↓
assert created request is visible with InReview status
```

### E2E validation path

```text
register/login client
        ↓
open request creation page
        ↓
submit with missing/invalid required data
        ↓
assert validation/error feedback visible
        ↓
correct input
        ↓
submit succeeds
```

Explicit non-goals for tests:

```text
Do not assert backend transaction/SaveChanges internals.
Do not assert React Query invalidation/cache internals.
Do not test make-default/current behavior here.
Do not test ApplicantParty lifecycle management here.
Do not test search/filter/sort in saved ApplicantParty selector.
```

## 14. Suggested File Placement

```text
src/pages/requests/create/
  CreateConnectionRequestPage.tsx
  createConnectionRequestPage.css

src/features/request/create-connection-request/ui/
  CreateConnectionRequestForm.tsx
  ApplicantContextSection.tsx
  ExistingApplicantSelector.tsx
  NewApplicantFields.tsx
  RequestDetailsFields.tsx
  ObjectAddressFields.tsx
  createConnectionRequestConst.ts
  createConnectionRequest.css

src/features/request/create-connection-request/model/
  useCreateConnectionRequestForm.ts
  createConnectionRequestSchema.ts
  createConnectionRequestFieldMap.ts
  buildCreateConnectionRequestDto.ts

src/features/request/create-connection-request/api/
  createConnectionRequest.ts

src/shared/api/
  l1RequestApi.ts

src/shared/config/
  clientRoutes.ts

src/app/router/
  router.tsx

tests/e2e/requests/
  create-connection-request.spec.ts
```

## 15. Dependent / Follow-up Slices

```text
SL-REQ-001 backend -> already owns POST /api/l1/requests.
SL-APPL-002.client -> ApplicantParties read/query/display foundation.
SL-APPL-003 -> future make default/current command.
L1-MY-REQUESTS-READ-LIST.client -> visible created request in My Requests.
L1-MY-REQUEST-DETAILS.client -> future direct details handoff if requestId response appears.
```

## 16. Implementation Checklist

```text
[ ] Add request creation route helper, likely /requests/create.
[ ] Add route in router.
[ ] Add shared API wrapper createConnectionRequest().
[ ] Add feature API adapter for create command.
[ ] Add form schema / field names / server field map.
[ ] Add Existing/New applicant context UI.
[ ] Load saved ApplicantParties through entity read query.
[ ] Initial-select current/default ApplicantParty when available.
[ ] Allow selecting non-default saved ApplicantParty.
[ ] Map Existing branch DTO.
[ ] Map New branch DTO.
[ ] Map request details and address DTO.
[ ] Add submit mutation and error feedback.
[ ] Add success/handoff behavior.
[ ] Add component/client tests.
[ ] Add shared API / feature model tests.
[ ] Add E2E Existing current/default path.
[ ] Add E2E Existing non-default path.
[ ] Add E2E New path.
[ ] Add E2E validation path.
```
