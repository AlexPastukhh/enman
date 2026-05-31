# Domain Discovery Workflow

Status: current domain discovery workflow  
Doc version: v0.1.0  
Scope: scenario behavior sources -> aggregate/value-object candidates and scenario-to-aggregate map

## 1. Purpose

Use this workflow after scenario layer sources exist and before creating or updating aggregate drafts.

This workflow creates or updates:

```text
planning/domain/scenario-to-aggregate-map.md
aggregate candidate register
value object candidate register
cross-aggregate relations
domain discovery questions
```

## 2. Required Reads

Root:

```text
planning/README.md
planning/workflow-activation-map.md
planning/planning-doc-responsibility-map.md
```

Domain:

```text
planning/domain/README.md
planning/domain/domain-responsibility-map.md
planning/domain/domain-modeling-principles.md
planning/domain/scenario-to-aggregate-map.md, if it exists
```

Scenario:

```text
planning/diagrams/README.md
planning/diagrams/scenario-text-specs/
planning/diagrams/scenario-data/
planning/diagrams/scenario-behavior-items/
planning/diagrams/scenario-clarifications/
planning/diagrams/scenario-questions-register.md
```

Historical / cross-check only:

```text
planning/tables/pre-domain-variants-input.md
planning/tables/domain-drafts/
planning/domain-draft-generation-guide.md
planning/domain-model.md
planning/domain-design-input-navigation-notes.md
planning/scenario-domain-validation-principles.md
```

## 3. Behavior Item Classification

| Category | Domain interpretation | Output |
|---|---|---|
| CMD | Command behavior | Candidate aggregate method / domain command / application command |
| LC | Lifecycle / state / condition behavior | State machine / lifecycle rule |
| IBS | Impossible business state | Invariant / impossible state |
| VI | Value integrity | Value object candidate / value validation rule |
| UCQ | Use-case coordination | Cross-aggregate relation / application coordination |
| READ | Read/list/access behavior | Read model / query / access placement |
| INT | Infrastructure/integration expectation | Out-of-domain note / app/infrastructure/testing note |
| FUT | Future/deferred behavior | Deferred note |
| NW | No-write / failure preservation | Failure/no-write proof requirement |

## 4. Workflow Steps

```text
1. Select scenario scope.
2. Read scenario text spec and DATA sources.
3. Read related behavior items.
4. Read clarifications/questions.
5. Classify behavior items by category: CMD / LC / IBS / VI / UCQ / READ / INT / FUT / NW.
6. Convert CMD items into candidate aggregate methods or application commands.
7. Convert LC items into lifecycle/state/status candidates.
8. Convert IBS items into invariant/impossible-state candidates.
9. Convert VI items into value object/value integrity candidates.
10. Convert UCQ items into cross-aggregate relation/application coordination candidates.
11. Route READ/INT/FUT/NW outside aggregate drafts when appropriate.
12. Compare with old domain drafts and compiled baselines only as cross-check.
13. Update scenario-to-aggregate-map.md.
14. Record unresolved discovery questions.
15. Identify next aggregate/value-object drafts to create.
```

## 5. Output

Expected output:

```text
updated scenario-to-aggregate map;
list of candidate aggregates;
list of candidate value objects;
cross-aggregate relation notes;
application coordination notes;
open questions;
next drafting steps.
```

## 6. Guardrails

```text
Do not create aggregate classes from every behavior item.
Do not treat compiled baselines as primary scenario truth when per-scenario behavior items exist.
Do not put application coordination rules inside one aggregate unless ownership is clear.
Do not create value object files for trivial primitive wrappers without source-backed invariants or reuse.
Do not rewrite historical monolithic domain drafts as current truth.
Do not skip scenario questions/clarifications when they affect aggregate boundaries.
```
