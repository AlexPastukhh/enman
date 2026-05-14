# Planning Tables Index

Status: current planning tables navigation

## 1. Current stage

Scenario text specs and DATA files are ready.

Validation-related files are kept separately.

The current coverage baselines are:

```text
planning/tables/pre-domain-variants-input.md
planning/tables/scenario-behavior-baseline-account-activation-addendum.md
```

The current saved domain draft is:

```text
planning/tables/domain-drafts/domain-draft-01.md
```

The current L1 implementation planning entry points are:

```text
planning/l1-domain-implementation-cut.md
planning/l1-domain-testing-rules.md
```

## 2. Current read order

```text
1. planning/diagrams/scenario-text-specs/
2. planning/diagrams/scenario-data/
3. planning/diagrams/scenario-text-specs/scenario-server-domain-validation-addendum.md
4. planning/diagrams/scenario-text-specs/scenario-account-activation-security-addendum.md
5. planning/tables/pre-domain-variants-input.md
6. planning/tables/scenario-behavior-baseline-account-activation-addendum.md
7. planning/domain-draft-generation-guide.md
8. planning/tables/domain-drafts/README.md
9. planning/tables/domain-drafts/domain-draft-01.md
10. planning/l1-domain-implementation-cut.md
11. planning/l1-domain-testing-rules.md
```

## 3. Current domain draft output folder

Use:

```text
planning/tables/domain-drafts/
```

Current saved draft:

```text
planning/tables/domain-drafts/domain-draft-01.md
```

Future drafts:

```text
domain-draft-02.md
domain-draft-03.md
...
final-domain-model-candidate.md
```

## 4. Implementation readiness

After a draft exists, the current step is not “create draft 1”.

The current step is:

```text
Review / refine domain-draft-01.md for L1 implementation readiness.
Use planning/l1-domain-implementation-cut.md before asking an implementation agent to code.
Use planning/l1-domain-testing-rules.md before writing tests.
```

Implementation agents should be given a narrow L1 cut.

Do not ask implementation agents to implement the whole draft in one step.

## 5. Testing rules

The current testing guide is:

```text
planning/l1-domain-testing-rules.md
```

Current testing position:

```text
- unit tests first;
- integration tests after first green domain implementation;
- existing Tests.EnergyManagement is the valid local test style reference;
- xUnit + FluentAssertions are the current unit-test baseline;
- Moq and WebApplicationFactory are available for later boundary/integration tests.
```

## 6. Replacement file generation

Manual replacement-file generation is documented in:

```text
planning/replacement-file-generation-guide.md
```

## 7. Current next step

```text
Review / refine domain-draft-01.md.
Then start the first L1 domain implementation cut from:
- planning/l1-domain-implementation-cut.md
- planning/l1-domain-testing-rules.md
```

## 8. Avoid

Do not add or use old intermediate files such as:

```text
domain-discovery-core.md
aggregate-boundary-candidates-core.md
domain-model-options-core.md
scenario-domain-responsibility-core.md
scenario-domain-design-input-core.md
scenario-domain-design-input-gate.md
```
