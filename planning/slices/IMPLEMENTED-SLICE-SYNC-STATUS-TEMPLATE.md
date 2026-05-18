# Implemented Slice Sync Status Template

Status: template block for slice drafts that already have implementation

Copy this block near the top of an implemented slice draft.

```markdown
## 0.2 Implementation Sync Status

Implementation status:
  implemented-current / implemented-needs-doc-sync / implemented-needs-code-sync / implemented-needs-test-sync / implemented-stale-source / implemented-stale-domain / implemented-stale-source-and-domain / implemented-docs-ahead-of-code / deprecated

Implemented files:
  server:
    - ...
  client:
    - ...
  tests:
    - ...

Checked against:
  source versions:
    - ...
  domain baseline:
    - ...
  slice derivation map version:
    - ...

Known drift:
  source:
    none / ...
  domain:
    none / ...
  code:
    none / ...
  tests:
    none / ...
  docs:
    none / ...

Last sync note:
  ...
```

## Status selection rule

Use one primary status and then describe details under `Known drift`.

Do not mark `implemented-current` unless source, domain, draft, implementation and tests agree.
