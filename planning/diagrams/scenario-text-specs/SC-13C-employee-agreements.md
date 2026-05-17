# SC-13C — Employee Agreements

Status: L2 scenario draft / derived from Domain Draft 02  
Source: `planning/tables/domain-drafts/domain-draft-02.md`

## 1. Purpose

Employee sees agreement exchanges that require employee action after request approval or after client counter-proposal.

## 2. Scenario Flow

```text
Employee opens Agreements area
        ↓
System shows approved requests without exchange and active exchanges
        ↓
Employee sees items that need action:
  approved request needing first employee proposal
  exchange AwaitingEmployeeResponse after client version
  exchange eligible for final refusal
        ↓
Employee opens details to send proposal version or final-refuse exchange
```

## 3. Domain Direction

```text
Employee is domain actor.
Use Employee, not Worker.
Domain methods receive Employee object.
Owned state stores scalar EmployeeId/SenderId.
```

## 4. Behavior Items

```text
L2-AGR-EMP-LIST-001 — Employee can see approved requests and agreement exchanges requiring employee action.
L2-AGR-EMP-LIST-002 — Employee-facing agreement scenarios use Employee terminology only.
```
