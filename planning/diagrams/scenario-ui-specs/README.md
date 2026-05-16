# Scenario UI Specs

Status: current UI-spec source area  
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

## 3. What Belongs Here

```text
- UI-visible requirements;
- UI behavior items;
- accepted UI conventions for this scenario;
- required user feedback;
- empty/loading/error/forbidden states;
- action availability expectations;
- validation feedback expectations;
- confirmation/warning behavior;
- accessibility-relevant expectations;
- questions that may affect scenario/client planning.
```

## 4. What Does Not Belong Here

```text
- React component tree;
- route file layout;
- hooks/query/mutation placement;
- CSS module filenames;
- API adapters;
- concrete Testing Library tests;
- final visual design.
```

Those belong in `.client.md` sidecars or client implementation docs.

## 5. Current UI Spec Files

| File | Scenario | Use |
|---|---|---|
| `SC-04-request-creation-ui.md` | SC-04 Request Creation | Request creation UI-visible behavior, applicant template prefill, missing-template empty fields, new ApplicantParty creation and set-current/default offer |
| `SC-10-applicant-data-ui.md` | SC-10 Applicant Data | Account page applicant data UI-visible behavior, per-type current/default templates and saved ApplicantParty creation |
| `SC-10B-my-applicant-parties-ui.md` | SC-10B My Applicant Parties | Future management of all saved ApplicantParty profiles |

## 6. UI Specs Vs Client Implementation Conventions

Scenario UI specs define UI-visible outcomes.

Client implementation conventions define reusable ways to implement those outcomes.

A UI spec should not turn an implementation convention into a domain or API requirement unless the scenario explicitly needs it.

Example:

```text
UI-visible requirement:
  after successful request creation, show success outcome and move user to My Requests.

Client implementation convention:
  if returned entity data is not needed, HTTP success is enough for command confirmation.

Not implied:
  domain/API must return requestId/status by default.
```

Use client-wide conventions from:

```text
planning/client/cross-cutting/
```

## 7. UI Behavior Items

UI behavior items answer:

```text
What must the user see, understand, enter, confirm, correct,
or be unable to do in the UI?
```

They complement domain/scenario behavior items.

Scenario UI behavior items should also be mirrored into:

```text
planning/diagrams/scenario-behavior-items/
```

so slice/client chats can reference stable IDs.

## 8. Status Values

```text
requirement
accepted convention
project decision
open question
deferred
superseded
future direction
```

In this project, an accepted convention is binding until explicitly changed.

## 9. Creation Rule

Do not create every scenario UI spec in advance.

Create a concrete `SC-XX-...-ui.md` file when UI planning starts for that scenario or when a UI-visible decision must not be lost.

## 10. Template

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

## 11. Downstream Use

Scenario UI specs feed `.client.md` UI Behavior Coverage, Scenario / DATA / UI Spec Coverage, component discovery, accessibility contract, client tests, E2E and UI-related scenario questions.

When a UI spec relies on a client-wide convention, reference that convention explicitly rather than restating it as scenario/domain behavior.
