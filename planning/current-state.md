# Current State

## Current phase

Pre-L1 planning cleanup is focused on making the next L1 domain implementation step safe for an implementation agent.

## Current branch

`my-changes`

## Current goal

Bring the old educational/experimental ASP.NET Core + React project to an L1 diploma MVP for processing client requests and simple document workflow for the network company ООО "ZSK".

## Current planning status

Scenario text specifications, DATA files, validation-related files and scenario behavior coverage baselines exist.

The current saved domain draft is:

```text
planning/tables/domain-drafts/domain-draft-01.md
```

The current implementation-readiness guide is:

```text
planning/l1-domain-implementation-cut.md
```

## Current implementation status

The repository already contains an older/current implementation snapshot and a parallel L1 subset.

This file is a repository implementation snapshot.

It is not the target domain model for the next L1 implementation step.

Target domain direction is now captured in:

```text
planning/tables/domain-drafts/domain-draft-01.md
```

Narrow implementation instructions are captured in:

```text
planning/l1-domain-implementation-cut.md
```

## Already present in repository

- ASP.NET Core backend project.
- React/Vite/TypeScript frontend project.
- Domain project.
- .NET test project.
- Playwright E2E folder at repository root.
- Existing old domain classes.
- New parallel L1 domain subset exists under `Domain.EnergyManagement.L1`.
- First parallel L1 application/persistence/API slice exists under `EnergyManagement.Server/L1`.
- Old EF/API flow still exists and remains registered in parallel.
- Structured planning folder exists.

## Current implemented L1 snapshot

Current implemented parallel L1 subset may still contain older target names/statuses:

```text
Account
ClientAccount
ApplicantParty
IndividualApplicantParty
ClientRequest
ConnectionRequest
RequestStatus.Submitted
```

Current implemented aggregate roots:

```text
ClientAccount
IndividualApplicantParty
ConnectionRequest
```

Important:

```text
This is implementation/background snapshot.
It is not the target for the next L1 domain refinement.
```

Target request statuses for the next domain direction are:

```text
InReview
Approved
Rejected
```

`Submitted` is legacy/current implementation state unless explicitly reintroduced with separate business meaning.

## Current implementation-readiness decision

Do not ask an implementation agent to implement the whole domain draft.

Use:

```text
planning/l1-domain-implementation-cut.md
```

First L1 domain implementation should focus on:

```text
Account / ClientAccount activation marker
Account.EnsureActivated
ApplicantParty / IndividualApplicantParty
ApplicantPartyVerificationStatus: Unverified / Verified
ConnectionRequest
RequestStatus: InReview / Approved / Rejected
ReviewDecisionRecord
Approve / Reject
optional RejectionFeedback
domain unit tests
```

Explicitly out of first cut unless requested:

```text
persistence
API
UI
Dapper read models
AgreementProposalExchange
file/blob storage
verification provider
anonymous request
future applicant types
email confirmation flow
password recovery flow
notification sending
```

## Current planning rules for L1 implementation

Use progressive file splitting.

Recommended mini-cut order:

```text
1. Account / ClientAccount activation marker
2. ApplicantParty / IndividualApplicantParty
3. ConnectionRequest / review behavior
4. AgreementProposalExchange later, after core request/refactor is stable
```

For each mini-cut:

```text
implement behavior
add unit tests
get tests green
split/normalize files
run tests again
```

## Explicitly not in first L1 implementation cut

- entrepreneur / legal entity applicant types;
- document uploads;
- PDF generation;
- agreement proposal exchange;
- final agreement refusal;
- contract versioning;
- mock verification;
- request clarification;
- extended search and filters;
- email confirmation;
- password recovery;
- rate limiting;
- account lockout;
- Windows Negotiate;
- anonymous requests;
- SMS;
- internal messages;
- real government integrations;
- electronic signature.

## Current blockers / known issues

- Integration tests may require SQL Server LocalDB/test database.
- Full solution build may be affected by frontend `.esproj` / JavaScript SDK availability in some environments.
- Playwright E2E tests/config may be stale.
- Existing old domain model still uses old class names.
- Current implemented L1 subset may still contain `RequestStatus.Submitted`; target draft uses `InReview`.
- Existing `planning/domain-model.md` is a background/compatibility note, not the current target domain draft.

## Last updated

2026-05-14 - Current state reframed for domain-draft-01 implementation readiness and narrow L1 domain implementation cut.
