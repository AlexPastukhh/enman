# Domain Value Object Draft — RejectionFeedback

Status: draft / extracted with ConnectionRequest aggregate  
Doc version: v0.1.0  
Scope: optional rejection feedback value for employee request review


## 1. Purpose

```text
Sources:
  Format/process:
    - planning/domain/value-object-drafting-workflow.md @ Doc version: v0.2.0
    - planning/domain/value-object-draft-template.md @ Doc version: v0.2.0
    - planning/domain/domain-modeling-principles.md @ Doc version: v0.1.0
  Content:
    - planning/domain/aggregates/connection-request.md @ Doc version: v0.1.0
    - planning/domain/scenario-to-aggregate-map.md @ Doc version: v0.1.0
  Internal dependencies:
    - none
  Not checked:
    - current implementation/test files are prior extraction evidence, not freshly rechecked in this pass
    - full API/OpenAPI/UI behavior was not audited in this pass
    - full EF/persistence mapping was not audited in this pass
```

`RejectionFeedback` represents optional feedback/reason text stored when an employee rejects a request review.

Current synchronized direction: rejection feedback is optional. When supplied, it must be a valid non-empty value within max length.

## 2. Source Inputs

```text
Sources:
  Format/process:
    - planning/domain/value-object-drafting-workflow.md @ Doc version: v0.2.0
    - planning/domain/value-object-draft-template.md @ Doc version: v0.2.0
    - planning/source-cascade-sync-workflow.md @ Doc version: v0.3.0
    - planning/SOURCE-SECTION-SOURCES-TEMPLATE.md @ Doc version: v0.2.0
  Content:
    - planning/domain/aggregates/connection-request.md @ Doc version: v0.1.0
    - planning/domain/scenario-to-aggregate-map.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-text-specs/SC-07B-employee-request-review.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-data/SC-07B-employee-request-review-data.md @ Doc version: v0.1.0
    - planning/diagrams/scenario-behavior-items/SC-07B-employee-request-review-behavior-items.md @ Doc version: v0.1.0
    - Domain.EnergyManagement/Requests/RejectionFeedback.cs @ implementation evidence from prior extraction/source pass, version not applicable
  Internal dependencies:
    - none
  Not checked:
    - current implementation/test files are prior extraction evidence, not freshly rechecked in this pass
    - full API/OpenAPI/UI behavior was not audited in this pass
    - full EF/persistence mapping was not audited in this pass
```

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

```text
Sources:
  Format/process:
    - planning/domain/value-object-drafting-workflow.md @ Doc version: v0.2.0
    - planning/domain/value-object-draft-template.md @ Doc version: v0.2.0
  Content:
    - planning/domain/aggregates/connection-request.md @ Doc version: v0.1.0
    - Domain.EnergyManagement/Requests/RejectionFeedback.cs @ implementation evidence from prior extraction/source pass, version not applicable
  Internal dependencies:
    - Source Inputs
  Not checked:
    - current implementation/test files are prior extraction evidence, not freshly rechecked in this pass
    - full API/OpenAPI/UI behavior was not audited in this pass
    - full EF/persistence mapping was not audited in this pass
```

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

```text
Sources:
  Format/process:
    - planning/domain/value-object-drafting-workflow.md @ Doc version: v0.2.0
    - planning/domain/value-object-draft-template.md @ Doc version: v0.2.0
  Content:
    - planning/domain/aggregates/connection-request.md @ Doc version: v0.1.0
    - planning/domain/scenario-to-aggregate-map.md @ Doc version: v0.1.0
    - Domain.EnergyManagement/Requests/RejectionFeedback.cs @ implementation evidence from prior extraction/source pass, version not applicable
  Internal dependencies:
    - Source Inputs
    - Used By
  Not checked:
    - current implementation/test files are prior extraction evidence, not freshly rechecked in this pass
    - full API/OpenAPI/UI behavior was not audited in this pass
    - full EF/persistence mapping was not audited in this pass
```

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

```text
Sources:
  Format/process:
    - planning/domain/value-object-drafting-workflow.md @ Doc version: v0.2.0
    - planning/domain/value-object-draft-template.md @ Doc version: v0.2.0
    - planning/domain/domain-modeling-principles.md @ Doc version: v0.1.0
  Content:
    - planning/domain/aggregates/connection-request.md @ Doc version: v0.1.0
    - planning/domain/scenario-to-aggregate-map.md @ Doc version: v0.1.0
    - Domain.EnergyManagement/Requests/RejectionFeedback.cs @ implementation evidence from prior extraction/source pass, version not applicable
  Internal dependencies:
    - Shape / Fields
  Not checked:
    - current implementation/test files are prior extraction evidence, not freshly rechecked in this pass
    - full API/OpenAPI/UI behavior was not audited in this pass
    - full EF/persistence mapping was not audited in this pass
```

| Invariant | Source | Failure/error |
|---|---|---|
| Present feedback must not be blank. | current RejectionFeedback implementation | `ValueIsRequired` |
| Present feedback must not exceed max length. | current RejectionFeedback implementation | `RejectionFeedbackIsTooLong` |
| Rejection feedback is optional at RequestReview level. | SC-07B synchronized direction | no error when omitted |

## 6. Creation / Normalization Rules

```text
Sources:
  Format/process:
    - planning/domain/value-object-drafting-workflow.md @ Doc version: v0.2.0
    - planning/domain/value-object-draft-template.md @ Doc version: v0.2.0
  Content:
    - planning/domain/aggregates/connection-request.md @ Doc version: v0.1.0
    - planning/domain/scenario-to-aggregate-map.md @ Doc version: v0.1.0
    - Domain.EnergyManagement/Requests/RejectionFeedback.cs @ implementation evidence from prior extraction/source pass, version not applicable
  Internal dependencies:
    - Shape / Fields
    - Invariants
  Not checked:
    - current implementation/test files are prior extraction evidence, not freshly rechecked in this pass
    - full API/OpenAPI/UI behavior was not audited in this pass
    - full EF/persistence mapping was not audited in this pass
```

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

```text
Sources:
  Format/process:
    - planning/domain/value-object-drafting-workflow.md @ Doc version: v0.2.0
    - planning/domain/value-object-draft-template.md @ Doc version: v0.2.0
  Content:
    - planning/domain/aggregates/connection-request.md @ Doc version: v0.1.0
    - planning/domain/scenario-to-aggregate-map.md @ Doc version: v0.1.0
    - Domain.EnergyManagement/Requests/RejectionFeedback.cs @ implementation evidence from prior extraction/source pass, version not applicable
  Internal dependencies:
    - Shape / Fields
  Not checked:
    - current implementation/test files are prior extraction evidence, not freshly rechecked in this pass
    - full API/OpenAPI/UI behavior was not audited in this pass
    - full EF/persistence mapping was not audited in this pass
```

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

```text
Sources:
  Format/process:
    - planning/domain/value-object-drafting-workflow.md @ Doc version: v0.2.0
    - planning/domain/value-object-draft-template.md @ Doc version: v0.2.0
    - planning/domain/domain-modeling-principles.md @ Doc version: v0.1.0
  Content:
    - planning/domain/aggregates/connection-request.md @ Doc version: v0.1.0
    - planning/domain/scenario-to-aggregate-map.md @ Doc version: v0.1.0
    - Domain.EnergyManagement/Requests/RejectionFeedback.cs @ implementation evidence from prior extraction/source pass, version not applicable
  Internal dependencies:
    - Invariants
    - Creation / Normalization Rules
  Not checked:
    - current implementation/test files are prior extraction evidence, not freshly rechecked in this pass
    - full API/OpenAPI/UI behavior was not audited in this pass
    - full EF/persistence mapping was not audited in this pass
```

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
Sources:
  Format/process:
    - planning/domain/value-object-drafting-workflow.md @ Doc version: v0.2.0
    - planning/domain/value-object-draft-template.md @ Doc version: v0.2.0
  Content:
    - planning/domain/aggregates/connection-request.md @ Doc version: v0.1.0
    - Domain.EnergyManagement/Requests/RejectionFeedback.cs @ implementation evidence from prior extraction/source pass, version not applicable
  Internal dependencies:
    - Shape / Fields
    - Used By
  Not checked:
    - current implementation/test files are prior extraction evidence, not freshly rechecked in this pass
    - full API/OpenAPI/UI behavior was not audited in this pass
    - full EF/persistence mapping was not audited in this pass
```

```text
Stored as part of RequestReview when present.
Null means rejected without feedback under current direction.
```

## 10. Invalid Examples

```text
Sources:
  Format/process:
    - planning/domain/value-object-drafting-workflow.md @ Doc version: v0.2.0
    - planning/domain/value-object-draft-template.md @ Doc version: v0.2.0
  Content:
    - planning/domain/aggregates/connection-request.md @ Doc version: v0.1.0
    - planning/domain/scenario-to-aggregate-map.md @ Doc version: v0.1.0
    - Domain.EnergyManagement/Requests/RejectionFeedback.cs @ implementation evidence from prior extraction/source pass, version not applicable
  Internal dependencies:
    - Invariants
    - Creation / Normalization Rules
    - Validation Boundary
  Not checked:
    - current implementation/test files are prior extraction evidence, not freshly rechecked in this pass
    - full API/OpenAPI/UI behavior was not audited in this pass
    - full EF/persistence mapping was not audited in this pass
```

| Invalid value/state | Why invalid | Source |
|---|---|---|
| present feedback = blank string | present value object must have meaningful text | current implementation |
| present feedback > 1000 characters | exceeds max length | current implementation |

## 11. Questions / Decisions

```text
Sources:
  Format/process:
    - planning/domain/value-object-drafting-workflow.md @ Doc version: v0.2.0
    - planning/domain/value-object-draft-template.md @ Doc version: v0.2.0
  Content:
    - planning/domain/aggregates/connection-request.md @ Doc version: v0.1.0
    - planning/domain/scenario-to-aggregate-map.md @ Doc version: v0.1.0
  Internal dependencies:
    - Source Inputs
    - Invariants
    - Validation Boundary
  Not checked:
    - current implementation/test files are prior extraction evidence, not freshly rechecked in this pass
    - full API/OpenAPI/UI behavior was not audited in this pass
    - full EF/persistence mapping was not audited in this pass
```

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
Sources:
  Format/process:
    - planning/source-cascade-sync-workflow.md @ Doc version: v0.3.0
  Content:
    - planning/domain/aggregates/connection-request.md @ Doc version: v0.1.0
    - planning/domain/scenario-to-aggregate-map.md @ Doc version: v0.1.0
    - Domain.EnergyManagement/Requests/RejectionFeedback.cs @ implementation evidence from prior extraction/source pass, version not applicable
  Internal dependencies:
    - all changed sections in this file
  Not checked:
    - current implementation/test files are prior extraction evidence, not freshly rechecked in this pass
    - full API/OpenAPI/UI behavior was not audited in this pass
    - full EF/persistence mapping was not audited in this pass
```

```text
- Extracted as value object draft during ConnectionRequest aggregate extraction.
- Added local section-level Sources blocks in DOM-VO-SRC-ALL-1 without changing domain semantics.
```
