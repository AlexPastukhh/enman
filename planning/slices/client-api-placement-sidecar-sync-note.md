# Client API Placement Sidecar Sync Note

Status: current / compatibility note for older client sidecars  
Scope: existing `.client.md` files that still mention business-specific `shared/api` wrappers

## 1. Current Interpretation

When an older sidecar says:

```text
[Shared API Layer]
shared/api/<businessArea>Api.ts
  owns low-level HTTP call
  owns generated DTO alias
```

interpret it now as:

```text
[Entity API Layer] for reads
entities/<entity>/api/<readOperation>.ts
  owns read endpoint path, fetchJson call and generated DTO alias

[Feature API Layer] for commands
features/<business-action>/api/<commandOperation>.ts
  owns command endpoint path, unsafe fetchJson call and generated DTO/result alias

[Shared Transport / Generated Layer]
shared/api/fetchJson.ts
shared/api/ProblemDetails helpers
shared/api/antiforgery helpers
shared/api/generated/openapi-types.ts
  owns only transport/security/generated infrastructure
```

## 2. Affected Examples

Read examples:

```text
SL-APPL-002.client
  listAccountApplicantParties should live under entities/applicant-party/api.

L2-EMP-DASH-001.client
  listEmployeeDashboardRequests should live under entities/employee-request/api.

L2-EMP-DETAILS-001.client
  getEmployeeRequestDetails should live under entities/employee-request/api.
```

Command examples:

```text
SL-APPL-001.client
  createIndividualApplicantParty should live under features/applicant-party/create-individual/api.

SL-APPL-003.client
  makeApplicantPartyCurrentDefault should live under features/applicant-party/make-current-default/api.

SL-REQ-001.client
  createConnectionRequest should live under features/request/create-connection-request/api.

L2-REVIEW-START-001.client
  startRequestReview should live under features/employee-request/start-review/api.
```

## 3. Migration Rule

Do not rewrite runtime code only for this note.

Apply this placement when:

```text
- implementing a new client slice;
- touching the specific API wrapper for concrete work;
- cleaning up an existing wrapper under an explicit cleanup task.
```
