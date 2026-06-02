# Root Source Sync Register

Status: skeleton / root planning dependency register not fully audited  
Doc version: v0.2.0  
Scope: source dependency skeleton for root planning workflow, routing, source-governance, Goal Map discovery and Tampermonkey command-helper files

## 1. Purpose

This register tracks root-level planning files whose behavior depends on other planning files, workflows, templates, registers, layer docs or helper projection files.

It is a skeleton register. It exists to make the root dependency model explicit before broad slice-side source cascade work starts.

This register does not claim complete source coverage yet.

After DISC-GM-TM1, this register also names the root discovery dependencies for:

```text
- living Goal Map maintenance;
- Tampermonkey inserted command interpretation;
- explicit review-diff-file archive routing;
- root onboarding / workflow activation / responsibility routing.
```

## 2. Register Rules

```text
- This register covers root planning/workflow/router/source-governance files and command-helper discovery files.
- It does not cover domain aggregate semantics themselves.
- It does not cover slice-layer source dependencies before the slice register exists.
- Register rows are skeleton rows until the listed file receives a local source pass or explicit file-level dependency audit.
- Do not treat a skeleton row as proof that every dependency was checked.
- Use Doc version when a source declares it.
- Use version not declared / needs-version when a source has no Doc version in the current checked state.
- Use implementation/helper source, version not applicable for userscript code unless a separate implementation version policy is introduced.
- Keep domain-layer semantic dependencies in planning/domain/domain-source-sync-register.md.
- Keep future slice-layer dependencies in planning/slices/slice-source-sync-register.md once that register exists.
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
This file is still skeleton.
SRC-AUD-ROOT-1 updates candidate rows after DISC-GM-TM1, but does not claim full root coverage.
```

## 3. Root Files With Source Dependencies

| Root file | Role | Current version/status | Register coverage state | Notes |
|---|---|---|---|---|
| `planning/source-cascade-sync-workflow.md` | source cascade workflow | Doc version: v0.2.0 | skeleton row added | Root workflow that defines local `Sources:` blocks, register states and layer register rules. |
| `planning/source-usage-cascade-profile.md` | Enman project profile | Doc version: v0.1.0 | skeleton row added | Project-specific source/consumer categories, row shape and cascade triggers. |
| `planning/SOURCE-SECTION-SOURCES-TEMPLATE.md` | local section `Sources:` block template | Doc version: v0.1.0 | candidate row only | General fenced `Sources:` block format. |
| `planning/root-source-sync-register.md` | root dependency register | Doc version: v0.2.0 | self row updated | Skeleton register; not proof of full root coverage. |
| `planning/README.md` | planning navigation and onboarding entrypoint | version not confirmed in this register | candidate row added | Now points new chats to Goal Map and Tampermonkey command-helper discovery docs. |
| `planning/planning-use-case-map.md` | root action-to-doc router | version not confirmed in this register | candidate row updated | Owns command routing, including `давай архив`, explicit review-diff-file archive route, Goal Map and Tampermonkey discovery behavior. |
| `planning/workflow-activation-map.md` | workflow activation router | version not confirmed in this register | candidate row updated | Activates Goal Map and Tampermonkey command-helper chains. |
| `planning/planning-doc-responsibility-map.md` | root planning responsibility router | version not confirmed in this register | candidate row updated | Owns root responsibility placement for Goal Map / Tampermonkey files. |
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

| Consumer file | Consumer scope | Source files | Source role | Source version/status | Sync status | Review outcome | Notes |
|---|---|---|---|---|---|---|---|
| `planning/source-cascade-sync-workflow.md` | source cascade rules and register state model | `planning/SOURCE-SECTION-SOURCES-TEMPLATE.md`; `planning/domain/AGGREGATE-SECTION-SOURCES-TEMPLATE.md`; `planning/slices/SERVER-SLICE-SECTION-SOURCES-TEMPLATE.md`; `planning/source-usage-cascade-profile.md`; `planning/root-source-sync-register.md`; `planning/domain/domain-source-sync-register.md`; planned `planning/slices/slice-source-sync-register.md` | format/process + register-index | mixed: Doc version where declared; slice register planned | skeleton | local/file-level audit needed | Workflow distinguishes current domain/root registers from planned slice register. |
| `planning/source-usage-cascade-profile.md` | Enman source/consumer categories and row conventions | `planning/documentation/field-kits/source-usage-cascade-field-kit.md`; `planning/source-cascade-sync-workflow.md`; `planning/root-source-sync-register.md`; `planning/domain/domain-source-sync-register.md`; planned `planning/slices/slice-source-sync-register.md`; `planning/documentation/examples/project-specific/enman/SOURCE-USAGE-CASCADE-ENMAN-SCENARIO-EXAMPLE.md` | format/process + project profile/example | mixed; not fully audited in this batch | skeleton | local/file-level audit needed | Profile includes register coverage model and root/router consumer category. |
| `planning/SOURCE-SECTION-SOURCES-TEMPLATE.md` | fenced `Sources:` block format | `planning/source-cascade-sync-workflow.md`; scenario DATA rule; local `Sources:` block rules | format/process | Doc version known for workflow; template declares Doc version | skeleton | not reviewed in detail in this batch | Candidate row only. |
| `planning/README.md` | onboarding, root navigation and command-helper discovery | `planning/workflow-activation-map.md`; `planning/planning-use-case-map.md`; `planning/planning-doc-responsibility-map.md`; `planning/goal-map-principles-workflow-template.md`; `planning/workstreams/command-system-and-tampermonkey-goal-map.md`; `planning/workstreams/tampermonkey-command-projection-plan.md`; `tools/tampermonkey/README.md` | navigation/process + workstream discovery | mixed; several root files have version not confirmed | skeleton | updated candidate row after DISC-GM-TM1 | New chat discovery now includes living Goal Map and helper projection docs. |
| `planning/planning-use-case-map.md` | action-to-doc-flow routing and command semantics | `planning/README.md`; `planning/workflow-activation-map.md`; `planning/planning-doc-responsibility-map.md`; `planning/source-cascade-sync-workflow.md`; `planning/source-usage-cascade-profile.md`; `planning/root-source-sync-register.md`; `planning/domain/domain-source-sync-register.md`; `planning/replacement-file-generation-guide.md`; `planning/documentation/review-diff-file-workflow.md`; `planning/documentation/reviewable-agent-output-and-commands-workflow.md`; `planning/goal-map-principles-workflow-template.md`; `planning/workstreams/command-system-and-tampermonkey-goal-map.md`; `planning/workstreams/tampermonkey-command-projection-plan.md`; layer readme/workflow files by use case | routing/process + command source-of-truth | mixed; root/router versions not fully confirmed | skeleton | root router audit needed | Keeps ordinary `давай архив` as clipboard diff and explicit `давай архив с review diff file` as separate route. |
| `planning/workflow-activation-map.md` | workflow activation and implicit workflow chains | `planning/planning-agent-protocol.md`; `planning/planning-use-case-map.md`; `planning/planning-doc-responsibility-map.md`; `planning/goal-map-principles-workflow-template.md`; `planning/workstreams/command-system-and-tampermonkey-goal-map.md`; `planning/workstreams/tampermonkey-command-projection-plan.md`; `tools/tampermonkey/README.md`; layer workflow files listed in the activation table | workflow routing/process | mixed; not fully version-audited | skeleton | workflow activation audit needed | DISC-GM-TM1 added Goal Map and Tampermonkey command-helper activation chains. |
| `planning/planning-doc-responsibility-map.md` | root planning responsibility routing | documentation responsibility map; scenario/domain/slice/local responsibility maps; `planning/goal-map-principles-workflow-template.md`; `planning/workstreams/command-system-and-tampermonkey-goal-map.md`; `planning/workstreams/tampermonkey-command-projection-plan.md`; `tools/tampermonkey/README.md`; `tools/tampermonkey/IMPLEMENTATION-NOTES.md`; `tools/tampermonkey/chat-command-palette.user.js` | responsibility routing/process | mixed; not fully version-audited | skeleton | responsibility map audit needed | Now includes command-system / Goal Map / Tampermonkey ownership anchors. |
| `planning/goal-map-principles-workflow-template.md` | Goal Map rules, brief shape and map sync command | `planning/planning-use-case-map.md`; `planning/documentation/reviewable-agent-output-and-commands-workflow.md`; `planning/goal-map-example.md`; `planning/documentation/examples/GOAL-MAP-BRIEF-RESPONSE-EXAMPLE.md`; `planning/documentation/examples/GOAL-MAP-SYNC-COMMAND-EXAMPLE.md`; relevant living Goal Map files | workflow/template + response-shape sources | mixed; not fully version-audited | skeleton | Goal Map owner audit needed | Added new-chat discovery rule; durable state lives in living Goal Maps, not in brief output. |
| `planning/workstreams/command-system-and-tampermonkey-goal-map.md` | living state for command-system/Tampermonkey workstream | `planning/goal-map-principles-workflow-template.md`; `planning/planning-use-case-map.md`; `planning/documentation/examples/README.md`; `planning/workstreams/tampermonkey-command-projection-plan.md`; `tools/tampermonkey/README.md`; `tools/tampermonkey/chat-command-palette.user.js`; `planning/replacement-file-generation-guide.md`; `planning/documentation/review-diff-file-workflow.md` | living-state/workstream source + route/process references | mixed; not fully version-audited | skeleton | workstream map audit needed | Snapshot currently points to SL-6 helper smoke testing after DISC-GM-TM1. |
| `planning/workstreams/tampermonkey-command-projection-plan.md` | helper command projection and prompt envelope | `planning/planning-use-case-map.md`; `planning/replacement-file-generation-guide.md`; `planning/documentation/review-diff-file-workflow.md`; `planning/goal-map-principles-workflow-template.md`; `planning/documentation/examples/README.md`; `tools/tampermonkey/README.md`; `tools/tampermonkey/chat-command-palette.user.js` | projection/process + command-route references | mixed; not fully version-audited | skeleton | projection plan audit needed | Helper profiles are route hints; UCM and owner workflows remain authoritative. |
| `tools/tampermonkey/README.md` | helper orientation and inserted-command interpretation | `planning/workstreams/tampermonkey-command-projection-plan.md`; `planning/planning-use-case-map.md`; `tools/tampermonkey/IMPLEMENTATION-NOTES.md`; `tools/tampermonkey/chat-command-palette.user.js` | implementation orientation + projection reference | implementation docs; version not declared | skeleton | helper docs audit needed | Not command source of truth. |
| `tools/tampermonkey/chat-command-palette.user.js` | userscript command profile implementation | `planning/workstreams/tampermonkey-command-projection-plan.md`; `planning/planning-use-case-map.md`; `planning/replacement-file-generation-guide.md`; `planning/documentation/review-diff-file-workflow.md`; `planning/goal-map-principles-workflow-template.md` | implementation projection source | implementation/helper source, version not applicable | skeleton | code/profile audit needed | Current command profiles should remain projections of route/owner docs. |
| `planning/replacement-file-generation-guide.md` | replacement archive/package output rules | `planning/planning-use-case-map.md`; `planning/documentation/review-diff-file-workflow.md`; `planning/documentation/documentation-update-workflow.md`; target files for each package | output/process | mixed; guide version not confirmed in this register | skeleton | archive guide audit needed | Default `давай архив` remains saved diff copied to clipboard. |
| `planning/documentation/review-diff-file-workflow.md` | explicit repo-stored review diff transfer | `planning/replacement-file-generation-guide.md`; `planning/planning-use-case-map.md`; `_ai-review-diffs/last-archive.diff` when explicitly generated | explicit transfer workflow | mixed; workflow version not confirmed in this register | skeleton | explicit mode audit needed | Applies only when user explicitly requests repo-stored review diff transfer or approves switch. |

## 5. Cross-Layer Dependency Rows

| Root file | Layer/source dependency | Meaning | Current status | Next action |
|---|---|---|---|---|
| `planning/source-cascade-sync-workflow.md` | `planning/domain/domain-source-sync-register.md` | Domain register is current upstream index for aggregate/domain dependencies. | current / synchronized on domain side | Keep wording current; do not call it planned. |
| `planning/source-cascade-sync-workflow.md` | `planning/slices/slice-source-sync-register.md` | Slice register is intended future layer register. | planned / not created | Create skeleton before broad slice refactor. |
| `planning/planning-use-case-map.md` | `planning/domain/domain-source-sync-register.md` | Source-cascade/domain rows should route domain dependency review through the domain register. | already routed for source/domain rows; slice rows still need review | Review slice rows before first slice refactor. |
| `planning/README.md` / `planning/workflow-activation-map.md` / `planning/planning-doc-responsibility-map.md` | Goal Map owner + living command-system Goal Map | New-chat and long-running workstream discovery now depend on these files. | candidate rows added after DISC-GM-TM1 | Later decide whether root docs need file-level source audits or local Sources blocks. |
| `planning/planning-use-case-map.md` / `planning/workstreams/tampermonkey-command-projection-plan.md` / `tools/tampermonkey/chat-command-palette.user.js` | replacement archive guide + explicit review-diff workflow | Helper command profiles and root routes distinguish default clipboard diff from explicit review-diff-file mode. | candidate rows added after TM12D/DISC-GM-TM1 | Keep ordinary `давай архив` and explicit review-diff-file route separate. |
| `planning/source-usage-cascade-profile.md` | SC-13D -> domain -> slice chain | Preserved first practical source-cascade chain. | pilot not filled | Decide after root/slice skeleton sync. |

## 6. Not Checked / Deferred

```text
- Full root planning file audit.
- Full Doc version seed for root planning/router/helper docs.
- Local section Sources blocks for root planning files, where useful.
- `planning/README.md` file-level dependency audit.
- `planning/planning-use-case-map.md` full dependency row audit.
- `planning/workflow-activation-map.md` dependency audit.
- `planning/planning-doc-responsibility-map.md` dependency audit.
- Goal Map owner/workstream map dependency audit.
- Tampermonkey projection plan and userscript profile audit against every root route.
- Domain value object local `Sources:` pass.
- `planning/slices/slice-source-sync-register.md` creation.
- Slice read order/template sync with the domain register.
- Testing layer source-sync register.
```

## 7. Source Delta / Change Log

```text
- SRC-AUD-ROOT-1 updated this root source-sync register after DISC-GM-TM1.
- Bumped this register to Doc version: v0.2.0 because the root dependency skeleton now covers Goal Map and Tampermonkey command-helper discovery dependencies.
- Added candidate rows for `planning/README.md`, `planning/planning-doc-responsibility-map.md`, Goal Map owner/living map files, Tampermonkey projection plan, helper docs and userscript.
- Updated root dependency rows to preserve TM12D archive policy: ordinary `давай архив` uses saved diff copied to clipboard; review-diff-file mode is explicit-only.
- Kept this register as skeleton / not fully audited.
- Did not add Doc version to unversioned root/router/helper files.
- Did not add local section `Sources:` blocks to root files.
- Did not edit domain aggregate drafts, value object drafts, slice drafts or source-cascade workflow rules.
```
