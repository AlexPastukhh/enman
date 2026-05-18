# Client Slice Drafting Workflow

Status: current workflow for drafting client sidecars

## 1. Before Drafting

Read:

```text
CLIENT-API-PLACEMENT-DECISION.md
CLIENT-LAYERING-FOR-READ-AND-COMMAND-SLICES.md
CLIENT-UI-STYLE-WORKFLOW.md
CLIENT-CSS-ARCHITECTURE-RULES.md
CLIENT-FORM-VALIDATION-WORKFLOW.md
CLIENT-A11Y-WORKFLOW.md
CLIENT-SLICE-TEMPLATE.md
```

## 2. Drafting Order

```text
1. Identify parent server slice.
2. Identify host surface/page.
3. Define actor and route.
4. Define Visual UI / Scenario Flow.
5. Define Visual Layout / Screen Composition.
6. Define Visual Client Implementation Flow.
7. Define Styling / CSS Ownership.
8. Define API contract.
9. Define validation/feedback/a11y.
10. Define verification plan.
```

## 3. Visual Client Implementation Flow Rule

Use `needed to`, not `does`.

Every dependency entry includes:

```text
from:
needed to:
```

Only UI-rendering dependencies include:

```text
visual:
```

The `visual` line describes the dependency as a block in the parent composition. It does not describe child internals.

Good:

```text
Uses:
  useAgreementExchangeDetailsQuery(exchangeId)
    from: entities/agreement-exchange/model/useAgreementExchangeDetailsQuery.ts
    needed to: load the exchange details read model for this route.

  AgreementExchangeDetailsView
    from: widgets/agreement-exchange-details/AgreementExchangeDetailsView.tsx
    needed to: render shared exchange details content and expose the action slot.
    visual: main details block placed below the page header; parent controls spacing around it.
```

Bad:

```text
visual: card with 16px padding, green status badge, button radius and internal borders.
```

That describes child internals and belongs in the child owner's styling rules.

## 4. Drafting Checklist

```text
[ ] I used CLIENT-SLICE-TEMPLATE.md.
[ ] I identified parent server slice and host surface.
[ ] I described the visual layout before implementation flow.
[ ] I used from / needed to / visual correctly.
[ ] I added visual only for UI-rendering dependencies.
[ ] I did not describe child internals in parent visual lines.
[ ] I placed read endpoint wrappers in entities/*/api.
[ ] I placed command endpoint wrappers in features/*/api.
[ ] I kept shared/api to fetchJson, ProblemDetails, CSRF helpers and generated OpenAPI types.
[ ] I added Styling / CSS Ownership.
[ ] I added Validation / Feedback / Error UI.
[ ] I added Accessibility / ARIA Contract.
[ ] I separated Behavior Coverage from Test/Verification Plan.
```
