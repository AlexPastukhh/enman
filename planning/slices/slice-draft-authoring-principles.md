# Slice Draft Authoring Principles

Status: current slice-layer authoring principles  
Scope: explains what a slice draft is and how its scope, boundary, coverage, implementation flow and verification sections should work

## 1. Purpose

Use this file when creating, reviewing or refactoring a slice draft.

This file is not a template and not a workflow. It explains the principles behind slice draft sections.

Concrete copyable structures live in templates. Repeatable step-by-step processes live in workflow files.

## 2. What Is A Slice

A slice is a responsibility boundary around a verifiable part of behavior.

A slice is not always a vertical end-to-end change through every application layer.

A slice can be:

```text
paired client/server;
client-only;
server-only;
extension/follow-up;
cross-cutting;
read-only;
state-changing command.
```

A scenario can be implemented by multiple slices. A slice can implement one independent chain of behavior items. A slice can also extend behavior already implemented by another slice.

## 3. What Is A Slice Draft

A slice draft is not a technical TODO and not only a list of files.

A slice draft records:

```text
which behavior the slice adds;
what it definitely does;
what it definitely does not do;
which existing sources, decisions and components it depends on;
which implementation responsibilities participate;
which domain/system/user outcomes must result;
which tests or checks prove those outcomes.
```

A good slice draft must be strict enough that an implementation agent does not add unrelated work, but not so fragile that harmless class/method renames look like implementation drift.

Core principle:

```text
Meaning and responsibility matter more than exact first-pass class names.
```

## 4. Scope Principle

Slice scope starts from scenario/source mapping, not from a random group of files.

A slice scope answers:

```text
Which part of the scenario/source behavior does this slice implement and verify?
```

Scope should include verifiable behavior:

```text
actor action;
system transition;
endpoint/page/action if relevant;
domain transition if relevant;
persisted or visible outcome;
verification expectation.
```

Bad scope:

```text
Implement agreement exchange.
```

Good scope:

```text
Add command for sending the next agreement proposal version for an existing exchange.
The command supports Client and Employee actors, creates one new proposal version,
supersedes the previous active proposal, switches waiting state to the other side,
and returns 204 No Content.
```

## 5. Scenario Scope / Slice Boundary

A slice draft should show the scenario/source boundary without becoming a full scenario dependency map.

The boundary section should answer:

```text
1. Which scenario/source artifacts does this slice use?
2. Which part of that behavior does this slice implement?
3. Which related behavior does this slice explicitly not implement?
4. Which other slice, layer, register or future work owns the excluded behavior?
5. Which cross-cutting concerns exist around the scenario and apply to this slice?
```

Scenario inventory belongs to the scenario layer. Slice drafts should reference relevant sources and mapping, not copy the whole scenario registry.

## 6. Behavior Not Implemented By This Slice

Every non-trivial slice draft should explicitly state important related behavior that is not in scope.

This prevents:

```text
scope creep;
agent overimplementation;
accidental UI/security/storage work inside a server command;
confusion between current slice and future slices.
```

Use an owner/destination column:

```text
Scenario behavior not in this slice | Owner / destination | Reason
```

Owners can be another slice, future work, a cross-cutting concern, client sidecar, server sidecar, domain layer, API layer or testing layer.

## 7. Cross-Cutting Concerns

Cross-cutting concerns should be listed only in the sense relevant to this slice.

A slice may follow a cross-cutting policy without owning the whole policy.

Example:

```text
CSRF / unsafe request protection:
  applies to this server command;
  this slice follows the policy;
  the full policy/matrix belongs to the CSRF cross-cutting source or slice.
```

Do not duplicate full cross-cutting designs inside every slice draft.

## 8. Slice Relations And Future-Change Check

Slice relations should stay short.

Include only:

```text
concrete prerequisites without which this slice does not work;
concrete planned follow-up slices if they are known;
future-change checks that should shape current design without expanding current scope.
```

A future-change check asks:

```text
Do we already know about a likely future change that should affect today's boundary or seam?
```

It is not a wishlist and not additional scope.

Use extension/change pressure principles for deeper decisions:

```text
planning/slices/change-extension-points-principles.md
```

## 9. Semantic First-Pass Implementation Names

Slice drafts may name expected components, classes and methods to make implementation responsibility visible.

Those names are semantic first-pass names, not eternal contracts.

Not drift by itself:

```text
class/method/file names differ from draft names;
service is named differently;
controller method is named differently;
repository shape differs but responsibility remains equivalent;
DTO name differs while contract shape remains equivalent.
```

Use names to explain responsibility, not to freeze naming forever.

## 10. Implementation Drift

Implementation drift is about responsibility and behavior, not harmless naming changes.

Real drift includes:

```text
controller owns lifecycle or turn rules;
validator checks domain ownership, exchange status or current turn;
application service mutates aggregate state without domain method when domain owns the invariant;
client UI visibility is used as a security boundary;
failed command partially writes persisted state;
command returns read/details DTO when the slice says 204 No Content;
binary upload appears inside a reference-only command slice;
a slice starts an aggregate/process that should only be added by another slice;
tests do not prove the behavior outcomes the slice claims to implement.
```

## 11. Domain / Application / Validation Placement

For server slices, domain methods and component responsibilities should be described before implementation flow.

General rule:

```text
controller = HTTP/auth/route/DTO binding/response mapping;
validator = request/query shape validation;
application = orchestration, current actor/load/save, transaction boundary;
domain = invariants, lifecycle, state transition and impossible-state protection;
persistence = durable atomic storage of the chosen changes.
```

Detailed backend/server implementation principles live in:

```text
planning/slices/server/server-implementation-principles.md
```

## 12. Behavior Coverage

Behavior Coverage is not Scope and not Test Plan.

```text
Scope:
  what behavior the slice should implement.

Behavior Coverage:
  how the behavior is classified after scope, out-of-scope, domain methods and implementation flow are known.

Test Plan:
  how behavior/outcomes are proved.
```

Behavior Coverage should classify behavior as:

```text
covered;
out of scope;
future;
delegated;
covered boundary.
```

## 13. Behavior-To-Test Trace Principle

Behavior-to-Test Trace is the main proof map for slice verification.

It should connect:

```text
behavior;
outcome proved;
test layer;
setup/action mechanism;
required assertions;
escape risk;
refactor risk;
planned/actual test.
```

Required assertions belong inside the trace. Loose success/no-write assertion lists after the trace are secondary and must not become the source of truth.

Testing details live in:

```text
planning/slices/slice-test-plan-workflow.md
```

## 14. UI / Client / Security Boundary

A server slice does not own client UI unless client behavior is explicitly in scope.

If UI/client work is out of scope, record it in:

```text
out-of-scope behavior;
planned follow-ups;
what not to test here;
guardrail summary.
```

Security must be enforced server-side and/or domain-side. UI visibility is UX, not a security boundary.

## 15. Agent Guardrails

Before editing a slice draft:

```text
1. Start from scenario/source mapping.
2. Identify what scenario behavior this slice implements.
3. Identify what related behavior this slice does not implement.
4. Identify applicable cross-cutting concerns.
5. List concrete prerequisites and follow-ups only if known.
6. Run future-change check without expanding scope.
7. Put domain methods and components before implementation flow.
8. Treat component names as semantic first-pass names.
9. Treat responsibility/behavior mismatch as drift.
10. Build Behavior Coverage before Test Plan.
11. Put required assertions inside Behavior-to-Test Trace.
12. Do not use client UI visibility as a security boundary.
```

## 16. Relationship To Other Slice Files

```text
slice-responsibility-map.md
  tells where slice-layer information belongs.

SERVER-SLICE-TEMPLATE.md / CLIENT-SLICE-TEMPLATE.md
  provide copyable draft structures.

SERVER-SLICE-DRAFTING-WORKFLOW.md / CLIENT-SLICE-DRAFTING-WORKFLOW.md
  describe repeatable drafting processes.

slice-test-plan-workflow.md
  owns verification trace details.

change-extension-points-principles.md
  owns change/extension pressure definitions and handling options.

implementation-principles.md
  owns common implementation principles near slice planning.
```
