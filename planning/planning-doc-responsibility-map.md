# Planning Document Responsibility Map

Status: current responsibility map  
Scope: planning file ownership, allowed content, forbidden content, workflow leakage prevention

## 1. Purpose

This file defines what each planning document type is responsible for.

## 2. Core Rule

A file should contain only content that belongs to its responsibility zone.

If a rule applies to all scenarios, all slices, all client sidecars, all agents, all archives, or the full planning workflow, it belongs in a central workflow/common file.

Local files may contain local coverage, local questions, local decisions and local notes.

## 3. Central Workflow / Common Files

| File | Responsibility | Allowed content | Not allowed |
|---|---|---|---|
| `planning/README.md` | Main planning index and read order | Current entry points, read order, high-level workflow map, links to central docs | Local scenario/slice details |
| `planning/planning-workflow-current.md` | Current active workflow | Current step, next planned steps, workflow gates, slice/client intake | Full scenario details |
| `planning/planning-agent-protocol.md` | AI/chat agent behavior rules | Stop-and-ask, relevant questions, assumptions, next-step, extension pressure rule | Scenario-specific decisions |
| `planning/planning-doc-responsibility-map.md` | File responsibility map | File type ownership, allowed/forbidden content | Domain/slice implementation content |
| `planning/scenario-specification-principles.md` | Scenario drafting principles | Scenario specs, UI specs relation, question loop, DATA/validation relation | Concrete component plans |
| `planning/replacement-file-generation-guide.md` | Replacement package rules | Archive/replacement rules | Scenario/slice workflow rules not about packages |
| `planning/slices/l1-slice-drafting-guide.md` | Slice/client sidecar drafting rules | Slice discovery, parent slice template, `.client.md` template, sidecar intake | Concrete slice decisions |
| `planning/slices/implementation-principles.md` | Common implementation principles | Parent/API ownership, Client/UI flow, FormValues vs DTO, CSRF, tests, change/extension references | Future concrete notes |
| `planning/slices/client-architecture-principles.md` | Client architecture mapping | app/pages/entities/features/widgets/shared responsibilities | Concrete slice-specific client plan |
| `planning/slices/client-component-discovery-guide.md` | Component discovery workflow | Component/layout planning questions, placement, accessibility/test prompts | Concrete component implementation |
| `planning/slices/change-extension-points-principles.md` | Change/extension/pressure principles | Definitions, trade-offs, parent/client sections, implementation questions | Local slice decisions |
| `planning/slices/slice-extension-points-register.md` | Cross-slice extension/change/pressure register | Global coverage, decisions, questions across slices | Detailed local implementation flow |

## 4. Scenario File Responsibility

### Scenario text specs

Own concrete scenario behavior, actors, goals, flows, branches, observable outcomes, mandatory user-visible behavior and scenario-local questions.

Not allowed: React component plans, route/hook layout, CSS/module details, global workflow rules.

### Scenario DATA files

Own only actor-visible/selectable/input/filter/attachment/reference DATA.

Not allowed: validation rules, invariants, access policy, layout choices, component decisions.

### Scenario UI specs

Pattern:

```text
planning/diagrams/scenario-ui-specs/SC-XX-*.md
```

Own UI-visible requirements, UI behavior items, accepted UI decisions for one scenario, UI state/feedback expectations and UI questions.

Not allowed: detailed React implementation, concrete file structure, hook/API placement, final visual design.

### Scenario behavior item files

Own derived behavior items for one scenario.

### Scenario questions register

Own questions that affect scenario behavior, DATA, UI-visible requirement, validation/security, visible outcome and scenario semantics.

## 5. Client Planning Responsibility

`planning/client/` and `planning/client/cross-cutting/` own client-wide conventions: accessibility, styling, deferred validation, client/server error mapping, and client-wide UI behavior conventions.

They do not own concrete slice implementation, backend domain/API rules, or scenario source-of-truth changes.

## 6. Slice File Responsibility

### Parent vertical slice files

Own vertical slice behavior, Scenario Slice Flow, behavior item coverage, API contract, server/cross-layer extension points, server/cross-layer change points, extension pressure / anti-coupling decisions, application/domain/persistence responsibilities, server/integration tests and local questions/decisions.

### Client sidecar files

Created only when concrete client work starts.

Own Client Behavior Coverage, UI Behavior Coverage, Client Implementation Questions Register, Scenario / DATA / UI Spec Coverage, Client Architecture Mapping, Client Extension Points, Client Behavior Change Points, Client Extension Pressure / Anti-Coupling Decisions, Component / Layout Plan, Styling Change Points, Accessibility / ARIA Contract, API Contract Used By Client, detailed client implementation flow and client tests.

### Shared support files

`planning/slices/shared/*.md` own slice-near reusable support notes.

If a convention is client-wide, prefer `planning/client/cross-cutting/`.

## 7. Local Content Exception

Local files may keep local coverage tables, local question tables, local decisions, local implementation notes, local test coverage, local API/client mapping, and local extension/change/pressure decisions.

## 8. Responsibility Decision Heuristic

```text
1. all planning / all agents -> planning-agent-protocol.md or planning-workflow-current.md
2. read order / navigation -> README.md or local README
3. file type ownership -> planning-doc-responsibility-map.md
4. scenario behavior -> scenario text spec
5. actor-visible data -> scenario DATA file
6. UI-visible requirement -> scenario UI spec
7. derived behavior item coverage -> scenario behavior item file
8. client-wide convention -> planning/client/cross-cutting/
9. slice boundary -> slice boundary draft
10. one vertical slice -> parent slice file
11. detailed frontend implementation for one slice -> `.client.md`
12. client architecture mapping for all sidecars -> client-architecture-principles.md
13. component discovery workflow -> client-component-discovery-guide.md
14. change/extension principles -> change-extension-points-principles.md
15. cross-slice extension/pressure overview -> slice-extension-points-register.md
16. future implementation thought not yet assigned -> slice-implementation-notes-register.md
```
