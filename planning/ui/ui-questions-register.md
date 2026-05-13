# UI Questions Register

Status: template / to be filled by UI planning chat  
Scope: unresolved UI alternatives found during test-site UI planning

## 1. Purpose

This file tracks unresolved UI alternatives found during test-site UI planning.

Use it when several UI options are possible and the scenario does not force one solution.

Do not silently choose UI behavior when it depends on unresolved domain or implementation decisions.

## 2. Status Values

```text
Open       — question is unresolved.
Tentative  — current preference exists but can change.
Accepted   — decision accepted for current UI plan.
Rejected   — option rejected.
Deferred   — not needed for current core / future expansion.
```

## 3. Question Template

```text
## [Q:UI-000] Question title

Status:
Open

Affected scenarios:
- SC-...

Affected pages:
- ...

Question:
...

Options:
A. ...
B. ...
C. ...

Current preference:
...

What blocks final decision:
...

Notes:
...
```

## 4. Open Questions

### [Q:UI-001] Where should client go after approved request feedback?

Status:

```text
Open
```

Affected scenarios:

```text
SC-05 — My Requests / Own Request Details
SC-13A — My Agreements
SC-13B — Agreement Proposal Details / Response
```

Affected pages:

```text
Own Request Details
My Agreements
Agreement Proposal Details / Response
```

Question:

```text
When a request becomes Approved, what should be the main UX path for the client?
```

Options:

```text
A. Keep client on Own Request Details and show approved result only.
B. Show link from Own Request Details to My Agreements.
C. Open My Agreements directly after approval feedback.
D. Open Agreement Proposal Details directly if employee proposal already exists.
E. Show both Request Details and My Agreements entries.
```

Current preference:

```text
E for test UI plan, because it covers both request result and agreement discovery without assuming proposal timing.
```

What blocks final decision:

```text
Agreement proposal creation timing:
- approval does not automatically create proposal;
- employee sends proposal separately.
```

Notes:

```text
Do not hardcode email-link mechanics unless scenario explicitly requires it.
```

### [Q:UI-002] How should unavailable review action be represented?

Status:

```text
Open
```

Affected scenarios:

```text
SC-06 — Employee Request Dashboard
SC-07A — Employee Request Details
SC-07B — Employee Request Review
```

Affected pages:

```text
Employee Request Dashboard
Employee Request Details
```

Question:

```text
For Approved/Rejected requests, should Review action be hidden or visible but disabled?
```

Options:

```text
A. Hide Review action for non-InReview requests.
B. Show disabled Review action with explanation.
C. Show action but server rejects if attempted.
```

Current preference:

```text
A or B for UI; server must reject invalid transition regardless.
```

What blocks final decision:

```text
UX preference and implementation detail.
```

Notes:

```text
Domain invariant is fixed: Approved/Rejected cannot be reviewed again in core.
```

### [Q:UI-003] Should accepted proposal be shown as final agreement or still as proposal in core?

Status:

```text
Open
```

Affected scenarios:

```text
SC-13A — My Agreements
SC-13B — Agreement Proposal Details / Response
```

Affected pages:

```text
My Agreements
Agreement Proposal Details / Response
```

Question:

```text
In core, after client accepts an agreement proposal, should the UI present it as an accepted proposal or as a final agreement?
```

Options:

```text
A. Show as Accepted proposal.
B. Show as Agreement in the same My Agreements list.
C. Show separate final-agreement section.
D. Defer final agreement concept until signature/finalization flow exists.
```

Current preference:

```text
A or B for core; avoid implying legal signature/finalization.
```

What blocks final decision:

```text
Meaning of "Accepted" and future signature/legal agreement flow.
```

Notes:

```text
Do not introduce Signed status in core.
```

## 5. Tentative Decisions

To be filled.

## 6. Accepted Decisions

To be filled.

## 7. Deferred Questions

To be filled.
