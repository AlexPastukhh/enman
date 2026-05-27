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
core business scenario + DATA + business behavior items
        ↓
UI scenario as presentation / UX projection
        ↓
client slice draft / UI tests / E2E / a11y
        ↓
implementation archive
```

Do not update a serious client UI slice draft without checking its scenario UI source.

UI scenario must stay consistent with core scenario and DATA. It must not silently create or change business behavior.

## 3. UI scenario vs core scenario / DATA / client slice draft

| Concern | Core scenario | DATA | Behavior items | UI scenario source | Client slice draft |
|---|---|---|---|---|---|
| Business capability | yes | references | processed | presents only | implements |
| Scenario information | references | yes | references | presents | maps to read/forms |
| User goal | yes | references | references | yes, from UI point of view | implements |
| Visible data | no/light | yes | references | presentation/UX | maps to components |
| Actions from user's view | business action | references | behavior unit | visible action/availability | feature/action slot |
| Loading/empty/error states | no | sometimes feedback data | projection note / UI-only entry | yes | implements and tests |
| Deferred validation / feedback | business/validation boundary | feedback information | projection note | yes | implements |
| CSS ownership | no | no | no | no | yes |
| React hooks/components | no | no | no | no | yes |
| API wrappers/query keys | no | no | no | no | yes |
| Implementation checklist | no | no | no | no | yes |

If a UI scenario appears to add business meaning, record a consistency issue or accepted decision instead of treating UI wording as a new core requirement.

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
