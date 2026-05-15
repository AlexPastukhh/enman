# Scenario DATA Index

Status: current per-scenario DATA catalog  
Scope: scenario DATA specs used by corrected scenario text specifications

This folder contains scenario DATA specs.

DATA means only what actor:

```text
- enters;
- sees;
- selects;
- filters/searches by;
- attaches/uploads;
- references as visible/selectable business item.
```

DATA files must not contain:

```text
- validation/rules sections;
- testable behavior sections;
- invariants;
- preconditions;
- branches;
- access rules;
- security policy.
```

Those belong to:

```text
planning/scenario-domain-validation-principles.md
planning/diagrams/scenario-text-specs/scenario-server-domain-validation-addendum.md
planning/tables/pre-domain-variants-input.md
```

## DATA Types

```text
Input DATA       = what actor enters.
Visible DATA     = what actor sees.
Selection DATA   = what actor selects from list/set.
Filter DATA      = what actor filters/searches by.
Attachment DATA  = what actor uploads/attaches/sends as file/document.
Reference DATA   = visible/selectable relation to another business item, only when useful.
```

Do not use:

```text
Decision DATA
Response DATA
Policy DATA
```

## Active DATA Files

```text
SC-01-registration-data.md
SC-02-login-data.md
SC-03A-password-recovery-data.md
SC-03B-account-owner-verified-data.md
SC-04-request-creation-data.md
SC-05-my-requests-data.md
SC-06-employee-dashboard-data.md
SC-07A-employee-request-details-data.md
SC-07B-employee-request-review-data.md
SC-10-applicant-data.md
SC-13A-my-agreements-data.md
SC-13B-agreement-proposal-details-response-data.md
SC-13C-employee-agreements-data.md
SC-13D-employee-agreement-proposal-create-response-data.md
SC-14-client-data-verification-data.md
```

## Request Creation DATA Summary

Request object location means:

```text
object address
```

Request creation input DATA includes:

```text
- requested service / request subject information;
- request details/description;
- object address.
```

Applicant DATA in request creation is Reference / Visible DATA:

```text
- current active ApplicantParty summary for the account;
- applicant type;
- applicant display name;
- applicant contact summary.
```

Applicant data changes are handled through SC-10 Applicant Data / future replacement flow.

Future UX:

```text
[VAR:EXPAND]
- inline shortcut to update applicant data before submit;
- return-to-request flow after applicant data update;
- clear/restore rejected-request prefill data when rejected-request retry is implemented.
```

## Applicant DATA Summary

Applicant types:

```text
physical person
individual entrepreneur
legal entity
```

Current active ApplicantParty policy:

```text
One L1 account has one current active ApplicantParty at a time.
Applicant types are alternative data shapes for the account-level applicant profile,
not separate simultaneously-active applicant contexts per type.
```

Current implemented L1 already has narrow physical-person applicant data:

```text
FullName
Email
PhoneNumber
```

Target scenario DATA also includes richer physical-person data needed for agreement realism:

```text
СНИЛС
паспортные данные
actual/residential address as target/future expansion
```

## Agreement Proposal DATA Summary

Agreement Proposal means:

```text
a concrete agreement document/version sent by one side to another side in the context of an Approved request.
```

Diagram-safe agreement proposal status terms:

```text
AwaitingClientConfirmation
SentByClient
SupersededByCounterProposal
Accepted
Rejected
```

Status meaning:

```text
Rejected
= explicit rejection/decline of a proposal.

SupersededByCounterProposal
= proposal is no longer current because another party sent a replacing counterproposal.
```

Core agreement proposal DATA:

```text
- related Approved request;
- sender: employee or client;
- status / lifecycle term;
- attached agreement document/file;
- text details/comment;
- visible summary/name.
```

Replacement / counterproposal rule:

```text
A previous client-sent proposal replaced by an employee counterproposal is superseded/replaced by counterproposal.

It must not be modeled as ordinary Rejected unless there is an explicit rejection/decline action.
```

Future statuses / behaviors:

```text
[VAR:EXPAND]
Signed
Expired
Cancelled
comment-only discussion
return to older proposal version
close/reject approved request if agreement cannot be reached
```

## Deferred / Removed

```text
SC-08 and SC-09 are merged.
SC-12 is merged into SC-05 + SC-04.
SC-16 is removed.
SC-18 is deferred.
```

## Domain Variant Use

DATA specs feed:

```text
planning/diagrams/scenario-text-specs/scenario-server-domain-validation-addendum.md
planning/tables/pre-domain-variants-input.md
domain model variants
diagram-generation Phase 1 preflight and agreement batch generation
```

Do not use DATA files as DB schema or DTO contract.
