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
| Rules | Narrow conventions for a specific implementation/styling concern. |
| Template | Copyable shape of a draft, report or section. |
| Draft | Concrete working slice, sidecar or concern document. |

## 4. How To Draft A Slice

Algorithm / process:

```text
planning/slices/slice-draft-authoring-workflow.md
```

General principles and section meaning:

```text
planning/slices/slice-draft-authoring-principles.md
```

Target draft forms:

```text
Server:
  planning/slices/server/SERVER-SLICE-TEMPLATE.md

Client:
  planning/slices/client/CLIENT-SLICE-TEMPLATE.md

Cross-cutting umbrella:
  planning/slices/cross-cutting/cross-cutting-umbrella-template.md
```

Side-specific algorithms:

```text
Server:
  planning/slices/server/SERVER-SLICE-DRAFTING-WORKFLOW.md

Client:
  planning/slices/client/CLIENT-SLICE-DRAFTING-WORKFLOW.md
```

Testing proof:

```text
planning/slices/slice-test-plan-workflow.md
```

Current implementation/status evidence:

```text
planning/documentation/status-reconciliation-workflow.md
```

## 5. Core Navigation / Placement Owners

| Information type | Owner file | Notes |
|---|---|---|
| Slice layer entry/read order | `README.md` | Navigation only. Do not turn it into the full ownership map. |
| Slice-layer placement and responsibility | `slice-responsibility-map.md` | This file. Read before adding new slice-layer information. |
| Concrete catalog of slice docs/files | `SLICE-INDEX.md` | Index/catalog, not placement authority. |
| Current client slice sublayer entry | `client/README.md` | Thin local pointer for client templates/drafts. Reusable client rules live in root `planning/slices/`. |
| Current server slice sublayer entry | `server/README.md` | Thin local pointer for server templates/drafts. Reusable server rules live in root `planning/slices/`. |
| Current cross-cutting umbrella entry | `cross-cutting/README.md` | Cross-cutting coordination docs and paired concern placement. |

## 6. Register Routing Rules

| Information type | Owner register | Notes |
|---|---|---|
| Scenario/source artifact to slice/sidecar mapping | `slice-scenario-flow-behavior-register.md` | Maps scenario text/DATA/UI/behavior sources to slices. It is not the scenario inventory owner and not full version tracking. |
| Active slice questions / decisions | `slice-questions-register.md` | Canonical active slice questions/decisions register. Historical `SLICE-QUESTIONS.md` content was consolidated here. |
| Extension points, change pressure, anti-coupling decisions | `slice-extension-points-register.md` | Use when the item can affect future slices or current implementation seams. |
| Future/current implementation, client or testing notes needing shared visibility | `slice-implementation-notes-register.md` | Use when a note is not safely owned by one active draft only. |

Register rule:

```text
Local slice drafts own detailed local context.
Shared registers own discoverability across future work.
```

Do not duplicate all local prose into registers. Mirror only the items that remain relevant outside the local file.

## 7. Scenario Source Boundary

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

## 8. Principles / Rules Owners

| Principle / rule area | Owner file | Notes |
|---|---|---|
| Slice draft authoring | `slice-draft-authoring-principles.md` | Scope, scenario boundary, behavior coverage, semantic names, implementation drift and section-authoring rules for slice drafts. |
| Draft-driven discovery | `draft-driven-discovery-principles.md` | Current slice-layer discovery loop for business/server/client/cross-cutting slice drafts and slice verification planning. |
| Change/extension points and extension pressure | `change-extension-points-principles.md` | Definitions and rules for change points, extension points, pressure and anti-coupling decisions. |
| Common slice implementation principles | `implementation-principles.md` | Shared implementation rules near slice planning. |
| Server implementation principles | `server-implementation-principles.md` | Backend/server implementation architecture principles for server slice drafts. |
| Client implementation principles | `client-implementation-principles.md` | Client layering, read/command ownership, API wrapper placement and generated type usage. |
| Client CSS architecture rules | `client-css-architecture-rules.md` | Client CSS ownership, forbidden CSS patterns and visual/CSS boundaries. |
| Client form validation implementation principles | `client-form-validation-implementation-principles.md` | How client slices apply deferred validation behavior from the cross-cutting behavior source. |
| Client accessibility / ARIA implementation principles | `client-a11y-implementation-principles.md` | Accessibility/ARIA implementation and user-visible test contract. |
| Client UI/style workflow | `client-ui-style-workflow.md` | Client UI/style implementation process and current visual/navigation direction. |

Do not put specialized client/server/CSS/a11y rules into `slice-draft-authoring-principles.md` if doing so would blur responsibility.

## 9. Workflow Owners

| Workflow area | Owner file | Notes |
|---|---|---|
| Slice draft authoring process | `slice-draft-authoring-workflow.md` | Root workflow for creating, reviewing and refactoring slice drafts before side-specific workflows. |
| Slice Test / Verification Plan | `slice-test-plan-workflow.md` | Practical workflow for Behavior-to-Test Trace and test planning sections. |
| Client slice drafting | `client/CLIENT-SLICE-DRAFTING-WORKFLOW.md` | Client sidecar drafting algorithm. Needs a later audit/update against the current root workflow and client template. |
| Server slice drafting | `server/SERVER-SLICE-DRAFTING-WORKFLOW.md` | Server/backend/API slice drafting algorithm. Needs a later audit/update against the current root workflow and server template. |
| Current implementation / status reconciliation | `planning/documentation/status-reconciliation-workflow.md` | Use when docs status may differ from code/tests/generated artifacts. |

Workflow rule:

```text
Workflow files describe how to perform a repeated task.
They should not be the main owner for layer-wide placement/routing rules.
```

## 10. Template Owners

| Template area | Owner file | Notes |
|---|---|---|
| Client slice draft | `client/CLIENT-SLICE-TEMPLATE.md` | Current canonical template for new `.client.md` drafts; aligned with slice draft authoring workflow and required assertions trace. |
| Server/backend/API slice draft | `server/SERVER-SLICE-TEMPLATE.md` | Current canonical template for server slice drafts; aligned with slice draft authoring workflow and server implementation principles. |
| Cross-cutting umbrella/coordination doc | `cross-cutting/cross-cutting-umbrella-template.md` | Current canonical template for umbrella/coordination docs. |

## 11. Draft Placement Rules

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

## 12. Legacy / Follow-Up Items

These items need later review, but are not canonical owners:

| Item | Current role | Later decision |
|---|---|---|
| `client/CLIENT-SLICE-DRAFTING-WORKFLOW.md` | Local client drafting algorithm | Audit/update after current target files are collected. |
| `server/SERVER-SLICE-DRAFTING-WORKFLOW.md` | Local server drafting algorithm | Audit/update after current target files are collected. |
| Historical L1/L2/root slice files | Legacy draft locations | Keep indexed until migrated or explicitly superseded. |

Removed/superseded artifacts:

```text
SLICE-QUESTIONS.md
SLICE-FOLDER-MAP.md
l1-slice-drafting-guide.md
implemented-slice-sync-workflow.md
IMPLEMENTED-SLICE-SYNC-CHECKLIST.md
IMPLEMENTED-SLICE-SYNC-STATUS-TEMPLATE.md
IMPLEMENTED-SLICE-SYNC-REPORT-TEMPLATE.md
CROSS-CUTTING-UMBRELLA-TEMPLATE.md
```

Deferred cleanup is tracked in:

```text
planning/planning-maintenance-register.md
```

## 13. Future Source / Version Tracking

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

## 14. Conflict Rules

If slice-layer files conflict:

```text
- slice-responsibility-map.md wins for slice-layer placement/routing.
- README.md wins for read order and navigation summary.
- SLICE-INDEX.md wins for concrete file catalog only.
- slice-scenario-flow-behavior-register.md wins for current scenario/source-to-slice mapping.
- slice-questions-register.md wins for active slice questions and decisions.
- slice-draft-authoring-principles.md wins for slice draft section-authoring principles.
- slice-draft-authoring-workflow.md wins for root slice draft authoring process.
- server-implementation-principles.md wins for server implementation principle status within server slice drafting.
- client-implementation-principles.md wins for client layering/API ownership principles.
- client-css-architecture-rules.md wins for client CSS ownership and styling boundaries.
- client-form-validation-implementation-principles.md wins for applying deferred validation behavior in client implementation.
- client-a11y-implementation-principles.md wins for client accessibility/ARIA implementation and test contract.
- server/SERVER-SLICE-TEMPLATE.md wins for server draft output shape.
- client/CLIENT-SLICE-TEMPLATE.md wins for client draft output shape.
- cross-cutting/cross-cutting-umbrella-template.md wins for cross-cutting umbrella output shape.
- status-reconciliation-workflow.md wins for code/test/current-state evidence reconciliation.
- workflow files win for their own process steps.
- scenario layer files win for scenario meaning.
- domain layer files win for domain model meaning.
- testing layer files win for general test-layer principles.
```

## 15. When To Create A New Slice-Layer File

Create a new slice-layer file only when:

```text
- no existing owner file fits;
- the information is reusable or must remain discoverable;
- placing it in an existing file would overload that file;
- the new file has a clear type: workflow, register, template, principle, draft, index, rule or responsibility map;
- README and/or SLICE-INDEX can make it discoverable.
```

Do not create new files for local-only notes that belong inside one active draft.

## 16. Do Not

```text
- Do not recreate SLICE-QUESTIONS.md for active questions.
- Do not create a separate folder map that competes with this responsibility map.
- Do not invent scenario behavior inside slice drafts when scenario/behavior sources exist.
- Do not treat slice-scenario-flow-behavior-register.md as exact source/version tracking.
- Do not create slice-source-usage-register.md until the source/version model is ready.
- Do not hide reusable client/server rules inside subfolders when they belong at slice root.
```

## 17. Success Criteria

Slice layer responsibility is clear when:

```text
- README gives entry/read order;
- this file answers where new slice-layer information belongs;
- SLICE-INDEX catalogs concrete files;
- registers have clear responsibilities;
- reusable principles/rules live in root `planning/slices/`;
- client/server subfolder READMEs are thin local pointers;
- target templates exist for server/client/cross-cutting umbrella drafts;
- scenario inventory remains in scenario layer;
- source/version tracking is honestly marked as future;
- future cleanup is tracked instead of hidden in chat memory.
```
