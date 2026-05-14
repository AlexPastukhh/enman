# L1 Slice Drafting Guide

Status: current slice drafting workflow  
Scope: L1 slice boundary discovery, per-slice implementation planning, Client/UI flow and shared support

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

A valid slice has observable behavior, clear start/end, scenario-derived flow, DATA/items/invariants/no-write rules in that flow, independent testability, clear dependency/extension markers, package boundary and implementation path in the per-slice file.

A slice is not a controller method, endpoint alone, repository, DB table, React component, aggregate alone, shared helper, or arbitrary technical task.

## 3. Two-Level File Model

### 3.1 General boundary file

The general boundary file is:

```text
planning/slices/l1-slice-boundary-draft-01.md
```

It contains slice discovery rules, slice boundary criteria, composite overview tables, scenarios, derived slices, Scenario Slice Flow, boundary questions, boundary decisions, dependency/extension/later-package mapping, flow assignment coverage, and ADR candidates.

It does not contain detailed implementation flow.

### 3.2 Per-slice files

Per-slice files contain detailed implementation planning.

They include:

```text
- Slice overview;
- questions overview near the beginning;
- flow coverage overview near the beginning;
- Scenario Slice Flow duplicated/refined from the boundary draft;
- Implementation Flow;
- Client/UI Flow as the first implementation-flow section when relevant;
- Test Plan / Test Coverage;
- detailed implementation notes;
- decisions;
- ADR links/candidates;
- implementation checklist.
```

## 4. Scenario Slice Flow

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
- client/UI/read/UX candidate items if relevant;
- note about related/dependent slices if this step is split out.
```

## 5. Implementation Flow

Implementation Flow belongs to per-slice files, not to the general boundary draft.

When the slice has client-visible behavior, the first implementation-flow section must be:

```text
I01 — Client/UI Flow
```

Then continue with server-side and persistence layers:

```text
I01. Client/UI Flow
I02. API endpoint / contract
I03. Server endpoint / handler
I04. Application service / orchestration
I05. Domain method calls
I06. Persistence / transaction / read model
I07. Response mapping
```

Implementation Flow should be written in words first. As the draft matures, it may include pseudocode or real code snippets inside specific implementation flow sections. If something is not implemented, say so explicitly.

## 6. Client/UI Flow Rule

Client/UI Flow is part of Implementation Flow.

It must describe concrete UI, not only abstract “UI calls API”.

Include:

```text
- page/screen name;
- component/form name;
- visible data;
- input fields;
- buttons/actions;
- disabled/hidden/action-availability rules;
- deferred client-side validation behavior;
- where field-level errors are shown;
- where global/form-level errors are shown;
- API request DTO construction;
- API client call;
- CSRF/antiforgery request token use for unsafe requests when applicable;
- server validation/problem mapping;
- success state;
- navigation after success;
- client-side tests needed.
```

## 7. Current Client-Side Logic Notes

Current known client-side logic includes:

```text
- deferred validation after input changes;
- client-side validation tests for that delayed behavior;
- server/global error message display;
- field-level error display;
- future applicant-data prefill into request forms;
- future client auth/session support for ASP.NET Core cookie auth and antiforgery tokens.
```

Client validation is described in the validation addendum, but behavior baseline may not yet expose stable client/UI item IDs. When needed, add `CLIENT-*` candidate items in per-slice files first.

## 8. Antiforgery / CSRF Shared Support Rule

ASP.NET Core cookie-auth unsafe requests need antiforgery support.

This is not a business slice.

Use:

```text
[SHARED SUPPORT][AUTH/FRAMEWORK][CROSS-SLICE]
```

Client responsibilities:

```text
- fetch request token;
- store token in runtime client state;
- attach token to unsafe requests;
- refetch token when auth/session state changes;
- usually refetch after login/logout so token pair matches the current security context.
```

Server responsibilities:

```text
- use IAntiforgery to create/write antiforgery cookie and produce request token;
- expose token fetch path/endpoint;
- validate unsafe requests using ASP.NET Core antiforgery infrastructure.
```

Document detailed support in:

```text
planning/slices/shared/antiforgery-token-session-context.md
```

## 9. Shared Support Rule

Shared helpers/support artifacts used by multiple slices should be documented under:

```text
planning/slices/shared/
```

They are not slices unless they become independently observable behavior.

Examples:

```text
- client/server validation error mapping;
- deferred client validation pattern;
- antiforgery token/session-context support;
- applicant data prefill notes used by request UI slices;
- shared API request helpers;
- server application result mapping.
```

## 10. Test Planning Rule

Tests should not be mixed into implementation narrative.

Use a separate section:

```text
## Test Plan / Test Coverage
```

Use groups:

```text
Client tests
- component/page tests;
- deferred validation tests;
- request builder/API client tests;
- server error mapping tests;
- navigation tests.

Server tests
- domain unit tests;
- application tests;
- integration/API/persistence tests;
- no-write tests.

End-to-end tests
- full user flow after client and server parts are stable.
```

End-to-end tests should be placed at the end of the slice explanation because they verify the whole assembled flow.

## 11. Per-Slice File Structure

```text
1. Slice overview
2. Questions overview
3. Flow coverage overview
4. Scenario Slice Flow
5. Implementation Flow
   I01. Client/UI Flow
   I02. API endpoint / contract
   I03. Server endpoint / handler
   I04. Application service / orchestration
   I05. Domain method calls
   I06. Persistence / transaction / read model
   I07. Response mapping
6. Test Plan / Test Coverage
   - Client tests
   - Server tests
   - End-to-end tests
7. Shared support used by this slice
8. Detailed Implementation Notes
9. Decisions
10. ADR Links / Candidates
11. Implementation Checklist
```

## 12. Done Criteria

General boundary draft is acceptable when it identifies real slices, embeds DATA/items/invariants/no-write expectations into Scenario Slice Flow, makes L1/L2/dependent/extension split explicit, and collects boundary questions/decisions.

Per-slice file is acceptable when it duplicates/refines Scenario Slice Flow, has Client/UI-first Implementation Flow when relevant, separates tests, documents current vs planned implementation, references shared support, and has an implementation checklist.
