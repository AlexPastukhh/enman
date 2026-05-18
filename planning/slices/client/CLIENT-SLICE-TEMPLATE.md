# Client Slice Template

Status: canonical template for new `.client.md` drafts

Copy this structure for new client sidecar drafts.

## Required Header

```markdown
# <SLICE-ID>.client — <Title>

Status:
Logical slice:
Parent server slice:
Host surface:
Actor:
Slice type:
Placement:
```

For client-only drafts, use `SINGLE-` prefix in the file name:

```text
SINGLE-CC-CLIENT-FORM-VALIDATION-001-deferred-validation.client.md
```

## Required Source Block

```markdown
## 0. Scenario Sources

Business scenario:
UI scenario:
Cross-cutting behavior:
Data source:
Behavior items:
Concern umbrella:
```

Rules:

```text
Business scenario may be empty for client-only cross-cutting behavior.
UI scenario may be empty if the slice has no visual behavior.
Cross-cutting behavior must be filled for shared/common behavior such as deferred validation.
Behavior items should identify the exact behavior chain covered by this slice.
Concern umbrella is used for paired server-client/security concerns.
```

## Required Sections

```markdown
## 1. Scope

## 2. Out of Scope

## 3. Related Slices / Owners

## 4. Visual UI / Scenario Flow

Write ordinary words first.

```text
User opens page
        ↓
System shows state
        ↓
User performs action
        ↓
System refreshes visible state
```

This section must be derived from UI scenario sources or cross-cutting behavior sources.

Add a scenario flow table when it clarifies responsibility.

## 5. Visual Layout / Screen Composition

Describe page/screen structure.

```text
[AppShell]
  Header
  main.pageContainer
    PageHeader
    ContentGrid
      MainContent
      ActionPanel
  Footer
```

Include:

```text
loading state
empty state
error/access state
success state
responsive notes
action placement
```

Do not put implementation details here. This section describes what the screen should look like and where the major blocks appear.

## 6. Visual Client Implementation Flow

Every layer block must describe:

```text
where it lives
what it owns
what it uses
what it does not own
```

Every `Uses` entry must include:

```text
name
from: source file path
needed to: one-line reason why this dependency is needed in this block
```

Only dependencies that render UI additionally include:

```text
visual: one-line visual/layout/CSS role of this block in the current composition
```

Do not add `visual` for hooks, API wrappers, query keys, helpers or pure model functions.

`visual` must describe only how the used UI block appears in the parent composition. It must not describe or override internal styling of the child component.

## 7. Styling / CSS Ownership

| Area | Owner | CSS file | Rule |
|---|---|---|---|
| Page layout | page | `pages/.../*.css` | container, spacing, grid placement only |
| Command form | feature | `features/.../*.css` | fields, buttons, validation/error feedback |
| Shared block | widget | `widgets/.../*.css` | reusable block layout |
| Business display | entity | `entities/.../*.css` | read-only display |
| Tokens/base | global | `styles/*.css` | tokens/reset/base/app shell only |

Checklist:

```text
[ ] no broad global selector
[ ] no hover layout shift
[ ] no border-width change on hover
[ ] no feature CSS changes app shell
[ ] no page CSS reaches into feature internals
[ ] no business-specific CSS in shared/ui
[ ] uses tokens for color/spacing/radius where possible
[ ] loading/empty/error states styled
[ ] manual visual checks listed
```

## 8. Client API / Server Contract

## 9. Validation / Feedback / Error UI

Must state:

```text
which fields validate after delay
which fields validate immediately
what submit does
where field-level errors appear
where global/form-level errors appear
how server errors combine with client errors
```

## 10. Accessibility / ARIA Contract

Include table:

| Component | Native semantic element | Accessible name source | Keyboard behavior | ARIA needed? | Test query |
|---|---|---|---|---|---|

## 11. Cross-Cutting Concerns

## 12. Questions / Decisions

## 13. Extension / Change Points

## 14. Behavior Coverage

List behavior items covered by this slice.

| Behavior item | Scenario/source meaning | Covered by this slice? | Notes |
|---|---|---|---|

## 15. Test / Verification Plan

Primary rule:

```text
Tests verify behavior items and visible scenario outcomes.
Implementation details are only setup/action/observation mechanisms.
```

Use:

```text
planning/slices/slice-test-plan-workflow.md
```

### Behavior-to-Test Trace

| Behavior item | Visible scenario outcome | Test layer | Implementation mechanism | Escape risk | Refactor risk | Planned/actual test |
|---|---|---|---|---|---|---|

Test buckets:

```text
component/page tests
API/model tests if client API wrappers/mapping are owned here
E2E planned coverage when the user flow needs browser proof
contract/generated checks when API shape changes
```

What not to test:

```text
React internal state shape
exact query key internals unless this slice owns that contract
CSS class names unless this slice owns a styling primitive contract
```

## 16. Suggested File Placement

## 17. Implementation Checklist

## 18. Next Step
```
