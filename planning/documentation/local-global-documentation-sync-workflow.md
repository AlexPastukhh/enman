# Local / Global Documentation Synchronization Workflow

Status: current documentation governance workflow  
Scope: how local planning files, shared indexes and shared registers stay synchronized

## 1. Purpose

Planning docs must be readable without a long external prompt.

A future chat should be able to start from:

```text
planning/README.md
```

follow the read order and understand:

```text
- where local details live;
- which shared index/register must know about them;
- which questions are local only;
- which questions must be visible globally;
- which files must be updated together.
```

This workflow defines the local-to-global sync rule.

## 2. Core Rule

Local sections are required, but local-only documentation is not enough when the information affects future work.

Whenever a local planning file changes, classify whether the change also needs a shared index/register update.

```text
local file detail
        ↓
classify responsibility
        ↓
update local file
        ↓
update shared index/register if needed
        ↓
update navigation if files were added/moved/superseded
```

## 3. Local vs Global

Local files own detailed context.

Examples:

```text
- one backend slice file;
- one .client.md sidecar;
- one scenario text spec;
- one DATA spec;
- one cross-cutting slice;
- one ADR candidate note.
```

Global files own discoverability, shared status and cross-file visibility.

Examples:

```text
- planning/README.md;
- folder README.md files;
- planning/planning-doc-responsibility-map.md;
- planning/slices/slice-questions-register.md;
- planning/slices/slice-extension-points-register.md;
- planning/slices/slice-implementation-notes-register.md;
- planning/diagrams/scenario-questions-register.md;
- planning/adr/architecture-decision-notes.md;
- planning/adr/adr-candidates.md.
```

## 4. Question Synchronization Rule

Every local `Questions / Decisions` section should put open questions and unresolved risks first.

Use this order:

```text
1. open questions;
2. unresolved behavior / contract / design risks;
3. accepted decisions.
```

A local question must be mirrored to a shared register when it is still relevant after the local draft and can affect:

```text
- another slice;
- a future slice;
- a client sidecar;
- scenario meaning;
- API contract;
- security requirement;
- testing/E2E responsibility;
- extension/change pressure;
- architecture explanation;
- diagram interpretation.
```

The local file keeps detailed context.

The shared register provides one place to find the question later.

## 5. Slice Question Register Rule

Use:

```text
planning/slices/slice-questions-register.md
```

as the shared overview of currently relevant local slice questions.

It should mirror every local slice/client/cross-cutting question that is:

```text
- open;
- future review;
- accepted direction but still important for future work;
- unresolved risk;
- cross-slice relevant;
- needed before starting another slice/client sidecar;
- likely to affect API/client/testing/security/architecture.
```

It does not replace local `Questions / Decisions` sections.

It does not need to keep tiny resolved local drafting questions forever.

If a local question remains local only, say why:

```text
Local only because:
- affects only this slice;
- no cross-slice consumer yet;
- not an extension/change pressure;
- not an architecture decision;
- resolved inside this draft and no longer affects future work.
```

## 6. Which Shared File To Update

| Local discovery | Shared target |
|---|---|
| Currently relevant slice question | `planning/slices/slice-questions-register.md` |
| Extension point, change pressure, anti-coupling decision | `planning/slices/slice-extension-points-register.md` |
| Future implementation/client/testing note not assigned to active file | `planning/slices/slice-implementation-notes-register.md` |
| Scenario/domain ambiguity | `planning/diagrams/scenario-questions-register.md` or `scenario-clarifications/` |
| New behavior item | `planning/diagrams/scenario-behavior-items/` |
| Client-wide convention | `planning/client/cross-cutting/` |
| API contract rule | `planning/api/` or relevant `CC-API` / `CC-CONST` slice |
| Architecture-wide accepted decision | `planning/adr/architecture-decision-notes.md` |
| Possible future full ADR | `planning/adr/adr-candidates.md` |
| New/moved/superseded doc | folder README + `planning/README.md` + responsibility map |

## 7. Back-Reference Rule

When a local question/note is mirrored into a shared register, leave a local back-reference.

Example:

```text
Shared register:
- planning/slices/slice-questions-register.md / Q-SL-REQ-001
- planning/slices/slice-extension-points-register.md / EPRESS-REQ-DOCS-001
```

The shared register should also point back to local files.

## 8. Navigation Rule

When adding, moving or superseding docs, update:

```text
planning/README.md
planning/planning-doc-responsibility-map.md
folder README.md
```

Update `planning/planning-agent-protocol.md` only when workflow behavior changes.

Do not leave orphan docs.

Do not add a folder without a README unless the user explicitly asks for a minimal one-file archive.

## 9. Status Reconciliation Rule

When implementation or generated artifacts changed, local docs and shared navigation/status docs must agree.

Check for:

```text
- local doc says planned but implementation exists;
- central README says implemented but local slice says planned;
- register says open but local doc says accepted;
- local question was resolved but shared register still says open;
- future review item is written like a current defect;
- planned work is overclaimed as implemented.
```

## 10. Preflight Checklist

Before producing a documentation archive, check:

```text
1. Which local files changed?
2. Does a shared index/register need to know?
3. Are important open questions first locally?
4. Are important local questions mirrored globally?
5. Are resolved/shared statuses aligned?
6. Are new files visible from README navigation?
7. Does the responsibility map know the owner?
8. Are local-only questions explicitly justified?
```

## 11. Do Not

```text
- Do not hide important questions only in a local slice table.
- Do not duplicate all local prose into registers.
- Do not treat the extension register as the only slice question register.
- Do not keep stale register rows after local docs resolve a question.
- Do not update central README only while detailed docs remain stale.
- Do not add docs without navigation/responsibility updates.
```
