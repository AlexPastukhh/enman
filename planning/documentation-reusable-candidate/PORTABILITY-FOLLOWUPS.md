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
| PF-001 | 12A, 13, 14, 15 | Source usage / cascade field-kit extraction | active project profile promoted | Review field kit/profile/example after use; decide whether old governance bridge can be deleted. | active `planning/source-usage-cascade-profile.md` + candidate field kit/profile/example | after pilot |
| PF-002 | 10, 11, 20, 26 | Status reconciliation / evidence-current-reality model | active project profile promoted | Use active `planning/status-evidence-profile.md`; review after real reconciliation use. | active status profile + candidate field kit | after use |
| PF-003 | 16 | Shared Visibility Map setup | active project map promoted | Use active `planning/shared-visibility-map.md`; review after real local/global sync use. | active shared visibility map + candidate field kit | after use |
| PF-004 | 14, 16, 18, 21 | Examples extraction | partially addressed | Four starter examples exist, including a preserved Enman SC-13D source-usage pilot example; more examples may be added after field-kit usage. | candidate examples folder / examples README | after F5 review |
| PF-005 | F3 open questions | Final naming and canonical switch decisions | still open | Decide final reusable principles name, adapter naming, active promotion path. | migration plan | later switch batch |
| PF-006 | 23, 24B | Archive/package and command preservation details | still open if needed | Principles keep the boundary; detailed command/source semantics remain in owner workflows. | documentation update workflow / replacement guide / use-case map | later cleanup if needed |
| PF-007 | F6A | Candidate README/read-order cleanup | addressed in candidate | Re-check after any future promotion/switch batch. | `planning/documentation-reusable-candidate/README.md` | later switch batch |
| PF-008 | F7A/F7B | Active-only reusable overlay into candidate | addressed in candidate | Re-check after final folder-switch planning; make sure active UCM kits and diff-command guardrails are preserved. | candidate overlay files + active docs layer | F7C / later switch verification |


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
- reviewing active promoted Enman project-instance files after real use.
```

## 4A. F7C Overlay Outputs

F7C mirrors active reusable additions into candidate so a later folder-switch plan does not lose active F7A/F7B work.

Candidate overlay files:

```text
planning/documentation-reusable-candidate/field-kits/root-use-case-map-field-kit.md
planning/documentation-reusable-candidate/profiles/scenario-domain-slice-use-case-field-kit.md
planning/documentation-reusable-candidate/use-case-map-workflow.md
planning/documentation-reusable-candidate/USE-CASE-MAP-TEMPLATE.md
planning/documentation-reusable-candidate/documentation-update-workflow.md
planning/documentation-reusable-candidate/documentation-action-log.md
```

Boundary:

```text
F7C does not make candidate canonical.
F7C does not copy root project files into candidate.
F7C does not rename or swap folders.
```

## 5. Guardrail

Do not treat addressed follow-ups as final active docs.

F5 outputs are candidate-only until a later migration/switch batch approves promotion.

F6A candidate cleanup addressed the immediate README/read-order inconsistency. Future promotion/switch work must re-check candidate navigation after any files move into active locations.

F6C promoted active root project-instance files for status evidence, shared visibility and source usage cascade. Candidate files remain as migration history/candidate references.
