# SC-04 — Client Request Creation

Status: current target scenario direction / explicit applicant context model  
Doc version: v0.1.0  
Related scenarios: `SC-10 Applicant Data`, `SC-10B My Applicant Parties`, `SC-05 My Requests`

## 1. Purpose

Signed-in client creates a connection request.

The request uses one applicant context:

```text
Existing:
  an owned saved ApplicantParty selected for this request.

New:
  applicant data entered during request creation;
  server creates ApplicantParty and uses it for this request.
```

## 2. Visual Scenario Flow

```text
Signed-in client opens request creation journey
        ↓
Request creation page shows request fields and applicant section
        ↓
System uses current/default ApplicantParty for selected applicant type as initial prefill/default when available
        ↓
 ┌──────────────────────────────┬──────────────────────────────┐
 │ current/default exists       │ current/default missing      │
 ▼                              ▼
Applicant fields are prefilled  Applicant fields are empty and ready for input
        ↓                              ↓
Client keeps existing/default,   Client enters new applicant data
chooses another saved one,
or clears/enters new data
        ↓                              ↓
 ┌──────────────────────────────┬──────────────────────────────┐
 │ selected saved ApplicantParty│ new applicant data           │
 ▼                              ▼
Request uses selected            New ApplicantParty is created
ApplicantParty                   and used for this request
        ↓                              ↓
Client provides request details + object address
        ↓
Client submits request
        ↓
Accepted request becomes InReview and appears in My Requests / employee review queue
```

## 3. Rules

```text
- Current/default is initial prefill/default selection, not server-side eligibility limit.
- Future UI may allow choosing any owned saved ApplicantParty via dropdown/list/search.
- Existing branch can use current/default or any non-default saved ApplicantParty.
- New branch creates ApplicantParty + request atomically.
- Creating new ApplicantParty does not replace existing ApplicantParties.
- Request command success does not require a response body unless UI scenario changes.
```
