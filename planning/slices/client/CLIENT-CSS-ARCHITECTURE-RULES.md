# Client CSS Architecture Rules

Status: current client CSS ownership convention

## 1. Default stack

```text
plain CSS + CSS Modules where scoped component styles are introduced + CSS variables/tokens
```

Do not introduce Tailwind, CSS-in-JS or UI framework unless a separate decision is made.

## 2. CSS is part of slice ownership

CSS changes are part of client slice implementation.

A client slice must state:

```text
which CSS files it changes
which layer owns each style
what visual states are covered
what global styles are touched
what visual checks are required
```

## 3. Ownership by layer

Global styles may own:

```text
tokens
reset
base typography
app shell
shared layout utilities
```

Page CSS owns:

```text
page container
page grid
section spacing
component placement
```

Widget CSS owns:

```text
reusable block layout
```

Entity CSS owns:

```text
read-only business display
```

Feature CSS owns:

```text
command form
action button area
pending/error/success feedback
field layout inside the feature form
```

Shared UI CSS owns:

```text
domain-agnostic primitives only
```

## 4. Parent/child boundary

Parent may describe and control:

```text
where child UI block is placed
spacing around child block
page/grid slot
```

Parent must not describe or override:

```text
child internal padding
child internal button styling
child internal field styling
child internal section layout
```

If child internals must change, change the child owner CSS.

## 5. Forbidden patterns

```text
- broad global selectors like ".someClass p, a"
- hover shrinking
- hover changing border-width
- feature CSS changing app shell
- page CSS reaching into feature internals
- business-specific styles in shared/ui
- visual meaning only through color
- one huge CSS file for a whole business area
```

## 6. Button rules

```text
hover must not change layout size
border-width must not change on hover
focus-visible must be visible
disabled state must be clear
danger actions must be visually distinct but not noisy
```

## 7. Form rules

```text
labels are always visible
field errors appear near fields
global/root errors appear in form error area
submit validates immediately
input validation may be deferred
autocomplete must be correct for auth fields
```

## 8. Slice CSS checklist

```text
[ ] CSS ownership stated in client draft
[ ] global CSS touched only for tokens/reset/base/app shell
[ ] no forbidden selectors/effects
[ ] loading/empty/error/success states styled
[ ] responsive notes included
[ ] manual visual routes listed
```

## 9. Visual lines in implementation flow

Only UI-rendering dependencies may have `visual` lines.

A `visual` line describes the block role in the parent layout, not child internals.

Good:

```text
visual: compact command form placed inside the details action panel.
```

Bad:

```text
visual: textarea has 12px padding, green border and button has 8px radius.
```
