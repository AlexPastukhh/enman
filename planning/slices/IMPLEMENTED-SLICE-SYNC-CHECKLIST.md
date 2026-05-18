# Implemented Slice Sync Checklist

Status: checklist for syncing implemented slice drafts

## 1. Source preflight

```text
[ ] SCENARIO-SOURCE-REGISTRY.md checked
[ ] relevant source versions identified
[ ] SCENARIO-DEPENDENCY-MAP.md checked if source changed
[ ] behavior items checked
[ ] data/UI scenario checked where relevant
```

## 2. Domain preflight

```text
[ ] DOMAIN-BASELINE.md checked
[ ] DOMAIN-CAPABILITY-MAP.md checked
[ ] domain disposition for behavior items identified
[ ] domain drift classified
```

## 3. Slice derivation

```text
[ ] SLICE-DERIVATION-MAP.md checked
[ ] slice row found or created
[ ] source versions match or drift reported
[ ] domain baseline matches or drift reported
[ ] draft status updated
```

## 4. Implementation inspection

```text
[ ] server files inspected if server slice exists
[ ] client files inspected if client slice exists
[ ] routes/endpoints checked
[ ] generated API/OpenAPI impact checked if relevant
[ ] actual file ownership checked against draft ownership
```

## 5. Test inspection

```text
[ ] actual tests found
[ ] tests mapped to behavior items
[ ] no-mutation tests checked for failed command behavior
[ ] escape/refactor risk considered
[ ] missing tests listed
```

## 6. Draft update

```text
[ ] Source / Domain / Slice Coverage Snapshot added
[ ] Implementation Sync Status added
[ ] Behavior Coverage updated
[ ] Behavior-to-Test Trace updated
[ ] stale implementation assumptions removed or marked transitional
[ ] out-of-scope updated
```

## 7. Stop conditions

Stop and do source-sync first if:

```text
[ ] source version drift detected
[ ] domain baseline drift detected
[ ] behavior item meaning changed
[ ] expected behavior changed
```
