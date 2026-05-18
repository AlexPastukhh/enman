# Archive Plan

## Slice pair

```text
SL-EMP-REQ-001 — Employee Request List Read
L2-EMP-DASH-001.client — Employee Request Dashboard
```

## Mode

```text
docs-only implemented-slice draft refactor;
implementation evidence inspected read-only;
runtime implementation not changed.
```

## Replacement strategy

```text
1. Preserve original server/client draft snapshots.
2. Replace server draft with current sync structure.
3. Replace client sidecar with current sync structure.
4. Record implementation evidence and known limitations.
5. Add Behavior-to-Test Trace using actual tests where known.
6. Verify no runtime files are included.
```
