# Slice/Test Source Refactor Goal Map

Status: active local workstream Goal Map / pre-refactor audit map
Doc version: v0.2.0
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

## 0. Source Sync / SLICE-REF-GM-0-AUD1

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
    - planning/root-source-sync-register.md @ Doc version: v1.9.0
  Internal dependencies:
    - Current Snapshot
    - Roadmap / Work Chain
    - Audit Summary
    - Gap Categories
    - Pilot Candidate Decision
    - AUD1 Inventory And Gap Findings
  Not checked:
    - concrete slice drafts were not edited in SLICE-REF-GM-0
    - per-slice local section Sources blocks are not added yet
    - concrete slice/test coverage remains unsynchronized
    - support owner docs beyond the 0B version-seeded core remain conditional per-slice preflight inputs
```

SLICE-REF-GM-0 creates this local Goal Map after the slice/testing skeleton registers and owner workflow/template versions exist. The map is an audit/refactor navigator only. It does not claim that concrete slice drafts or test plans are synchronized.

SLICE-REF-GM-0-AUD1 records the first archive-level inventory/gap audit. It adds preliminary counts, missing-index findings and candidate audit cards, but still does not start the first concrete slice refactor.

## 0. Current Snapshot

Last updated:
  2026-06-05 / after SLICE-REF-GM-0-AUD1 archive-level inventory/gap audit

Current goal:
  Prepare a controlled one-slice-at-a-time refactor of slice/testing planning files using source-sync registers, versioned owner workflows/templates and local section/file-level source evidence.

Current focus:
  SLICE-REF-GM-0 — audit slice/test files and keep a local refactor Goal Map before the first concrete slice refactor.

Active slice:
  SLICE-REF-GM-0 — Audit And Local Goal Map Before First Slice Refactor
    Status: ▶ NOW / AUD1 inventory and candidate cards captured; pilot decision still pending

Already available:
  - CASCADE-SRC-1A source-impact / targeted cascade foundation is done.
  - CASCADE-ROUTE-1B root route ownership and example-fit cleanup is done.
  - SLICE-TEST-SRC-0A created slice/testing source-sync skeleton registers.
  - SLICE-TEST-SRC-0B seeded active slice/testing owner workflow/template/rule versions.
  - `planning/slices/slice-source-sync-register.md` exists as skeleton / incomplete / not synchronized.
  - `planning/testing/testing-source-sync-register.md` exists as skeleton / incomplete / not synchronized.
  - Active server/client slice templates and slice/test-plan owner workflows have Doc versions.
  - SLICE-REF-GM-0 map exists as local workstream navigator.
  - SLICE-REF-GM-0-AUD1 archive-level inventory and candidate cards are recorded below.

Latest completed:
  - Created slice/testing source-sync skeleton registers.
  - Seeded active slice/testing owner workflow/template/rule versions.
  - Created this local Goal Map.
  - Recorded AUD1 inventory summary, gap stats, missing index rows and two pilot candidate cards.
  - Verified that concrete slice drafts still need per-file audit/refactor and should not be treated as synchronized.

Now:
  - Use this map to drive the next audit-card/detail pass.
  - Decide whether the first pilot should be the safer structural client sidecar or the real AgreementProposalExchange cascade pilot.
  - Keep concrete slice/test coverage unsynchronized until a per-slice source review happens.

Next action:
  Run SLICE-REF-GM-0-AUD2 or choose a narrow pilot-decision pass:
    validate candidate source chains, decide SLICE-PILOT-1, then plan the first concrete slice refactor.

Recommended next action:
  Do not start `SLICE-PILOT-1` until the pilot decision is explicitly recorded in this map. AUD1 recorded candidate cards, but did not choose the pilot.

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
| SLICE-REF-GM-0 | Audit all slice/test files and maintain this local refactor Goal Map. | Map exists; AUD1 inventory/gap stats and two candidate cards are recorded; pilot decision is pending. | ▶ NOW |
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

AUD1 source:
  archive-level scan of `planning/slices/**/*.md`, `planning/testing/**/*.md` and `planning/slices/SLICE-INDEX.md` in the latest snapshot used for this workstream.

AUD1 status:
  preliminary inventory/gap audit recorded; not a final per-slice synchronization proof.

### 4.1 Inventory Summary

| Area | Count / status |
|---|---:|
| `planning/slices/**/*.md` total markdown files | 135 |
| Filename/path-based concrete-ish slice artifacts | 84 |
| `planning/testing/**/*.md` markdown files | 10 |
| Concrete artifacts with `Doc version:` | 0 / 84 |
| Concrete artifacts with exact `## 0. Scenario Sources / Source Sync` | 0 / 84 |
| Concrete artifacts with legacy `## 0. Scenario Sources` | 26 / 84 |
| Concrete artifacts with `Section-Level Sources` heading | 0 / 84 |
| Concrete artifacts with any standalone `Sources:` block | 1 / 84 |
| Concrete artifacts with `Local / Global Sync Check` | 0 / 84 |
| Concrete artifacts with `Behavior-to-Test Trace` | 26 / 84 |
| Concrete artifacts with `Test / Verification Plan` | 46 / 84 |
| Concrete artifacts with `Behavior Coverage` | 60 / 84 |
| Concrete artifacts with `Guardrail Summary` | 24 / 84 |
| Concrete artifacts with `Questions / Decisions` | 50 / 84 |

Interpretation:
  the slice layer has many behavior/test sections already, but concrete slice artifacts are still before the target source-sync/version model.

### 4.2 Concrete-ish Artifact Groups

| Group | Count | Notes |
|---|---:|---|
| Root slice files under `planning/slices/SL-*` | 35 | Main legacy/current server/client/root slice pool. |
| Legacy `planning/slices/l2/*` | 20 | Needs active/legacy/sidecar/sync-note classification before refactor. |
| Cross-cutting `planning/slices/cross-cutting/*` | 9 | Needs separate cross-cutting treatment. |
| Legacy `planning/slices/l1/*` | 7 | Needs active/legacy classification. |
| Client folder concrete/example artifacts | 4 | Includes one real client sidecar and client examples. |
| Server folder concrete artifacts | 3 | Activation server slice family. |
| Examples | 4 | Must not be treated as active source-of-truth slices. |
| Implementation prompt | 1 | Helper/prompt artifact, not canonical source-of-truth. |
| Other root legacy client artifact | 1 | Needs classification. |

### 4.3 Testing Layer State

Versioned core testing owners:

```text
planning/testing/testing-source-sync-register.md @ Doc version: v0.2.0
planning/testing/testing-responsibility-map.md @ Doc version: v0.1.0
planning/testing/testing-principles.md @ Doc version: v0.1.0
planning/testing/server-slice-test-plan-rules.md @ Doc version: v0.1.0
```

Conditional/unversioned testing support docs:

```text
planning/testing/README.md
planning/testing/e2e-testing-workflow.md
planning/testing/e2e-playwright-workflow.md
planning/testing/test-object-patterns.md
planning/testing/playwright-e2e-cleanup-plan.md
planning/testing/playwright-e2e-and-screenshot-plan.md
```

Use the conditional support docs only when the selected slice consumes E2E/object-pattern/screenshot/evidence behavior. Do not globally version them in this audit step.

### 4.4 SLICE-INDEX Missing-File Finding

`planning/slices/SLICE-INDEX.md` contains rows for files that were not present in the archive snapshot:

```text
planning/slices/l2/L2-REVIEW-APPROVE-001-approve-request-review.client.md
planning/slices/l2/L2-REVIEW-REJECT-001-reject-request-review.client.md
planning/slices/server/cross-cutting/CC-SEC-CSRF-001-unsafe-command-protection.server.md
planning/slices/client/cross-cutting/CC-SEC-CSRF-001-unsafe-command-protection.client.md
```

Treat this as `GAP-11: index row points to missing file`. Do not silently create these files. First classify whether each row is stale, planned, renamed or intentionally absent.

### 4.5 Candidate Audit Cards Recorded In AUD1

AUD1 records two pilot candidates. The cards are enough to compare pilot direction, but not enough to start refactor without a final pilot decision.

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
| GAP-11 | `SLICE-INDEX` row points to file missing from archive snapshot | Classify as stale/planned/renamed/absent before creating or refactoring anything. |

AUD1 confirmed GAP-1, GAP-2, GAP-3 and GAP-4 as broad concrete-slice-layer gaps. GAP-11 is a concrete index consistency finding.

## 6. Pilot Candidate Decision

Do not start a pilot until the pilot decision is explicitly recorded. AUD1 records candidate cards but keeps the decision pending.

### 6.1 Candidate A — safer structural client sidecar pilot

| Field | Value |
|---|---|
| File | `planning/slices/client/SL-AUTH-ACT-001-email-activation-ui-and-protected-gate.client.md` |
| Type | client sidecar |
| Current status | draft / ready for implementation planning |
| Target template | `planning/slices/client/CLIENT-SLICE-TEMPLATE.md @ Doc version: v0.1.0` |
| Doc version | missing |
| Source Sync block | legacy `## 0. Scenario Sources` |
| Section-level Sources | missing |
| Testing trace | present |
| Test / Verification Plan | present |
| Behavior Coverage | present |
| Local / Global Sync Check | missing |
| Guardrail Summary | missing |
| Register impact | slice + testing |
| Risk | medium-low |
| Recommended action | good structural pilot candidate if we want to validate target shape/process first |

Related activation family files:

```text
planning/slices/server/SL-AUTH-ACT-001-register-pending-and-send-activation-email.server.md
planning/slices/server/SL-AUTH-ACT-002-activate-client-account.server.md
planning/slices/server/SL-AUTH-ACT-003-active-account-guard.server.md
planning/slices/cross-cutting/CC-AUTH-ACT-001-email-activation-flow.md
```

Boundary:
  if this client sidecar is chosen, do not silently refactor the whole activation family in the same batch.

### 6.2 Candidate B — real cascade pilot

| Field | Value |
|---|---|
| File | `planning/slices/SL-AGR-EXCH-001-start-agreement-exchange-with-initial-employee-proposal.md` |
| Type | server command slice |
| Current status | implemented slice draft refactor / implementation not rechecked |
| Target template | `planning/slices/server/SERVER-SLICE-TEMPLATE.md @ Doc version: v0.1.0` |
| Doc version | missing |
| Source Sync block | legacy `## 0. Scenario Sources` plus source/domain/slice coverage snapshot |
| Section-level Sources | missing |
| Testing trace | present |
| Test / Verification Plan | present |
| Behavior Coverage | present |
| Local / Global Sync Check | missing |
| Guardrail Summary | present |
| Register impact | slice + testing + domain/scenario source chain |
| Risk | higher |
| Recommended action | best real cascade pilot if source-chain gaps remain manageable |

Known attractive source chain:

```text
SC-13D scenario text/spec/data/behavior files
planning/domain/domain-source-sync-register.md @ Doc version: v0.2.0
planning/domain/aggregates/agreement-proposal-exchange.md @ Doc version: v0.1.0
planning/slices/slice-source-sync-register.md @ Doc version: v0.2.0 / skeleton
planning/testing/testing-source-sync-register.md @ Doc version: v0.2.0 / skeleton
```

Related sidecar/prompt artifacts:

```text
planning/slices/l2/L2-AGR-EXCH-START-001-start-agreement-exchange-with-initial-employee-proposal.client.md
planning/slices/implementation-prompts/SL-AGR-EXCH-001-start-agreement-exchange-with-initial-employee-proposal.prompt.md
```

Boundary:
  the L2 sidecar and prompt are not automatically part of the first server-slice pilot. Treat the prompt as helper/non-canonical unless separately promoted.

### 6.3 Preferred decision rule after AUD1

```text
1. If the goal is process/shape validation with lower risk, choose Candidate A.
2. If the goal is validating the real scenario -> domain -> slice -> testing cascade, choose Candidate B.
3. Before choosing Candidate B, perform a narrow source-chain preflight for SC-13D and AgreementProposalExchange.
4. Whichever pilot is selected, keep the batch to one concrete slice file unless explicitly approved otherwise.
```

Current recommendation:
  lean toward Candidate B as the real cascade pilot, but run/record one narrow source-chain preflight before starting `SLICE-PILOT-1`.

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
  - run first pilot before AUD1 findings, pilot decision and required source-chain preflight are recorded;
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
- SLICE-REF-GM-0-AUD1 recorded archive-level inventory/gap stats, missing SLICE-INDEX file rows and two candidate audit cards, bumped this map to Doc version: v0.2.0 and kept pilot decision pending.
- Concrete slice drafts and test plans remain untouched and unsynchronized.
```
