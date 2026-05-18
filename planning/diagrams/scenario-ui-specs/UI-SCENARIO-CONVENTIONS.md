# UI Scenario Conventions

Status: current UI scenario authoring convention

## 1. Purpose

UI scenario docs are the source for visible user requirements.

They answer:

```text
what the user sees
where the user enters the screen
which blocks appear
which data is visible
which actions are available
which states exist
which feedback is visible
which actor differences matter
```

They do not answer:

```text
which React component owns the screen
which query hook is used
which CSS file is changed
which API wrapper is called
which query key is invalidated
```

Those belong to client slice drafts.

## 2. Canonical chain

```text
scenario text / data / behavior / UI scenario
        ↓
client slice draft
        ↓
implementation archive
```

Do not update a serious client UI slice draft without checking its scenario UI source.

## 3. UI scenario vs client slice draft

| Concern | UI scenario source | Client slice draft |
|---|---|---|
| User goal | yes | references/implements |
| Screen composition | yes | translates into page/widget composition |
| Visible data | yes | maps to read model/components |
| Actions from user's view | yes | maps to features/action slots |
| Loading/empty/error states | yes | implements and tests |
| CSS ownership | no | yes |
| React hooks/components | no | yes |
| API wrappers/query keys | no | yes |
| Implementation checklist | no | yes |

## 4. Status values

Use one of:

```text
current
current / needs normalization
partial
stub
future
deprecated / replaced
missing
```

## 5. Completeness levels

```text
complete:
  enough to update a client slice draft without inventing UI requirements locally.

mostly complete:
  usable source, but should be normalized to template before large UI refactor.

partial:
  useful decisions exist, but missing important sections.

stub:
  short note only; not enough for implementation.

missing:
  must be created before drafting/refactoring the related client slice.

deprecated / replaced:
  not a current UI source; points to canonical replacement.
```

## 6. Template

Use:

```text
planning/diagrams/scenario-ui-specs/UI-SCENARIO-TEMPLATE.md
```

## 7. No implementation leakage

Avoid implementation-specific language such as:

```text
React Query
query key
mutation hook
CSS module file name
component path
fetchJson
cache invalidation
```

unless it is needed only as an explicit non-goal or to clarify that it belongs to the client slice draft.
