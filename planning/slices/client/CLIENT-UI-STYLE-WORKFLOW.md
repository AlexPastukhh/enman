# Client UI / Style Workflow

Status: current client UI workflow

## 1. Goal

Build clean, maintainable, slice-owned UI before adding decorative styling.

The immediate target is a simple and presentable application UI:

```text
clear flow
stable spacing
readable forms
flow-based navigation
calm colors
cards and sections with enough air
```

## 2. Workflow

```text
1. Discover current UI/CSS and existing components.
2. Define real route/page flow first.
3. Define visual layout before coding.
4. Define validation and feedback behavior.
5. Define CSS ownership by layer.
6. Implement foundation/layout first.
7. Normalize forms and deferred validation.
8. Normalize production pages.
9. Add restrained visual styling.
10. Only then consider decorative/legacy-site-inspired UI.
```

## 3. Header/navigation rule

Current header is flow-based only.

Guest:

```text
Главная
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

Do not add decorative public navigation first pass:

```text
О компании
Потребителям
Закупки
Вакансии
```

Those require a separate public/marketing slice.

## 4. Visual direction

Use restrained visual direction inspired by the reference screenshots:

```text
white top/header
purple navigation/accent area
calm green accents
white cards
clear spacing
stable buttons
simple forms
```

Do not copy the legacy site as a public portal first pass.

## 5. Implementation order

Archive 1:

```text
foundation/layout/forms/header/home/account/auth
```

Archive 2:

```text
production pages normalization:
  requests
  employee requests
  agreement exchanges
```

Archive 3:

```text
cleanup/debt removal:
  duplicate UI
  unused CSS
  test/mock isolation
```

## 6. Page flow before decoration

Before adding decorative styles, every production route should have:

```text
page title
clear primary action
loading state
empty state
error/access state
stable layout
valid navigation path from home/header
```

## 7. Visual checks

For each UI slice, list manual visual routes and states to inspect.
