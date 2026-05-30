# Enman Status Evidence Profile

Status: candidate Enman project-instance profile  
Scope: concrete evidence/current-reality model and status vocabulary for Enman documentation reconciliation

> Candidate note: this file belongs to `planning/documentation-reusable-candidate/`. It is not active Enman source of truth until a later migration/switch batch approves it.

## 1. Purpose

This file answers the status reconciliation field kit for the Enman project.

It defines which evidence sources and status labels should be used when reconciling Enman documentation status claims.

Kit owner:

```text
planning/documentation-reusable-candidate/status-reconciliation-field-kit.md
```

Repeated workflow:

```text
planning/documentation-reusable-candidate/status-reconciliation-workflow.md
```

## 2. Applicability

Use this profile for Enman software/application documentation claims such as:

```text
implemented;
first-stage implemented;
partially implemented;
planned;
deferred;
historical;
needs evidence check;
external-output-ready.
```

## 3. Evidence Order

Prefer current repo evidence:

```text
1. current Git branch;
2. code;
3. tests;
4. migrations;
5. OpenAPI / generated contracts;
6. runtime screenshots when available;
7. current planning docs;
8. recent explicit user decisions;
9. older archives;
10. memory only as a last resort and never as sole evidence.
```

## 4. Status Vocabulary

| Status | Meaning |
|---|---|
| implemented | Current evidence shows the behavior/work exists. |
| first-stage implemented | Core workflow/evidence exists, but hardening, edge cases or broader integration remain. |
| partially implemented | Some expected scope exists, but meaningful parts are missing. |
| planned | Target direction exists, but current evidence does not show implementation. |
| deferred | Intentionally delayed or out of current scope. |
| historical | Useful past snapshot; not current truth. |
| needs evidence check | Claim may be true, but evidence was not checked or is conflicting. |
| not applicable | Status model does not apply to that claim. |

## 5. Enman Evidence Source Categories

```text
current branch;
server/client code;
tests and E2E baselines;
migrations;
generated contracts / OpenAPI artifacts;
runtime screenshots;
planning/slices/;
planning/api/;
planning/testing/;
planning/architecture/;
planning/client/;
planning/diagrams/;
planning/vkr-clean-reference.md and planning/thesis/ for external-facing claims.
```

## 6. External Output Notes

VKR/thesis-facing claims should not rely on internal planning status alone.

Use current evidence and clean external-facing wording.

Related adapter:

```text
planning/documentation-reusable-candidate/enman-docs-adapter.md
```

## 7. Do Not

```text
- Do not treat slice docs as proof of implementation.
- Do not treat old status snapshots as current truth.
- Do not use memory as sole evidence.
- Do not mark VKR/thesis claims as ready without evidence and wording review.
```
