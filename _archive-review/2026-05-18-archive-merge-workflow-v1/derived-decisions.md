# Derived Decisions

Status: archive-local derived decisions / reflected in docs

## Decisions

```text
Archives that replace existing files must preserve original copies.
```

```text
Original copies live under _archive-review/<unique-archive-slug>/original-files/.
```

```text
Large replacement archives are treated as safe merge step 1, not final merge.
```

```text
Post-apply review compares applied files against archived originals.
```

```text
If information was lost, a second smaller correction archive is created.
```

```text
Archive-local raw author logs and derived decisions prevent multiple archives from overwriting each other.
```
