# Scenario DATA Index

Status: current per-scenario DATA catalog / ApplicantParty template-per-type model synchronized  
Scope: scenario DATA specs used by corrected scenario text specifications

DATA means only what actor:

```text
- enters;
- sees;
- selects;
- filters/searches by;
- attaches/uploads;
- references as visible/selectable business item.
```

DATA files must not contain validation rules, invariants, access rules or implementation mechanics.

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
SC-10B-my-applicant-parties-data.md
SC-13A-my-agreements-data.md
SC-13B-agreement-proposal-details-response-data.md
SC-13C-employee-agreements-data.md
SC-13D-employee-agreement-proposal-create-response-data.md
SC-14-client-data-verification-data.md
```

## Request Creation DATA Summary

Request creation input DATA includes:

```text
- requested service / request subject information;
- request details/description;
- object address;
- applicant context data: existing default/selected ApplicantParty or new applicant data.
```

Applicant DATA in request creation:

```text
- current/default ApplicantParty for selected type, when available;
- prefilled applicant fields;
- empty applicant fields when no default exists;
- clear/reset action for prefilled fields;
- new applicant data entered after clearing or when default is missing;
- future saved ApplicantParty dropdown/list.
```

## Applicant DATA Summary

Applicant types:

```text
physical person
individual entrepreneur
legal entity
```

Target ApplicantParty model:

```text
A client account may store many ApplicantParties.
One current/default template may exist per applicant type.
Creating new ApplicantParty does not replace existing ApplicantParties.
Creating the first ApplicantParty of a type may initialize the default for that type.
Creating another same-type ApplicantParty leaves existing default unchanged.
```

Current implemented L1 still has narrower physical-person data:

```text
FullName
Email
PhoneNumber
```

Target scenario DATA may later include richer fields:

```text
СНИЛС
паспортные данные
actual/residential address
entrepreneur/legal-entity fields
```

## Deferred / Removed

```text
SC-08 and SC-09 are merged.
SC-12 is merged into SC-05 + SC-04.
SC-16 is removed.
SC-18 is deferred.
```

## Domain Variant Use

DATA specs feed scenario behavior items, domain model variants, slice planning and diagram-generation preflight.

Do not use DATA files as DB schema or DTO contract.
