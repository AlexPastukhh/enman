# SC-04 — Request Creation UI Scenario

Status: partial / normalized UI scenario source  
Doc version: v0.1.0  
Applies to: client request creation screen  
Actors: Client  
Related scenario: `SC-04 — Client Request Creation`  
Related slices: `SL-REQ-001-create-connection-request`, client create request sidecar when migrated

## 1. User Goal

The authenticated Client creates a new connection request and can use saved/current applicant data when available.

## 2. Screen Entry Points

```text
Client opens Create Request
        ↓
System shows request creation form
        ↓
Client fills request details and applicant section
        ↓
Client submits request
        ↓
System shows success and moves user to My Requests/read context
```

## 3. Screen Composition

```text
Create Request page
  Page title / intro
  Request details section
  Address/object section
  Applicant section
  Validation/root feedback area
  Submit action area
```

## 4. Visible Data

The UI should show:

```text
request details/address fields
applicant section
current/default ApplicantParty prefilled values when available
empty applicant fields when no current/default exists
```

## 5. Actions

```text
Submit request
Clear prefilled applicant fields
Enter new applicant data
```

Future UI may allow choosing any owned saved ApplicantParty from dropdown/list/search.

## 6. State Matrix

| State | Visible UI | Available actions | Notes |
|---|---|---|---|
| Current/default ApplicantParty exists | Applicant fields prefilled | Submit, clear/edit fields | Prefill is convenience, not lock |
| No current/default ApplicantParty | Applicant fields empty | Submit after filling required data | No blocking saved applicant requirement |
| Validation error | Field/root errors visible | Correct and resubmit | Errors allow correction |
| Success | Success outcome + move to My Requests/read context | Open/view created request if available | Exact navigation belongs to client slice |

## 7. Empty / Loading / Error States

```text
loading: stable form/page placeholder
validation errors: visible near fields or form-level area
server errors: visible and actionable
```

## 8. Actor-Specific Differences

Client-only screen first pass.

## 9. Feedback / Validation Requirements

```text
field-level validation errors near fields
root/server errors in form error area
submit validates immediately
successful creation shows visible outcome
```

## 10. Accessibility Notes

```text
form fields have visible connected labels
field errors use aria-describedby/aria-invalid when visible
submit action is a native button
```

## 11. Out of Scope

```text
React component placement
query key mechanics
backend command implementation
future saved ApplicantParty picker
employee review UI
```

## 12. Related Client Slice Drafts

```text
client create request slice draft when migrated to planning/slices/client/
```
