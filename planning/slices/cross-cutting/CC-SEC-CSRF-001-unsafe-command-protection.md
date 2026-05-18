# CC-SEC-CSRF-001 — Unsafe Command Protection

Status: umbrella draft / server-client security concern  
Concern type: security / server-client cross-cutting  
Applies to: unsafe state-changing commands  
Scenario source: `planning/diagrams/scenario-cross-cutting/security/CC-SEC-CSRF-001-unsafe-command-protection.behavior.md`

## 1. Purpose

Coordinate server and client slice drafts for CSRF/antiforgery protection of unsafe commands.

This file is not the detailed server or client implementation draft.

## 2. Behavior Source

```text
planning/diagrams/scenario-cross-cutting/security/CC-SEC-CSRF-001-unsafe-command-protection.behavior.md
```

## 3. Scope

```text
unsafe command protection requirement
server rejection of missing/invalid token
no business mutation on rejected unsafe request
normal same-site app command path
client use of CSRF-aware unsafe command boundary
cross-side test proof coordination
```

## 4. Out of Scope

```text
exact antiforgery library choice
exact token storage mechanism
business authorization unrelated to CSRF
domain lifecycle validation
full matrix in every feature slice
```

## 5. Side-Specific Slice Drafts

Server slice:

```text
planning/slices/server/cross-cutting/CC-SEC-CSRF-001-unsafe-command-protection.server.md
```

Client slice:

```text
planning/slices/client/cross-cutting/CC-SEC-CSRF-001-unsafe-command-protection.client.md
```

## 6. Shared Decisions

```text
Each unsafe command family may include one CSRF smoke.
Full CSRF behavior belongs to CC-SEC-CSRF-001 tests.
Do not duplicate the full CSRF matrix in every feature test file.
Server proof and client proof are both needed for full concern coverage.
```

## 7. Cross-Side Behavior-to-Test Trace

| Behavior item | Server proof | Client proof | E2E/user proof if needed | Gap |
|---|---|---|---|---|
| `CC-SEC-CSRF-001-B01` | unsafe command without token is rejected | client uses unsafe command boundary when sending commands | optional smoke through representative command | server proof alone does not prove client sends token |
| `CC-SEC-CSRF-001-B02` | missing/invalid token rejects before domain handler mutates state | not client-owned except surfacing recoverable error | optional | client proof alone does not prove server rejects |
| `CC-SEC-CSRF-001-B03` | DB/no-mutation assertion after rejected unsafe request | not client-owned | not required per command | need representative no-mutation server proof |
| `CC-SEC-CSRF-001-B04` | normal same-site command accepted when token/session valid | command wrapper/fetch boundary attaches/uses protection | one representative E2E if useful | avoid duplicating full matrix everywhere |
| `CC-SEC-CSRF-001-B05` | server returns recognizable auth/security failure | client surfaces recoverable/auth/security error | optional | exact UX belongs to client feedback slice |

## 8. Risks / Open Questions

```text
exact client token refresh/retry behavior may need separate client slice detail.
exact status/problem shape should be confirmed from current server implementation.
```

## 9. Implementation Order

```text
1. Confirm server antiforgery behavior and representative unsafe command.
2. Draft/refresh server CSRF slice.
3. Draft/refresh client CSRF slice.
4. Add representative tests with Behavior-to-Test Trace.
5. Avoid duplicating full CSRF test matrix inside every feature slice.
```

## 10. Current Status

Umbrella coordination only. Side-specific server/client implementation drafts are future unless already present in current repo.
