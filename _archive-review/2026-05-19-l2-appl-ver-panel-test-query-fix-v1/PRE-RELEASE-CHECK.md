# PRE-RELEASE CHECK — L2 Applicant Verification Panel Test Query Fix v1

Status: ready for local application.

Scope:
- Replace only `energymanagement.client/src/features/employee-request/applicant-verification/ui/ApplicantPartyVerificationPanel.test.tsx`.
- No runtime component changes.
- No backend/domain/docs/generated changes.

Reason:
- Full client suite failed only because `ApplicantPartyVerificationPanel.test.tsx` used `screen.getByText(...)` for labels intentionally rendered twice: once in `ApplicantVerificationStatusBadge` and once in panel message.
- The production UI is acceptable; test selectors were too ambiguous.

Fix:
- Use `getAllByText` through a helper and assert both visible instances are present.

Expected targeted check after applying:

```powershell
npm.cmd --prefix .\energymanagement.client run test -- --run ApplicantPartyVerificationPanel
```

Then full gate:

```powershell
npm.cmd --prefix .\energymanagement.client run test -- --run
npm.cmd --prefix .\energymanagement.client run build
```
