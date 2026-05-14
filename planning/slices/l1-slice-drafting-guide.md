# L1 Slice Drafting Guide

Status: current slice drafting workflow  
Scope: L1 slice boundary discovery and per-slice implementation planning

## 1. Purpose

This guide defines how to move from scenarios to independently testable implementation slices.

Current workflow:

```text
scenario specs / DATA / baseline items
-> L1 slice boundary draft
-> per-slice implementation file
-> implement one slice
-> update coverage / questions / decisions / ADR candidates
-> next slice
```

A slice draft is not an endpoint list.

## 2. Working Definition

```text
Slice = independently testable unit of observable behavior
        + implementation path needed to deliver/test that behavior.
```

A valid slice has:

```text
- observable behavior;
- clear start and end;
- scenario-derived flow;
- DATA / behavior items / invariants / no-write rules included in that flow;
- independent testability;
- clear dependency/extension markers;
- clear package boundary: L1 / L2 / later;
- clear implementation path in the per-slice file.
```

A slice is not a controller method, endpoint alone, repository, DB table, React component, aggregate alone, or arbitrary technical task.

## 3. Two-Level File Model

### 3.1 General boundary file

The general boundary file is:

```text
planning/slices/l1-slice-boundary-draft-01.md
```

It contains slice discovery rules, slice boundary criteria, composite overview tables, scenarios, derived slices, Scenario Slice Flow, boundary questions, boundary decisions, dependency/extension/later-package mapping, flow assignment coverage, and ADR candidates.

It does not contain detailed implementation flow.

The boundary file answers:

```text
What are the slices and why are they real slices?
```

### 3.2 Per-slice files

Per-slice files contain detailed implementation planning.

They include:

```text
- Slice overview;
- questions overview near the beginning;
- flow coverage overview near the beginning;
- Scenario Slice Flow duplicated/refined from the boundary draft;
- Implementation Flow;
- UI blueprint;
- Test Plan / Test Coverage;
- detailed implementation notes;
- decisions;
- ADR links/candidates;
- implementation checklist.
```

A per-slice file answers:

```text
How is this slice implemented and tested?
```

## 4. Slice Discovery Questions

Ask before naming slices:

```text
1. What separate observable behavior units exist in this scenario?
2. What does the user or system observe on success?
3. What failure/no-write behavior is observable?
4. Which behavior changes state?
5. Which behavior is read/visibility-only?
6. Which behavior is UI-only or UX-only?
7. Which behavior extends an existing flow?
8. Which behavior depends on another slice?
9. Which behavior could be a plugin/replaceable module or external integration?
10. Which behavior is cross-cutting across multiple slices?
11. Can this behavior be tested independently?
12. Can it be tested without full UI?
13. Does it belong to current L1 or later package?
14. Is it a slice, a branch of a slice, or a cross-cutting concern?
```

## 5. Scenario Slice Flow

Scenario Slice Flow belongs both to the general boundary draft and per-slice files.

It is not implementation detail.

Each flow step should include:

```text
- step id;
- scenario action / observable behavior;
- DATA involved;
- behavior item refs;
- invariants / rules;
- no-write expectations;
- UI/read/UX candidate items if relevant;
- note about related/dependent slices if this step is split out.
```

Example:

```text
F04. Client provides request details.
     DATA:
     - request details text

     Items / rules:
     - REQ-CMD-CREATE-001
     - invalid request details do not create request

     No-write:
     - failed validation creates no persisted request
```

## 6. Implementation Flow

Implementation Flow belongs to per-slice files, not to the general boundary draft.

Typical flow:

```text
I01. UI blueprint
I02. API endpoint / contract
I03. Controller / endpoint handler
I04. Application service / orchestration
I05. Domain method calls
I06. Persistence / transaction / read model
I07. Response mapping
I08. Testing hooks / integration boundary
```

Implementation Flow should be written in words first.

As the draft matures, it may include pseudocode or real code snippets inside specific implementation flow sections.

If something is not implemented, say so explicitly.

## 7. UI Blueprint Rule

Every per-slice file should mention UI.

If UI is inside this slice, describe it as part of the Implementation Flow.

If UI is a dependent slice, still include a UI blueprint:

```text
UI is not part of the current implemented scope.
Related UI slice: ...
Blueprint:
- screen/page;
- user action;
- inputs/visible data;
- success feedback;
- validation/error feedback;
- navigation/result.
```

## 8. Test Planning Rule

Tests should not be mixed into implementation narrative.

Implementation Flow may briefly say “verified by integration test”, but detailed test planning belongs to:

```text
## Test Plan / Test Coverage
```

Use groups:

```text
Domain unit tests
Application tests
Integration tests
UI/component/E2E tests
Current coverage
Missing tests
No-write tests
```

## 9. General Boundary File Structure

```text
1. Purpose
2. Slice boundary criteria
3. Package / marker model
4. Composite L1 slice overview
5. Boundary questions overview
6. Scenario flow assignment coverage overview
7. Existing L1 foundation context
8. Scenario sections
9. Consolidated slice boundary table
10. Extension / dependent / later-package slice register
11. Decisions / resolved boundary questions
12. Open boundary questions
13. ADR candidates
14. Next per-slice files
```

## 10. Per-Slice File Structure

```text
1. Slice overview
2. Questions overview
3. Flow coverage overview
4. Scenario Slice Flow
5. Implementation Flow
6. UI Blueprint
7. Test Plan / Test Coverage
8. Detailed Implementation Notes
9. Decisions
10. ADR Links / Candidates
11. Implementation Checklist
```

## 11. Auth / Framework Guard Rule

Protected active account guard is not a standalone business slice.

It is:

```text
[AUTH/FRAMEWORK][CROSS-CUTTING]
```

It is applied through application service / auth boundary inside protected slices.

## 12. Done Criteria

General boundary draft is acceptable when it identifies real slices, embeds DATA/items/invariants/no-write expectations into Scenario Slice Flow, makes L1/L2/dependent/extension split explicit, and collects boundary questions/decisions.

Per-slice file is acceptable when it duplicates/refines Scenario Slice Flow, has Implementation Flow, includes UI blueprint, separates tests, documents current vs planned implementation, and has an implementation checklist.
