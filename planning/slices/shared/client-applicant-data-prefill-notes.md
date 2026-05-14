# Shared Support Note — Applicant Data Prefill

Status: planning note  
Marker: `[SHARED SUPPORT][CLIENT/UI][CROSS-SLICE]`

## Purpose

Capture notes about pre-filling applicant data in client forms before concrete Client/UI slice planning.

## Why This Is Not A Slice Yet

Applicant-data prefill may become part of a concrete Client/UI slice such as:

```text
SL-REQ-UI-001 — Request creation Client/UI
```

By itself, the prefill helper is not independent business behavior.

## Current Rule

When request creation needs applicant data in the form:

```text
- client may prefill fields from selected/saved ApplicantParty;
- user may edit request-local fields if the slice allows it;
- editing request-local fields must not mutate saved ApplicantParty;
- saving ApplicantParty changes is a separate slice/action.
```

## Related Domain Decision

```text
Request creation uses ApplicantParty as source/reference.
Changing request-local fields does not update saved ApplicantParty.
```

## Future Slice Usage

In `SL-REQ-UI-001`, specify:

```text
- which ApplicantParty data is shown;
- whether fields are read-only, editable, or copied into request-local input;
- how user sees that saved ApplicantParty will not be changed;
- which client tests verify prefill behavior.
```
