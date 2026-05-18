# Merge Risk Report

## Summary

Risk level: medium for planning semantics, low for runtime.

The archive changes planning drafts only. No runtime implementation, tests, generated artifacts, navigation or UI/page-flow files are included.

## Risks checked

| Risk | Result / mitigation |
|---|---|
| Losing server route/auth/body/response direction | Preserved `GET /api/agreement-exchanges/{exchangeId}`, Client/Employee auth, no body/query first pass, `200 OK` details DTO. |
| Losing ClientAccountId visibility guard | Preserved as server-side filter and privacy guard. |
| Accidentally adding ResponsibleEmployeeId guard | Explicitly forbidden in both replacement drafts. |
| Losing document boundary | Preserved: document refs only, no bytes/download/upload. |
| Turning read draft into command behavior | Explicitly forbidden; no send/accept/final-refuse methods in read slice. |
| Losing client shared endpoint/query/widget decision | Preserved; actor-specific wrappers remain forbidden first pass. |
| Losing client question IDs | `Q-L2-AGR-DETAILS-CLIENT-001` through `011` retained. |
| Over-claiming implementation status | Drafts state implementation was not rechecked. |
| Touching UI/runtime/page flow | Not included; out of scope in both drafts. |

## Known planning gaps intentionally preserved

```text
- stable source behavior item IDs pending scenario/source registry
- generated endpoint/DTO names pending OpenAPI confirmation
- dedicated UI scenario source pending
- implementation not rechecked in this pass
```
