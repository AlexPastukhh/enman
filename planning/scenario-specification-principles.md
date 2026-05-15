# Scenario Specification Principles

Status: current source of truth for scenario specification principles  
Scope: textual scenario specs, scenario DATA blocks, validation addenda, behavior items and scenario questions

## 1. Read Order

```text
planning/README.md
planning/planning-workflow-current.md
planning/planning-agent-protocol.md
planning/scenario-specification-principles.md
planning/scenario-domain-validation-principles.md
planning/diagrams/scenario-text-specs/README.md
planning/diagrams/scenario-data/README.md
planning/diagrams/scenario-questions-register.md
planning/diagrams/scenario-behavior-items/README.md
planning/tables/pre-domain-variants-input.md
planning/slices/README.md
```

## 2. Scenario Specs

Scenario specs describe user-facing behavior and planning semantics.

They may contain actor/screen/goal, entry points, preconditions, DATA refs, main flow, branches, invariants, observable UI requirements, client-side validation, server/domain validation, observable outcomes, open questions and ADR candidates.

They must not become controller specs, endpoint maps, DB schemas, ORM mappings, React component plans, final aggregate implementation or final visual design.

## 3. Scenario Drafting Output Set

For each active scenario, maintain these artifacts together:

```text
1. Scenario text spec.
2. Scenario DATA spec.
3. Validation/security addendum entries, when needed.
4. Per-scenario behavior items file, when behavior items are migrated/created.
5. Scenario questions register entries, when unresolved questions exist.
```

Behavior items are derived from scenario main flow, branches, invariants, outcomes, DATA requirements and validation/security addenda.

Behavior items must not invent new behavior.

## 4. Scenario Question Loop

When domain draft, slice draft, client sidecar or implementation planning reveals a scenario-level question:

```text
question appears
-> classify it
-> if scenario-level, add/update scenario questions register
-> clarify / choose current direction
-> update scenario text spec if behavior changed
-> update DATA file if visible/input/selectable/filter/attachment data changed
-> update validation/security addendum if needed
-> update behavior items if required behavior changed
-> continue implementation planning
```

Scenario-level means the question affects required behavior, DATA, validation/security, user-visible outcome or scenario semantics.

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

Detailed client implementation belongs in `.client.md` sidecars when concrete client work starts.

## 6. Scenario Behavior Items

Per-scenario behavior items live under:

```text
planning/diagrams/scenario-behavior-items/
```

The compiled downstream baseline remains:

```text
planning/tables/pre-domain-variants-input.md
```

Use the same behavior item category/card/table style as the compiled baseline.

Default categories:

```text
CMD
LC
IBS
VI
UCQ
READ
INT
FUT
NW
SQ
```

Behavior item migration / cleanup is a separate future step.

## 7. DATA

Use `DATA`, not `DETAIL`.

DATA means only what actor enters, sees, selects, filters/searches by, attaches/uploads, or references as visible/selectable business item.

DATA files must not contain validation/rules sections, testable behavior sections, invariants, preconditions, branches, access rules, security policy or layout choices.

DATA specs feed per-scenario behavior items, scenario questions register, domain drafts, slice boundary drafts, parent slice files, client sidecar Scenario / DATA Coverage tables and read/query DTO planning.

## 8. Validation

Scenario specs should distinguish client-side validation from server-side/domain validation.

Client-side validation is UX feedback and correction loop.

Server-side/domain validation is authoritative.

Use:

```text
planning/diagrams/scenario-text-specs/scenario-server-domain-validation-addendum.md
planning/scenario-domain-validation-principles.md
```

## 9. Current Project Decisions

Request statuses:

```text
InReview
Approved
Rejected
```

Do not use `Submitted` unless reintroduced later with precise meaning.

Applicant DATA reuse in request creation:

```text
matching saved applicant DATA may be copied/prefilled into request form
client may edit prefilled request-local fields
editing request-local fields does not delete or mutate saved ApplicantData
```

Agreement proposal rule:

```text
Approval does not automatically create agreement proposal.
Agreement proposal starts only by employee action on an Approved request.
```

## 10. Source Of Truth Rule

Use corrected text specs, DATA specs, validation addenda, scenario questions register and behavior items.

Do not use stale generated diagram package summaries or stale `.drawio` pages as semantic source of truth until regenerated.

## 11. Current Downstream Planning Step

Current active branch:

```text
scenario specs / DATA / validation
-> behavior items / questions register
-> slice boundary / parent slice / client sidecar workflow
```
