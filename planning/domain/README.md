# Domain Planning Index

Status: current domain-layer entrypoint / first-pass aggregate-based model  
Doc version: v0.1.0  
Scope: domain discovery, aggregate drafts, value object drafts, domain overview, domain notes and domain decisions

## 1. Purpose

This folder owns the current target documentation model for domain planning.

Use it for:

```text
scenario behavior sources -> domain discovery -> scenario-to-aggregate map -> aggregate/value-object drafts -> domain overview
```

This folder does not own scenario specs, slice drafts, API contracts, testing workflows or implementation code.

## 2. Current Model

The target domain model is aggregate-based:

```text
planning/domain/scenario-to-aggregate-map.md
  scenario behavior sources -> aggregate/value-object candidates and cross-aggregate relations.

planning/domain/domain-source-sync-register.md
  derived source dependency and cross-aggregate sync index for active aggregate drafts.

planning/domain/domain-model-overview.md
  high-level first-pass aggregate/value-object relationship map.

planning/domain/aggregates/
  one file per aggregate boundary.

planning/domain/value-objects/
  one file per reusable/non-trivial value object or value-integrity concept.

planning/domain/decisions/
  accepted/proposed domain decisions that are not just notes and not owned by one aggregate.

planning/domain/domain-notes-register.md
  loose domain notes that should not be lost but are not yet owned by a specific file.
```

## 3. Current First-Pass Drafts

Aggregates:

```text
planning/domain/aggregates/account.md
planning/domain/aggregates/applicant-party.md
planning/domain/aggregates/connection-request.md
planning/domain/aggregates/agreement-proposal-exchange.md
```

Value objects:

```text
planning/domain/value-objects/agreement-proposal-version.md
planning/domain/value-objects/agreement-proposal-author.md
planning/domain/value-objects/agreement-document-ref.md
planning/domain/value-objects/proposal-comment.md
planning/domain/value-objects/final-refusal-reason.md
planning/domain/value-objects/object-address.md
planning/domain/value-objects/rejection-feedback.md
planning/domain/value-objects/applicant-identity.md
planning/domain/value-objects/applicant-contact.md
```

Decisions:

```text
planning/domain/decisions/account-employee-tph-decision.md
```

## 4. Transitional Sources

Older domain files remain source material during migration:

```text
planning/domain-draft-generation-guide.md
planning/domain-model.md
planning/domain-design-input-navigation-notes.md
planning/scenario-domain-validation-principles.md
planning/tables/domain-drafts/
planning/tables/pre-domain-variants-input.md
```

Older monolithic domain drafts are historical discovery snapshots, not the target current shape for new domain docs.

## 5. Read Order

For domain discovery:

```text
1. planning/domain/README.md
2. planning/domain/domain-responsibility-map.md
3. planning/domain/domain-modeling-principles.md
4. planning/domain/domain-discovery-workflow.md
5. planning/domain/scenario-to-aggregate-map.md
6. scenario sources and behavior items
```


For domain source/version/cascade review:

```text
1. planning/domain/README.md
2. planning/domain/domain-responsibility-map.md
3. planning/domain/scenario-to-aggregate-map.md
4. planning/domain/domain-source-sync-register.md
5. relevant aggregate local section `Sources:` blocks
```

For aggregate drafting:

```text
1. planning/domain/README.md
2. planning/domain/domain-responsibility-map.md
3. planning/domain/domain-modeling-principles.md
4. planning/domain/scenario-to-aggregate-map.md
5. planning/domain/domain-source-sync-register.md when reviewing existing source dependencies or downstream sync impact
6. planning/domain/aggregate-drafting-workflow.md
7. planning/domain/aggregate-draft-template.md
8. relevant scenario/domain sources
```

For value object drafting:

```text
1. planning/domain/value-object-drafting-workflow.md
2. planning/domain/value-object-draft-template.md
3. related aggregate drafts and VI behavior items
```

For current overview:

```text
1. planning/domain/scenario-to-aggregate-map.md
2. planning/domain/domain-model-overview.md
3. detailed aggregate/value-object/decision files
```

## 6. Related Layers

```text
planning/diagrams/
  owns scenario text specs, DATA, behavior items, clarifications and scenario questions.

planning/tables/
  owns compiled/historical baselines and pre-domain source snapshots.

planning/slices/
  owns slice drafts and behavior-to-test trace inside slice drafts.

planning/testing/
  owns cross-slice testing principles and reusable testing workflows.
```
