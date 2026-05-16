# Planning Index

Status: current / applicant-template-per-type synchronized

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

Read slice docs through:

```text
planning/slices/README.md
planning/slices/slice-scenario-flow-behavior-register.md
```
