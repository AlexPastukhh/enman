# SC-13C вЂ” Employee Agreements

Status: L2 scenario draft / Employee exchange list direction synchronized  
Doc version: v0.1.0  
Source: `planning/tables/domain-drafts/domain-draft-02.md`

## 1. Purpose

Employee sees existing agreement exchanges that may require Employee-side attention.

Approved requests without an agreement exchange are started from Employee request details, not from the agreement exchange list first pass.

## 2. Scenario Flow

```text
Employee opens Agreements area
        в†“
System shows existing agreement proposal exchanges visible to Employees
        в†“
Employee sees items that need action:
  exchange AwaitingEmployeeResponse after client version
  active exchange eligible for final refusal
  exchange with final outcome for review/history
        в†“
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
L2-AGR-EMP-LIST-001 вЂ” Employee can see employee-visible existing agreement exchanges.
L2-AGR-EMP-LIST-002 вЂ” Employee-facing agreement scenarios use Employee terminology only.
L2-AGR-EMP-LIST-003 вЂ” Employee is not exchange-level owner first pass; proposal sender is tracked per version.
```

## Diagram / Implementation Markers

These markers are for diagrams and diploma planning only. They do not replace current repo implementation evidence.

| Scenario element | Marker | Diagram / implementation meaning |
|---|---|---|
| Employee agreement exchange list | `[PLANNED]` | Employee-facing read surface for existing agreement exchanges. |
| Employee access: any active Employee first pass | `[DESIGNED]` | Accepted first-pass service model; no `ResponsibleEmployeeId` guard. |
| Employee opens existing exchange details | `[PLANNED]` | Existing exchange details/actions are agreement area work. |
| Starting exchange from approved request details | `[PLANNED]` | Start is triggered from Employee request details (`SC-13D`), because the exchange does not exist yet. |
| Department/assignment ownership model | `[DEFERRED]` | Future employee visibility/assignment model if needed. |

