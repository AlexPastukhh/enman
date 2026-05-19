# SL-AUTH-ACT-001.client — Email Activation UI And Protected Client Gate

Status: draft / ready for implementation planning  
Logical slice: `CC-AUTH-ACT-001`  
Parent server slices:

```text
SL-AUTH-ACT-001.server
SL-AUTH-ACT-002.server
SL-AUTH-ACT-003.server
```

Host surface: registration, login, activation route, app shell/protected client pages  
Actor: Guest / pending Client / active Client  
Slice type: client sidecar / auth activation UI  
Placement: `planning/slices/client/`

## 0. Scenario Sources

Business scenario:

```text
Guest registers account and must activate email before using protected client functionality.
```

UI scenario:

```text
Registration success -> check email.
Activation link -> success/error page.
Pending account login -> activation-required message.
Protected pages/actions hidden/blocked until active.
```

Cross-cutting behavior:

```text
Account activation required for protected functionality.
```

Data source:

```text
ClientAccount.Email is activation email.
```

Behavior items:

```text
AUTH-ACT-UI-001 — Registration success shows check-email state.
AUTH-ACT-UI-002 — Activation page posts token and shows success/error.
AUTH-ACT-UI-003 — Pending login shows activation-required error.
AUTH-ACT-UI-004 — App shell does not show protected Client navigation for inactive session/state.
AUTH-ACT-UI-005 — Protected pages show account-not-activated state if inactive state is ever present.
AUTH-ACT-UI-006 — Active account can continue normal app flow.
```

Concern umbrella:

```text
CC-AUTH-ACT-001
```

Stable source behavior item IDs are pending scenario/source registry.

## 1. Scope

In scope:

```text
- check-email page/state after registration;
- activation page route;
- activation API wrapper/hook;
- login activation-required error mapping;
- app shell/protected route active-account gate;
- Playwright updates for registration and business flows.
```

Minimal first-pass UX:

```text
Register -> check-email page.
Activation link opens /activate-account?token=...
Activation success -> show success and "Войти".
Pending login -> show activation-required error, no app session.
```

## 2. Out of Scope

| Out of scope | Owner / destination |
|---|---|
| Server token creation/activation endpoint | server slices |
| Resend activation email button | future client/server slice |
| Limited inactive session UX | future UX/security decision |
| Email template design | notification/content slice |
| Full visual redesign | later UI style pass |
| Password recovery | auth recovery slice |

## 3. Related Slices / Owners

| Slice | Relationship |
|---|---|
| `SL-ACC-001-register-client-account.client.md` | registration submit flow changes after success |
| `SL-AUTH-001-login-client-account.client.md` | maps activation-required problem |
| `SL-AUTH-002-current-user.client.md` | session active-state interpretation |
| `CC-AUTH-ACT-001` | cross-cutting coordination |
| `SL-AUTH-ACT-001.server` | registration semantics |
| `SL-AUTH-ACT-002.server` | activation endpoint |
| `SL-AUTH-ACT-003.server` | login/protected guard |

## 4. Visual UI / Scenario Flow

Registration:

```text
Guest opens /register
      ↓
Guest enters email/password
      ↓
Submit success
      ↓
System shows "Проверьте email"
      ↓
User follows email link
```

Activation:

```text
User opens /activate-account?token=...
      ↓
Client posts token to API
      ↓
Activating...
      ↓
success?
  ├─ yes → account activated, show login link
  └─ no  → show safe error and support/check-email guidance
```

Login pending:

```text
Guest opens /login
      ↓
Guest enters valid pending-account credentials
      ↓
API returns activation-required
      ↓
Login form shows "Аккаунт не активирован"
      ↓
No app shell protected nav is shown
```

## 5. Visual Layout / Screen Composition

Check-email page:

```text
[AppShell]
  Header guest/auth-neutral
  main.pageContainer
    PageCard
      Eyebrow: Регистрация
      h1: Проверьте email
      p: Мы отправили письмо на <email>.
      p: Перейдите по ссылке в письме, чтобы активировать аккаунт.
      Actions:
        Войти
        Зарегистрироваться заново / Изменить email (optional link)
  Footer
```

Activation page:

```text
[AppShell]
  Header guest/auth-neutral
  main.pageContainer
    PageCard
      pending: Активируем аккаунт...
      success: Аккаунт активирован
      error: Не удалось активировать аккаунт
      action: Войти
```

Protected inactive state if ever reachable:

```text
[AppShell]
  Header limited/no protected Client nav
  main.pageContainer
    PageCard
      h1: Аккаунт ожидает активации
      p: Проверьте письмо.
      actions: Выйти, Войти после активации
```

Responsive:

```text
Single-column cards.
No decorative marketing hero required in first pass.
```

## 6. Visual Client Implementation Flow

Route block:

```text
where it lives:
  energymanagement.client/src/app/router/router.tsx
owns:
  /activation-required or /activate-account route
uses:
  name: ActivationPage
  from: pages/auth/activate/ActivationPage.tsx
  needed to: read token query and render activation states
  visual: centered auth/status card in app shell
does not own:
  API mutation internals
```

Registration feature:

```text
where it lives:
  features/auth/register/**
owns:
  submit, field validation, registration API error mapping, success navigation
uses:
  name: registerClientAccount
  from: features/auth/register/api or shared auth wrapper
  needed to: submit registration data
does not own:
  activation token generation
```

Activation feature:

```text
where it lives:
  features/auth/activate-account/**
owns:
  activation API wrapper, mutation hook, token validation on client, visible states
uses:
  name: fetchJson
  from: shared/api/fetchJson.ts
  needed to: call POST /api/auth/activate
does not own:
  session creation
```

App shell/Header:

```text
where it lives:
  shared/ui/layout/Header.tsx
owns:
  visible navigation by session role and active state
uses:
  name: useSession / useSessionQuery
  from: entities/session/model
  needed to: decide role/active navigation
does not own:
  activation domain rules
```

## 7. Styling / CSS Ownership

| Area | Owner | CSS file | Rule |
|---|---|---|---|
| Activation/check-email page layout | page | `pages/auth/activate/*.css` or existing auth page CSS | container/card spacing only |
| Activation mutation feedback | feature/page | page/feature CSS | pending/success/error message |
| Login activation-required error | feature | `features/auth/login/ui/*.css` | root error already exists |
| Header nav active-state hiding | shared layout | `shared/ui/layout/*.css` | do not add business page styling |
| Tokens/base | global | `styles/*.css` | only if missing token needed |

Checklist:

```text
[ ] no broad global selector
[ ] no hover layout shift
[ ] no page CSS reaches into feature internals
[ ] loading/error/success states styled
[ ] accessible focus states preserved
```

## 8. Client API / Server Contract

Activation endpoint:

```text
POST /api/auth/activate
body: { token: string }
success: 204 No Content
errors: ProblemDetails with activation codes
```

Registration success:

```text
After successful register, client navigates to:
  /check-email?email=<encoded email>
or a route-state-only check-email page.

Avoid depending on email query if privacy requirements prefer not to show it.
```

Login error mapping:

```text
ProblemDetails code:
  auth.account.activation_required

maps to root form message:
  Аккаунт не активирован. Проверьте письмо и перейдите по ссылке активации.
```

## 9. Validation / Feedback / Error UI

Activation page:

```text
token missing:
  show invalid link error, do not call API.

token present:
  immediately call activation API once.
  show pending state.
  on success show activation success.
  on failure show safe error.
```

Registration:

```text
local field validation unchanged.
server validation unchanged.
on success navigate to check-email, not login/home.
```

Login:

```text
activation-required ProblemDetails maps to root error.
field errors remain for credential validation.
```

## 10. Accessibility / ARIA Contract

| Component | Native semantic element | Accessible name source | Keyboard behavior | ARIA needed? | Test query |
|---|---|---|---|---|---|
| Check-email page heading | `h1` | visible text | normal | no | `getByRole("heading", { name: /Проверьте email/ })` |
| Activation status | `section` / `p` | visible text | normal | `aria-live="polite"` for pending/result | `getByText(...)` |
| Login activation error | `p role=alert` | visible text | normal | `role="alert"` | `getByRole("alert")` |
| Login link | `a` | visible text | Enter activates | no | `getByRole("link", { name: /Войти/ })` |
| Header nav | `nav` | existing label | tab through links | no | role/link queries |

## 11. Cross-Cutting Concerns

| Concern | Applies? | Consideration / owner |
|---|---:|---|
| Auth/session | yes | pending accounts do not get full session |
| Authorization/visibility | yes | protected Client nav hidden unless active |
| ProblemDetails | yes | activation-required code mapped to root error |
| OpenAPI/generated | yes | activation endpoint types generated |
| CSRF | yes | activation POST may require antiforgery token from SPA |
| Privacy | yes | avoid leaking raw token/email unnecessarily |
| E2E | yes | registration E2E changes from login redirect to check-email |

## 12. Questions / Decisions

| ID | Status | Question | Assumption / current direction | Impact |
|---|---|---|---|---|
| SL-AUTH-ACT-UI-Q001 | accepted direction | Does pending account receive limited session? | No. Login is rejected first pass. | Simpler client gate. |
| SL-AUTH-ACT-UI-Q002 | accepted direction | Where does register navigate after success? | Check-email page/state. | Changes register E2E. |
| SL-AUTH-ACT-UI-Q003 | accepted direction | Does activation auto-login user? | No. Show success + login link. | Avoids session complexity. |
| SL-AUTH-ACT-UI-Q004 | future review | Is resend activation needed? | Deferred. | Future UX. |
| SL-AUTH-ACT-UI-Q005 | open | Should email be visible on check-email page? | Can show submitted email first pass; avoid if privacy policy says otherwise. | UI copy/privacy. |
| SL-AUTH-ACT-UI-Q006 | accepted direction | Should protected nav show for inactive account? | No, hide protected Client actions. | Header/session gate. |

## 13. Extension / Change Points

```text
- Add resend activation action.
- Add limited session if UX chooses it.
- Add account activation status page in account area.
- Add notification outbox/retry.
- Add email template preview/testing.
```

## 14. Behavior Coverage

| Behavior item | Scenario/source meaning | Covered by this slice? | Notes |
|---|---|---:|---|
| AUTH-ACT-UI-001 | Register success shows check-email | yes | page/route |
| AUTH-ACT-UI-002 | Activation page handles token | yes | mutation + states |
| AUTH-ACT-UI-003 | Pending login shows activation error | yes | form root error |
| AUTH-ACT-UI-004 | Protected nav hidden for inactive | yes | shell/session gate |
| AUTH-ACT-UI-005 | Protected pages have inactive state | yes | if inactive session can exist |
| AUTH-ACT-UI-006 | Active account normal flow works | yes | regression |

## 15. Test / Verification Plan

Primary rule:

```text
Tests verify behavior items and visible scenario outcomes.
Implementation details are only setup/action/observation mechanisms.
```

### Behavior-to-Test Trace

| Behavior item | Visible scenario outcome | Test layer | Implementation mechanism | Escape risk | Refactor risk | Planned/actual test |
|---|---|---|---|---|---|---|
| AUTH-ACT-UI-001 | After register, user sees check-email page | E2E + page test | real/register mocked API | Medium if mocked only; E2E lowers risk | Low | `register.spec.ts` update |
| AUTH-ACT-UI-002 | Valid activation link shows success | Page/component + E2E if token helper available | mocked API or test token | Medium | Medium | `ActivationPage.test.tsx` |
| AUTH-ACT-UI-002 | Invalid/missing token shows safe error | Page/component | route query + mocked API | Low | Low | `ActivationPage_InvalidToken_ShowsError` |
| AUTH-ACT-UI-003 | Pending login shows activation-required alert | Login form/page test + API integration/E2E | ProblemDetails fixture | Medium | Low | `LoginForm_ActivationRequired_ShowsRootError` |
| AUTH-ACT-UI-004 | Header does not show Create/My Requests/My Agreements for inactive session | Header test | session fixture inactive | Low | Low | `Header_HidesClientProtectedNavForInactiveAccount` |
| AUTH-ACT-UI-006 | Activated account can use normal business flow | E2E | seeded activated user or activation helper | Low | Low | Playwright business flows updated |

E2E update rule:

```text
Registration E2E:
  register -> check-email page.

Business E2E:
  use activated seeded account
  OR register + activate through test helper/token
  before creating ApplicantParty/request.
```

## 16. Suggested File Placement

```text
src/pages/auth/check-email/
  CheckEmailPage.tsx
  checkEmailPage.css

src/pages/auth/activate/
  ActivationPage.tsx
  activationPage.css

src/features/auth/activate-account/api/
  activateAccount.ts
  activateAccountApiTypes.ts

src/features/auth/activate-account/model/
  useActivateAccountMutation.ts

src/features/auth/login/
  update API error mapping for auth.account.activation_required

src/shared/ui/layout/Header.tsx
  hide protected Client nav unless session.isActive

src/entities/session/model/
  ensure CurrentUserResponseDto.isActive is available in session model
```

Do not add business-specific activation wrapper to `shared/api` unless existing auth wrapper compatibility requires it.

## 17. Implementation Checklist

```text
[ ] Add check-email route/page.
[ ] Add activation route/page.
[ ] Add activation API wrapper/mutation.
[ ] Update register success navigation.
[ ] Update login ProblemDetails mapping.
[ ] Update Header protected nav gating by isActive.
[ ] Add protected route/page inactive fallback if inactive session is possible.
[ ] Update component/page tests.
[ ] Update Playwright registration and business setup.
[ ] Regenerate OpenAPI/types after server endpoint exists.
```

## 18. Next Step

Implement server slices first, then regenerate contracts, then implement this client sidecar.

This pass did not perform implementation verification.
