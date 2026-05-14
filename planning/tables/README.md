# Planning Tables Index

Status: current planning tables navigation

## 1. Current stage

Scenario text specs and DATA files are ready.

Validation-related file is kept separately.

Before generating domain drafts, use:

```text
planning/tables/pre-domain-variants-input.md
```

Historical filename note:

```text
pre-domain-variants-input.md now acts as the Scenario Behavior Coverage Baseline.
```

## 2. Current read order

```text
1. planning/diagrams/scenario-text-specs/
2. planning/diagrams/scenario-data/
3. planning/diagrams/scenario-text-specs/scenario-server-domain-validation-addendum.md
4. planning/tables/pre-domain-variants-input.md
5. planning/domain-draft-generation-guide.md
6. planning/tables/domain-drafts/README.md
```

## 3. Why pre-domain-variants-input.md exists

It collects scenario-derived behavior items with stable IDs before domain drafts are created.

It includes:

```text
- command behavior cards;
- scenario state/condition matrices;
- impossible business state candidates;
- value integrity / anti-primitive-obsession items;
- use-case coordination items;
- read/access/integration/future items.
```

It intentionally does not define:

```text
- domain classes;
- aggregate boundaries;
- final method names;
- final persistence schema;
- final layer placement.
```

Each domain draft uses the stable item IDs to show coverage.

## 4. Current domain draft output folder

Use:

```text
planning/tables/domain-drafts/
```

Expected first draft path:

```text
planning/tables/domain-drafts/domain-draft-01.md
```

## 5. Replacement file generation

Manual replacement-file generation is documented in:

```text
planning/replacement-file-generation-guide.md
```

When the user asks for replacement files:

```text
- generate complete files;
- keep repository-relative paths;
- package them in a zip;
- include previous unapplied archive content if the user says it was not applied.
```

## 6. Current next step

```text
Create / refine domain draft 1.
```

## 7. Avoid

Do not add or use old intermediate files such as:

```text
domain-discovery-core.md
aggregate-boundary-candidates-core.md
domain-model-options-core.md
scenario-domain-responsibility-core.md
scenario-domain-design-input-core.md
scenario-domain-design-input-gate.md
```

Do not use the older competing-variant workflow:

```text
planning/domain-model-variant-generation-guide.md
planning/tables/domain-variants/
```

If those files exist historically, treat them as stale/superseded notes or delete/archive them.
