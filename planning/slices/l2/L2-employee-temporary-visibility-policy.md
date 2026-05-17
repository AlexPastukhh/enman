# L2 Employee Temporary Visibility Policy

Status: accepted temporary L2 read-policy note / first Employee dashboard pass  
Scope: Employee dashboard/list/details read authorization and filtering before department/region/assignment model exists

## 1. Purpose

This note prevents ambiguity around the phrase `employee-visible requests`.

For the first L2 Employee dashboard/read pass, employee visibility is a **backend authorization/read-filtering policy**, not UI visibility.

It answers:

```text
Which requests may a specific Employee see in the employee dashboard/read endpoints?
```

It does not answer:

```text
which rows are visually hidden by the client;
which buttons are enabled;
which review command is allowed after the row is loaded.
```

## 2. Temporary Policy

Temporary L2 employee visibility policy:

```text
All active Employees can see all review-relevant requests.
```

In first implementation terms:

```text
- Employee authentication/active status is required;
- the list/details query may return all review-relevant requests;
- EmployeeId is used to derive employee-relative review labels;
- EmployeeId does not yet narrow the request list by department, region, assignment or personal queue.
```

This means the handler may load all review-relevant requests and use current Employee id only to distinguish:

```text
StartedByCurrentEmployee
StartedByAnotherEmployee
```

That is not a bug in the first pass if the temporary policy is explicitly documented.

## 3. Review-Relevant Meaning

For the first pass, `review-relevant` means requests that belong to the employee review lifecycle/read model, such as requests in review or completed review-related states included by the slice contract.

The exact status set is owned by the relevant read slice contract:

```text
SL-EMP-REQ-001 — Employee Request List Read
SL-EMP-REQ-002 — Employee Request Details Read
```

## 4. Not Current Scope

The first pass does not implement:

```text
- department-based visibility;
- region-based visibility;
- assigned-only visibility;
- personal queues;
- workload balancing;
- employee permissions beyond active Employee access;
- row-level visibility based on the Employee who started review.
```

## 5. Future Extension Direction

Future employee visibility policy may become:

```text
- department/region based;
- assigned Employee based;
- all NotStarted visible, StartedByAnother locked/readonly;
- permission/capability based;
- queue/claim based.
```

When that happens, update:

```text
- scenario text/DATA/behavior items;
- SL-EMP-REQ-001 list read;
- SL-EMP-REQ-002 details read;
- Employee dashboard/details client sidecars;
- review command authorization checks;
- tests for visible/not-visible requests.
```

## 6. Testing Implication

For first-pass read tests:

```text
- prove authenticated active Employee can read review-relevant requests;
- prove Client/non-Employee cannot read Employee endpoints;
- prove Employee-relative label derivation uses current Employee id;
- do not add department/region/assignment filtering tests yet.
```

A mixed dataset test should include a row started by another Employee and assert it remains visible with:

```text
reviewState = StartedByAnotherEmployee
```

That validates the temporary policy and the label derivation without introducing a future assignment model.
