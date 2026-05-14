# Manifest — L1 Domain Testing Rules Update

Archive: `enman-l1-domain-testing-rules-v1.zip`

## Files

```text
planning/l1-domain-testing-rules.md
planning/README.md
planning/planning-workflow-current.md
planning/tables/README.md
planning/l1-domain-implementation-cut.md
APPLY.md
MANIFEST.md
```

## Add

```text
planning/l1-domain-testing-rules.md
```

## Replace

```text
planning/README.md
planning/planning-workflow-current.md
planning/tables/README.md
planning/l1-domain-implementation-cut.md
```

## Delete

```text
nothing
```

## Purpose

Add a dedicated L1 domain testing rules artifact and update navigation so implementation agents know:

```text
- unit tests are first;
- integration tests come later;
- existing Tests.EnergyManagement is the valid local style baseline;
- xUnit + FluentAssertions are the current unit test baseline;
- no-write behavior must be tested;
- persistence/API/UI tests are out of the first domain unit-test cut.
```
