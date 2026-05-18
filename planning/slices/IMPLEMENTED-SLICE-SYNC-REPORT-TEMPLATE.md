# Implemented Slice Sync Report Template

Status: template for reporting implemented slice sync audit before changing a draft

```markdown
# Implemented Slice Sync Report

Slice:
Draft path:
Current implementation status:

## 1. Requested work

```text
...
```

## 2. Source/domain/map check

| Area | Draft/map expected | Current | Result |
|---|---|---|---|
| Source versions | ... | ... | match / drift |
| Domain baseline | ... | ... | match / drift |
| Slice derivation map | ... | ... | match / drift |

## 3. Implementation files found

Server:
- ...

Client:
- ...

Tests:
- ...

## 4. Behavior comparison

| Behavior item | Expected behavior | Implementation observed | Test proof | Drift |
|---|---|---|---|---|
| ... | ... | ... | ... | none / docs / code / tests / source / domain |

## 5. Drift classification

```text
implemented-current / implemented-needs-doc-sync / implemented-needs-code-sync / implemented-needs-test-sync / implemented-stale-source / implemented-stale-domain / implemented-docs-ahead-of-code / deprecated
```

## 6. Recommended next step

```text
docs-only draft sync
source-sync first
implementation fix
test sync
deprecate/replaced note
blocked / needs user decision
```
```
