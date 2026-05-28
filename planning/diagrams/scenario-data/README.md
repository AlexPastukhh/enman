# Scenario DATA Index

Status: current scenario DATA navigation index / reusable DATA concept and transitional sidecar owner  
Scope: reusable scenario DATA concepts, extracted DATA sidecars and migration notes

## 1. Purpose

Scenario DATA starts inline in the core/business scenario text spec.

DATA means scenario information: what the actor/user/system-facing scenario:

```text
- enters;
- sees;
- selects;
- filters/searches by;
- attaches/uploads;
- references as visible/selectable business item;
- receives as result/feedback information.
```

DATA comes from scenario information needs. It is not derived from UI layout.

This folder owns extracted reusable scenario DATA concepts and transitional DATA sidecars.

It is no longer mandatory to create a separate DATA file for every scenario.

## 2. When To Extract DATA To This Folder

Extract DATA to `planning/diagrams/scenario-data/` only when:

```text
- the same business data concept appears in multiple scenarios;
- the DATA concept is too large for one scenario text spec;
- the DATA concept needs separate review/audit;
- downstream domain/slice/client/testing drafts need a stable reusable reference;
- an existing sidecar already exists and has not yet been merged/reclassified.
```

Do not extract DATA only because a scenario exists.

Core scenario text should still keep a readable DATA summary even when reusable DATA concepts are extracted.

## 3. Ownership / Conflict Rule

```text
Core scenario owns local scenario meaning.

Reusable DATA sidecar owns shared concept shape.

If a scenario and sidecar conflict, record a reconciliation question.
Do not silently override the scenario with the sidecar.
```

Scenario-specific variation belongs in the scenario text or in a clearly marked variation section in the reusable DATA concept.

## 4. DATA / UI Boundary

DATA files answer:

```text
What information participates in this scenario or reusable scenario concept?
```

UI scenario files answer:

```text
How is that information presented, entered, selected, confirmed or visually/UX-wise handled?
```

A DATA item may include short UI/UX presentation notes when they clarify how the information is shown/entered/selected.

Example:

```text
DATA item:
- Request status

Meaning:
- current business status of the request.

UI/UX presentation:
- general pattern: visible status indicator / badge;
- exact color, placement and animation belong to the UI scenario/style rules.
```

Do not invent UI requirements for every DATA item.

Use one of:

```text
- none / no scenario-specific UI requirement;
- general pattern: form field / table column / card field / status badge / detail section / feedback message;
- specific: ...;
- see UI scenario: ...
```

Do not duplicate full UI scenario details in DATA files.

## 5. Reusable DATA Concept Shape

Use this shape when a DATA sidecar is a reusable concept instead of a transitional per-scenario sidecar:

```text
# Scenario DATA Concept — <Name>

Status:
Scope:

## 1. Purpose

## 2. Used By Scenarios

| Scenario | How used | Local variation |
|---|---|---|

## 3. Business Information

Entered:
- ...

Seen:
- ...

Selected / referenced:
- ...

Filtered / searched:
- ...

Attached / uploaded:
- ...

Outcome / feedback:
- ...

## 4. Scenario Variations

- ...

## 5. UI / UX Presentation Notes

No scenario-specific UI requirement by default.
General patterns:
- form / table / card / detail section / status badge / feedback block

Specific UI requirements:
- see related UI scenarios.

## 6. Downstream Notes

Domain:
- ...

Slice:
- ...

Client/testing:
- ...

## 7. Questions / Decisions
```

## 6. Transitional Existing DATA Files

Existing `scenario-data/*.md` files remain valid transitional artifacts until reviewed.

Do not delete, rename or rewrite them in policy/template updates.

Later reclassify each existing DATA sidecar as one of:

```text
- merge into core scenario text;
- keep as reusable DATA concept;
- keep as transitional sidecar;
- split into reusable concepts;
- mark historical.
```

## 7. Read First

```text
../scenario-responsibility-map.md
../scenario-artifact-map.md
../scenario-text-specs/README.md
../scenario-text-specs/SCENARIO-TEXT-SPEC-TEMPLATE.md
00-scenario-data-index.md
../scenario-questions-register.md
../scenario-behavior-items/README.md
../scenario-behavior-items/00-scenario-behavior-items-index.md
```

## 8. DATA Is Not Validation

DATA files must not contain validation/rules sections, testable behavior sections, invariants, preconditions, branches, access rules, security policy or layout choices.

Current validation/domain route:

```text
planning/scenario-domain-validation-principles.md
planning/diagrams/scenario-questions-register.md
planning/diagrams/scenario-clarifications/
planning/domain/, when domain interpretation is needed
```

Deprecated global validation addenda may be used as historical context only. Do not reintroduce them as current DATA/validation sources.

## 9. Scenario Questions

If DATA is underspecified, add/update:

```text
planning/diagrams/scenario-questions-register.md
```

Use the scenario question loop before continuing implementation planning.

## 10. Downstream Use

Inline DATA sections and reusable DATA concepts feed:

```text
scenario questions register
per-scenario behavior items
scenario UI presentation
compiled baselines
domain drafts
slice boundary drafts
parent slice files
.client.md Scenario / DATA Coverage tables
read/query DTO planning
client-visible page data planning
```

DATA itself is not a DB schema, DTO contract or UI layout.

## 11. Active / Transitional DATA Files

See:

```text
00-scenario-data-index.md
```

Remember: index entries can point to transitional sidecars until PMR-driven reclassification is done.
