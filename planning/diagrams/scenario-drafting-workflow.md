# Scenario Drafting Workflow

Status: current scenario-drafting workflow  
Doc version: v0.1.0  
Scope: how to create/update scenario text specs, DATA, UI specs, behavior items, scenario questions and diagram requests/prompts

## 1. Purpose

Scenario drafting is a source-work activity.

A scenario draft chat defines user/system behavior and the related source artifacts that downstream domain, slice, client, testing and diagram work consume.

Scenario drafting must keep these artifacts synchronized when relevant:

```text
scenario text spec
inline scenario DATA / reusable DATA concept sidecar
scenario UI spec
validation/security addendum
scenario behavior items
scenario questions register
scenario clarifications
scenario artifact map when currentness or file routing changes
diagram request/prompt, when diagrams are requested
```

## 2. Required Read Order

Before scenario drafting, read:

```text
planning/README.md
planning/planning-agent-protocol.md
planning/agent-roles-and-required-actions.md
planning/scenario-specification-principles.md
planning/scenario-domain-validation-principles.md
planning/diagrams/README.md
planning/diagrams/scenario-responsibility-map.md
planning/diagrams/scenario-artifact-map.md
planning/diagrams/scenario-text-specs/README.md
planning/diagrams/scenario-data/README.md
planning/diagrams/scenario-ui-specs/README.md, if exists
planning/diagrams/scenario-behavior-items/README.md
planning/diagrams/scenario-questions-register.md, if exists
planning/diagrams/scenario-clarifications/README.md, if exists
```

If the scenario work may lead to diagrams, also read:

```text
planning/diagrams/diagram-prompt-generation-workflow.md
planning/diagrams/drawio-diagram-generation-workflow.md
```

## 3. Scenario Artifact Set

For each active scenario, maintain the source set together when relevant:

| Artifact | Responsibility |
|---|---|
| Scenario text spec | User/system behavior, actors, goal, preconditions, main flow, branches, observable outcomes, scenario questions |
| Scenario inline DATA | Data entered, seen, selected, filtered, attached or referenced by actor/system; owned by the scenario text spec by default |
| Reusable DATA concept sidecar | Shared/large/audited scenario DATA concept under `scenario-data/`, only when extraction criteria are met |
| Scenario UI spec | UI-visible requirements and accepted UI decisions, not component implementation |
| Validation/security addendum | Cross-scenario validation/security rules that should not be duplicated into every scenario |
| Scenario behavior items | Required behavior items derived from scenario/source docs |
| Scenario questions register | Open/assumption/future/accepted-important scenario questions visible from one place |
| Scenario clarification | Accepted clarification when sources conflict or terminology must be stabilized |

Do not update only one artifact when the change affects the others.

## 4. Default Scenario Draft Flow

Scenario drafting is iterative, not a strict post-processing sequence.

Use this loop:

```text
1. Identify source request and scenario scope.
2. Classify statement status:
   direct requirement / accepted direction / assumption / open question / future / deferred / stale.
3. Draft or update core business scenario text.
4. Draft/update DATA inline inside the core scenario.
5. Extract reusable DATA sidecar only when extraction criteria are met.
6. Draft/update UI scenario in parallel when visual/UX presentation matters.
7. Derive/update business behavior items from core scenario + inline DATA + reusable DATA concept references.
8. Add UI/UX presentation notes to DATA items only when needed.
9. Add UI/UX projection notes to business behavior items only when needed.
10. Add UI-only behavior entries only when independently presentation/interaction-specific and source-linked.
11. Use DATA gaps and behavior gaps to refine core scenario text.
12. Use UI scenario to check presentation consistency, not to create business behavior silently.
13. Update questions, clarifications, indexes and scenario-artifact-map.md.
14. Record downstream impact for domain/slice/client/testing when relevant.
```

Do not wait until the core scenario is “final” before drafting DATA and behavior items. Inline DATA and behavior items are analysis artifacts used during scenario drafting to expose unclear branches, missing information, weak outcomes and downstream drafting gaps.

Do not create a separate DATA file mechanically for every scenario.

## 5. Scenario Text Spec Rules

Scenario specs describe user-facing behavior and planning semantics.

New or meaningfully updated core/business scenario specs should use:

```text
planning/diagrams/scenario-text-specs/SCENARIO-TEXT-SPEC-TEMPLATE.md
```

They may include:

```text
actor / role
screen or entry point
goal
preconditions
DATA refs
main flow
branches
invariants
observable UI requirements
client-side validation notes
server/domain validation notes
observable outcomes
open questions and assumptions
ADR candidates
```

They must not become:

```text
controller specs
endpoint maps
DB schemas
ORM mappings
React component plans
CSS/layout specs
final aggregate implementation
final visual design
```

## 6. DATA Rules

Scenario DATA starts inline in the core/business scenario text spec.

DATA says only what actor/system:

```text
enters
sees
selects
filters/searches by
attaches/uploads
references as a visible/selectable business item
receives as result/feedback information
```

Use a separate `scenario-data/` sidecar only when:

```text
- the same business data concept appears in multiple scenarios;
- the DATA concept is too large for one scenario text spec;
- the DATA concept needs separate review/audit;
- downstream domain/slice/client/testing drafts need a stable reusable reference;
- an existing sidecar already exists and has not yet been merged/reclassified.
```

DATA sections and DATA sidecars must not contain:

```text
validation/rules sections
testable behavior sections
invariants
preconditions
branches
access rules
security policy
layout choices
component placement
```

If a scenario change adds/removes visible or entered data, update the inline DATA section in the same pass. Update or create a reusable DATA sidecar only when the extraction criteria are met.

Existing `scenario-data/*.md` files remain transitional until reclassified.

## 7. UI Spec Rules

Scenario UI specs capture UI-visible requirements and accepted UI decisions.

They may say:

```text
what user must see
what action must be available
what feedback/status/result must be visible
what forbidden action must be prevented or rejected
what success/error outcome must be visible
```

They must not say:

```text
React component names
hooks
API adapter code
CSS module filenames
final layout implementation
```

Detailed client implementation belongs in `.client.md` sidecars when concrete client work starts.



### Core / DATA / UI Relationship

```text
Core scenario
  owns business capability and business meaning.

DATA
  owns scenario information: entered, seen, selected, attached, referenced or received information.

UI scenario
  owns presentation and UX projection over DATA and business behavior.
```

A UI scenario must stay consistent with the core scenario and DATA. If UI wording appears to introduce new business behavior, record a question, accepted decision, correction or future/deferred extension instead of silently changing core business scope.
## 8. Behavior Items Rule

Behavior items must be derived from scenario/spec sources.

They must not invent behavior.

Correct chain:

```text
scenario text / DATA / UI / validation / security source
        ↓
required behavior item
        ↓
scenario behavior items file
        ↓
used later by slice/domain/client/testing work
```

Behavior items should be specific enough that a slice/client/test planner can trace coverage later.

Implementation mechanics are not behavior items.

Not behavior items:

```text
controller calls handler
repository is invoked
DTO has property
React component lives under features/
OpenAPI generator writes file
```

## 9. Scenario Questions And Assumptions

Every important scenario question must include:

```text
Question status:
Question:
Assumption / current direction:
Impact:
Shared register / local-only reason:
```

Use statuses:

```text
open
blocked
assumption
accepted direction
future review
resolved
superseded
local only
```

Questions order:

```text
1. open questions;
2. blocked questions;
3. assumptions/current directions needing confirmation;
4. unresolved behavior/DATA/UI/security risks;
5. accepted decisions;
6. resolved/superseded history, only when still useful.
```

Assumption rule:

```text
If the scenario draft continues without a final answer, state the assumption clearly so the user can confirm, reject or refine it.
```

Scenario-level questions must be added or updated in:

```text
planning/diagrams/scenario-questions-register.md
```

when they affect required behavior, DATA, UI-visible requirements, validation/security, user-visible outcome, diagrams or scenario semantics.

If an accepted clarification resolves a conflict, update or add a clarification under:

```text
planning/diagrams/scenario-clarifications/
```

## 10. Local / Global Sync For Scenario Work

When a local scenario file changes, check whether shared files must be updated.

| Local change | Shared update |
|---|---|
| New or changed scenario file | scenario text specs README/index |
| New or changed inline DATA section | scenario text spec only; update artifact map when DATA location/currentness changes |
| New or changed reusable DATA sidecar | scenario DATA README/index + scenario artifact map |
| New or changed UI spec | scenario UI specs README/index |
| New required behavior | scenario behavior items file/index |
| Open/assumption/future scenario question | scenario questions register |
| Accepted terminology/meaning clarification | scenario clarifications README + clarification file |
| Diagram-relevant source conflict | scenario questions/clarifications + diagram prompt notes |
| Downstream slice question | slice questions register, if it affects slice work |
| Architecture-wide decision | ADR notes/candidates |

Do not leave a new scenario artifact orphaned.

## 11. Diagram Request Responsibility

When the user asks for diagrams from scenarios, the Scenario Draft Chat may prepare a repo-grounded diagram request/prompt for the single Diagram Chat.

It must:

```text
1. Read diagram-prompt-generation-workflow.md.
2. Read drawio-diagram-generation-workflow.md.
3. Identify current scenario/spec folders and actual index files.
4. Identify source families the Diagram Chat must read.
5. Identify scenario conflicts, clarifications and open questions.
6. Decide recommended diagram batch.
7. Prepare a diagram request/prompt for the Diagram Chat.
8. Require Phase 1 preflight first.
9. Require draw.io XML as target format.
10. Require one multi-page .drawio diagram book as preferred artifact.
11. Require status markers and no overclaiming.
12. Not draw diagrams itself.
```

The diagram prompt must require reading:

```text
planning/README.md
planning/planning-agent-protocol.md
planning/diagrams/README.md
planning/diagrams/diagram-prompt-generation-workflow.md
planning/diagrams/drawio-diagram-generation-workflow.md
planning/diagrams/scenario-text-specs/README.md
planning/diagrams/scenario-data/README.md
planning/diagrams/scenario-ui-specs/README.md, if exists
planning/diagrams/scenario-behavior-items/README.md
planning/diagrams/scenario-questions-register.md, if exists
planning/diagrams/scenario-clarifications/README.md, if exists
```

It must also require the Diagram Chat to inspect enough implementation evidence to avoid overclaiming `[IMPLEMENTED]` status.

## 12. Diagram Request / Prompt Output Skeleton

When asked to prepare a diagram request/prompt, use this structure:

```text
Repository:
Branch:
Role: Diagram Chat
Goal:

Phase 1 only:
- read current repo;
- list sources read;
- list scenario/spec folders and actual indexes;
- identify implementation facts for status markers;
- identify conflicts/stale wording/open questions;
- propose diagram batches;
- do not generate diagrams yet unless explicitly asked.

Required sources:
- ...

Conflict checks:
- ...

Status markers:
[CORE], [IMPLEMENTED], [DESIGNED], [PLANNED], [DEFERRED], [QUESTION]

Target artifact:
draw.io XML, preferably one multi-page .drawio diagram book.

Archive:
complete repo-relative files + MANIFEST.md + APPLY.md.
```

## 13. Downstream Handoff

When scenario drafting reaches a boundary, hand off explicitly:

```text
Boundary reached:
Target role:
Reason:
Scenario facts:
DATA/UI/behavior items updated:
Questions / assumptions:
Recommended next action:
```

Examples:

```text
Scenario Draft Chat -> Domain Draft Chat
Scenario Draft Chat -> Slice Draft Chat
Scenario Draft Chat -> Diagram Chat
Scenario Draft Chat -> Documentation Keeper Chat
```

## 14. Do Not

```text
- Do not write scenario specs from memory only.
- Do not update scenario text without checking DATA/UI/behavior items impact.
- Do not put backend controller mechanics into scenario specs.
- Do not put React implementation into scenario UI specs.
- Do not invent behavior items.
- Do not hide assumptions in prose.
- Do not leave important scenario questions only in local files.
- Do not draw diagrams in the scenario draft chat.
```
