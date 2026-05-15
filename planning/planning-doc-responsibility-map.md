# Planning Document Responsibility Map

Status: current responsibility map  
Scope: planning file ownership, allowed content, forbidden content, workflow leakage prevention

## 1. Purpose

This file defines what each planning document type is responsible for.

It prevents workflow rules, agent rules and general implementation rules from leaking into local scenario/slice/DATA/UI files.

## 2. Core Rule

A file should contain only content that belongs to its responsibility zone.

If a rule applies to all scenarios, all slices, all client sidecars, all agents, all archives, all ADRs, or the full planning workflow, it belongs in a central workflow/common file.

Local files may contain local coverage, local questions, local decisions and local notes.

## 3. Central Workflow / Common Files

| File | Responsibility | Allowed content | Not allowed |
|---|---|---|---|
| `planning/README.md` | Main planning index and read order | Current entry points, read order, high-level workflow map, links to central docs | Local scenario/slice details |
| `planning/planning-workflow-current.md` | Current active workflow | Current step, next planned steps, workflow gates, slice/client/ADR intake, current sequence | Full scenario details |
| `planning/planning-agent-protocol.md` | AI/chat agent behavior rules | Stop-and-ask rules, relevant questions, assumptions, next-step protocol, extension pressure rule, ADR capture rule | Scenario-specific decisions |
| `planning/planning-doc-responsibility-map.md` | File responsibility map | What each file type owns, allowed/forbidden content, workflow leakage rule | Domain/slice implementation content |
| `planning/scenario-specification-principles.md` | Scenario drafting principles | Scenario specs, UI specs relation, question loop, DATA/validation relation | Concrete component plans |
| `planning/replacement-file-generation-guide.md` | Replacement package rules | Archive/replacement rules | Scenario/slice workflow rules not about packages |

## 4. ADR File Responsibility

### ADR README

File:

```text
planning/adr/README.md
```

Owns ADR folder navigation and high-level distinction between decision notes, candidates and full ADRs.

### ADR workflow

File:

```text
planning/adr/adr-workflow.md
```

Owns:

```text
- decision documentation levels;
- source priority;
- when to create/update notes;
- when to create/update candidates;
- when to promote full ADR;
- agent ADR capture rules.
```

### Architecture decision notes

File:

```text
planning/adr/architecture-decision-notes.md
```

Owns accepted/current architecture decisions that guide planning and implementation.

This is the guiding decision registry until full numbered ADRs are created.

### ADR candidates

File:

```text
planning/adr/adr-candidates.md
```

Owns possible/future full ADR candidates and promotion backlog.

It is not the primary guiding source.

### Full ADRs

Pattern:

```text
planning/adr/ADR-XXXX-title.md
```

Own stable formal architecture decisions.

Do not create full numbered ADRs unless explicitly requested.

## 5. Scenario File Responsibility

Scenario text specs own concrete scenario behavior.

Scenario DATA files own only actor-visible/selectable/input/filter/attachment/reference DATA.

Scenario UI specs own UI-visible requirements, UI behavior items, accepted UI decisions and UI questions.

Scenario behavior item files own derived behavior items for one scenario.

Scenario questions register owns questions that affect scenario behavior, DATA, UI-visible requirement, validation/security, visible outcome and scenario semantics.

## 6. Client Planning Responsibility

`planning/client/` owns client-wide conventions such as accessibility, styling, deferred validation and client/server error mapping.

## 7. Slice File Responsibility

Parent slice files own vertical behavior, API contract, server/cross-layer extension/change points, extension pressure decisions, implementation responsibilities and server/integration tests.

Client sidecar files own detailed client implementation planning for one concrete slice.

`planning/slices/change-extension-points-principles.md` owns change/extension/pressure principles.

`planning/slices/slice-extension-points-register.md` owns cross-slice extension/change/pressure overview and questions.

## 8. Local Content Exception

Local files may keep:

```text
- local coverage tables;
- local question tables;
- local decisions;
- local implementation notes;
- local test coverage;
- local API/client mapping;
- local extension/change/pressure decisions.
```

This is not workflow leakage when the content is local to that file’s subject.

## 9. Responsibility Decision Heuristic

When unsure where content belongs, ask:

```text
1. all planning / all agents -> planning-agent-protocol.md or planning-workflow-current.md
2. read order / navigation -> README.md or local README
3. file type ownership -> planning-doc-responsibility-map.md
4. accepted/current architecture decision -> architecture-decision-notes.md
5. possible future full ADR -> adr-candidates.md
6. scenario behavior -> scenario text spec
7. actor-visible data -> scenario DATA file
8. UI-visible requirement -> scenario UI spec
9. derived behavior item coverage -> scenario behavior item file
10. client-wide convention -> planning/client/cross-cutting/
11. slice boundary -> slice boundary draft
12. one vertical slice -> parent slice file
13. detailed frontend implementation for one slice -> `.client.md`
14. change/extension principles -> change-extension-points-principles.md
15. cross-slice extension/pressure overview -> slice-extension-points-register.md
16. future implementation thought not yet assigned -> slice-implementation-notes-register.md
```
