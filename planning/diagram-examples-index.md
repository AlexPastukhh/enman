# Diagram Examples Index

Status: index of approved and planned correct diagram examples.

Only files marked `Approved` are actual canonical examples.

`TODO` rows are placeholders until explicitly created. Do not treat missing TODO files as an error.

## Folder Roles

```text
planning/examples/
= approved canonical examples

planning/diagrams/
= generated scenario packages, scenario overview and consistency reports
```

Do not confuse these folders.

Approved examples are calibration references. They define scenario grammar, layout discipline, semantic color usage, editability expectations and readability standards.

Generated scenario packages are project artifacts that live under `planning/diagrams/` when committed.

## Scenario Diagram Examples

| Example | Draw.io | SVG | PNG | Notes | Status | Purpose |
|---|---|---|---|---|---|---|
| Login Scenario - SC-02 | `planning/examples/scenario-login-correct-v3.drawio` | `planning/examples/scenario-login-correct-v3.svg` | `planning/examples/scenario-login-correct-v3.png` | `planning/examples/scenario-login-correct-v3.md` | Approved | Canonical scenario-flow example: decision branch, include, off-page password recovery, invariant at enforcement point, compact end states, semantic color usage, readable layout |
| Client Request Creation - SC-04 | `planning/examples/scenario-request-creation-correct.drawio` | `planning/examples/scenario-request-creation-correct.svg` | `planning/examples/scenario-request-creation-correct.png` | `planning/examples/scenario-request-creation-correct.md` | TODO | Applicant data alternative, request submit, details/criteria node, correction loop for validation errors, step postconditions, extension links |
| Employee Request Review - SC-07 | `planning/examples/scenario-request-review-correct.drawio` | `planning/examples/scenario-request-review-correct.svg` | `planning/examples/scenario-request-review-correct.png` | `planning/examples/scenario-request-review-correct.md` | TODO | Approve/reject branches, review invariants, optional clarification link, finalization rules |
| Password Recovery - SC-03 | `planning/examples/scenario-password-recovery-correct.drawio` | `planning/examples/scenario-password-recovery-correct.svg` | `planning/examples/scenario-password-recovery-correct.png` | `planning/examples/scenario-password-recovery-correct.md` | TODO | Off-page scenario opened from login, neutral confirmation, account existence not revealed, recovery email/link behavior |
| My Requests / Request List - SC-05 | `planning/examples/scenario-my-requests-correct.drawio` | `planning/examples/scenario-my-requests-correct.svg` | `planning/examples/scenario-my-requests-correct.png` | `planning/examples/scenario-my-requests-correct.md` | TODO | Request list, status visibility, filter/search criteria, select request, link to request details/result |
| Clarification Flow - SC-12A/SC-12B | `planning/examples/scenario-clarification-correct.drawio` | `planning/examples/scenario-clarification-correct.svg` | `planning/examples/scenario-clarification-correct.png` | `planning/examples/scenario-clarification-correct.md` | TODO | Split employee clarification request and client clarification response instead of forcing two actors into one crowded page |
| Global Scenario Overview | `planning/examples/scenario-overview-correct.drawio` | `planning/examples/scenario-overview-correct.svg` | `planning/examples/scenario-overview-correct.png` | `planning/examples/scenario-overview-correct.md` | TODO | Area/navigation map, not a mega-workflow; groups scenarios by Guest/Auth, Client Request, Employee Review, Result/Notification/System areas |

## Approved Example File Conventions

Approved examples may include:

```text
.drawio = editable source
.svg = scalable visual preview
.png = quick visual reference, optional but recommended
.md = semantic explanation of why the example is correct
```

For Login Correct V3, the approved files are `.drawio`, `.svg`, `.png`, and `.md`.

`planning/examples/scenario-login-correct-v3.png` is the approved quick visual reference for the canonical Login example.

For future examples, do not create PNG previews unless a task explicitly asks for them.

## What Approved Examples Control

Approved examples control:

```text
- scenario grammar;
- flow-oriented structure;
- readable label + strict ref style;
- include placement;
- off-page/subscenario link placement;
- invariant attachment to enforcement point;
- compact end-state usage;
- semantic color meanings;
- editability expectations;
- text-fit expectations;
- spacing and connector discipline.
```

Approved examples do **not** force:

```text
- one mandatory background theme for all future diagrams;
- a specific light/dark choice;
- business behavior for unrelated scenarios;
- architecture decisions;
- implementation details;
- repository writes.
```

Theme may be light or dark in future generated packages, but it must be consistent within one package, readable on the chosen background and preserve semantic color meanings.

## Example Rules

```text
Only files marked Approved are actual canonical examples.
TODO rows are placeholders until explicitly created.
Do not treat missing TODO files as an error.
Do not create actual example files during documentation-only planning tasks unless explicitly requested.
Do not use approved examples as a reason to invent business behavior in another scenario.
Do not use approved examples as a reason to force a specific background theme.
```

## Suggested Future Example Priorities

If new approved examples are created, prioritize examples that cover recurring semantic problems:

```text
1. Client Request Creation - SC-04
   - details/criteria node;
   - applicant data alternative;
   - validation correction loop;
   - initial status open question.

2. My Requests / Request List - SC-05
   - request list status visibility;
   - filter/search criteria;
   - select request;
   - request details entry points.

3. Password Recovery - SC-03
   - neutral confirmation;
   - account existence not revealed;
   - recovery email is sent;
   - guest opens email link.

4. Clarification - SC-12A/SC-12B
   - employee request side;
   - client response side;
   - review loop semantics.

5. Global Scenario Overview
   - area/navigation map;
   - no scenario internals;
   - no mega-workflow.
```
