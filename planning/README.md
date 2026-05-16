# Planning Index

Status: current / applicant-template-per-type synchronized / server validation principles added

## Current Applicant/Request Target Direction

```text
ApplicantParty:
- many saved ApplicantParties over time;
- one current/default template per applicant type;
- default is prefill/default selection only;
- first of type may initialize default;
- additional same-type create does not switch default;
- create is additive, not replacement.

Request creation:
- explicit applicant context: Existing ApplicantPartyId or New applicant data;
- Existing can use any owned saved ApplicantParty;
- New creates ApplicantParty + ConnectionRequest atomically;
- command success has no required body for first cut.
```

## Current Server Validation Direction

```text
FluentValidation exists in the project and is used by legacy/manual controller flow.
L1 request DTO FluentValidation is not a consistent implemented layer yet.
Future/changed L1 API slices should plan request-level FluentValidation explicitly.
```

Use:

```text
planning/slices/cross-cutting/CC-VALIDATION-001-server-request-validation-and-fluentvalidation.md
```

Split validation responsibilities:

```text
FluentValidation:
- request DTO/query shape;
- required fields;
- branch/discriminator rules;
- mutually exclusive fields;
- allowed query values.

Application/domain:
- account existence;
- ownership;
- selected entity belongs to current account;
- domain invariants;
- state transitions;
- no-write/atomicity.
```

Read slice docs through:

```text
planning/slices/README.md
planning/slices/slice-scenario-flow-behavior-register.md
```
