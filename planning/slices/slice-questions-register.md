# Slice Questions Register

Status: active / applicant-template-per-type synchronized / server validation questions added

## 1. Questions / Decisions

| ID | Local file(s) | Area | Status | Question | Current direction |
|---|---|---|---|---|---|
| `SL-APPL-Q-001` | `SL-APPL-001` | create model | accepted | Does create replace existing ApplicantParties? | No. Create is additive. |
| `SL-APPL-Q-002` | `SL-APPL-001` / `SL-APPL-003` | default | future | What happens to default on create? | Current standalone create preserves existing applicants; explicit per-type default-template persistence remains future. |
| `SL-APPL-Q-003` | `SL-APPL-001` | API identity | accepted | Should standalone create return ApplicantPartyId? | Yes; API support, not behavior item. |
| `SL-APPL-Q-004` | `SL-APPL-001.client` | UI details | accepted | Separate details page required? | No. Details inline. |
| `SL-APPL-Q-005` | `SL-APPL-001` / `SL-APPL-004` | service | implemented | Shared creation service? | Yes; service validates/creates/adds and has no SaveChanges; outer handler commits. |
| `SL-REQ-Q-APPL-001` | `SL-REQ-001` | applicant context | accepted | Current active only? | No. Explicit Existing/New context. |
| `SL-REQ-Q-APPL-002` | `SL-REQ-001` | Existing branch | accepted | Can selected existing be non-default? | Yes, any owned saved ApplicantParty. |
| `SL-REQ-Q-APPL-003` | `SL-REQ-001` | New branch | accepted | Two client calls? | No, one atomic server operation. |
| `SL-REQ-Q-APPL-004` | `SL-REQ-001` | response | open | Return request id? | Not required unless direct detail navigation appears. |
| `SL-REQ-Q-APPL-005` | `SL-REQ-001` | history | future | Snapshot or reference? | Future read/history decision. |
| `CC-VALIDATION-Q-001` | `CC-VALIDATION-001` | validation mechanism | open | Should L1 use manual per-controller validators or a shared validation pipeline/filter? | Start explicit per-slice planning; choose implementation style when first L1 validator is implemented. |
| `CC-VALIDATION-Q-002` | `CC-VALIDATION-001` / `fluentvalidation-error-code-policy-note.md` | error codes | future review | Should old FluentValidation ErrorMessage usage migrate to ErrorCode with WithErrorCode(...)? | Do not migrate blindly; inspect helpers/tests and migrate only with tests. |
| `CC-VALIDATION-Q-003` | `CC-VALIDATION-001` | validation layering | accepted direction | Should handlers/domain still validate if FluentValidation checks DTO shape? | Yes. FluentValidation handles request-shape; handlers/domain own business invariants. |
| `SL-REQ-Q-VALIDATION-001` | `SL-REQ-001` / future implementation | request DTO validation | accepted direction | Should Existing/New applicant context branch rules be FluentValidation request-level rules? | Yes. Existing/New discriminator, mutually exclusive fields and required branch payload belong to request DTO validation. |

## 2. Superseded

```text
- one current active ApplicantParty per account;
- request creation uses server-selected single current active ApplicantParty as target;
- ApplicantPartyId as behavior item;
- E2E asserting refetch.
```
