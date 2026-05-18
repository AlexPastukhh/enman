# Implemented Slice Draft Sync Workflow

Status: current workflow for refactoring existing slice drafts that already have implementation

## 1. Purpose

This workflow is for old or legacy slice drafts that already have code and tests.

Such drafts are not ordinary draft-only planning docs. They must be synchronized against:

```text
scenario/source versions;
domain baseline and capability map;
slice derivation map;
actual implementation files;
actual tests.
```

Do not rewrite an implemented slice draft just to match the newest template without checking code and tests.

## 2. When to use

Use this workflow when:

```text
a slice draft already has implementation;
a legacy L1/L2 draft needs migration to current structure;
a draft looks stale compared to current source/domain/map;
implementation exists but draft lacks source/domain/slice coverage snapshot;
implementation exists but test trace is missing;
code behavior and documented behavior may differ.
```

## 3. Primary rule

```text
Implemented slice sync is not only document rewrite.

It is a sync pass:
  source/domain/map
    vs
  existing draft
    vs
  actual implementation
    vs
  actual tests.
```

If expected behavior changed, stop and use source-sync first.

## 4. Required preflight

Before updating the draft:

```text
1. Open SCENARIO-SOURCE-REGISTRY.md.
2. Open DOMAIN-BASELINE.md.
3. Open DOMAIN-CAPABILITY-MAP.md.
4. Open SLICE-DERIVATION-MAP.md.
5. Find the slice row.
6. Read existing draft.
7. Inspect current implementation files.
8. Inspect current tests.
9. Identify drift type.
```

If source/domain versions drift:

```text
STOP.
Output Source / Domain Drift Report.
Update registry/changelog/derivation map first.
```

## 5. Drift types

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
  source, domain, draft, code and tests agree.

implemented-needs-doc-sync:
  implementation and tests are acceptable, but draft is missing current structure/snapshot/test trace.

implemented-needs-code-sync:
  source/draft target behavior is current, but implementation differs.

implemented-needs-test-sync:
  code behavior is acceptable, but tests do not prove behavior items.

implemented-stale-source:
  source changed after draft/implementation baseline.

implemented-stale-domain:
  domain baseline/capability changed after draft/implementation baseline.

implemented-docs-ahead-of-code:
  draft describes target behavior not implemented yet.

deprecated/replaced:
  slice is no longer canonical.
```

## 6. Required draft block

Every implemented slice draft should include near the top:

```markdown
## 0.2 Implementation Sync Status

Implementation status:
  implemented-current / implemented-needs-doc-sync / implemented-needs-code-sync / implemented-needs-test-sync / implemented-stale-source / implemented-stale-domain / deprecated

Implemented files:
  server:
  client:
  tests:

Checked against:
  source versions:
  domain baseline:
  slice derivation map version:

Known drift:
  none / source / domain / code / tests / docs

Last sync note:
  ...
```

Use:

```text
planning/slices/IMPLEMENTED-SLICE-SYNC-STATUS-TEMPLATE.md
```

## 7. Sync algorithm

```text
1. Identify slice and current draft path.
2. Check SLICE-DERIVATION-MAP row.
3. Compare source versions.
4. Compare domain baseline.
5. Inspect implementation files.
6. Inspect tests.
7. Classify drift.
8. If source/domain drift exists, stop and use source-sync.
9. If behavior target is current but draft is old, update draft structure only.
10. Add Source / Domain / Slice Coverage Snapshot.
11. Add Implementation Sync Status.
12. Add or update Behavior Coverage.
13. Add or update Behavior-to-Test Trace.
14. Mark draft status in SLICE-DERIVATION-MAP.
15. Do not change runtime code in a docs-only sync archive.
```

## 8. What to compare

### Source vs draft

```text
Does draft reference current scenario/source versions?
Does draft cover current behavior item chain?
Does draft use old behavior item IDs?
Does draft assume old UI scenario or route?
```

### Domain vs draft

```text
Does domain provide the behavior item?
Did domain capability move responsibility to application/API/client?
Does draft manually implement behavior now owned by domain?
Does draft assume domain method/status that no longer exists?
```

### Code vs draft

```text
Are documented endpoints/routes present?
Are documented client files present?
Are documented feature/entity/widget boundaries true?
Are command/read responsibilities implemented as documented?
Are there old shared/api business wrappers that draft should mark transitional?
```

### Tests vs behavior

```text
Does test plan trace behavior items?
Do actual tests prove persisted/visible outcomes?
Are failed commands tested for no-mutation?
Are tests too implementation-coupled?
```

## 9. Allowed outcomes

After sync pass, produce one of:

```text
docs-only draft sync archive;
source-sync update needed before draft;
implementation fix draft/archive needed;
test sync needed;
deprecate/replaced note needed.
```

## 10. Important stop rule

If implementation differs from draft, do not automatically decide that code is wrong.

First decide:

```text
Is expected behavior still correct?
Did source behavior change?
Did domain capability change?
Is the draft stale?
Is implementation intentionally ahead of docs?
```

Only then classify code/doc/test drift.
