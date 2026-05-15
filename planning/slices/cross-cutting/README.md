# Cross-Cutting And Helper Slices

Status: current cross-cutting/helper slice index  
Scope: independently testable technical/support behavior used by multiple business slices

## 1. Purpose

This folder contains cross-cutting and helper slices.

These are not business scenario slices, but they still have:

```text
- observable/support behavior;
- implementation flow;
- test plan;
- consumers / used-by slices;
- coverage table;
- local questions;
- ADR impact when relevant.
```

## 2. Terms

### Business slice

Scenario-derived business behavior slice.

Example:

```text
SL-REQ-001-create-connection-request
SL-REVIEW-001-approve-request-and-verify-applicant
```

### Client sidecar

Client implementation file for a concrete business slice.

Example:

```text
SL-REQ-001-create-connection-request.client.md
```

### Cross-cutting slice

Technical/support slice with observable behavior, implementation flow and tests, used by multiple business slices.

Example:

```text
CC-CONST-001-client-constants-generation-and-contract-testing.md
```

### Helper slice

Smaller reusable helper/support behavior with implementation and tests.

It is narrower than a cross-cutting slice but is still more structured than a shared note.

### Shared note/helper doc

Reusable note without full slice behavior/test flow.

Example:

```text
planning/slices/shared/*.md
```

## 3. Current Cross-Cutting Slices

| Slice | Purpose | Status |
|---|---|---|
| `CC-CONST-001-client-constants-generation-and-contract-testing.md` | Generates and verifies client-facing constants artifacts and testing strategy | implementation-ready |

## 4. When To Create A Cross-Cutting Or Helper Slice

Create one when the work:

```text
- is used by multiple business slices;
- has observable/support behavior;
- has a concrete implementation path;
- has independent tests;
- introduces a reusable contract, artifact, tool, helper, mapper or shared support flow;
- is too concrete to be only a workflow note.
```

Do not create one for a purely conceptual rule with no implementation/test path.

## 5. Implementation Flow Detail Rule

Implementation flow is behavior-first.

It may include involved classes, methods and short code snippets, but it must not become a full code listing.

Include code/class/method details when they explain:

```text
- contract boundary;
- non-obvious behavior;
- behavior that was discussed/questioned;
- important trade-off;
- future extension/change point;
- error handling;
- testability;
- no-write/no-side-effect guarantee;
- generated artifact shape;
- API/client boundary.
```

Keep high-level only when implementation is routine:

```text
- basic argument parsing;
- simple file write mechanics;
- obvious object construction;
- plain DTO records;
- straightforward method forwarding.
```

If class/method details make the flow noisy, extract them into a sibling `.impl.md` file.

Do not create `.impl.md` files in advance.
