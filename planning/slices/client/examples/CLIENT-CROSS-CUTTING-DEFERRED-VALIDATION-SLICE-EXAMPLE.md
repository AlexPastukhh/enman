# Example — SINGLE-CC-CLIENT-FORM-VALIDATION-001.client

Status: example only  
Logical slice: CC-CLIENT-FORM-VALIDATION-001 — Deferred Form Validation  
Parent server slice: none  
Host surface: client forms across auth/request/applicant/review/agreement flows  
Actor: Guest / Client / Employee depending on host form  
Slice type: client-only cross-cutting behavior  
Placement: `planning/slices/client/cross-cutting/`

## 0. Scenario Sources

Business scenario: none; cross-cutting client behavior  
UI scenario: applies to multiple form UI scenarios  
Cross-cutting behavior:

```text
planning/diagrams/scenario-cross-cutting/client-behavior/CC-CLIENT-FORM-VALIDATION-001-deferred-validation.behavior.md
planning/diagrams/scenario-cross-cutting/client-behavior/CC-CLIENT-FEEDBACK-001-error-feedback.behavior.md
```

Data source: per host scenario/form  
Behavior items:

```text
CC-CLIENT-FORM-VALIDATION-001-B01
CC-CLIENT-FORM-VALIDATION-001-B02
CC-CLIENT-FORM-VALIDATION-001-B03
CC-CLIENT-FORM-VALIDATION-001-B04
CC-CLIENT-FORM-VALIDATION-001-B05
CC-CLIENT-FORM-VALIDATION-001-B06
```

Concern umbrella: none; intentionally client-only, therefore `SINGLE-`.

## 1. Scope

Provide a reusable client-side validation behavior pattern for forms:

```text
user edits field
  -> field validation waits briefly
  -> field-level error appears near field if invalid
  -> valid input clears field-level error
  -> submit validates immediately
  -> server field/root errors remain visible and actionable
```

This slice covers the client behavior and implementation convention. Each concrete form slice still decides exact fields, messages and server contract.

## 2. Out of Scope

```text
server validation implementation
FluentValidation rules
exact validation copy for every form
business-specific form fields
toast system
modal confirmation flows
```

## 3. Related Slices / Owners

```text
Auth login/register slices consume this behavior.
Request creation form consumes this behavior.
ApplicantParty create form consumes this behavior.
Agreement/review command forms consume this behavior when they have fields.
```

Owner split:

```text
shared/form or shared/lib:
  reusable deferred validation helper if introduced

feature form:
  concrete fields, messages, submit handling, server error mapping

page:
  placement around the form only
```

## 4. Visual UI / Scenario Flow

```text
User starts typing in a field
        ↓
UI does not immediately show noisy validation on every keypress
        ↓
After configured delay, invalid field shows nearby error
        ↓
User corrects field
        ↓
Field error clears
        ↓
User submits form
        ↓
All fields validate immediately
        ↓
Client-side errors block submit, or request is sent
        ↓
Server field/root errors are shown in field/form areas
```

## 5. Visual Layout / Screen Composition

Generic host form composition:

```text
[FormBlock]
  Form title / intro if owned by host form
  Form-level error area
  Field group
    Label
    Input
    Field-level help/error
  Actions row
    Primary submit
    Secondary/cancel action if host form has it
```

State expectations:

```text
idle:
  labels and fields visible

typing:
  no layout jump; pending validation must not resize button/action area

field invalid:
  error appears near field

submit invalid:
  all relevant field/root errors appear immediately

server rejected:
  field/root errors are visible and actionable

pending submit:
  submit disabled or marked pending without hover/layout shift
```

## 6. Visual Client Implementation Flow

[Deferred validation behavior helper]  
`shared/lib/validation/useDeferredFieldValidation.ts`

Lives here:
  `useDeferredFieldValidation`

Owns:
  deferred timing behavior;
  validation scheduling;
  clearing obsolete field errors when value becomes valid.

Uses:
  browser timer/debounce primitive
    from: platform/browser runtime
    needed to: delay validation feedback while user is typing.

Does not own:
  concrete field names;
  server ProblemDetails mapping;
  visual field rendering;
  form submission.

[Form error mapping helper]  
`shared/api/problemDetailsToFormErrors.ts`

Lives here:
  `problemDetailsToFormErrors`

Owns:
  converting server ProblemDetails-like response into field/root form error shape.

Uses:
  `ApiError`
    from: `shared/api/fetchJson.ts`
    needed to: read server validation/domain error details.

Does not own:
  field rendering;
  specific copy decisions;
  business validation rules.

[Concrete feature form]  
`features/<feature>/<action>/ui/<FeatureForm>.tsx`

Lives here:
  concrete form component

Owns:
  fields;
  labels;
  submit button;
  field-level errors;
  form-level error area;
  pending/disabled state;
  applying deferred validation behavior to concrete fields.

Uses:
  `useDeferredFieldValidation`
    from: `shared/lib/validation/useDeferredFieldValidation.ts`
    needed to: avoid noisy immediate field errors while typing.

  `problemDetailsToFormErrors`
    from: `shared/api/problemDetailsToFormErrors.ts`
    needed to: display server field/root errors in the form.

  `FormField`
    from: `shared/ui/form/FormField.tsx`
    needed to: render accessible label/input/error grouping.
    visual: one labeled field block inside the host form; parent form controls spacing around field blocks.

Does not own:
  page placement around the form;
  server validation rules;
  transport-level fetch behavior.

## 7. Styling / CSS Ownership

| Area | Owner | CSS file | Rule |
|---|---|---|---|
| Form block layout | feature form | `features/.../<FeatureForm>.css` | concrete form spacing/actions/errors |
| Generic field primitive | shared/ui | `shared/ui/form/formField.css` | domain-agnostic label/input/error grouping |
| Page placement | page | `pages/.../*.css` | only placement around form |
| Tokens/base | global | `styles/*.css` | color/spacing/radius variables only |

Checklist:

```text
[ ] no broad global selector
[ ] no hover layout shift
[ ] no border-width change on hover
[ ] field error does not push unrelated page blocks unpredictably
[ ] form-level error is visually distinct
[ ] disabled/pending submit is clear
```

## 8. Client API / Server Contract

No new endpoint is introduced by this slice.

This slice consumes existing server error contracts through shared transport/error shape.

Server remains source of truth:

```text
client validation improves UX only;
server validation/domain errors must still be displayed.
```

## 9. Validation / Feedback / Error UI

Required behavior:

```text
field validation after delay while typing
submit validation immediately
field-level errors near fields
root/domain errors in form-level area
server field errors mapped to fields when possible
server root/domain errors mapped to form-level area
```

Auth-specific autocomplete convention:

```text
email -> autocomplete="email"
login password -> autocomplete="current-password"
register password -> autocomplete="new-password"
confirm password -> autocomplete="new-password"
```

## 10. Accessibility / ARIA Contract

| Component | Native semantic element | Accessible name source | Keyboard behavior | ARIA needed? | Test query |
|---|---|---|---|---|---|
| Field label/input | `label` + `input`/`textarea` | visible label | normal tab/input | `aria-invalid`, `aria-describedby` when invalid | `getByLabelText(...)` |
| Field error | text element | field relationship | none | referenced by `aria-describedby` | text/error query |
| Form root error | alert/status block | visible error text | none | `role="alert"` when immediate error | `getByRole("alert")` |
| Submit | `button` | button text | Enter/click | no extra ARIA unless pending text hidden | `getByRole("button", { name: ... })` |

## 11. Cross-Cutting Concerns

```text
CC-CLIENT-FEEDBACK-001-error-feedback.behavior.md
CLIENT-CSS-ARCHITECTURE-RULES.md
CLIENT-A11Y-WORKFLOW.md
slice-test-plan-workflow.md
```

## 12. Questions / Decisions

Accepted:

```text
deferred validation is client-only behavior unless server contract changes;
server errors must remain visible;
generic-only "Something went wrong" is not enough when useful server messages exist.
```

Open:

```text
exact debounce duration can be chosen during implementation.
```

## 13. Extension / Change Points

Future extension points:

```text
shared form primitives
toast/status system
field array validation
async uniqueness validation
modal confirmation forms
```

## 14. Behavior Coverage

| Behavior item | Covered? | Notes |
|---|---|---|
| `CC-CLIENT-FORM-VALIDATION-001-B01` | yes | field validation may be deferred |
| `CC-CLIENT-FORM-VALIDATION-001-B02` | yes | submit validates immediately |
| `CC-CLIENT-FORM-VALIDATION-001-B03` | yes | field error near field |
| `CC-CLIENT-FORM-VALIDATION-001-B04` | yes | root/domain errors in form area |
| `CC-CLIENT-FORM-VALIDATION-001-B05` | yes | server remains source of truth |
| `CC-CLIENT-FORM-VALIDATION-001-B06` | yes | auth autocomplete convention |
| `CC-CLIENT-FEEDBACK-001-B05` | yes | avoid generic-only error when useful server message exists |

## 15. Test / Verification Plan

Primary rule:

```text
Tests verify behavior items and visible scenario outcomes.
Implementation details are allowed only as setup/action/observation mechanisms.
```

### Behavior-to-Test Trace

| Behavior item | Visible scenario outcome | Test layer | Implementation mechanism | Escape risk | Refactor risk | Planned/actual test |
|---|---|---|---|---|---|---|
| `CC-CLIENT-FORM-VALIDATION-001-B01` | Error does not appear immediately on every keypress | component test | fake timers, render test form, type input | Low: immediate noisy validation is caught | Medium: timer implementation refactor may require helper-level test update | `useDeferredFieldValidation_DelaysFieldErrorWhileTyping` |
| `CC-CLIENT-FORM-VALIDATION-001-B02` | Submit shows validation immediately | component test | render test form, click submit | Low: delayed-only submit bug is caught | Low: form internals can refactor if visible error remains | `DeferredFormValidation_SubmitValidatesImmediately` |
| `CC-CLIENT-FORM-VALIDATION-001-B03` | Field error appears near related field | component test | render field, invalid value, query label/error association | Low: detached/global-only error is caught | Low: styling refactor should not break semantic association | `FormField_ShowsFieldErrorNearInput` |
| `CC-CLIENT-FORM-VALIDATION-001-B04` | Server root/domain error appears in form error area | component test | mock rejected command with root ProblemDetails | Low: swallowed/root-hidden error is caught | Low/Medium: copy changes may require semantic query | `FeatureForm_ShowsServerRootErrorInFormAlert` |
| `CC-CLIENT-FORM-VALIDATION-001-B05` | Server field error remains visible even if client precheck passed | component test | mock server field error response | Low: client-only validation bypass bug is caught | Medium: exact ProblemDetails mapping refactor may require helper test update | `FeatureForm_MapsServerFieldErrorToField` |
| `CC-CLIENT-FORM-VALIDATION-001-B06` | Auth inputs expose correct autocomplete | component test | render login/register forms | Low for autocomplete regression | Low: layout refactor should not affect input attributes | `AuthForms_UseExpectedAutocompleteAttributes` |

### Test buckets

Component/helper tests:

```text
deferred field error appears after delay
submit validates immediately
server field error maps to field
server root error maps to form alert
autocomplete attributes are correct
```

E2E smoke:

```text
one representative form flow can verify visible validation behavior through browser UI
```

What not to test:

```text
exact hook call order
exact timer implementation if visible behavior is preserved
exact CSS class name
React internal state shape
```

No-mutation:

```text
not applicable at client-only layer;
server command slices must prove no-mutation separately.
```

## 16. Suggested File Placement

```text
planning/slices/client/cross-cutting/SINGLE-CC-CLIENT-FORM-VALIDATION-001-deferred-validation.client.md

shared/lib/validation/useDeferredFieldValidation.ts
shared/api/problemDetailsToFormErrors.ts
shared/ui/form/FormField.tsx
shared/ui/form/formField.css
```

Concrete forms adopt this behavior gradually in their own feature slices.

## 17. Implementation Checklist

```text
[ ] add/update behavior source if needed
[ ] implement deferred validation helper
[ ] implement form error mapping helper if missing
[ ] update one representative form first
[ ] add component/helper tests with Behavior-to-Test Trace mapping
[ ] document adoption path for other forms
```

## 18. Next Step

Create implementation archive only after this draft is accepted and one target representative form is selected.
