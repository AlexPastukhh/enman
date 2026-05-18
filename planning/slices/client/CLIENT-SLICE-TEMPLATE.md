# Client Slice Template

Status: canonical template for new `.client.md` drafts

Copy this structure for new client sidecar drafts.

## Required Header

```markdown
# <SLICE-ID>.client — <Title>

Status:
Parent server slice:
Host surface:
Actor:
Slice type:
Placement:
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

Example:

```text
[Layer Name]
path/to/File.tsx

Lives here:
  ComponentOrHookName

Owns:
  route params;
  page title;
  page branch composition.

Uses:
  useSomething(id)
    from: entities/.../model/useSomething.ts
    needed to: load data for this route.

  SomeWidget
    from: widgets/.../SomeWidget.tsx
    needed to: render shared read-only content and expose action slot.
    visual: main content block placed below page header; parent controls spacing around it.

  SomeFeatureForm
    from: features/.../ui/SomeFeatureForm.tsx
    needed to: provide command form in the action area.
    visual: compact command block placed inside the page action panel.

Does not own:
  internal layout of SomeWidget;
  internal field styling of SomeFeatureForm;
  endpoint wrapper;
  server authorization/lifecycle rules.
```

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

## 15. Verification Plan

Separate component tests, API/model tests and E2E planned coverage.

## 16. Suggested File Placement

## 17. Implementation Checklist

## 18. Next Step
```
