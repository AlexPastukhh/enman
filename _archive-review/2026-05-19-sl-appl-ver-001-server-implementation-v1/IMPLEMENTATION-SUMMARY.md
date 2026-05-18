# SL-APPL-VER-001 Server Implementation Summary

Scope: server/backend implementation only.

Implemented endpoint:

```http
POST /api/employee/requests/{requestId}/applicant-party/verification/run
```

Implemented behavior:

```text
- Employee-only command endpoint with CSRF protection.
- Controller resolves EmployeeId from current session claims.
- Handler loads Employee, then request by requestId.
- Handler applies the current temporary employee visibility policy.
- Handler resolves ApplicantParty through request.ApplicantPartyId.
- Deterministic mock service returns Passed.
- Handler calls ApplicantParty.MarkVerified().
- ApplicantParty.VerificationStatus is persisted as Verified.
- Request status/review state are not mutated.
- AgreementProposalExchange is not created.
- Success returns 200 OK with RunApplicantPartyVerificationResponseDto.
```

Out of scope and not included:

```text
- Domain model changes.
- Failed / Unavailable verification states.
- Verification history.
- ApproveReview / RejectReview changes.
- Client UI changes.
- Generated OpenAPI/type updates.
- Planning/docs changes outside this archive review note.
```
