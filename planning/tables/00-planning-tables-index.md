# Planning Tables Index

Status: current table index for gradual domain discovery

## 1. Current Active Files

| Order | File | Status | Purpose |
|---:|---|---|---|
| 1 | `pre-domain-variants-input.md` | current pre-domain coverage baseline | Collect scenario-derived behavior items with stable IDs. |
| 2 | `domain-drafts/README.md` | current draft folder index | Explains where iterative domain drafts are stored. |

## 2. Current Read Order

```text
1. planning/diagrams/scenario-text-specs/
2. planning/diagrams/scenario-data/
3. planning/diagrams/scenario-text-specs/scenario-server-domain-validation-addendum.md
4. planning/tables/pre-domain-variants-input.md
5. planning/domain-draft-generation-guide.md
6. planning/tables/domain-drafts/README.md
```

## 3. Current Next Step

```text
Create / refine planning/tables/domain-drafts/domain-draft-01.md
```

Then:

```text
coverage review
-> domain draft 2
-> coverage review
-> ...
-> final domain model candidate
```

## 4. Replacement File Generation

Manual replacement-package generation is documented in:

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

## 5. Why No More Intermediate Tables Now

Do not add more intermediate files such as:

```text
domain-discovery-core.md
aggregate-boundary-candidates-core.md
domain-model-options-core.md
scenario-domain-responsibility-core.md
scenario-domain-design-input-core.md
scenario-domain-design-input-gate.md
```

The current pre-domain control file is:

```text
pre-domain-variants-input.md
```

The current draft folder is:

```text
domain-drafts/
```

## 6. Superseded / Historical

If these files exist historically, treat them as stale/superseded notes, not active workflow:

| File / folder | Current replacement |
|---|---|
| `scenario-domain-design-input-gate.md` | `pre-domain-variants-input.md` + current workflow docs |
| `scenario-domain-design-input-core.md` | `pre-domain-variants-input.md` |
| `domain-discovery-core.md` | iterative domain drafts |
| `aggregate-boundary-candidates-core.md` | domain drafts + coverage review |
| `domain-model-options-core.md` | domain drafts + coverage review |
| `scenario-responsibility-core.md` | `pre-domain-variants-input.md` |
| `scenario-domain-responsibility-core.md` | `pre-domain-variants-input.md` |
| `planning/domain-model-variant-generation-guide.md` | `planning/domain-draft-generation-guide.md` |
| `planning/tables/domain-variants/` | `planning/tables/domain-drafts/` |
