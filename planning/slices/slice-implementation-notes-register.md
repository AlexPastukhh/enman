# Slice Implementation Notes Register

Status: active / applicant-template-per-type synchronized

| ID | Related slice | Layer | Note | Status |
|---|---|---|---|---|
| `NOTE-APPL-CREATE-001` | `SL-APPL-001` | API/client | Standalone create returns ApplicantPartyId for identity/cache/future actions. | accepted |
| `NOTE-APPL-CREATE-002` | `SL-APPL-001` | behavior | Creating ApplicantParty is additive; integration coverage verifies second create does not delete/deactivate first. | implemented for standalone create |
| `NOTE-APPL-CREATE-003` | `SL-APPL-003` | default | Explicit per-type default-template persistence remains future; current standalone create does not implement default selection. | future |
| `NOTE-APPL-SVC-001` | `SL-APPL-001` / `SL-APPL-004` | application | Shared creation service exists, has no SaveChanges, and standalone handler commits. | implemented for standalone create |
| `NOTE-REQ-APPL-001` | `SL-REQ-001` | API | Target request create uses explicit Existing/New applicant context. | accepted |
| `NOTE-REQ-APPL-002` | `SL-REQ-001` | transaction | New applicant + request must be atomic. | accepted |
| `NOTE-REQ-APPL-003` | future request `.client.md` | client UX | Future UI can choose any owned saved ApplicantParty; default is only initial prefill. | future |
| `NOTE-APPL-CLIENT-001` | `SL-APPL-001.client` | testing | E2E asserts visible state, not refetch mechanics. | accepted |
