# Root Source Sync Register

Status: skeleton / root planning dependency register not fully audited  
Doc version: v0.1.0  
Scope: source dependency skeleton for root planning workflow, routing and source-governance files

## 1. Purpose

This register tracks root-level planning files whose behavior depends on other planning files, workflows, templates, registers or layer docs.

It is a skeleton register. It exists to make the root dependency model explicit before the slice-side source cascade work starts.

This register does not claim complete source coverage yet.

## 2. Register Rules

```text
- This register covers root planning/workflow/router/source-governance files, not domain aggregate content itself.
- Register rows are skeleton rows until the listed file receives a local source pass or explicit file-level dependency audit.
- Do not treat a skeleton row as proof that every dependency was checked.
- Use Doc version when a source declares it.
- Use version not declared / needs-version when a source has no Doc version in the current checked state.
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

## 3. Root Files With Source Dependencies

| Root file | Role | Current version/status | Register coverage state | Notes |
|---|---|---|---|---|
| `planning/source-cascade-sync-workflow.md` | source cascade workflow | Doc version: v0.2.0 | skeleton row added | Root workflow that defines local Sources, register states and layer register rules. |
| `planning/source-usage-cascade-profile.md` | Enman project profile | Doc version: v0.1.0 | skeleton row added | Project-specific source/consumer categories, row shape and cascade triggers. |
| `planning/SOURCE-SECTION-SOURCES-TEMPLATE.md` | local section Sources block template | Doc version: v0.1.0 | candidate row only | General fenced Sources block format. |
| `planning/planning-use-case-map.md` | root action-to-doc router | version not confirmed in this register | candidate row only | Known source-cascade router; needs root version/source pass. |
| `planning/workflow-activation-map.md` | workflow activation router | version not confirmed in this register | candidate row only | Needs audit before claiming source coverage. |
| `planning/planning-agent-protocol.md` | planning agent protocol | version not confirmed in this register | candidate row only | Needs audit before claiming source coverage. |
| `planning/replacement-file-generation-guide.md` | replacement package guidance | version not confirmed in this register | candidate row only | Needs audit before claiming source coverage. |
| `planning/documentation-action-log.md` | action log / historical record | version not confirmed in this register | candidate row only | Log is an evidence trail, not a workflow source by itself. |

## 4. Root Dependency Rows

| Consumer file | Consumer scope | Source files | Source role | Source version/status | Sync status | Review outcome | Notes |
|---|---|---|---|---|---|---|---|
| `planning/source-cascade-sync-workflow.md` | source cascade rules and register state model | `planning/SOURCE-SECTION-SOURCES-TEMPLATE.md`; `planning/domain/AGGREGATE-SECTION-SOURCES-TEMPLATE.md`; `planning/slices/SERVER-SLICE-SECTION-SOURCES-TEMPLATE.md`; `planning/source-usage-cascade-profile.md`; `planning/root-source-sync-register.md`; `planning/domain/domain-source-sync-register.md`; planned `planning/slices/slice-source-sync-register.md` | format/process + register-index | mixed: Doc version where declared; slice register planned | skeleton | local/file-level audit needed | Workflow now distinguishes current domain/root registers from planned slice register. |
| `planning/source-usage-cascade-profile.md` | Enman source/consumer categories and row conventions | `planning/documentation/field-kits/source-usage-cascade-field-kit.md`; `planning/source-cascade-sync-workflow.md`; `planning/root-source-sync-register.md`; `planning/domain/domain-source-sync-register.md`; planned `planning/slices/slice-source-sync-register.md`; `planning/documentation/examples/project-specific/enman/SOURCE-USAGE-CASCADE-ENMAN-SCENARIO-EXAMPLE.md` | format/process + project profile/example | mixed; not fully audited in this batch | skeleton | local/file-level audit needed | Profile now includes register coverage model and root/router consumer category. |
| `planning/SOURCE-SECTION-SOURCES-TEMPLATE.md` | fenced Sources block format | `planning/source-cascade-sync-workflow.md`; scenario DATA rule; local Sources block rules | format/process | Doc version known for workflow after this batch; template already declares Doc version | skeleton | not reviewed in detail in this batch | Candidate row only; no file edit in this batch. |
| `planning/planning-use-case-map.md` | action-to-doc-flow routing | `planning/README.md`; `planning/workflow-activation-map.md`; `planning/source-cascade-sync-workflow.md`; `planning/source-usage-cascade-profile.md`; `planning/root-source-sync-register.md`; `planning/domain/domain-source-sync-register.md`; layer readme/workflow files by use case | routing/process | version not confirmed for several root files | skeleton | root router audit needed | Do not claim complete routing sync from this skeleton row. |

## 5. Cross-Layer Dependency Rows

| Root file | Layer/source dependency | Meaning | Current status | Next action |
|---|---|---|---|---|
| `planning/source-cascade-sync-workflow.md` | `planning/domain/domain-source-sync-register.md` | Domain register is current upstream index for aggregate/domain dependencies. | current / synchronized on domain side | Keep wording current; do not call it planned. |
| `planning/source-cascade-sync-workflow.md` | `planning/slices/slice-source-sync-register.md` | Slice register is intended future layer register. | planned / not created | Create skeleton before broad slice refactor. |
| `planning/planning-use-case-map.md` | `planning/domain/domain-source-sync-register.md` | Source-cascade/domain rows should route domain dependency review through the domain register. | already routed for source/domain rows; slice rows still need review | Review slice rows before first slice refactor. |
| `planning/source-usage-cascade-profile.md` | SC-13D -> domain -> slice chain | Preserved first practical source-cascade chain. | pilot not filled | Decide after root/slice skeleton sync. |

## 6. Not Checked / Deferred

```text
- Full root planning file audit.
- Full Doc version seed for root planning files.
- Local section Sources blocks for root planning files, where useful.
- `planning/planning-use-case-map.md` full dependency row audit.
- `planning/workflow-activation-map.md` dependency audit.
- `planning/slices/slice-source-sync-register.md` creation.
- Slice read order/template sync with the domain register.
- Testing layer source-sync register.
```

## 7. Source Delta / Change Log

```text
- Created root source-sync register skeleton.
- Registered initial root source-cascade workflow/profile/router dependency candidates.
- Marked this register as skeleton, not complete coverage.
- Recorded current domain register and planned slice register states.
- No slice draft edited.
- No domain aggregate draft edited.
```
