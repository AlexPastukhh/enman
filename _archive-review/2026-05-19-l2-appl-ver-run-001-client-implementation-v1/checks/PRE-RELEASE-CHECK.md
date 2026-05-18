# PRE-RELEASE CHECK — L2-APPL-VER-RUN-001 client implementation

## Scope check

PASS — archive contains only client implementation/test files plus `_archive-review`.

No files included under:

```text
Domain.EnergyManagement/
EnergyManagement.Server/
Shared/openapi.json
energymanagement.client/src/shared/api/generated/openapi-types.ts
planning/
```

## Behavior check

PASS — client verification command sends only `requestId` in the route:

```text
POST /api/employee/requests/{requestId}/applicant-party/verification/run
```

PASS — API wrapper sends no request body.

PASS — dashboard/details use `applicantVerification` from generated read DTOs.

PASS — no client inference from `applicantPartyId`, display name, or unrelated fields.

PASS — `ApproveReview`, `RejectReview`, `StartReview` behavior is not changed.

PASS — no optimistic Verified state; mutation invalidates details and employee request reads.

## Tests included

Focused tests are included for:

```text
runApplicantPartyVerification API wrapper
useRunApplicantPartyVerificationMutation invalidation
RunApplicantPartyVerificationButton
ApplicantPartyVerificationPanel
ApplicantVerificationStatusBadge
EmployeeRequestDashboardList applicant verification rendering/no-guess behavior
EmployeeRequestDetailsView renderApplicantVerification slot
```

## Not executed here

Node dependencies are not available in this sandbox, so tests/build were not executed here.
Run after applying:

```powershell
npm.cmd --prefix .\energymanagement.client run test -- --run
npm.cmd --prefix .\energymanagement.client run build
```

## Manual archive integrity check

PASS — archive built with no wrapper folder.
PASS — original snapshots are included for existing modified files under `_archive-review/.../original-files`.
