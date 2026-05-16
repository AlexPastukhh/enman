# Slice Questions Register

Status: active / applicant-template-per-type synchronized

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

## 2. Superseded

```text
- one current active ApplicantParty per account;
- request creation uses server-selected single current active ApplicantParty as target;
- ApplicantPartyId as behavior item;
- E2E asserting refetch.
```
