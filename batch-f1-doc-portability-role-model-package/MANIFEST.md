# Batch F1 — Documentation Portability Role Model Package

Status: replacement package created  
Scope: pre-split documentation-layer portability role model and migration plan

## Included changes

This package:

- adds `planning/documentation/documentation-layer-portability-migration-plan.md`;
- adds `planning/documentation/documentation-responsibility-zone-review-workflow.md`;
- clarifies principles responsibility as high-level invariants and file-type theory;
- introduces/records Field Kit and Adapter/Profile file-type boundaries;
- updates documentation-layer navigation and responsibility routing;
- corrects `арх` semantics as source snapshot, not output package generation;
- adds responsibility-zone review route in the root use-case map;
- records deferred example coverage;
- records the completed documentation action.

## Intentionally not included

This package does **not**:

- split `planning-docs-architecture-principles.md`;
- create a scenario/domain/slice profile;
- create an Enman/project adapter;
- move VKR/thesis material;
- move source usage pilots or scoped sync notes;
- create `planning/documentation-reusable-candidate/`;
- add scripts.

## Files

```text
planning/documentation/documentation-layer-portability-migration-plan.md
planning/documentation/documentation-responsibility-zone-review-workflow.md
planning/documentation/planning-docs-architecture-principles.md
planning/documentation/documentation-responsibility-map.md
planning/documentation/README.md
planning/planning-use-case-map.md
planning/documentation/documentation-update-workflow.md
planning/documentation/examples/README.md
planning/documentation/documentation-action-log.md
```

## Delivery safety

- Large/shared files: yes (`planning-docs-architecture-principles.md`, `planning-use-case-map.md`).
- Fresh source snapshot: provided by `арх`.
- Preferred delivery: complete replacement package.
- Script needed: no.
