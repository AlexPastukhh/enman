# Current Planning Workflow

Status: applicant-template-per-type synchronized / server validation principles added

Always separate current implementation from target scenario direction.

Current target direction:

```text
many ApplicantParties; default/current per applicant type; explicit request applicant context; no hidden replacement.
```

Current server validation direction:

```text
FluentValidation exists and legacy manual usage exists.
L1 currently relies mostly on application/domain validation.
When planning new/changed L1 API input, add request-level FluentValidation to the implementation flow.
```

Implementation planning must separate:

```text
FluentValidation request DTO/query validation
from
application/domain validation.
```

Testing rules:

```text
Behavior Coverage is not Test Coverage.
ApplicantPartyId is API support, not behavior item.
E2E asserts visible state/outcome, not refetch mechanics.
Validator/unit tests and API validation tests are part of server validation planning when request DTO rules are non-trivial.
```
