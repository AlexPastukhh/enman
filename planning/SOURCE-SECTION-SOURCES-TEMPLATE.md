# Source Section Sources Template

Status: active Enman section-source template
Doc version: v0.2.0
Scope: local fenced `Sources:` block format for active planning/domain/slice draft sections

## 1. Template

```text
Sources:
  Format/process:
    - planning/source-cascade-sync-workflow.md @ Doc version: v0.3.0
  Content:
    - planning/source-usage-cascade-profile.md @ Doc version: v0.1.0
  Internal dependencies:
    - none
  Not checked:
    - downstream domain/slice template examples beyond the declared field shape
```

Place this block immediately after a section heading.

````markdown
## <Section Heading>

```text
Sources:
  Format/process:
    - <workflow/template/principles/responsibility-map file> @ <Doc version/status>
  Content:
    - <scenario/domain/slice/testing/API source file or section> @ <Doc version/status>
  Internal dependencies:
    - <section in this same draft>
  Not checked:
    - <explicitly unchecked source/evidence>
```

<section body>
````

## 2. Field Meaning

```text
Sources:
  Format/process:
    - planning/source-cascade-sync-workflow.md @ Doc version: v0.3.0
  Content:
    - planning/source-usage-cascade-profile.md @ Doc version: v0.1.0
  Internal dependencies:
    - Template
  Not checked:
    - downstream domain/slice template examples beyond the declared field shape
```

```text
Format/process:
  Files that define how the section should be shaped, reviewed or owned.
  Usually workflow/template/principles/responsibility-map files.

Content:
  Files or sections that provide the business, scenario, domain, slice, testing or implementation meaning used by this section.

Internal dependencies:
  Sections in the same draft that the current section depends on.

Not checked:
  Sources/evidence that should matter but were not reviewed in this pass.
```

## 3. Rules

```text
Sources:
  Format/process:
    - planning/source-cascade-sync-workflow.md @ Doc version: v0.3.0
  Content:
    - planning/source-usage-cascade-profile.md @ Doc version: v0.1.0
  Internal dependencies:
    - Template
    - Field Meaning
  Not checked:
    - downstream domain/slice template examples beyond the declared field shape
```

```text
- Use repo-relative paths.
- Use section names or anchors when the whole file is too broad.
- Do not list sources that were not actually checked.
- If a source should be checked but was not, list it under Not checked.
- Keep the block local to the section.
- Do not copy the same full source universe into every section.
- Separate format/process sources from content sources.
- Every Format/process and Content source should include @ Doc version / @ historical / @ version not declared / @ implementation evidence / @ not checked status.
- Internal dependencies use section names; they do not need source version labels.
- Do not invent Doc version values for sources that do not declare or inherit one from an explicit version seed/register rule.
- Prefer scenario text #DATA for scenario-specific DATA.
- Use scenario-data sidecars only when they are reusable, shared, audited or transitional sources.
```

## 4. Domain Section Example

````markdown
## 9. Value Objects Used

```text
Sources:
  Format/process:
    - planning/domain/aggregate-drafting-workflow.md @ Doc version: v0.1.0
    - planning/domain/aggregate-draft-template.md @ Doc version: v0.1.0
    - planning/domain/value-object-drafting-workflow.md @ Doc version: v0.2.0
    - planning/domain/value-object-draft-template.md @ Doc version: v0.2.0
  Content:
    - planning/domain/scenario-to-aggregate-map.md @ Doc version: v0.1.0
    - planning/domain/value-objects/agreement-proposal-version.md @ Doc version: v0.1.0
    - planning/domain/value-objects/agreement-proposal-author.md @ Doc version: v0.1.0
    - planning/domain/value-objects/agreement-document-ref.md @ Doc version: v0.1.0
    - planning/domain/value-objects/proposal-comment.md @ Doc version: v0.1.0
    - planning/domain/value-objects/final-refusal-reason.md @ Doc version: v0.1.0
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
    - planning/slices/slice-test-plan-workflow.md @ <Doc version/status>
    - planning/testing/server-slice-test-plan-rules.md @ <Doc version/status>
  Content:
    - planning/diagrams/scenario-behavior-items/<scenario-behavior-items>.md @ <Doc version/status>
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

## 6. Source Delta / Change Log

```text
Sources:
  Format/process:
    - planning/source-cascade-sync-workflow.md @ Doc version: v0.3.0
  Content:
    - planning/source-usage-cascade-profile.md @ Doc version: v0.1.0
  Internal dependencies:
    - Template
    - Field Meaning
    - Rules
    - Domain Section Example
    - Slice Section Example
  Not checked:
    - downstream domain/slice template examples beyond the declared field shape
```

```text
- SRC-LOCAL-RULE-1 updated this template to require version/status labels and prefer scenario text #DATA for scenario-specific DATA.
- ROOT-SRC-1 added local section-level Sources blocks to this template without changing the template shape or removing domain/slice examples.
```
