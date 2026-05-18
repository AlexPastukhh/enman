# Client API Placement Decision

Status: current / accepted client architecture direction  
Scope: placement of client HTTP wrappers, generated DTO aliases, entity read APIs and feature command APIs

## 1. Decision

Use the stricter client placement rule:

```text
shared/
  owns only genuinely shared infrastructure.

entities/*/
  owns read-side business/entity API wrappers.

features/*/
  owns command/user-action API wrappers.
```

This supersedes the earlier planning direction where `shared/api/*Api.ts` owned all low-level business endpoint wrappers and entities/features delegated to them.

## 2. Why The Previous Option Was Plausible

The previous option was coherent if `shared/api` was treated as a centralized backend contract adapter layer:

```text
generated OpenAPI types
        ↓
shared/api/<businessArea>Api.ts
        ↓
entities/*/api or features/*/api
        ↓
query/mutation hooks
        ↓
UI
```

Possible benefits:

```text
- one folder to inspect all server endpoint wrappers;
- generated DTO aliases near transport code;
- endpoint path and fetchJson usage centralized;
- lower chance of ad-hoc raw fetch outside the shared boundary;
- easy contract-drift repair point after OpenAPI generation.
```

However, those benefits require business-aware files under `shared/api`.

## 3. Why The New Rule Is Preferred

The stronger project direction is:

```text
shared = actually shared infrastructure;
entities = read/data/display ownership;
features = user-action/command ownership.
```

Business endpoint wrappers belong close to the thing that owns their behavior:

```text
entities/applicant-party/api/listAccountApplicantParties.ts
  owns read endpoint call for ApplicantParty list.

features/applicant-party/make-current-default/api/makeApplicantPartyCurrentDefault.ts
  owns command endpoint call for make-current/default action.
```

## 4. Ownership

### shared

Owns:

```text
fetchJson
ApiError / ProblemDetails parsing
AntiforgeryApiError / CSRF transport helpers
generated OpenAPI types
generic request/response helpers
generic query-string/build-url helpers if not business-specific
generic config/lib/ui primitives
```

Does not own:

```text
ApplicantParty endpoint wrapper
EmployeeRequest endpoint wrapper
Request command wrapper
AgreementExchange endpoint wrapper
business DTO aliases
business path constants
entity-specific mapping
mutation success behavior
```

### entities

Own read-side API and display behavior:

```text
entities/<entity>/api/
  read endpoint wrappers;
  read DTO aliases from generated types;
  read DTO -> view model mapping when needed;
  endpoint path constants when entity-specific.

entities/<entity>/model/
  query keys;
  React Query read hooks;
  read model types;
  read helpers.

entities/<entity>/ui/
  read-only display components.
```

Allowed examples:

```text
entities/applicant-party/api/listAccountApplicantParties.ts
entities/applicant-party/api/getCurrentIndividualApplicantParty.ts
entities/employee-request/api/listEmployeeDashboardRequests.ts
entities/employee-request/api/getEmployeeRequestDetails.ts
entities/agreement-exchange/api/listAgreementExchanges.ts
entities/agreement-exchange/api/getAgreementExchangeDetails.ts
```

### features

Own command/user-action behavior:

```text
features/<business-action>/api/
  mutation endpoint wrapper;
  command DTO/result aliases from generated types;
  command-specific request building.

features/<business-action>/model/
  mutation hooks;
  submit orchestration;
  success/error behavior;
  query invalidation/refetch after action.

features/<business-action>/ui/
  action buttons;
  forms;
  pending/error/success feedback.
```

Allowed examples:

```text
features/applicant-party/create-individual/api/createIndividualApplicantParty.ts
features/applicant-party/make-current-default/api/makeApplicantPartyCurrentDefault.ts
features/employee-request/start-review/api/startRequestReview.ts
features/employee-request/approve-review/api/approveRequestReview.ts
features/employee-request/reject-review/api/rejectRequestReview.ts
features/agreement-exchange/accept-proposal/api/acceptAgreementProposal.ts
features/agreement-exchange/final-refuse/api/finalRefuseAgreementExchange.ts
```

## 5. Generated Types Rule

Generated OpenAPI types remain shared infrastructure:

```text
shared/api/generated/openapi-types.ts
```

Entities and features may import generated types directly and create local aliases near the owner.

Do not manually edit generated artifacts.
