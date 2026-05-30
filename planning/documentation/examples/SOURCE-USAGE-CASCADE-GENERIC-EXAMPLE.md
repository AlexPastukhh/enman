# Source Usage Cascade Generic Example

Status: active reusable example  
Scope: generic source/consumer/cascade example for the source usage cascade field kit

## 1. Purpose

This example demonstrates the generic source usage relationship model.

Kit owner:

```text
planning/documentation/field-kits/source-usage-cascade-field-kit.md
```

It is not Enman-specific.

## 2. Generic Source Relationship

A source artifact changes:

```text
source artifact: product requirement section A
source scope: acceptance rule R-12
```

A downstream consumer uses it:

```text
consumer artifact: testing plan section B
consumer scope: test case group T-4
```

## 3. Source Usage Row

| source_id | source_scope | source_status | consumer_id | consumer_scope | reviewed_against | sync_status | review_outcome | last_reviewed |
|---|---|---|---|---|---|---|---|---|
| product-requirements.md | section A / R-12 | current | testing-plan.md | section B / T-4 | req-v3 | needs-review | follow-up | 2026-05-30 |

## 4. Cascade Review Outcome

After review:

```text
The test plan still covers the rule, but metadata must be updated to reviewed_against=req-v4.
No content change needed.
sync_status=metadata-only-reviewed.
```

## 5. Key Lesson

The source usage row prevents broad re-review while still making downstream synchronization visible.
