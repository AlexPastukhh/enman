# CL-FEEDBACK-001 — Client Feedback Messages

Status: implementation convention draft / client-wide cross-cutting concern  
Type: client cross-cutting convention / reusable client concern  
Scope: user-visible feedback messages for success, info, warning, global/action errors  
Used by: logout, login/register/applicant/request forms, command success flows, global API errors  
Not a domain/API requirement.

## 1. Purpose

Client features need a consistent way to show user-visible feedback.

This concern prevents every `.client.md` slice from inventing its own success/error/warning display rules.

It covers message purpose and placement selection, not a final UI component implementation.

## 2. Sources

```text
planning/client/README.md
planning/client/cross-cutting/CL-ERROR-HANDLING-001-client-server-errors.md
planning/client/cross-cutting/CL-COMMAND-001-command-success-without-required-response-body.md
planning/slices/client-architecture-principles.md
```

Client-wide conventions belong in `planning/client/cross-cutting`.

Concrete feature behavior stays in matching `.client.md` sidecars.

## 3. Visual Concern / UI Flow

```text
┌──────────────────────────────────────────────┐
│ Client feature/action/read flow              │
│ login / logout / applicant save / request    │
└──────────────┬───────────────────────────────┘
               │
               ▼
┌──────────────────────────────────────────────┐
│ Outcome needs user-visible feedback?         │
└──────────────┬───────────────────────────────┘
               │
       ┌───────┼───────────────┬──────────────┐
       │       │               │              │
    success   info          warning          error
       │       │               │              │
       ▼       ▼               ▼              ▼
┌──────────┐ ┌──────────┐ ┌────────────┐ ┌────────────────┐
│ success  │ │ neutral  │ │ caution /  │ │ field / action │
│ message  │ │ message  │ │ confirm    │ │ page / global  │
└────┬─────┘ └────┬─────┘ └─────┬──────┘ └───────┬────────┘
     │            │             │                │
     ▼            ▼             ▼                ▼
┌──────────────────────────────────────────────┐
│ Feedback surface chosen by context           │
│ field error / form alert / page banner /     │
│ global notification / persistent status      │
└──────────────────────────────────────────────┘
```

## 4. Message Types

| Type | Use for | Typical surface |
|---|---|---|
| success | Completed user action with visible confirmation need | notification, page banner, inline success |
| info | Neutral state change or useful context | page banner, inline hint, notification |
| warning | Action is allowed but risky/needs attention | confirmation, warning banner, inline warning |
| field error | Error tied to a specific field | field message |
| form/action error | Error tied to a form or action submit | form alert / action alert |
| page/global error | Error not owned by one form/action | page error boundary / global notification |
| persistent status | Long-lived state user should see while on page | page status area |

## 5. Selection Rule

Choose the smallest feedback surface that matches the behavior:

```text
field-specific problem -> field error
form/action failure -> form/action alert
page load failure -> page/global surface
cross-feature/session problem -> global notification or shell-level feedback
success that changes page state visibly -> success message optional unless scenario requires it
success without visible state change -> success message likely required
```

Do not use a global message when a field/form message is enough.

Do not show success when the action cannot be confirmed.

Do not turn this convention into a domain/API requirement.

## 6. Relationship To Existing Conventions

Use `CL-ERROR-HANDLING-001` for API/ProblemDetails mapping.

Use `CL-COMMAND-001` when HTTP success is enough to confirm a command and no response body is needed.

Use this file to decide how the resulting user-visible message should be surfaced.

## 7. Consumer Rule For `.client.md` Sidecars

A client sidecar should state:

```text
- which outcome needs feedback;
- feedback type: success/info/warning/field/form/page/global;
- whether success message is required or optional;
- how unexpected failure avoids false success;
- whether feedback is local to the feature or global/shell-level;
- whether the exact copy is final, placeholder, or future UX copy.
```

## 8. Logout Consumer Direction

For logout:

```text
- success message is not required by default;
- Home/public guest state is the visible success outcome;
- 401 logout response is treated as stale/already unauthenticated session and should clear local auth state;
- unexpected failure should show action/global error feedback and must not falsely show successful logout.
```

Detailed logout behavior belongs in:

```text
planning/slices/SL-AUTH-003-logout.client.md
```

## 9. Questions / Decisions

| ID | Question status | Question | Assumption / current direction | Impact |
|---|---|---|---|---|
| CL-FEEDBACK-Q-001 | assumption | Should project start with local action alerts or global notification host? | Use local action/form/page alerts first; introduce global host when multiple features need shell-level messages. | affects implementation seam and tests |
| CL-FEEDBACK-Q-002 | future review | Should success messages auto-dismiss by default? | Feature decides. Applicant create uses self-dismissing success notification; logout success does not require message. | affects UI consistency |
| CL-FEEDBACK-Q-003 | accepted direction | Should failures ever display success outcome? | No. Unexpected failure must show failure feedback or preserve prior state. | affects logout/request/applicant tests |

## 10. Test / Verification Direction

Feature tests should assert feedback behavior at the feature boundary.

Shared feedback infrastructure tests are needed only when a reusable implementation component/store is introduced.

Do not add broad cross-feature E2E only to test message plumbing.
