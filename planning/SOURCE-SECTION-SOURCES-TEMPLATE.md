# Source Section Sources Template

Status: active Enman section-source template  
Doc version: v0.1.0  
Scope: local fenced `Sources:` block format for active planning/domain/slice draft sections

## 1. Template

Place this block immediately after a section heading.

````markdown
## <Section Heading>

```text
Sources:
  Format/process:
    - <workflow/template/principles/responsibility-map file>
  Content:
    - <scenario/domain/slice/testing/API source file or section>
  Internal dependencies:
    - <section in this same draft>
  Not checked:
    - <explicitly unchecked source/evidence>
```

<section body>
````

## 2. Field Meaning

```text
Format/process:
  Files that define how the section should be shaped, reviewed or owned.
  Usually workflow/template/principles/responsibility-map files.

Content:
  Files or sections that provide the business, scenario, domain, slice, testing or implementation meaning used by this section.

Internal dependencies:
  Sections in the same draft that this section depends on.

Not checked:
  Sources/evidence that should matter but were not reviewed in this pass.
```

## 3. Rules

```text
- Use repo-relative paths.
- Use section names or anchors when the whole file is too broad.
- Do not list sources that were not actually checked.
- If a source should be checked but was not, list it under Not checked.
- Keep the block local to the section.
- Do not copy the same full source universe into every section.
- Separate format/process sources from content sources.
- Prefer scenario text #DATA for scenario-specific DATA.
- Use scenario-data sidecars only when they are reusable, shared, audited or transitional sources.
```

## 4. Domain Section Example

````markdown
## 9. Value Objects Used

```text
Sources:
  Format/process:
    - planning/domain/aggregate-drafting-workflow.md
    - planning/domain/aggregate-draft-template.md
    - planning/domain/value-object-drafting-workflow.md
    - planning/domain/value-object-draft-template.md
  Content:
    - planning/domain/scenario-to-aggregate-map.md
    - planning/domain/value-objects/agreement-proposal-version.md
    - planning/domain/value-objects/agreement-proposal-author.md
    - planning/domain/value-objects/agreement-document-ref.md
    - planning/domain/value-objects/proposal-comment.md
    - planning/domain/value-objects/final-refusal-reason.md
  Internal dependencies:
    - Owned State
    - Domain Methods / Commands
  Not checked:
    - current runtime implementation unless explicitly read
```

<section body>
````

## 5. Slice Section Example

````markdown
## Test / Verification Plan

```text
Sources:
  Format/process:
    - planning/slices/slice-test-plan-workflow.md
    - planning/testing/server-slice-test-plan-rules.md
  Content:
    - planning/diagrams/scenario-behavior-items/<scenario-behavior-items>.md
    - Behavior Coverage
    - Domain Methods / Domain Behavior Contract
  Internal dependencies:
    - Behavior Coverage
    - Implementation Flow
  Not checked:
    - current test files unless explicitly read
```

<section body>
````
