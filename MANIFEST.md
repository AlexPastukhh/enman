# l2-validation-scenario-cleanup-sync

Docs-only sync archive.

## Adds

```text
planning/diagrams/scenario-clarifications/L2-validation-and-agreement-exchange-source-cleanup.md
APPLY-l2-validation-cleanup-sync.ps1
```

## Replaces / updates

```text
planning/diagrams/README.md
planning/diagrams/diagram-prompt-generation-workflow.md
planning/diagrams/scenario-clarifications/README.md
planning/diagrams/scenario-text-specs/00-scenario-text-specs-index.md
planning/diagrams/scenario-text-specs/SC-07B-employee-request-review.md
planning/diagrams/scenario-behavior-items/SC-07B-employee-request-review-behavior-items.md
planning/diagrams/scenario-questions-register.md
```

## Optional cleanup performed by script

If present, moves:

```text
planning/diagrams/scenario-text-specs/scenario-server-domain-validation-addendum.md
```

to:

```text
planning/diagrams/scenario-text-specs/deprecated/scenario-server-domain-validation-addendum.deprecated.md
```

and removes it from active path.

## Scope

No runtime code.  
No tests.  
No generated artifacts.
