# Scenario DATA Index

Status: draft per-scenario DATA catalog

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

Those belong to scenario text specs, security specs or later testing maps.

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
SC-14-client-data-verification-data.md
```

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
a concrete agreement document/version sent by one side to another side in the context of an approved request.
```

Core statuses:

```text
AwaitingClientConfirmation
SentByClient
Accepted
```

Future statuses:

```text
Signed
Rejected
Expired
Superseded
Cancelled
```

## Deferred / Removed

```text
SC-08 and SC-09 are merged.
SC-12 is merged into SC-05 + SC-04.
SC-16 is removed.
SC-18 is deferred.
```
