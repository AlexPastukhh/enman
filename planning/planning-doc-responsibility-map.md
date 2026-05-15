# Planning Document Responsibility Map

Status: current responsibility map  
Scope: planning file ownership, allowed content, forbidden content, workflow leakage prevention

## 1. Purpose

This file defines what each planning document type is responsible for.

It prevents workflow rules, agent rules and general implementation rules from leaking into local scenario/slice/DATA files.

## 2. Core Rule

A file should contain only content that belongs to its responsibility zone.

If a rule applies to all scenarios, all slices, all client sidecars, all agents, all archives, or the full planning workflow, it belongs in a central workflow/common file.

Local files may contain local coverage, local questions, local decisions and local notes.

Local files must not become hidden sources of global workflow rules.

## 3. Central Workflow / Common Files

| File | Responsibility | Allowed content | Not allowed |
|---|---|---|---|
| `planning/README.md` | Main planning index and read order | Current entry points, read order, high-level workflow map, links to central docs | Local scenario/slice details, detailed implementation notes |
| `planning/planning-workflow-current.md` | Current active workflow | Current step, next planned steps, workflow gates, slice/client intake, current sequence | Full scenario details, detailed slice implementation |
| `planning/planning-agent-protocol.md` | AI/chat agent behavior rules | Stop-and-ask rules, relevant questions rule, assumptions rule, next-step protocol, archive behavior, no-auto-continue rule | Scenario-specific decisions, client route/component details |
| `planning/planning-doc-responsibility-map.md` | File responsibility map | What each file type owns, allowed/forbidden content, workflow leakage rule, audit protocol | Domain/slice implementation content |
| `planning/scenario-specification-principles.md` | Scenario drafting principles | What belongs in scenario text specs, scenario question loop, DATA/validation relation | Individual scenario flow details except examples |
| `planning/scenario-domain-validation-principles.md` | Validation/domain validation principles | Client vs server/domain validation principles, value/invariant validation guidance | Concrete UI/client implementation details |
| `planning/replacement-file-generation-guide.md` | Replacement package rules | Archive/replacement rules, complete-file generation, apply instructions standard | Scenario/slice workflow rules not about packages |
| `planning/domain-draft-generation-guide.md` | Domain draft generation rules | How to create/refine domain drafts, coverage style, aggregate explanation rules | Client sidecar implementation details |
| `planning/l1-domain-testing-rules.md` | L1 testing rules | Unit/integration testing principles and local examples | Scenario behavior source rules |
| `planning/slices/l1-slice-drafting-guide.md` | Slice/client sidecar drafting rules | Slice discovery, parent slice structure, `.client.md` structure, test sections, sidecar intake | Concrete slice implementation decisions |
| `planning/slices/implementation-principles.md` | Common implementation principles | Parent/API ownership, Client/UI flow, FormValues vs DTO, CSRF, error mapping, tests | Future concrete notes for specific slices |
| `planning/slices/slice-implementation-notes-register.md` | Future concrete implementation notes | Notes that must be checked before starting slices/sidecars | Stable general rules; move those to principles |

## 4. Scenario File Responsibility

### Scenario text specs

Pattern:

```text
planning/diagrams/scenario-text-specs/SC-XX-*.md
```

Own:

```text
- concrete scenario behavior;
- actors;
- goal;
- entry conditions;
- main flow;
- branches;
- observable outcome;
- scenario-local questions;
- mandatory user-visible requirements;
- validation references.
```

Allowed:

```text
- local scenario questions;
- local behavior clarifications;
- local links to DATA/behavior items;
- local observable UI requirements.
```

Not allowed:

```text
- global planning workflow;
- agent protocol;
- replacement archive rules;
- general slice/client sidecar rules;
- detailed React route/component/hook design;
- DB schema or ORM mapping.
```

### Scenario DATA files

Pattern:

```text
planning/diagrams/scenario-data/SC-XX-*.md
```

Own only DATA:

```text
- what actor enters;
- sees;
- selects;
- filters/searches by;
- attaches/uploads;
- references as visible/selectable business item.
```

Not allowed:

```text
- validation rules;
- invariants;
- access policy;
- branches;
- preconditions;
- layout choices;
- general workflow rules.
```

### Scenario behavior item files

Pattern:

```text
planning/diagrams/scenario-behavior-items/SC-XX-*.md
```

Own:

```text
- behavior items owned by one scenario;
- item registry;
- command cards;
- lifecycle/state matrices;
- impossible business states;
- value integrity items;
- use-case coordination items;
- read/integration/future/no-write items;
- questions raised by item migration.
```

Not allowed:

```text
- global behavior item workflow except short links to central guide;
- detailed domain implementation;
- client route/component/hook structure.
```

### Scenario questions register

File:

```text
planning/diagrams/scenario-questions-register.md
```

Own:

```text
- questions that affect scenario behavior;
- DATA;
- validation/security;
- visible outcome;
- scenario semantics.
```

Not allowed:

```text
- pure implementation questions;
- route/component naming questions;
- local helper shape questions.
```

## 5. Slice File Responsibility

### General slice boundary draft

File:

```text
planning/slices/l1-slice-boundary-draft-01.md
```

Own:

```text
- slice discovery;
- slice boundary reasoning;
- Scenario Slice Flow;
- dependency/extension/read/client/plugin/shared-support-assisted slice identification;
- boundary decisions and questions;
- ADR candidates.
```

Not allowed:

```text
- detailed implementation flow;
- detailed client routes/views/hooks;
- code-level implementation steps for one slice.
```

### Parent vertical slice files

Pattern:

```text
planning/slices/SL-XXX.md
```

Own:

```text
- vertical slice behavior;
- Scenario Slice Flow;
- behavior item coverage summary;
- API contract;
- cross-layer implementation flow in summary;
- application/domain/persistence responsibilities;
- server/integration test plan;
- local decisions/questions;
- link to `.client.md` when client work starts.
```

Not allowed:

```text
- full frontend route/view/hook/component implementation once client work is non-trivial;
- global slice drafting rules;
- global agent/workflow rules.
```

### Client sidecar files

Pattern:

```text
planning/slices/SL-XXX.client.md
```

Created only when concrete client work starts.

Own:

```text
- Client Behavior Coverage;
- Client Implementation Questions Register;
- Scenario / DATA Coverage;
- API Contract Used By Client;
- routes;
- views;
- components;
- query functions;
- mutation functions;
- hooks;
- form state;
- FormValues to DTO mapping;
- action availability;
- submit flow;
- success/failure handling;
- cache invalidation;
- client tests.
```

Not allowed:

```text
- inventing a backend API contract that differs from parent slice;
- global client sidecar workflow;
- domain aggregate design unrelated to client behavior.
```

### Shared support files

Pattern:

```text
planning/slices/shared/*.md
```

Own reusable support mechanisms used by multiple slices:

```text
- deferred validation;
- client/server error mapping;
- FormValues to API DTO mapping;
- antiforgery token/session context;
- applicant data prefill notes;
- shared API request helpers.
```

Not allowed:

```text
- concrete full slice behavior;
- scenario source-of-truth changes;
- hidden global workflow rules.
```

## 6. ADR Files

Pattern:

```text
planning/adr/*.md
```

Own:

```text
- architecture decisions;
- decision context;
- alternatives;
- chosen direction;
- consequences;
- related slices/scenarios.
```

Not allowed:

```text
- implementation task lists;
- local test coverage tables;
- current step navigation.
```

## 7. Local Content Exception

Local files may keep local details even if they resemble workflow structure.

Allowed local content:

```text
- local coverage tables;
- local question tables;
- local decisions;
- local implementation notes;
- local test coverage;
- local API/client mapping;
- local scenario/DATA details.
```

This is not workflow leakage when the content is local to that file’s subject.

## 8. Workflow Leakage Rule

Workflow leakage exists when a local file contains rules that apply broadly outside that file.

Examples of leakage:

```text
- a scenario file defines how all slices should be written;
- a DATA README defines global AI agent behavior;
- a slice file defines replacement package rules;
- a behavior item file defines global category policy beyond a local note;
- a client sidecar defines API ownership rules for all sidecars.
```

When found, move the rule to the correct central file and leave only a short link in the local file.

## 9. Responsibility Decision Heuristic

When unsure where content belongs, ask:

```text
1. Does this apply to all planning / all agents?
   -> planning-agent-protocol.md or planning-workflow-current.md

2. Does this define read order / navigation?
   -> README.md or local README if local-only

3. Does this define what a file type owns?
   -> planning-doc-responsibility-map.md

4. Does this define scenario behavior?
   -> scenario text spec

5. Does this define actor-visible data?
   -> scenario DATA file

6. Does this define derived behavior item coverage?
   -> scenario behavior item file

7. Does this define slice boundary?
   -> slice boundary draft

8. Does this define one vertical slice?
   -> parent slice file

9. Does this define detailed frontend implementation for one slice?
   -> .client.md sidecar

10. Does this define reusable support used by many slices?
   -> planning/slices/shared/

11. Does this define future implementation thought not yet assigned?
   -> slice-implementation-notes-register.md
```

## 10. Workflow Centralization Audit Protocol

A future audit should:

```text
1. Scan planning docs.
2. Find global workflow/common rules inside local files.
3. Classify each rule using this responsibility map.
4. Move rules to central files.
5. Leave local files with local content and links to central rules.
6. Do not move local coverage/questions/decisions out of local files.
```

## 11. Relevant Questions Rule For Responsibility Work

Before moving or rewriting responsibility-related content, ask only questions that affect the current responsibility decision.

For each relevant question, include the agent’s assumption/preferred answer.

Example:

```text
Question:
Should replacement archive rules be included in the responsibility map?

Assumption:
Yes. They have a distinct file owner: replacement-file-generation-guide.md.
```

Do not ask future implementation questions while doing responsibility-map work.
