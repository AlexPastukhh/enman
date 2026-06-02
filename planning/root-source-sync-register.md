# Root Source Sync Register

Status: skeleton / root planning dependency register partially derived for source-governance core and root router/onboarding core
Doc version: v0.6.0
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
    - full root planning file audit outside ROOT-SRC-2A scope
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
    - full root planning file audit outside ROOT-SRC-2A scope
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
    - root output/Goal Map/Tampermonkey/protocol-role rows outside ROOT-SRC-2A scope
    - planned planning/slices/slice-source-sync-register.md does not exist yet
```

| Root file | Role | Current version/status | Register coverage state | Notes |
|---|---|---|---|---|
| `planning/source-cascade-sync-workflow.md` | source cascade workflow | Doc version: v0.4.0 | derived from local section Sources in ROOT-SRC-1; rule updated in SRC-DEP-CMD-1 | Root workflow that defines local `Sources:` blocks, register states, layer register rules, structured-file local Sources requirement and explicit source dependency/link declaration rule. |
| `planning/source-usage-cascade-profile.md` | Enman project profile | Doc version: v0.2.0 | derived from local section Sources in ROOT-SRC-1; triggers updated in SRC-DEP-CMD-1 | Project-specific source/consumer categories, row shape and cascade triggers. |
| `planning/SOURCE-SECTION-SOURCES-TEMPLATE.md` | local section `Sources:` block template | Doc version: v0.2.0 | derived from local section Sources in ROOT-SRC-1; source labels refreshed in SRC-DEP-CMD-1 | General fenced `Sources:` block format with version/status labels. |
| `planning/root-source-sync-register.md` | root dependency register | Doc version: v0.6.0 | self row partially synchronized for ROOT-SRC-1/SRC-DEP-CMD-1/ROOT-SRC-2A scope | Still skeleton for full root coverage; source-governance core and root router/onboarding core rows are derived/synchronized. |
| `planning/README.md` | planning navigation and onboarding entrypoint | Doc version: v0.1.0 | derived from local section Sources in ROOT-SRC-2A | Points new chats to activation/use-case/responsibility routing, Goal Map and Tampermonkey command-helper discovery docs. |
| `planning/planning-use-case-map.md` | root action-to-doc router | Doc version: v0.2.0 | derived from local section Sources in ROOT-SRC-2A; source dependency route added in SRC-DEP-CMD-1 | Owns command routing, including `давай архив`, explicit review-diff-file archive route, Goal Map, Tampermonkey discovery behavior and source dependency/link declaration routing. |
| `planning/workflow-activation-map.md` | workflow activation router | Doc version: v0.1.0 | derived from local section Sources in ROOT-SRC-2A | Activates workflow preflight, root routing, Goal Map and Tampermonkey command-helper chains. |
| `planning/planning-doc-responsibility-map.md` | root planning responsibility router | Doc version: v0.1.0 | derived from local section Sources in ROOT-SRC-2A | Owns root layer placement and responsibility routing, including Goal Map / Tampermonkey responsibility anchors. |
| `planning/goal-map-principles-workflow-template.md` | Goal Map owner workflow/template | version not confirmed in this register | candidate row added | Owns living Goal Map rules, brief shape and `синх карта`. |
| `planning/workstreams/command-system-and-tampermonkey-goal-map.md` | living command-system/Tampermonkey Goal Map | version not confirmed in this register | candidate row added | Workstream state artifact; not a generic workflow. |
| `planning/workstreams/tampermonkey-command-projection-plan.md` | Tampermonkey command projection plan | version not confirmed in this register | candidate row added | Defines helper prompt body projection model. |
| `tools/tampermonkey/README.md` | Tampermonkey helper orientation | version not declared / implementation docs | candidate row added | Not command source of truth. |
| `tools/tampermonkey/IMPLEMENTATION-NOTES.md` | Tampermonkey implementation notes | version not declared / implementation docs | candidate row added | Working notes; not command source of truth. |
| `tools/tampermonkey/chat-command-palette.user.js` | userscript implementation | implementation/helper source, version not applicable | candidate row added | Last in source-of-truth order; must not invent command semantics. |
| `planning/replacement-file-generation-guide.md` | replacement package guidance | version not confirmed in this register | candidate row updated | Owns default clipboard diff archive review flow. |
| `planning/documentation/review-diff-file-workflow.md` | explicit repo-stored review diff workflow | version not confirmed in this register | candidate row added | Used only for explicit review-diff-file archive mode. |
| `planning/documentation-action-log.md` | action log / historical record | version not confirmed in this register | candidate row only | Log is an evidence trail, not a workflow source by itself. |
| `planning/planning-agent-protocol.md` | planning agent protocol | version not confirmed in this register | candidate row only | Needs audit before claiming source coverage. |

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
    - root output/Goal Map/Tampermonkey/protocol-role rows outside ROOT-SRC-2A scope
    - planned planning/slices/slice-source-sync-register.md does not exist yet
```

| Consumer file | Consumer scope | Source files | Source role | Source version/status | Sync status | Review outcome | Notes |
|---|---|---|---|---|---|---|---|
| `planning/source-cascade-sync-workflow.md` | source cascade rules and register state model | `planning/SOURCE-SECTION-SOURCES-TEMPLATE.md`; `planning/domain/AGGREGATE-SECTION-SOURCES-TEMPLATE.md`; `planning/slices/SERVER-SLICE-SECTION-SOURCES-TEMPLATE.md`; `planning/source-usage-cascade-profile.md`; `planning/root-source-sync-register.md`; `planning/domain/domain-source-sync-register.md`; planned `planning/slices/slice-source-sync-register.md` | format/process + register-index | source workflow Doc version: v0.4.0; general template Doc version: v0.2.0; profile Doc version: v0.2.0; domain register Doc version: v0.2.0; slice register planned | derived/synchronized for ROOT-SRC-1 local Sources scope; rule updated in SRC-DEP-CMD-1 | explicit source dependency/link rule added; source-block metadata refreshed | Workflow has local section Sources blocks, distinguishes current domain/root registers from planned slice register and classifies file-to-file/section-to-file dependencies. |
| `planning/source-usage-cascade-profile.md` | Enman source/consumer categories and row conventions | `planning/documentation/field-kits/source-usage-cascade-field-kit.md`; `planning/source-cascade-sync-workflow.md`; `planning/root-source-sync-register.md`; `planning/domain/domain-source-sync-register.md`; planned `planning/slices/slice-source-sync-register.md`; `planning/documentation/examples/project-specific/enman/SOURCE-USAGE-CASCADE-ENMAN-SCENARIO-EXAMPLE.md` | format/process + project profile/example | source workflow Doc version: v0.4.0; root register Doc version: v0.5.0; domain register Doc version: v0.2.0; field-kit/example versions not confirmed; slice register planned | derived/synchronized for ROOT-SRC-1 local Sources scope; triggers updated in SRC-DEP-CMD-1 | source dependency/link trigger added; source-block metadata refreshed | Profile has local section Sources blocks for categories, register model, triggers, pilot and related files. |
| `planning/SOURCE-SECTION-SOURCES-TEMPLATE.md` | fenced `Sources:` block format | `planning/source-cascade-sync-workflow.md`; `planning/source-usage-cascade-profile.md`; local `Sources:` block rules | format/process | source workflow Doc version: v0.4.0; profile Doc version: v0.2.0; template Doc version: v0.2.0 | derived/synchronized for ROOT-SRC-1 local Sources scope; source labels refreshed in SRC-DEP-CMD-1 | no semantic change; source-block metadata refreshed | Template has minimal local Sources blocks for template, field meaning, rules and source delta sections. |
| `planning/README.md` | onboarding, root navigation and command-helper discovery | `planning/workflow-activation-map.md`; `planning/planning-use-case-map.md`; `planning/planning-doc-responsibility-map.md`; `planning/source-cascade-sync-workflow.md`; `planning/source-usage-cascade-profile.md`; `planning/root-source-sync-register.md`; `planning/goal-map-principles-workflow-template.md`; `planning/workstreams/command-system-and-tampermonkey-goal-map.md`; `planning/workstreams/tampermonkey-command-projection-plan.md`; `tools/tampermonkey/README.md` | navigation/process + source-governance + workstream discovery | README Doc version: v0.1.0; activation/responsibility root files Doc version: v0.1.0; use-case map Doc version: v0.2.0; Goal Map/Tampermonkey sources not fully audited | derived/synchronized for ROOT-SRC-2A local Sources scope | local Sources added; no navigation semantics changed | New chat discovery includes living Goal Map and helper projection docs; Goal Map/Tampermonkey rows remain deferred outside ROOT-SRC-2A. |
| `planning/planning-use-case-map.md` | action-to-doc-flow routing and command semantics | `planning/README.md`; `planning/workflow-activation-map.md`; `planning/planning-doc-responsibility-map.md`; `planning/source-cascade-sync-workflow.md`; `planning/SOURCE-SECTION-SOURCES-TEMPLATE.md`; `planning/source-usage-cascade-profile.md`; `planning/root-source-sync-register.md`; `planning/domain/domain-source-sync-register.md`; `planning/replacement-file-generation-guide.md`; `planning/documentation/review-diff-file-workflow.md`; `planning/documentation/reviewable-agent-output-and-commands-workflow.md`; `planning/goal-map-principles-workflow-template.md`; `planning/workstreams/command-system-and-tampermonkey-goal-map.md`; `planning/workstreams/tampermonkey-command-projection-plan.md`; layer readme/workflow files by use case | routing/process + command source-of-truth | use-case map Doc version: v0.2.0; source-governance versions current; mixed versions/statuses for output/Goal Map/Tampermonkey owners | derived/synchronized for ROOT-SRC-2A local Sources scope | local Sources added; existing command routes preserved | Keeps ordinary `давай архив` as clipboard diff and explicit `давай архив с review diff file` as separate route; routes source dependency/link commands to source-cascade workflow/template/profile and relevant registers. |
| `planning/workflow-activation-map.md` | workflow activation and implicit workflow chains | `planning/README.md`; `planning/planning-agent-protocol.md`; `planning/agent-roles-and-required-actions.md`; `planning/planning-use-case-map.md`; `planning/planning-doc-responsibility-map.md`; `planning/source-cascade-sync-workflow.md`; `planning/root-source-sync-register.md`; `planning/goal-map-principles-workflow-template.md`; `planning/workstreams/command-system-and-tampermonkey-goal-map.md`; `planning/workstreams/tampermonkey-command-projection-plan.md`; `tools/tampermonkey/README.md`; layer workflow files listed in the activation table | workflow routing/process | activation map Doc version: v0.1.0; README/responsibility root files Doc version: v0.1.0; use-case map Doc version: v0.2.0; protocol/role versions not confirmed | derived/synchronized for ROOT-SRC-2A local Sources scope | local Sources added; registry semantics preserved | Activates Goal Map and Tampermonkey command-helper chains; protocol/role source pass deferred to ROOT-SRC-2B. |
| `planning/planning-doc-responsibility-map.md` | root planning responsibility routing | `planning/README.md`; `planning/planning-use-case-map.md`; `planning/workflow-activation-map.md`; documentation/scenario/domain/slice/local responsibility maps; `planning/goal-map-principles-workflow-template.md`; `planning/workstreams/command-system-and-tampermonkey-goal-map.md`; `planning/workstreams/tampermonkey-command-projection-plan.md`; `tools/tampermonkey/README.md`; `tools/tampermonkey/IMPLEMENTATION-NOTES.md`; `tools/tampermonkey/chat-command-palette.user.js` | responsibility routing/process | responsibility map Doc version: v0.1.0; root router sources versioned; local layer responsibility maps mixed/not fully audited | derived/synchronized for ROOT-SRC-2A local Sources scope | local Sources added; responsibility semantics preserved | Includes command-system / Goal Map / Tampermonkey ownership anchors; local layer responsibility passes remain deferred. |
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
    - root output/Goal Map/Tampermonkey/protocol-role rows outside ROOT-SRC-2A scope
    - planned planning/slices/slice-source-sync-register.md does not exist yet
```

| Root file | Layer/source dependency | Meaning | Current status | Next action |
|---|---|---|---|---|
| `planning/source-cascade-sync-workflow.md` | `planning/domain/domain-source-sync-register.md` | Domain register is current upstream index for aggregate/value-object dependencies. | current / synchronized on domain side | Keep wording current; do not call it planned. |
| `planning/source-cascade-sync-workflow.md` | `planning/slices/slice-source-sync-register.md` | Slice register is intended future layer register. | planned / not created | Create skeleton before broad slice refactor. |
| `planning/planning-use-case-map.md` | `planning/domain/domain-source-sync-register.md` | Source-cascade/domain rows should route domain dependency review through the domain register. | already routed for source/domain rows; slice rows still need review | Review slice rows before first slice refactor. |
| `planning/planning-use-case-map.md` | `planning/source-cascade-sync-workflow.md` / `planning/SOURCE-SECTION-SOURCES-TEMPLATE.md` / `planning/source-usage-cascade-profile.md` | Explicit source dependency/link commands should route through the source-cascade workflow/template/profile and then the relevant layer/root register. | derived/synchronized in ROOT-SRC-2A for the router row | Local Sources were added to the use-case map; later non-router root files still need separate audits. |
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
    - root output/Goal Map/Tampermonkey/protocol-role rows outside ROOT-SRC-2A scope
    - planned planning/slices/slice-source-sync-register.md does not exist yet
```

```text
- Full root planning file audit.
- Full Doc version seed for root planning/router/helper docs.
- Local section Sources blocks for root planning files outside ROOT-SRC-1 scope.
- `planning/README.md` file-level dependency audit.
- `planning/planning-use-case-map.md` full dependency row audit.
- `planning/workflow-activation-map.md` dependency audit.
- `planning/planning-doc-responsibility-map.md` dependency audit.
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
    - root output/Goal Map/Tampermonkey/protocol-role rows outside ROOT-SRC-2A scope
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
- Kept this register as skeleton / not fully audited for root files outside the source-governance core.
- Did not add Doc version to unversioned root/router/helper files.
- Did not edit domain aggregate drafts, value object drafts or slice drafts.
```
