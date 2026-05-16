# SC-10B — My Applicant Parties DATA

Status: future DATA spec  
Scope: saved ApplicantParty management DATA

## DATA Blocks

### SC-10B-DATA-01 — ApplicantParty list DATA

Type: Visible DATA / Reference DATA  
Actor: Client

Visible DATA:

```text
- ApplicantParty display name;
- applicant type;
- contact summary;
- verification state;
- current/default marker for applicant type;
- created/updated summary when available;
- safe/risky action availability when policy exists.
```

### SC-10B-DATA-02 — ApplicantParty management actions

Type: Selection DATA / Action DATA  
Actor: Client

Actions:

```text
- add ApplicantParty;
- view details inline or in future details view;
- edit ApplicantParty when policy allows;
- delete/archive/hide ApplicantParty when policy allows;
- set as current/default template for its applicant type.
```

### SC-10B-DATA-03 — Delete/archive warning DATA

Type: Visible DATA / Confirmation DATA  
Actor: Client

Warning DATA:

```text
- whether ApplicantParty is used by requests;
- whether ApplicantParty is used by approved/reviewed requests;
- whether ApplicantParty is current/default for its type;
- consequence of deletion/archive/hide.
```

## Notes

```text
Hard delete is not accepted by default.
Use delete/archive/hide wording until policy is decided.
```
