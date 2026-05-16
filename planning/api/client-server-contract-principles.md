# Client / Server Contract Principles

Status: applicant-context model synchronized

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
