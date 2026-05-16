# SC-04 — Request Creation UI Spec

Status: current UI-spec source / explicit applicant context model  
Marker: `[UI-SCENARIO]`

## 1. UI Behavior

```text
- Request creation UI shows request details/address fields.
- Request creation UI shows applicant section.
- Current/default ApplicantParty can prefill applicant fields.
- If no current/default exists, applicant fields are empty.
- If fields are prefilled, user can clear them and enter new data.
- Future UI may allow choosing any owned saved ApplicantParty from dropdown/list/search.
- Successful request creation shows success outcome and moves to My Requests/read context.
- Errors are visible and allow correction.
```

## 2. Testing Note

E2E should assert visible request creation outcome and My Requests visibility, not internal cache/refetch mechanics.
