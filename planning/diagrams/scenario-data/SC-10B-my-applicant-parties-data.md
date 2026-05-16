# SC-10B — My Applicant Parties DATA

## Purpose

Define future visible/input/reference DATA for managing saved ApplicantParty profiles.

## DATA Blocks

### SC-10B-DATA-01 — ApplicantParty list DATA

Type: Visible DATA / Filter DATA  
Actor: Client  
Used by: future My Applicant Parties list

Visible DATA:

```text
- applicant type;
- applicant display name;
- contact summary;
- identifiers relevant for type;
- verification status;
- current/default marker for its applicant type;
- usage indicator when available, for example used by requests / approved requests.
```

Filter / grouping DATA:

```text
- applicant type;
- verification status;
- current/default marker.
```

### SC-10B-DATA-02 — ApplicantParty details DATA

Type: Visible DATA / Reference DATA  
Actor: Client  
Used by: future ApplicantParty details

Visible DATA:

```text
- applicant type;
- full applicant data for the type;
- verification status;
- current/default marker;
- created/updated dates, if exposed;
- usage/history summary, if exposed.
```

### SC-10B-DATA-03 — ApplicantParty management action DATA

Type: Action DATA / Confirmation DATA  
Actor: Client  
Used by: future add/edit/delete/archive/set-default actions

Action DATA:

```text
- add ApplicantParty;
- edit ApplicantParty;
- delete/archive/hide ApplicantParty;
- set as current/default template for applicant type.
```

Warning / confirmation DATA:

```text
- deletion risk level;
- whether ApplicantParty is current/default;
- whether ApplicantParty is used by existing requests;
- whether ApplicantParty is used by approved requests;
- consequence of delete/archive/hide.
```

## Notes

```text
This DATA is future planning.
Do not claim current client implementation already has My Applicant Parties management.
```

## Scenario References

```text
planning/diagrams/scenario-text-specs/SC-10B-my-applicant-parties.md
planning/diagrams/scenario-text-specs/SC-10-applicant-data.md
planning/diagrams/scenario-text-specs/SC-04-client-request-creation.md
```
