# UI normalization mini-fix

## Changed

- normalized Header navigation and guest/session actions
- removed confusing AccountPage unauthenticated RegisterForm duplicate
- added signed-out account prompt with Login/Register actions
- added auth page intro shells for Login/Register
- translated auth constants to Russian
- fixed global anchor CSS selector that affected all links
- removed primary button shrink hover behavior
- stopped hollow button border width jump on hover
- improved header responsiveness and spacing

## Verified

```text
npm --prefix ./energymanagement.client run build
→ success
```
