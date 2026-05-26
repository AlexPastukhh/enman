# Cross-Cutting Umbrella Template

Status: canonical template for cross-cutting umbrella / coordination docs  
Scope: copyable structure for concerns that coordinate multiple slice drafts, sides, generated artifacts or shared proof

Use with:

```text
planning/slices/slice-draft-authoring-principles.md
planning/slices/slice-draft-authoring-workflow.md
planning/slices/slice-test-plan-workflow.md
planning/slices/cross-cutting/README.md
```

Authoring notes:

```text
- Use planning/slices/slice-draft-authoring-workflow.md for the process.
- Use this template for umbrella/coordination shape.
- Umbrella docs coordinate ownership and proof across side-specific drafts.
- Server implementation details belong in `.server.md` drafts.
- Client implementation details belong in `.client.md` drafts.
- Single-sided implementation details belong in `SINGLE-*.server.md` or `SINGLE-*.client.md` drafts.
- Compact mode is allowed for small concerns when scope, boundary and proof remain clear.
- For high-risk or separately reviewed sections, include section-level sources using the shape from slice-draft-authoring-workflow.md.
```

````markdown
# <CC-ID> — <Title>

Status:
Concern ID:
Concern type:
Applies to:
Primary behavior source:
Implementation status:
Runtime implementation checked: yes/no

## 0. Concern Sources / Source Sync

Behavior source:
  -

Scenario/source files:
  -

Behavior items:
  -

Related slice source mapping:
  - planning/slices/slice-scenario-flow-behavior-register.md

Related server slice drafts:
  -

Related client slice drafts:
  -

Single-sided slice drafts:
  -

API/generated contract sources:
  -

Testing/source workflow:
  - planning/slices/slice-test-plan-workflow.md

Current code/tests checked, if implemented/current-state sync:
  -

Source status:
  current / provisional / pending source sync / blocked

Notes:

## 0.1 Section-Level Sources

Use this block only for high-risk or separately reviewed sections.

```text
Sources:
  Format:
    - <workflow/template/principles file>
  Content:
    - <scenario/cross-cutting/API/slice source file or earlier section>
  Internal dependencies:
    - <umbrella sections this section depends on>
  Not checked:
    - <explicitly unchecked evidence/source>
```

Recommended sections for section-level sources:

```text
Concern Sources / Source Sync;
Concern Scope / Boundary;
Side-Specific Slice Drafts;
Implementation Coordination Flow;
Behavior Coverage;
Cross-Side Behavior-to-Test Trace.
```

## 1. Purpose

Explain why this umbrella exists.

```text
This umbrella coordinates <concern> across <server/client/generated/testing/etc.> work.
It does not replace side-specific implementation drafts.
```

## 2. Concern Scope

This umbrella coordinates:

```text
-
```

Scope rules:

```text
- Scope comes from cross-cutting behavior/source mapping, not from a random file list.
- Umbrella scope is coordination scope, not side implementation scope.
- The umbrella should not become a full scenario registry or full side-specific implementation draft.
```

## 3. Concern Boundary

### 3.1 Owned By This Umbrella

```text
-
```

### 3.2 Not Owned By This Umbrella

| Not owned behavior / responsibility | Owner / destination | Reason |
|---|---|---|

### 3.3 Conflict / Authority Rule

```text
- Behavior source wins for required behavior.
- Side-specific `.server.md` / `.client.md` drafts win for implementation details on that side.
- This umbrella wins for cross-side coordination, shared decisions and cross-side proof expectations.
```

## 4. Scenario / Behavior Scope

### 4.1 Behavior Identity

Concern behavior:

Behavior purpose:

Source status:

### 4.2 Behavior Covered By This Umbrella

This umbrella coordinates these behavior items/outcomes:

```text
-
```

### 4.3 Behavior Not Implemented By This Umbrella

| Behavior/source item not implemented here | Owner / destination | Reason |
|---|---|---|

## 5. Side-Specific Slice Drafts

| Side | Draft | Responsibility | Status | Notes |
|---|---|---|---|---|
| Server |  |  |  |  |
| Client |  |  |  |  |
| Single-sided |  |  |  |  |
| Generated/API/tooling |  |  |  |  |

Rules:

```text
- Use paired logical IDs with `.server.md` / `.client.md` when a counterpart exists or may exist.
- Use `SINGLE-` prefix when a slice draft is intentionally one-sided.
- Do not hide side implementation details in the umbrella when a side draft should own them.
```

## 6. Cross-Cutting Concern Applicability

| Concern aspect | Server applies? | Client applies? | Generated/tooling applies? | Umbrella rule / owner |
|---|---:|---:|---:|---|
| Auth/session |  |  |  |  |
| CSRF / unsafe request protection |  |  |  |  |
| ProblemDetails / error mapping |  |  |  |  |
| Client feedback / visibility |  |  |  |  |
| Accessibility / ARIA |  |  |  |  |
| OpenAPI/generated artifacts |  |  |  |  |
| Testing / E2E proof |  |  |  |  |

## 7. Shared Decisions

| ID | Decision | Status | Applies to | Notes |
|---|---|---|---|---|

Decision statuses:

```text
accepted
current direction
open
blocked
superseded
```

## 8. Implementation Coordination Flow

Describe the cross-side coordination flow, not low-level side implementation internals.

```text
[Behavior source]
-

[Server slice / if applies]
-

[Client slice / if applies]
-

[Generated/API artifacts / if applies]
-

[Cross-side verification]
-

[Rollout/status]
-
```

## 9. Behavior Coverage

Behavior Coverage is not Scope and not Test Plan.

| Behavior | Status | Covered by / delegated to | Notes |
|---|---|---|---|

Status examples:

```text
covered
covered by server slice
covered by client slice
delegated
out of scope
future
blocked
```

## 10. Cross-Side Behavior-to-Test Trace

Primary rule:

```text
Tests verify behavior items and cross-side outcomes.
Implementation details are only setup/action/observation mechanisms.
Required assertions live inside the trace.
```

| Behavior | Outcome proved | Server proof | Client proof | E2E/user proof if needed | Required assertions | Gap / owner |
|---|---|---|---|---|---|---|

Proof guidance:

```text
- Server proof owns server/domain/API behavior and persisted outcomes.
- Client proof owns visible UI behavior, client mapping, feedback and accessibility where relevant.
- E2E proof is for critical integrated flows, not a replacement for all server/client matrix tests.
- Generated/API checks prove contract artifact consistency, not business behavior by themselves.
```

## 11. Generated / API / Contract Verification

```text
OpenAPI generation/check:
TypeScript client generation/check:
Constants/error codes generation/check:
check:api or equivalent:
```

## 12. Rollout / Implementation Order

| Step | Owner | Depends on | Done when |
|---|---|---|---|

## 13. Risks / Open Questions

| ID | Risk / Question | Owner | Status | Next action |
|---|---|---|---|---|

## 14. Current Status / Sync Check

```text
Current status:
Runtime implementation checked:
Current code/tests checked:
Known drift:
Blocked by:
```

Do not claim implemented/current unless current code/tests/generated artifacts were checked where relevant.

## 15. Local / Global Sync Check

After drafting or refactoring, decide whether to update:

```text
planning/slices/SLICE-INDEX.md
planning/slices/slice-scenario-flow-behavior-register.md
planning/slices/slice-questions-register.md
planning/slices/slice-extension-points-register.md
planning/slices/slice-implementation-notes-register.md
planning/slices/slice-responsibility-map.md
planning/slices/README.md
```

Sync result:

```text
Not needed / Updated / Deferred to <register or PMR item>
```

## 16. Guardrail Summary

```text
- Do not broaden concern scope silently.
- Do not invent behavior items locally when cross-cutting behavior sources exist.
- Do not hide server implementation details in the umbrella.
- Do not hide client implementation details in the umbrella.
- Do not use UI visibility as security.
- Do not leave required assertions outside the cross-side trace.
- Do not claim current implementation without current code/test evidence.
```
````
