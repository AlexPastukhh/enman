# Client / Server Contract Principles

Status: ApplicantParty account list, applicant-context and My Requests contract synchronized

## 1. Core Rule

Client code must not guess server contract.

Use:

```text
Shared/openapi.json
energymanagement.client/src/shared/api/generated/openapi-types.ts
Shared/constants.json
Shared/errorcodes.json
```

OpenAPI is the structural contract. Generated constants are the semantic contract.

## 2. Current L1 Endpoint Baseline

Current L1 target endpoints include:

```text
POST /api/l1/auth/register
POST /api/l1/auth/login
GET  /api/l1/auth/current-user
POST /api/l1/auth/logout
POST /api/l1/applicant-parties/individual
GET  /api/l1/applicant-parties/current-individual
GET  /api/l1/applicant-parties
POST /api/l1/requests
GET  /api/l1/requests
GET  /api/l1/requests/{requestId}
```

When current implementation differs from target planning, the relevant slice must say so explicitly.

`GET /api/l1/applicant-parties/current-individual` is old/narrow current implementation support. New Applicant Parties page work should target `GET /api/l1/applicant-parties`.

## 3. ApplicantParty

Standalone create ApplicantParty returns `ApplicantPartyId` for stable identity/cache/future actions.

This is API support, not scenario behavior.

## 4. Applicant Parties Account List Contract

Target read endpoint for the one Applicant Parties page / section:

```text
GET /api/l1/applicant-parties
```

Response:

```ts
type L1AccountApplicantPartiesResponse = {
  applicantParties: L1ApplicantPartySummaryDto[];
};
```

Summary DTO direction:

```ts
type L1ApplicantPartySummaryDto = {
  applicantPartyId: number;
  applicantPartyType: "Individual" | "IndividualEntrepreneur" | "LegalEntity";
  displayName: string;
  fullName?: L1FullNameDto | null;
  email?: string | null;
  phoneNumber?: string | null;
  verificationStatus: string;
  isCurrentDefault: boolean;
  createdAt?: string | null;
};
```

Rules:

```text
- client does not submit accountId/clientAccountId;
- endpoint returns all owned ApplicantParties in one flat list;
- endpoint does not split currentDefaults and other saved parties in the API response;
- client groups current/default vs other saved parties by isCurrentDefault;
- empty account returns 200 with applicantParties = [];
- response does not expose clientAccountId;
- isCurrentDefault is the API-facing target name;
- current implementation marker IsCurrentActiveVersion may be used behind the API until naming cleanup happens;
- no 422 request-shape validation is expected because this read endpoint has no body/query input.
```

Primary slice:

```text
planning/slices/SL-APPL-002-account-applicant-parties-read.md
```

## 5. Request Creation Target

Target direction for future request creation with applicant context:

```ts
type L1CreateConnectionRequestDto = {
  applicantContextType: "Existing" | "New";
  existingApplicantPartyId?: number | null;
  newApplicantParty?: L1CreateIndividualApplicantPartyDto | null;
  details: string;
  address: L1AddressDto;
};
```

Rules:

```text
- explicit branch marker;
- Existing verifies selected ApplicantParty belongs to current account;
- Existing can use current/default or any owned saved ApplicantParty;
- New creates ApplicantParty + request atomically;
- no required response body for initial command success unless UI needs it.
```

Server request validation must follow:

```text
planning/slices/cross-cutting/CC-VALIDATION-001-server-request-validation-and-fluentvalidation.md
```

## 6. My Requests List Contract

Current backend:

```text
GET /api/l1/requests
GET /api/l1/requests?status=InReview|Approved|Rejected
```

Response:

```text
L1MyRequestSummaryDto[]
```

Client direction:

```text
Status is the first supported filter in an extensible My Requests filter model.
The page owns URL query params.
The filter feature owns controls and parse/serialize helpers.
The entity query accepts a filter object.
The shared API maps supported filters to query string.
```

## 7. Own Request Details Contract

Current backend:

```text
GET /api/l1/requests/{requestId}
```

Response:

```text
L1MyRequestDetailsDto
```

Rules:

```text
- client does not submit accountId;
- missing and not-owned requests both map to 404;
- InReview can have reviewResult = null;
- Rejected includes rejection reason when available;
- client details sidecar owns UI/not-found behavior.
```

## 8. ProblemDetails And Validation

Server validation errors use API DTO field names, not React form field names.

Client sidecars map DTO field names to form fields when needed.

Request-level FluentValidation should be separated from application/domain validation in slice drafts.
