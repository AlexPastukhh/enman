# Enman Source Usage Cascade Profile

Status: candidate Enman project-instance profile  
Scope: concrete Enman source/consumer categories, row conventions and cascade triggers

> Candidate note: this file belongs to `planning/documentation-migration/`. It is not active Enman source of truth until a later migration/switch batch approves it.

## 1. Purpose

This file answers the source usage cascade field kit for Enman.

Kit owner:

```text
planning/documentation/field-kits/source-usage-cascade-field-kit.md
```

Generic example:

```text
planning/documentation/examples/SOURCE-USAGE-CASCADE-GENERIC-EXAMPLE.md
```

## 2. Source Categories

| Source category | Candidate Enman paths |
|---|---|
| Scenario text/spec source | `planning/diagrams/scenario-text-specs/` |
| Scenario DATA source | `planning/diagrams/scenario-data/` |
| Scenario behavior item source | `planning/diagrams/scenario-behavior-items/` |
| Scenario clarification source | `planning/diagrams/scenario-clarifications/` |
| Domain source | domain drafts/decisions where accepted/current |
| Slice source | `planning/slices/`, `planning/slices/l2/`, `planning/slices/cross-cutting/` |
| Architecture/API/client/testing source | `planning/architecture/`, `planning/api/`, `planning/client/`, `planning/testing/` |
| External-output source/consumer | `planning/vkr-clean-reference.md`, `planning/thesis/` |

## 3. Consumer Categories

| Consumer category | Examples |
|---|---|
| DATA/behavior consumers | derived scenario DATA and behavior item sets |
| Domain consumers | domain drafts that use scenario/DATA/behavior source |
| Slice consumers | slice docs that use scenario/domain/source material |
| API/testing consumers | contracts, error contracts, testing plans and E2E workflows |
| External-output consumers | VKR/thesis materials that consume internal planning docs |

## 4. Candidate Row Shape

| Field | Enman convention |
|---|---|
| source_id | repo-relative path + optional section/scope |
| source_scope | scenario/data/behavior/domain/slice/API/testing section or row |
| source_status | draft, accepted, current, historical, superseded |
| consumer_id | repo-relative file path |
| consumer_scope | downstream section/decision/claim |
| reviewed_against | commit/date/source version or explicit reviewed source state |
| sync_status | current, stale, needs-review, metadata-only-reviewed, not-applicable |
| review_outcome | no-change, update-needed, follow-up, superseded, blocked |
| last_reviewed | date/commit/review marker |
| notes | short reason only |

## 5. Cascade Triggers

Run cascade review when:

```text
- scenario/source behavior changes;
- DATA/behavior items change;
- domain interpretation changes;
- slice scope changes;
- API/testing contract or evidence changes;
- external-facing VKR/thesis claim depends on changed internal source.
```

## 6. Pilot Scope

Recommended first pilot:

```text
one scenario source -> one DATA/behavior item set -> one domain/slice consumer -> one external/evidence-related claim if relevant
```

Keep the pilot small enough to prove row shape and review value.

## 6A. Preserved Pilot Candidate: SC-13D / AgreementProposalExchange

Earlier candidate governance notes identified a concrete Enman pilot candidate:

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

Earlier skeleton register path:

```text
planning/documentation/source-usage-pilots/SC-13D-agreement-exchange-source-usage-register.md
```

F5 does not activate this as an active Enman register. It preserves the pilot candidate so later source-usage review can decide whether to run, revise or archive it.

Detailed candidate example:

```text
planning/documentation/examples/SOURCE-USAGE-CASCADE-ENMAN-SCENARIO-EXAMPLE.md
```

## 7. Do Not

```text
- Do not create broad cascade review for every docs change by default.
- Do not copy upstream truth into downstream files just for self-containment.
- Do not treat version markers as source truth by themselves.
- Do not use this candidate profile as active Enman routing before a switch batch.
```
