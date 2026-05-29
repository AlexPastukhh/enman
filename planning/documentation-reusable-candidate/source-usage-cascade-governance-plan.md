# Source Usage Cascade Governance Plan

Status: pilot framework / not full workflow  
Scope: source usage relationships, layer encapsulation, attention preservation and cascade-review pilot planning

## 1. Purpose

This plan explains how to pilot source usage and cascade review before creating a full source-version-cascade workflow.

It exists because planning docs are built as a layered pipeline:

```text
scenario sources
  -> DATA / behavior item sets
  -> domain discovery and domain drafts
  -> slice discovery and slice drafts
  -> testing / verification planning
  -> implementation evidence and VKR/thesis output when relevant
```

The goal is not file versioning by itself.

The goal is to preserve layer encapsulation and human review attention: when upstream work has already been reviewed, downstream docs should reference published upstream artifacts instead of reconstructing the upstream reasoning.

## 2. Current State

Current architecture principles already define:

```text
- layer dependency direction;
- source-of-truth hierarchy;
- layer encapsulation and attention preservation;
- source/version principle;
- dependency cascade principle;
- section-level sources principle.
```

The missing part is operational evidence from a real pilot:

```text
- what a source usage row should contain;
- how granular source scopes need to be;
- how downstream consumer scopes should be reviewed;
- when metadata-only review is enough;
- where permanent registers should live.
```

## 3. Why This Is Not The Full Workflow Yet

Do not create or activate a full cascade workflow from abstract assumptions.

This file is a governance plan and pilot frame. It does not replace a future workflow such as:

```text
planning/documentation/source-version-cascade-sync-workflow.md
```

That workflow should be created only after at least one real source usage register pilot proves the row shape and review process.

## 4. Core Model

The key unit is not a standalone file version.

The key unit is a source usage relationship:

```text
source artifact/scope
  -> consumer artifact/scope
  -> reviewed_against / sync_status / review_outcome when needed
```

This is relationship-first, not version-first.

Version markers exist to support review and cascade synchronization. They are not the source of truth by themselves.

## 5. Terms

```text
Source artifact
  Canonical or accepted upstream file, section, table row, register entry, artifact-map row or workflow rule that can affect downstream docs.

Source scope
  The exact part of the source artifact consumed by a downstream file or section.

Source granularity
  file / section / table row / register entry / artifact-map row / behavior item group / workflow rule.

Source status
  State of the source itself: current / draft / transitional / historical / deprecated.

Consumer artifact
  Downstream file or section that uses, maps, summarizes, derives from, tests, visualizes, formats or is constrained by the source.

Consumer scope
  The exact section, table, row or local block inside the consumer artifact.

Usage type
  derives-from / maps-to / summarizes / validates / tests / visualizes / routes / formats / constrains.

Reviewed against
  Lightweight marker of the source state the consumer was checked against: commit SHA, date, source marker, artifact-map status, source heading or future explicit version.

Sync status
  State of the consumer relative to the source: current / needs-review / partial / stale / blocked / unknown / not-applicable.

Review outcome
  What happened during review: not-reviewed / reviewed-no-change / updated / metadata-only / follow-up-needed / blocked.

Cascade trigger
  Source change type that should cause the consumer scope to be reviewed.
```

## 6. External Dependency Rule

External dependencies cross file or layer boundaries.

Use source usage rows when downstream synchronization matters:

```text
SC-13D behavior item
  -> domain aggregate draft method section

Domain aggregate method section
  -> server slice Domain Behavior section

Slice Behavior Coverage row
  -> Test / Verification Plan row
```

External dependency rows should help a future chat identify affected downstream scopes without reading every downstream file from scratch.

## 7. Internal Section Dependency Rule

Internal section dependencies are same-file analytical dependencies.

They normally do not need version markers.

Use local section-level source/internal dependency blocks when a section is high-risk, separately reviewed or depends on earlier sections.

Example:

```text
In a domain aggregate draft:
- Domain Methods depend on Aggregate Boundary and Owned State.
- Invariants depend on Domain Methods and Value Objects.
- Behavior Coverage depends on Methods, Invariants and Lifecycle.
```

If a foundational section changes, review the whole file. If a later section has explicit internal dependencies, review it when those dependencies change.

Do not put internal section dependencies into an external source usage register unless that section is consumed by another file.

## 8. Source Usage Row Shape

Pilot registers should start with this table shape:

```md
| ID | Source artifact | Source scope | Source status | Consumer artifact | Consumer scope | Usage type | Reviewed against | Sync status | Review outcome | Cascade trigger | Notes |
|---|---|---|---|---|---|---|---|---|---|---|---|
```

The shape is pilot-level. Do not copy it into every template before the pilot proves it.

## 9. Source Status vs Sync Status

Source status and sync status are different.

```text
source_status
  describes the source artifact itself.

sync_status
  describes whether the consumer is current relative to that source.
```

Examples:

```text
source_status = current
sync_status = needs-review

source_status = transitional
sync_status = current
```

## 10. Reviewed Against Rule

Use `reviewed_against` before inventing heavy semantic versioning.

Acceptable pilot markers:

```text
- commit SHA or short SHA;
- source path + section heading;
- artifact-map status row;
- date + source status;
- explicit source marker if one already exists.
```

Do not add section-level versions everywhere during the pilot.

## 11. Source Change Detection Modes

Source changes may be detected from:

```text
GitHub/direct commit source change
  Compare changed files/sections from the repository.

Replacement archive source change
  Use the reviewed archive diff and post-apply preservation check.

Local unpushed source change
  Do not claim remote/current repo sync until pushed or archive diff is reviewed.

Manual/user-described source change
  Treat as unverified until the source artifact is read.
```

## 12. Pilot Scope

Initial pilot:

```text
SC-13D / AgreementProposalExchange source usage pilot
```

Target chain:

```text
SC-13D scenario text / DATA / behavior items
  -> scenario artifact/currentness map
  -> scenario-to-aggregate map
  -> AgreementProposalExchange aggregate draft
  -> related value object drafts
  -> SL-AGR-EXCH-001 server slice draft
  -> slice Behavior Coverage and Test / Verification Plan sections
```

The first skeleton file is:

```text
planning/documentation/source-usage-pilots/SC-13D-agreement-exchange-source-usage-register.md
```

## 13. Pilot Review Process

For the real pilot fill pass:

```text
1. Read the source artifacts for the selected SC-13D chain.
2. Identify source scopes that are actually consumed downstream.
3. Identify consumer artifacts and consumer scopes.
4. Add source usage rows only where review/cascade value is real.
5. Mark unknowns as unknown/blocked instead of inventing precision.
6. Simulate one upstream change and verify whether affected consumers can be found.
7. Record whether the table shape was sufficient or too heavy.
```

Review should not mean reading every downstream file by default.

Review starts from source usage rows, then opens only affected consumer scopes and necessary internal dependencies.

## 14. Cascade Review Output

A cascade review should report:

```text
- upstream source changed;
- source scope affected;
- affected consumers;
- rows reviewed;
- rows updated;
- rows marked needs-review / stale / blocked;
- content changes required / not required;
- follow-up PMR/register tasks;
- files intentionally not checked.
```

## 15. Exit Criteria For Full Workflow

Create the full source usage/cascade workflow only after:

```text
- at least one real pilot register is filled from current repo sources;
- at least one cascade review simulation is performed;
- row shape is validated or revised;
- permanent register placement is decided or narrowed;
- domain/slice/template impact is understood.
```

## 16. What Not To Do Yet

```text
- Do not create the full source-version-cascade-sync workflow yet.
- Do not update workflow-activation-map.md as if the workflow exists.
- Do not add source-version fields to every use-case row.
- Do not add section-level versions everywhere.
- Do not turn scenario-artifact-map.md into a full source usage register.
- Do not turn scenario-to-aggregate-map.md into a full cascade register.
- Do not turn slice drafts into source registries.
- Do not mass-update domain/slice templates before the pilot proves the shape.
```

## 17. Maintenance And Action Log Tracking

The full source usage/cascade workflow follow-up is tracked by PMR-002.

This framework introduction should be recorded in:

```text
planning/documentation/documentation-action-log.md
```

Rules:

```text
- Use PMR for future work, waiting conditions and reminders.
- Use the documentation action log for completed logical documentation actions and why they happened.
- When a pilot fill proves or rejects this row shape, update the action log.
- When the pilot creates future work, update PMR and reference the PMR relation from the action log.
```
