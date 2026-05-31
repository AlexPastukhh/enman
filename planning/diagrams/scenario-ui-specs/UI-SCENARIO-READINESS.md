# UI Scenario Readiness

Status: current readiness snapshot / update before client slice draft refresh
Doc version: v0.1.0  

## Purpose

Track which UI scenario sources are ready enough to drive client slice drafts and UI implementation work.

## Readiness table

| UI scenario | Status | Current assessment | Required before client slice update |
|---|---|---|---|
| `APP-UI-001-app-shell-home-auth-flow-ui.md` | current first pass | Newly added foundation UI scenario for app shell/home/auth/account flow | Use before UI foundation implementation archive |
| `SC-04-request-creation-ui.md` | partial / normalized | Useful source exists; now normalized to template but still needs more field-level detail if request form is refactored deeply | Add exact visible fields/validation if form UI is substantially changed |
| `SC-05-my-requests-ui.md` | mostly complete / normalized | Strongest current UI spec; list/filter/details entry behavior is clear | Add details page UI source separately before own-request-details refactor |
| `SC-10-applicant-data-ui.md` | substantial / canonical ApplicantParty account section | Canonical current ApplicantParty UI source; Account page section owns My Applicant Parties behavior | Add exact edit/delete/archive future specs only when those actions are implemented |
| `SC-10B-my-applicant-parties-ui.md` | deprecated / replaced | Not current as standalone UI scenario | Use `SC-10-applicant-data-ui.md` instead |
| `SC-06-employee-request-dashboard-ui.md` | missing | Employee dashboard UI source not yet written in scenario-ui-specs | Create before refreshing employee dashboard client draft |
| `SC-07A-employee-request-details-ui.md` | missing | Employee request details UI source not yet written in scenario-ui-specs | Create before refreshing employee request details client draft |
| `SC-07B-employee-request-review-actions-ui.md` | missing | Start/approve/reject review action UI source not yet written in scenario-ui-specs | Create before refreshing review action client drafts |
| `SC-13A-agreement-exchange-list-ui.md` | missing | Agreement exchange list UI source not yet written in scenario-ui-specs | Create before refreshing exchange list client draft |
| `SC-13B-agreement-exchange-details-ui.md` | missing | Agreement exchange details UI source not yet written in scenario-ui-specs | Create before refreshing exchange details client draft |
| `SC-13D-start-agreement-exchange-ui.md` | missing | Start exchange action UI source not yet written in scenario-ui-specs | Create before refreshing start exchange client draft |
| `SC-13E-final-refuse-exchange-ui.md` | missing | Final refusal action UI source not yet written in scenario-ui-specs | Create before refreshing final-refuse client draft |
| `SC-14-agreement-documents-ui.md` | future/missing | Document UI/storage is future work | Create when document/reference UI is planned |

## Rule

Before updating a client slice draft or creating a UI implementation archive:

```text
1. Check this readiness file.
2. If the relevant UI scenario is missing or stub-only, write/normalize UI scenario source first.
3. Then update client slice draft.
4. Then implement.
```
