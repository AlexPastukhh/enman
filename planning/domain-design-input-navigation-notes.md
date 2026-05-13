# Domain Design Input Navigation Notes

## Replace / add

```text
planning/README.md
planning/planning-workflow-current.md
planning/tables/README.md
planning/tables/00-planning-tables-index.md
planning/tables/scenario-domain-design-input-gate.md
```

## Purpose

This update strengthens navigation around the required bridge artifact:

```text
planning/tables/scenario-domain-design-input-core.md
```

## Key rule

After scenario specs and DATA files are ready, do not jump directly to aggregate design.

Use:

```text
scenario specs + DATA + validation addendum
-> scenario-domain-design-input-core.md
-> domain-discovery-core.md
-> aggregate-boundary-candidates-core.md
```

## Why

`scenario-domain-design-input-core.md` collects:

```text
- value object candidates;
- server/domain validation;
- persisted state/status values;
- state-transition invariants;
- candidate domain methods;
- aggregate boundary pressure.
```
