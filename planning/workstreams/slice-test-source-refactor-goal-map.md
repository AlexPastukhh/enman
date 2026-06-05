# Slice/Test Source Refactor Goal Map

Status: active local workstream Goal Map / pre-refactor audit map
Doc version: v0.1.0
Owner format: `planning/goal-map-principles-workflow-template.md`
Scope: local living map for slice/testing source-register refactor after slice/testing skeleton registers and owner workflow/template versions are seeded

This is a living workstream map. Update it when slice/test refactor status, audit results, pilot selection, source-register evidence or next action changes.

This file does not own slice drafting semantics, testing rules, source truth, command routing, output modes or permission boundaries. It records the local refactor path and points to the owner files.

Owner / routing files:

```text
planning/goal-map-principles-workflow-template.md
planning/planning-use-case-map.md
planning/source-cascade-sync-workflow.md
planning/slices/slice-source-sync-register.md
planning/testing/testing-source-sync-register.md
```

## 0. Source Sync / SLICE-REF-GM-0

```text
Sources:
  Format/process:
    - planning/goal-map-principles-workflow-template.md @ Doc version: v0.1.0
    - planning/planning-use-case-map.md @ Doc version: v0.8.0
    - planning/source-cascade-sync-workflow.md @ Doc version: v1.0.0
    - planning/SOURCE-SECTION-SOURCES-TEMPLATE.md @ Doc version: v0.2.0
    - planning/slices/slice-source-sync-register.md @ Doc version: v0.2.0 / skeleton
    - planning/testing/testing-source-sync-register.md @ Doc version: v0.2.0 / skeleton
  Content:
    - planning/domain/domain-source-sync-register.md @ Doc version: v0.2.0
    - planning/slices/slice-draft-authoring-workflow.md @ Doc version: v0.1.0
    - planning/slices/slice-draft-authoring-principles.md @ Doc version: v0.1.0
    - planning/slices/server/SERVER-SLICE-DRAFTING-WORKFLOW.md @ Doc version: v0.1.0
    - planning/slices/server/SERVER-SLICE-TEMPLATE.md @ Doc version: v0.1.0
    - planning/slices/client/CLIENT-SLICE-DRAFTING-WORKFLOW.md @ Doc version: v0.1.0
    - planning/slices/client/CLIENT-SLICE-TEMPLATE.md @ Doc version: v0.1.0
    - planning/slices/slice-test-plan-workflow.md @ Doc version: v0.1.0
    - planning/testing/testing-principles.md @ Doc version: v0.1.0
    - planning/testing/server-slice-test-plan-rules.md @ Doc version: v0.1.0
    - planning/root-source-sync-register.md @ Doc version: v1.8.0
  Internal dependencies:
    - Current Snapshot
    - Roadmap / Work Chain
    - Audit Summary
    - Gap Categories
    - Pilot Candidate Decision
  Not checked:
    - concrete slice drafts were not edited in SLICE-REF-GM-0
    - per-slice local section Sources blocks are not added yet
    - concrete slice/test coverage remains unsynchronized
    - support owner docs beyond the 0B version-seeded core remain conditional per-slice preflight inputs
```

SLICE-REF-GM-0 creates this local Goal Map after the slice/testing skeleton registers and owner workflow/template versions exist. The map is an audit/refactor navigator only. It does not claim that concrete slice drafts or test plans are synchronized.

## 0. Current Snapshot

Last updated:
  2026-06-05 / after SLICE-TEST-SRC-0B owner workflow/template version seed and SLICE-REF-GM-0 map sync

Current goal:
  Prepare a controlled one-slice-at-a-time refactor of slice/testing planning files using source-sync registers, versioned owner workflows/templates and local section/file-level source evidence.

Current focus:
  SLICE-REF-GM-0 — audit slice/test files and keep a local refactor Goal Map before the first concrete slice refactor.

Active slice:
  SLICE-REF-GM-0 — Audit And Local Goal Map Before First Slice Refactor
    Status: ▶ NOW / map created, full per-file card audit still pending

Already available:
  - CASCADE-SRC-1A source-impact / targeted cascade foundation is done.
  - CASCADE-ROUTE-1B root route ownership and example-fit cleanup is done.
  - SLICE-TEST-SRC-0A created slice/testing source-sync skeleton registers.
  - SLICE-TEST-SRC-0B seeded active slice/testing owner workflow/template/rule versions.
  - `planning/slices/slice-source-sync-register.md` exists as skeleton / incomplete / not synchronized.
  - `planning/testing/testing-source-sync-register.md` exists as skeleton / incomplete / not synchronized.
  - Active server/client slice templates and slice/test-plan owner workflows have Doc versions.

Latest completed:
  - Created slice/testing source-sync skeleton registers.
  - Seeded active slice/testing owner workflow/template/rule versions.
  - Verified that concrete slice drafts still need per-file audit/refactor and should not be treated as synchronized.

Now:
  - Keep this local map current.
  - Use it to drive the full slice/test file audit.
  - Classify gaps before choosing the first concrete pilot slice.

Next action:
  Run SLICE-REF-GM-0 audit pass:
    collect per-file cards for concrete slice/test artifacts, compare them with the target server/client/testing models, then choose the first pilot slice.

Recommended next action:
  Do not start `SLICE-PILOT-1` until the audit cards and pilot decision are recorded in this map.

Open decisions:
  - DEC-1: First pilot slice: safer structural pilot vs real cascade pilot.
  - DEC-2: Whether support owner docs beyond the 0B core should be versioned globally or only when a chosen slice consumes them.
  - DEC-3: Exact grouping for legacy L1/L2, cross-cutting and example/prompt slice artifacts.

Update rule:
  Update this map after every meaningful slice/test audit batch, pilot decision, per-slice refactor, source-register row change or next-action change.

Planning rule:
  When planning inside the slice/test refactor workstream, read this map first, then use the slice/testing registers and versioned owner docs to choose the next step.

## 1. Roadmap / Work Chain

Status labels:

```text
✅ DONE     completed and backed by visible evidence
▶ NOW      active work
⏭ NEXT     next planned work
⬜ PLANNED  planned but not active
⬜ DEFERRED deferred / not part of current workstream
⚠ BLOCKED  blocked or needs a decision
```

| Step | Goal | Evidence / current meaning | Status |
|---|---|---|---|
| CASCADE-SRC-1A | Establish targeted source-impact and avoid fake broad cascades. | Route/read/navigation links are not source/version dependencies by default. | ✅ DONE |
| CASCADE-ROUTE-1B | Clean root route ownership and example-fit rules before slice/testing work. | UCM owns concrete command/action routing; WAM is activation/read-order helper; example-fit confirmation exists. | ✅ DONE |
| SLICE-TEST-SRC-0A | Create slice/testing source-sync register skeletons. | Slice/testing registers exist as skeleton / incomplete / not synchronized. | ✅ DONE |
| SLICE-TEST-SRC-0B | Seed active slice/testing owner workflow/template/rule versions. | Core slice/testing owner docs are versioned at v0.1.0 and registers know the owner-version seed exists. | ✅ DONE |
| SLICE-REF-GM-0 | Audit all slice/test files and maintain this local refactor Goal Map. | This map exists; per-file audit cards and pilot decision are pending. | ▶ NOW |
| SLICE-PILOT-1 | Refactor the first concrete slice with local Sources/source-register evidence. | Not started; wait for audit cards and pilot decision. | ⏭ NEXT |
| Per-slice batches | Continue refactor one slice at a time. | Not started. | ⬜ PLANNED |
| Command-system/Tampermonkey expansion | Command helper evolution outside slice/test source refactor. | Parked while slice/testing source work is active. | ⬜ DEFERRED |

## 2. Source Of Truth For Target Model

Core source/register model:

| Purpose | Source |
|---|---|
| Source cascade workflow | `planning/source-cascade-sync-workflow.md @ Doc version: v1.0.0` |
| Generic local Sources block shape | `planning/SOURCE-SECTION-SOURCES-TEMPLATE.md @ Doc version: v0.2.0` |
| Slice source-sync register | `planning/slices/slice-source-sync-register.md @ Doc version: v0.2.0 / skeleton` |
| Testing source-sync register | `planning/testing/testing-source-sync-register.md @ Doc version: v0.2.0 / skeleton` |
| Domain upstream dependencies | `planning/domain/domain-source-sync-register.md @ Doc version: v0.2.0` |

Slice owner docs:

| Purpose | Source |
|---|---|
| Root slice authoring workflow | `planning/slices/slice-draft-authoring-workflow.md @ Doc version: v0.1.0` |
| Slice authoring principles | `planning/slices/slice-draft-authoring-principles.md @ Doc version: v0.1.0` |
| Server slice workflow | `planning/slices/server/SERVER-SLICE-DRAFTING-WORKFLOW.md @ Doc version: v0.1.0` |
| Server slice template | `planning/slices/server/SERVER-SLICE-TEMPLATE.md @ Doc version: v0.1.0` |
| Client sidecar workflow | `planning/slices/client/CLIENT-SLICE-DRAFTING-WORKFLOW.md @ Doc version: v0.1.0` |
| Client sidecar template | `planning/slices/client/CLIENT-SLICE-TEMPLATE.md @ Doc version: v0.1.0` |
| Slice Test / Verification Plan workflow | `planning/slices/slice-test-plan-workflow.md @ Doc version: v0.1.0` |

Testing owner docs:

| Purpose | Source |
|---|---|
| Testing layer responsibility | `planning/testing/testing-responsibility-map.md @ Doc version: v0.1.0` |
| Testing principles | `planning/testing/testing-principles.md @ Doc version: v0.1.0` |
| Server/API slice test plan rules | `planning/testing/server-slice-test-plan-rules.md @ Doc version: v0.1.0` |

Conditional support-owner docs:

```text
The following support docs are not globally version-seeded in SLICE-TEST-SRC-0B.
Treat them as conditional per-slice preflight sources when a chosen slice consumes them:

- planning/slices/server-implementation-principles.md
- planning/slices/client-implementation-principles.md
- planning/slices/client-css-architecture-rules.md
- planning/slices/client-form-validation-implementation-principles.md
- planning/slices/client-a11y-implementation-principles.md
- planning/slices/client-ui-style-workflow.md
- planning/testing/e2e-testing-workflow.md
- planning/testing/test-object-patterns.md
```

## 3. Target Model Summary

A refactored server/client slice draft should eventually have:

```text
- Doc version;
- `## 0. Scenario Sources / Source Sync`;
- section-level `Sources:` blocks where the section depends on separately reviewed sources;
- explicit scenario/client/server/domain/testing boundaries;
- behavior coverage;
- Test / Verification Plan with Behavior-to-Test Trace when applicable;
- Local / Global Sync Check;
- Guardrail Summary / Next Step;
- corresponding slice/testing register rows after local/file-level source review.
```

Do not mark a concrete slice as synchronized until the slice file local Sources or a documented file-level audit were checked against the register.

## 4. Audit Summary / Current Layer State

Preliminary archive-level inventory before per-file cards:

| Area | Current observation |
|---|---|
| Concrete-ish slice artifacts | about 80 filename/path-based candidates need classification |
| Concrete slice `Doc version:` | missing across checked candidates |
| Target `Scenario Sources / Source Sync` heading | absent across checked candidates |
| Legacy `Scenario Sources` heading | present in part of the set |
| Section-level Sources | essentially absent from concrete slice drafts |
| Local / Global Sync Check | absent across checked candidates |
| Behavior-to-Test Trace | present in some drafts, but not normalized to target source model |
| Test / Verification Plan | present in many drafts, but not yet synchronized with testing register |

This is an audit starting point, not final per-file evidence. SLICE-REF-GM-0 must produce concrete file cards before the first slice refactor.

## 5. Gap Categories

| Gap ID | Meaning | Default handling |
|---|---|---|
| GAP-1 | Missing `Doc version:` in concrete slice draft | Add only during that slice's individual refactor batch. |
| GAP-2 | Missing target `Scenario Sources / Source Sync` block | Replace/upgrade legacy source section during per-slice refactor. |
| GAP-3 | Missing section-level `Sources:` blocks | Add only where a section consumes separately reviewed sources. |
| GAP-4 | Missing `Local / Global Sync Check` | Add during per-slice refactor. |
| GAP-5 | Partial or missing Behavior-to-Test Trace | Normalize with `slice-test-plan-workflow.md` and testing register when in scope. |
| GAP-6 | Legacy L1/L2 path/status | Classify before deciding whether it is active, legacy, example or prompt-only. |
| GAP-7 | Cross-cutting slice classification needed | Classify CC files separately from server/client drafts. |
| GAP-8 | Support owner docs unversioned | Treat as conditional preflight sources per slice type. |
| GAP-9 | E2E/object-pattern owner dependency unclear | Version or mark as `version not confirmed` only when selected slice consumes these. |
| GAP-10 | Implementation/evidence claims need reconciliation | Use status reconciliation before turning implementation claims into proof. |

## 6. Pilot Candidate Decision

Do not start a pilot until per-file cards exist. Current candidates:

| Candidate | Why it is useful | Risk |
|---|---|---|
| `planning/slices/client/SL-AUTH-ACT-001-email-activation-ui-and-protected-gate.client.md` | Safer structural client sidecar pilot; likely closer to target shape. | May not exercise the full scenario/domain/slice/testing cascade. |
| `planning/slices/SL-AGR-EXCH-001-start-agreement-exchange-with-initial-employee-proposal.md` | Real cascade pilot through AgreementProposalExchange / scenario-domain-slice-testing chain. | Bigger, likely needs more upstream source checks and related sidecar review. |

Preferred decision rule:

```text
1. Build per-file audit cards for both candidates.
2. If the cascade candidate has manageable source gaps, choose it as SLICE-PILOT-1.
3. If not, run the safer client sidecar pilot first to validate target shape and register process.
```

## 7. Per-File Audit Card Template

Use this shape during SLICE-REF-GM-0:

| Field | Value |
|---|---|
| File | `<path>` |
| Type | `server / client / cross-cutting / legacy / example / prompt-only / testing` |
| Current status | `<active / legacy / candidate / unknown>` |
| Target template | `<server/client/testing/none>` |
| Doc version | `<present/missing>` |
| Source Sync block | `<target/legacy/missing>` |
| Section-level Sources | `<present/missing/not needed yet>` |
| Testing trace | `<target/partial/missing/not applicable>` |
| Register impact | `<slice/testing/both/none>` |
| Blockers | `<support owner version / source uncertainty / stale path / none>` |
| Recommended action | `<pilot/refactor later/defer/archive/needs classification>` |

## 8. Boundaries

```text
Do not:
  - edit concrete `SL-*` slice drafts in SLICE-REF-GM-0;
  - add local section `Sources:` blocks to slice drafts yet;
  - claim synchronized coverage for concrete slice/test files;
  - run first pilot before audit cards and pilot decision are recorded;
  - make support owner docs globally versioned unless a separate step chooses that.

Allowed:
  - update this map;
  - update register rows that describe audit/refactor planning state;
  - update action log after approved map-sync/archive application;
  - choose pilot candidate after audit cards exist.
```

## 9. Change Log

```text
- SLICE-REF-GM-0 created this local Goal Map at Doc version: v0.1.0 after SLICE-TEST-SRC-0A/0B.
- The map records current slice/testing refactor state, target sources, preliminary gaps and pilot decision rules.
- Concrete slice drafts and test plans remain untouched and unsynchronized.
```
