# Aggregate Drafting Workflow

Status: current aggregate drafting workflow  
Doc version: v0.1.0  
Scope: create/update one aggregate draft from scenario-to-aggregate map and source behavior

## 1. Purpose

Use this workflow when creating or updating one aggregate draft.

One aggregate draft owns one aggregate boundary.

## 2. Required Reads

Domain:

```text
planning/domain/README.md
planning/domain/domain-responsibility-map.md
planning/domain/domain-modeling-principles.md
planning/domain/scenario-to-aggregate-map.md
planning/domain/aggregate-draft-template.md
```

Sources:

```text
scenario behavior items mapped to this aggregate;
scenario text specs and DATA sources for those items;
relevant clarifications/questions;
relevant old monolithic domain drafts;
relevant domain decisions;
relevant value object drafts.
```

## 3. Workflow Steps

```text
1. Pick one aggregate candidate.
2. Confirm the aggregate has enough source evidence.
3. Identify aggregate root.
4. Identify child entities.
5. Identify value objects used.
6. Identify external aggregate references.
7. Identify what is not part of this aggregate.
8. Define owned state.
9. Define domain methods/commands.
10. Define invariants.
11. Define lifecycle/state machine.
12. Define impossible states prevented.
13. Define cross-aggregate relations.
14. Separate application coordination from aggregate-owned rules.
15. Add behavior coverage.
16. Add persistence/EF notes only where needed.
17. Add cross-layer placement notes.
18. Add open questions/accepted decisions.
19. Update scenario-to-aggregate-map.md if the aggregate boundary changed.
20. Update domain notes or decisions follow-ups if needed.
```

## 4. Output

Expected output:

```text
one aggregate draft file;
updated scenario-to-aggregate map if needed;
domain notes/decisions follow-ups if needed;
source delta when sources changed.
```

## 5. Guardrails

```text
One aggregate draft owns one aggregate boundary.
Child entities stay inside the owning aggregate file.
External aggregates are referenced, not owned.
Cross-aggregate invariants are application coordination unless one aggregate clearly owns the rule.
Value object definitions belong in value-object files when reusable/non-trivial.
EF navigation convenience is not domain ownership.
Do not let read/query convenience define write aggregate shape.
```
