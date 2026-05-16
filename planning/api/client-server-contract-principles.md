# Client / Server Contract Principles

Status: applicant-context model synchronized / server validation boundary added

## ApplicantParty

Standalone create ApplicantParty returns ApplicantPartyId for stable identity/cache/future actions. This is API support, not scenario behavior.

## Request Creation Target

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

## Server Request Validation Boundary

For client/server contract work, distinguish API request-shape validation from application/domain validation.

Request DTO/query validation belongs to the server request validation boundary and should be planned with FluentValidation for new/changed L1 inputs:

```text
- required fields;
- discriminator/branch values;
- mutually exclusive fields;
- nested DTO presence;
- allowed query parameter values;
- simple API DTO shape checks.
```

Application/domain validation remains in handlers/domain:

```text
- account exists and has correct type;
- selected ApplicantParty exists;
- selected ApplicantParty belongs to current account;
- domain value object invariants;
- state transitions;
- no-write/atomicity.
```

For the target request creation DTO above, FluentValidation should cover:

```text
Existing:
- applicantContextType == Existing;
- existingApplicantPartyId is required;
- newApplicantParty is null/absent.

New:
- applicantContextType == New;
- newApplicantParty is required;
- existingApplicantPartyId is null/absent.

Always:
- details required/not blank;
- address required;
- required address fields present.
```

Use:

```text
planning/slices/cross-cutting/CC-VALIDATION-001-server-request-validation-and-fluentvalidation.md
```
