# SL-AUTH-002.client — Current User Session Bootstrap

Status: first-stage implemented client session bootstrap / route-guard and tests pending  
Parent slice: `planning/slices/SL-AUTH-002-current-user.md`  
Source scenario: client authentication/session state  
Slice type: client sidecar / session entity/bootstrap  
Current implementation status: implemented provider/query/API mapping/session context; protected route policy and component/E2E coverage not found in current inspection

## 1. Sidecar Overview

Implemented client behavior:

```text
Client app starts
        ↓
AppProviders mount QueryClientProvider + PageErrorProvider + SessionProvider
        ↓
SessionProvider runs current-user query
        ↓
GET /api/l1/auth/current-user
        ↓
 ┌──────────────────────────┬──────────────────────────┐
 │ 200 current user         │ 401 unauthorized          │
 ▼                          ▼
Map response to             Return null session
SessionState
        ↓
Provide sessionContext
        ↓
useSession consumers can read current session or null
```

Current implementation evidence:

```text
energymanagement.client/src/main.tsx
energymanagement.client/src/app/App.tsx
energymanagement.client/src/app/providers/AppProviders.tsx
energymanagement.client/src/entities/session/model/SessionProvider.tsx
energymanagement.client/src/entities/session/model/useSessionQuery.ts
energymanagement.client/src/entities/session/api/getCurrentSession.ts
energymanagement.client/src/entities/session/model/sessionTypes.ts
energymanagement.client/src/entities/session/model/sessionKeys.ts
energymanagement.client/src/entities/session/model/useSession.ts
energymanagement.client/src/shared/api/l1AuthApi.ts
energymanagement.client/src/shared/api/generated/openapi-types.ts
```

Out of scope / not implemented here:

```text
- final protected route guard policy;
- global failure UI for current-user non-401 errors;
- logout UI/cache invalidation policy;
- role-based authorization UI;
- browser E2E evidence for current-user bootstrap.
```

## 2. Sources / Source Behavior Items

Planning/source docs:

```text
planning/slices/SL-AUTH-002-current-user.md
planning/client/cross-cutting/CL-ERROR-HANDLING-001-client-server-errors.md
planning/api/client-server-contract-principles.md
planning/api/openapi-contract-generation.md
```

Source behavior items used by this client sidecar:

```text
Source BI TBD — App checks current user/session state.
Source BI TBD — Authenticated current-user response becomes client SessionState.
Source BI TBD — Unauthorized current-user response becomes guest/null session.
Source BI TBD — Session state is exposed through a client context/hook.
```

## 3. Visual UI / Scenario Flow

```text
┌──────────────────────────────┐
│ Client App                   │
│ starts / renders root        │
└──────────────┬───────────────┘
               ▼
┌──────────────────────────────┐
│ SessionProvider              │
│ asks server who current user is
└──────────────┬───────────────┘
               ▼
┌──────────────────────────────┐
│ Current-user API result      │
└──────────────┬───────────────┘
               │
       ┌───────┴────────┐
       │                │
  authenticated      unauthenticated
       │                │
       ▼                ▼
┌──────────────────────┐   ┌─────────────────────────────┐
│ Provide SessionState │   │ Provide null session         │
│ account/email/role   │   │ guest state                  │
└──────────┬───────────┘   └──────────────┬──────────────┘
           │                              │
           ▼                              ▼
┌────────────────────────────────────────────────────────┐
│ useSession consumers can branch on session/null        │
└────────────────────────────────────────────────────────┘
```

## 4. UI Slice Flow

| Step | UI/client behavior | Implementation evidence | Status |
|---|---|---|---|
| F01 | React app renders `App`. | `main.tsx`, `app/App.tsx` | implemented |
| F02 | App wraps routes with app providers. | `app/providers/AppProviders.tsx` | implemented |
| F03 | Providers include React Query and SessionProvider. | `AppProviders.tsx` | implemented |
| F04 | SessionProvider runs current-user query. | `SessionProvider.tsx`, `useSessionQuery.ts` | implemented |
| F05 | Query calls shared current-user API. | `getCurrentSession.ts`, `l1AuthApi.ts` | implemented |
| F06 | 200 response maps to `SessionState`. | `sessionTypes.ts` | implemented |
| F07 | 401 response maps to `null` session. | `getCurrentSession.ts` | implemented |
| F08 | Non-401 error is rethrown. | `getCurrentSession.ts` | implemented |
| F09 | Session is available through `useSession`. | `SessionProvider.tsx`, `useSession.ts` | implemented |
| F10 | Protected route/redirect policy. | not present as a complete policy in inspected files | planned/gap |

## 5. Visual Client Implementation Flow

```text
┌──────────────────────────────────────────────┐
│ Entry                                        │
│ src/main.tsx                                 │
│ renders <App />                              │
└──────────────────┬───────────────────────────┘
                   ▼
┌──────────────────────────────────────────────┐
│ App Shell                                    │
│ app/App.tsx                                  │
│ AppProviders + RouterProvider                │
└──────────────────┬───────────────────────────┘
                   ▼
┌──────────────────────────────────────────────┐
│ Providers                                    │
│ app/providers/AppProviders.tsx               │
│ QueryClientProvider + PageErrorProvider      │
│ + SessionProvider                            │
└──────────────────┬───────────────────────────┘
                   ▼
┌──────────────────────────────────────────────┐
│ Session Entity Provider                      │
│ entities/session/model/SessionProvider.tsx   │
│ provides SessionState | null                 │
└──────────────────┬───────────────────────────┘
                   ▼
┌──────────────────────────────────────────────┐
│ Session Query                                │
│ entities/session/model/useSessionQuery.ts    │
│ queryKey: entities/session/model/sessionKeys │
└──────────────────┬───────────────────────────┘
                   ▼
┌──────────────────────────────────────────────┐
│ Session API Adapter                          │
│ entities/session/api/getCurrentSession.ts    │
│ 401 -> null, 200 -> SessionState             │
└──────────────────┬───────────────────────────┘
                   ▼
┌──────────────────────────────────────────────┐
│ Shared API                                   │
│ shared/api/l1AuthApi.ts                      │
│ GET /api/l1/auth/current-user                │
└──────────────────┬───────────────────────────┘
                   ▼
┌──────────────────────────────────────────────┐
│ Generated Contract                           │
│ shared/api/generated/openapi-types.ts        │
│ L1CurrentUserResponse                        │
└──────────────────────────────────────────────┘
```

## 6. Client Implementation Flow

| Layer | Responsibility | Current implementation | Status |
|---|---|---|---|
| Entry/App | Mount React application. | `main.tsx`, `App.tsx`. | implemented |
| Providers | Provide QueryClient, page errors and session context. | `AppProviders.tsx`. | implemented |
| Session entity | Own client session state source. | `SessionProvider`, `sessionContext`, `useSession`. | implemented |
| Query model | Fetch current user with stable query key. | `useSessionQuery`, `sessionQueryKey`. | implemented |
| API adapter | Convert 401 into guest/null session. | `getCurrentSession`. | implemented |
| Mapping | Require expected current-user fields. | `mapCurrentUserToSession`. | implemented |
| Shared API | Call current-user endpoint. | `l1AuthApi.getCurrentUser`. | implemented |
| Route guard | Redirect/protect route policy. | not finalized in inspected files. | planned/gap |

## 7. Client API / Generated Contract

| Client API function | Endpoint | Generated OpenAPI type(s) used | Response used? | Error behavior | Status |
|---|---|---|---|---|---|
| `getCurrentUser()` | `GET /api/l1/auth/current-user` | `L1CurrentUserResponse` | mapped to `SessionState` | 401 -> null session; non-401 throws | implemented |

## 8. Questions / Decisions

### Q-AUTH-CURRENT-CLIENT-001 — What is the protected route policy?

Question status: open  
Question: Should protected pages redirect before render, branch inline, or use route loaders/guards?  
Assumption / current direction: Current implementation exposes session context and lets consumers branch; full protected route policy is future work.  
Impact: Affects Account page, request creation, My Requests and E2E.  
Shared register: `planning/slices/slice-questions-register.md / SL-AUTH-CURRENT-CLIENT-Q-001`

### Q-AUTH-CURRENT-CLIENT-002 — How should non-401 current-user failures surface?

Question status: open  
Question: Should 500/network current-user failures show a global error, page error, retry, or guest state?  
Assumption / current direction: Current implementation returns null only for 401 and rethrows other errors.  
Impact: Affects app bootstrap UX and error boundary/page-error behavior.  
Shared register: `planning/slices/slice-questions-register.md / SL-AUTH-Q-005`

### D-AUTH-CURRENT-CLIENT-001 — Unauthorized current user means null session

Question status: accepted direction  
Question: How does client represent unauthenticated current-user?  
Assumption / current direction: Current implementation maps 401 to `null` session.  
Impact: Consumers can branch on `session === null`.  
Shared register: `planning/slices/slice-questions-register.md / SL-AUTH-CURRENT-CLIENT-D-001`

## 9. Behavior Coverage

| Scenario behavior item | How sidecar covers it | Draft/file location | Status |
|---|---|---|---|
| Source BI TBD — App checks current session | SessionProvider runs current-user query. | UI Slice Flow / Implementation Flow | covered |
| Source BI TBD — Authenticated user becomes session | `L1CurrentUserResponse` maps to `SessionState`. | Implementation Flow | covered |
| Source BI TBD — Unauthorized user is guest | 401 maps to `null` session. | API / Decisions | covered |
| Source BI TBD — Client consumers can read session | `useSession` reads session context. | Implementation Flow | covered |
| Protected route redirect behavior | Not finalized as a shared policy. | Questions / Follow-up | not covered |
| Non-401 bootstrap UX | Rethrown, but UX policy not finalized. | Questions / Follow-up | partial |

## 10. Client / Component / E2E Verification Plan

| Test / check | Verifies | Layer | Status |
|---|---|---|---|
| 200 current-user maps to SessionState | Field mapping and required fields. | model/unit | planned/gap |
| 401 current-user returns null | Guest bootstrap behavior. | model/API adapter | planned/gap |
| non-401 current-user error throws | Global error path. | model/API adapter | planned/gap |
| SessionProvider exposes session/null | Provider behavior. | component | planned/gap |
| Login invalidates session query | Auth/session integration. | feature/integration | planned/gap |
| Browser bootstrap after login | Cookie -> current-user -> visible session behavior. | E2E | planned after auth E2E scope decision |

## 11. Dependent / Follow-up Slices

```text
SL-AUTH-001-login-client-account.client.md
SL-AUTH-003-logout.client.md, when concrete logout UI starts
SL-APPL-001-create-individual-applicant-party.client.md
SL-REQ-001-create-connection-request.client.md
My Requests read/list/detail client slices
```

## 12. Implementation Checklist

```text
[x] AppProviders includes QueryClientProvider
[x] AppProviders includes SessionProvider
[x] SessionProvider calls current-user query
[x] current-user query uses shared API
[x] 401 maps to null session
[x] 200 maps to SessionState
[x] useSession hook exists
[ ] protected route policy finalized
[ ] component/model tests found/confirmed
[ ] E2E found/confirmed
```
