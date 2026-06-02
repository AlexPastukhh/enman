# Value Object Drafting Workflow

Status: current value object drafting workflow  
Doc version: v0.2.0  
Scope: create/update value object drafts from VI behavior items, aggregate usage and local section-level source coverage

## 1. Purpose

Use this workflow when a value object candidate has source-backed invariants, repeated usage or non-trivial validation/normalization rules.

This workflow creates or updates one value object draft and keeps aggregate references synchronized.

Value object drafts are structured domain files. When a value object draft participates in source/version/cascade work, add local section-level `Sources:` blocks for sections that rely on real external sources or meaning-bearing internal dependencies.

## 2. Required Reads

```text
planning/domain/README.md
planning/domain/domain-responsibility-map.md
planning/domain/value-object-draft-template.md
planning/source-cascade-sync-workflow.md
planning/SOURCE-SECTION-SOURCES-TEMPLATE.md
planning/domain/scenario-to-aggregate-map.md
planning/domain/domain-source-sync-register.md when downstream aggregate/slice sync impact is involved
related aggregate drafts
related VI behavior items
related DATA fields
old domain drafts if relevant
```

## 3. Workflow Steps

```text
1. Identify value object candidate.
2. Confirm it is non-trivial, reused or source-backed.
3. Read related VI behavior items and DATA fields.
4. Identify aggregates/entities that use it.
5. Identify the section-level source set for Purpose, Source Inputs, Used By, Shape, Invariants, Creation, Equality, Validation, Persistence, Invalid Examples and Questions.
6. Define shape/fields.
7. Define invariants.
8. Define creation/normalization rules.
9. Define equality rule.
10. Define validation boundary.
11. Define persistence/serialization notes.
12. Add invalid examples.
13. Add or update local section-level Sources blocks immediately after each stable section heading whose content depends on external sources or internal dependencies.
14. Replace generic "Full source/version/cascade metadata not checked" wording with explicit Not checked items in the relevant local Sources blocks.
15. Update aggregate drafts to reference this value object when relevant.
16. Update scenario-to-aggregate-map.md when candidate status changes.
17. Update domain-source-sync-register only inside the register's declared aggregate/value-object coverage state.
```

## 4. Output

Expected output:

```text
one value object draft file with Doc version and local section-level Sources blocks where needed;
updated aggregate references if needed;
updated scenario-to-aggregate map if needed;
updated domain source-sync register rows only when scope/state allows;
questions/decisions when ownership is unclear.
```

## 5. Guardrails

```text
Do not create files for every primitive wrapper.
Do not put aggregate lifecycle rules into value objects.
Do not duplicate DTO validation rules unless they protect domain value integrity.
Do not make persistence shape the only reason for a value object.
Do not hide value object usage only in aggregate prose if it has a separate file.
Do not leave source/version/cascade coverage as a generic unchecked note after a source coverage pass; list exact unchecked sources instead.
Do not claim full domain source coverage from a value-object pass that covers only selected files.
```
