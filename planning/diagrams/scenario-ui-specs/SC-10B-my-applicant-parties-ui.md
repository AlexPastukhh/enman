# SC-10B — My Applicant Parties UI Spec

Status: deprecated / replaced by `SC-10-applicant-data-ui.md`  
Marker: `[UI-SCENARIO]`  
Replacement: `planning/diagrams/scenario-ui-specs/SC-10-applicant-data-ui.md`

## Current Decision

This is not the current canonical UI target as a separate page/scenario.

Current direction:

```text
Account page contains the My Applicant Parties / Applicant Parties section.

All current and future ApplicantParty actions happen inside that Account section unless a later decision changes it.
```

The canonical UI source is:

```text
planning/diagrams/scenario-ui-specs/SC-10-applicant-data-ui.md
```

## Historical/Future Notes Preserved

Future behavior may include:

```text
show all saved ApplicantParties
visually identify current/default per type
allow future edit/delete/archive
warn before destructive/history-affecting actions
allow explicit current/default selection
```

But edit/delete/archive/current selection are not implemented now and must not be treated as current UI requirements without dedicated future slices.
