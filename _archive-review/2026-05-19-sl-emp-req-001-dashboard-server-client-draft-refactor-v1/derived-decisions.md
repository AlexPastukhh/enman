# Derived Decisions

## Confirmed from implementation evidence

```text
GET /api/employee/requests is the current server list endpoint.
status and reviewState are the current query filters.
EmployeeRequestListQueryDtoValidator validates query enum values.
EmployeeRequestListHandler uses Dapper/read projection.
ReviewState values are NotStarted, StartedByCurrentEmployee, StartedByAnotherEmployee, Approved, Rejected.
Client dashboard API wrapper lives in entities/employee-request/api.
EmployeeDashboardPage hosts StartReviewButton for NotStarted/InReview rows only.
StartReview command remains owned by separate command sidecar.
```

## Conservative wording

```text
The current endpoint evidence uses Employee role auth and current employee id from claims.
This archive does not overclaim a separate active Employee database validation for the list endpoint.
```
