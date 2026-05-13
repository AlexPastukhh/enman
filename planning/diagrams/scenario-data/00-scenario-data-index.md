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
planning/tables/scenario-domain-design-input-core.md
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

Applicant DATA in request creation may be:

```text
- copied/prefilled from previously provided matching applicant DATA;
- entered inline if no matching applicant DATA exists;
- edited for this specific request if client wants different data.
```

Future UX:

```text
[VAR:EXPAND]
- clear prefilled applicant DATA action;
- restore prefilled applicant DATA action.
```

Clearing or editing request-local applicant fields does not delete saved applicant DATA.

## Applicant DATA Summary

Applicant types:

```text
physical person
individual entrepreneur
legal entity
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

Core agreement proposal statuses:

```text
AwaitingClientConfirmation
SentByClient
Accepted
Rejected
```

Core agreement proposal DATA:

```text
- related Approved request;
- sender: employee or client;
- status;
- attached agreement document/file;
- text details/comment;
- visible summary/name.
```

Future statuses / behaviors:

```text
[VAR:EXPAND]
Signed
Expired
Superseded
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

## Domain Planning Use

DATA specs feed:

```text
planning/diagrams/scenario-text-specs/scenario-server-domain-validation-addendum.md
planning/tables/scenario-domain-design-input-core.md
planning/tables/domain-discovery-core.md
```

Do not use DATA files as DB schema or DTO contract.
