# Scenario Specification Principles

Status: current source of truth for scenario specification principles  
Scope: textual scenario specs, scenario DATA blocks, scenario UI specs, validation addenda, behavior items, scenario questions and diagram prompt handoff

## 1. Read Order

```text
planning/README.md
planning/planning-workflow-current.md
planning/planning-agent-protocol.md
planning/agent-roles-and-required-actions.md
planning/scenario-specification-principles.md
planning/scenario-domain-validation-principles.md
planning/diagrams/scenario-drafting-workflow.md
planning/diagrams/scenario-text-specs/README.md
planning/diagrams/scenario-data/README.md
planning/diagrams/scenario-ui-specs/README.md
planning/diagrams/scenario-questions-register.md
planning/diagrams/scenario-behavior-items/README.md
planning/client/README.md
planning/slices/README.md
```

If scenario work may lead to diagrams, also read:

```text
planning/diagrams/diagram-prompt-generation-workflow.md
planning/diagrams/drawio-diagram-generation-workflow.md
```

## 2. Scenario Specs

Scenario specs describe user-facing behavior and planning semantics.

They may contain actor/screen/goal, entry points, preconditions, DATA refs, main flow, branches, invariants, observable UI requirements, client-side validation, server/domain validation, observable outcomes, open questions, assumptions and ADR candidates.

They must not become controller specs, endpoint maps, DB schemas, ORM mappings, React component plans, final aggregate implementation or final visual design.

## 3. Scenario Drafting Output Set

For each active scenario, maintain these artifacts together when relevant:

```text
1. Scenario text spec.
2. Scenario DATA spec.
3. Scenario UI spec, when client-visible behavior is being planned.
4. Validation/security addendum entries, when needed.
5. Per-scenario behavior items file.
6. Scenario questions register entries, when unresolved or important questions exist.
7. Scenario clarification file, when accepted clarification resolves source conflict or terminology risk.
8. Diagram-generation prompt, when diagrams are requested from scenario sources.
```

Use the practical workflow:

```text
planning/diagrams/scenario-drafting-workflow.md
```

## 4. Scenario Question Loop

```text
question appears
-> classify it
-> write question status and assumption/current direction
-> if scenario-level, add/update scenario questions register
-> clarify / choose current direction
-> update scenario text spec if behavior changed
-> update DATA file if visible/input/selectable/filter/attachment data changed
-> update scenario UI spec if UI-visible requirement changed
-> update validation/security addendum if needed
-> update behavior items / UI behavior items if required behavior changed
-> update scenario clarifications if accepted terminology/meaning is clarified
-> continue downstream planning
```

Scenario-level means the question affects required behavior, DATA, UI-visible requirement, validation/security, user-visible outcome, scenario semantics or diagram interpretation.

Question fields:

```text
Question status:
Question:
Assumption / current direction:
Impact:
Shared register / local-only reason:
```

Important open/blocked/assumption questions should appear before accepted decisions.

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

They are derived from scenario text, DATA, UI, validation/security and clarification sources.

## 8. DATA

DATA means only what actor enters, sees, selects, filters/searches by, attaches/uploads, or references as visible/selectable business item.

DATA files must not contain validation/rules sections, testable behavior sections, invariants, preconditions, branches, access rules, security policy or layout choices.

## 9. Validation

Scenario specs should distinguish client-side validation from server-side/domain validation.

Use:

```text
planning/scenario-domain-validation-principles.md
```

## 10. Diagram Request Handoff

When the user asks for diagrams from scenario sources, the Scenario Draft Chat may prepare a repo-grounded diagram request/prompt for the single Diagram Chat.

It must use:

```text
planning/diagrams/diagram-prompt-generation-workflow.md
planning/diagrams/drawio-diagram-generation-workflow.md
```

The Scenario Draft Chat must not draw diagrams itself.

The generated prompt must require:

```text
- Phase 1 preflight first;
- reading current scenario/spec folders and actual index files;
- reading scenario text specs, DATA, UI specs, behavior items, questions and clarifications;
- checking API/security addenda;
- checking current implementation evidence enough to avoid overclaiming implementation status;
- using status markers [CORE], [IMPLEMENTED], [DESIGNED], [PLANNED], [DEFERRED], [QUESTION];
- target draw.io XML;
- preferred one multi-page .drawio diagram book;
- no AI/internal workflow wording in final VKR-clean diagrams.
```

## 11. Current Project Decisions

```text
Request statuses: InReview, Approved, Rejected.
Do not use Submitted unless reintroduced later with precise meaning.

Approval does not automatically create agreement proposal.
Agreement proposal starts only by employee action on an Approved request.

Agreement proposal replacement by counterproposal is not ordinary Rejected.
Use superseded/replaced by counterproposal, or SupersededByCounterProposal when a domain state is needed.
Rejected is only for explicit rejection/decline.
```

## 12. Source Of Truth Rule

Use corrected text specs, DATA specs, scenario UI specs, validation addenda, scenario questions register, clarifications and behavior items.

Do not use stale generated diagram package summaries or stale `.drawio` pages as semantic source of truth until regenerated.

## 13. Current Downstream Planning Step

```text
scenario specs / DATA / UI specs / validation
-> behavior items / UI behavior items / questions register / clarifications
-> domain draft / slice boundary / parent slice / client sidecar workflow
-> diagram prompt workflow, when diagrams are requested
```

## 14. Scenario-local Diagram Status Markers

Marker: SCENARIO-STATUS-MARKERS-2026-05

Scenario docs may mark planned/future/deferred elements for diagram and diploma/VKR work using:

```text
[CORE]
[IMPLEMENTED]
[DESIGNED]
[PLANNED]
[DEFERRED]
[QUESTION]
```

Use:

```text
planning/diagrams/scenario-status-marker-rules.md
```

Rules:

```text
- [PLANNED] marks future implementation work in the current plan.
- [DEFERRED] marks extension points outside the current cut.
- [DESIGNED] marks accepted scenario/domain direction.
- [QUESTION] marks unresolved source/contract conflicts.
- [IMPLEMENTED] requires current repo implementation evidence, not only scenario text.
```

Scenario marker sections are diagram-facing planning hints. They do not replace scenario source-of-truth text, slice docs, domain drafts, current OpenAPI/code evidence or implementation verification.

