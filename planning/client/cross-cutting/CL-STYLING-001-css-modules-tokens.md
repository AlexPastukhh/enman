# CL-STYLING-001 — CSS Modules And Tokens

Status: current client-wide styling convention  
Type: client cross-cutting convention

## 1. Purpose

Define the default styling architecture for client implementation.

Current default:

```text
plain CSS + CSS Modules + CSS variables/tokens
```

Do not introduce Tailwind, CSS-in-JS or UI framework unless a separate decision is made.

## 2. Styling Ownership

```text
Component owns internal styling.
Parent/page owns layout placement.
```

Examples:

```text
Button owns visual variants and inner spacing.
Toolbar/page owns margin, layout placement and width.

RequestStatusBadge owns status color variant.
Request details page owns where the badge is placed.
```

## 3. File Ownership

```text
app/styles/
  global.css
  tokens.css

shared/ui/
  domain-agnostic primitives and styles

entities/*/components/
  reusable business display component styles

pages/*/
  page/read-context layout styles

features/*/
  command/action/form styles

widgets/*/
  large reusable block styles only if widget appears
```

## 4. Styling Change Points

A `.client.md` should list styling change points when styling decisions affect future changes.

| Aspect | Change through | Owner | Affected components | Tests/visual checks |
|---|---|---|---|---|
| Primary color | CSS variable | app/styles/tokens.css | buttons, links | visual check |
| Request status colors | status variant | entities/request | RequestStatusBadge | component test |
| Dashboard spacing | page CSS module | page | EmployeeRequestsPage | visual check |

## 5. Rules

```text
- Prefer CSS variables for project-wide tokens.
- Prefer CSS Modules for component/page scoped styles.
- Do not encode business meaning only through color.
- Keep page layout styles out of shared primitives.
- Keep domain-specific styles out of shared primitives.
```
