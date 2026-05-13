# Domain Design Input Replacement Notes

## Add

```text
planning/scenario-domain-validation-principles.md
planning/diagrams/scenario-text-specs/scenario-server-domain-validation-addendum.md
planning/tables/scenario-domain-design-input-core.md
```

## Replace / mark superseded

```text
planning/tables/scenario-domain-responsibility-core.md
```

Replace it with the small superseded note from this package, or keep both but treat `scenario-domain-design-input-core.md` as the active artifact.

## Why

The useful next step is not a generic responsibility table.

The useful next step is a domain design input table that extracts:

```text
- value objects;
- domain validation;
- state-transition invariants;
- aggregate owner candidates;
- domain method candidates.
```

## Optional later updates

Individual scenario text specs can add sections:

```text
## Client-side validation
## Server-side / Domain validation
```

The addendum file already provides the content in one place to avoid duplicating validation rules across all scenario files.
