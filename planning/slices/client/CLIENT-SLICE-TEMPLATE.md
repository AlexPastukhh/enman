# Client Slice Template

Status: canonical template for new `.client.md` drafts  
Scope: copyable structure for client sidecar drafts using current slice authoring and test-trace principles

Copy this structure for new client sidecar drafts.

Use with:

```text
planning/slices/slice-draft-authoring-principles.md
planning/slices/client/CLIENT-SLICE-DRAFTING-WORKFLOW.md
planning/slices/slice-test-plan-workflow.md
```

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
Implementation status:
Runtime implementation checked: yes/no
```

For client-only drafts, use `SINGLE-` prefix in the file name:

```text
SINGLE-CC-CLIENT-FORM-VALIDATION-001-deferred-validation.client.md
```

## Required Source Block

```markdown
## 0. Scenario Sources / Source Sync

Business scenario:
UI scenario:
Cross-cutting behavior:
Data source:
Behavior items:
Concern umbrella:
Slice source mapping:
Source status:
```

Rules:

```text
Scope comes from scenario/source mapping, not from a random component/page list.
Business scenario may be empty for client-only cross-cutting behavior.
UI scenario may be empty if the slice has no visual behavior.
Cross-cutting behavior must be filled for shared/common behavior such as deferred validation.
Behavior items should identify the exact behavior chain covered by this slice.
Concern umbrella is used for paired server-client/security concerns.
Source status should say current / provisional / pending source sync / blocked when relevant.
```

## Required Sections

```markdown
## 1. Scope

State the verifiable client-visible behavior subset this slice implements.

Bad:

```text
Make the page work.
```

Good:

```text
Show the request details action area for an Employee, submit Start Review from the details page,
show pending/success/error feedback, and refresh visible details state after success.
```

## 2. Scenario Scope / Client Boundary

### 2.1 Scenario / UI Identity

Scenario:
UI scenario:
Scenario purpose:
Source status:

### 2.2 This Client Slice Scope Inside The Scenario

This client slice implements:

```text
-
```

### 2.3 Scenario Behavior Not Implemented By This Client Slice

| Scenario/source behavior not in this client slice | Owner / destination | Reason |
|---|---|---|

## 3. Related Slices / Owners

List concrete related slices/owners only when known.

```text
Parent server slice:
Peer client sidecar:
Cross-cutting concern:
Future follow-up:
```

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

Implementation drift means wrong responsibility or behavior, not harmless naming differences.

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

State:

```text
API wrapper owner:
Generated OpenAPI types used:
Generated constants/error codes used:
Request/response DTO mapping:
ProblemDetails / error mapping:
Query invalidation/refetch behavior:
```

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

Client validation and visibility do not replace server/domain security.

## 10. Accessibility / ARIA Contract

Include table:

| Component | Native semantic element | Accessible name source | Keyboard behavior | ARIA needed? | Test query |
|---|---|---|---|---|---|

## 11. Cross-Cutting Concerns

| Concern | Applies? | Rule for this slice | Owner/source |
|---|---:|---|---|
| Auth/session UI |  |  |  |
| Client feedback / error visibility |  |  |  |
| Accessibility |  |  |  |
| CSRF / unsafe command transport |  |  |  |
| ProblemDetails mapping |  |  |  |
| OpenAPI/generated artifacts |  |  |  |

Security rule:

```text
UI visibility is UX only. Security remains server/application/domain responsibility.
```

## 12. Questions / Decisions

| ID | Question / Decision | Status | Current direction | Impact |
|---|---|---|---|---|

## 13. Extension / Change Points

| Possible future change | Type | What to account for now |
|---|---|---|

Do not turn possible future UI variants into current scope unless they are accepted.

## 14. Behavior Coverage

Behavior Coverage is not Scope and not Test Plan.

| Behavior | Status | Covered by / delegated to | Notes |
|---|---|---|---|

Status examples:

```text
covered
covered boundary
out of scope
future
delegated
blocked
```

## 15. Test / Verification Plan

Primary rule:

```text
Tests verify behavior items and visible scenario outcomes.
Implementation details are only setup/action/observation mechanisms.
Required assertions live inside Behavior-to-Test Trace.
```

Use:

```text
planning/slices/slice-test-plan-workflow.md
```

### Behavior-to-Test Trace

| Behavior | Visible scenario outcome | Test layer | Setup/action mechanism | Required assertions | Escape risk | Refactor risk | Planned/actual test |
|---|---|---|---|---|---|---|---|

Test buckets:

```text
component/page tests
API/model tests if client API wrappers/mapping are owned here
E2E planned coverage when the user flow needs browser proof
contract/generated checks when API shape changes
```

What not to test here:

```text
server/domain authorization as primary proof
React internal state shape
exact query key internals unless this slice owns that contract
CSS class names unless this slice owns a styling primitive contract
full E2E matrix when component/API tests cover branches
```

## 16. Suggested File Placement

```text
page:
widget:
feature:
entity:
shared:
styles:
tests:
```

## 17. Implementation Checklist

```text
[ ] Confirm source/UI behavior mapping.
[ ] Confirm out-of-scope behavior is explicit.
[ ] Confirm API wrapper and generated types placement.
[ ] Confirm validation/feedback/error UI behavior.
[ ] Confirm accessibility contract.
[ ] Confirm CSS ownership boundaries.
[ ] Confirm UI visibility is not treated as security.
[ ] Confirm Behavior Coverage is separate from Test Plan.
[ ] Confirm required assertions are inside Behavior-to-Test Trace.
```

## 18. Guardrail Summary / Next Step

```text
- Do not broaden scope silently.
- Do not implement server/domain behavior in a client sidecar.
- Do not use UI visibility as security.
- Do not treat harmless naming differences as drift.
- Do not leave required assertions outside Behavior-to-Test Trace.
```
```
