# Portability Follow-ups

Status: candidate deferred review list  
Scope: items intentionally not forced into reusable principles, scenario-driven profile or Enman adapter during F4

> Candidate note: this file belongs to `planning/documentation-reusable-candidate/`. It tracks deferred work for later candidate migration batches.

## 1. Purpose

F4 separates obvious reusable principles, scenario-driven profile material and Enman-specific adapter mappings.

Some material should not be forced into those files yet because it needs workflow, field-kit, template or example review.

This file preserves those follow-ups so they are not lost or incorrectly dumped into the Enman adapter.

## 2. Follow-up Table

| ID | Origin sections | Topic | Why deferred | Likely owner | Target batch |
|---|---|---|---|---|---|
| PF-001 | 12A, 13, 14, 15 | Source usage / cascade field-kit extraction | Reusable principle is clear, but row model, reviewed_against, sync_status and section-source blocks need setup/tooling review. | `source-usage-cascade-field-kit.md` or revised source usage governance file | F5 |
| PF-002 | 10, 11, 20, 26 | Status reconciliation / evidence-current-reality model | Reusable principle is clear, but evidence model differs across software, notes, planning, research and other domains. | `status-reconciliation-workflow.md` + project evidence profile/adapter section | F5 |
| PF-003 | 16 | Shared Visibility Map setup | Local/global principle is clear, but reusable setup and Enman exact target map need workflow/adapter alignment. | `local-global-documentation-sync-workflow.md` + adapter shared visibility map | F5 |
| PF-004 | 14, 16, 18, 21 | Examples extraction | Concrete examples should be separate, but exact example files are easier after F4/F5 owner docs stabilize. | candidate examples folder / examples README | F6 or after F5 |
| PF-005 | F3 open questions | Final naming and canonical switch decisions | Candidate names may change before final reusable layer switch. | migration plan | later switch batch |
| PF-006 | 23, 24B | Archive/package and command preservation details | Principles keep the boundary, but detailed output/source command semantics are owned elsewhere. | documentation update workflow / replacement guide / use-case map | later cleanup if needed |

## 3. F5 Candidates

F5 should focus on the three key files already discussed:

```text
planning/documentation-reusable-candidate/status-reconciliation-workflow.md
planning/documentation-reusable-candidate/local-global-documentation-sync-workflow.md
planning/documentation-reusable-candidate/source-usage-cascade-governance-plan.md
```

Expected treatment:

| File | F5 direction |
|---|---|
| `status-reconciliation-workflow.md` | Add/clarify applicability gate and domain-specific evidence/current-reality model setup. |
| `local-global-documentation-sync-workflow.md` | Keep as reusable workflow, but make Shared Visibility Map project-defined. |
| `source-usage-cascade-governance-plan.md` | Likely convert or split toward a source usage/cascade field kit. |

## 4. F6+ Candidates

After F5, consider:

```text
- extracting concrete examples into candidate examples files;
- deciding final reusable principles filename;
- deciding whether Enman adapter becomes `enman-docs-adapter.md` permanently or a generic `project-docs-adapter.md` template is added;
- deciding canonical switch process;
- deciding whether active `planning/documentation/` is replaced, archived or kept as Enman project docs.
```

## 5. Guardrail

Do not use this file as a substitute for doing the follow-up work.

A follow-up row means:

```text
This was intentionally not solved in F4.
Review it in the target batch with the right owner workflow/field kit/template/adapter.
```
