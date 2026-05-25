# Implemented Slice Draft Sync Workflow

Status: current transitional workflow for refactoring existing slice drafts that already have implementation  
Scope: synchronize implemented slice drafts against current sources, current docs, current code and current tests

## 1. Purpose

This workflow is for old or legacy slice drafts that already have code and tests.

Such drafts are not ordinary draft-only planning docs. They must be synchronized against:

```text
current scenario/source files;
current domain files;
current slice source mapping;
actual implementation files;
actual tests.
```

Do not rewrite an implemented slice draft just to match the newest template without checking code and tests.

This workflow intentionally does not assume that a full source/version register already exists. Exact source/version cascade sync is future work.

## 2. Current Source Model

Use current files that actually exist in the repo.

Scenario/source inputs may include:

```text
planning/diagrams/scenario-text-specs/
planning/diagrams/scenario-data/
planning/diagrams/scenario-ui-specs/
planning/diagrams/scenario-behavior-items/
planning/diagrams/scenario-clarifications/
planning/diagrams/scenario-cross-cutting/
```

Slice source mapping currently lives in:

```text
planning/slices/slice-scenario-flow-behavior-register.md
```

Domain inputs currently live in the domain/tables layer, including:

```text
planning/tables/
planning/tables/domain-drafts/
planning/domain-draft-generation-guide.md
```

Implementation truth comes from:

```text
current branch code;
current tests;
migrations;
generated API/OpenAPI artifacts when relevant;
runtime screenshots only when the task explicitly includes them.
```

Future source/version usage may later be tracked by a source usage register, but this workflow must not pretend that register exists before it is created.

## 3. When To Use

Use this workflow when:

```text
a slice draft already has implementation;
a legacy L1/L2 draft needs migration to current structure;
a draft looks stale compared to current scenario/domain/source mapping;
implementation exists but draft lacks source/domain/slice coverage snapshot;
implementation exists but test trace is missing;
code behavior and documented behavior may differ.
```

## 4. Primary Rule

```text
Implemented slice sync is not only document rewrite.

It is a sync pass:
  current sources / source mapping / domain docs
    vs
  existing draft
    vs
  actual implementation
    vs
  actual tests.
```

If expected source behavior appears to have changed, stop and report the drift. Do not silently rewrite source meaning from implementation evidence.

## 5. Required Preflight

Before updating the draft:

```text
1. Identify the slice and current draft path.
2. Read planning/slices/slice-responsibility-map.md.
3. Read planning/slices/slice-scenario-flow-behavior-register.md.
4. Read relevant scenario/source files.
5. Read relevant domain files.
6. Read the existing slice draft.
7. Inspect current implementation files.
8. Inspect current tests.
9. Identify drift type.
10. Explain whether this is docs-only, code/test sync, source/domain sync, or blocked.
```

If current source/domain meaning is unclear:

```text
STOP.
Output Source / Domain Drift Report.
Do not rewrite the implemented slice draft as if the target behavior were certain.
```

Repository edits, file deletion, file moves, commits, implementation changes and test changes still require explicit user instruction.

## 6. Drift Types

Use these statuses:

```text
implemented-current
implemented-needs-doc-sync
implemented-needs-code-sync
implemented-needs-test-sync
implemented-stale-source
implemented-stale-domain
implemented-stale-source-and-domain
implemented-docs-ahead-of-code
deprecated/replaced
blocked
```

Meaning:

```text
implemented-current:
  sources, domain docs, draft, code and tests agree.

implemented-needs-doc-sync:
  implementation and tests are acceptable, but draft is missing current structure/snapshot/test trace.

implemented-needs-code-sync:
  source/draft target behavior is current, but implementation differs.

implemented-needs-test-sync:
  code behavior is acceptable, but tests do not prove behavior items/outcomes.

implemented-stale-source:
  source behavior changed or source state is uncertain relative to draft/implementation.

implemented-stale-domain:
  domain model/capability changed or is uncertain relative to draft/implementation.

implemented-docs-ahead-of-code:
  draft describes target behavior not implemented yet.

deprecated/replaced:
  slice is no longer canonical.

blocked:
  required source/code/test evidence is missing or ambiguous.
```

## 7. Implementation Sync Status Block

Every implemented slice draft should include near the top:

```markdown
## 0.2 Implementation Sync Status

Implementation status:
  implemented-current / implemented-needs-doc-sync / implemented-needs-code-sync / implemented-needs-test-sync / implemented-stale-source / implemented-stale-domain / deprecated / blocked

Runtime implementation checked:
  yes / no

Implemented files checked:
  server:
  client:
  tests:

Checked against:
  scenario/source files:
  domain files:
  slice source mapping:
  current branch / commit:

Known drift:
  none / source / domain / code / tests / docs / blocked

Last sync note:
  ...
```

This block is defined here. There is no separate implemented sync status template file.

## 8. Sync Algorithm

```text
1. Identify slice and current draft path.
2. Find relevant slice-to-source mapping.
3. Compare source files and behavior items against the draft.
4. Compare domain docs against the draft.
5. Inspect implementation files.
6. Inspect tests.
7. Classify drift.
8. If source/domain drift exists, stop and report it.
9. If behavior target is current but draft is old, update draft structure only.
10. Add or update source/domain/slice coverage snapshot if needed.
11. Add or update Implementation Sync Status.
12. Add or update Behavior Coverage.
13. Add or update Behavior-to-Test Trace.
14. Do not change runtime code in a docs-only sync pass.
```

## 9. What To Compare

### Source vs draft

```text
Does draft reference current scenario/source files?
Does draft cover current behavior item chain?
Does draft use old behavior item IDs?
Does draft assume old UI scenario or route?
Does slice-scenario-flow-behavior-register.md point to different sources?
```

### Domain vs draft

```text
Does domain provide the behavior item?
Did domain capability move responsibility to application/API/client?
Does draft manually implement behavior now owned by domain?
Does draft assume a domain method/status that no longer exists?
Does the draft conflict with the current domain draft/decision direction?
```

### Code vs draft

```text
Are documented endpoints/routes present?
Are documented client files present?
Are documented feature/entity/widget boundaries true?
Are command/read responsibilities implemented as documented?
Are there old shared/api business wrappers that draft should mark transitional?
Does implementation preserve the responsibility boundaries described by the draft?
```

### Tests vs behavior

```text
Does the test plan trace behavior items/outcomes?
Do actual tests prove persisted/visible outcomes?
Are failed commands tested for no-mutation?
Are tests too implementation-coupled?
Are required assertions present in Behavior-to-Test Trace?
```

## 10. Drift Rules

Naming drift is not automatically implementation drift.

Not drift by itself:

```text
class/method/file names differ from draft names;
service is named differently;
controller method is named differently;
repository is implemented through a different equivalent persistence shape;
DTO name differs but contract shape is equivalent.
```

Real drift:

```text
controller owns lifecycle/turn rules;
validator checks domain ownership or exchange status;
application service mutates aggregate state without domain method;
client UI visibility is used as security boundary;
failed command partially creates or changes persisted state;
command returns read/details DTO when draft says 204 No Content;
binary upload appears inside reference-only command slice;
slice starts an exchange when it should only add a version;
tests do not prove the behavior outcomes the slice claims to implement.
```

## 11. Backend Implementation Checklist

Use this checklist only after identifying the relevant slice draft and current source/code/test files.

```text
[ ] Confirm endpoint/route is present or mark implementation drift.
[ ] Confirm success response matches draft.
[ ] Confirm auth/session/role boundary matches draft.
[ ] Confirm CSRF/unsafe request policy if command is unsafe.
[ ] Confirm controller does not own lifecycle/domain state rules.
[ ] Confirm DTO validator checks shape only.
[ ] Confirm aggregate/load path includes required state.
[ ] Confirm application/domain responsibilities match draft.
[ ] Confirm persistence is atomic where required.
[ ] Confirm failure paths do not partially mutate state.
[ ] Confirm actual tests prove DB/visible outcomes.
[ ] Confirm missing implementation verification is explicitly marked.
```

If implementation was not rechecked, write:

```text
Runtime implementation checked: no.
This pass did not perform runtime implementation verification.
```

## 12. Allowed Outcomes

After sync pass, produce one of:

```text
docs-only draft sync;
source/domain sync needed before draft update;
implementation fix needed;
test sync needed;
deprecate/replaced note needed;
blocked pending source/code/test evidence.
```

## 13. Important Stop Rule

If implementation differs from draft, do not automatically decide that code is wrong.

First decide:

```text
Is expected behavior still correct?
Did source behavior change?
Did domain capability change?
Is the draft stale?
Is implementation intentionally ahead of docs?
Are tests proving current behavior or only implementation mechanics?
```

Only then classify code/doc/test drift.
