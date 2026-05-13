# Planning Tables Index

Status: simplified current table index

## Current Active File

| Order | File | Status | Purpose |
|---:|---|---|---|
| 1 | `pre-domain-variants-input.md` | current active bridge | Collect invariants, write state, state-changing actions, allowed/forbidden transitions, no-write behavior and method pressure before generating domain variants. |

## Current Read Order

```text
1. planning/diagrams/scenario-text-specs/
2. planning/diagrams/scenario-data/
3. planning/diagrams/scenario-text-specs/scenario-server-domain-validation-addendum.md
4. planning/tables/pre-domain-variants-input.md
```

## Current Next Step

```text
Generate domain model variant 1.
```

Then:

```text
Generate domain model variant 2.
Compare/refine variants.
Choose domain model direction.
Then plan aggregates/slices/implementation.
```

## Why No More Intermediate Tables Now

Do not add more intermediate files such as:

```text
domain-discovery-core.md
aggregate-boundary-candidates-core.md
domain-model-options-core.md
scenario-domain-responsibility-core.md
scenario-domain-design-input-core.md
scenario-domain-design-input-gate.md
```

Those names are too technical and fragmented for the current workflow.

Keep the post-scenario bridge as one file:

```text
pre-domain-variants-input.md
```

## Superseded / Historical

If these files exist historically, treat them as stale/superseded notes, not active workflow:

| File | Current replacement |
|---|---|
| `scenario-domain-design-input-gate.md` | `pre-domain-variants-input.md` + current workflow docs |
| `scenario-domain-design-input-core.md` | `pre-domain-variants-input.md` |
| `domain-discovery-core.md` | domain model variants generated one by one |
| `aggregate-boundary-candidates-core.md` | domain model variants + later comparison |
| `domain-model-options-core.md` | domain model variants + later comparison |
| `scenario-responsibility-core.md` | `pre-domain-variants-input.md` |
| `scenario-domain-responsibility-core.md` | `pre-domain-variants-input.md` |
