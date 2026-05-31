# Domain Value Object Draft — RejectionFeedback

Status: draft / extracted with ConnectionRequest aggregate  
Doc version: v0.1.0  
Scope: optional rejection feedback value for employee request review

## 1. Purpose

`RejectionFeedback` represents optional feedback/reason text stored when an employee rejects a request review.

Current synchronized direction: rejection feedback is optional. When supplied, it must be a valid non-empty value within max length.

## 2. Source Inputs

Scenario sources:

```text
planning/diagrams/scenario-text-specs/SC-07B-employee-request-review.md
planning/diagrams/scenario-data/SC-07B-employee-request-review-data.md
```

Behavior items:

```text
REQ-CMD-REJECT-001
REQ-IBS-002
L2-REVIEW-REJECT-002
Q-SC-07B-001 accepted optional current direction
```

Existing implementation sources:

```text
Domain.EnergyManagement/Requests/RejectionFeedback.cs
Domain.EnergyManagement/Requests/RequestReview.cs
Tests.EnergyManagement/Domain/Requests/ConnectionRequestL2ReviewTests.cs
Tests.EnergyManagement/Integration/App/EmployeeRequests/EmployeeRejectRequestReviewIntegrationTests.cs
```

Not checked:

```text
Full API/OpenAPI optionality was not audited in this pass.
```

## 3. Used By

Aggregates:

```text
ConnectionRequest
```

Entities / child records:

```text
RequestReview
```

Application/API references:

```text
RejectReview command / endpoint maps optional feedback text to RejectionFeedback when present.
```

## 4. Shape / Fields

Fields:

```text
Value: string
```

Optionality:

```text
The value object itself represents present feedback.
RequestReview.RejectionFeedback may be null when rejection feedback is omitted.
```

Current max length:

```text
1000 characters
```

## 5. Invariants

| Invariant | Source | Failure/error |
|---|---|---|
| Present feedback must not be blank. | current RejectionFeedback implementation | `ValueIsRequired` |
| Present feedback must not exceed max length. | current RejectionFeedback implementation | `RejectionFeedbackIsTooLong` |
| Rejection feedback is optional at RequestReview level. | SC-07B synchronized direction | no error when omitted |

## 6. Creation / Normalization Rules

Creation:

```text
Create RejectionFeedback only when user supplied meaningful feedback text.
Pass null to RejectReview when feedback is omitted.
```

Normalization:

```text
No trimming/canonicalization rule was extracted in this pass.
```

Rejected values:

```text
blank feedback when trying to create a present RejectionFeedback value;
feedback longer than current max length.
```

## 7. Equality Rule

Equality is based on:

```text
Value
```

Identity is not:

```text
request id;
employee id;
review id.
```

## 8. Validation Boundary

Belongs in value object:

```text
present feedback text non-blank and max-length rule.
```

Belongs in DTO/input validation:

```text
transport field shape and optionality in API request body.
```

Belongs in aggregate/application:

```text
whether rejection can happen now;
who may reject;
request/review state transition.
```

## 9. Persistence / Serialization Notes

```text
Stored as part of RequestReview when present.
Null means rejected without feedback under current direction.
```

## 10. Invalid Examples

| Invalid value/state | Why invalid | Source |
|---|---|---|
| present feedback = blank string | present value object must have meaningful text | current implementation |
| present feedback > 1000 characters | exceeds max length | current implementation |

## 11. Questions / Decisions

Open:

```text
- Should feedback remain optional long-term after API/OpenAPI sync?
- Should blank string be treated as omitted at API/application boundary before value object creation?
```

Accepted:

```text
- Feedback is optional in current synchronized domain direction.
- Present feedback is a value object with value integrity rules.
```

## 12. Source Delta / Change Log

```text
- Extracted as value object draft during ConnectionRequest aggregate extraction.
```
