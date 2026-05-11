# Diagram Examples Index

Status: index of approved and planned correct diagram examples.

Only files marked `Approved` are actual canonical examples.

`TODO` rows are placeholders until explicitly created. Do not treat missing TODO files as an error.

## Scenario Diagram Examples

| Example | Draw.io | SVG | PNG | Notes | Status | Purpose |
|---|---|---|---|---|---|---|
| Login Scenario - SC-02 | `planning/examples/scenario-login-correct-v3.drawio` | `planning/examples/scenario-login-correct-v3.svg` | `planning/examples/scenario-login-correct-v3.png` | `planning/examples/scenario-login-correct-v3.md` | Approved | Canonical scenario-flow example: decision branch, include, off-page password recovery, invariant at enforcement point, compact end states, approved dark visual theme |
| Client Request Creation - SC-04 | `planning/examples/scenario-request-creation-correct.drawio` | `planning/examples/scenario-request-creation-correct.svg` | `planning/examples/scenario-request-creation-correct.png` | `planning/examples/scenario-request-creation-correct.md` | TODO | Applicant data alternative, request submit, step postconditions, extension links |
| Employee Request Review - SC-07 | `planning/examples/scenario-request-review-correct.drawio` | `planning/examples/scenario-request-review-correct.svg` | `planning/examples/scenario-request-review-correct.png` | `planning/examples/scenario-request-review-correct.md` | TODO | Approve/reject branches and review invariants |
| Password Recovery - SC-03 | `planning/examples/scenario-password-recovery-correct.drawio` | `planning/examples/scenario-password-recovery-correct.svg` | `planning/examples/scenario-password-recovery-correct.png` | `planning/examples/scenario-password-recovery-correct.md` | TODO | Off-page scenario opened from login |

## Approved Example File Conventions

Approved examples may include:

```text
.drawio = editable source
.svg = scalable visual preview
.png = quick visual reference, optional but recommended
.md = semantic explanation of why the example is correct
```

For Login Correct V3, the approved files are `.drawio`, `.svg`, and `.md`.

`planning/examples/scenario-login-correct-v3.png` is the intended optional quick visual reference name if a PNG preview is added later.

Do not create a PNG preview unless a task explicitly asks for it.

## Example Rules

```text
Only files marked Approved are actual canonical examples.
TODO rows are placeholders until explicitly created.
Do not treat missing TODO files as an error.
Do not create actual example files during documentation-only planning tasks unless explicitly requested.
```
