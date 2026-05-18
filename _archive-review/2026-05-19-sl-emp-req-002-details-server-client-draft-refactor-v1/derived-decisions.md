# Derived Decisions

```text
GET /api/employee/requests/{requestId} is the canonical implemented details read endpoint.
EmployeeRequestDetailsDto is current generated/server DTO for details.
Review state is compact string state: NotStarted, StartedByCurrentEmployee, StartedByAnotherEmployee, Approved, Rejected.
Current page composes StartReview, ApproveReview, RejectReview and StartAgreementExchange feature actions, but command mutations remain feature sidecar ownership.
No runtime UI/page-flow changes belong to this archive.
```
