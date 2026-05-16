# Slice Implementation Notes Register

Status: active / applicant-template-per-type synchronized

| ID | Related slice | Layer | Note | Status |
|---|---|---|---|---|
| `NOTE-APPL-CREATE-001` | `SL-APPL-001` | API/client | Standalone create returns ApplicantPartyId for identity/cache/future actions. | accepted |
| `NOTE-APPL-CREATE-002` | `SL-APPL-001` | behavior | Creating ApplicantParty is additive, not replacement. | accepted |
| `NOTE-APPL-CREATE-003` | `SL-APPL-001` | default | First of type may initialize default; second same-type does not switch. | accepted |
| `NOTE-APPL-SVC-001` | `SL-APPL-004` | application | Shared creation service, no SaveChanges; outer handler commits. | accepted |
| `NOTE-REQ-APPL-001` | `SL-REQ-001` | API | Target request create uses explicit Existing/New applicant context. | accepted |
| `NOTE-REQ-APPL-002` | `SL-REQ-001` | transaction | New applicant + request must be atomic. | accepted |
| `NOTE-REQ-APPL-003` | future request `.client.md` | client UX | Future UI can choose any owned saved ApplicantParty; default is only initial prefill. | future |
| `NOTE-APPL-CLIENT-001` | `SL-APPL-001.client` | testing | E2E asserts visible state, not refetch mechanics. | accepted |
