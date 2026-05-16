# Current Planning Workflow

Status: applicant-template-per-type synchronized

Always separate current implementation from target scenario direction.

Current target direction:

```text
many ApplicantParties; default/current per applicant type; explicit request applicant context; no hidden replacement.
```

Testing rules:

```text
Behavior Coverage is not Test Coverage.
ApplicantPartyId is API support, not behavior item.
E2E asserts visible state/outcome, not refetch mechanics.
```
