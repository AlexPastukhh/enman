# Architecture Decision Notes

Status: active accepted-decision registry  
Scope: current accepted architecture decisions that guide planning/implementation and may support diploma text

## 1. Cross-Cutting / Helper Slice Decisions

| ID | Decision | Current accepted direction | Rationale / trade-off | Affected artifacts | ADR promotion |
|---|---|---|---|---|---|
| ADN-061 | Cross-cutting/helper slices are allowed | Technical/support work with behavior, implementation flow and tests can be documented as cross-cutting/helper slice. | Avoids burying implementation-ready support work in generic workflow notes. | slice workflow docs | Candidate |
| ADN-062 | Implementation flow detail filter | Include classes/methods/code snippets only for key decisions/boundaries/non-obvious behavior; keep routine code high-level. | Keeps slice files readable while still useful for diploma/implementation. | slice docs, client docs, cross-cutting slices | Candidate |
| ADN-071 | Cross-cutting/helper slices use same format as business slices | Source requirements -> behavior items -> concern flow -> implementation flow -> tests -> coverage/questions/ADR impact. | Keeps technical concerns traceable and consistent with business slices. | cross-cutting README, slice guide | Candidate |
| ADN-072 | Concern-derived behavior items are first-class behavior items | Security/API/tooling/testing/infrastructure-derived items are valid and must be covered by concern flow. | Prevents technical behavior from being hidden only in implementation notes. | behavior item docs, cross-cutting slices | Candidate |

## 2. CSRF / Antiforgery Decisions

| ID | Decision | Current accepted direction | Rationale / trade-off | Affected artifacts | ADR promotion |
|---|---|---|---|---|---|
| ADN-073 | CSRF/antiforgery is a cross-cutting slice | `CC-CSRF-001` is the primary implementation-ready slice for antiforgery token/session behavior. | Concern affects many unsafe browser API commands and has independent behavior/tests. | CC-CSRF-001, browser security addendum | Candidate |
| ADN-074 | CSRF behavior items are security-derived | Create behavior items even though CSRF is not a business scenario. | Adds documentation overhead but improves traceability for diploma/security implementation. | CC-CSRF behavior items | Candidate |
| ADN-075 | Cookie-auth unsafe browser API requests require antiforgery support | Browser unsafe API commands use antiforgery token support. | Cookie auth needs CSRF protection for unsafe browser requests. | CC-CSRF-001, auth/API/client helpers | Candidate |
| ADN-076 | Token refresh/reset follows session context changes | Client refetches/resets token after login/logout/session context changes. | Token validity is tied to security/session context. | client auth/session helpers | Candidate |
| ADN-077 | Antiforgery failure normalized by always-run result filter | Use always-run result filter to convert framework antiforgery failure into project ProblemDetails. | Keeps API error contract consistent for client handling. | API filters, ProblemDetails | Candidate |
| ADN-078 | Antiforgery filter checks failure marker/result, not generic HTTP 400 | Filter must detect antiforgery failure specifically. | Avoids mislabeling ordinary DTO validation or other bad requests as CSRF failures. | API filters/tests | Candidate |
| ADN-079 | Client does not blindly replay unsafe commands after token refresh | Antiforgery failure can trigger token refetch, but unsafe command requires explicit user retry. | Avoids hidden side effects and makes recovery explicit. | client request helper/tests | Candidate |

## 3. Existing Testing / Constants Decisions Referenced

| ID | Decision | Current accepted direction |
|---|---|---|
| ADN-058 | Constants generation/testing is a cross-cutting slice | `CC-CONST-001` is the primary implementation-ready pseudo-slice. |
| ADN-059 | Constants testing strategy | Use sync check, generated JSON API tests, critical literal tests and generator tests. |
| ADN-063 | Testing responsibility split | Domain/server/client/E2E tests have separate responsibilities. |
| ADN-064 | E2E scope | E2E verifies browser-client-server wiring, not exhaustive UI behavior. |
