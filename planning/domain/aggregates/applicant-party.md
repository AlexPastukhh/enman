# Domain Aggregate Draft — ApplicantParty

Status: draft / first-pass extraction  
Scope: reusable applicant/contact/template data owned by a client account

## 1. Purpose

`ApplicantParty` owns reusable applicant data that a client can maintain and use when creating requests.

It covers:

```text
- applicant party identity/contact data;
- applicant party type;
- verification status;
- current/default template marker per client/type;
- edit/verification boundary;
- request creation reference by ApplicantPartyId.
```

It does not own request lifecycle, request review, agreement proposal exchange or account authentication.

## 2. Source Inputs

Scenario sources:

```text
planning/diagrams/scenario-text-specs/SC-10-applicant-data.md
planning/diagrams/scenario-text-specs/SC-10B-my-applicant-parties.md
planning/diagrams/scenario-text-specs/SC-04-client-request-creation.md
planning/diagrams/scenario-data/SC-10-applicant-data.md
planning/diagrams/scenario-data/SC-10B-my-applicant-parties-data.md
planning/diagrams/scenario-data/SC-04-request-creation-data.md
```

Behavior items:

```text
planning/diagrams/scenario-behavior-items/SC-10-applicant-data-behavior-items.md
planning/diagrams/scenario-behavior-items/SC-10B-my-applicant-parties-behavior-items.md
planning/diagrams/scenario-behavior-items/SC-04-client-request-creation-behavior-items.md
planning/diagrams/scenario-behavior-items/SC-04-request-creation-behavior-items.md
```

Historical domain sources:

```text
planning/tables/domain-drafts/domain-draft-01.md
planning/tables/domain-drafts/domain-draft-02.md
```

Current implementation sources checked in archive:

```text
Domain.EnergyManagement/Applicants/ApplicantParty.cs
Domain.EnergyManagement/Applicants/IndividualApplicantParty.cs
Domain.EnergyManagement/Applicants/ApplicantPartyType.cs
Domain.EnergyManagement/Applicants/ApplicantPartyVerificationStatus.cs
EnergyManagement.Server/L1/Application/Services/ApplicantPartyCreationService.cs
Domain.EnergyManagement/Requests/ConnectionRequest.cs
```

Not checked:

```text
Full UI sidecar coverage for applicant party management was not audited in this pass.
Full source/version/cascade alignment is still deferred.
```

## 3. Aggregate Boundary

Aggregate root:

```text
ApplicantParty
```

Concrete current subtype:

```text
IndividualApplicantParty
```

Future/possible subtypes:

```text
EntrepreneurApplicantParty
LegalEntityApplicantParty
```

Value objects used:

```text
ApplicantIdentity
ApplicantContact
```

Not part of this aggregate:

```text
ClientAccount authentication/email/password lifecycle
ConnectionRequest lifecycle
RequestReview
AgreementProposalExchange
request applicant snapshot data, if introduced later
```

External aggregate references:

```text
ClientAccountId
```

## 4. Owned State

Root state:

```text
ClientAccountId
ApplicantPartyType
VerificationStatus
Email
PhoneNumber
IsCurrentActiveVersion / current-default template marker
CreatedAt
```

Individual subtype state:

```text
FullName
```

Derived/read-only state:

```text
GetDisplayName()
HasMinimumDataForVerification()
```

Not stored here:

```text
request status;
request review result;
auth account email/password hash;
agreement proposal data.
```

## 5. Domain Methods / Commands

### `CreateIndividualApplicantParty`

Purpose:
- Create a reusable individual applicant party for a client account.

Input:
- `ClientAccountId`
- `FullName`
- applicant contact email
- applicant phone number
- `createdAt`

Preconditions:
- client account id is positive;
- full name is present;
- contact email is present and valid;
- phone number is present and valid.

State changes:
- creates `IndividualApplicantParty`;
- starts as `Unverified`;
- starts as not current/default in the aggregate constructor/factory.

Domain errors:
- client account missing;
- full name missing;
- email missing/invalid;
- phone missing/invalid.

Source behavior:
- Applicant data scenarios and historical domain draft decisions.

### `MarkAsCurrentDefaultTemplate`

Purpose:
- Mark this applicant party as the current/default template for its client/type.

Preconditions:
- aggregate instance exists.

State changes:
- sets current/default marker on this instance.

Application coordination:
- application service must deactivate any previous current/default applicant party for the same `ClientAccountId + ApplicantPartyType`.

### `MarkInactiveVersion`

Purpose:
- Remove current/default marker from this applicant party.

State changes:
- sets current/default marker to false.

### `MarkVerified`

Purpose:
- Mark applicant party as verified after a source-backed verification flow.

Preconditions:
- minimum data for verification is present.

State changes:
- sets `VerificationStatus = Verified`.

Application coordination:
- request approval may verify the applicant party in current implementation;
- `ConnectionRequest` must not directly mutate `ApplicantParty`.

## 6. Invariants

| Invariant | Protected by | Source | Failure/error |
|---|---|---|---|
| Applicant party belongs to one client account. | `ClientAccountId` required at creation. | domain drafts / implementation | client account required |
| Individual applicant party requires full name, contact email and phone. | `IndividualApplicantParty.Create`. | SC-10 / domain drafts / implementation | missing data errors |
| New applicant party starts unverified. | constructor/factory. | domain drafts / implementation | n/a |
| New applicant party should not silently become current/default inside aggregate constructor. | constructor sets current marker false; application service owns first-of-type/default coordination. | domain drafts / implementation | app coordination error if violated |
| At most one current/default applicant party per client/type. | application service + persistence constraint, not one aggregate instance alone. | domain drafts | coordination invariant |
| Verified applicant party edit is blocked for current core. | policy/future method; not fully implemented in this aggregate draft. | domain drafts | open/future decision |

## 7. Lifecycle / State Machine

Verification states:

```text
Unverified -> Verified
Verified -> Verified is idempotent when minimum data remains valid
```

Current/default marker:

```text
not current/default -> current/default
current/default -> not current/default
```

Allowed transitions:

```text
Create -> Unverified + not current/default
Unverified -> Verified
not current/default -> current/default through application coordination
current/default -> not current/default when another same-type applicant becomes current/default
```

Forbidden / deferred transitions:

```text
Verified -> Unverified is not part of current core.
Verified in-place edit is blocked for now.
Delete/archive policy remains future review.
```

## 8. Impossible States Prevented

| Impossible state | Prevented by | Source |
|---|---|---|
| Individual applicant party without full name. | create validation. | implementation / domain drafts |
| Individual applicant party without contact email. | create validation. | implementation / domain drafts |
| Individual applicant party without phone. | create validation. | implementation / domain drafts |
| Aggregate directly changing request lifecycle. | boundary rule. | domain modeling principles |
| Request aggregate directly verifying ApplicantParty. | application coordination rule. | domain drafts / ConnectionRequest extraction |

## 9. Value Objects Used

| Value object | File | Purpose in this aggregate |
|---|---|---|
| ApplicantIdentity | `planning/domain/value-objects/applicant-identity.md` | Current individual applicant identity data. |
| ApplicantContact | `planning/domain/value-objects/applicant-contact.md` | Applicant contact email/phone, separate from account auth email. |

## 10. Cross-Aggregate Relations

References to other aggregates:

```text
ClientAccount by ClientAccountId.
ConnectionRequest by ApplicantPartyId reference from request side.
```

Rules not owned here:

```text
Request creation lifecycle.
Request approval lifecycle.
Agreement exchange lifecycle.
Auth/login/account activation lifecycle.
```

Application coordination needed:

```text
- first-of-type current/default selection;
- switching current/default applicant party;
- creating request from selected/current ApplicantParty;
- marking ApplicantParty verified after request approval.
```

## 11. Behavior Coverage

| Source item | Covered by | Status | Notes |
|---|---|---|---|
| Applicant save/create behavior | `IndividualApplicantParty.Create` | covered / first-pass | Future applicant party types deferred. |
| Applicant current/default behavior | application coordination + `MarkAsCurrentDefaultTemplate` / `MarkInactiveVersion` | partial | Single aggregate instance cannot enforce uniqueness alone. |
| Applicant edit behavior | aggregate policy / future methods | partial / deferred | Verified edit is blocked for current core; detailed edit slice not extracted here. |
| Applicant verification through request approval | application coordination + `MarkVerified` | partial | ConnectionRequest does not own verification. |
| Request creation from selected applicant | external reference by `ApplicantPartyId` | covered by relation | Request owns request lifecycle. |

## 12. Persistence / EF Notes

```text
ApplicantParty inheritance is current direction.
Only IndividualApplicantParty is current concrete implementation.
Current/default uniqueness likely needs persistence/application enforcement for ClientAccountId + ApplicantPartyType.
```

## 13. Cross-Layer Placement Notes

Application layer:
- owns first-of-type default/current selection;
- owns request creation transaction when creating/selecting ApplicantParty and creating ConnectionRequest;
- owns request approval -> ApplicantParty verification coordination.

API:
- owns DTO validation and endpoint authorization.

Client:
- owns applicant selection/edit UI and current/default display.

Testing:
- aggregate tests should cover create, verification and current marker methods;
- integration tests should cover coordination with request creation/approval.

## 14. Questions / Decisions

Open:
- Should verified applicant data edits create a new applicant party version later?
- Should request store immutable applicant snapshot data, or only ApplicantPartyId?
- What is the final delete/archive policy for request-referenced/current/verified applicant parties?
- When do EntrepreneurApplicantParty and LegalEntityApplicantParty become current?

Accepted:
- Applicant data is represented as ApplicantParty, not separate ApplicantData aggregate.
- ApplicantParty is separate from ConnectionRequest.
- ApplicantParty contact email is not account auth email.
- Request stores ApplicantPartyId and does not relink when current/default applicant changes.

Deferred:
- Full edit/archive/versioning model.
- Full source/version/cascade metadata alignment.

## 15. Source Delta / Change Log

```text
- Extracted as first-pass aggregate draft from old monolithic domain drafts, applicant scenarios, request creation behavior and current ApplicantParty implementation.
```
