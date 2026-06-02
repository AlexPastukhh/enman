# Root Source Sync Register

Status: skeleton / root planning dependency register partially derived for source-governance core, root router/onboarding core, protocol/role core and output/archive core; root folder inventory classified
Doc version: v0.9.0
Scope: source dependency skeleton for root planning workflow, routing, source-governance, Goal Map discovery and Tampermonkey command-helper files

## 1. Purpose

```text
Sources:
  Format/process:
    - planning/source-cascade-sync-workflow.md @ Doc version: v0.4.0
    - planning/SOURCE-SECTION-SOURCES-TEMPLATE.md @ Doc version: v0.2.0
    - planning/source-usage-cascade-profile.md @ Doc version: v0.2.0
  Content:
    - planning/domain/domain-source-sync-register.md @ Doc version: v0.2.0
  Internal dependencies:
    - none
  Not checked:
    - active root source passes outside ROOT-FULL-0 classification scope
    - planned planning/slices/slice-source-sync-register.md does not exist yet
```

This register tracks root-level planning files whose behavior depends on other planning files, workflows, templates, registers, layer docs or helper projection files.

It is still a skeleton register for broad root coverage. It exists to make the root dependency model explicit before broad slice-side source cascade work starts.

This register does not claim complete source coverage yet.

After DISC-GM-TM1, this register also names the root discovery dependencies for:

```text
- living Goal Map maintenance;
- Tampermonkey inserted command interpretation;
- explicit review-diff-file archive routing;
- root onboarding / workflow activation / responsibility routing.
```

After SRC-LOCAL-RULE-1, this register also records that structured files with stable semantic sections need local section-level `Sources:` blocks when section content has real source dependencies.

After ROOT-SRC-1, this register records partial derived coverage for the source-governance core only:

```text
planning/source-cascade-sync-workflow.md
planning/SOURCE-SECTION-SOURCES-TEMPLATE.md
planning/source-usage-cascade-profile.md
planning/root-source-sync-register.md
```

After SRC-DEP-CMD-1, this register also records that `planning/planning-use-case-map.md` has an explicit source dependency/link route. The router row remains skeleton until a full root-router local/file-level audit is performed.

After ROOT-SRC-2A, this register records partial derived/synchronized coverage for the root router/onboarding core only:

```text
planning/README.md
planning/planning-use-case-map.md
planning/workflow-activation-map.md
planning/planning-doc-responsibility-map.md
```

After ROOT-SRC-2B, this register records partial derived/synchronized coverage for the root protocol/role core only:

```text
planning/planning-agent-protocol.md
planning/agent-roles-and-required-actions.md
```


After ROOT-SRC-3A, this register records partial derived/synchronized coverage for the root output/archive core only:

```text
planning/replacement-file-generation-guide.md
planning/documentation/review-diff-file-workflow.md
planning/documentation/reviewable-agent-output-and-commands-workflow.md
planning/documentation/file-update-overview-workflow.md
planning/documentation/FILE-UPDATE-OVERVIEW-TEMPLATE.md
```

After ROOT-FULL-0, this register contains an explicit root folder inventory/classification table. ROOT-FULL-0 is not a source pass and does not convert active deferred rows to derived/synchronized coverage.

## 2. Register Rules

```text
Sources:
  Format/process:
    - planning/source-cascade-sync-workflow.md @ Doc version: v0.4.0
    - planning/SOURCE-SECTION-SOURCES-TEMPLATE.md @ Doc version: v0.2.0
    - planning/source-usage-cascade-profile.md @ Doc version: v0.2.0
  Content:
    - planning/domain/domain-source-sync-register.md @ Doc version: v0.2.0
  Internal dependencies:
    - Purpose
  Not checked:
    - active root source passes outside ROOT-FULL-0 classification scope
    - planned planning/slices/slice-source-sync-register.md does not exist yet
```

```text
- This register covers root planning/workflow/router/source-governance files and command-helper discovery files.
- It does not cover domain aggregate/value-object semantics themselves.
- It does not cover slice-layer source dependencies before the slice register exists.
- Register rows are skeleton rows until the listed file receives a local source pass or explicit file-level dependency audit.
- Do not treat a skeleton row as proof that every dependency was checked.
- Use Doc version when a source declares it.
- Use version not declared / needs-version when a source has no Doc version in the current checked state.
- Use implementation/helper source, version not applicable for userscript code unless a separate implementation version policy is introduced.
- Keep domain-layer semantic dependencies in planning/domain/domain-source-sync-register.md.
- Keep future slice-layer dependencies in planning/slices/slice-source-sync-register.md once that register exists.
- ROOT-SRC-1 changes only source-governance core rows from skeleton to derived/synchronized; other root rows remain skeleton unless explicitly audited later.
```

Register states:

```text
skeleton:
  Candidate dependency rows are named, but not fully audited.

derived:
  Rows were derived from local Sources blocks or a file-level dependency audit.

synchronized:
  Rows were compared with current source paths and version/status labels.
```

Current register state:

```text
This file remains skeleton for full root coverage.
ROOT-SRC-1 makes source-governance core rows partially derived/synchronized.
ROOT-SRC-2A makes root router/onboarding core rows partially derived/synchronized.
ROOT-SRC-2B makes protocol/role core rows partially derived/synchronized.
ROOT-SRC-3A makes output/archive core rows partially derived/synchronized.
ROOT-FULL-0 classifies the root folder inventory but does not claim local source coverage for deferred active files.
SRC-AUD-ROOT-1 updated candidate rows after DISC-GM-TM1, but does not claim full root coverage.
SRC-LOCAL-RULE-1 updated source/template version candidates for structured local section Sources rules, but did not perform a root local Sources pass.
```

## 3. Root Files With Source Dependencies

```text
Sources:
  Format/process:
    - planning/source-cascade-sync-workflow.md @ Doc version: v0.4.0
    - planning/SOURCE-SECTION-SOURCES-TEMPLATE.md @ Doc version: v0.2.0
    - planning/source-usage-cascade-profile.md @ Doc version: v0.2.0
  Content:
    - planning/domain/domain-source-sync-register.md @ Doc version: v0.2.0
  Internal dependencies:
    - Purpose
    - Register Rules
  Not checked:
    - active root file source passes outside ROOT-FULL-0 classification scope
    - planned planning/slices/slice-source-sync-register.md does not exist yet
```

| Root file | Role | Current version/status | Register coverage state | Notes |
|---|---|---|---|---|
| `planning/source-cascade-sync-workflow.md` | source cascade workflow | Doc version: v0.4.0 | derived from local section Sources in ROOT-SRC-1; rule updated in SRC-DEP-CMD-1 | Root workflow that defines local `Sources:` blocks, register states, layer register rules, structured-file local Sources requirement and explicit source dependency/link declaration rule. |
| `planning/source-usage-cascade-profile.md` | Enman project profile | Doc version: v0.2.0 | derived from local section Sources in ROOT-SRC-1; triggers updated in SRC-DEP-CMD-1 | Project-specific source/consumer categories, row shape and cascade triggers. |
| `planning/SOURCE-SECTION-SOURCES-TEMPLATE.md` | local section `Sources:` block template | Doc version: v0.2.0 | derived from local section Sources in ROOT-SRC-1; source labels refreshed in SRC-DEP-CMD-1 | General fenced `Sources:` block format with version/status labels. |
| `planning/root-source-sync-register.md` | root dependency register | Doc version: v0.9.0 | self row partially synchronized for ROOT-SRC-1/SRC-DEP-CMD-1/ROOT-SRC-2A/ROOT-SRC-2B/ROOT-SRC-3A; inventory classified in ROOT-FULL-0 | Still skeleton for full root source coverage; source-governance, root router/onboarding, protocol/role and output/archive core rows are derived/synchronized, while deferred active rows are only classified. |
| `planning/README.md` | planning navigation and onboarding entrypoint | Doc version: v0.3.0 | derived from local section Sources in ROOT-SRC-2A; source labels refreshed in ROOT-SRC-2B/ROOT-SRC-3A | Points new chats to activation/use-case/responsibility routing, output/archive rules, Goal Map and Tampermonkey command-helper discovery docs. |
| `planning/planning-use-case-map.md` | root action-to-doc router | Doc version: v0.4.0 | derived from local section Sources in ROOT-SRC-2A; source dependency route added in SRC-DEP-CMD-1; source labels refreshed in ROOT-SRC-2B/ROOT-SRC-3A | Owns command routing, including `давай архив`, explicit review-diff-file archive route, response commands, Goal Map, Tampermonkey discovery behavior and source dependency/link declaration routing. |
| `planning/workflow-activation-map.md` | workflow activation router | Doc version: v0.3.0 | derived from local section Sources in ROOT-SRC-2A; protocol/role sources covered by ROOT-SRC-2B; output/archive labels refreshed in ROOT-SRC-3A | Activates workflow preflight, root routing, output/archive flows, Goal Map and Tampermonkey command-helper chains. |
| `planning/planning-doc-responsibility-map.md` | root planning responsibility router | Doc version: v0.3.0 | derived from local section Sources in ROOT-SRC-2A; source labels refreshed in ROOT-SRC-2B/ROOT-SRC-3A | Owns root layer placement and responsibility routing, including output/archive, Goal Map and Tampermonkey responsibility anchors. |
| `planning/goal-map-principles-workflow-template.md` | Goal Map owner workflow/template | Doc version: v0.1.0 | candidate row; inventory classified in ROOT-FULL-0 | Owns living Goal Map rules, brief shape and `синх карта`; local source pass still pending. |
| `planning/workstreams/command-system-and-tampermonkey-goal-map.md` | living command-system/Tampermonkey Goal Map | Doc version: v0.1.0 | candidate row; inventory classified in ROOT-FULL-0 | Workstream state artifact; not a generic workflow; local source/status refresh still pending. |
| `planning/workstreams/tampermonkey-command-projection-plan.md` | Tampermonkey command projection plan | Doc version: v0.1.0 | candidate row; inventory classified in ROOT-FULL-0 | Defines helper prompt body projection model; local source/status refresh still pending. |
| `tools/tampermonkey/README.md` | Tampermonkey helper orientation | Doc version: v0.1.0 / implementation docs | candidate row; inventory classified in ROOT-FULL-0 | Not command source of truth; helper docs source pass still pending. |
| `tools/tampermonkey/IMPLEMENTATION-NOTES.md` | Tampermonkey implementation notes | Doc version: v0.1.0 / implementation docs | candidate row; inventory classified in ROOT-FULL-0 | Working notes; not command source of truth; helper docs source pass still pending. |
| `tools/tampermonkey/chat-command-palette.user.js` | userscript implementation | implementation/helper source, version not applicable | candidate row; inventory classified in ROOT-FULL-0 | Last in source-of-truth order; must not invent command semantics or receive a Doc version without a code version policy. |
| `planning/replacement-file-generation-guide.md` | replacement package guidance | Doc version: v0.1.0 | derived from local section Sources in ROOT-SRC-3A | Owns default replacement package generation, complete replacement-files layout, apply commands and clipboard diff archive review flow. |
| `planning/documentation/review-diff-file-workflow.md` | explicit repo-stored review diff workflow | Doc version: v0.1.0 | derived from local section Sources in ROOT-SRC-3A | Used only for explicit review-diff-file archive mode; default archive flow stays clipboard diff. |
| `planning/documentation/reviewable-agent-output-and-commands-workflow.md` | response-level output and commands workflow | Doc version: v0.1.0 | derived from local section Sources in ROOT-SRC-3A | Owns answer levels, response-level commands, Goal Map Brief placement and file-update overview placement. |
| `planning/documentation/file-update-overview-workflow.md` | file update overview workflow | Doc version: v0.1.0 | derived from local section Sources in ROOT-SRC-3A | Owns `План файл-обновление` process and final file/update overview placement. |
| `planning/documentation/FILE-UPDATE-OVERVIEW-TEMPLATE.md` | file update overview template | Doc version: v0.1.0 | derived from local section Sources in ROOT-SRC-3A | Owns exact Markdown shape for `План файл-обновление`. |
| `planning/documentation-action-log.md` | action log / historical record | version not confirmed in this register | candidate row only | Log is an evidence trail, not a workflow source by itself. |
| `planning/planning-agent-protocol.md` | planning agent protocol | Doc version: v0.1.0 | derived from local section Sources in ROOT-SRC-2B | Owns core planning protocol, workflow activation rule, role identification rule, line-link rule, scope boundaries and cross-layer planning gates. |
| `planning/agent-roles-and-required-actions.md` | planning role map and required actions | Doc version: v0.1.0 | derived from local section Sources in ROOT-SRC-2B | Owns planning roles, required read order, mandatory actions and role handoff boundaries. |


## 3A. Root Full Folder Inventory / Classification

```text
Sources:
  Format/process:
    - planning/source-cascade-sync-workflow.md @ Doc version: v0.4.0
    - planning/SOURCE-SECTION-SOURCES-TEMPLATE.md @ Doc version: v0.2.0
    - planning/source-usage-cascade-profile.md @ Doc version: v0.2.0
  Content:
    - planning/** root-level markdown inventory @ current repository tree checked in ROOT-FULL-0
    - planning/workstreams/** @ current repository tree checked in ROOT-FULL-0
    - tools/tampermonkey/** @ current repository tree checked in ROOT-FULL-0
  Internal dependencies:
    - Register Rules
    - Root Files With Source Dependencies
  Not checked:
    - local section-level Sources for active deferred files outside already covered core groups
    - planned planning/slices/slice-source-sync-register.md does not exist yet
```

ROOT-FULL-0 classifies root-folder files before broad source/version passes.

This table is an inventory and routing decision table, not proof that every listed file has local `Sources:` blocks.

Classification meanings:

```text
covered active core:
  Already audited with Doc version and local/file-level source coverage in ROOT-SRC-* batches.

active deferred source pass:
  Current root source/workflow/state file; needs its own Doc version/source pass or label refresh.

active bridge / layer-routed:
  Root file that points into scenario/domain/slice/API/testing/VKR/diagram layers. It should not duplicate layer-owned semantics.

historical / cleanup / superseded:
  Not current source-of-truth unless explicitly revived. Keep as historical/reference or migrate/delete later.

implementation/helper:
  Code/helper artifact. Use implementation/helper source, version not applicable unless a separate implementation version policy is introduced.
```

### Covered active core

| File | Classification | Current version/status | Coverage decision | Next action |
|---|---|---|---|---|
| `planning/README.md` | covered active core | Doc version: v0.3.0 | root onboarding/navigation coverage done | Refresh only when dependencies change. |
| `planning/planning-use-case-map.md` | covered active core | Doc version: v0.4.0 | root action/router coverage done | Refresh routes as commands change. |
| `planning/workflow-activation-map.md` | covered active core | Doc version: v0.3.0 | workflow activation coverage done | Refresh after new workflow owners are added. |
| `planning/planning-doc-responsibility-map.md` | covered active core | Doc version: v0.3.0 | root responsibility coverage done | Refresh after new responsibility owners are added. |
| `planning/planning-agent-protocol.md` | covered active core | Doc version: v0.1.0 | protocol coverage done | Refresh when protocol boundaries change. |
| `planning/agent-roles-and-required-actions.md` | covered active core | Doc version: v0.1.0 | role/read-order coverage done | Refresh when roles or required reads change. |
| `planning/source-cascade-sync-workflow.md` | covered active core | Doc version: v0.4.0 | source-cascade workflow coverage done | Refresh when source/register rules change. |
| `planning/source-usage-cascade-profile.md` | covered active core | Doc version: v0.2.0 | source usage profile coverage done | Refresh when source categories/triggers change. |
| `planning/SOURCE-SECTION-SOURCES-TEMPLATE.md` | covered active core | Doc version: v0.2.0 | generic local Sources template coverage done | Refresh when template shape changes. |
| `planning/root-source-sync-register.md` | covered active core / register | Doc version: v0.9.0 | self row partially synchronized; ROOT-FULL-0 adds inventory only | Continue as partial until deferred active groups pass. |
| `planning/replacement-file-generation-guide.md` | covered active core | Doc version: v0.1.0 | replacement/archive workflow coverage done | Refresh when archive rules change. |
| `planning/documentation/review-diff-file-workflow.md` | covered active core | Doc version: v0.1.0 | explicit review-diff-file workflow coverage done | Keep explicit-only boundary. |
| `planning/documentation/reviewable-agent-output-and-commands-workflow.md` | covered active core | Doc version: v0.1.0 | response-level command/output coverage done | Refresh when output commands change. |
| `planning/documentation/file-update-overview-workflow.md` | covered active core | Doc version: v0.1.0 | `План файл-обновление` workflow coverage done | Refresh when overview rules change. |
| `planning/documentation/FILE-UPDATE-OVERVIEW-TEMPLATE.md` | covered active core | Doc version: v0.1.0 | `План файл-обновление` template coverage done | Refresh when exact output shape changes. |

### Active deferred root source passes

| File | Classification | Current version/status | Coverage decision | Next action |
|---|---|---|---|---|
| `planning/goal-map-principles-workflow-template.md` | active deferred source pass | Doc version: v0.1.0 | active Goal Map owner; needs source-label refresh after ROOT-SRC-3A and local Sources review | ROOT-FULL-1. |
| `planning/goal-map-example.md` | active example / deferred source pass | Doc version: v0.2.0 | example only, but referenced by Goal Map owner | Review with ROOT-FULL-1; do not treat as command source of truth. |
| `planning/workstreams/command-system-and-tampermonkey-goal-map.md` | active living workstream state | Doc version: v0.1.0 | living Goal Map; needs source/status refresh | ROOT-FULL-1. |
| `planning/workstreams/tampermonkey-command-projection-plan.md` | active deferred source pass | Doc version: v0.1.0 | helper projection plan; needs source/status refresh against current routes | ROOT-FULL-1. |
| `planning/deferred-goals-and-ideas.md` | active backlog / deferred source pass | Doc version: v0.1.0 | active backlog owner, not a current workflow | Review with Goal Map/maintenance pass. |
| `planning/repo-structure-memory.md` | active orientation / deferred source pass | Doc version: v0.1.0 | repo-orientation memory; needs source/status refresh | Review with Goal Map/Tampermonkey or maintenance pass. |
| `tools/tampermonkey/README.md` | active helper implementation docs | Doc version: v0.1.0 | helper orientation; not command source of truth | ROOT-FULL-1. |
| `tools/tampermonkey/IMPLEMENTATION-NOTES.md` | active helper implementation notes | Doc version: v0.1.0 | working implementation notes | ROOT-FULL-1; avoid overclaiming command semantics. |
| `tools/tampermonkey/chat-command-palette.user.js` | implementation/helper | implementation/helper source, version not applicable | no Doc version policy for JS | Audit projection consistency in ROOT-FULL-1 without inventing Doc version. |

### Evidence, status and maintenance root files

| File | Classification | Current version/status | Coverage decision | Next action |
|---|---|---|---|---|
| `planning/status-evidence-profile.md` | active project profile | version not confirmed | source/status profile affects evidence claims | ROOT-FULL-2. |
| `planning/shared-visibility-map.md` | active project map | version not confirmed | visibility/status source; needs audit | ROOT-FULL-2. |
| `planning/repo-grounded-github-line-links-workflow.md` | active workflow | version not confirmed | current answer-evidence workflow | ROOT-FULL-2. |
| `planning/planning-maintenance-register.md` | active maintenance register | version not confirmed | maintenance routing/register | ROOT-FULL-2. |
| `planning/current-state.md` | status/evidence note | version not declared | decide active vs historical before versioning | ROOT-FULL-2 classification/source pass. |
| `planning/decisions.md` | decisions log | version not declared | log/evidence, not workflow by default | ROOT-FULL-2; keep evidence-trail semantics. |
| `planning/risk-log.md` | risk log | version not declared | log/evidence, not workflow by default | ROOT-FULL-2; keep evidence-trail semantics. |
| `planning/action-log.md` | legacy/general action log | version not declared | likely historical/evidence trail | ROOT-FULL-2 classify before use. |
| `planning/documentation-action-log.md` | documentation action log | version not confirmed / evidence trail | evidence trail only, not workflow source by itself | Keep candidate/evidence row; do not force local Sources. |
| `planning/planning-workflow-current.md` | current planning workflow/status note | version not confirmed | may be active or superseded by root router chain | ROOT-FULL-2 decide active/currentness. |
| `planning/l1-current-implementation-status.md` | status/implementation inventory | version not confirmed | implementation-status evidence; not current proof without repo check | ROOT-FULL-2. |
| `planning/l2-current-planning-status.md` | status/planning inventory | version not confirmed | planning-status evidence; needs freshness review | ROOT-FULL-2. |
| `planning/l1-domain-implementation-cut.md` | implementation-readiness bridge | version not confirmed | bridge/status file | ROOT-FULL-2 or ROOT-FULL-4. |
| `planning/l1-domain-testing-rules.md` | testing rules/status bridge | version not confirmed | bridge to testing/domain | ROOT-FULL-2 or ROOT-FULL-4. |

### Safety, reusable context and VKR root files

| File | Classification | Current version/status | Coverage decision | Next action |
|---|---|---|---|---|
| `planning/agent-scope-boundaries-and-prompt-safety.md` | active safety workflow | version not confirmed | should be versioned/source-covered if still active | ROOT-FULL-3. |
| `planning/agent-rules.md` | agent rule note | version not declared | decide active vs superseded by protocol/scope docs | ROOT-FULL-3. |
| `planning/general-project-info.md` | reusable project context | version not declared | context/reference, not implementation proof | ROOT-FULL-3. |
| `planning/vkr-clean-reference.md` | active VKR wording reference | version not confirmed | clean wording owner | ROOT-FULL-3. |
| `planning/vkr-formulation-guide.md` | active/internal VKR writing guide | version not confirmed | reusable writing guide | ROOT-FULL-3. |
| `planning/vkr-work-context-current.md` | current VKR work context | version not confirmed | current-context evidence; needs freshness boundary | ROOT-FULL-3. |

### Scenario/domain/slice/API/testing bridge root files

| File | Classification | Current version/status | Coverage decision | Next action |
|---|---|---|---|---|
| `planning/scenario-specification-principles.md` | active scenario bridge | version not confirmed | root principle that should route to scenario layer docs | ROOT-FULL-4. |
| `planning/scenario-domain-validation-principles.md` | active scenario/domain bridge | version not confirmed | root principle bridge; avoid duplicating domain register | ROOT-FULL-4. |
| `planning/scenario-to-implementation-workflow-v5-consolidated.md` | superseded compatibility bridge | superseded | historical unless explicitly revived | Mark historical in ROOT-FULL-4. |
| `planning/domain-draft-generation-guide.md` | legacy domain bridge | transitional legacy guide | likely historical/superseded by domain workflows | ROOT-FULL-4. |
| `planning/domain-design-input-navigation-notes.md` | domain navigation note | version not declared | bridge note; verify if still active | ROOT-FULL-4. |
| `planning/domain-model.md` | background / compatibility note | version not confirmed | context/evidence only; domain register owns current domain sources | ROOT-FULL-4. |
| `planning/layer-plan.md` | layer bridge note | version not declared | likely planning/status bridge | ROOT-FULL-4. |
| `planning/api-plan.md` | API bridge note | version not declared | likely superseded by planning/api/** | ROOT-FULL-4. |
| `planning/testing-strategy.md` | testing bridge note | version not declared | likely superseded by planning/testing/** | ROOT-FULL-4. |
| `planning/use-cases.md` | use-case bridge note | version not declared | likely historical/root overview | ROOT-FULL-4. |

### Diagram root files

| File | Classification | Current version/status | Coverage decision | Next action |
|---|---|---|---|---|
| `planning/diagram-brief.md` | diagram root bridge | version not declared | likely legacy bridge to diagramming/diagrams layer | ROOT-FULL-5. |
| `planning/diagram-common-mistakes.md` | diagram root reference | version not confirmed | active or legacy reference; classify before use | ROOT-FULL-5. |
| `planning/diagram-domain-db-brief.md` | diagram root bridge | version not declared | likely legacy bridge | ROOT-FULL-5. |
| `planning/diagram-examples-index.md` | diagram examples index | version not confirmed | route to current examples if active | ROOT-FULL-5. |
| `planning/diagram-generation-rules-with-example.md` | diagram guide | version not confirmed | active or superseded by diagramming workflows | ROOT-FULL-5. |
| `planning/diagram-prompting-guide.md` | diagram prompting guide | version not confirmed | active or superseded by diagramming workflows | ROOT-FULL-5. |
| `planning/diagram-scenario-spec.md` | diagram scenario representation source | version not confirmed | likely bridge to scenario/diagramming layer | ROOT-FULL-5. |

### Historical, cleanup and migration root notes

| File | Classification | Current version/status | Coverage decision | Next action |
|---|---|---|---|---|
| `planning/navigation-cleanup-notes.md` | cleanup note | version not declared | historical unless reopened | Keep historical/deferred. |
| `planning/navigation-cleanup-v3-notes.md` | cleanup note | version not declared | historical unless reopened | Keep historical/deferred. |
| `planning/planning-navigation-replacement-notes.md` | cleanup/replacement note | version not declared | historical unless reopened | Keep historical/deferred. |
| `planning/solution-map-and-cleanup-plan.md` | cleanup/solution note | version not declared | historical unless reopened | Keep historical/deferred. |
```

## 4. Root Dependency Rows

```text
Sources:
  Format/process:
    - planning/source-cascade-sync-workflow.md @ Doc version: v0.4.0
    - planning/SOURCE-SECTION-SOURCES-TEMPLATE.md @ Doc version: v0.2.0
    - planning/source-usage-cascade-profile.md @ Doc version: v0.2.0
  Content:
    - planning/domain/domain-source-sync-register.md @ Doc version: v0.2.0
  Internal dependencies:
    - Root Files With Source Dependencies
  Not checked:
    - active root file source passes outside ROOT-FULL-0 classification scope
    - planned planning/slices/slice-source-sync-register.md does not exist yet
```

| Consumer file | Consumer scope | Source files | Source role | Source version/status | Sync status | Review outcome | Notes |
|---|---|---|---|---|---|---|---|
| `planning/source-cascade-sync-workflow.md` | source cascade rules and register state model | `planning/SOURCE-SECTION-SOURCES-TEMPLATE.md`; `planning/domain/AGGREGATE-SECTION-SOURCES-TEMPLATE.md`; `planning/slices/SERVER-SLICE-SECTION-SOURCES-TEMPLATE.md`; `planning/source-usage-cascade-profile.md`; `planning/root-source-sync-register.md`; `planning/domain/domain-source-sync-register.md`; planned `planning/slices/slice-source-sync-register.md` | format/process + register-index | source workflow Doc version: v0.4.0; general template Doc version: v0.2.0; profile Doc version: v0.2.0; domain register Doc version: v0.2.0; slice register planned | derived/synchronized for ROOT-SRC-1 local Sources scope; rule updated in SRC-DEP-CMD-1 | explicit source dependency/link rule added; source-block metadata refreshed | Workflow has local section Sources blocks, distinguishes current domain/root registers from planned slice register and classifies file-to-file/section-to-file dependencies. |
| `planning/source-usage-cascade-profile.md` | Enman source/consumer categories and row conventions | `planning/documentation/field-kits/source-usage-cascade-field-kit.md`; `planning/source-cascade-sync-workflow.md`; `planning/root-source-sync-register.md`; `planning/domain/domain-source-sync-register.md`; planned `planning/slices/slice-source-sync-register.md`; `planning/documentation/examples/project-specific/enman/SOURCE-USAGE-CASCADE-ENMAN-SCENARIO-EXAMPLE.md` | format/process + project profile/example | source workflow Doc version: v0.4.0; root register Doc version: v0.7.0; domain register Doc version: v0.2.0; field-kit/example versions not confirmed; slice register planned | derived/synchronized for ROOT-SRC-1 local Sources scope; triggers updated in SRC-DEP-CMD-1 | source dependency/link trigger added; source-block metadata refreshed | Profile has local section Sources blocks for categories, register model, triggers, pilot and related files. |
| `planning/SOURCE-SECTION-SOURCES-TEMPLATE.md` | fenced `Sources:` block format | `planning/source-cascade-sync-workflow.md`; `planning/source-usage-cascade-profile.md`; local `Sources:` block rules | format/process | source workflow Doc version: v0.4.0; profile Doc version: v0.2.0; template Doc version: v0.2.0 | derived/synchronized for ROOT-SRC-1 local Sources scope; source labels refreshed in SRC-DEP-CMD-1 | no semantic change; source-block metadata refreshed | Template has minimal local Sources blocks for template, field meaning, rules and source delta sections. |
| `planning/README.md` | onboarding, root navigation and command-helper discovery | `planning/workflow-activation-map.md`; `planning/planning-use-case-map.md`; `planning/planning-doc-responsibility-map.md`; `planning/source-cascade-sync-workflow.md`; `planning/source-usage-cascade-profile.md`; `planning/root-source-sync-register.md`; `planning/goal-map-principles-workflow-template.md`; `planning/workstreams/command-system-and-tampermonkey-goal-map.md`; `planning/workstreams/tampermonkey-command-projection-plan.md`; `tools/tampermonkey/README.md` | navigation/process + source-governance + workstream discovery | README Doc version: v0.2.0; activation/responsibility root files Doc version: v0.2.0; use-case map Doc version: v0.3.0; Goal Map/Tampermonkey sources not fully audited | derived/synchronized for ROOT-SRC-2A local Sources scope | local Sources added; no navigation semantics changed | New chat discovery includes living Goal Map and helper projection docs; Goal Map/Tampermonkey rows remain deferred outside ROOT-SRC-2B. |
| `planning/planning-use-case-map.md` | action-to-doc-flow routing and command semantics | `planning/README.md`; `planning/workflow-activation-map.md`; `planning/planning-doc-responsibility-map.md`; `planning/source-cascade-sync-workflow.md`; `planning/SOURCE-SECTION-SOURCES-TEMPLATE.md`; `planning/source-usage-cascade-profile.md`; `planning/root-source-sync-register.md`; `planning/domain/domain-source-sync-register.md`; `planning/replacement-file-generation-guide.md`; `planning/documentation/review-diff-file-workflow.md`; `planning/documentation/reviewable-agent-output-and-commands-workflow.md`; `planning/goal-map-principles-workflow-template.md`; `planning/workstreams/command-system-and-tampermonkey-goal-map.md`; `planning/workstreams/tampermonkey-command-projection-plan.md`; layer readme/workflow files by use case | routing/process + command source-of-truth | use-case map Doc version: v0.3.0; source-governance versions current; mixed versions/statuses for output/Goal Map/Tampermonkey owners | derived/synchronized for ROOT-SRC-2A local Sources scope | local Sources added; existing command routes preserved | Keeps ordinary `давай архив` as clipboard diff and explicit `давай архив с review diff file` as separate route; routes source dependency/link commands to source-cascade workflow/template/profile and relevant registers. |
| `planning/workflow-activation-map.md` | workflow activation and implicit workflow chains | `planning/README.md`; `planning/planning-agent-protocol.md`; `planning/agent-roles-and-required-actions.md`; `planning/planning-use-case-map.md`; `planning/planning-doc-responsibility-map.md`; `planning/source-cascade-sync-workflow.md`; `planning/root-source-sync-register.md`; `planning/goal-map-principles-workflow-template.md`; `planning/workstreams/command-system-and-tampermonkey-goal-map.md`; `planning/workstreams/tampermonkey-command-projection-plan.md`; `tools/tampermonkey/README.md`; layer workflow files listed in the activation table | workflow routing/process | activation map Doc version: v0.2.0; README/responsibility root files Doc version: v0.2.0; use-case map Doc version: v0.3.0; protocol/role Doc version: v0.1.0 | derived/synchronized for ROOT-SRC-2A local Sources scope; protocol/role sources covered by ROOT-SRC-2B | local Sources added; registry semantics preserved | Activates Goal Map and Tampermonkey command-helper chains; protocol/role sources are now versioned but broader Goal Map/Tampermonkey rows remain deferred. |
| `planning/planning-doc-responsibility-map.md` | root planning responsibility routing | `planning/README.md`; `planning/planning-use-case-map.md`; `planning/workflow-activation-map.md`; documentation/scenario/domain/slice/local responsibility maps; `planning/goal-map-principles-workflow-template.md`; `planning/workstreams/command-system-and-tampermonkey-goal-map.md`; `planning/workstreams/tampermonkey-command-projection-plan.md`; `tools/tampermonkey/README.md`; `tools/tampermonkey/IMPLEMENTATION-NOTES.md`; `tools/tampermonkey/chat-command-palette.user.js` | responsibility routing/process | responsibility map Doc version: v0.2.0; root router sources versioned; local layer responsibility maps mixed/not fully audited | derived/synchronized for ROOT-SRC-2A local Sources scope | local Sources added; responsibility semantics preserved | Includes command-system / Goal Map / Tampermonkey ownership anchors; local layer responsibility passes remain deferred. |
| `planning/planning-agent-protocol.md` | planning protocol, workflow activation gate, role identification and scope boundaries | `planning/README.md`; `planning/workflow-activation-map.md`; `planning/planning-use-case-map.md`; `planning/planning-doc-responsibility-map.md`; `planning/agent-roles-and-required-actions.md`; role/layer workflow files by protocol section | protocol/process + permission boundaries | protocol Doc version: v0.1.0; root router sources versioned; downstream role/layer workflows mixed/not fully audited | derived/synchronized for ROOT-SRC-2B local Sources scope | local Sources added; protocol semantics preserved | Protocol owns preflight, role identification, line-link, scope and question-first rules; downstream layer workflows remain checked per task. |
| `planning/agent-roles-and-required-actions.md` | planning roles, required read order and role handoff boundaries | `planning/README.md`; `planning/planning-agent-protocol.md`; `planning/workflow-activation-map.md`; `planning/planning-doc-responsibility-map.md`; role-specific workflow docs by section | role routing/process + mandatory actions | role map Doc version: v0.1.0; root protocol/router sources versioned; role-specific workflows mixed/not fully audited | derived/synchronized for ROOT-SRC-2B local Sources scope | local Sources added; role boundaries preserved | Role map names role-specific must-reads but does not audit every downstream workflow. |
| `planning/replacement-file-generation-guide.md` | replacement package rules, apply command shape and diff capture | `planning/planning-use-case-map.md`; `planning/documentation/review-diff-file-workflow.md`; `planning/documentation/file-update-overview-workflow.md`; target files by package scope | output/process + review loop | replacement guide Doc version: v0.1.0; review-diff workflow Doc version: v0.1.0; file-update overview workflow Doc version: v0.1.0 | derived/synchronized for ROOT-SRC-3A local Sources scope | local Sources added; default clipboard diff preserved | Owns `давай архив` replacement-package mode and explicitly separates it from archive read-source mode. |
| `planning/documentation/review-diff-file-workflow.md` | explicit repo-stored review diff workflow | `planning/replacement-file-generation-guide.md`; `planning/planning-use-case-map.md`; `_ai-review-diffs/last-archive.diff` when explicit mode is used | output/process + review artifact | review-diff workflow Doc version: v0.1.0; replacement guide Doc version: v0.1.0; use-case map Doc version: v0.4.0 | derived/synchronized for ROOT-SRC-3A local Sources scope | local Sources added; explicit-only boundary preserved | Used only when user explicitly requests repo-stored review diff. |
| `planning/documentation/reviewable-agent-output-and-commands-workflow.md` | response levels and response commands | `planning/planning-use-case-map.md`; `planning/documentation/file-update-overview-workflow.md`; `planning/documentation/FILE-UPDATE-OVERVIEW-TEMPLATE.md`; `planning/goal-map-principles-workflow-template.md`; examples as demonstrations | response/process + command output shape | reviewable workflow Doc version: v0.1.0; file-update overview workflow Doc version: v0.1.0; template Doc version: v0.1.0; Goal Map owner version not confirmed | derived/synchronized for ROOT-SRC-3A local Sources scope | local Sources added; response-command semantics preserved | Owns Level 1/2/3, `кц`, `крит`, `Краткое саммари` and `План файл-обновление` placement. |
| `planning/documentation/file-update-overview-workflow.md` | file update overview workflow | `planning/documentation/reviewable-agent-output-and-commands-workflow.md`; `planning/documentation/FILE-UPDATE-OVERVIEW-TEMPLATE.md`; `planning/planning-use-case-map.md`; `planning/replacement-file-generation-guide.md` | output/process + file update overview rules | file-update overview workflow Doc version: v0.1.0; reviewable workflow Doc version: v0.1.0; template Doc version: v0.1.0 | derived/synchronized for ROOT-SRC-3A local Sources scope | local Sources added; overview semantics preserved | Owns final `План файл-обновление` block for plans, archive responses and diff reviews. |
| `planning/documentation/FILE-UPDATE-OVERVIEW-TEMPLATE.md` | file update overview template | `planning/documentation/file-update-overview-workflow.md`; `planning/documentation/reviewable-agent-output-and-commands-workflow.md` | output/template | template Doc version: v0.1.0; workflow Doc version: v0.1.0; reviewable workflow Doc version: v0.1.0 | derived/synchronized for ROOT-SRC-3A local Sources scope | local Sources added; template shape preserved | Exact Markdown shape for `План файл-обновление`. |
| `planning/goal-map-principles-workflow-template.md` | Goal Map rules, brief shape and map sync command | `planning/planning-use-case-map.md`; `planning/documentation/reviewable-agent-output-and-commands-workflow.md`; `planning/goal-map-example.md`; `planning/documentation/examples/GOAL-MAP-BRIEF-RESPONSE-EXAMPLE.md`; `planning/documentation/examples/GOAL-MAP-SYNC-COMMAND-EXAMPLE.md`; relevant living Goal Map files | workflow/template + response-shape sources | mixed; not fully version-audited | skeleton | Goal Map owner audit needed | Added new-chat discovery rule; durable state lives in living Goal Maps, not in brief output. |
| `planning/workstreams/command-system-and-tampermonkey-goal-map.md` | living state for command-system/Tampermonkey workstream | `planning/goal-map-principles-workflow-template.md`; `planning/planning-use-case-map.md`; `planning/documentation/examples/README.md`; `planning/workstreams/tampermonkey-command-projection-plan.md`; `tools/tampermonkey/README.md`; `tools/tampermonkey/chat-command-palette.user.js`; `planning/replacement-file-generation-guide.md`; `planning/documentation/review-diff-file-workflow.md` | living-state/workstream source + route/process references | mixed; not fully version-audited | skeleton | workstream map audit needed | Snapshot currently points to SL-6 helper smoke testing after DISC-GM-TM1. |
| `planning/workstreams/tampermonkey-command-projection-plan.md` | helper command projection and prompt envelope | `planning/planning-use-case-map.md`; `planning/replacement-file-generation-guide.md`; `planning/documentation/review-diff-file-workflow.md`; `planning/goal-map-principles-workflow-template.md`; `planning/documentation/examples/README.md`; `tools/tampermonkey/README.md`; `tools/tampermonkey/chat-command-palette.user.js` | projection/process + command-route references | mixed; not fully version-audited | skeleton | projection plan audit needed | Helper profiles are route hints; UCM and owner workflows remain authoritative. |
| `tools/tampermonkey/README.md` | helper orientation and inserted-command interpretation | `planning/workstreams/tampermonkey-command-projection-plan.md`; `planning/planning-use-case-map.md`; `tools/tampermonkey/IMPLEMENTATION-NOTES.md`; `tools/tampermonkey/chat-command-palette.user.js` | implementation orientation + projection reference | implementation docs; version not declared | skeleton | helper docs audit needed | Not command source of truth. |
| `tools/tampermonkey/chat-command-palette.user.js` | userscript command profile implementation | `planning/workstreams/tampermonkey-command-projection-plan.md`; `planning/planning-use-case-map.md`; `planning/replacement-file-generation-guide.md`; `planning/documentation/review-diff-file-workflow.md`; `planning/goal-map-principles-workflow-template.md` | implementation projection source | implementation/helper source, version not applicable | skeleton | code/profile audit needed | Current command profiles should remain projections of route/owner docs. |
| `planning/replacement-file-generation-guide.md` | replacement archive/package output rules | `planning/planning-use-case-map.md`; `planning/documentation/review-diff-file-workflow.md`; `planning/documentation/documentation-update-workflow.md`; target files for each package | output/process | mixed; guide version not confirmed in this register | skeleton | archive guide audit needed | Default `давай архив` remains saved diff copied to clipboard. |
| `planning/documentation/review-diff-file-workflow.md` | explicit repo-stored review diff transfer | `planning/replacement-file-generation-guide.md`; `planning/planning-use-case-map.md`; `_ai-review-diffs/last-archive.diff` when explicitly generated | explicit transfer workflow | mixed; workflow version not confirmed | skeleton | explicit mode audit needed | Applies only when user explicitly requests repo-stored review diff transfer or approves switch. |

## 5. Cross-Layer Dependency Rows

```text
Sources:
  Format/process:
    - planning/source-cascade-sync-workflow.md @ Doc version: v0.4.0
    - planning/source-usage-cascade-profile.md @ Doc version: v0.2.0
  Content:
    - planning/domain/domain-source-sync-register.md @ Doc version: v0.2.0
  Internal dependencies:
    - Root Dependency Rows
  Not checked:
    - active root file source passes outside ROOT-FULL-0 classification scope
    - planned planning/slices/slice-source-sync-register.md does not exist yet
```

| Root file | Layer/source dependency | Meaning | Current status | Next action |
|---|---|---|---|---|
| `planning/source-cascade-sync-workflow.md` | `planning/domain/domain-source-sync-register.md` | Domain register is current upstream index for aggregate/value-object dependencies. | current / synchronized on domain side | Keep wording current; do not call it planned. |
| `planning/source-cascade-sync-workflow.md` | `planning/slices/slice-source-sync-register.md` | Slice register is intended future layer register. | planned / not created | Create skeleton before broad slice refactor. |
| `planning/planning-use-case-map.md` | `planning/domain/domain-source-sync-register.md` | Source-cascade/domain rows should route domain dependency review through the domain register. | already routed for source/domain rows; slice rows still need review | Review slice rows before first slice refactor. |
| `planning/planning-use-case-map.md` | `planning/source-cascade-sync-workflow.md` / `planning/SOURCE-SECTION-SOURCES-TEMPLATE.md` / `planning/source-usage-cascade-profile.md` | Explicit source dependency/link commands should route through the source-cascade workflow/template/profile and then the relevant layer/root register. | derived/synchronized in ROOT-SRC-2A for the router row | Local Sources were added to the use-case map; later non-router root files still need separate audits. |
| `planning/workflow-activation-map.md` / `planning/planning-use-case-map.md` / `planning/planning-doc-responsibility-map.md` | `planning/planning-agent-protocol.md` / `planning/agent-roles-and-required-actions.md` | Root router files depend on the protocol and role map to select workflow preflight, active role, required read order and permission boundaries. | derived/synchronized in ROOT-SRC-2B for protocol/role rows | Protocol/role files now have local Sources; Goal Map/Tampermonkey/output/archive rows still need separate audits. |
| `planning/README.md` / `planning/workflow-activation-map.md` / `planning/planning-doc-responsibility-map.md` | Goal Map owner + living command-system Goal Map | New-chat and long-running workstream discovery now depend on these files. | candidate rows added after DISC-GM-TM1 | Later decide whether root docs need file-level source audits or local Sources blocks. |
| `planning/planning-use-case-map.md` / `planning/workstreams/tampermonkey-command-projection-plan.md` / `tools/tampermonkey/chat-command-palette.user.js` | replacement archive guide + explicit review-diff workflow | Helper command profiles and root routes distinguish default clipboard diff from explicit review-diff-file mode. | candidate rows added after TM12D/DISC-GM-TM1 | Keep ordinary `давай архив` and explicit review-diff-file route separate. |
| `planning/source-cascade-sync-workflow.md` / `planning/SOURCE-SECTION-SOURCES-TEMPLATE.md` / `planning/domain/value-object-drafting-workflow.md` / `planning/domain/value-object-draft-template.md` | domain value object local section Sources | Structured value-object files now have a declared local section Sources requirement and template shape. | rule/template updated by SRC-LOCAL-RULE-1; value-object files covered by DOM-VO-SRC-ALL-1 | Use domain register v0.2.0 before relying on value objects for first slice-side source coverage. |
| `planning/source-usage-cascade-profile.md` | SC-13D -> domain -> slice chain | Preserved first practical source-cascade chain. | pilot not filled | Decide after root/slice skeleton sync. |

## 6. Not Checked / Deferred

```text
Sources:
  Format/process:
    - planning/source-cascade-sync-workflow.md @ Doc version: v0.4.0
    - planning/source-usage-cascade-profile.md @ Doc version: v0.2.0
  Content:
    - planning/domain/domain-source-sync-register.md @ Doc version: v0.2.0
  Internal dependencies:
    - Root Dependency Rows
    - Cross-Layer Dependency Rows
  Not checked:
    - active root file source passes outside ROOT-FULL-0 classification scope
    - planned planning/slices/slice-source-sync-register.md does not exist yet
```

```text
- Full root planning source pass for every active root file.
- Doc version seed for active root files that are still version-not-declared after ROOT-FULL-0 classification.
- Local section Sources blocks for active root planning files outside already covered core groups.
- Goal Map owner/workstream map dependency audit.
- Tampermonkey projection plan and userscript profile audit against every root route.
- `planning/slices/slice-source-sync-register.md` creation.
- Slice read order/template sync with the domain register.
- Testing layer source-sync register.
```

## 7. Source Delta / Change Log

```text
Sources:
  Format/process:
    - planning/source-cascade-sync-workflow.md @ Doc version: v0.4.0
  Content:
    - planning/domain/domain-source-sync-register.md @ Doc version: v0.2.0
  Internal dependencies:
    - Root Files With Source Dependencies
    - Root Dependency Rows
    - Cross-Layer Dependency Rows
    - Not Checked / Deferred
  Not checked:
    - active root file source passes outside ROOT-FULL-0 classification scope
```

```text
- SRC-AUD-ROOT-1 updated this root source-sync register after DISC-GM-TM1.
- Bumped this register to Doc version: v0.2.0 because the root dependency skeleton now covers Goal Map and Tampermonkey command-helper discovery dependencies.
- Added candidate rows for `planning/README.md`, `planning/planning-doc-responsibility-map.md`, Goal Map owner/living map files, Tampermonkey projection plan, helper docs and userscript.
- Updated root dependency rows to preserve TM12D archive policy: ordinary `давай архив` uses saved diff copied to clipboard; review-diff-file mode is explicit-only.
- SRC-LOCAL-RULE-1 bumped this register to Doc version: v0.3.0 because source-cascade/template candidates now include the structured-file local section Sources rule.
- Recorded `planning/source-cascade-sync-workflow.md` @ Doc version: v0.3.0 and `planning/SOURCE-SECTION-SOURCES-TEMPLATE.md` @ Doc version: v0.2.0.
- Recorded value-object workflow/template local Sources rule updates without claiming value-object file coverage.
- DOM-VO-SRC-ALL-1 made the domain register cover active aggregate and active value-object sources before ROOT-SRC-1.
- ROOT-SRC-1 added local section-level Sources blocks to the source-governance core files and updated those root register rows to derived/synchronized for this narrow scope.
- SRC-DEP-CMD-1 added the explicit source dependency/link route to `planning/planning-use-case-map.md`, bumped this register to Doc version: v0.5.0 and kept the router row skeleton until a full root-router audit.
- ROOT-SRC-2A added local section-level Sources blocks to README/use-case/activation/responsibility router files, bumped this register to Doc version: v0.6.0 and kept full root coverage deferred.
- ROOT-SRC-2B added local section-level Sources blocks to the protocol/role files, bumped this register to Doc version: v0.7.0 and kept full root coverage deferred.
- Kept this register as skeleton / not fully audited for root files outside the source-governance, router/onboarding, protocol/role and output/archive core.
- Did not add Doc version to Goal Map/Tampermonkey/helper files outside ROOT-SRC-3A.
- Did not edit domain aggregate drafts, value object drafts or slice drafts.
- ROOT-SRC-3A added output/archive workflow source coverage, bumped this register to Doc version: v0.8.0 and kept Goal Map/Tampermonkey rows deferred.
- ROOT-FULL-0 added root folder inventory/classification, bumped this register to Doc version: v0.9.0 and did not claim source coverage for deferred active files.
```
