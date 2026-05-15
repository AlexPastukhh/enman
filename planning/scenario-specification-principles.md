# Scenario Specification Principles

Status: current source of truth for scenario specification principles  
Scope: textual scenario specs, scenario DATA blocks, scenario UI specs, validation addenda, behavior items and scenario questions

## 1. Read Order

```text
planning/README.md
planning/planning-workflow-current.md
planning/planning-agent-protocol.md
planning/scenario-specification-principles.md
planning/scenario-domain-validation-principles.md
planning/diagrams/scenario-text-specs/README.md
planning/diagrams/scenario-data/README.md
planning/diagrams/scenario-ui-specs/README.md
planning/diagrams/scenario-questions-register.md
planning/diagrams/scenario-behavior-items/README.md
planning/client/README.md
planning/slices/README.md
```

## 2. Scenario Specs

Scenario specs describe user-facing behavior and planning semantics.

They may contain actor/screen/goal, entry points, preconditions, DATA refs, main flow, branches, invariants, observable UI requirements, client-side validation, server/domain validation, observable outcomes, open questions and ADR candidates.

They must not become controller specs, endpoint maps, DB schemas, ORM mappings, React component plans, final aggregate implementation or final visual design.

## 3. Scenario Drafting Output Set

For each active scenario, maintain these artifacts together when relevant:

```text
1. Scenario text spec.
2. Scenario DATA spec.
3. Scenario UI spec, when client-visible behavior is being planned.
4. Validation/security addendum entries, when needed.
5. Per-scenario behavior items file.
6. Scenario questions register entries, when unresolved questions exist.
```

## 4. Scenario Question Loop

```text
question appears
-> classify it
-> if scenario-level, add/update scenario questions register
-> clarify / choose current direction
-> update scenario text spec if behavior changed
-> update DATA file if visible/input/selectable/filter/attachment data changed
-> update scenario UI spec if UI-visible requirement changed
-> update validation/security addendum if needed
-> update behavior items / UI behavior items if required behavior changed
-> continue implementation planning
```

Scenario-level means the question affects required behavior, DATA, UI-visible requirement, validation/security, user-visible outcome or scenario semantics.

## 5. Observable UI Requirements In Scenario Specs

Scenario specs may include mandatory business-visible UI behavior:

```text
what user must see
what user must understand
what action must be available
what status/result/feedback must be visible
what forbidden access/action must be prevented or rejected
```

Do not put layout or component choices into scenario specs.

Detailed UI-visible decisions can be placed in scenario UI specs.

Detailed client implementation belongs in `.client.md` sidecars when concrete client work starts.

## 6. Scenario UI Specs

Scenario UI specs live under:

```text
planning/diagrams/scenario-ui-specs/
```

They capture UI-visible requirements, UI behavior items, accepted UI decisions and UI questions.

They do not define React components, hooks, API adapters, CSS module filenames or final visual design.

## 7. Scenario Behavior Items

Per-scenario behavior items live under:

```text
planning/diagrams/scenario-behavior-items/
```

Behavior items and UI behavior items must not invent new behavior.

## 8. DATA

DATA means only what actor enters, sees, selects, filters/searches by, attaches/uploads, or references as visible/selectable business item.

DATA files must not contain validation/rules sections, testable behavior sections, invariants, preconditions, branches, access rules, security policy or layout choices.

## 9. Validation

Scenario specs should distinguish client-side validation from server-side/domain validation.

## 10. Current Project Decisions

```text
Request statuses: InReview, Approved, Rejected.
Do not use Submitted unless reintroduced later with precise meaning.

Approval does not automatically create agreement proposal.
Agreement proposal starts only by employee action on an Approved request.
```

## 11. Source Of Truth Rule

Use corrected text specs, DATA specs, scenario UI specs, validation addenda, scenario questions register and behavior items.

Do not use stale generated diagram package summaries or stale `.drawio` pages as semantic source of truth until regenerated.

## 12. Current Downstream Planning Step

```text
scenario specs / DATA / UI specs / validation
-> behavior items / UI behavior items / questions register
-> slice boundary / parent slice / client sidecar workflow
```
