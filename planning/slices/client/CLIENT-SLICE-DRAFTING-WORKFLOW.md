# Client Slice Drafting Workflow

Status: current workflow for drafting client sidecars / scenario source and test trace synchronized

## 1. Before Drafting

Read:

```text
CLIENT-API-PLACEMENT-DECISION.md
CLIENT-LAYERING-FOR-READ-AND-COMMAND-SLICES.md
CLIENT-UI-STYLE-WORKFLOW.md
CLIENT-CSS-ARCHITECTURE-RULES.md
CLIENT-FORM-VALIDATION-WORKFLOW.md
CLIENT-A11Y-WORKFLOW.md
planning/slices/slice-test-plan-workflow.md
CLIENT-SLICE-TEMPLATE.md
```

Then identify scenario sources:

```text
business scenario:
UI scenario:
cross-cutting behavior:
data source:
behavior items:
concern umbrella:
```

## 2. UI Scenario Source Check

Before writing `Visual UI / Scenario Flow`:

```text
1. Open planning/diagrams/scenario-ui-specs/.
2. Check UI-SCENARIO-READINESS.md when present.
3. Find the related UI scenario source.
4. If the UI scenario is missing/stub/partial and the slice changes meaningful UI behavior, update the UI scenario first.
5. Do not invent UI requirements only inside the client slice draft.
```

## 3. Cross-Cutting Behavior Source Check

If the slice implements common client behavior used by many scenarios:

```text
1. Check planning/diagrams/scenario-cross-cutting/client-behavior/.
2. If no behavior source exists, create/update one before writing the implementation slice draft.
3. Use a normal client slice draft structure even for cross-cutting implementation.
```

Examples:

```text
deferred validation
common error feedback
flow navigation
loading/empty/error visibility
```

## 4. Drafting Order

```text
1. Identify scenario sources.
2. Identify parent logical slice or concern.
3. Identify host surface/page or shared behavior scope.
4. Define actor and route/context if applicable.
5. Define Visual UI / Scenario Flow from UI/cross-cutting scenario sources.
6. Define Visual Layout / Screen Composition.
7. Define Visual Client Implementation Flow.
8. Define Styling / CSS Ownership.
9. Define API/server contract if any.
10. Define validation/feedback/a11y.
11. Define behavior coverage.
12. Define Behavior-to-Test Trace and verification plan.
```

## 5. Visual Client Implementation Flow Rule

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

## 6. Test / Verification Plan Rule

Use:

```text
planning/slices/slice-test-plan-workflow.md
```

Every client slice draft must include Behavior-to-Test Trace.

For each planned/actual test, answer:

```text
Which behavior item or visible scenario outcome does this test prove?
Which implementation details are used only as setup/action/observation mechanisms?
Can a bad implementation pass this test and still break the scenario?
Can a behavior-preserving refactor break this test?
What no-mutation/negative outcome is relevant, if any?
```

Component/page tests prove visible client behavior for a given server response or command result.

Server behavior must be proven by server/API tests. E2E can connect the flow but does not replace all matrix tests.

## 7. Paired/Single Naming

If no server counterpart is expected, use `SINGLE-` prefix in the client draft file name:

```text
SINGLE-CC-CLIENT-FORM-VALIDATION-001-deferred-validation.client.md
```

If a server counterpart is expected or possible, do not use `SINGLE-`; use matching logical ID:

```text
CC-SEC-CSRF-001-unsafe-command-protection.client.md
```

## 8. Drafting Checklist

```text
[ ] I used CLIENT-SLICE-TEMPLATE.md.
[ ] I identified scenario sources.
[ ] I checked UI scenario source/readiness when UI behavior is involved.
[ ] I checked cross-cutting behavior source when common behavior is involved.
[ ] I identified parent server slice, logical slice, or concern.
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
[ ] I added Behavior Coverage.
[ ] I added Behavior-to-Test Trace.
[ ] I separated Behavior Coverage from Test / Verification Plan.
```
