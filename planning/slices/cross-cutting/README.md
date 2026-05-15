# Cross-Cutting And Helper Slices

Status: current cross-cutting/helper slice index  
Scope: independently testable technical/support behavior used by multiple business slices

## 1. Purpose

This folder contains cross-cutting and helper slices.

These are not business scenario slices, but they still use the same source-items-flow-implementation-tests discipline as business slices.

## 2. Terms

### Business slice

Scenario-derived business behavior slice.

### Client sidecar

Client implementation file for a concrete business slice.

### Cross-cutting slice

Technical/support slice with observable behavior, implementation flow and tests, used by multiple business slices.

Examples:

```text
CC-CONST-001-client-constants-generation-and-contract-testing.md
CC-CSRF-001-antiforgery-token-session-context.md
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
| `CC-CSRF-001-antiforgery-token-session-context.md` | Antiforgery token/session context behavior, failure normalization and tests | implementation-ready draft |

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

## 5. Same Format Rule

Cross-cutting and helper slices must follow the same planning shape as business slices:

```text
source requirements
-> behavior items
-> slice flow
-> implementation flow
-> tests
-> coverage/questions/ADR impact
```

They are not allowed to skip behavior items or flow just because the concern is technical.

## 6. Source Type Difference

Business slice behavior items are usually:

```text
scenario-derived
```

Cross-cutting/helper behavior items may be:

```text
security-derived
API-contract-derived
tooling-derived
testing-derived
client-cross-cutting-derived
infrastructure-derived
```

These are still first-class behavior items.

## 7. Flow Naming

Business slices use:

```text
Scenario Slice Flow
```

Cross-cutting/helper slices use:

```text
Concern Slice Flow
```

or a more specific name:

```text
Security Concern Flow
API Contract Concern Flow
Tooling Concern Flow
Testing Concern Flow
```

## 8. Cross-Cutting / Helper Slice Template

```text
# CC-XXX — Title

Status:
Slice type: cross-cutting slice / helper slice
Layers:
Depends on:
Used by:

## 1. Purpose
## 2. Why This Is A Cross-Cutting/Helper Slice
## 3. Inputs / Sources
## 4. Concern-Derived Behavior Items
## 5. Coverage Overview
## 6. Concern Slice Flow
## 7. Implementation Flow
## 8. Target Types / Components
## 9. Test Plan
## 10. Consumer Rule For Business Slices
## 11. Local Questions
## 12. ADR Impact
```

## 9. Behavior Items Must Appear In Flow

Every behavior item must appear in the slice flow before implementation flow.

The flow explains required behavior in user/system/security/tooling terms.

Implementation Flow then explains how this required behavior is implemented.

## 10. Implementation Flow Detail Rule

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
