# SC-04 — Client Request Creation DATA

Status: current DATA spec / explicit applicant context model

## 1. Request DATA

```text
- request details/description;
- object address;
- request submit availability;
- validation feedback.
```

## 2. Applicant Context DATA

```text
- applicantContextType = Existing | New;
- existingApplicantPartyId, when Existing branch is used;
- new applicant data, when New branch is used;
- current/default ApplicantParty prefill, when available;
- saved ApplicantParties for future picker/list/search;
- clear/reset action for prefilled fields.
```

Existing branch may use any owned saved ApplicantParty.

New branch creates ApplicantParty and uses it for the request.

## 3. Accepted Submit Outcome DATA

```text
- request created;
- request status = InReview;
- request appears in My Requests;
- request appears in employee review queue.
```
