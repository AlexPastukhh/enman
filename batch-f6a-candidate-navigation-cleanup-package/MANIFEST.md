# Batch F6A — Candidate Navigation Cleanup Package

Status: replacement package created  
Scope: candidate consistency / read-order / owner-link cleanup after F4/F5

## Included changes

This package:

- rewrites `planning/documentation-reusable-candidate/README.md` as a candidate index/read-order file;
- adds a current navigation pointer to `CANDIDATE-NOTICE.md`;
- updates `PORTABILITY-FOLLOWUPS.md` with F6A cleanup state;
- records F6A in the active migration plan;
- records F6A in the documentation action log.

## Intentionally not included

This package does **not**:

- promote Enman project-instance files to active `planning/` root;
- switch canonical documentation ownership;
- delete the source usage governance bridge;
- create new field kits/examples;
- rename candidate principles;
- rewrite active documentation workflows;
- create scripts.

## Files

```text
planning/documentation-reusable-candidate/README.md
planning/documentation-reusable-candidate/CANDIDATE-NOTICE.md
planning/documentation-reusable-candidate/PORTABILITY-FOLLOWUPS.md
planning/documentation/documentation-layer-portability-migration-plan.md
planning/documentation/documentation-action-log.md
```

## Delivery safety

- Large/shared files: no large content rewrite except candidate README.
- Preferred delivery: complete replacement package.
- Script needed: no.
