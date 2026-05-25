# Slice Responsibility Map

Status: current local responsibility map for slice layer  
Scope: routes slice-layer information to the correct owner file, register, workflow, template or draft

## 1. Purpose

Use this file after you already know that new information belongs to the slice layer.

This file answers:

```text
Where inside planning/slices/ should this information go?
Which slice-layer file owns this rule, workflow, register entry, template or draft?
Which slice-layer files must be checked or synchronized after the change?
```

This file does not own scenario inventory, scenario text meaning, DATA definitions, behavior item meaning, domain model truth, API contract rules or testing-layer principles. It only routes slice-layer information.

Root layer routing lives in:

```text
planning/planning-doc-responsibility-map.md
```

## 2. Authority

Global planning-docs architecture theory:

```text
planning/documentation/planning-docs-architecture-principles.md
```

Root layer routing:

```text
planning/planning-doc-responsibility-map.md
```

Slice layer navigation:

```text
planning/slices/README.md
```

This file owns slice-layer placement and responsibility routing.

## 3. Slice Layer File Type Model

| File type | Responsibility in slice layer |
|---|---|
| README | Entry point, read order and short layer overview. |
| Responsibility map | Where new slice-layer information belongs and which file owns what. |
| Index | Concrete catalog of existing slice docs, workflows, templates, registers and drafts. |
| Register | Shared state that must be discoverable across slice files. |
| Source mapping register | Mapping from scenario/source artifacts to slice drafts or sidecars. |
| Workflow | Repeatable process for slice drafting, testing or synchronization. |
| Principles | Stable rules and reasoning principles used by slice work. |
| Template | Copyable shape of a draft, report or section. |
| Draft | Concrete working slice, sidecar or concern document. |
| Checklist | Transitional helper only when it does not duplicate workflow. |

## 4. Core Navigation / Placement Owners

| Information type | Owner file | Notes |
|---|---|---|
| Slice layer entry/read order | `README.md` | Navigation only. Do not turn it into the full ownership map. |
| Slice-layer placement and responsibility | `slice-responsibility-map.md` | This file. Read before adding new slice-layer information. |
| Concrete catalog of slice docs/files | `SLICE-INDEX.md` | Index/catalog, not placement authority. |
| Folder placement / migration paths | `slice-responsibility-map.md` now; `SLICE-FOLDER-MAP.md` transitional | Fold useful placement rules into this map over time. |
| Current client slice sublayer entry | `client/README.md` | Client sublayer read order and placement inside `client/`. |
| Current server slice sublayer entry | `server/README.md` | Server sublayer read order and placement inside `server/`. |
| Current cross-cutting umbrella entry | `cross-cutting/README.md` | Cross-cutting coordination docs and paired concern placement. |

## 5. Register Routing Rules

| Information type | Owner register | Notes |
|---|---|---|
| Scenario/source artifact to slice/sidecar mapping | `slice-scenario-flow-behavior-register.md` | Maps scenario text/DATA/UI/behavior sources to slices. It is not the scenario inventory owner and not full version tracking. |
| Active slice questions / decisions | `slice-questions-register.md` | Canonical active slice questions/decisions register candidate. |
| Legacy or taxonomy-level slice questions | `SLICE-QUESTIONS.md` transitional | Must be audited/merged/superseded later. Do not add new active questions here by default. |
| Extension points, change pressure, anti-coupling decisions | `slice-extension-points-register.md` | Use when the item can affect future slices or current implementation seams. |
| Future/current implementation, client or testing notes needing shared visibility | `slice-implementation-notes-register.md` | Use when a note is not safely owned by one active draft only. |

Register rule:

```text
Local slice drafts own detailed local context.
Shared registers own discoverability across future work.
```

Do not duplicate all local prose into registers. Mirror only the items that remain relevant outside the local file.

## 6. Scenario Source Boundary

Scenario inventory belongs to the scenario layer, not the slice layer.

Slice layer owns only this question:

```text
Which scenario/source artifacts does this slice or sidecar consume?
```

Current owner:

```text
planning/slices/slice-scenario-flow-behavior-register.md
```

Future exact source/version usage may need a separate source usage register, but it does not exist yet.

## 7. Principles Owners

| Principle area | Owner file | Notes |
|---|---|---|
| Draft-driven discovery | `draft-driven-discovery-principles.md` | Current practical principle, but scope is broader than slice layer and needs future decision. |
| Change/extension points and extension pressure | `change-extension-points-principles.md` | Definitions and rules for change points, extension points, pressure and anti-coupling decisions. |
| Common slice implementation principles | `implementation-principles.md` | Shared implementation rules near slice planning. |

Do not put long workflow algorithms into principles files unless they are high-level principles.

## 8. Workflow Owners

| Workflow area | Owner file | Notes |
|---|---|---|
| Slice Test / Verification Plan | `slice-test-plan-workflow.md` | Practical workflow for Behavior-to-Test Trace and test planning sections. |
| Client slice drafting | `client/CLIENT-SLICE-DRAFTING-WORKFLOW.md` | Client sidecar and client slice drafting process. |
| Server slice drafting | `server/SERVER-SLICE-DRAFTING-WORKFLOW.md` | Server/backend/API slice drafting process. |
| Implemented slice draft sync | `implemented-slice-sync-workflow.md` transitional | Useful but not fully canonical until refactored under the new source/dependency model. |
| Client UI/style/form/a11y workflows | `client/*-WORKFLOW.md` | Client-specific workflows for UI, style, form validation, accessibility and implementation handoff. |

Workflow rule:

```text
Workflow files describe how to perform a repeated task.
They should not be the main owner for layer-wide placement/routing rules.
```

## 9. Template Owners

| Template area | Owner file | Notes |
|---|---|---|
| Client slice draft | `client/CLIENT-SLICE-TEMPLATE.md` | Current canonical template for new `.client.md` drafts. |
| Server/backend/API slice draft | `server/SERVER-SLICE-TEMPLATE.md` | Current canonical template for server slice drafts. |
| Cross-cutting umbrella/coordination doc | `cross-cutting/CROSS-CUTTING-UMBRELLA-TEMPLATE.md` | Current canonical template for umbrella docs. |
| Implemented sync status block | `IMPLEMENTED-SLICE-SYNC-STATUS-TEMPLATE.md` transitional | Decide later whether to keep, fold into workflow or update under new source model. |
| Implemented sync report | `IMPLEMENTED-SLICE-SYNC-REPORT-TEMPLATE.md` transitional | Decide later whether to keep or fold into implemented sync workflow / reviewable output model. |

Current client/server/cross-cutting templates stay as-is until a dedicated template update task.

## 10. Draft Placement Rules

New slice docs are grouped by implementation responsibility, not by old L1/L2 level.

| Draft type | Target location |
|---|---|
| Client slice drafts / sidecars | `planning/slices/client/` |
| Client cross-cutting implementation drafts | `planning/slices/client/cross-cutting/` |
| Server/backend/API slice drafts | `planning/slices/server/` |
| Server cross-cutting implementation drafts | `planning/slices/server/cross-cutting/` |
| Cross-cutting paired concern umbrella docs | `planning/slices/cross-cutting/` |
| Historical L1/L2/root slice files | Existing legacy paths only until migrated |

Rules:

```text
- Do not create new `planning/slices/l1` or `planning/slices/l2` files.
- Do not mass-move historical drafts without a dedicated cleanup task.
- Keep `SLICE-INDEX.md` updated while old and new paths coexist.
- Use `SINGLE-` prefix when a slice draft is intentionally one-sided.
- Use paired logical IDs with `.client.md` / `.server.md` when a counterpart may exist.
```

## 11. Transitional Files

These files are intentionally kept but need later cleanup:

| File | Current role | Later decision |
|---|---|---|
| `SLICE-FOLDER-MAP.md` | Transitional folder placement map | Fold into this responsibility map, then reduce to pointer or supersede. |
| `SLICE-QUESTIONS.md` | Transitional/legacy questions or taxonomy decision log | Merge active items into `slice-questions-register.md` or mark superseded/historical. |
| `l1-slice-drafting-guide.md` | Transitional/general slice drafting guide | Keep for now; later simplify or migrate useful short-draft guidance. |
| `implemented-slice-sync-workflow.md` | Transitional implemented sync workflow | Refactor to current source/dependency/status model before treating as canonical. |
| `IMPLEMENTED-SLICE-SYNC-CHECKLIST.md` | Transitional checklist | Fold useful checks into workflow or supersede. |
| `IMPLEMENTED-SLICE-SYNC-STATUS-TEMPLATE.md` | Transitional template | Update or fold into implemented sync workflow/template model. |
| `IMPLEMENTED-SLICE-SYNC-REPORT-TEMPLATE.md` | Transitional template | Update or fold into implemented sync workflow/reviewable output model. |

Deferred cleanup is tracked in:

```text
planning/planning-maintenance-register.md
```

## 12. Future Source / Version Tracking

Current source mapping owner:

```text
planning/slices/slice-scenario-flow-behavior-register.md
```

Future possible owner:

```text
planning/slices/slice-source-usage-register.md
```

Do not create a full source/version register yet.

Create it only after:

```text
- source/dependency model is clear;
- at least one source usage register pilot exists;
- source/version cascade sync workflow is designed;
- scenario/domain/slice local responsibility maps are stable enough.
```

Future source usage fields may include:

```text
consumer file
source file path
content_version
reviewed_against / derived_from
sync_status
last_reviewed
notes
```

## 13. Conflict Rules

If slice-layer files conflict:

```text
- slice-responsibility-map.md wins for slice-layer placement/routing.
- README.md wins for read order and navigation summary.
- SLICE-INDEX.md wins for concrete file catalog only.
- slice-scenario-flow-behavior-register.md wins for current scenario/source-to-slice mapping.
- slice-questions-register.md should be treated as the active questions register unless SLICE-QUESTIONS.md is explicitly being audited.
- workflow files win for their own process steps.
- template files win for output/draft shape.
- scenario layer files win for scenario meaning.
- domain layer files win for domain model meaning.
- testing layer files win for general test-layer principles.
```

## 14. When To Create A New Slice-Layer File

Create a new slice-layer file only when:

```text
- no existing owner file fits;
- the information is reusable or must remain discoverable;
- placing it in an existing file would overload that file;
- the new file has a clear type: workflow, register, template, principle, draft, index or responsibility map;
- README and/or SLICE-INDEX can make it discoverable.
```

Do not create new files for local-only notes that belong inside one active draft.

## 15. Do Not

```text
- Do not put active new slice questions into SLICE-QUESTIONS.md by default.
- Do not use SLICE-FOLDER-MAP.md as the long-term placement authority once this map exists.
- Do not invent scenario behavior inside slice drafts when scenario/behavior sources exist.
- Do not treat slice-scenario-flow-behavior-register.md as exact source/version tracking.
- Do not delete transitional files until useful content is migrated or explicitly superseded.
- Do not update templates as part of basic responsibility-map creation.
- Do not create slice-source-usage-register.md until the source/version model is ready.
```

## 16. Success Criteria

Slice layer responsibility is clear when:

```text
- README gives entry/read order;
- this file answers where new slice-layer information belongs;
- SLICE-INDEX catalogs concrete files;
- registers have clear responsibilities;
- transitional files are visible and not mistaken for canonical owners;
- scenario inventory remains in scenario layer;
- source/version tracking is honestly marked as future;
- future cleanup is tracked instead of hidden in chat memory.
```
