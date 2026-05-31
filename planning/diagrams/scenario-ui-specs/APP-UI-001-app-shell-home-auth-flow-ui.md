# APP-UI-001 — App Shell / Home / Auth / Flow Navigation UI Scenario

Status: current first-pass UI foundation source  
Doc version: v0.1.0  
Applies to: app shell, home page, header navigation, auth/account entry screens  
Actors: Guest, Client, Employee  
Related scenario: application flow foundation  
Related slices: future `CLIENT-APP-SHELL-001.client`

## 1. User Goal

The user should be able to open the application, understand the available flow, and navigate to the correct first action for their role.

## 2. Screen Entry Points

```text
Guest opens application root
        ↓
System shows home page with Guest navigation and login/register actions

Client opens application root
        ↓
System shows home page with Client flow actions

Employee opens application root
        ↓
System shows home page with Employee work actions
```

Primary routes:

```text
/
 /login
 /register
 /account
```

## 3. Screen Composition

```text
[AppShell]
  Header
    logo/title area
    flow-based navigation
  main.pageContainer
    Home or auth/account content
  Footer
```

Home page first pass:

```text
Home page
  hero / intro
  role-aware primary actions
  flow cards:
    Client flow
    Employee flow
    Guest flow
```

Auth pages first pass:

```text
Auth page
  intro block
  form card
  secondary action link
```

Account signed-out first pass:

```text
Account page without session
  signed-out explanation
  login/register actions
```

## 4. Visible Data

Home/header should show enough information for the user to choose the next flow.

Guest navigation:

```text
Главная
Войти
Регистрация
```

Client navigation:

```text
Создать заявку
Мои заявки
Мои договоры
Личный кабинет
```

Employee navigation:

```text
Заявки
Договорные обмены
Личный кабинет
```

## 5. Actions

Guest:

```text
Войти
Регистрация
```

Client:

```text
Создать заявку
Мои заявки
Мои договоры
Личный кабинет
```

Employee:

```text
Заявки
Договорные обмены
Личный кабинет
```

## 6. State Matrix

| State | Visible UI | Available actions | Notes |
|---|---|---|---|
| Guest | Guest header + home intro | Login, Register | No decorative public portal navigation first pass |
| Client | Client header + client flow actions | Create request, My requests, My agreements, Account | App flow navigation only |
| Employee | Employee header + employee work actions | Employee requests, Agreement exchanges, Account | App flow navigation only |
| Signed-out account | Signed-out panel | Login, Register | Do not render registration form inside account page |

## 7. Empty / Loading / Error States

Session loading:

```text
show stable app shell and non-jumping loading/placeholder state
```

Auth error:

```text
show field/root error in form area
do not show only "Something went wrong" when better server messages exist
```

## 8. Actor-Specific Differences

Header links and home primary actions differ by actor role.

No decorative public nav items first pass:

```text
О компании
Потребителям
Закупки
Вакансии
```

Those require a separate public/marketing UI scenario.

## 9. Feedback / Validation Requirements

Login/register forms should follow deferred validation rules:

```text
field errors near fields
root errors in form alert area
submit validates immediately
auth autocomplete values are correct
```

## 10. Accessibility Notes

```text
Header navigation uses semantic links.
Buttons use native buttons or links with accessible names.
Forms have visible labels connected to fields.
Error messages are associated with fields when possible.
Root form errors may use role="alert".
```

## 11. Out of Scope

```text
decorative public portal navigation
large marketing dropdowns
legacy-site full visual copy
full account ApplicantParty section internals
request/agreement production page internals
```

## 12. Related Client Slice Drafts

```text
future planning/slices/client/CLIENT-APP-SHELL-001.client.md
existing auth/account/home client drafts when migrated
```
