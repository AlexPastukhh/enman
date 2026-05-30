# Portability Follow-ups

Status: candidate deferred review list  
Scope: deferred work that remains after F5 field-kit and project-instance split

> Candidate note: this file belongs to `planning/documentation-reusable-candidate/`. It tracks deferred work for later candidate migration batches.

## 1. Purpose

F4 separated obvious reusable principles, scenario-driven profile material and Enman-specific adapter mappings.

F5 separated the main field-kit/project-instance responsibilities for status reconciliation, shared visibility and source usage cascade.

This file now tracks what remains after that split.

## 2. Follow-up Table

| ID | Origin sections | Topic | F5 status | Remaining work | Likely owner | Target batch |
|---|---|---|---|---|---|---|
| PF-001 | 12A, 13, 14, 15 | Source usage / cascade field-kit extraction | addressed in candidate | Review field kit/profile/example after use; decide whether old governance bridge can be deleted. | `source-usage-cascade-field-kit.md`, Enman source profile | F6 or after pilot |
| PF-002 | 10, 11, 20, 26 | Status reconciliation / evidence-current-reality model | addressed in candidate | Review Enman profile against real docs/evidence before active promotion. | `status-reconciliation-field-kit.md`, Enman status profile | F6 / active promotion |
| PF-003 | 16 | Shared Visibility Map setup | addressed in candidate | Review Enman map against active registers before active promotion. | `shared-visibility-map-field-kit.md`, Enman shared visibility map | F6 / active promotion |
| PF-004 | 14, 16, 18, 21 | Examples extraction | partially addressed | Four starter examples exist, including a preserved Enman SC-13D source-usage pilot example; more examples may be added after field-kit usage. | candidate examples folder / examples README | after F5 review |
| PF-005 | F3 open questions | Final naming and canonical switch decisions | still open | Decide final reusable principles name, adapter naming, active promotion path. | migration plan | later switch batch |
| PF-006 | 23, 24B | Archive/package and command preservation details | still open if needed | Principles keep the boundary; detailed command/source semantics remain in owner workflows. | documentation update workflow / replacement guide / use-case map | later cleanup if needed |

## 3. F5 Outputs

F5 candidate outputs:

```text
planning/documentation-reusable-candidate/status-reconciliation-field-kit.md
planning/documentation-reusable-candidate/status-reconciliation-workflow.md
planning/documentation-reusable-candidate/enman-status-evidence-profile.md

planning/documentation-reusable-candidate/shared-visibility-map-field-kit.md
planning/documentation-reusable-candidate/local-global-documentation-sync-workflow.md
planning/documentation-reusable-candidate/enman-shared-visibility-map.md

planning/documentation-reusable-candidate/source-usage-cascade-field-kit.md
planning/documentation-reusable-candidate/source-usage-cascade-governance-plan.md
planning/documentation-reusable-candidate/enman-source-usage-cascade-profile.md
```

## 4. F6+ Candidates

After F5, consider:

```text
- reviewing the field kits against actual use;
- deleting or archiving the source usage governance bridge after review;
- adding more examples if workflows/templates are used;
- deciding final reusable principles filename;
- deciding whether Enman adapter remains `enman-docs-adapter.md` or a generic project adapter template is added;
- deciding canonical switch process;
- deciding whether active `planning/documentation/` is replaced, archived or kept as Enman project docs;
- deciding whether Enman project-instance files should be promoted into active `planning/` root.
```

## 5. Guardrail

Do not treat addressed follow-ups as final active docs.

F5 outputs are candidate-only until a later migration/switch batch approves promotion.
