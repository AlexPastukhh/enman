# Domain Responsibility Map

Status: current domain-layer responsibility map  
Doc version: v0.1.0  
Scope: where domain-layer information belongs during the aggregate-based domain model migration

## 1. Purpose

This map routes domain information to the correct owner.

It separates:

```text
scenario-to-domain discovery;
aggregate drafts;
value object drafts;
domain decisions;
domain notes;
historical monolithic domain drafts;
compiled tables/baselines.
```

## 2. Routing Table

| Information type | Belongs in | Does not belong in |
|---|---|---|
| Domain layer entrypoint and read order | `planning/domain/README.md` | aggregate drafts |
| Domain placement/routing rules | `planning/domain/domain-responsibility-map.md` | README-only prose |
| Scenario -> aggregate discovery | `planning/domain/scenario-to-aggregate-map.md` | old compiled tables as current source |
| Aggregate boundary/details | `planning/domain/aggregates/<aggregate>.md` | scenario files or global maps |
| Child entity owned by an aggregate | owning aggregate draft | separate aggregate file unless it is an aggregate root |
| Value object definition | `planning/domain/value-objects/<name>.md` | aggregate file, except references/usage |
| Value object usage inside one aggregate | aggregate draft `Value Objects Used` section | value object definition file only |
| Cross-aggregate relation overview | `planning/domain/scenario-to-aggregate-map.md` during discovery; later `domain-model-overview.md` | one aggregate file unless the rule is clearly aggregate-owned |
| Application coordination rule | aggregate draft coordination notes or scenario-to-aggregate map | hidden inside one aggregate invariant |
| Accepted domain decision | `planning/domain/decisions/<decision>.md` | notes register |
| Loose future domain note | `planning/domain/domain-notes-register.md` | README or chat memory only |
| Historical monolithic domain draft | `planning/tables/domain-drafts/` | current aggregate file unless extracted |
| Compiled behavior baseline / pre-domain source snapshot | `planning/tables/` | current domain overview |

## 3. Migration Rule

Old monolithic domain drafts remain source snapshots until their content is extracted into aggregate, value-object or decision files.

Do not rewrite or delete old domain drafts as part of scaffold work.

## 4. Note Triage Rule

When a domain note appears:

```text
specific aggregate behavior/invariant
  -> aggregate draft

specific value integrity or reusable value concept
  -> value object draft

accepted modeling decision
  -> decisions/

relationship between aggregates
  -> scenario-to-aggregate-map.md or later domain-model-overview.md

loose future modeling note
  -> domain-notes-register.md

docs architecture follow-up
  -> planning/planning-maintenance-register.md
```

## 5. Guardrails

```text
Do not use planning/tables/ as the current domain entrypoint.
Do not create aggregate files for every class candidate.
Do not create value-object files for trivial wrappers without source-backed invariants or reuse.
Do not hide cross-aggregate coordination inside one aggregate unless ownership is clear.
Do not treat EF navigation convenience as domain ownership.
Do not leave durable domain notes only in chat.
```
