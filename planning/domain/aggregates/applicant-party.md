# Domain Aggregate Draft — ApplicantParty

Status: draft / first-pass extraction  
Doc version: v0.1.0  
Scope: reusable applicant/contact/template data owned by a client account

## 1. Purpose

```text
Sources:
  Format/process:
    - planning/domain/aggregate-drafting-workflow.md @ Doc version: v0.1.0
    - planning/domain/aggregate-draft-template.md @ Doc version: v0.1.0
    - planning/domain/domain-modeling-principles.md @ Doc version: v0.1.0
  Content:
    - planning/domain/scenario-to-aggregate-map.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-text-specs/SC-10-applicant-data.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-text-specs/SC-10B-my-applicant-parties.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-text-specs/SC-04-client-request-creation.md @ Doc version: v0.1.0
    - planning/tables/domain-drafts/domain-draft-01.md @ historical/cross-check, version not declared
    - planning/tables/domain-drafts/domain-draft-02.md @ historical/cross-check, version not declared
  Internal dependencies:
    - none
  Not checked:
    - full UI sidecar coverage for applicant party management
    - full source/version/cascade alignment for all applicant/request sources
```

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

```text
Sources:
  Format/process:
    - planning/domain/aggregate-drafting-workflow.md @ Doc version: v0.1.0
    - planning/domain/aggregate-draft-template.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-data/README.md @ Doc version: v0.1.0
  Content:
    - planning/domain/scenario-to-aggregate-map.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-text-specs/SC-10-applicant-data.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-text-specs/SC-10B-my-applicant-parties.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-text-specs/SC-04-client-request-creation.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-behavior-items/SC-10-applicant-data-behavior-items.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-behavior-items/SC-10B-my-applicant-parties-behavior-items.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-behavior-items/SC-04-client-request-creation-behavior-items.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-behavior-items/SC-04-request-creation-behavior-items.md @ Doc version: v0.1.0
    - planning/tables/domain-drafts/domain-draft-01.md @ historical/cross-check, version not declared
    - planning/tables/domain-drafts/domain-draft-02.md @ historical/cross-check, version not declared
  Internal dependencies:
    - none
  Not checked:
    - full UI sidecar coverage for applicant party management
    - full source/version/cascade alignment for all applicant/request sources
```

This section is the aggregate-level reviewed source overview. Section-level `Sources:` blocks below are authoritative for local section work.

Scenario text sources:

```text
planning/diagrams/scenario-text-specs/SC-10-applicant-data.md @ Doc version: v0.1.0
planning/diagrams/scenario-text-specs/SC-10B-my-applicant-parties.md @ Doc version: v0.1.0
planning/diagrams/scenario-text-specs/SC-04-client-request-creation.md @ Doc version: v0.1.0
```

Scenario-specific DATA rule:

```text
Primary scenario-specific DATA belongs in the scenario text spec #DATA section.
Use planning/diagrams/scenario-data/ only for reusable/shared/audited/transitional DATA sidecars.
```

Reusable/shared/audited/transitional DATA sidecars checked or referenced by the prior draft:

```text
planning/diagrams/scenario-data/SC-10-applicant-data.md @ Doc version: v0.1.0
planning/diagrams/scenario-data/SC-10B-my-applicant-parties-data.md @ Doc version: v0.1.0
planning/diagrams/scenario-data/SC-04-request-creation-data.md @ Doc version: v0.1.0
```

Behavior items:

```text
planning/diagrams/scenario-behavior-items/SC-10-applicant-data-behavior-items.md @ Doc version: v0.1.0
planning/diagrams/scenario-behavior-items/SC-10B-my-applicant-parties-behavior-items.md @ Doc version: v0.1.0
planning/diagrams/scenario-behavior-items/SC-04-client-request-creation-behavior-items.md @ Doc version: v0.1.0
planning/diagrams/scenario-behavior-items/SC-04-request-creation-behavior-items.md @ Doc version: v0.1.0
```

Historical domain sources:

```text
planning/tables/domain-drafts/domain-draft-01.md @ historical/cross-check, version not declared
planning/tables/domain-drafts/domain-draft-02.md @ historical/cross-check, version not declared
```

Current implementation sources checked in prior archive/source pass:

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
Current implementation files are not treated as freshly rechecked evidence unless explicitly reviewed in a later implementation-sync pass.
```

## 3. Aggregate Boundary

```text
Sources:
  Format/process:
    - planning/domain/domain-discovery-workflow.md @ Doc version: v0.1.0
    - planning/domain/aggregate-drafting-workflow.md @ Doc version: v0.1.0
    - planning/domain/domain-modeling-principles.md @ Doc version: v0.1.0
  Content:
    - planning/domain/scenario-to-aggregate-map.md @ Doc version: v0.1.0
    - planning/domain/aggregates/account.md @ Doc version: v0.1.0
    - planning/domain/aggregates/connection-request.md @ Doc version: v0.1.0
    - planning/tables/domain-drafts/domain-draft-01.md @ historical/cross-check, version not declared
    - planning/tables/domain-drafts/domain-draft-02.md @ historical/cross-check, version not declared
  Internal dependencies:
    - Purpose
  Not checked:
    - current runtime implementation boundary beyond previously checked archive/source pass
```

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

```text
Sources:
  Format/process:
    - planning/domain/aggregate-drafting-workflow.md @ Doc version: v0.1.0
    - planning/domain/aggregate-draft-template.md @ Doc version: v0.1.0
    - planning/domain/domain-modeling-principles.md @ Doc version: v0.1.0
  Content:
    - planning/domain/scenario-to-aggregate-map.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-text-specs/SC-10-applicant-data.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-text-specs/SC-10B-my-applicant-parties.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-text-specs/SC-04-client-request-creation.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-behavior-items/SC-10-applicant-data-behavior-items.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-behavior-items/SC-10B-my-applicant-parties-behavior-items.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-behavior-items/SC-04-client-request-creation-behavior-items.md @ Doc version: v0.1.0
    - planning/domain/value-objects/applicant-identity.md @ Doc version: v0.1.0
    - planning/domain/value-objects/applicant-contact.md @ Doc version: v0.1.0
  Internal dependencies:
    - Aggregate Boundary
  Not checked:
    - current persistence mapping / EF configuration
```

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

```text
Sources:
  Format/process:
    - planning/domain/aggregate-drafting-workflow.md @ Doc version: v0.1.0
    - planning/domain/aggregate-draft-template.md @ Doc version: v0.1.0
    - planning/domain/domain-modeling-principles.md @ Doc version: v0.1.0
  Content:
    - planning/domain/scenario-to-aggregate-map.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-text-specs/SC-10-applicant-data.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-text-specs/SC-10B-my-applicant-parties.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-text-specs/SC-04-client-request-creation.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-behavior-items/SC-10-applicant-data-behavior-items.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-behavior-items/SC-10B-my-applicant-parties-behavior-items.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-behavior-items/SC-04-client-request-creation-behavior-items.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-behavior-items/SC-04-request-creation-behavior-items.md @ Doc version: v0.1.0
    - planning/domain/value-objects/applicant-identity.md @ Doc version: v0.1.0
    - planning/domain/value-objects/applicant-contact.md @ Doc version: v0.1.0
    - planning/domain/aggregates/account.md @ Doc version: v0.1.0
    - planning/domain/aggregates/connection-request.md @ Doc version: v0.1.0
  Internal dependencies:
    - Aggregate Boundary
    - Owned State
  Not checked:
    - application service orchestration beyond previously checked ApplicantPartyCreationService source pass
    - current implementation code/tests beyond previously checked archive/source pass
```

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

```text
Sources:
  Format/process:
    - planning/domain/aggregate-drafting-workflow.md @ Doc version: v0.1.0
    - planning/domain/domain-modeling-principles.md @ Doc version: v0.1.0
  Content:
    - planning/diagrams/scenario-behavior-items/SC-10-applicant-data-behavior-items.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-behavior-items/SC-10B-my-applicant-parties-behavior-items.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-behavior-items/SC-04-client-request-creation-behavior-items.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-behavior-items/SC-04-request-creation-behavior-items.md @ Doc version: v0.1.0
    - planning/domain/value-objects/applicant-identity.md @ Doc version: v0.1.0
    - planning/domain/value-objects/applicant-contact.md @ Doc version: v0.1.0
    - planning/domain/aggregates/connection-request.md @ Doc version: v0.1.0
    - Domain Methods / Commands
    - Owned State
  Internal dependencies:
    - Aggregate Boundary
    - Owned State
    - Domain Methods / Commands
  Not checked:
    - runtime enforcement / tests unless explicitly reviewed
```

| Invariant | Protected by | Source | Failure/error |
|---|---|---|---|
| Applicant party belongs to one client account. | `ClientAccountId` required at creation. | domain drafts / implementation | client account required |
| Individual applicant party requires full name, contact email and phone. | `IndividualApplicantParty.Create`. | SC-10 / domain drafts / implementation | missing data errors |
| New applicant party starts unverified. | constructor/factory. | domain drafts / implementation | n/a |
| New applicant party should not silently become current/default inside aggregate constructor. | constructor sets current marker false; application service owns first-of-type/default coordination. | domain drafts / implementation | app coordination error if violated |
| At most one current/default applicant party per client/type. | application service + persistence constraint, not one aggregate instance alone. | domain drafts | coordination invariant |
| Verified applicant party edit is blocked for current core. | policy/future method; not fully implemented in this aggregate draft. | domain drafts | open/future decision |

## 7. Lifecycle / State Machine

```text
Sources:
  Format/process:
    - planning/domain/aggregate-drafting-workflow.md @ Doc version: v0.1.0
    - planning/domain/domain-modeling-principles.md @ Doc version: v0.1.0
  Content:
    - planning/diagrams/scenario-text-specs/SC-10-applicant-data.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-text-specs/SC-10B-my-applicant-parties.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-text-specs/SC-04-client-request-creation.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-behavior-items/SC-10-applicant-data-behavior-items.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-behavior-items/SC-10B-my-applicant-parties-behavior-items.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-behavior-items/SC-04-client-request-creation-behavior-items.md @ Doc version: v0.1.0
    - Domain Methods / Commands
    - Invariants
  Internal dependencies:
    - Domain Methods / Commands
    - Invariants
  Not checked:
    - full edit/archive/versioning implementation and tests
```

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

```text
Sources:
  Format/process:
    - planning/domain/aggregate-drafting-workflow.md @ Doc version: v0.1.0
    - planning/domain/domain-modeling-principles.md @ Doc version: v0.1.0
  Content:
    - planning/diagrams/scenario-behavior-items/SC-10-applicant-data-behavior-items.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-behavior-items/SC-10B-my-applicant-parties-behavior-items.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-behavior-items/SC-04-client-request-creation-behavior-items.md @ Doc version: v0.1.0
    - planning/domain/value-objects/applicant-identity.md @ Doc version: v0.1.0
    - planning/domain/value-objects/applicant-contact.md @ Doc version: v0.1.0
    - planning/domain/aggregates/connection-request.md @ Doc version: v0.1.0
    - Invariants
    - Lifecycle / State Machine
    - Domain Methods / Commands
  Internal dependencies:
    - Invariants
    - Lifecycle / State Machine
    - Domain Methods / Commands
  Not checked:
    - current runtime code/tests unless explicitly reviewed
```

| Impossible state | Prevented by | Source |
|---|---|---|
| Individual applicant party without full name. | create validation. | implementation / domain drafts |
| Individual applicant party without contact email. | create validation. | implementation / domain drafts |
| Individual applicant party without phone. | create validation. | implementation / domain drafts |
| Aggregate directly changing request lifecycle. | boundary rule. | domain modeling principles |
| Request aggregate directly verifying ApplicantParty. | application coordination rule. | domain drafts / ConnectionRequest extraction |

## 9. Value Objects Used

```text
Sources:
  Format/process:
    - planning/domain/aggregate-drafting-workflow.md @ Doc version: v0.1.0
    - planning/domain/aggregate-draft-template.md @ Doc version: v0.1.0
    - planning/domain/value-object-drafting-workflow.md @ Doc version: v0.1.0
    - planning/domain/value-object-draft-template.md @ Doc version: v0.1.0
  Content:
    - planning/domain/scenario-to-aggregate-map.md @ Doc version: v0.1.0
    - planning/domain/value-objects/applicant-identity.md @ Doc version: v0.1.0
    - planning/domain/value-objects/applicant-contact.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-behavior-items/SC-10-applicant-data-behavior-items.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-behavior-items/SC-04-client-request-creation-behavior-items.md @ Doc version: v0.1.0
  Internal dependencies:
    - Owned State
    - Domain Methods / Commands
    - Invariants
  Not checked:
    - future EntrepreneurApplicantParty and LegalEntityApplicantParty value object needs
```

| Value object | File | Purpose in this aggregate |
|---|---|---|
| ApplicantIdentity | `planning/domain/value-objects/applicant-identity.md` | Current individual applicant identity data. |
| ApplicantContact | `planning/domain/value-objects/applicant-contact.md` | Applicant contact email/phone, separate from account auth email. |

## 10. Cross-Aggregate Relations

```text
Sources:
  Format/process:
    - planning/domain/domain-discovery-workflow.md @ Doc version: v0.1.0
    - planning/domain/aggregate-drafting-workflow.md @ Doc version: v0.1.0
    - planning/domain/domain-modeling-principles.md @ Doc version: v0.1.0
  Content:
    - planning/domain/scenario-to-aggregate-map.md @ Doc version: v0.1.0
    - planning/domain/aggregates/account.md @ Doc version: v0.1.0
    - planning/domain/aggregates/connection-request.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-text-specs/SC-04-client-request-creation.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-behavior-items/SC-04-client-request-creation-behavior-items.md @ Doc version: v0.1.0
    - Aggregate Boundary
    - Domain Methods / Commands
  Internal dependencies:
    - Aggregate Boundary
    - Domain Methods / Commands
    - Invariants
  Not checked:
    - application service implementation beyond prior ApplicantPartyCreationService source pass
```

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

```text
Sources:
  Format/process:
    - planning/domain/aggregate-drafting-workflow.md @ Doc version: v0.1.0
    - planning/domain/aggregate-draft-template.md @ Doc version: v0.1.0
  Content:
    - planning/diagrams/scenario-behavior-items/SC-10-applicant-data-behavior-items.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-behavior-items/SC-10B-my-applicant-parties-behavior-items.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-behavior-items/SC-04-client-request-creation-behavior-items.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-behavior-items/SC-04-request-creation-behavior-items.md @ Doc version: v0.1.0
    - Domain Methods / Commands
    - Invariants
    - Lifecycle / State Machine
    - Impossible States Prevented
    - Cross-Aggregate Relations
  Internal dependencies:
    - Domain Methods / Commands
    - Invariants
    - Lifecycle / State Machine
    - Impossible States Prevented
  Not checked:
    - UI sidecar behavior not audited in this pass
    - all applicant party tests in current repo state
```

| Source item | Covered by | Status | Notes |
|---|---|---|---|
| Applicant save/create behavior | `IndividualApplicantParty.Create` | covered / first-pass | Future applicant party types deferred. |
| Applicant current/default behavior | application coordination + `MarkAsCurrentDefaultTemplate` / `MarkInactiveVersion` | partial | Single aggregate instance cannot enforce uniqueness alone. |
| Applicant edit behavior | aggregate policy / future methods | partial / deferred | Verified edit is blocked for current core; detailed edit slice not extracted here. |
| Applicant verification through request approval | application coordination + `MarkVerified` | partial | ConnectionRequest does not own verification. |
| Request creation from selected applicant | external reference by `ApplicantPartyId` | covered by relation | Request owns request lifecycle. |

## 12. Persistence / EF Notes

```text
Sources:
  Format/process:
    - planning/domain/aggregate-drafting-workflow.md @ Doc version: v0.1.0
    - planning/domain/domain-modeling-principles.md @ Doc version: v0.1.0
  Content:
    - Owned State
    - Value Objects Used
    - Aggregate Boundary
    - Cross-Aggregate Relations
    - planning/tables/domain-drafts/domain-draft-01.md @ historical/cross-check, version not declared
    - planning/tables/domain-drafts/domain-draft-02.md @ historical/cross-check, version not declared
  Internal dependencies:
    - Aggregate Boundary
    - Owned State
    - Value Objects Used
  Not checked:
    - current EF configuration unless explicitly reviewed
```

```text
ApplicantParty inheritance is current direction.
Only IndividualApplicantParty is current concrete implementation.
Current/default uniqueness likely needs persistence/application enforcement for ClientAccountId + ApplicantPartyType.
```

## 13. Cross-Layer Placement Notes

```text
Sources:
  Format/process:
    - planning/domain/domain-responsibility-map.md @ Doc version: v0.1.0
    - planning/domain/domain-modeling-principles.md @ Doc version: v0.1.0
    - planning/domain/aggregate-drafting-workflow.md @ Doc version: v0.1.0
  Content:
    - Aggregate Boundary
    - Domain Methods / Commands
    - Cross-Aggregate Relations
    - Behavior Coverage
    - planning/domain/scenario-to-aggregate-map.md @ Doc version: v0.1.0
  Internal dependencies:
    - Aggregate Boundary
    - Cross-Aggregate Relations
    - Behavior Coverage
  Not checked:
    - API/client/slice implementation unless explicitly reviewed
```

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

```text
Sources:
  Format/process:
    - planning/domain/aggregate-drafting-workflow.md @ Doc version: v0.1.0
    - planning/domain/aggregate-draft-template.md @ Doc version: v0.1.0
  Content:
    - Source Inputs
    - Behavior Coverage
    - Cross-Layer Placement Notes
    - planning/domain/aggregates/connection-request.md @ Doc version: v0.1.0
    - planning/domain/value-objects/applicant-identity.md @ Doc version: v0.1.0
    - planning/domain/value-objects/applicant-contact.md @ Doc version: v0.1.0
  Internal dependencies:
    - Source Inputs
    - Behavior Coverage
    - Cross-Aggregate Relations
  Not checked:
    - sources needed to resolve open edit/archive/versioning policy questions
```

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
Sources:
  Format/process:
    - planning/domain/aggregate-drafting-workflow.md @ Doc version: v0.1.0
    - planning/domain/aggregate-draft-template.md @ Doc version: v0.1.0
    - planning/source-cascade-sync-workflow.md @ version not confirmed in this batch
  Content:
    - changed sections in this ApplicantParty aggregate draft
    - planning/domain/AGGREGATE-SECTION-SOURCES-TEMPLATE.md @ Doc version: v0.1.0
    - planning/SOURCE-SECTION-SOURCES-TEMPLATE.md @ version not confirmed in this batch
  Internal dependencies:
    - all changed sections in this draft
  Not checked:
    - downstream aggregates/slices not reviewed in this pass
```

```text
- Added `Doc version: v0.1.0`.
- Added local section-level fenced `Sources:` blocks for aggregate draft work.
- Reclassified `## 2. Source Inputs` as overview; section-level `Sources:` blocks are authoritative for local section work.
- Clarified scenario-specific DATA rule: prefer scenario text spec #DATA; use `planning/diagrams/scenario-data/` only for reusable/shared/audited/transitional sidecars.
- No ApplicantParty domain behavior semantics changed in this pass.
```
