# Home flow UI fix

## Purpose

Adds a real home page flow with role-aware action cards and fixes the top navigation so Client and Employee sessions have useful routes from the app shell.

## Files

- `energymanagement.client/vite.config.ts`
  - Keeps Vite on IPv4-first localhost.
- `energymanagement.client/src/main.tsx`
  - Imports `general.css` and `layout.css` so global layout/buttons/header styles are applied.
- `energymanagement.client/src/pages/home/HomePage.tsx`
  - Replaces empty home content with hero, quick action cards and app flow explanation.
- `energymanagement.client/src/pages/home/homePage.css`
  - Styles the new home page.
- `energymanagement.client/src/shared/ui/layout/Header.tsx`
  - Makes navigation role-aware.
  - Client sees create request, my requests, my agreements.
  - Employee sees request dashboard and agreement exchange dashboard.
- `energymanagement.client/src/shared/ui/layout/headerConst.ts`
  - Adds labels for the new navigation links.
- `energymanagement.client/src/styles/layout.css`
  - Adds missing `.header__nav-link` styles used by `HeaderNavLink`.

## Checks run

```powershell
npm --prefix ./energymanagement.client install
npm --prefix ./energymanagement.client run build
```

Build passed. Vite printed the existing chunk-size warning.
