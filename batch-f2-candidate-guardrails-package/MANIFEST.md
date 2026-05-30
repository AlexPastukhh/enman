# Batch F2 — Candidate Guardrails Package

Status: replacement package created  
Scope: small completion batch after `planning/documentation/` was copied to `planning/documentation-reusable-candidate/`

## Included changes

This package:

- adds `planning/documentation-reusable-candidate/CANDIDATE-NOTICE.md`;
- updates the active migration plan with candidate baseline state;
- updates the active documentation README with candidate workspace guardrails;
- updates the documentation responsibility map with candidate lifecycle routing;
- records the F2 guardrail action in the documentation action log.

## Intentionally not included

This package does **not**:

- edit the copied candidate files except for the new notice;
- split principles;
- create a scenario-domain-slice profile;
- create an Enman/project adapter;
- move VKR/source-usage pilots/sync notes;
- create scripts.

## Files

```text
planning/documentation-reusable-candidate/CANDIDATE-NOTICE.md
planning/documentation/documentation-layer-portability-migration-plan.md
planning/documentation/README.md
planning/documentation/documentation-responsibility-map.md
planning/documentation/documentation-action-log.md
```

## Delivery safety

- Large/shared files: small active docs updates plus one new candidate notice.
- Fresh source snapshot: current remote/copy state is expected after F1 and manual candidate copy.
- Preferred delivery: complete replacement package.
- Script needed: no.
