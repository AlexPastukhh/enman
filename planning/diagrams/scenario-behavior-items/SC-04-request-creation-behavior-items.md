# SC-04 — Request Creation Behavior Items

Status: current source behavior items / explicit applicant context model

## 1. Scenario Behavior Items

| ID | Behavior | Status |
|---|---|---|
| `SC-04-BI-001` | Signed-in client can create a connection request. | current target |
| `SC-04-BI-002` | Client provides request details and object address. | current target |
| `SC-04-BI-003` | Request creation uses one applicant context. | target |
| `SC-04-BI-004` | Request can use an existing owned saved ApplicantParty. | target |
| `SC-04-BI-005` | Existing ApplicantParty can be current/default or any other owned saved ApplicantParty. | future-ready target |
| `SC-04-BI-006` | Request can use new applicant data entered in the request journey. | target |
| `SC-04-BI-007` | New applicant data creates a new ApplicantParty and uses it for the request. | target |
| `SC-04-BI-008` | New ApplicantParty + ConnectionRequest are one atomic user intent. | target |
| `SC-04-BI-009` | Creating request with new applicant data does not replace older ApplicantParties. | target |
| `SC-04-BI-010` | Created request enters InReview. | current target |
| `SC-04-BI-011` | Request appears in My Requests and employee review queue. | dependent read behavior |
| `SC-04-BI-012` | Invalid request/applicant data produces feedback and no partial write. | target |

## 2. Not Behavior Items

```text
- DTO branch nullable mechanics;
- service extraction;
- SaveChanges boundary;
- React Query invalidation.
```
