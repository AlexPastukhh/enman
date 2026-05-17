# SC-13C — Employee Agreements

Status: L2 scenario draft / Employee exchange list direction synchronized  
Source: `planning/tables/domain-drafts/domain-draft-02.md`

## 1. Purpose

Employee sees existing agreement exchanges that may require Employee-side attention.

Approved requests without an agreement exchange are started from Employee request details, not from the agreement exchange list first pass.

## 2. Scenario Flow

```text
Employee opens Agreements area
        ↓
System shows existing agreement proposal exchanges visible to Employees
        ↓
Employee sees items that need action:
  exchange AwaitingEmployeeResponse after client version
  active exchange eligible for final refusal
  exchange with final outcome for review/history
        ↓
Employee opens agreement exchange details to send proposal version or final-refuse exchange
```

Start of a new exchange is covered by `SC-13D` and is hosted by Employee request details because the exchange does not exist yet.

## 3. Domain Direction

```text
Employee is domain actor.
Use Employee, not Worker.
Domain methods receive Employee object.
Owned state stores scalar EmployeeId/SenderId.
No EmployeeRef.
```

## 4. Employee Access Direction

```text
Do not add ResponsibleEmployeeId as first-pass authorization guard.
Any active Employee can service the same agreement exchange first pass.
Proposal versions still record the actual sending Employee through Sender and SenderId.
```

Example valid proposal history:

```text
v1 Employee employee-1
v2 Client client-1
v3 Employee employee-2
```

## 5. Behavior Items

```text
L2-AGR-EMP-LIST-001 — Employee can see employee-visible existing agreement exchanges.
L2-AGR-EMP-LIST-002 — Employee-facing agreement scenarios use Employee terminology only.
L2-AGR-EMP-LIST-003 — Employee is not exchange-level owner first pass; proposal sender is tracked per version.
```
