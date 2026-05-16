# SL-APPL-001.client replacement archive

This archive replaces:

```text
planning/slices/SL-APPL-001-create-individual-applicant-party.client.md
```

Scope:

```text
planning docs only
runtime client code is not changed
```

Reason:

The target client model for Create Individual ApplicantParty changed:

```text
- many saved ApplicantParties per account;
- current/default template per applicant type;
- first ApplicantParty of a type may initialize default;
- adding another ApplicantParty of the same type does not implicitly change default;
- creating ApplicantParty is not replacement by default;
- ApplicantPartyId remains useful in standalone create response;
- E2E should assert visible Account page state, not refetch mechanics.
```

Runtime implementation is intentionally not included because the target client requires backend/read-model support that is not present in the current narrow `current-individual` flow:

```text
- list saved ApplicantParties;
- current/default template per applicant type;
- multiple ApplicantParties per account;
- initial default rule;
- no implicit replacement on additional create.
```

Apply from repo root after extracting:

```powershell
powershell -ExecutionPolicy Bypass -File .\apply-sl-appl-001-client-replacement.ps1
```

or just extract with overwrite into repo root.
