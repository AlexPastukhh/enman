# Value Object Drafting Workflow

Status: current value object drafting workflow  
Doc version: v0.1.0  
Scope: create/update value object drafts from VI behavior items and aggregate usage

## 1. Purpose

Use this workflow when a value object candidate has source-backed invariants, repeated usage or non-trivial validation/normalization rules.

This workflow creates or updates one value object draft and keeps aggregate references synchronized.

## 2. Required Reads

```text
planning/domain/README.md
planning/domain/domain-responsibility-map.md
planning/domain/value-object-draft-template.md
planning/domain/scenario-to-aggregate-map.md
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
5. Define shape/fields.
6. Define invariants.
7. Define creation/normalization rules.
8. Define equality rule.
9. Define validation boundary.
10. Define persistence/serialization notes.
11. Add invalid examples.
12. Update aggregate drafts to reference this value object when relevant.
13. Update scenario-to-aggregate-map.md when candidate status changes.
```

## 4. Output

Expected output:

```text
one value object draft file;
updated aggregate references if needed;
updated scenario-to-aggregate map if needed;
questions/decisions when ownership is unclear.
```

## 5. Guardrails

```text
Do not create files for every primitive wrapper.
Do not put aggregate lifecycle rules into value objects.
Do not duplicate DTO validation rules unless they protect domain value integrity.
Do not make persistence shape the only reason for a value object.
Do not hide value object usage only in aggregate prose if it has a separate file.
```
