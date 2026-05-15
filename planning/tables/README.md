# Planning Tables Index

Status: current planning tables navigation

## 1. Current Role

The current saved domain draft is:

```text
planning/tables/domain-drafts/domain-draft-01.md
```

The compiled behavior baseline remains:

```text
planning/tables/pre-domain-variants-input.md
planning/tables/scenario-behavior-baseline-account-activation-addendum.md
```

New workflow introduces per-scenario behavior item files under:

```text
planning/diagrams/scenario-behavior-items/
```

`pre-domain-variants-input.md` remains useful as a compiled downstream baseline / existing domain draft input.

It is no longer the only intended upstream source for future slice/client planning.

## 2. Current Read Order

```text
1. planning/diagrams/scenario-text-specs/
2. planning/diagrams/scenario-data/
3. planning/diagrams/scenario-questions-register.md
4. planning/diagrams/scenario-behavior-items/README.md
5. planning/tables/pre-domain-variants-input.md
6. planning/tables/scenario-behavior-baseline-account-activation-addendum.md
7. planning/tables/domain-drafts/domain-draft-01.md
8. planning/l1-domain-implementation-cut.md
9. planning/l1-domain-testing-rules.md
10. planning/slices/README.md
11. planning/slices/l1-slice-drafting-guide.md
12. planning/slices/l1-slice-boundary-draft-01.md
13. planning/adr/README.md
14. planning/adr/adr-candidates.md
```

## 3. Behavior Items Position

Future desired flow:

```text
scenario text spec
+ scenario DATA file
+ validation/security addendum
-> per-scenario behavior items
-> domain drafts / slice drafts / client sidecars
```

Behavior items migration / cleanup is a separate future step.

## 4. Slice Planning

Current slice planning entry points:

```text
planning/slices/README.md
planning/slices/l1-slice-drafting-guide.md
planning/slices/implementation-principles.md
planning/slices/slice-implementation-notes-register.md
planning/slices/l1-slice-boundary-draft-01.md
```

Parent slice files plan vertical behavior and API/server responsibilities.

`.client.md` sidecars are created only when concrete client work starts.

## 5. Current Next Step

Use the updated workflow to prepare the next concrete Client/UI sidecar after getting a UI plan.

Likely target:

```text
request creation Client/UI for already implemented SL-REQ-001 server-side behavior
```

## 6. Avoid

Do not add or use old intermediate files such as:

```text
domain-discovery-core.md
aggregate-boundary-candidates-core.md
domain-model-options-core.md
scenario-domain-responsibility-core.md
scenario-domain-design-input-core.md
scenario-domain-design-input-gate.md
```
