# Shared Support Note — Applicant Data Prefill

Status: planning note  
Marker: `[SHARED SUPPORT][CLIENT/UI][CROSS-SLICE]`

## Purpose

Capture notes about pre-filling applicant data in client forms before concrete Client/UI sidecar planning.

## Current Rule

When request creation needs applicant data in the form:

```text
- client may prefill fields from selected/saved ApplicantParty;
- user may edit request-local fields if the slice allows it;
- editing request-local fields must not mutate saved ApplicantParty;
- saving ApplicantParty changes is a separate slice/action.
```

## Future Sidecar Usage

In request creation `.client.md`, specify:

```text
- which ApplicantParty data is shown;
- whether fields are read-only, editable, or copied into request-local input;
- how user sees that saved ApplicantParty will not be changed;
- which client tests verify prefill behavior.
```
