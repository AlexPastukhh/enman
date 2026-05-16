# Scenario UI Specs

Status: current UI-spec source area / ApplicantParty template-per-type sources synchronized  
Scope: per-scenario UI-visible requirements, UI behavior items, accepted UI conventions and UI questions

## 1. Purpose

Scenario UI specs capture what the user must see, understand, enter, confirm, correct, or be prevented from doing in the UI.

```text
Scenario text spec = use case / business-visible behavior.
Scenario UI spec   = UI-visible requirements and accepted UI decisions.
.client.md         = implementation planning for one concrete client slice.
```

## 2. Source Marker

Use `[UI-SCENARIO]` for UI-visible scenario sources consumed by client sidecars.

A `[UI-SCENARIO]` source is binding source behavior until changed through scenario drafting.

It is not React implementation detail.

## 3. Current UI Spec Files

| File | Scenario | Use |
|---|---|---|
| `SC-04-request-creation-ui.md` | SC-04 Request Creation | Request creation UI-visible behavior, applicant default/prefill/clear/new-applicant behavior, success/error outcomes |
| `SC-10-applicant-data-ui.md` | SC-10 Applicant Data | Account page Applicant Parties section, current/default templates by type, saved list, create/refetch behavior |
| `SC-10B-my-applicant-parties-ui.md` | SC-10B My Applicant Parties | Future saved ApplicantParty management UI |

## 4. ApplicantParty UI Direction

```text
Account page may show current/default templates grouped by applicant type at the top.
Saved ApplicantParties, including non-default profiles, remain visible.
Adding a new ApplicantParty does not silently replace existing defaults when a default already exists.
If no default exists for the type, the first created ApplicantParty may be shown as initial default after server-truth refetch.
```

## 5. UI Specs Vs Client Implementation Conventions

Scenario UI specs define UI-visible outcomes.

Client implementation conventions define reusable ways to implement those outcomes.

When a UI spec relies on a client-wide convention, reference that convention explicitly rather than restating it as scenario/domain behavior.

## 6. Template

```text
# SC-XX — Scenario Name UI Spec

Status:
Source scenario:
Source DATA:
Behavior item sources:
Marker: [UI-SCENARIO]

## 1. Purpose
## 2. UI Source Summary
## 3. UI Behavior Items
## 4. UI State / Feedback Matrix
## 5. Validation / Error Feedback
## 6. Action Availability
## 7. Accessibility Notes
## 8. UI Questions
## 9. Downstream Use
```
