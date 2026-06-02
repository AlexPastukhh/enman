# Repo Structure Memory

Status: current repo-structure orientation owner  
Doc version: v0.1.0  
Scope: compact memory of known repository areas, documentation layers, source-of-truth chain and read-next guidance for repo-structure orientation commands

This file supports the command family:

```text
вспомни структуру репо
структура репо
вспомни репо
слои репо
где что лежит
repo structure
repo layers
```

It is not a complete audited tree. It is a working orientation map. If a task requires exact current files, verify with GitHub/repo tree or an uploaded archive.

## 0. Source Sync / ROOT-FULL-1

```text
Sources:
  Format/process:
    - planning/source-cascade-sync-workflow.md @ Doc version: v0.5.0
    - planning/SOURCE-SECTION-SOURCES-TEMPLATE.md @ Doc version: v0.2.0
    - planning/source-usage-cascade-profile.md @ Doc version: v0.2.0
  Content:
    - planning/README.md @ Doc version: v0.3.0
    - planning/planning-use-case-map.md @ Doc version: v0.4.0
    - planning/planning-doc-responsibility-map.md @ Doc version: v0.3.0
    - planning/root-source-sync-register.md @ Doc version: v1.0.0
  Internal dependencies:
    - known repository areas
    - source-of-truth chain
    - read-next guidance
  Not checked:
    - full root coverage outside ROOT-FULL-1 scope
    - planned planning/slices/slice-source-sync-register.md does not exist yet
```

ROOT-FULL-1 source pass treats this file as repo-structure orientation owner.
This section records the files that must be checked when this file is created, updated or used as a source for later work.
It does not make the Tampermonkey userscript or any example file a source of truth for command semantics.

## 1. Purpose

Use this file when the chat needs to reorient before planning or editing:

```text
- what root areas exist;
- which planning/docs layers exist;
- where source-of-truth routes live;
- where active workstream state lives;
- where helper implementation lives;
- where app/example code appears to live;
- what must be checked next for the current task.
```

## 2. Known Root Areas

Current known root areas:

```text
planning/
tools/
energymanagement.client/
examples/
```

Known from current workstream evidence:

```text
planning/
  planning docs, workflows, owner files, examples, workstreams and command routing.

tools/
  repo tooling and helper implementation files.

tools/tampermonkey/
  Tampermonkey Chat Command Helper docs and userscript.

energymanagement.client/
  known app/client project area.

examples/
  known example project area, including hospital project client files.
```

Known project-file evidence from search:

```text
energymanagement.client/energymanagement.client.esproj
examples/hospital.proj.client/hospital.proj.client.esproj
```

This does not prove the full app architecture. Read the relevant project files when an app/code task depends on them.

## 3. Documentation / Source-Of-Truth Layers

### 3.1 Root planning layer

```text
planning/README.md
planning/workflow-activation-map.md
planning/planning-doc-responsibility-map.md
planning/planning-use-case-map.md
```

Responsibilities:

```text
planning/README.md
  overview / entry point / source-of-truth map.

planning/workflow-activation-map.md
  workflow activation and preflight behavior.

planning/planning-doc-responsibility-map.md
  root planning layer routing.

planning/planning-use-case-map.md
  concrete command/use-case route table.
```

### 3.2 Reusable workflow / template layer

```text
planning/documentation/
planning/documentation/field-kits/
planning/documentation/examples/
planning/documentation/profiles/
```

Responsibilities:

```text
planning/documentation/
  reusable workflows, templates, output formats and owner files.

planning/documentation/field-kits/
  reusable setup kits.

planning/documentation/examples/
  examples of valid execution; examples do not own command semantics.

planning/documentation/profiles/
  reusable profile-specific setup guidance.
```

### 3.3 Workstream layer

```text
planning/workstreams/
```

Responsibilities:

```text
- living Goal Maps;
- workstream-specific planning files;
- current focus and next action state;
- implementation planning for active long-running goals.
```

Current known workstream files:

```text
planning/workstreams/command-system-and-tampermonkey-goal-map.md
planning/workstreams/tampermonkey-command-projection-plan.md
```

### 3.4 Tooling / implementation layer

```text
tools/tampermonkey/
```

Current known files:

```text
tools/tampermonkey/README.md
tools/tampermonkey/IMPLEMENTATION-NOTES.md
tools/tampermonkey/chat-command-palette.user.js
```

Responsibilities:

```text
README.md
  implementation entrypoint and manual-test orientation.

IMPLEMENTATION-NOTES.md
  current implementation notes, decisions, use cases and caveats.

chat-command-palette.user.js
  actual Tampermonkey userscript skeleton.
```

## 4. Command Source-Of-Truth Chain

When command behavior is in question, use this order:

```text
1. planning/planning-use-case-map.md
2. owner workflow/template files linked from the use-case row
3. reusable examples linked from the route/example index
4. relevant living Goal Map / workstream plan
5. implementation notes or userscript only for helper/UI behavior
```

Do not treat the userscript as command-semantics source of truth.

## 5. Read This For X

| Need | Start with | Then read |
|---|---|---|
| Command route / accepted command behavior | `planning/planning-use-case-map.md` | owner workflow/template from the route |
| Goal Map / progress / current workstream state | `planning/goal-map-principles-workflow-template.md` | relevant file in `planning/workstreams/` |
| Tampermonkey helper command bodies | `planning/workstreams/tampermonkey-command-projection-plan.md` | `tools/tampermonkey/IMPLEMENTATION-NOTES.md` |
| Tampermonkey implementation behavior | `tools/tampermonkey/README.md` | `tools/tampermonkey/IMPLEMENTATION-NOTES.md`, then userscript |
| Deferred or far-future ideas | `planning/deferred-goals-and-ideas.md` | relevant workstream notes |
| File/docs/code/archive update planning | `planning/documentation/file-update-overview-workflow.md` | `planning/planning-use-case-map.md` and target files |
| Replacement archive output | `planning/replacement-file-generation-guide.md` | active plan/scope and target files |
| App/client code | target project file / named area | relevant source files after repo/tree check |
| Example project code | named example project area | relevant source files after repo/tree check |

## 6. Repo Structure Brief Output Shape

Use this output shape for the command:

```text
## Repo Structure Brief

Known root areas:
  ...

Documentation/source-of-truth layers:
  ...

Active workstream files:
  ...

Code/tooling/example areas:
  ...

Known vs uncertain:
  ...

Read next for this task:
  ...
```

## 7. Boundaries

```text
- Do not edit files.
- Do not create an archive.
- Do not commit or push.
- Do not invent root files that were not checked.
- Do not use this memory as proof of complete current repository state.
- If exact structure matters, check GitHub/repo tree or a provided archive.
- If the task touches unknown app/code layers, read target project/source files before planning changes.
```

## 8. Current Known Repo Structure Snapshot

```text
repo root
├─ planning/
│  ├─ planning-use-case-map.md
│  ├─ goal-map-principles-workflow-template.md
│  ├─ goal-map-example.md
│  ├─ deferred-goals-and-ideas.md
│  ├─ repo-structure-memory.md
│  ├─ documentation/
│  │  ├─ reviewable-agent-output-and-commands-workflow.md
│  │  ├─ file-update-overview-workflow.md
│  │  ├─ examples/
│  │  ├─ field-kits/
│  │  └─ profiles/
│  └─ workstreams/
│     ├─ command-system-and-tampermonkey-goal-map.md
│     └─ tampermonkey-command-projection-plan.md
├─ tools/
│  └─ tampermonkey/
│     ├─ README.md
│     ├─ IMPLEMENTATION-NOTES.md
│     └─ chat-command-palette.user.js
├─ energymanagement.client/
└─ examples/
   └─ hospital.proj.client/
```

This snapshot is intentionally compact. Verify exact directories/files before making app/code claims.
