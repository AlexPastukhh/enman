# Algorithm: visual material review

Run when the user sends diagrams, images, PDF visual drafts or asks what figures/tables are needed.

## Main question

A visual must support a desired result, semantic point or full-text paragraph.

Ask:

```text
Which result does this visual help explain?
```

## Table vs figure rule

Do not automatically create a diagram for every process.

Use a diagram when it helps show:

- branches;
- roles interacting;
- state transitions;
- dependencies;
- system boundaries.

Use a table when it helps show:

- participants and roles;
- state → allowed actions;
- levels of automation;
- comparison of options;
- concise mapping.

For linear material, prefer tables.

## Chapter 1 preference

For Chapter 1, usually prefer:

- participant/object table;
- state → allowed actions table;
- automation levels table.

Do not show implementation details, API, DB, mock external verification or automatic contract generation.
