# Test Site Mockup

This folder contains an isolated low-fidelity React mockup for walking through diploma UI scenarios.

It is not production frontend, not backend/API implementation, not database schema, not a final visual design system, and not a rewrite of existing pages.

## Entry

The mockup is mounted through the dev route:

```text
/test-ui
```

## Files

```text
src/test-site-mockup/TestSiteMockup.tsx
src/test-site-mockup/mockData.ts
src/test-site-mockup/mockApi.ts
src/test-site-mockup/mockupStyles.css
src/test-site-mockup/README.md
```

## Integration boundaries

- Uses existing `Header`, `Footer`, and safe CSS primitives such as `button-primary`, `button-hollow`, and `formControl`.
- Does not use real API clients, production services, auth mutations, upload services, real stores, or backend queries.
- Uses local fake/demo state for roles, account activation, request statuses, agreement proposal statuses, sender, and UI error states.
- File behavior is represented only as selected filename/reference, accepted attachment reference, missing document, or rejected file state.

## Open UI questions shown as provisional

- Q:UI-001: Approved request shows both request details and My Agreements path for walkthrough only.
- Q:UI-002: Review action for non-InReview requests is hidden/disabled with explanation.
- Q:UI-003: Accepted proposal is shown as an Accepted proposal only; no Signed status or legal finalization wording is introduced.

## Recommended style refinement later

No existing global styles or shared UI components were changed in this archive.

Potential future refinements, outside this mockup archive:

- `src/styles/index.css`: consider replacing `--color-background:#9AF6E5` with a calmer neutral background token for the production site. Risk: high, because it affects all pages using the token.
- `src/styles/layout.css`: consider defining `--header-height` explicitly or removing reliance on it. Risk: medium, because header layout may shift.
- `src/styles/forms.css`: consider making form widths responsive instead of fixed `30rem`. Risk: medium, because current register/login views may change visually.
- `src/Components/Layout/*`: consider extracting generic Button/Card/FormField primitives later. Risk: medium, because shared components affect production routes.
